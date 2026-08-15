using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class SSWNsRepository : ISSWNsRepository
{
    public async Task<SSWNs> GetSSWNByIdAsync(string id)
    {
        SSWNs sswn = new SSWNs();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM sswns where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    sswn = new SSWNs{
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

        return sswn;
    }
    public async Task<InsertOrUpdateResult<SSWNs>> InsertSSWNAsync(SSWNs sswn)
    {
        if (sswn == null || string.IsNullOrEmpty(sswn.Id))
        {
            return InsertOrUpdateResult<SSWNs>.Failure("Dữ liệu SSWN hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO sswns (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", sswn.Id);
            command.Parameters.AddWithValue("@name", sswn.Name);
            command.Parameters.AddWithValue("@base_multiplier", sswn.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", sswn.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<SSWNs>.Inserted(sswn)
                : InsertOrUpdateResult<SSWNs>.Failure("Thêm mới SSWN thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertSSWN: " + ex.Message);
            return InsertOrUpdateResult<SSWNs>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<SSWNs>> UpdateSSWNAsync(SSWNs sswn)
    {
        if (sswn == null || string.IsNullOrEmpty(sswn.Id))
        {
            return InsertOrUpdateResult<SSWNs>.Failure("Dữ liệu SSWN hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE sswns 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", sswn.Id);
            command.Parameters.AddWithValue("@name", sswn.Name);
            command.Parameters.AddWithValue("@base_multiplier", sswn.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", sswn.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<SSWNs>.Updated(sswn)
                : InsertOrUpdateResult<SSWNs>.Failure("Không tìm thấy SSWN để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateSSWN: " + ex.Message);
            return InsertOrUpdateResult<SSWNs>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}