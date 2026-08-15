using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UniversesRepository : IUniversesRepository
{
    public async Task<Universes> GetUniverseByIdAsync(string id)
    {
        Universes universe = new Universes();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM universes where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    universe = new Universes{
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

        return universe;
    }
    public async Task<InsertOrUpdateResult<Universes>> InsertUniverseAsync(Universes universe)
    {
        if (universe == null || string.IsNullOrEmpty(universe.Id))
        {
            return InsertOrUpdateResult<Universes>.Failure("Dữ liệu Universe hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO universes (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", universe.Id);
            command.Parameters.AddWithValue("@name", universe.Name);
            command.Parameters.AddWithValue("@base_multiplier", universe.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", universe.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Universes>.Inserted(universe)
                : InsertOrUpdateResult<Universes>.Failure("Thêm mới Universe thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertUniverse: " + ex.Message);
            return InsertOrUpdateResult<Universes>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Universes>> UpdateUniverseAsync(Universes universe)
    {
        if (universe == null || string.IsNullOrEmpty(universe.Id))
        {
            return InsertOrUpdateResult<Universes>.Failure("Dữ liệu Universe hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE universes 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", universe.Id);
            command.Parameters.AddWithValue("@name", universe.Name);
            command.Parameters.AddWithValue("@base_multiplier", universe.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", universe.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Universes>.Updated(universe)
                : InsertOrUpdateResult<Universes>.Failure("Không tìm thấy Universe để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateUniverse: " + ex.Message);
            return InsertOrUpdateResult<Universes>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}