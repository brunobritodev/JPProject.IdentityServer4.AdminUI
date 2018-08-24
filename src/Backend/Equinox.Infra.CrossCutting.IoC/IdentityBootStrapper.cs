using System.IO;
using Equinox.Domain.Interfaces;
using Equinox.Infra.CrossCutting.Identity.Models;
using Equinox.Infra.CrossCutting.Identity.Services;
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
            services.AddSingleton<IEmailConfiguration>(config.GetSection("EmailConfiguration").Get<EmailConfiguration>());

            // Infra - Identity
            services.AddScoped<ISystemUser, AspNetUser>();
        }
    }
}
