using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class DailyCheckinRepository : IDailyCheckinRepository
{
    public async Task InsertDailyCheckinAsync(DailyCheckin dailyCheckin)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                INSERT INTO daily_checkin (id, day, month, year, type, object_id, quantity) VALUES 
                (@id, @day, @month, @year, @type, @object_id, @quantity)
            ";

                await using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", dailyCheckin.Id);
                    command.Parameters.AddWithValue("@day", dailyCheckin.Date);
                    command.Parameters.AddWithValue("@month", dailyCheckin.Month);
                    command.Parameters.AddWithValue("@year", dailyCheckin.Year);
                    command.Parameters.AddWithValue("@type", dailyCheckin.Type);
                    command.Parameters.AddWithValue("@object_id", dailyCheckin.ObjectId);
                    command.Parameters.AddWithValue("@quantity", dailyCheckin.Quantity);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public async Task DeleteDailyCheckinAsync(string dailyCheckinId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"DELETE FROM daily_checkin WHERE id = @id;";

                await using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", dailyCheckinId);
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
}