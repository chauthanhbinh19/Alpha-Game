using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class FeaturesRepository : IFeaturesRepository
{
    public async Task<Dictionary<string, Features>> GetFeaturesByTypeAsync(string type)
    {
        Dictionary<string, Features> features = new Dictionary<string, Features>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT id, feature_name, required_level FROM features WHERE type = @type";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@type", type);

                using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string featureName = reader.GetString(1);
                        Features feature = new Features
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2)
                        };

                        features[featureName] = feature;
                    }
                }
            }
        }

        return features;
    }
    public async Task<Dictionary<string, Features>> GetAnimeFeaturesByTypeAsync(string type)
    {
        Dictionary<string, Features> features = new Dictionary<string, Features>();
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
                        string featureType = reader.GetString(1);

                        Features feature = new Features
                        {
                            Id = reader.GetString(0),
                            FeatureName = reader.GetString(1),
                            RequiredLevel = reader.GetInt32(2)
                        };
                        // Vì query KHÔNG có cột thứ 2, đặt value mặc định
                        features[featureType] = feature;
                    }
                }
            }
        }

        return features;
    }
}