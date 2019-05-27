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
            app.UseXfo(options => options.SameOrigin());
            app.UseReferrerPolicy(options => options.NoReferrer());

            app.UseCsp(options =>
            {
                options.DefaultSources(o => o.Self());
                options.FrameSources(o => o.Self()
                    // this custom source can be removed in your build
                    .CustomSources("https://ghbtns.com"));
                options.FrameAncestors(o => o.CustomSources("http:"));
                options.StyleSources(o => o.Self());
                options.ObjectSources(o => o.None());
                options.ImageSources(a =>
                {
                    a.Self();
                    a.CustomSources = new[] { "data: https:" };
                });
                options.FontSources(configuration => configuration.Self().CustomSources("https://fonts.googleapis.com/", "https://fonts.gstatic.com/"));
                options.ConnectSources(s => s.CustomSources("https://dc.services.visualstudio.com"));
                options.ScriptSources(s => s.Self().CustomSources("https://az416426.vo.msecnd.net", @"sha256-ZT3q7lL9GXNGhPTB1Vvrvds2xw/kOV0zoeok2tiV23I="));

            });
        }

    }
}
