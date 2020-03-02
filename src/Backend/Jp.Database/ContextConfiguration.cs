using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Jp.Database
{
    /// <summary>
    /// SqlServer configuration
    /// </summary>
    public static class ContextConfiguration
    {

        /// <summary>
        /// ASP.NET Identity Context config
        /// </summary>
        public static IServiceCollection PersistStore<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> databaseConfig) where TContext : DbContext
        {
            // Add a DbContext to store Keys. SigningCredentials and DataProtectionKeys
            if (services.All(x => x.ServiceType != typeof(TContext)))
                services.AddDbContext<TContext>(databaseConfig);
            return services;
        }

        /// <summary>
        /// IdentityServer4 context config
        /// </summary>
        public static IIdentityServerBuilder OAuth2Store(this IIdentityServerBuilder builder, Action<DbContextOptionsBuilder> databaseConfig)
        {
            builder.AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = databaseConfig;
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = databaseConfig;
                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    //options.TokenCleanupInterval = 15; // frequency in seconds to cleanup stale grants. 15 is useful during debugging
                });

            return builder;
        }


    }
}
