using System.Windows;
using System.Windows.Input;
using WebScraper.Entities;

namespace WebScraper
{
    public partial class Scraper : Window
    {
        public Scraper()
        {
            InitializeComponent();

            if (Config.CurrentUser is null || Config.CurrentUser.IdUser == -1)
            {
                new MainWindow().Show();
                Close();
                return;
            }

            Welcome.Content = "Welcome " + Config.CurrentUser.Name;

            KeyUp += (sender, e) =>
            {
                if (e.Key == Key.Enter)
                    Scrape(sender, e);
            };
        }

        private void Scrape(object sender, RoutedEventArgs e)
        {
            Invalid.Content = string.Empty;
            Result.Text = string.Empty;

            var url = Url.Text.Trim();

            if (!Valid.Url(url))
            {
                Invalid.Content = "Invalid Url";
                return;
            }

            var res = Http.Get(url);

            if (string.IsNullOrEmpty(res))
            {
                Invalid.Content = "Couldn't GET from URL";
                return;
            }

            if (chkEmails.IsChecked ?? false)
                PrintResult("EMAILS:\n", Extract.Emails(res));

            if (chkCreditCards.IsChecked ?? false)
                PrintResult("CREDIT CARDS:\n", Extract.CreditCards(res));

            if (chkIps.IsChecked ?? false)
                PrintResult("IP ADDRESSES:\n", Extract.Ips(res));

            if (chkLinks.IsChecked ?? false)
                PrintResult("LINKS:\n", Extract.Links(res));
        }

        private void PrintResult(string head, string[] content)
        {
            Result.Text += head;

            foreach (var s in content)
                Result.Text += "\t" + s + "\n";

            Result.Text += "\n";
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Config.CurrentUser = new();

            new MainWindow().Show();

            Close();
        }
    }
}
