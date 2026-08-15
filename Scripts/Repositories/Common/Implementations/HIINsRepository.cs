using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIINsRepository : IHIINsRepository
{
    public async Task<HIINs> GetHIINByIdAsync(string id)
    {
        HIINs hiin = new HIINs();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM hiins where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    hiin = new HIINs{
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

        return hiin;
    }
    public async Task<InsertOrUpdateResult<HIINs>> InsertHIINAsync(HIINs hiin)
    {
        if (hiin == null || string.IsNullOrEmpty(hiin.Id))
        {
            return InsertOrUpdateResult<HIINs>.Failure("Dữ liệu HIIN hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO hiins (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", hiin.Id);
            command.Parameters.AddWithValue("@name", hiin.Name);
            command.Parameters.AddWithValue("@base_multiplier", hiin.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hiin.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HIINs>.Inserted(hiin)
                : InsertOrUpdateResult<HIINs>.Failure("Thêm mới HIIN thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertHIIN: " + ex.Message);
            return InsertOrUpdateResult<HIINs>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<HIINs>> UpdateHIINAsync(HIINs hiin)
    {
        if (hiin == null || string.IsNullOrEmpty(hiin.Id))
        {
            return InsertOrUpdateResult<HIINs>.Failure("Dữ liệu HIIN hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE hiins 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", hiin.Id);
            command.Parameters.AddWithValue("@name", hiin.Name);
            command.Parameters.AddWithValue("@base_multiplier", hiin.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hiin.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HIINs>.Updated(hiin)
                : InsertOrUpdateResult<HIINs>.Failure("Không tìm thấy HIIN để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateHIIN: " + ex.Message);
            return InsertOrUpdateResult<HIINs>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}