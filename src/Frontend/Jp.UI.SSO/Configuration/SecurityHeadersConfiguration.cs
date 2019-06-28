using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;

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
            //app.UseXfo(options => options.Deny());
            app.UseReferrerPolicy(options => options.NoReferrer());

            app.UseCsp(options =>
            {
                options.DefaultSources(o => o.Self());
                options.ObjectSources(o => o.None());
                options.Sandbox(directive => directive.AllowForms().AllowSameOrigin().AllowScripts().AllowPopups());
                options.BaseUris(configuration => configuration.Self());
                options.FrameSources(o => o.Self()
                    // this custom source can be removed in your build
                    .CustomSources("https://ghbtns.com"));

                if (env.IsProduction())
                {
                    options.UpgradeInsecureRequests();
                    // You can set your custom domains here
                    // options.FrameAncestors(o => o.CustomSources());
                }
                options.ImageSources(a =>
                {
                    a.Self();
                    a.CustomSources = new[] { "data: https:" };
                });
                options.FontSources(configuration => configuration.Self().CustomSources("https://fonts.googleapis.com/", "https://fonts.gstatic.com/"));
                options.ConnectSources(s => s.Self().CustomSources("https://dc.services.visualstudio.com"));
                options.ScriptSources(s => s.Self().UnsafeInline().CustomSources("https://az416426.vo.msecnd.net"));

            });


        }

    }
}
