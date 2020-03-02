using Jp.Database;
using JPProject.Domain.Core.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using static Jp.Database.ProviderConfiguration;

namespace Microsoft.Extensions.Configuration
{
    public static class ProviderSelector
    {
        public static IServiceCollection ConfigureProviderForContext<TContext>(
            this IServiceCollection services,
            (DatabaseType, string) options) where TContext : DbContext
        {
            var (database, connString) = options;
            Build(connString);
            return database switch
            {
                DatabaseType.SqlServer => services.PersistStore<TContext>(With.SqlServer),
                DatabaseType.MySql => services.PersistStore<TContext>(With.MySql),
                DatabaseType.Postgre => services.PersistStore<TContext>(With.Postgre),
                DatabaseType.Sqlite => services.PersistStore<TContext>(With.Sqlite),

                _ => throw new ArgumentOutOfRangeException(nameof(database), database, null)
            };
        }

        public static Action<DbContextOptionsBuilder> WithProviderAutoSelection((DatabaseType, string) options)
        {
            var (database, connString) = options;
            Build(connString);
            return database switch
            {
                DatabaseType.SqlServer => With.SqlServer,
                DatabaseType.MySql => With.MySql,
                DatabaseType.Postgre => With.Postgre,
                DatabaseType.Sqlite => With.Sqlite,

                _ => throw new ArgumentOutOfRangeException(nameof(database), database, null)
            };
        }

        public static IIdentityServerBuilder ConfigureContext(this IIdentityServerBuilder builder, (DatabaseType, string) options, IWebHostEnvironment env)
        {
            var (databaseType, connectionString) = options;

            Build(connectionString);
            switch (databaseType)
            {
                case DatabaseType.SqlServer:
                    builder.OAuth2Store(With.SqlServer);
                    break;
                case DatabaseType.MySql:
                    builder.OAuth2Store(With.MySql);
                    break;
                case DatabaseType.Postgre:
                    builder.OAuth2Store(With.Postgre);
                    break;
                case DatabaseType.Sqlite:
                    builder.OAuth2Store(With.Sqlite);
                    break;
            }

            if (env.IsProduction())
                builder.AddConfigurationStoreCache();

            return builder;
        }

    }
}