using System.Collections.ObjectModel;
using System.Text;

namespace WebScraper.Entities
{
    internal static class Config
    {
        public const bool LOGGING = true;

        public const string LOG_FILE = "../../../Entities/log.txt";

        // [HOMELAPTOP]: "Data Source=localhost\\SQLEXPRESS;Initial Catalog=WebScraper;User ID=WebScraperSolution;Password=Password1!;TrustServerCertificate=True"
        // [WORKLAPTOP]: "Data Source=AH974032\\SQLEXPRESS;Initial Catalog=WebScraper;User ID=WebScraperSolution;Password=Password1!;"
        public const string CONN_STR = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=WebScraper;User ID=WebScraperSolution;Password=Password1!;TrustServerCertificate=True";

        public static readonly ReadOnlyCollection<byte> ENCRYPT_KEY = Encoding.UTF8.GetBytes("nqKAL2KCk1wgDZhvcpilnDXTJhleNZHb").AsReadOnly();

        public static readonly ReadOnlyCollection<byte> ENCRYPT_IV = Encoding.UTF8.GetBytes("HR$2pI$yr$2pvj92").AsReadOnly();

        // [ADMIN]: Username: admin | Password: Admin1!
        public static User? CurrentUser { get; set; }
    }
}
