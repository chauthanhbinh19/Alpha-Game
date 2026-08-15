using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class ScienceFictionsRepository : IScienceFictionsRepository
{
    public async Task<ScienceFictions> GetScienceFictionByIdAsync(string id)
    {
        ScienceFictions scienceFiction = new ScienceFictions();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM science_fictions where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    scienceFiction = new ScienceFictions{
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

        return scienceFiction;
    }
    public async Task<InsertOrUpdateResult<ScienceFictions>> InsertScienceFictionAsync(ScienceFictions scienceFiction)
    {
        if (scienceFiction == null || string.IsNullOrEmpty(scienceFiction.Id))
        {
            return InsertOrUpdateResult<ScienceFictions>.Failure("Dữ liệu ScienceFiction hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO science_fictions (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", scienceFiction.Id);
            command.Parameters.AddWithValue("@name", scienceFiction.Name);
            command.Parameters.AddWithValue("@base_multiplier", scienceFiction.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", scienceFiction.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<ScienceFictions>.Inserted(scienceFiction)
                : InsertOrUpdateResult<ScienceFictions>.Failure("Thêm mới ScienceFiction thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertScienceFiction: " + ex.Message);
            return InsertOrUpdateResult<ScienceFictions>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<ScienceFictions>> UpdateScienceFictionAsync(ScienceFictions scienceFiction)
    {
        if (scienceFiction == null || string.IsNullOrEmpty(scienceFiction.Id))
        {
            return InsertOrUpdateResult<ScienceFictions>.Failure("Dữ liệu ScienceFiction hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE science_fictions 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", scienceFiction.Id);
            command.Parameters.AddWithValue("@name", scienceFiction.Name);
            command.Parameters.AddWithValue("@base_multiplier", scienceFiction.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", scienceFiction.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<ScienceFictions>.Updated(scienceFiction)
                : InsertOrUpdateResult<ScienceFictions>.Failure("Không tìm thấy ScienceFiction để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateScienceFiction: " + ex.Message);
            return InsertOrUpdateResult<ScienceFictions>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}