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

                string insertSQL = @"
                INSERT INTO daily_checkin (id, day, month, year, type, object_id, quantity) VALUES 
                (@id, @day, @month, @year, @type, @object_id, @quantity)
            ";

                await using (var insertCommand = new MySqlCommand(insertSQL, connection))
                {
                    insertCommand.Parameters.AddWithValue("@id", dailyCheckin.Id);
                    insertCommand.Parameters.AddWithValue("@day", dailyCheckin.Date);
                    insertCommand.Parameters.AddWithValue("@month", dailyCheckin.Month);
                    insertCommand.Parameters.AddWithValue("@year", dailyCheckin.Year);
                    insertCommand.Parameters.AddWithValue("@type", dailyCheckin.Type);
                    insertCommand.Parameters.AddWithValue("@object_id", dailyCheckin.ObjectId);
                    insertCommand.Parameters.AddWithValue("@quantity", dailyCheckin.Quantity);

                    await insertCommand.ExecuteNonQueryAsync();
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

                string deleteSQL = @"DELETE FROM daily_checkin WHERE id = @id;";

                await using (var deleteCommand = new MySqlCommand(deleteSQL, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@id", dailyCheckinId);
                    await deleteCommand.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
}