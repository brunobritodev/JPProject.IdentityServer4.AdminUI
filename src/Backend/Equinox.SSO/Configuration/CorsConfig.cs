using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Equinox.SSO.Configuration
{
    public static class CorsConfig
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Devlopment",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            return services;
        }

        public static void UseDefaultCors(this IApplicationBuilder app)
        {
            app.UseCors("Devlopment");
        }
    }
}
