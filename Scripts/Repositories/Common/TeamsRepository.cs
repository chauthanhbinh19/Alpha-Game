using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
public class TeamsRepository : ITeamsRepository
{ 
    public List<Teams> GetUserTeams(string user_id)
    {
        List<Teams> teams = new List<Teams>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = "SELECT * FROM Teams WHERE user_id=@user_id";
            MySqlCommand userCommand = new MySqlCommand(userQuery, connection);
            userCommand.Parameters.AddWithValue("@user_id", user_id);
            MySqlDataReader reader = userCommand.ExecuteReader();
            while (reader.Read())
            {
                teams.Add(new Teams
                {
                    user_id = reader.GetString("user_id"),
                    team_id = reader.GetString("team_id")
                });
            }
        }
        return teams;
    }
    public bool InsertUserTeams(string user_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = "INSERT INTO TEAMS VALUES (@user_id, @team_id)";
            MySqlCommand userCommand = new MySqlCommand(userQuery, connection);
            userCommand.Parameters.AddWithValue("@user_id", user_id);
            userCommand.Parameters.AddWithValue("@team_id", GetMaxTeamId(connection) + 1);
            userCommand.ExecuteNonQuery();
        }
        return true;
    }
    public int GetMaxTeamId(MySqlConnection connection)
    {
        string query = "SELECT MAX(team_id) FROM teams where user_id=@user_id";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
        object result = command.ExecuteScalar();

        if (result != DBNull.Value)
        {
            return Convert.ToInt32(result);
        }
        return 0; // Nếu bảng rỗng, trả về 0
    }
    public double GetTeamsPower(string user_id)
    {
        double totalPower = 0;

        return totalPower;
    }
}