using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace JPProject.Admin.Api.Configuration
{
    public static class AuthenticationConfig
    {
        public static void ConfigureOAuth2Server(this IServiceCollection services, IConfiguration configuration)
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(o =>
                    {
                        o.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                        o.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    })
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = configuration["ApplicationSettings:Authority"];
                        options.RequireHttpsMetadata = configuration.GetValue<bool>("ApplicationSettings:RequireHttpsMetadata"); ;
                        options.ApiSecret = configuration["ApplicationSettings:ApiSecret"];
                        options.ApiName = configuration["ApplicationSettings:ApiName"];
                    });
        }
    }
}
