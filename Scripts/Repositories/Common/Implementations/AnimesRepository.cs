using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class AnimesRepository : IAnimesRepository
{
    public async Task<Animes> GetAnimeByIdAsync(string id)
    {
        Animes anime = new Animes();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM animes where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    anime = new Animes
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

        return anime;
    }
    public async Task<InsertOrUpdateResult<Animes>> InsertAnimeAsync(Animes anime)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO animes (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", anime.Id);
            command.Parameters.AddWithValue("@name", anime.Name);
            command.Parameters.AddWithValue("@base_multiplier", anime.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", anime.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Animes>.Inserted(anime)
                : InsertOrUpdateResult<Animes>.Failure("Thêm mới Anime thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertAnime: " + ex.Message);
            return InsertOrUpdateResult<Animes>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Animes>> UpdateAnimeAsync(Animes anime)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE animes 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", anime.Id);
            command.Parameters.AddWithValue("@name", anime.Name);
            command.Parameters.AddWithValue("@base_multiplier", anime.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", anime.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Animes>.Updated(anime)
                : InsertOrUpdateResult<Animes>.Failure("Không tìm thấy Anime để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateAnime: " + ex.Message);
            return InsertOrUpdateResult<Animes>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}