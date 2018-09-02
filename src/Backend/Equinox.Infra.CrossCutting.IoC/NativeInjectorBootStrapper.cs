using Jp.Domain.Core.Bus;
using Jp.Domain.Core.Events;
using Jp.Domain.Interfaces;
using Jp.Infra.CrossCutting.Bus;
using Jp.Infra.CrossCutting.Identity.Authorization;
using Jp.Infra.CrossCutting.Tools.Serializer;
using Jp.Infra.Data.Context;
using Jp.Infra.Data.EventSourcing;
using Jp.Infra.Data.Repository.EventSourcing;
using Jp.Infra.Data.UoW;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
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
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<JpContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();

            // Infra - Identity Services
            IdentityBootStrapper.RegisterServices(services);

            // Infra Tools
            // ASP.NET Authorization Polices
            services.AddSingleton<ISerializer, ServiceStackTextSerializer>();
            

        }
    }
}