using Jp.Infra.Data.MySql.Configuration;
using Jp.Infra.Data.PostgreSQL.Configuration;
using Jp.Infra.Data.Sql.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Infra.CrossCutting.Database
{
    public static class DbSettingsConfig
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
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
            }
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
            }
        }
    }
}