using IdentityServer4.EntityFramework.Options;
using Jp.Infra.CrossCutting.Identity.Context;
using Jp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jp.Infra.Data.PostgreSQL.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityPostgreSql(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(IdentityConfig).GetTypeInfo().Assembly.GetName().Name;

            var operationalStoreOptions = new OperationalStoreOptions();
            services.AddSingleton(operationalStoreOptions);

            var storeOptions = new ConfigurationStoreOptions();
            services.AddSingleton(storeOptions);

            services.AddEntityFrameworkNpgsql().AddDbContext<ApplicationIdentityContext>(options => options.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));
            services.AddDbContext<JpContext>(options => options.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));
            services.AddDbContext<EventStoreContext>(options => options.UseNpgsql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));

            return services;
        }
    }
}
