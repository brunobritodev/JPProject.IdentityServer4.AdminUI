using Jp.Infra.CrossCutting.Tools.DefaultConfig;
using Jp.Infra.Migrations.MySql.Configuration;
using Jp.Infra.Migrations.Sql.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Jp.Management.Configuration
{
    public static class DbSettingsConfig
    {
        public static void ConfigureIdentityDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            if (JpProjectConfiguration.DatabaseType("MySql"))
                services.AddIdentityMySql(configuration);
            else
                services.AddIdentitySqlServer(configuration);
        }
    }
}
