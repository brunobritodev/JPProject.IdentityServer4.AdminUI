using Jp.Infra.CrossCutting.Identity.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Infra.CrossCutting.IdentityServer.Configuration
{
    public static class IdentityServerConfig
    {
        public static IIdentityServerBuilder AddOAuth2(this IServiceCollection services,
            IConfiguration configuration)
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

            builder.AddSigninCredentialFromConfig(configuration.GetSection("CertificateOptions"));

            return builder;
        }

    }
}
