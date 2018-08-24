using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Equinox.SSO.Configuration
{
    public static class SocialIntegrationConfig
    {
        public static IServiceCollection AddSocialIntegration(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = "371356724436-50s0nucv60p35d2negqi6ra77q2bl77n.apps.googleusercontent.com";
                    options.ClientSecret = "BZSvopBUlKgyFyyWDcJD-3xY";

                });

            return services;
        }
    }
}
