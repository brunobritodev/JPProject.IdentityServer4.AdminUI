using Jp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Jp.Infra.Data.MySql.Configuration
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddMySql(this IServiceCollection services, string connectionString)
        {
            var migrationsAssembly = typeof(DatabaseConfig).GetTypeInfo().Assembly.GetName().Name;
            services.AddContext(options => options.UseMySql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));

            return services;
        }
    }
}