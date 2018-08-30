using Equinox.Application.Interfaces;
using Equinox.Application.Services;
using Equinox.Domain.CommandHandlers;
using Equinox.Domain.Commands;
using Equinox.Domain.Core.Bus;
using Equinox.Domain.Core.Events;
using Equinox.Domain.Core.Notifications;
using Equinox.Domain.EventHandlers;
using Equinox.Domain.Events;
using Equinox.Domain.Events.Customer;
using Equinox.Domain.Interfaces;
using Equinox.Infra.CrossCutting.Bus;
using Equinox.Infra.CrossCutting.Identity.Authorization;
using Equinox.Infra.CrossCutting.Identity.Models;
using Equinox.Infra.CrossCutting.Identity.Services;
using Equinox.Infra.CrossCutting.Tools;
using Equinox.Infra.CrossCutting.Tools.Serializer;
using Equinox.Infra.Data.Context;
using Equinox.Infra.Data.EventSourcing;
using Equinox.Infra.Data.Repository;
using Equinox.Infra.Data.Repository.EventSourcing;
using Equinox.Infra.Data.UoW;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Equinox.Infra.CrossCutting.IoC
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
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<EquinoxContext>();

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