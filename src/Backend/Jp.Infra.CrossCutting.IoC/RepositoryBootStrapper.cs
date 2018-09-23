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
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<JpContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSQLContext>();
        }
    }
}
