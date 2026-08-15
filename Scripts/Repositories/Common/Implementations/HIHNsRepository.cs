using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIHNsRepository : IHIHNsRepository
{
    public async Task<HIHNs> GetHIHNByIdAsync(string id)
    {
        HIHNs hihn = new HIHNs();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM hihns where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    hihn = new HIHNs{
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

        return hihn;
    }
    public async Task<InsertOrUpdateResult<HIHNs>> InsertHIHNAsync(HIHNs hihn)
    {
        if (hihn == null || string.IsNullOrEmpty(hihn.Id))
        {
            return InsertOrUpdateResult<HIHNs>.Failure("Dữ liệu HIHN hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO hihns (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", hihn.Id);
            command.Parameters.AddWithValue("@name", hihn.Name);
            command.Parameters.AddWithValue("@base_multiplier", hihn.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hihn.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HIHNs>.Inserted(hihn)
                : InsertOrUpdateResult<HIHNs>.Failure("Thêm mới HIHN thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertHIHN: " + ex.Message);
            return InsertOrUpdateResult<HIHNs>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<HIHNs>> UpdateHIHNAsync(HIHNs hihn)
    {
        if (hihn == null || string.IsNullOrEmpty(hihn.Id))
        {
            return InsertOrUpdateResult<HIHNs>.Failure("Dữ liệu HIHN hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE hihns 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", hihn.Id);
            command.Parameters.AddWithValue("@name", hihn.Name);
            command.Parameters.AddWithValue("@base_multiplier", hihn.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hihn.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HIHNs>.Updated(hihn)
                : InsertOrUpdateResult<HIHNs>.Failure("Không tìm thấy HIHN để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateHIHN: " + ex.Message);
            return InsertOrUpdateResult<HIHNs>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}