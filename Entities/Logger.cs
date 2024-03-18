using System.IO;

namespace WebScraper.Entities
{
    internal class Logger
    {
        public static void Log(LogType type, string s)
        {
            if (!Config.LOGGING)
                return;

            if (!Valid.Log(s))
                return;

            using (var sw = new StreamWriter(Config.LOG_FILE, true))
            {
                sw.WriteLine(type + " : " + s);

                sw.Close();
            }
        }
    }
}
