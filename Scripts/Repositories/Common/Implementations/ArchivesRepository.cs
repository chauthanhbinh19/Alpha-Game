using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class ArchivesRepository : IArchivesRepository
{
    public async Task<Archives> GetArchiveByIdAsync(string id)
    {
        Archives archive = new Archives();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM archives where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    archive = new Archives
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

        return archive;
    }
    public async Task<InsertOrUpdateResult<Archives>> InsertArchiveAsync(Archives archive)
    {
        if (archive == null || string.IsNullOrEmpty(archive.Id))
        {
            return InsertOrUpdateResult<Archives>.Failure("Dữ liệu Archive hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO archives (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", archive.Id);
            command.Parameters.AddWithValue("@name", archive.Name);
            command.Parameters.AddWithValue("@base_multiplier", archive.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", archive.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Archives>.Inserted(archive)
                : InsertOrUpdateResult<Archives>.Failure("Thêm mới Archive thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertArchive: " + ex.Message);
            return InsertOrUpdateResult<Archives>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Archives>> UpdateArchiveAsync(Archives archive)
    {
        if (archive == null || string.IsNullOrEmpty(archive.Id))
        {
            return InsertOrUpdateResult<Archives>.Failure("Dữ liệu Archive hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE archives 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", archive.Id);
            command.Parameters.AddWithValue("@name", archive.Name);
            command.Parameters.AddWithValue("@base_multiplier", archive.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", archive.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Archives>.Updated(archive)
                : InsertOrUpdateResult<Archives>.Failure("Không tìm thấy Archive để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateArchive: " + ex.Message);
            return InsertOrUpdateResult<Archives>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}