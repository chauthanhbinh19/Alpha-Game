using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserRepository : IUserRepository
{
    public User GetUserByUsername(string username)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT * FROM users WHERE username = @username";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@username", username);

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new User
                    {
                        id = reader["id"].ToString(),
                        Username = reader.GetString("username"),
                        Password = reader.GetString("password"),
                        name = reader["name"].ToString(),
                        level = Convert.ToInt32(reader["level"]),
                        experiment = Convert.ToInt32(reader["experiment"]),
                        vip = Convert.ToInt32(reader["vip"]),
                        power = Convert.ToInt32(reader["power"])
                    };
                }
            }
        }

        return null; // Không tìm thấy
    }

    public string RegisterUser(string username, string password)
    {
        string connectionString = DatabaseConfig.ConnectionString;


        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string checkQuery = "Select count(*) from Users WHERE username = @username";
            MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@username", username);
            int count = Convert.ToInt32(checkCommand.ExecuteScalar());
            if (count > 0)
            {
                return null;
            }
            else
            {
                string userId = DateTime.Now.Ticks.ToString();
                string query = "INSERT INTO users VALUES (@id, @username, @password, @name, @level, @experiment, @vip, @power)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", userId);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@name", "");
                command.Parameters.AddWithValue("@level", 1);
                command.Parameters.AddWithValue("@experiment", 0);
                command.Parameters.AddWithValue("@vip", 0);
                command.Parameters.AddWithValue("@power", 0);

                try
                {
                    command.ExecuteNonQuery();

                    Debug.Log("User registered successfully!");
                    return userId;
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error while registering user: " + ex.Message);
                }
            }

            connection.Close();
        }
        return "";
    }
    // private int GetMaxId(MySqlConnection connection)
    // {
    //     string query = "SELECT MAX(id) FROM users";
    //     MySqlCommand command = new MySqlCommand(query, connection);
    //     object result = command.ExecuteScalar();

    //     if (result != DBNull.Value)
    //     {
    //         return Convert.ToInt32(result);
    //     }
    //     return 0; // Nếu bảng rỗng, trả về 0
    // }
    public User SignInUser(string username, string password)
    {
        if (string.IsNullOrEmpty(username)) username = User.savedUsername;
        if (string.IsNullOrEmpty(password)) password = User.savedPassword;

        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = "SELECT * FROM Users WHERE username = @username AND password = @password";
            MySqlCommand userCommand = new MySqlCommand(userQuery, connection);
            userCommand.Parameters.AddWithValue("@username", username);
            userCommand.Parameters.AddWithValue("@password", password);
            MySqlDataReader reader = userCommand.ExecuteReader();
            if (reader.Read())
            {
                string userId = reader.GetString("id");
                string Name = reader.GetString("name");
                string Username = reader.GetString("username");
                string Password = reader.GetString("password");
                int Level = reader.GetInt32("level");
                int Vip = reader.GetInt32("vip");
                int Power = reader.GetInt32("power");
                int Experiment = reader.GetInt32("experiment");

                User.CurrentUserId = userId;
                User.savedUsername = Username;
                User.savedPassword = Password;
                User.CurrentUserLevel = Level;

                reader.Close();

                string currencyQuery = "SELECT c.image, c.name, uc.currency_id, uc.quantity FROM user_currency uc, currency c WHERE user_id = @userId and uc.currency_id=c.id";
                MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@userId", userId);

                MySqlDataReader currencyReader = currencyCommand.ExecuteReader();

                List<Currency> currencies = new List<Currency>();
                while (currencyReader.Read())
                {
                    string image = currencyReader.GetString("image");
                    string name = currencyReader.GetString("name");
                    string currencyId = currencyReader.GetString("currency_id");
                    int quantity = currencyReader.GetInt32("quantity");
                    currencies.Add(new Currency
                    {
                        id = currencyId,
                        name = name,
                        image = image,
                        quantity = quantity
                    });
                }
                currencyReader.Close();

                User user = new User
                {
                    id = userId,
                    name = Name,
                    Username = username,
                    Password = password,
                    level = Level,
                    vip = Vip,
                    experiment = Experiment,
                    power = Power,
                    image = "",
                    border = "",
                    Currencies = currencies
                };
                // Debug.Log(user.name);
                return user;
            }
            else
            {
                return null;
            }
        }
    }
    public User GetUserById(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = "SELECT * FROM Users WHERE id=@id";
            MySqlCommand userCommand = new MySqlCommand(userQuery, connection);
            userCommand.Parameters.AddWithValue("@id", Id);
            MySqlDataReader reader = userCommand.ExecuteReader();
            if (reader.Read())
            {
                string userId = reader.GetString("id");
                string Name = reader.GetString("name");
                string username = reader.GetString("username");
                string password = reader.GetString("password");
                int Level = reader.GetInt32("level");
                int Vip = reader.GetInt32("vip");
                // int Power = reader.GetInt32("power");
                int Experiment = reader.GetInt32("experiment");

                double Power = TeamsService.Create().GetTeamsPower(Id);
                // Đóng `reader` trước khi thực hiện truy vấn tiếp theo
                reader.Close();

                // Lấy thông tin từ bảng `user_currency`
                string currencyQuery = "SELECT c.image, c.name, uc.currency_id, uc.quantity FROM user_currency uc, currency c WHERE user_id = @userId and uc.currency_id=c.id";
                MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@userId", userId);

                MySqlDataReader currencyReader = currencyCommand.ExecuteReader();

                List<Currency> currencies = new List<Currency>();
                while (currencyReader.Read())
                {
                    string image = currencyReader.GetString("image");
                    string name = currencyReader.GetString("name");
                    string currencyId = currencyReader.GetString("currency_id");
                    int quantity = currencyReader.GetInt32("quantity");
                    currencies.Add(new Currency
                    {
                        id = currencyId,
                        name = name,
                        image = image,
                        quantity = quantity
                    });
                }
                currencyReader.Close();

                User user = new User
                {
                    id = userId,
                    name = Name,
                    Username = username,
                    Password = password,
                    level = Level,
                    vip = Vip,
                    experiment = Experiment,
                    power = Power,
                    image = "",
                    border = "",
                    Currencies = currencies
                };
                return user;
            }
            else
            {
                return null; // Đăng nhập thất bại
            }
        }
    }
    public void UpdateUserName(string user_id, string new_name)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string updateQuery = "UPDATE Users SET name = @name WHERE id = @id";
            MySqlCommand command = new MySqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@name", new_name);
            command.Parameters.AddWithValue("@id", user_id);

            command.ExecuteNonQuery();
            // namePanel.SetActive(false);
            AuthenticationManager.Instance.deleteCreateNamePanel();
        }
    }
    public void createUserCurrency(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            for (int currencyId = 1; currencyId <= 73; currencyId++) // Vòng lặp từ 1 đến 71
            {
                string updateQuery = "INSERT INTO user_currency (user_id, currency_id, quantity) VALUES (@id, @currency_id, @quantity)";
                MySqlCommand command = new MySqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@currency_id", currencyId);
                command.Parameters.AddWithValue("@quantity", 1000000000);

                command.ExecuteNonQuery();
            }
        }
    }
}