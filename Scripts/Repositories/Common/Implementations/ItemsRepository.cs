using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class ItemsRepository : IItemsRepository
{
    public async Task<List<string>> GetUniqueItemsIdAsync()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT DISTINCT id FROM items";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        typeList.Add(reader.GetString(0));
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return typeList;
    }
    public async Task<List<string>> GetUniqueItemsTypesAsync()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT DISTINCT type FROM items ORDER BY type ASC";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        typeList.Add(reader.GetString(0));
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return typeList;
    }
    public async Task<List<Items>> GetItemsAsync()
    {
        List<Items> items = new List<Items>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT * FROM items";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Items item = new Items
                        {
                            Id = reader.GetStringSafe("id"),
                            Name = reader.GetStringSafe("name"),
                            Image = reader.GetStringSafe("image"),
                            Type = reader.GetStringSafe("type"),
                            Price = reader.GetDoubleSafe("price")
                        };
                        items.Add(item);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return items;
    }
    public async Task<bool> IsItemDeletedOrInactiveAsync(string id)
    {
        if (string.IsNullOrEmpty(id)) return true;

        string connectionString = DatabaseConfig.ConnectionString;
        const string sql = "SELECT 1 FROM items WHERE id = @id AND (is_deleted = TRUE OR is_active = FALSE) LIMIT 1;";

        try
        {
            await using var conn = new MySqlConnection(connectionString);
            await using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.Add("@id", MySqlDbType.VarChar, 32).Value = id;

            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();
            return result != null && result != DBNull.Value;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error checking item status: {ex.Message}");
            return true;
        }
    }
}