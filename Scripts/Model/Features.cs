using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Features
{
    private string feature_name1;
    private int required_level1;
    private string type1;
    private string description1;

    public string feature_name { get => feature_name1; set => feature_name1 = value; }
    public int required_level { get => required_level1; set => required_level1 = value; }
    public string type { get => type1; set => type1 = value; }
    public string description { get => description1; set => description1 = value; }
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
