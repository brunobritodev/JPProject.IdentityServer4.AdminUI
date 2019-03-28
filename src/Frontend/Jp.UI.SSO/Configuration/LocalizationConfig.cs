using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Jp.UI.SSO.Configuration
{
    public static class LocalizationConfig
    {
        public static void AddMvcLocalization(this IServiceCollection services)
        {
            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            services.Configure<RequestLocalizationOptions>(
                            opts =>
                            {
                                var supportedCultures = new[]
                                {
                                    new CultureInfo("pt-BR"),
                                    new CultureInfo("en"),
                                };

                                opts.DefaultRequestCulture = new RequestCulture("en");
                                opts.SupportedCultures = supportedCultures;
                                opts.SupportedUICultures = supportedCultures;
                            });
        }

        public static void UseLocalization(this IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
        }

    }
}
