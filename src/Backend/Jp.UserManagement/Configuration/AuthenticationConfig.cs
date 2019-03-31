using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jp.Management.Configuration
{
    public static class AuthenticationConfig
    {
        public static void AddIdentityServerAuthentication(this IServiceCollection services, ILogger logger, IConfiguration configuration)
        {
            logger.LogInformation($"Authority URI: {configuration.GetValue<string>("ApplicationSettings:Authority")}");
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            services

                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration.GetValue<string>("ApplicationSettings:Authority");
                    options.RequireHttpsMetadata = false;
                    options.ApiSecret = "Q&tGrEQMypEk.XxPU:%bWDZMdpZeJiyMwpLv4F7d**w9x:7KuJ#fy,E8KPHpKz++";
                    options.ApiName = "jp_api";

                });
        }


    }
}
