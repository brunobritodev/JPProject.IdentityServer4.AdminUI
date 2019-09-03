using IdentityModel;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Jp.UI.SSO.Configuration
{
    public static class FederationGatewayConfig
    {
        public static IServiceCollection AddFederationGateway(this IServiceCollection services,
            IConfiguration configuration)
        {
            var authBuilder = services.AddAuthentication();


            if (configuration.GetSection("ExternalLogin:Google").Exists() &&
                !string.IsNullOrEmpty(configuration.GetSection("ExternalLogin:Google:ClientId").Value) &&
                !string.IsNullOrEmpty(configuration.GetSection("ExternalLogin:Google:ClientSecret").Value))
            {
                authBuilder.AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = configuration.GetValue<string>("ExternalLogin:Google:ClientId");
                    options.ClientSecret = configuration.GetValue<string>("ExternalLogin:Google:ClientSecret");
                    options.Events = new OAuthEvents
                    {
                        OnCreatingTicket = context =>
                        {
                            if (context.User.ContainsKey("image"))
                                context.Identity.AddClaim(new Claim(JwtClaimTypes.Picture, context.User.GetValue("image").SelectToken("url").ToString()));
                            return Task.CompletedTask;
                        }
                    };
                });
            }

            if (configuration.GetSection("ExternalLogin:Facebook").Exists() &&
                !string.IsNullOrEmpty(configuration.GetSection("ExternalLogin:Facebook:ClientId").Value) &&
                !string.IsNullOrEmpty(configuration.GetSection("ExternalLogin:Facebook:ClientSecret").Value))
            {
                authBuilder.AddFacebook("Facebook", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.ClientId = configuration.GetValue<string>("ExternalLogin:Facebook:ClientId");
                    options.ClientSecret = configuration.GetValue<string>("ExternalLogin:Facebook:ClientSecret");
                    options.Fields.Add("picture");
                    options.Events = new OAuthEvents
                    {
                        OnCreatingTicket = context =>
                        {
                            if (context.User.ContainsKey("picture"))
                                context.Identity.AddClaim(new Claim(JwtClaimTypes.Picture, context.User.GetValue("picture").SelectToken("data").SelectToken("url").ToString()));
                            return Task.CompletedTask;
                        }
                    };
                });
            }


            return services;
        }
    }
}
