using IdentityModel;
using Jp.Infra.CrossCutting.Identity.Context;
using Jp.Infra.CrossCutting.Identity.Entities.Identity;
using Jp.Infra.Data.MySql.Configuration;
using Jp.Infra.Data.PostgreSQL.Configuration;
using Jp.Infra.Data.Sql.Configuration;
using Jp.Infra.Data.Sqlite.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Infra.CrossCutting.Database
{
    public static class DbSettingsConfig
    {
        public static void AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
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

            services.AddIdentity<UserIdentity, UserIdentityRole>(options =>
                {
                    options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
                    options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Name;
                    options.SignIn.RequireConfirmedAccount = true;
                    options.User.RequireUniqueEmail = true;

                    // NIST Password best practices: https://pages.nist.gov/800-63-3/sp800-63b.html#appA
                    options.Lockout.MaxFailedAccessAttempts = 10;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredUniqueChars = 0;

                })


                .AddEntityFrameworkStores<ApplicationIdentityContext>();
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