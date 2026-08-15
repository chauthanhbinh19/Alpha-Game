using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIENsRepository : IHIENsRepository
{
    public async Task<HIENs> GetHIENByIdAsync(string id)
    {
        HIENs hien = new HIENs();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM hiens where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    hien = new HIENs{
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

        return hien;
    }
    public async Task<InsertOrUpdateResult<HIENs>> InsertHIENAsync(HIENs hien)
    {
        if (hien == null || string.IsNullOrEmpty(hien.Id))
        {
            return InsertOrUpdateResult<HIENs>.Failure("Dữ liệu HIEN hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO hiens (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", hien.Id);
            command.Parameters.AddWithValue("@name", hien.Name);
            command.Parameters.AddWithValue("@base_multiplier", hien.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hien.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HIENs>.Inserted(hien)
                : InsertOrUpdateResult<HIENs>.Failure("Thêm mới HIEN thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertHIEN: " + ex.Message);
            return InsertOrUpdateResult<HIENs>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<HIENs>> UpdateHIENAsync(HIENs hien)
    {
        if (hien == null || string.IsNullOrEmpty(hien.Id))
        {
            return InsertOrUpdateResult<HIENs>.Failure("Dữ liệu HIEN hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE hiens 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", hien.Id);
            command.Parameters.AddWithValue("@name", hien.Name);
            command.Parameters.AddWithValue("@base_multiplier", hien.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hien.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HIENs>.Updated(hien)
                : InsertOrUpdateResult<HIENs>.Failure("Không tìm thấy HIEN để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateHIEN: " + ex.Message);
            return InsertOrUpdateResult<HIENs>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}