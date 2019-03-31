using IdentityModel;
using Jp.Domain.Commands.User;
using Jp.Domain.Commands.UserManagement;
using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Notifications;
using Jp.Domain.Core.ViewModels;
using Jp.Domain.Interfaces;
using Jp.Domain.Models;
using Jp.Infra.CrossCutting.Identity.Entities.Identity;
using Jp.Infra.CrossCutting.Identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Jp.Infra.CrossCutting.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IMediatorHandler _bus;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public UserService(
            UserManager<UserIdentity> userManager,
            IEmailSender emailSender,
            IMediatorHandler bus,
            ILoggerFactory loggerFactory,
            IConfiguration config)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _bus = bus;
            _config = config;
            _logger = loggerFactory.CreateLogger<UserService>(); ;
        }

        public Task<Guid?> CreateUserWithPass(IDomainUser user, string password)
        {
            return CreateUser(user, password, null, null);
        }

        public Task<Guid?> CreateUserWithProvider(IDomainUser user, string provider, string providerUserId)
        {
            return CreateUser(user, null, provider, providerUserId);
        }

        public Task<Guid?> CreateUserWithProviderAndPass(IDomainUser user, string password, string provider, string providerId)
        {
            return CreateUser(user, password, provider, providerId);
        }

        private async Task<Guid?> CreateUser(IDomainUser user, string password, string provider, string providerId)
        {
            var newUser = new UserIdentity
            {
                Id = Guid.NewGuid(),
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                UserName = user.UserName,
                Name = user.Name,
                Picture = user.Picture
            };
            IdentityResult result;

            if (!string.IsNullOrEmpty(provider))
            {
                var userByProvider = await _userManager.FindByLoginAsync(provider, providerId);
                if (userByProvider != null)
                    await _bus.RaiseEvent(new DomainNotification("1001", $"User already taken with {provider}"));
            }

            if (string.IsNullOrWhiteSpace(password))
                result = await _userManager.CreateAsync(newUser);
            else
                result = await _userManager.CreateAsync(newUser, password);

            if (result.Succeeded)
            {
                // User claim for write customers data
                //await _userManager.AddClaimAsync(newUser, new Claim("User", "Write"));

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var callbackUrl = $"{_config.GetValue<string>("ApplicationSettings:UserManagementURL")}/confirm-email?user={user.Email.UrlEncode()}&code={code.UrlEncode()}";
                await _emailSender.SendEmailConfirmationAsync(user.Email, callbackUrl);


                await AddClaims(newUser);

                if (!string.IsNullOrWhiteSpace(provider))
                    await AddLoginAsync(newUser, provider, providerId);


                if (!string.IsNullOrEmpty(password))
                    _logger.LogInformation("User created a new account with password.");

                if (!string.IsNullOrEmpty(provider))
                    _logger.LogInformation($"Provider {provider} associated.");
                return newUser.Id;
            }

            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return null;
        }

        private async Task AddClaims(UserIdentity user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("username", user.UserName));
            claims.Add(new Claim(JwtClaimTypes.Email, user.Email));

            if (!string.IsNullOrEmpty(user.Picture))
                claims.Add(new Claim(JwtClaimTypes.Picture, user.Picture));

            var identityResult = await _userManager.AddClaimsAsync(user, claims);
        }

        public async Task<bool> UsernameExist(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user != null;
        }

        public async Task<bool> EmailExist(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<User> FindByLoginAsync(string provider, string providerUserId)
        {
            var model = await _userManager.FindByLoginAsync(provider, providerUserId);
            return GetUser(model);
        }

        public async Task<Guid?> SendResetLink(string requestEmail, string requestUsername)
        {
            var user = await _userManager.FindByEmailAsync(requestEmail);
            if (user == null)
            {
                // Don't reveal that the user does not exist or is not confirmed
                user = await _userManager.FindByNameAsync(requestUsername);
            }

            if (user == null)
                return null;


            // For more information on how to enable account confirmation and password reset please
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = $"{_config.GetValue<string>("ApplicationSettings:UserManagementURL")}/reset-password?email={user.Email.UrlEncode()}&code={code.UrlEncode()}";

            await _emailSender.SendEmailAsync(user.Email, "Reset Password", $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

            _logger.LogInformation("Reset link sended to user.");
            return user.Id;
        }

        public async Task<Guid?> ResetPassword(ResetPasswordCommand request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return null;
            }

            var result = await _userManager.ResetPasswordAsync(user, request.Code, request.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("Password reseted successfull.");
                return user.Id;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
                }
            }

            return null;
        }

        public async Task<Guid?> ConfirmEmailAsync(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                await _bus.RaiseEvent(new DomainNotification("Email", $"Unable to load user with ID '{email}'."));
                return null;
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return user.Id;

            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return null;
        }



        public async Task<bool> UpdateProfileAsync(UpdateProfileCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.Id.ToString());

            user.Name = command.Name;
            user.Bio = command.Bio;
            user.Company = command.Company;
            user.JobTitle = command.JobTitle;
            user.Url = command.Url;
            user.PhoneNumber = command.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                if (!user.Name.Equals(command.Name))
                    await AddOrUpdateClaimAsync(user, claims, "name", user.Name);

                return true;
            }

            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return false;
        }

        public async Task<bool> UpdateProfilePictureAsync(UpdateProfilePictureCommand command)
        {
            var user = await _userManager.FindByIdAsync(command.Id.ToString());

            user.Picture = command.Picture;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var claims = await _userManager.GetClaimsAsync(user);
                await AddOrUpdateClaimAsync(user, claims, "picture", user.Picture);
                return true;
            }

            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return false;
        }

        private async Task AddOrUpdateClaimAsync(UserIdentity user, IEnumerable<Claim> claims, string key, string value)
        {
            var customClaim = claims.FirstOrDefault(a => a.Type == key);
            if (customClaim != null)
                await _userManager.RemoveClaimAsync(user, customClaim);

            await _userManager.AddClaimAsync(user, new Claim(key, value));
        }

        public async Task<bool> CreatePasswordAsync(SetPasswordCommand request)
        {
            var user = await _userManager.FindByIdAsync(request.Id.Value.ToString());

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (hasPassword)
            {
                /*
                 * DO NOT display the reason.
                 * if this happen is because user are trying to hack.
                 */
                throw new Exception("Unknown error");
            }

            var result = await _userManager.AddPasswordAsync(user, request.Password);
            if (result.Succeeded)
                return true;

            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return false;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordCommand request)
        {
            var user = await _userManager.FindByIdAsync(request.Id.Value.ToString());
            var result = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.Password);
            if (result.Succeeded)
                return true;

            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return false;
        }

        public async Task<bool> RemoveAccountAsync(RemoveAccountCommand request)
        {
            var user = await _userManager.FindByIdAsync(request.Id.Value.ToString());
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return true;

            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return false;
        }

        public async Task<bool> HasPassword(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            return await _userManager.HasPasswordAsync(user);
        }

        public async Task<IEnumerable<User>> GetByIdAsync(params string[] id)
        {
            var users = await _userManager.Users.Where(w => id.Contains(w.Id.ToString())).ToListAsync();

            return users.Select(GetUser).ToList(); ;
        }

        private User GetUser(UserIdentity s)
        {
            if (s == null)
                return null;
            return new User(
                s.Id,
                s.Email,
                s.EmailConfirmed,
                s.Name,
                s.SecurityStamp,
                s.AccessFailedCount,
                s.Bio,
                s.Company,
                s.JobTitle,
                s.LockoutEnabled,
                s.LockoutEnd,
                s.PhoneNumber,
                s.PhoneNumberConfirmed,
                s.Picture,
                s.TwoFactorEnabled,
                s.Url,
                s.UserName
            );
        }

        public async Task<IEnumerable<User>> GetUsers(PagingViewModel paging)
        {
            List<UserIdentity> users = null;
            if (!string.IsNullOrEmpty(paging.Search))
                users = await _userManager.Users.Where(UserFind(paging.Search)).Skip((paging.Page - 1) * paging.Quantity).Take(paging.Quantity).ToListAsync();
            else
                users = await _userManager.Users.Skip((paging.Page - 1) * paging.Quantity).Take(paging.Quantity).ToListAsync();
            return users.Select(GetUser);
        }

        private static Expression<Func<UserIdentity, bool>> UserFind(string search)
        {
            return w => w.UserName.Contains(search) ||
                        w.Email.Contains(search) ||
                        w.Name.Contains(search);
        }

        private async Task<bool> AddLoginAsync(UserIdentity user, string provider, string providerUserId)
        {
            var result = await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, providerUserId, provider));

            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return result.Succeeded;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return GetUser(user);
        }

        public async Task<User> FindByNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return GetUser(user);
        }

        public async Task<User> FindByProviderAsync(string provider, string providerUserId)
        {
            var user = await _userManager.FindByLoginAsync(provider, providerUserId);
            return GetUser(user);
        }

        public async Task<User> GetUserAsync(Guid user)
        {
            var userDb = await _userManager.FindByIdAsync(user.ToString());
            return GetUser(userDb);
        }

        public async Task UpdateUserAsync(User user)
        {
            var userDb = await _userManager.FindByNameAsync(user.UserName);
            userDb.Email = user.Email;
            userDb.EmailConfirmed = user.EmailConfirmed;
            userDb.AccessFailedCount = user.AccessFailedCount;
            userDb.LockoutEnabled = user.LockoutEnabled;
            userDb.LockoutEnd = user.LockoutEnd;
            userDb.Name = user.Name;
            userDb.TwoFactorEnabled = user.TwoFactorEnabled;
            userDb.PhoneNumber = user.PhoneNumber;
            userDb.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            await _userManager.UpdateAsync(userDb);
        }

        public async Task<IEnumerable<Claim>> GetClaimByName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var claims = await _userManager.GetClaimsAsync(user);

            return claims;
        }

        public async Task<bool> SaveClaim(Guid userDbId, Claim claim)
        {
            var user = await _userManager.FindByIdAsync(userDbId.ToString());
            var result = await _userManager.AddClaimAsync(user, claim);

            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return result.Succeeded;
        }

        public async Task<bool> RemoveClaim(Guid userId, string type)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var claims = await _userManager.GetClaimsAsync(user);
            var claimToRemove = claims.First(c => c.Type == type);
            var result = await _userManager.RemoveClaimAsync(user, claimToRemove);

            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return result.Succeeded;
        }

        public async Task<IEnumerable<string>> GetRoles(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> RemoveRole(Guid userDbId, string requestRole)
        {
            var user = await _userManager.FindByIdAsync(userDbId.ToString());
            var result = await _userManager.RemoveFromRoleAsync(user, requestRole);

            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return result.Succeeded;
        }


        public async Task<bool> SaveRole(Guid userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.AddToRoleAsync(user, role);

            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return result.Succeeded;
        }

        public async Task<IEnumerable<UserLogin>> GetUserLogins(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var logins = await _userManager.GetLoginsAsync(user);
            return logins.Select(a => new UserLogin(a.LoginProvider, a.ProviderDisplayName, a.ProviderKey));
        }

        public async Task<bool> RemoveLogin(Guid userId, string loginProvider, string providerKey)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.RemoveLoginAsync(user, loginProvider, providerKey);
            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return result.Succeeded;
        }

        public async Task<IEnumerable<User>> GetUserFromRole(string[] role)
        {
            var users = new List<UserIdentity>();
            foreach (var s in role)
            {
                users.AddRange(await _userManager.GetUsersInRoleAsync(s));
            }
            return users.Select(GetUser);
        }

        public async Task<bool> RemoveUserFromRole(string name, string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var result = await _userManager.RemoveFromRoleAsync(user, name);
            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return result.Succeeded;
        }

        public async Task<bool> ResetPasswordAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            await _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, password);
            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return result.Succeeded;
        }

        public Task<int> Count(string search)
        {
            return !string.IsNullOrEmpty(search) ? _userManager.Users.Where(UserFind(search)).CountAsync() : _userManager.Users.CountAsync();
        }

        public async Task<Guid?> AddLoginAsync(string email, string provider, string providerId)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;

            await AddLoginAsync(user, provider, providerId);

            return user.Id;
        }
    }
}
