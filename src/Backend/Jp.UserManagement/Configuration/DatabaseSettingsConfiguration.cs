using Jp.Infra.Data.MySql.Configuration;
using Jp.Infra.Data.PostgreSQL.Configuration;
using Jp.Infra.Data.Sql.Configuration;
using Jp.Infra.Data.Sqlite.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Management.Configuration
{
    public static class DatabaseSettingsConfiguration
    {
        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var database = configuration["ApplicationSettings:DatabaseType"].ToUpper();
            var connString = configuration.GetConnectionString("SSOConnection");
            switch (database)
            {
                case "MYSQL":
                    services.AddMySql(connString);
                    break;
                case "SQLSERVER":
                    services.AddSqlServer(connString);
                    break;
                case "POSTGRESQL":
                    services.AddPostgreSql(connString);
                    break;
                case "SQLITE":
                    services.AddSqlite(connString);
                    break;
            }

        }

    }
}
