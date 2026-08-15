using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class ModulesRepository : IModulesRepository
{
    public async Task<Modules> GetModuleByIdAsync(string id)
    {
        Modules module = new Modules();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM modules where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    module = new Modules{
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

        return module;
    }
    public async Task<InsertOrUpdateResult<Modules>> InsertModuleAsync(Modules module)
    {
        if (module == null || string.IsNullOrEmpty(module.Id))
        {
            return InsertOrUpdateResult<Modules>.Failure("Dữ liệu Module hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO modules (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", module.Id);
            command.Parameters.AddWithValue("@name", module.Name);
            command.Parameters.AddWithValue("@base_multiplier", module.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", module.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Modules>.Inserted(module)
                : InsertOrUpdateResult<Modules>.Failure("Thêm mới Module thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertModule: " + ex.Message);
            return InsertOrUpdateResult<Modules>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<Modules>> UpdateModuleAsync(Modules module)
    {
        if (module == null || string.IsNullOrEmpty(module.Id))
        {
            return InsertOrUpdateResult<Modules>.Failure("Dữ liệu Module hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE modules 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", module.Id);
            command.Parameters.AddWithValue("@name", module.Name);
            command.Parameters.AddWithValue("@base_multiplier", module.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", module.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<Modules>.Updated(module)
                : InsertOrUpdateResult<Modules>.Failure("Không tìm thấy Module để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateModule: " + ex.Message);
            return InsertOrUpdateResult<Modules>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}