using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class HIRNsRepository : IHIRNsRepository
{
    public async Task<HIRNs> GetHIRNByIdAsync(string id)
    {
        HIRNs hirn = new HIRNs();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM hirns where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    hirn = new HIRNs{
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

        return hirn;
    }
    public async Task<InsertOrUpdateResult<HIRNs>> InsertHIRNAsync(HIRNs hirn)
    {
        if (hirn == null || string.IsNullOrEmpty(hirn.Id))
        {
            return InsertOrUpdateResult<HIRNs>.Failure("Dữ liệu HIRN hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO hirns (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", hirn.Id);
            command.Parameters.AddWithValue("@name", hirn.Name);
            command.Parameters.AddWithValue("@base_multiplier", hirn.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hirn.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HIRNs>.Inserted(hirn)
                : InsertOrUpdateResult<HIRNs>.Failure("Thêm mới HIRN thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertHIRN: " + ex.Message);
            return InsertOrUpdateResult<HIRNs>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<HIRNs>> UpdateHIRNAsync(HIRNs hirn)
    {
        if (hirn == null || string.IsNullOrEmpty(hirn.Id))
        {
            return InsertOrUpdateResult<HIRNs>.Failure("Dữ liệu HIRN hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE hirns 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", hirn.Id);
            command.Parameters.AddWithValue("@name", hirn.Name);
            command.Parameters.AddWithValue("@base_multiplier", hirn.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hirn.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<HIRNs>.Updated(hirn)
                : InsertOrUpdateResult<HIRNs>.Failure("Không tìm thấy HIRN để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateHIRN: " + ex.Message);
            return InsertOrUpdateResult<HIRNs>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}