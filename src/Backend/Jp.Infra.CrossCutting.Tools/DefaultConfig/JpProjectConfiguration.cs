using System;
using System.IO;
using Microsoft.Extensions.Configuration;

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


        public static string UserManagementUrl => $"{Environment.GetEnvironmentVariable("USER_MANAGEMENT_URI") ?? Configuration.GetSection("ApplicationSettings").GetSection("UserManagementURL").Value}";
        public static string IdentityServerAdminUrl => $"{Environment.GetEnvironmentVariable("IS4_MANAGEMENT_URI") ?? Configuration.GetSection("ApplicationSettings").GetSection("Is4ManagementURL").Value}";
        public static string ResourceServer => $"{Environment.GetEnvironmentVariable("RESOURCE_SERVER_URI") ?? Configuration.GetSection("ApplicationSettings").GetSection("ResourceServerURL").Value}";
    }
}
