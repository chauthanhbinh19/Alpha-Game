using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
public class DailyCheckinRepository : IDailyCheckinRepository
{
    public void InsertDailyCheckin(DailyCheckin dailyCheckin)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                string query = @"
                    INSERT INTO daily_checkin (id, day, month, year, type, object_id, quantity) VALUES 
                    (@id, @day, @month, @year, @type, @object_id, @quantity)
                    ";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", dailyCheckin.id);
                command.Parameters.AddWithValue("@day", dailyCheckin.date);
                command.Parameters.AddWithValue("@month", dailyCheckin.month);
                command.Parameters.AddWithValue("@year", dailyCheckin.year);
                command.Parameters.AddWithValue("@type", dailyCheckin.type);
                command.Parameters.AddWithValue("@object_id", dailyCheckin.object_id);
                command.Parameters.AddWithValue("@quantity", dailyCheckin.quantity);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
    public void DeleteDailyCheckin(string dailyCheckinId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                string query = @"
                    DELETE FROM daily_checkin where id = @id;
                    ";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", dailyCheckinId);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}