using IdentityServer4.AccessTokenValidation;
using Jp.Infra.CrossCutting.Tools.DefaultConfig;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jp.Management.Configuration
{
    public static class AuthenticationConfig
    {
        public static void AddIdentityServerAuthentication(this IServiceCollection services, ILogger logger)
        {
            logger.LogInformation($"Authority URI: {JpProjectConfiguration.IdentityServerUrl}");
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            services

                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = JpProjectConfiguration.IdentityServerUrl;
                    options.RequireHttpsMetadata = false;
                    options.ApiSecret = "Q&tGrEQMypEk.XxPU:%bWDZMdpZeJiyMwpLv4F7d**w9x:7KuJ#fy,E8KPHpKz++";
                    options.ApiName = "jp_api";
                    
                });
        }


    }
}
