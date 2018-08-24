using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Equinox.SSO.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Identity Server 4 Single Sign On Server",
                    Description = "Swagger surface",
                    Contact = new Contact { Name = "Bruno Brito", Email = "bhdebrito@gmail.com", Url = "http://www.brunobrito.net.br" },
                    License = new License { Name = "MIT", Url = "https://github.com/brunohbrito/CognitesServicesAzure-Example/blob/master/LICENSE" },

                });
            });

            return services;
        }
    }
}
