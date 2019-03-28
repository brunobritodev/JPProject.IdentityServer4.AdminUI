using Jp.Domain.Core.Events;
using Jp.Domain.Interfaces;
using Jp.Infra.Data.Context;
using Jp.Infra.Data.EventSourcing;
using Jp.Infra.Data.Repository;
using Jp.Infra.Data.Repository.EventSourcing;
using Jp.Infra.Data.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Infra.CrossCutting.IoC
{
    internal class RepositoryBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IPersistedGrantRepository, PersistedGrantRepository>();
            services.AddScoped<IApiResourceRepository, ApiResourceRepository>();
            services.AddScoped<IApiScopeRepository, ApiScopeRepository>();

            services.AddScoped<IIdentityResourceRepository, IdentityResourceRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientSecretRepository, ClientSecretRepository>();
            services.AddScoped<IApiSecretRepository, ApiSecretRepository>();

            services.AddScoped<IClientClaimRepository, ClientClaimRepository>();
            services.AddScoped<IClientPropertyRepository, ClientPropertyRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<JpContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreContext>();
        }
    }
}
