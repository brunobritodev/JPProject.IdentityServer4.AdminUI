using IdentityServer4.EntityFramework.Options;
using Jp.Infra.CrossCutting.Identity.Context;
using Jp.Infra.CrossCutting.Identity.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Jp.Infra.Data.Context;

namespace Jp.Infra.Migrations.MySql.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityMySql(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("CUSTOMCONNSTR_DATABASE_CONNECTION") ?? configuration.GetConnectionString("SSOConnection");
            var migrationsAssembly = typeof(IdentityConfig).GetTypeInfo().Assembly.GetName().Name;

            var operationalStoreOptions = new OperationalStoreOptions();
            services.AddSingleton(operationalStoreOptions);

            var storeOptions = new ConfigurationStoreOptions();
            services.AddSingleton(storeOptions);

            services.AddEntityFrameworkMySql().AddDbContext<ApplicationIdentityContext>(options => options.UseMySql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));
            services.AddDbContext<JpContext>(options => options.UseMySql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));
            services.AddDbContext<EventStoreContext>(options => options.UseMySql(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly)));

            services.AddIdentity<UserIdentity, UserIdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityContext>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
