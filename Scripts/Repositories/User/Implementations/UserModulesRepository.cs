using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserModulesRepository : IUserModulesRepository
{
    public async Task<UserModules> GetUserModulesAsync(string moduleId, string userTable, string objectColumn)
    {
        UserModules userModule = new UserModules();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = $@"
                SELECT {objectColumn},module_id, current_level, current_multiplier
                FROM {userTable}
                WHERE user_id = @user_id AND module_id = @module_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@module_id", moduleId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userModule.Id = reader.GetStringSafe("module_id");
                            userModule.CurrentLevel = reader.GetIntSafe("current_level");
                            userModule.CurrentMultiplier = reader.GetDoubleSafe("current_multiplier");
                        }
                    }
                }
                return userModule;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task InsertOrUpdateUserModulesAsync(string userId, UserModules module, string objectId, string userTable, string objectColumn)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string checkSQL = $@"
            INSERT INTO {userTable} (
                user_id,
                {objectColumn},
                module_id,
                current_level,
                current_multiplier
            )
            VALUES (
                @user_id,
                @object_id,
                @module_id,
                @current_level,
                @current_multiplier
            ) AS new
            ON DUPLICATE KEY UPDATE
                current_level = new.current_level,
                current_multiplier = new.current_multiplier;";

            await using (var insertOrUpdateCommand = new MySqlCommand(checkSQL, connection))
            {
                insertOrUpdateCommand.Parameters.AddWithValue("@user_id", userId);
                insertOrUpdateCommand.Parameters.AddWithValue("@object_id", objectId);
                insertOrUpdateCommand.Parameters.AddWithValue("@module_id", module.Id);
                insertOrUpdateCommand.Parameters.AddWithValue("@current_level", module.CurrentLevel);
                insertOrUpdateCommand.Parameters.AddWithValue("@current_multiplier", module.CurrentMultiplier);

                await insertOrUpdateCommand.ExecuteNonQueryAsync();
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    public async Task<UserModules> GetSumUserModulesAsync(string userId, string objectId, string userTable, string objectColumn)
    {
        UserModules userModules = new UserModules();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = $@"
                SELECT 
                    SUM(current_multiplier) AS total_multiplier
                FROM {userTable}
                WHERE user_id = @user_id AND {objectColumn} = @object_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);
                    selectCommand.Parameters.AddWithValue("@object_id", objectId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userModules.Power = reader.IsDBNull(reader.GetOrdinal("total_multiplier")) ? 0 : reader.GetDoubleSafe("total_multiplier");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return userModules;
    }
}