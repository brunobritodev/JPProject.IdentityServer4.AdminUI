using System;
using Jp.Infra.CrossCutting.Identity.Context;
using Jp.Infra.CrossCutting.Identity.Entities.Identity;
using Jp.Infra.CrossCutting.IdentityServer.Context;
using Jp.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.UI.SSO.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("SQLSERVER_CONNECTION") ?? configuration.GetConnectionString("SSOConnection");

            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<JpContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<IdentityServerContext>(options => options.UseSqlServer(connectionString));
            services.AddDbContext<EventStoreSQLContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<UserIdentity, UserIdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            return services;
        }
    }
}
