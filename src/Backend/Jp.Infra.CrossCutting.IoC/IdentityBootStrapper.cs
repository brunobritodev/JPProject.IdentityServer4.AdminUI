using System.IO;
using Jp.Application.Interfaces;
using Jp.Domain.Interfaces;
using Jp.Infra.CrossCutting.Identity.Models;
using Jp.Infra.CrossCutting.Identity.Services;
using Jp.Infra.CrossCutting.Tools.CloudServices.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Infra.CrossCutting.IoC
{
    internal class IdentityBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration config)
        {
           // Infra - Identity Services
            services.AddSingleton<IEmailConfiguration>(config.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            services.AddTransient<ISmsSender, AuthSMSMessageSender>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddSingleton<IImageStorage, AzureImageStoreService>();

            // Infra - Identity
            services.AddScoped<ISystemUser, AspNetUser>();
        }
    }
}
