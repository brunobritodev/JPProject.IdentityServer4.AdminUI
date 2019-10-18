using Jp.Infra.CrossCutting.Identity.Entities.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jp.Infra.CrossCutting.IdentityServer.Configuration
{
    public static class IdentityServerConfig
    {
        public static IIdentityServerBuilder AddOAuth2(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment env)
        {

            var builder = services.AddIdentityServer(
                    options =>
                    {
                        options.Events.RaiseErrorEvents = true;
                        options.Events.RaiseInformationEvents = true;
                        options.Events.RaiseFailureEvents = true;
                        options.Events.RaiseSuccessEvents = true;
                    })
                .AddAspNetIdentity<UserIdentity>();
            if (!env.IsProduction())
                builder.AddDeveloperSigningCredential();
            else
                builder.AddSigninCredentialFromConfig(configuration.GetSection("CertificateOptions"));

            return builder;
        }

    }
}
