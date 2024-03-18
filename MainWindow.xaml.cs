using System.Windows;
using System.Windows.Input;
using WebScraper.Entities;

namespace WebScraper
{
    public partial class MainWindow : Window
    {
        private DbContext? Context;

        public MainWindow()
        {
            InitializeComponent();
            
            KeyUp += (sender, e) =>
            {
                if (e.Key == Key.Enter)
                    Login(sender, e);
            };
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            Invalid.Content = "";

            var user = new User();

            var username = Username.Text.Trim();
            var password = Password.Password.Trim();

            if (!Valid.Username(username) || !Valid.Password(password))
            {
                Invalid.Content = "Invalid username or password";
                return;
            }

            using (Context = new())
            {
                user = Context.GetUser(username, password);
            }

            if (user.IdUser == -1)
            {
                Invalid.Content = "Invalid username or password";
                return;
            }
            
            Config.CurrentUser = user;

            new Scraper().Show();
            Close();
        }

        private void CreateAccount(object sender, RoutedEventArgs e)
        {
            new NewUser().Show();
            Close();
        }

    }
}