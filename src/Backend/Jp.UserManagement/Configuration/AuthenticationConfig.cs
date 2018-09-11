using System;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jp.Management.Configuration
{
    public static class AuthenticationConfig
    {
        public static void AddIdentityServerAuthentication(this IServiceCollection services, ILogger logger)
        {
            var authorityUri = Environment.GetEnvironmentVariable("AUTHORITY") ?? "https://localhost:5000";
            logger.LogInformation($"Authority URI: {authorityUri}");

            services

                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = authorityUri;
                    options.RequireHttpsMetadata = false;
                    options.ApiSecret = "Q&tGrEQMypEk.XxPU:%bWDZMdpZeJiyMwpLv4F7d**w9x:7KuJ#fy,E8KPHpKz++";
                    options.ApiName = "management-api";
                    options.JwtBearerEvents.OnMessageReceived = (messae) =>
                    {
                        messae.Options.TokenValidationParameters.ValidateIssuer = bool.Parse(Environment.GetEnvironmentVariable("VALIDATE_ISSUER") ?? "true");
                        return Task.CompletedTask;
                    };
                });
        }


    }
}
