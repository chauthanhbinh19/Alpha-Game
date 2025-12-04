using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class FeaturesRepository : IFeaturesRepository
{
    public async Task<Dictionary<string, int>> GetFeaturesByTypeAsync(string type)
    {
        Dictionary<string, int> features = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT feature_name, required_level FROM features WHERE type = @type";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(0);
                        int requiredLevel = reader.GetInt32(1);

                        features[featureName] = requiredLevel;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, int>> GetAnimeFeaturesByTypeAsync(string type)
    {
        Dictionary<string, int> features = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT DISTINCT type FROM features WHERE type = @type";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureType = reader.GetString(0);

                        // Vì query KHÔNG có cột thứ 2, đặt value mặc định
                        features[featureType] = 1;
                    }
                }
            }
        }

        return features;
    }
}