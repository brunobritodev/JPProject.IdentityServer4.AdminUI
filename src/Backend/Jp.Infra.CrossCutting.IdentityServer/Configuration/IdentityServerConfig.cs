using Jp.Infra.CrossCutting.Identity.Entities.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jp.Infra.CrossCutting.IdentityServer.Configuration
{
    public static class IdentityServerConfig
    {
        public static IIdentityServerBuilder AddIdentityServer(this IServiceCollection services,
            IConfiguration configuration, IHostingEnvironment environment, ILogger logger)
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

            builder.AddSigninCredentialFromConfig(configuration.GetSection("CertificateOptions"), logger, environment);

            return builder;
        }

    }
}
