using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

public class User
{
    public GameObject signInPanel;
    public GameObject namePanel;
    public int id;
    public string name;
    public string image;
    public string border;
    public int level;
    public int experiment;
    public int vip;
    public int power;
    public string Username { get; set; }
    public string Password { get; set; }
    public static int CurrentUserId { get; private set; }
    private static string savedUsername;
    private static string savedPassword;
    public List<Currency> Currencies { get; set; }
    public User(){
        Currencies = new List<Currency>();
    }
    public User(GameObject namepanel)
    {
        this.namePanel = namepanel;
    }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
    public User(string username, string password, GameObject namepanel, GameObject signinpanel)
    {
        Username = username;
        Password = password;
        namePanel = namepanel;
        signInPanel = signinpanel;
    }
    public int RegisterUser()
    {
        string connectionString = DatabaseConfig.ConnectionString;


        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string checkQuery = "Select count(*) from Users WHERE username = @username";
            MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
            checkCommand.Parameters.AddWithValue("@username", Username);
            int count = Convert.ToInt32(checkCommand.ExecuteScalar());
            if (count > 0)
            {
                return 0;
            }
            else
            {
                int maxId = GetMaxId(connection);
                string query = "INSERT INTO users VALUES (@id, @username, @password, @name, @image, @border, @level, @experiment, @vip, @power)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", maxId + 1);
                command.Parameters.AddWithValue("@username", Username);
                command.Parameters.AddWithValue("@password", Password);
                command.Parameters.AddWithValue("@name", "");
                command.Parameters.AddWithValue("@image", "Avatar/valorpass154.png");
                command.Parameters.AddWithValue("@border", "Border/Activity_Border_4.png");
                command.Parameters.AddWithValue("@level", 1);
                command.Parameters.AddWithValue("@experiment", 0);
                command.Parameters.AddWithValue("@vip", 0);
                command.Parameters.AddWithValue("@power", 0);

                try
                {
                    command.ExecuteNonQuery();
                    createUserCurrency(maxId + 1);
                    CurrentUserId=maxId+1;
                    Borders borders = new Borders();
                    borders.InsertUserBordersById(359);
                    borders.InsertBordersGallery(359);
                    PowerManager powerManager = new PowerManager();
                    powerManager.InsertUserStats();
                    Teams team= new Teams();
                    team.InsertUserTeams();
                    Debug.Log("User registered successfully!");
                }
                catch (Exception ex)
                {
                    Debug.LogError("Error while registering user: " + ex.Message);
                }
            }

            connection.Close();
        }
        return 1;
    }
    private int GetMaxId(MySqlConnection connection)
    {
        string query = "SELECT MAX(id) FROM users";
        MySqlCommand command = new MySqlCommand(query, connection);
        object result = command.ExecuteScalar();

        if (result != DBNull.Value)
        {
            return Convert.ToInt32(result);
        }
        return 0; // Nếu bảng rỗng, trả về 0
    }
    public User SignInUser()
    {
        if (string.IsNullOrEmpty(Username)) Username = savedUsername;
        if (string.IsNullOrEmpty(Password)) Password = savedPassword;

        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = "SELECT * FROM Users WHERE username = @username AND password = @password";
            MySqlCommand userCommand = new MySqlCommand(userQuery, connection);
            userCommand.Parameters.AddWithValue("@username", Username);
            userCommand.Parameters.AddWithValue("@password", Password);
            MySqlDataReader reader = userCommand.ExecuteReader();
            if (reader.Read())
            {
                int userId = reader.GetInt32("id");
                string Name = reader.GetString("name");
                string username = reader.GetString("username");
                string password = reader.GetString("password");
                string Image = reader.GetString("image");
                string border = reader.GetString("border");
                int Level = reader.GetInt32("level");
                int Vip = reader.GetInt32("vip");
                int Power = reader.GetInt32("power");
                int Experiment = reader.GetInt32("experiment");

                CurrentUserId = userId;
                savedUsername = username;
                savedPassword = password;

                if (string.IsNullOrEmpty(Name))
                {
                    AuthenticationManager.Instance.createCreateNamePanel();
                    // signInPanel.SetActive(false);
                    return null;
                }
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
                    string image=currencyReader.GetString("image");
                    string name=currencyReader.GetString("name");
                    int currencyId = currencyReader.GetInt32("currency_id");
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

                User user = new User{
                    id = userId,
                    name=Name,
                    Username = username,
                    Password=password,
                    level=Level,
                    vip=Vip,
                    experiment=Experiment,
                    power=Power,
                    image=Image, 
                    border = border,   
                    Currencies = currencies
                };
                // Debug.Log(user);
                return user;
            }
            else
            {
                return null; // Đăng nhập thất bại
            }
        }
    }
    public User GetUserById(int Id)
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
                int userId = reader.GetInt32("id");
                string Name = reader.GetString("name");
                string username = reader.GetString("username");
                string password = reader.GetString("password");
                string Image = reader.GetString("image");
                string border = reader.GetString("border");
                int Level = reader.GetInt32("level");
                int Vip = reader.GetInt32("vip");
                int Power = reader.GetInt32("power");
                int Experiment = reader.GetInt32("experiment");

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
                    string image=currencyReader.GetString("image");
                    string name=currencyReader.GetString("name");
                    int currencyId = currencyReader.GetInt32("currency_id");
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

                User user = new User{
                    id = userId,
                    name=Name,
                    Username = username,
                    Password=password,
                    level=Level,
                    vip=Vip,
                    experiment=Experiment,
                    power=Power,
                    image=Image, 
                    border = border,   
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

    public User UpdateUserName(string newName)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string updateQuery = "UPDATE Users SET name = @name WHERE id = @id";
            MySqlCommand command = new MySqlCommand(updateQuery, connection);
            command.Parameters.AddWithValue("@name", newName);
            command.Parameters.AddWithValue("@id", CurrentUserId);

            command.ExecuteNonQuery();
            // namePanel.SetActive(false);
            AuthenticationManager.Instance.deleteCreateNamePanel();
        }
        return SignInUser();
    }
    public int GetUserId()
    {
        return CurrentUserId;
    }
    public void createUserCurrency(int id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            for (int currencyId = 1; currencyId <= 73; currencyId++) // Vòng lặp từ 1 đến 71
            {
                string updateQuery = "INSERT INTO user_currency (user_id, currency_id, quantity) VALUES (@id, @currency_id, @quantity)";
                MySqlCommand command = new MySqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@currency_id", currencyId);
                command.Parameters.AddWithValue("@quantity", 1000000000);

                command.ExecuteNonQuery();
            }
        }
    }
}
