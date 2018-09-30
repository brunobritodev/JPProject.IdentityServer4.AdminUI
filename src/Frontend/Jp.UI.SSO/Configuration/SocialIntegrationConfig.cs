using IdentityModel;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Jp.UI.SSO.Configuration
{
    public static class SocialIntegrationConfig
    {
        public static IServiceCollection AddSocialIntegration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var authBuilder = services.AddAuthentication();

            if (configuration.GetSection("ExternalLogin").GetSection("Google").Exists())
            {
                authBuilder.AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = configuration.GetSection("ExternalLogin").GetSection("Google").GetSection("ClientId").Value;
                    options.ClientSecret = configuration.GetSection("ExternalLogin").GetSection("Google").GetSection("ClientSecret").Value;
                    options.Events = new OAuthEvents
                    {
                        OnCreatingTicket = context =>
                        {
                            var identity = (ClaimsIdentity)context.Principal.Identity;
                            var profileImg = context.User["image"].Value<string>("url");
                            identity.AddClaim(new Claim(JwtClaimTypes.Picture, profileImg));
                            identity.AddClaim(new Claim("IsExternalLogin", "true"));
                            identity.AddClaim(new Claim("ExternalProvider", "Google"));
                            return Task.CompletedTask;
                        }
                    };
                });
            }

            if (configuration.GetSection("ExternalLogin").GetSection("Facebook").Exists())
            {
                authBuilder.AddFacebook("Facebook", options =>
                    {
                        options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                        options.ClientId = configuration.GetSection("ExternalLogin").GetSection("Facebook").GetSection("ClientId").Value;
                        options.ClientSecret = configuration.GetSection("ExternalLogin").GetSection("Facebook").GetSection("ClientSecret").Value;
                        options.Fields.Add("picture");
                        options.Events = new OAuthEvents
                        {
                            OnCreatingTicket = context =>
                            {
                                var identity = (ClaimsIdentity)context.Principal.Identity;
                                var profileImg = context.User["picture"]["data"].Value<string>("url");
                                identity.AddClaim(new Claim(JwtClaimTypes.Picture, profileImg));
                                identity.AddClaim(new Claim("IsExternalLogin", "true"));
                                identity.AddClaim(new Claim("ExternalProvider", "Facebook"));
                                return Task.CompletedTask;
                            }
                        };
                    });
            }

            return services;
        }
    }
}
