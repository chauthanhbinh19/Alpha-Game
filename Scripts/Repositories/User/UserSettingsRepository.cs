using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserSettingsRepository : IUserSettingsRepository
{
    public List<UserSettings> GetUserSettings(string userId)
    {
        List<UserSettings> userSettingList = new List<UserSettings>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                string query = "SELECT * FROM user_settings WHERE user_id = @userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserSettings userSetting = new UserSettings
                    {
                        Id = reader.GetInt32("id"),
                        UserId = reader.GetString("user_id"),
                        SettingKey = reader.GetString("setting_key"),
                        SettingValue = reader.GetString("setting_value"),
                        ValueType = reader.GetString("value_type"),
                    };
                    userSettingList.Add(userSetting);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        return userSettingList; // Không tìm thấy
    }
    public void InsertUserSettings(string userId, UserSettings userSetting)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                string query = @"INSERT INTO user_settings
                (user_id, setting_key, setting_value, value_type) 
                Values
                (@user_id, @setting_key, @setting_value, @value_type)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddWithValue("@setting_key", userSetting.SettingKey);
                command.Parameters.AddWithValue("@setting_value", userSetting.SettingValue);
                command.Parameters.AddWithValue("@value_type", userSetting.ValueType);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public void UpdateUserSettings(string userId, UserSettings userSetting)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                string query = @"UPDATE user_settings
                SET setting_value = @setting_value
                WHERE user_id = @user_id and setting_key = @setting_key";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddWithValue("@setting_key", userSetting.SettingKey);
                command.Parameters.AddWithValue("@setting_value", userSetting.SettingValue);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}