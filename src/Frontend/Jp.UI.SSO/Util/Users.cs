using System;

namespace Jp.UI.SSO.Util
{
    public class Users
    {
        public static string AdminUserName = Environment.GetEnvironmentVariable("DEFAULT_USER") ?? "bruno";
        public static string AdminPassword = Environment.GetEnvironmentVariable("DEFAULT_PASS") ?? "Pa$$word123";
        public static string AdminEmail = Environment.GetEnvironmentVariable("DEFAULT_EMAIL") ?? "bhdebrito@gmail.com";
    }
}
