using System;
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

        public static bool IsMissing(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsPresent(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }


        public static string AddSpacesToSentence(this string state)
        {
            var text = state.ToCharArray();
            var chars = new char[text.Length + HowManyCapitalizedChars(text)];

            chars[0] = text[0];
            int j = 1;
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                {
                    if (text[i - 1] != ' ' && !char.IsUpper(text[i - 1]) ||
                        (char.IsUpper(text[i - 1]) && i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                    {
                        chars[j++] = ' ';
                        chars[j++] = text[i];
                        continue;
                    }
                }

                chars[j++] = text[i];
            }

            return new string(chars.AsSpan());
        }
        private static int HowManyCapitalizedChars(char[] state)
        {
            var count = 0;
            for (var i = 0; i < state.Length; i++)
            {
                if (char.IsUpper(state[i]))
                    count++;
            }

            return count;
        }
    }
}
