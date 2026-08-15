using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HITNsRepository : IHITNsRepository
{
    public async Task<HITNs> GetHITNByIdAsync(string id)
    {
        HITNs hitn = new HITNs();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM hitns where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    hitn = new HITNs{
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

        return hitn;
    }
    public async Task<InsertOrUpdateResult<HITNs>> InsertHITNAsync(HITNs hitn)
    {
        if (hitn == null || string.IsNullOrEmpty(hitn.Id))
        {
            return InsertOrUpdateResult<HITNs>.Failure("Dữ liệu HITN hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO hitns (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", hitn.Id);
            command.Parameters.AddWithValue("@name", hitn.Name);
            command.Parameters.AddWithValue("@base_multiplier", hitn.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hitn.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HITNs>.Inserted(hitn)
                : InsertOrUpdateResult<HITNs>.Failure("Thêm mới HITN thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertHITN: " + ex.Message);
            return InsertOrUpdateResult<HITNs>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<HITNs>> UpdateHITNAsync(HITNs hitn)
    {
        if (hitn == null || string.IsNullOrEmpty(hitn.Id))
        {
            return InsertOrUpdateResult<HITNs>.Failure("Dữ liệu HITN hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE hitns 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", hitn.Id);
            command.Parameters.AddWithValue("@name", hitn.Name);
            command.Parameters.AddWithValue("@base_multiplier", hitn.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hitn.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HITNs>.Updated(hitn)
                : InsertOrUpdateResult<HITNs>.Failure("Không tìm thấy HITN để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateHITN: " + ex.Message);
            return InsertOrUpdateResult<HITNs>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}