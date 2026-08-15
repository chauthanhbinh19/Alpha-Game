using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UpgradesRepository : IUpgradesRepository
{
    public async Task<Upgrades> GetUpgradeByIdAsync(string id)
    {
        Upgrades upgrade = new Upgrades();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM upgrades where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    upgrade = new Upgrades{
                        Id = reader.GetStringSafe("id"),
                        Name = reader.GetStringSafe("name"),
                        BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                        MaxLevel = reader.GetIntSafe("max_level"),
                    };
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

        return upgrade;
    }
    public async Task<InsertOrUpdateResult<Upgrades>> InsertUpgradeAsync(Upgrades upgrade)
    {
        if (upgrade == null || string.IsNullOrEmpty(upgrade.Id))
        {
            return InsertOrUpdateResult<Upgrades>.Failure("Dữ liệu Upgrade hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO upgrades (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", upgrade.Id);
            command.Parameters.AddWithValue("@name", upgrade.Name);
            command.Parameters.AddWithValue("@base_multiplier", upgrade.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", upgrade.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Upgrades>.Inserted(upgrade)
                : InsertOrUpdateResult<Upgrades>.Failure("Thêm mới Upgrade thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertUpgrade: " + ex.Message);
            return InsertOrUpdateResult<Upgrades>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Upgrades>> UpdateUpgradeAsync(Upgrades upgrade)
    {
        if (upgrade == null || string.IsNullOrEmpty(upgrade.Id))
        {
            return InsertOrUpdateResult<Upgrades>.Failure("Dữ liệu Upgrade hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE upgrades 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", upgrade.Id);
            command.Parameters.AddWithValue("@name", upgrade.Name);
            command.Parameters.AddWithValue("@base_multiplier", upgrade.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", upgrade.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Upgrades>.Updated(upgrade)
                : InsertOrUpdateResult<Upgrades>.Failure("Không tìm thấy Upgrade để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateUpgrade: " + ex.Message);
            return InsertOrUpdateResult<Upgrades>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}