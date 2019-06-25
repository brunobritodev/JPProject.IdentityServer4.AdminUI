using Jp.Infra.CrossCutting.Database;
using Jp.Infra.CrossCutting.IdentityServer.Configuration;
using Jp.Infra.CrossCutting.IoC;
using Jp.UI.SSO.Configuration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jp.UI.SSO
{
    public class Startup
    {
        private readonly ILogger _logger;
        public IConfiguration Configuration { get; }
        private readonly IHostingEnvironment _environment;

        public Startup(IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _logger = logger;
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (environment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }


            Configuration = builder.Build();
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization();

            // Config identity
            services.ConfigureDatabase(Configuration);

            // Add localization
            services.AddMvcLocalization();

            // Configure identity server
            services.AddIdentityServer(Configuration, _environment, _logger).ConfigureIdentityServerDatabase(Configuration);

            // Configure authentication and external logins
            services.AddAuth(Configuration);

            // Configure automapper
            services.AddAutoMapperSetup();

            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // .NET Native DI Abstraction
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseSecurityHeaders(env);
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseLocalization();
            app.UseMvcWithDefaultRoute();
        }


        private void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services, Configuration);
        }
    }

}
