using Jp.Infra.CrossCutting.Identity.Context;
using Jp.Infra.CrossCutting.Identity.Entities.Identity;
using Jp.Infra.Data.MySql.Configuration;
using Jp.Infra.Data.PostgreSQL.Configuration;
using Jp.Infra.Data.Sql.Configuration;
using Jp.Infra.Data.Sqlite.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Infra.CrossCutting.Database
{
    public static class DbSettingsConfig
    {
        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var database = configuration["ApplicationSettings:DatabaseType"].ToUpper();
            var connString = configuration.GetConnectionString("SSOConnection");
            switch (database)
            {
                case "MYSQL":
                    services.AddIdentityMySql(connString);
                    break;
                case "SQLSERVER":
                    services.AddIdentitySqlServer(connString);
                    break;
                case "POSTGRESQL":
                    services.AddIdentityPostgreSql(connString);
                    break;
                case "SQLITE":
                    services.AddIdentitySqlite(connString);
                    break;
            }

            services.AddIdentity<UserIdentity, UserIdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityContext>()
                .AddDefaultTokenProviders();

            services.UpgradePasswordSecurity().UseArgon2<IdentityUser>();
        }

        public static void ConfigureIdentityServerDatabase(this IIdentityServerBuilder builder, IConfiguration configuration)
        {
            var database = configuration["ApplicationSettings:DatabaseType"].ToUpper();
            var connString = configuration.GetConnectionString("SSOConnection");
            switch (database)
            {
                case "MYSQL":
                    builder.UseIdentityServerMySqlDatabase(connString);
                    break;
                case "SQLSERVER":
                    builder.UseIdentityServerSqlDatabase(connString);
                    break;
                case "POSTGRESQL":
                    builder.UseIdentityServerPostgreSqlDatabase(connString);
                    break;
                case "SQLITE":
                    builder.UseIdentityServerSqlite(connString);
                    break;
            }
        }
    }
}