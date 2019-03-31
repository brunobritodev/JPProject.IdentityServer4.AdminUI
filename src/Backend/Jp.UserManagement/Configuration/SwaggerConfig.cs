using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;

namespace Jp.Management.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Identity Server 4 User Management API ",
                    Description = "Swagger surface",
                    Contact = new Contact { Name = "Bruno Brito", Email = "bhdebrito@gmail.com", Url = "http://www.brunobrito.net.br" },
                    License = new License { Name = "MIT", Url = "https://github.com/brunohbrito/JP-Project/blob/master/LICENSE" },

                });

                options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Flow = "implicit",
                    AuthorizationUrl = $"{configuration.GetValue<string>("ApplicationSettings:Authority")}/connect/authorize",
                    Scopes = new Dictionary<string, string> {
                        { "jp_api.user", "User Management API - full access" },
                        { "jp_api.is4", "IS4 Management API - full access" },
                    }
                });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }
    }
}
