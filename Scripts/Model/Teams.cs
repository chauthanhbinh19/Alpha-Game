using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Teams
{
    public int user_id {get; set;}
    public int team_id {get; set;}
    public Teams(){

    }
    public List<Teams>  GetUserTeams(){
        List<Teams> teams = new List<Teams>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = "SELECT * FROM Teams WHERE user_id=@user_id";
            MySqlCommand userCommand = new MySqlCommand(userQuery, connection);
            userCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            MySqlDataReader reader = userCommand.ExecuteReader();
            while(reader.Read())
            {
                teams.Add(new Teams{
                    user_id = reader.GetInt32("user_id"),
                    team_id = reader.GetInt32("team_id")
                });  
            }
        }
        return teams;
    }
    public bool InsertUserTeams(){
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string userQuery = "INSERT INTO TEAMS VALUES (@user_id, @team_id)";
            MySqlCommand userCommand = new MySqlCommand(userQuery, connection);
            userCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            userCommand.Parameters.AddWithValue("@team_id", GetMaxTeamId(connection)+1);
            userCommand.ExecuteNonQuery();
        }
        return true;
    }
    public int GetMaxTeamId(MySqlConnection connection){
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
}
