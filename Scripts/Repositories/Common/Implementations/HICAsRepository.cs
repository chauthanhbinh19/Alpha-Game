using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HICAsRepository : IHICAsRepository
{
    public async Task<HICAs> GetHICAByIdAsync(string id)
    {
        HICAs hica = new HICAs();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM hicas where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    hica = new HICAs
                    {
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

        return hica;
    }
    public async Task<InsertOrUpdateResult<HICAs>> InsertHICAAsync(HICAs hica)
    {
        if (hica == null || string.IsNullOrEmpty(hica.Id))
        {
            return InsertOrUpdateResult<HICAs>.Failure("Dữ liệu HICA hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO hicas (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", hica.Id);
            command.Parameters.AddWithValue("@name", hica.Name);
            command.Parameters.AddWithValue("@base_multiplier", hica.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hica.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HICAs>.Inserted(hica)
                : InsertOrUpdateResult<HICAs>.Failure("Thêm mới HICA thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertHICA: " + ex.Message);
            return InsertOrUpdateResult<HICAs>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<HICAs>> UpdateHICAAsync(HICAs hica)
    {
        if (hica == null || string.IsNullOrEmpty(hica.Id))
        {
            return InsertOrUpdateResult<HICAs>.Failure("Dữ liệu HICA hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE hicas 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", hica.Id);
            command.Parameters.AddWithValue("@name", hica.Name);
            command.Parameters.AddWithValue("@base_multiplier", hica.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hica.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HICAs>.Updated(hica)
                : InsertOrUpdateResult<HICAs>.Failure("Không tìm thấy HICA để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateHICA: " + ex.Message);
            return InsertOrUpdateResult<HICAs>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}