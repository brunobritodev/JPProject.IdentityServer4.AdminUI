using System.IO;
using Equinox.Application.Interfaces;
using Equinox.Domain.Interfaces;
using Equinox.Infra.CrossCutting.Identity.Models;
using Equinox.Infra.CrossCutting.Identity.Services;
using Equinox.Infra.CrossCutting.Tools.CloudServices.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Equinox.Infra.CrossCutting.IoC
{
    internal class IdentityBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Infra - Identity Services
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserManager, UserService>();
            services.AddSingleton<IEmailConfiguration>(config.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddSingleton<IImageStorage, AzureImageStoreService>();

            // Infra - Identity
            services.AddScoped<ISystemUser, AspNetUser>();
        }
    }
}
