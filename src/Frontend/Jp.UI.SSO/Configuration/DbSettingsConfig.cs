using Jp.Infra.CrossCutting.IdentityServer.Configuration;
using Jp.Infra.CrossCutting.Tools.DefaultConfig;
using Jp.Infra.Migrations.MySql.Identity.Configuration;
using Jp.Infra.Migrations.MySql.IdentityServer.Configuration;
using Jp.Infra.Migrations.Sql.Identity.Configuration;
using Jp.Infra.Migrations.Sql.IdentityServer.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jp.UI.SSO.Configuration
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

        public static void ConfigureIdentityServerDatabase(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment environment, ILogger logger)
        {
            var identityServerBuilder = services.AddIdentityServer(configuration, environment, logger);
            if (JpProjectConfiguration.DatabaseType("MySql"))
                identityServerBuilder.UseIdentityServerMySqlDatabase(services, configuration, logger);
            else
                identityServerBuilder.UseIdentityServerSqlDatabase(services, configuration, logger);
        }
    }
}
