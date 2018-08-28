using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Domain.Interfaces;
using Equinox.Domain.Models;
using Equinox.Infra.CrossCutting.Identity.Entities.Identity;
using Equinox.Infra.CrossCutting.Identity.Extensions;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceStack;
using ServiceStack.Text;

namespace Equinox.Infra.CrossCutting.Identity.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserIdentity> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IMediatorHandler _bus;
        private readonly ILogger _logger;
        private readonly IConfigurationRoot _config;

        public UserService(
            UserManager<UserIdentity> userManager,
            IEmailSender emailSender,
            IMediatorHandler bus,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _bus = bus;
            _logger = loggerFactory.CreateLogger<UserService>(); ;
            _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
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
            if (string.IsNullOrWhiteSpace(password))
                result = await _userManager.CreateAsync(newUser);
            else
                result = await _userManager.CreateAsync(newUser, password);

            if (result.Succeeded)
            {
                // User claim for write customers data
                //await _userManager.AddClaimAsync(newUser, new Claim("User", "Write"));

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var callbackUrl = $"{_config.GetSection("ApplicationSettings").GetSection("UserManagementURL").Value}/confirm-email?user={user.Email.UrlEncode()}&code={code.UrlEncode()}";
                await _emailSender.SendEmailConfirmationAsync(user.Email, callbackUrl);


                await AddClaims(newUser);

                if (!string.IsNullOrWhiteSpace(provider))
                    await AddLoginAsync(newUser, provider, providerId);


                if (!string.IsNullOrEmpty(password))
                    _logger.LogInformation("User created a new account with password.");

                if (!string.IsNullOrEmpty(provider))
                    _logger.LogInformation($"Provider {provider} associated.");
                return user.Id;
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
            claims.Add(new Claim(JwtClaimTypes.Name, user.Name));
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

            return Get(model);
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

            // get the configuration from the app settings
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            // For more information on how to enable account confirmation and password reset please
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = $"{configuration.GetSection("WebAppUrl").Value}/reset-password?email={user.Email.UrlEncode()}&code={code.UrlEncode()}";

            await _emailSender.SendEmailAsync(user.Email, "Reset Password", $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

            _logger.LogInformation("Reset link sended to user.");
            return user.Id;
        }

        public async Task<Guid?> ResetPassword(string email, string requestPassword, string requestCode)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return null;
            }

            var result = await _userManager.ResetPasswordAsync(user, requestCode, requestPassword);

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

        private async Task<bool> AddLoginAsync(UserIdentity user, string provider, string providerUserId)
        {
            var result = await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, providerUserId, provider));

            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return result.Succeeded;
        }


        private User Get(UserIdentity user)
        {
            return JsonSerializer.DeserializeFromString<User>(JsonSerializer.SerializeToString(user));
        }

        public Task<UserIdentity> FindByEmailAsync(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }

        public Task<UserIdentity> FindByNameAsync(string username)
        {
            return _userManager.FindByNameAsync(username);
        }

        public Task<UserIdentity> FindByProviderAsync(string provider, string providerUserId)
        {
            return _userManager.FindByLoginAsync(provider, providerUserId);
        }
    }
}
