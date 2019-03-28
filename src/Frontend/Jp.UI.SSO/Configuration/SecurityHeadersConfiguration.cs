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

            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXContentTypeOptions();

            app.UseXfo(options => options.Deny());
            app.UseReferrerPolicy(options => options.NoReferrer());

            app.UseCsp(options =>
            {
                options.DefaultSources(o => o.Self());

                options.FrameAncestors(o => o.None());
                options.StyleSources(o => o.None());
                options.ObjectSources(o => o.None());
                options.ImageSources(a =>
                {
                    a.Self();
                    a.CustomSources = new[] { "data: https:" };
                });
                options.FontSources(configuration =>
                {
                    configuration.SelfSrc = true;
                    configuration.CustomSources("https://fonts.googleapis.com/", "https://fonts.gstatic.com/");
                });
                options.ConnectSources(s => s.CustomSources("https://dc.services.visualstudio.com"));
                options.ScriptSources(s => s.Self().CustomSources("https://az416426.vo.msecnd.net"));

                // Can be removed in your own build
                options.ChildSources(s => s.CustomSources("https://ghbtns.com"));
            });
        }

    }
}
