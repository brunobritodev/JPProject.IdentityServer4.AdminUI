using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Notifications;
using Equinox.Domain.Interfaces;
using Equinox.Infra.CrossCutting.Identity.Entities.Identity;
using Equinox.Infra.CrossCutting.Identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ServiceStack;

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

        public async Task<bool> CreateUser<TUserId>(IUser<TUserId> user, string password)
        {
            var newUser = new UserIdentity
            {
                Id = Guid.NewGuid(),
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                UserName = user.UserName,
                Name = user.Name
            };

            var result = await _userManager.CreateAsync(newUser, password);
            if (result.Succeeded)
            {
                // User claim for write customers data
                //await _userManager.AddClaimAsync(newUser, new Claim("User", "Write"));
                _logger.LogInformation("User created a new account with password.");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var callbackUrl = $"{_config.GetSection("WebAppUrl").Value}/confirm-email?user={user.Email.UrlEncode()}&code={code.UrlEncode()}";
                await _emailSender.SendEmailConfirmationAsync(user.Email, callbackUrl);

                _logger.LogInformation(3, "User created a new account with password.");
                return true;
            }

            foreach (var error in result.Errors)
            {
                await _bus.RaiseEvent(new DomainNotification(result.ToString(), error.Description));
            }

            return false;
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
    }
}
