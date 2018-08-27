using IdentityServer4;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Equinox.UI.SSO.Configuration
{
    public static class SocialIntegrationConfig
    {
        public static IServiceCollection AddSocialIntegration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = configuration.GetSection("ExternalLogin").GetSection("Google").GetSection("ClientId").Value;
                    options.ClientSecret = configuration.GetSection("ExternalLogin").GetSection("Google").GetSection("ClientSecret").Value;
                })
                .AddFacebook("Facebook", options =>
                {

                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = configuration.GetSection("ExternalLogin").GetSection("Facebook").GetSection("ClientId").Value;
                    options.ClientSecret = configuration.GetSection("ExternalLogin").GetSection("Facebook").GetSection("ClientSecret").Value;
                });

            return services;
        }
    }
}
