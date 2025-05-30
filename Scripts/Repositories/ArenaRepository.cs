using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;
public class ArenaRepository : IArenaRepository
{
    public List<string> GetUniqueTypes()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT DISTINCT id, mode FROM arena_mode ORDER BY id ASC;";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(1));
            }
        }
        return typeList;
    }
    public string GetArenaModeId(string type)
    {
        string id = "";
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT id FROM arena_mode where mode = @mode";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("mode", type);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                id = reader.GetString("id");
            }
        }
        return id;
    }
    public Dictionary<string, int> GetArenaParticipantByRanking(string arena_id)
    {
        Dictionary<string, int> userRankings = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string selectQuery = @"
                    SELECT arena_id, user_id, rank_point,
                    RANK() OVER (PARTITION BY arena_id ORDER BY rank_point DESC) AS user_rank
                    FROM arena_participant WHERE arena_id = @arena_id;";

                MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);
                selectCommand.Parameters.AddWithValue("@arena_id", arena_id);
                MySqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    string userId = reader.GetString("user_id");
                    int rank = reader.GetInt32("user_rank");

                    if (!userRankings.ContainsKey(userId))
                    {
                        userRankings.Add(userId, rank);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return userRankings;
    }
    public int GetArenaParticipantPoint(string user_id, string arena_id)
    {
        int point = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM arena_participant 
                WHERE user_id = @user_id AND arena_id = @arena_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", user_id);
                checkCommand.Parameters.AddWithValue("@arena_id", arena_id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    InsertArenaParticipant(user_id, arena_id);
                    string selectQuery = @"
                    SELECT rank_point FROM arena_participant 
                    WHERE user_id = @user_id AND arena_id = @arena_id;";

                    MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@arena_id", arena_id);
                    MySqlDataReader reader = selectCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        point = reader.GetInt32("rank_point");
                    }
                }
                else
                {
                    string selectQuery = @"
                    SELECT rank_point FROM arena_participant 
                    WHERE user_id = @user_id AND arena_id = @arena_id;";

                    MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection);
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@arena_id", arena_id);
                    MySqlDataReader reader = selectCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        point = reader.GetInt32("rank_point");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return point;
    }
    public void InsertArenaParticipant(string user_id, string arena_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string insertQuery = @"
                    INSERT INTO arena_participant (arena_id, user_id, rank_point)
                    VALUES (@arena_id, @user_id, @rank_point);
                ";

                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@arena_id", arena_id);
                insertCommand.Parameters.AddWithValue("@user_id", user_id);
                insertCommand.Parameters.AddWithValue("@rank_point", 1000);
                insertCommand.ExecuteNonQuery();
                Debug.Log($"Inserted user {user_id} into arena {arena_id}.");
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
}