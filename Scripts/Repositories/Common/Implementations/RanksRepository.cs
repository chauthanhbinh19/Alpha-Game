using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class RanksRepository : IRanksRepository
{
    public async Task<Ranks> GetRankByIdAsync(string id)
    {
        Ranks rank = new Ranks();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM ranks where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    rank = new Ranks{
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

        return rank;
    }
    public async Task<InsertOrUpdateResult<Ranks>> InsertRankAsync(Ranks rank)
    {
        if (rank == null || string.IsNullOrEmpty(rank.Id))
        {
            return InsertOrUpdateResult<Ranks>.Failure("Dữ liệu Rank hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO ranks (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", rank.Id);
            command.Parameters.AddWithValue("@name", rank.Name);
            command.Parameters.AddWithValue("@base_multiplier", rank.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", rank.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Ranks>.Inserted(rank)
                : InsertOrUpdateResult<Ranks>.Failure("Thêm mới Rank thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertRank: " + ex.Message);
            return InsertOrUpdateResult<Ranks>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Ranks>> UpdateRankAsync(Ranks rank)
    {
        if (rank == null || string.IsNullOrEmpty(rank.Id))
        {
            return InsertOrUpdateResult<Ranks>.Failure("Dữ liệu Rank hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE ranks 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", rank.Id);
            command.Parameters.AddWithValue("@name", rank.Name);
            command.Parameters.AddWithValue("@base_multiplier", rank.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", rank.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Ranks>.Updated(rank)
                : InsertOrUpdateResult<Ranks>.Failure("Không tìm thấy Rank để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateRank: " + ex.Message);
            return InsertOrUpdateResult<Ranks>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}