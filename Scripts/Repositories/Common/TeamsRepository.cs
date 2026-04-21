using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class TeamsRepository : ITeamsRepository
{
    public async Task<List<Teams>> GetUserTeamsAsync(string user_id)
    {
        var teams = new List<Teams>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync(); // mở connection async

            string userQuery = "SELECT * FROM Teams WHERE user_id=@user_id ORDER BY team_number ASC";
            using (var userCommand = new MySqlCommand(userQuery, connection))
            {
                userCommand.Parameters.AddWithValue("@user_id", user_id);

                using (var reader = await userCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        teams.Add(new Teams
                        {
                            UserId = reader.GetString("user_id"),
                            TeamId = reader.GetString("team_id"),
                            TeamNumber = reader.GetInt32("team_number"),
                            TeamAvatar = reader.GetString("team_avatar"),
                            TeamBorder = reader.GetString("team_border"),
                        });
                    }
                }
            }
        }

        return teams;
    }
    public async Task<bool> InsertUserTeamsAsync(string user_id, int team_number = 1)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync(); // mở connection async

            string userQuery = @"
            INSERT INTO TEAMS (user_id, team_id, team_number, team_avatar, team_border) 
            VALUES (@user_id, @team_id, @team_number, @team_avatar, @team_border)";

            using (var userCommand = new MySqlCommand(userQuery, connection))
            {
                userCommand.Parameters.AddWithValue("@user_id", user_id);
                userCommand.Parameters.AddWithValue("@team_id", Guid.NewGuid().ToString());
                userCommand.Parameters.AddWithValue("@team_number", team_number);
                userCommand.Parameters.AddWithValue("@team_avatar", "Team/Avatar/Team_Avatar_1");
                userCommand.Parameters.AddWithValue("@team_border", "Team/Border/Team_Border_1");

                await userCommand.ExecuteNonQueryAsync(); // chạy async
            }
        }

        return true;
    }
    public async Task<int> GetMaxTeamIdAsync(MySqlConnection connection)
    {
        string query = "SELECT MAX(team_id) FROM teams WHERE user_id = @user_id";
        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

            object result = await command.ExecuteScalarAsync(); // async call

            if (result != DBNull.Value && result != null)
            {
                return Convert.ToInt32(result);
            }
            return 0; // nếu bảng rỗng
        }
    }
    public double GetTeamsPower(string user_id)
    {
        double totalPower = 0;

        return totalPower;
    }
    public async Task<bool> InsertUserTeamEmblemsAsync(string user_id, EmblemDTO emblemDTO)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync(); // mở connection async

            string userQuery = @"
            INSERT INTO TEAM_EMBLEMS (user_id, team_id, position, card_type, emblem_id, emblem_quantity) 
            VALUES (@user_id, @team_id, @position, @card_type, @emblem_id, @emblem_quantity)";

            using (var userCommand = new MySqlCommand(userQuery, connection))
            {
                userCommand.Parameters.AddWithValue("@user_id", user_id);
                userCommand.Parameters.AddWithValue("@team_id", emblemDTO.TeamId);
                userCommand.Parameters.AddWithValue("@position", emblemDTO.Position);
                userCommand.Parameters.AddWithValue("@card_type", emblemDTO.CardType);
                userCommand.Parameters.AddWithValue("@emblem_id", emblemDTO.EmblemId);
                userCommand.Parameters.AddWithValue("@emblem_quantity", emblemDTO.Count);

                await userCommand.ExecuteNonQueryAsync(); // chạy async
            }
        }

        return true;
    }
    public async Task<bool> DeleteUserTeamEmblemsAsync(string user_id, EmblemDTO emblemDTO)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            // Câu lệnh SQL DELETE với các điều kiện WHERE cụ thể
            string deleteQuery = @"
            DELETE FROM TEAM_EMBLEMS 
            WHERE user_id = @user_id 
              AND team_id = @team_id 
              AND position = @position 
              AND card_type = @card_type";

            using (var command = new MySqlCommand(deleteQuery, connection))
            {
                // Thêm các tham số để tránh SQL Injection
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@team_id", emblemDTO.TeamId);
                command.Parameters.AddWithValue("@position", emblemDTO.Position);
                command.Parameters.AddWithValue("@card_type", emblemDTO.CardType);

                // Thực thi lệnh xóa và lấy số lượng dòng bị ảnh hưởng
                int rowsAffected = await command.ExecuteNonQueryAsync();

                // Trả về true nếu có ít nhất một dòng bị xóa, ngược lại false
                return rowsAffected > 0;
            }
        }
    }
}