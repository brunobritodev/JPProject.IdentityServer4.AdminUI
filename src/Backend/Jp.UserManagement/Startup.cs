using System;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using Jp.Infra.CrossCutting.IoC;
using Jp.UserManagement.Configuration;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.UserManagement
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostEnvironment { get; }

        public Startup(IHostingEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            HostEnvironment = hostEnvironment;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddIdentity(Configuration);
            services.ConfigureCors();


            //services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    
                })
                    .AddIdentityServerAuthentication(options =>
                                                    {
                                                        options.Authority = Environment.GetEnvironmentVariable("AUTHORITY") ?? "http://localhost:5000";
                                                        options.RequireHttpsMetadata = false;
                                                        options.ApiSecret = "Q&tGrEQMypEk.XxPU:%bWDZMdpZeJiyMwpLv4F7d**w9x:7KuJ#fy,E8KPHpKz++";
                                                        options.ApiName = "UserManagementApi";


                                                        options.JwtBearerEvents.OnMessageReceived = (messae) =>
                                                        {
                                                            messae.Options.TokenValidationParameters.ValidateIssuer = bool.TryParse(Environment.GetEnvironmentVariable("VALIDATE_ISSUER") ?? "true", out _);
                                                            return Task.CompletedTask;
                                                        };
                                                    });

            services.AddSwagger();

            // Config automapper
            services.AddAutoMapperSetup();
            // Adding MediatR for Domain Events and Notifications
            services.AddMediatR(typeof(Startup));

            // .NET Native DI Abstraction
            RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDefaultCors();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseAuthentication();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ID4 User Management");
                c.OAuthClientId("Swagger");
                c.OAuthAppName("User Management UI - full access");
            });
            app.UseMvc();
        }


        private void RegisterServices(IServiceCollection services)
        {
            // Adding dependencies from another layers (isolated from Presentation)
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
