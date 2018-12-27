using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using System.Collections.Generic;

namespace Jp.UI.SSO.Configuration
{
    public static class SecurityHeadersConfiguration
    {
        public static void UseSecurityHeaders(this IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions()
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (!env.IsDevelopment())
            {
                app.UseHsts(options => options.MaxAge(days: 365));
                app.UseHttpsRedirection();
            }

            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXContentTypeOptions();


            app.UseReferrerPolicy(options => options.NoReferrer());
            var allowCspUrls = new List<string>
            {
                "https://fonts.googleapis.com",
                "https://fonts.gstatic.com",
                "https://res.cloudinary.com",
                "https://jznasafiles.blob.core.windows.net",
                // App Insights
                "https://az416426.vo.msecnd.net",
                "https://dc.services.visualstudio.com/v2/track",
                "https://buttons.github.io"
            };

            app.UseCsp(options =>
            {
                options.DefaultSources(o =>
                {
                    o.SelfSrc = true;
                    o.CustomSources = allowCspUrls;

                });


                options.FrameAncestors(o => o.NoneSrc = true);
                options.ObjectSources(o => o.NoneSrc = true);

                options.ImageSources(a =>
                {
                    a.SelfSrc = true;
                    a.CustomSources = new[] { "data: https:" };
                });
                options.ScriptSources(configuration =>
                {
                    configuration.SelfSrc = true;
                    configuration.CustomSources = new[]
                    {
                        // script APP INSIGHTS
                        "'sha256-ens1+L1QiRof8iQt9GGprsLPJLm7aHpJpMjs/sYNZsQ='",
                        "https://az416426.vo.msecnd.net/scripts/a/ai.0.js",

                        // GitHub buttons
                        "https://buttons.github.io/buttons.js",
                        // Script for redirect after logout
                        "'sha256-v44QeYZ1sjF8Msk4wkn9AbfmXuect8D2JeBtZOoGPo0='",
                        // Script for hybrid flows
                         "'sha256-r43669MWR28/ZEW1fD3aPcmhqe1QnbPzNKwC6Jq5bSw='",
                    };
                    configuration.UnsafeInlineSrc = false;
                    configuration.UnsafeEvalSrc = false;

                });
                options.StyleSources(o =>
                {
                    o.UnsafeInline();
                    o.Self();
                });

            });
        }
    }
}
