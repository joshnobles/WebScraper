namespace WebScraper.Entities
{
    internal class User
    {
        public int IdUser { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public User()
        {
            IdUser = -1;
            Username = string.Empty;
            Password = string.Empty;
            Name = string.Empty;
        }

        public User(int idUser, string username, string password, string name)
        {
            IdUser = idUser;
            Username = username;
            Password = password;
            Name = name;
        }

    }
}
