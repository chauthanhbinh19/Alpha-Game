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
        List<UserSettings> userSettings = new List<UserSettings>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM user_settings WHERE user_id = @userId";
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    await using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            UserSettings userSetting = new UserSettings
                            {
                                Id = reader.GetIntSafe("id"),
                                UserId = reader.GetStringSafe("user_id"),
                                SettingKey = reader.GetStringSafe("setting_key"),
                                SettingValue = reader.GetStringSafe("setting_value"),
                                ValueType = reader.GetStringSafe("value_type"),
                            };
                            userSettings.Add(userSetting);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return userSettings;
    }
    public async Task InsertUserSettingAsync(string userId, UserSettings userSetting)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string insertSQL = @"INSERT INTO user_settings
                             (user_id, setting_key, setting_value, value_type) 
                             VALUES
                             (@user_id, @setting_key, @setting_value, @value_type)";
                await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
                {
                    insertCommand.Parameters.AddWithValue("@user_id", userId);
                    insertCommand.Parameters.AddWithValue("@setting_key", userSetting.SettingKey);
                    insertCommand.Parameters.AddWithValue("@setting_value", userSetting.SettingValue);
                    insertCommand.Parameters.AddWithValue("@value_type", userSetting.ValueType);

                    await insertCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task UpdateUserSettingAsync(string userId, UserSettings userSetting)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"UPDATE user_settings
                             SET setting_value = @setting_value
                             WHERE user_id = @user_id AND setting_key = @setting_key";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@setting_key", userSetting.SettingKey);
                    updateCommand.Parameters.AddWithValue("@setting_value", userSetting.SettingValue);

                    await updateCommand.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}