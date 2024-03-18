using System.Data.SqlClient;

namespace WebScraper.Entities
{
    internal class DbContext : IDisposable
    {
        private readonly SqlConnection _connection = new(Config.CONN_STR);

        public DbContext()
        {
            _connection.Open();
        }

        public User GetUser(string username, string password)
        {
            try
            {
                if (!Valid.Username(username) || !Valid.Password(password))
                    return new User();

                var query = "SELECT * FROM [dbo].[User] WHERE Username = @Username and Password = @Password";

                var cmd = new SqlCommand(query, _connection);

                Logger.Log(LogType.INFO, $"[DbContext.GetUser] Executing Query: {cmd.CommandText} with @Username: {username}, @Password: {password}");

                username = Service.EncryptString(username);
                password = Service.Hash(password);

                cmd.Parameters.Add(new SqlParameter("@Username", username));
                cmd.Parameters.Add(new SqlParameter("@Password", password));

                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();

                    if (reader["IdUser"] is null)
                        return new User();

                    return new User((int)reader["IdUser"], Service.DecryptString(reader["Username"] + ""), reader["Password"] + "", Service.DecryptString(reader["Name"] + ""));
                }
            }
            catch (Exception e)
            {
                Logger.Log(LogType.ERROR, "[DbContext.GetUser] " + e.StackTrace ?? e.Message);

                return new User();
            }
        }

        public User GetUser(int idUser)
        {
            try
            {
                var query = "SELECT * FROM [dbo].[User] WHERE IdUser = @IdUser";

                var cmd = new SqlCommand(query, _connection);

                cmd.Parameters.Add(new SqlParameter("@IdUser", idUser));

                Logger.Log(LogType.INFO, $"[DbContext.GetUser] Executing Query: {cmd.CommandText} with @IdUser: {idUser}");

                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();

                    if (reader["IdUser"] is null)
                        return new User();

                    return new User((int)reader["IdUser"], Service.DecryptString(reader["Username"] + ""), reader["Password"] + "", Service.DecryptString(reader["Name"] + ""));
                }
            }
            catch (Exception e)
            {
                Logger.Log(LogType.ERROR, "[DbContext.GetUser] " + e.StackTrace ?? e.Message);

                return new User();
            }
        }

        public int AddUser(string username, string password, string name)
        {
            try
            {
                if (!Valid.Username(username) || !Valid.Password(password) || !Valid.Name(name))
                    return -1;

                var query = "INSERT INTO [dbo].[User] (Username, Password, Name) OUTPUT INSERTED.IdUser VALUES (@Username, @Password, @Name)";

                var cmd = new SqlCommand(query, _connection);

                Logger.Log(LogType.INFO, $"[DbContext.AddUser] Executing Query: {cmd.CommandText} with @Username: {username}, @Password: {password}, @Name: {name}");

                username = Service.EncryptString(username);
                password = Service.Hash(password);
                name = Service.EncryptString(name);

                cmd.Parameters.Add(new SqlParameter("@Username", username));
                cmd.Parameters.Add(new SqlParameter("@Password", password));
                cmd.Parameters.Add(new SqlParameter("@Name", name));

                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();

                    if (reader["IdUser"] is null)
                        return -1;

                    return (int)reader["IdUser"];
                }
            }
            catch (Exception e)
            {
                Logger.Log(LogType.ERROR, "[DbContext.AddUser] " + e.StackTrace ?? e.Message);

                return -1;
            }
        }

        public bool UserExists(string username)
        {
            try
            {
                if (!Valid.Username(username))
                    return true;

                var query = "SELECT * FROM [dbo].[User] WHERE Username = @Username";

                var cmd = new SqlCommand(query, _connection);

                Logger.Log(LogType.INFO, $"[DbContext.UserExists] Executing Query: {cmd.CommandText} with @Username: {username}");

                username = Service.EncryptString(username);

                cmd.Parameters.Add(new SqlParameter("@Username", username));

                return cmd.ExecuteScalar() is not null;
            }
            catch (Exception e)
            {
                Logger.Log(LogType.ERROR, "[DbContext.UserExists] " + e.StackTrace ?? e.Message);

                return true;
            }
        }

        public void Dispose()
        {
            _connection.Close();
        }

    }
}
