using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class User
{
    public GameObject signInPanel;
    public GameObject namePanel;
    public string Username { get; set; }
    public string Password { get; set; }
    public static int CurrentUserId { get; private set; }
    public User(GameObject namepanel){
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
        signInPanel=signinpanel;
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
                string query = "INSERT INTO users VALUES (@id, @username, @password, @name, @image, @level, @experiment, @vip, @power)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", maxId + 1);
                command.Parameters.AddWithValue("@username", Username);
                command.Parameters.AddWithValue("@password", Password);
                command.Parameters.AddWithValue("@name", "");
                command.Parameters.AddWithValue("@image", "");
                command.Parameters.AddWithValue("@level", 1);
                command.Parameters.AddWithValue("@experiment", 0);
                command.Parameters.AddWithValue("@vip", 0);
                command.Parameters.AddWithValue("@power", 0);

                try
                {
                    command.ExecuteNonQuery();
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
    public int SignInUser()
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string checkQuery = "Select id,name from Users WHERE username = @username and password = @password";
            MySqlCommand command = new MySqlCommand(checkQuery, connection);
            command.Parameters.AddWithValue("@username", Username);
            command.Parameters.AddWithValue("@password", Password);

            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                int userId = reader.GetInt32(0);
                string userName = reader.GetString(1);
                CurrentUserId = userId;

                if (string.IsNullOrEmpty(userName))
                {
                    namePanel.SetActive(true);
                    signInPanel.SetActive(false);
                    return 2;
                }else{
                    return 1;
                }
            }
            else
            {
                return 0;
            }
        }

    }
    public void UpdateUserName(string newName)
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
            namePanel.SetActive(false);
        }
    }
    public int GetUserId(){
        return CurrentUserId;
    }
}
