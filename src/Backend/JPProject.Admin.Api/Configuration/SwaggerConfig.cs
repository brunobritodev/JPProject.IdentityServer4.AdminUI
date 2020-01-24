using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace JPProject.Admin.Api.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "Identity Server 4 User Management API ",
                    Description = "Swagger surface",
                    Contact = new OpenApiContact()
                    {
                        Name = "Bruno Brito",
                        Email = "bhdebrito@gmail.com",
                        Url = new Uri("https://www.brunobrito.net.br")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/brunohbrito/JP-Project/blob/master/LICENSE")
                    },

                });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{configuration["ApplicationSettings:Authority"]}/connect/authorize"),
                            Scopes = new Dictionary<string, string>
                            {
                                {"jp_api.is4", "IS4 Management API - full access"},
                            },
                        }
                    }
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { "jp_api.is4" }
                    }
                });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }
    }
}
