using IdentityServer4;
using Microsoft.Extensions.DependencyInjection;

namespace Equinox.UI.SSO.Configuration
{
    public static class SocialIntegrationConfig
    {
        public static IServiceCollection AddSocialIntegration(this IServiceCollection services)
        {
            services
                .AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = "434483408261-55tc8n0cs4ff1fe21ea8df2o443v2iuc.apps.googleusercontent.com";
                    options.ClientSecret = "3gcoTrEDPPJ0ukn_aYYT6PWo";
                })
                .AddFacebook("Facebook", options =>
                {

                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = "2205593199670245";
                    options.ClientSecret = "c5646224e92f226ffa49be2e1482d284";
                });

            return services;
        }
    }
}
