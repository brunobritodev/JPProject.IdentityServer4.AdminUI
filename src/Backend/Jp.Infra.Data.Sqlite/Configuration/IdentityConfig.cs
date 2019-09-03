using IdentityServer4.EntityFramework.Options;
using Jp.Infra.CrossCutting.Identity.Context;
using Jp.Infra.CrossCutting.Identity.Entities.Identity;
using Jp.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jp.Infra.Data.Sqlite.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentitySqlite(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(IdentityConfig).GetTypeInfo().Assembly.GetName().Name;

            var operationalStoreOptions = new OperationalStoreOptions();
            services.AddSingleton(operationalStoreOptions);

            var storeOptions = new ConfigurationStoreOptions();
            services.AddSingleton(storeOptions);

            services.AddEntityFrameworkSqlite().AddDbContext<ApplicationIdentityContext>(options => options.UseSqlite(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));
            services.AddDbContext<JpContext>(options => options.UseSqlite(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));
            services.AddDbContext<EventStoreContext>(options => options.UseSqlite(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));

          
            return services;
        }
    }
}
