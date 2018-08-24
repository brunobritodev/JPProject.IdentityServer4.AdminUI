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
            services
                .AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = "27416902506-r7o9rfmcma3m6gnuck7q5vf1939o3003.apps.googleusercontent.com";
                    options.ClientSecret = "BZ3muDLrCavUcsFPz44hK9-i";

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
