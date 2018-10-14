using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Jp.Infra.CrossCutting.Tools.DefaultConfig
{
    /// <summary>
    /// The main purpouse is to provide Environments or AppSettings config.
    /// </summary>
    public static class JpProjectConfiguration
    {
        private static IConfiguration _configuration;
        private static IConfiguration Configuration => _configuration ?? (_configuration = new ConfigurationBuilder()
                                                           .SetBasePath(Directory.GetCurrentDirectory())
                                                           .AddJsonFile("appsettings.json")
                                                           .Build());


        public static string IdentityServerUrl => $"{Environment.GetEnvironmentVariable("AUTHORITY") ?? Configuration.GetSection("ApplicationSettings").GetSection("Authority").Value}";
        public static string UserManagementUrl => $"{Environment.GetEnvironmentVariable("USER_MANAGEMENT_URI") ?? Configuration.GetSection("ApplicationSettings").GetSection("UserManagementURL").Value}";
        public static string IdentityServerAdminUrl => $"{Environment.GetEnvironmentVariable("IS4_ADMIN_UI") ?? Configuration.GetSection("ApplicationSettings").GetSection("IS4AdminUi").Value}";
        public static string ResourceServer => $"{Environment.GetEnvironmentVariable("RESOURCE_SERVER_URI") ?? Configuration.GetSection("ApplicationSettings").GetSection("ResourceServerURL").Value}";

        public static bool DatabaseType(string type)
        {
            var db = $"{Environment.GetEnvironmentVariable("DATABASE_TYPE") ?? Configuration.GetSection("ApplicationSettings").GetSection("DatabaseType").Value}";
            return db.ToUpper().Equals(type.ToUpper());
        }
    }
}
