using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
public class FeaturesRepository : IFeaturesRepository
{ 
    public Dictionary<string, int> GetFeaturesByType(string type)
    {
        Dictionary<string, int> features = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT feature_name, required_level FROM features WHERE type = @type";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@type", type);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string featureName = reader.GetString(0);
                int requiredLevel = reader.GetInt32(1);
                features[featureName] = requiredLevel;
            }
        }
        return features;
    }
    public Dictionary<string, int> GetAnimeFeaturesByType(string type)
    {
        Dictionary<string, int> features = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT distinct type FROM features WHERE type = @type";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@type", type);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string featureName = reader.GetString(0);
                int requiredLevel = reader.GetInt32(1);
                features[featureName] = requiredLevel;
            }
        }
        return features;
    }
}