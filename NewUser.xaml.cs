using System.Windows;
using System.Windows.Input;
using WebScraper.Entities;

namespace WebScraper
{
    public partial class NewUser : Window
    {
        private DbContext? Context;

        public NewUser()
        {
            InitializeComponent();

            KeyUp += (sender, e) =>
            {
                if (e.Key == Key.Enter)
                    CreateUser(sender, e);
            };
        }

        private void CreateUser(object sender, RoutedEventArgs e)
        {
            Invalid.Content = "";

            int idUser;
            var username = Username.Text.Trim();
            var password = Password.Password.Trim();
            var name = Name.Text.Trim();

            if (!Valid.Username(username) || !Valid.Password(password) || !Valid.Name(name))
            {
                Invalid.Content = "Invalid username, password, or name";
                return;
            }

            using (Context = new())
            {
                if (Context.UserExists(username))
                {
                    Invalid.Content = "User already exists";
                    return;
                }

                idUser = Context.AddUser(username, password, name);

                if (idUser == -1)
                {
                    Invalid.Content = "Couldn't add user";
                    return;
                }

                Config.CurrentUser = Context.GetUser(idUser);
            }

            if (Config.CurrentUser.IdUser == -1)
            {
                Invalid.Content = "Couldn't get user";
                return;
            }

            new Scraper().Show();

            Close();
        }
    }
}
