using Microsoft.Extensions.Configuration;

namespace Jp.UI.SSO.Util
{
    public static class Users
    {
        public static string GetUser(IConfiguration configuration)
        {
            return configuration.GetValue<string>("ApplicationSettings:DefaultUser") ?? "bruno";
        }
        public static string GetPassword(IConfiguration configuration)
        {
            return configuration.GetValue<string>("ApplicationSettings:DefaultPass") ?? "Pa$$word123";
        }
        public static string GetEmail(IConfiguration configuration)
        {
            return configuration.GetValue<string>("ApplicationSettings:DefaultEmail") ?? "bhdebrito@gmail.com";
        }
    }
}
