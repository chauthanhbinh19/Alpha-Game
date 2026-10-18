using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class GameConfigRepository : IGameConfigRepository
{
    /// <summary>
    /// Lấy chính xác 1 bản ghi cấu hình dựa trên object_type và object_column
    /// </summary>
    public async Task<GameConfig> GetGameConfigAsync(string objectType, string objectColumn)
    {
        if (string.IsNullOrEmpty(objectType) || string.IsNullOrEmpty(objectColumn))
        {
            return null;
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string selectSQL = @"SELECT object_type, object_column, min_value, max_value, description 
                            FROM game_config 
                            WHERE object_type = @object_type AND object_column = @object_column 
                            LIMIT 1;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(selectSQL, connection);

            command.Parameters.AddWithValue("@object_type", objectType);
            command.Parameters.AddWithValue("@object_column", objectColumn);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new GameConfig
                {
                    ObjectType = reader.GetStringSafe("object_type"),
                    ObjectColumn = reader.GetStringSafe("object_column"),
                    MinValue = reader.GetStringSafe("min_value"),
                    MaxValue = reader.GetStringSafe("max_value"),
                    Description = reader.GetStringSafe("description")
                };
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error GetGameConfigAsync ({objectType} - {objectColumn}): {ex.Message}");
        }

        return null;
    }

    /// <summary>
    /// Lấy toàn bộ danh sách cấu hình trong game_config
    /// </summary>
    public async Task<List<GameConfig>> GetAllGameConfigsAsync()
    {
        var configList = new List<GameConfig>();
        string connectionString = DatabaseConfig.ConnectionString;
        string selectSQL = @"SELECT object_type, object_column, min_value, max_value, description 
                            FROM game_config;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(selectSQL, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                configList.Add(new GameConfig
                {
                    ObjectType = reader.GetStringSafe("object_type"),
                    ObjectColumn = reader.GetStringSafe("object_column"),
                    MinValue = reader.GetStringSafe("min_value"),
                    MaxValue = reader.GetStringSafe("max_value"),
                    Description = reader.GetStringSafe("description")
                });
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error GetAllGameConfigsAsync: " + ex.Message);
        }
        return configList;
    }
}
