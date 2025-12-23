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

                string query = "SELECT DISTINCT id FROM items";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
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

                string query = "SELECT DISTINCT type FROM items ORDER BY type ASC";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
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

                string query = @"
                SELECT * FROM items
                ORDER BY id REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(id, '[0-9]+$') AS UNSIGNED), id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
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
}