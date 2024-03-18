using System.Text.RegularExpressions;

namespace WebScraper.Entities
{
    internal class Extract
    {
        public static string[] Emails(string s)
        {
            return Find(s, @"(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})");
        }

        public static string[] CreditCards(string s)
        {
            return Find(s, @"[0-9]{4}[- ]?[0-9]{4}[- ]?[0-9]{4}[- ]?[0-9]{4}");
        }

        public static string[] Ips(string s)
        {
            return Find(s, @"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
        }

        public static string[] Links(string s)
        {
            return Find(s, "href=\"(.*?)\"");
        }

        private static string[] Find(string s, string regex)
        {
            var res = new List<string>();

            var rx = new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rx.Matches(s);

            foreach (Match match in matches.Cast<Match>())
                res.Add(match.Value);

            return [.. res];
        }

    }
}
