using System.Text.RegularExpressions;

namespace WebScraper.Entities
{
    internal class Valid
    {
        public static bool Log(string s)
        {
            if (string.IsNullOrWhiteSpace(s) || s.Length > 512)
                return false;

            return true;
        }

        public static bool Username(string s)
        {
            if (string.IsNullOrWhiteSpace(s) || s.Length > 512 || s.Length < 4 || s[0] == '-' || s[0] == '@')
                return false;

            var pattern = @"^[a-zA-Z0-9_.@-]*$";
            var reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(s.ToLower());
        }

        public static bool Password(string s)
        {
            if (string.IsNullOrWhiteSpace(s) || s.Length > 512 || s.Length < 5 || s[0] == '\'' || s[0] == '"' || s[0] == '\\' || s[0] == '%' || s[0] == '&' || s[0] == ';' || s.Contains("--"))
                return false;

            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{5,512}$";
            var reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(s.ToLower());
        }

        public static bool Name(string s)
        {
            if (string.IsNullOrWhiteSpace(s) || s.Length > 512 || s.Length < 3 || s[0] == '\'' || s[0] == '-' || s.Contains("--"))
                return false;

            var pattern = @"^[A-Za-z '-]*$";
            var reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(s.ToLower());
        }

        public static bool Url(string s)
        {
            if (s.Length < 5 || s.Length > 256)
                return false;

            string pattern = @"^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$";
            var reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return reg.IsMatch(s.ToLower());
        }

    }
}
