using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySqlConnector;
using System;
using System.Threading.Tasks;
public class ArenaRepository : IArenaRepository
{
    public async Task<List<string>> GetUniqueTypesAsync()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT DISTINCT id, mode FROM arena_mode ORDER BY id ASC;";
            await using (var command = new MySqlCommand(query, connection))
            await using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    typeList.Add(reader.GetStringSafe("mode"));
                }
            }
        }

        return typeList;
    }
    public async Task<string> GetArenaModeIdAsync(string type)
    {
        string id = "";
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT id FROM arena_mode WHERE mode = @mode";
            await using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@mode", type);

                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        id = reader.GetStringSafe("id");
                    }
                }
            }
        }

        return id;
    }
    public async Task<Dictionary<string, int>> GetArenaParticipantByRankingAsync(string arena_id)
    {
        var userRankings = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string selectQuery = @"
            SELECT arena_id, user_id, rank_point,
                   RANK() OVER (PARTITION BY arena_id ORDER BY rank_point DESC) AS user_rank
            FROM arena_participant 
            WHERE arena_id = @arena_id;";

            await using var selectCommand = new MySqlCommand(selectQuery, connection);
            selectCommand.Parameters.AddWithValue("@arena_id", arena_id);

            await using var reader = await selectCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                string userId = reader.GetStringSafe("user_id");
                int rank = reader.GetIntSafe("user_rank");

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

        return userRankings;
    }
    public async Task<int> GetArenaParticipantPointAsync(string user_id, string arena_id)
    {
        int point = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            // Kiểm tra xem bản ghi đã tồn tại chưa
            string checkQuery = @"
            SELECT COUNT(*) FROM arena_participant 
            WHERE user_id = @user_id AND arena_id = @arena_id;";

            await using (var checkCommand = new MySqlCommand(checkQuery, connection))
            {
                checkCommand.Parameters.AddWithValue("@user_id", user_id);
                checkCommand.Parameters.AddWithValue("@arena_id", arena_id);

                int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());
                if (count == 0)
                {
                    await InsertArenaParticipantAsync(user_id, arena_id);
                }
            }

            // Lấy rank_point
            string selectQuery = @"
            SELECT rank_point FROM arena_participant 
            WHERE user_id = @user_id AND arena_id = @arena_id;";

            await using (var selectCommand = new MySqlCommand(selectQuery, connection))
            {
                selectCommand.Parameters.AddWithValue("@user_id", user_id);
                selectCommand.Parameters.AddWithValue("@arena_id", arena_id);

                await using var reader = await selectCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    point = reader.GetIntSafe("rank_point");
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return point;
    }
    public async Task InsertArenaParticipantAsync(string user_id, string arena_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string insertQuery = @"
            INSERT INTO arena_participant (arena_id, user_id, rank_point)
            VALUES (@arena_id, @user_id, @rank_point);";

            await using var insertCommand = new MySqlCommand(insertQuery, connection);
            insertCommand.Parameters.AddWithValue("@arena_id", arena_id);
            insertCommand.Parameters.AddWithValue("@user_id", user_id);
            insertCommand.Parameters.AddWithValue("@rank_point", 1000);

            await insertCommand.ExecuteNonQueryAsync();
            // Debug.Log($"Inserted user {user_id} into arena {arena_id}.");
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
}