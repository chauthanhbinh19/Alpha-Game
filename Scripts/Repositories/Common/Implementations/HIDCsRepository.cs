using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIDCsRepository : IHIDCsRepository
{
    public async Task<HIDCs> GetHIDCByIdAsync(string id)
    {
        HIDCs hidc = new HIDCs();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM hidcs where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    hidc = new HIDCs{
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

        return hidc;
    }
    public async Task<InsertOrUpdateResult<HIDCs>> InsertHIDCAsync(HIDCs hidc)
    {
        if (hidc == null || string.IsNullOrEmpty(hidc.Id))
        {
            return InsertOrUpdateResult<HIDCs>.Failure("Dữ liệu HIDC hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO hidcs (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", hidc.Id);
            command.Parameters.AddWithValue("@name", hidc.Name);
            command.Parameters.AddWithValue("@base_multiplier", hidc.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hidc.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HIDCs>.Inserted(hidc)
                : InsertOrUpdateResult<HIDCs>.Failure("Thêm mới HIDC thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertHIDC: " + ex.Message);
            return InsertOrUpdateResult<HIDCs>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<HIDCs>> UpdateHIDCAsync(HIDCs hidc)
    {
        if (hidc == null || string.IsNullOrEmpty(hidc.Id))
        {
            return InsertOrUpdateResult<HIDCs>.Failure("Dữ liệu HIDC hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE hidcs 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", hidc.Id);
            command.Parameters.AddWithValue("@name", hidc.Name);
            command.Parameters.AddWithValue("@base_multiplier", hidc.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hidc.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HIDCs>.Updated(hidc)
                : InsertOrUpdateResult<HIDCs>.Failure("Không tìm thấy HIDC để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateHIDC: " + ex.Message);
            return InsertOrUpdateResult<HIDCs>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}