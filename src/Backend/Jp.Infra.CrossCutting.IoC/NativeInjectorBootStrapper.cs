using Jp.Domain.Core.Bus;
using Jp.Domain.Interfaces;
using Jp.Infra.CrossCutting.Bus;
using Jp.Infra.CrossCutting.Identity.Authorization;
using Jp.Infra.CrossCutting.Tools.Serializer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // ASP.NET Authorization Polices
            services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();

            // Application
            ApplicationBootStrapper.RegisterServices(services);

            // Domain - Events
            DomainEventsBootStrapper.RegisterServices(services);

            // Domain - Commands
            DomainCommandsBootStrapper.RegisterServices(services);

            // Infra - Data
            RepositoryBootStrapper.RegisterServices(services);
            

            // Infra - Identity Services
            IdentityBootStrapper.RegisterServices(services, configuration);

            // Infra Tools
            // ASP.NET Authorization Polices
            services.AddSingleton<ISerializer, ServiceStackTextSerializer>();
            

        }
    }
}