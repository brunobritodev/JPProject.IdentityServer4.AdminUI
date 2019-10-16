using System.Text.RegularExpressions;

namespace Jp.Domain.Core.StringUtils
{
    public static class StringExtensions
    {
        public static bool IsEmail(this string username)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(username, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

    }
}
