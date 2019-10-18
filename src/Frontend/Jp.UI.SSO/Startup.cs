using Jp.Infra.CrossCutting.Database;
using Jp.Infra.CrossCutting.Identity.Entities.Identity;
using Jp.Infra.CrossCutting.IdentityServer.Configuration;
using Jp.Infra.CrossCutting.IoC;
using Jp.UI.SSO.Configuration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Jp.UI.SSO
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });
            services.AddControllersWithViews()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization();
            services.AddRazorPages();

            // The following line enables Application Insights telemetry collection.
            services.AddApplicationInsightsTelemetry();

            // Config identity
            services.AddIdentityConfiguration(Configuration);

            // Add localization
            services.AddMvcLocalization();

            // Configure identity server
            services.AddOAuth2(Configuration, _env).ConfigureIdentityServerDatabase(Configuration);

            // Improve password security
            services.UpgradePasswordSecurity().UseArgon2<UserIdentity>();

            // Configure Federation gateway (external logins), such as Facebook, Google etc
            services.AddFederationGateway(Configuration);

            // Configure automapper
            services.AddAutoMapperSetup();

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // .NET Native DI Abstraction
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Only use HTTPS redirect in Production Ambients
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts(options => options.MaxAge(days: 365));
                app.UseHttpsRedirection();
            }

            app.UseSerilogRequestLogging();
            app.UseSecurityHeaders(env);
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseLocalization();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            NativeInjectorBootStrapper.RegisterServices(services, Configuration);
        }
    }

}
