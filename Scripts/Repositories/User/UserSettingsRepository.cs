using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserSettingsRepository : IUserSettingsRepository
{
    public async Task<List<UserSettings>> GetUserSettingsAsync(string userId)
    {
        List<UserSettings> userSettingList = new List<UserSettings>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = "SELECT * FROM user_settings WHERE user_id = @userId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
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
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return userSettingList;
    }
    public async Task InsertUserSettingAsync(string userId, UserSettings userSetting)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO user_settings
                             (user_id, setting_key, setting_value, value_type) 
                             VALUES
                             (@user_id, @setting_key, @setting_value, @value_type)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@setting_key", userSetting.SettingKey);
                    command.Parameters.AddWithValue("@setting_value", userSetting.SettingValue);
                    command.Parameters.AddWithValue("@value_type", userSetting.ValueType);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error inserting user setting: " + ex.Message);
            }
        }
    }
    public async Task UpdateUserSettingAsync(string userId, UserSettings userSetting)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"UPDATE user_settings
                             SET setting_value = @setting_value
                             WHERE user_id = @user_id AND setting_key = @setting_key";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", userId);
                    command.Parameters.AddWithValue("@setting_key", userSetting.SettingKey);
                    command.Parameters.AddWithValue("@setting_value", userSetting.SettingValue);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error updating user setting: " + ex.Message);
            }
        }
    }
}