using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            if (!env.IsDevelopment())
            {
                app.UseHsts(options => options.MaxAge(days: 365));
                app.UseHttpsRedirection();
            }

            app.UseXXssProtection(options => options.EnabledWithBlockMode());
            app.UseXContentTypeOptions();

            app.UseXfo(options => options.Deny());
            app.UseReferrerPolicy(options => options.NoReferrer());
            var allowCspUrls = new List<string>
            {
                "https://fonts.googleapis.com/",
                "https://fonts.gstatic.com/"
            };

            app.UseCsp(options =>
            {
                options.DefaultSources(o => o.SelfSrc = true);
                options.FrameAncestors(o => o.NoneSrc = true);
                options.ObjectSources(o => o.NoneSrc = true);
                options.ImageSources(a =>
                {
                    a.SelfSrc = true;
                    a.CustomSources = new[] { "data: https:" };
                });
                options.FontSources(configuration =>
                {
                    configuration.SelfSrc = true;
                    configuration.CustomSources = allowCspUrls;
                });

                options.ScriptSources(configuration =>
                {
                    configuration.SelfSrc = true;
                    configuration.CustomSources = new[] { "'sha256-iMxJ7OVhtXNAJK8UhwgDeXu0BTuJ/ARay62Lmqs61F0='", "'sha256-v44QeYZ1sjF8Msk4wkn9AbfmXuect8D2JeBtZOoGPo0='", "'sha256-VuNUSJ59bpCpw62HM2JG/hCyGiqoPN3NqGvNXQPU+rY='" };
                    configuration.UnsafeInlineSrc = false;
                    configuration.UnsafeEvalSrc = false;
                });

                options.StyleSources(configuration =>
                {
                    configuration.SelfSrc = true;
                    configuration.CustomSources = allowCspUrls;
                    configuration.UnsafeInlineSrc = true;
                });

            });
        }
    }
}
