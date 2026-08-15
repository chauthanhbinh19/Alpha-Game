using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class MastersRepository : IMastersRepository
{
    public async Task<Masters> GetMasterByIdAsync(string id)
    {
        Masters master = new Masters();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM masters where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    master = new Masters{
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

        return master;
    }
    public async Task<InsertOrUpdateResult<Masters>> InsertMasterAsync(Masters master)
    {
        if (master == null || string.IsNullOrEmpty(master.Id))
        {
            return InsertOrUpdateResult<Masters>.Failure("Dữ liệu Master hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO masters (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", master.Id);
            command.Parameters.AddWithValue("@name", master.Name);
            command.Parameters.AddWithValue("@base_multiplier", master.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", master.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Masters>.Inserted(master)
                : InsertOrUpdateResult<Masters>.Failure("Thêm mới Master thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertMaster: " + ex.Message);
            return InsertOrUpdateResult<Masters>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Masters>> UpdateMasterAsync(Masters master)
    {
        if (master == null || string.IsNullOrEmpty(master.Id))
        {
            return InsertOrUpdateResult<Masters>.Failure("Dữ liệu Master hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE masters 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", master.Id);
            command.Parameters.AddWithValue("@name", master.Name);
            command.Parameters.AddWithValue("@base_multiplier", master.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", master.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Masters>.Updated(master)
                : InsertOrUpdateResult<Masters>.Failure("Không tìm thấy Master để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateMaster: " + ex.Message);
            return InsertOrUpdateResult<Masters>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}