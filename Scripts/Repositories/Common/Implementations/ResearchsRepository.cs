using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class ResearchsRepository : IResearchsRepository
{
    public async Task<Researchs> GetResearchByIdAsync(string id)
    {
        Researchs sswn = new Researchs();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM researchs where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    sswn = new Researchs{
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
    public async Task<InsertOrUpdateResult<Researchs>> InsertResearchAsync(Researchs research)
    {
        if (research == null || string.IsNullOrEmpty(research.Id))
        {
            return InsertOrUpdateResult<Researchs>.Failure("Dữ liệu Research hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO researchs (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", research.Id);
            command.Parameters.AddWithValue("@name", research.Name);
            command.Parameters.AddWithValue("@base_multiplier", research.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", research.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Researchs>.Inserted(research)
                : InsertOrUpdateResult<Researchs>.Failure("Thêm mới Research thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertResearch: " + ex.Message);
            return InsertOrUpdateResult<Researchs>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Researchs>> UpdateResearchAsync(Researchs research)
    {
        if (research == null || string.IsNullOrEmpty(research.Id))
        {
            return InsertOrUpdateResult<Researchs>.Failure("Dữ liệu Research hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE researchs 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", research.Id);
            command.Parameters.AddWithValue("@name", research.Name);
            command.Parameters.AddWithValue("@base_multiplier", research.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", research.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Researchs>.Updated(research)
                : InsertOrUpdateResult<Researchs>.Failure("Không tìm thấy Research để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateResearch: " + ex.Message);
            return InsertOrUpdateResult<Researchs>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}