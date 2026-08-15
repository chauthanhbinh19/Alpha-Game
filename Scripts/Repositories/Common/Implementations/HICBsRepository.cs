using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HICBsRepository : IHICBsRepository
{
    public async Task<HICBs> GetHICBByIdAsync(string id)
    {
        HICBs hicb = new HICBs();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM hicbs where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    hicb = new HICBs{
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

        return hicb;
    }
    public async Task<InsertOrUpdateResult<HICBs>> InsertHICBAsync(HICBs hicb)
    {
        if (hicb == null || string.IsNullOrEmpty(hicb.Id))
        {
            return InsertOrUpdateResult<HICBs>.Failure("Dữ liệu HICB hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO hicbs (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", hicb.Id);
            command.Parameters.AddWithValue("@name", hicb.Name);
            command.Parameters.AddWithValue("@base_multiplier", hicb.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hicb.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HICBs>.Inserted(hicb)
                : InsertOrUpdateResult<HICBs>.Failure("Thêm mới HICB thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertHICB: " + ex.Message);
            return InsertOrUpdateResult<HICBs>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<HICBs>> UpdateHICBAsync(HICBs hicb)
    {
        if (hicb == null || string.IsNullOrEmpty(hicb.Id))
        {
            return InsertOrUpdateResult<HICBs>.Failure("Dữ liệu HICB hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE hicbs 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", hicb.Id);
            command.Parameters.AddWithValue("@name", hicb.Name);
            command.Parameters.AddWithValue("@base_multiplier", hicb.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hicb.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HICBs>.Updated(hicb)
                : InsertOrUpdateResult<HICBs>.Failure("Không tìm thấy HICB để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateHICB: " + ex.Message);
            return InsertOrUpdateResult<HICBs>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}