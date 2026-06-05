using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserUpgradesRepository : IUserUpgradesRepository
{
    public async Task<UserUpgrades> GetUserUpgradesAsync(string upgradeId, string userTable, string objectColumn)
    {
        UserUpgrades userUpgrade = new UserUpgrades();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = $@"
                SELECT {objectColumn},upgrade_id, current_level, current_multiplier
                FROM {userTable}
                WHERE user_id = @user_id AND upgrade_id = @upgrade_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@upgrade_id", upgradeId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userUpgrade.Id = reader.GetStringSafe("upgrade_id");
                            userUpgrade.CurrentLevel = reader.GetIntSafe("current_level");
                            userUpgrade.CurrentMultiplier = reader.GetDoubleSafe("current_multiplier");
                        }
                    }
                }
                return userUpgrade;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task InsertOrUpdateUserUpgradesAsync(string userId, UserUpgrades upgrade, string objectId, string userTable, string objectColumn)
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
                upgrade_id,
                current_level,
                current_multiplier
            )
            VALUES (
                @user_id,
                @object_id,
                @upgrade_id,
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
                insertOrUpdateCommand.Parameters.AddWithValue("@upgrade_id", upgrade.Id);
                insertOrUpdateCommand.Parameters.AddWithValue("@current_level", upgrade.CurrentLevel);
                insertOrUpdateCommand.Parameters.AddWithValue("@current_multiplier", upgrade.CurrentMultiplier);

                await insertOrUpdateCommand.ExecuteNonQueryAsync();
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    public async Task<UserUpgrades> GetSumUserUpgradesAsync(string userId, string objectId, string userTable, string objectColumn)
    {
        UserUpgrades userUpgrades = new UserUpgrades();
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
                            userUpgrades.Power = reader.IsDBNull(reader.GetOrdinal("total_multiplier")) ? 0 : reader.GetDoubleSafe("total_multiplier");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return userUpgrades;
    }
}