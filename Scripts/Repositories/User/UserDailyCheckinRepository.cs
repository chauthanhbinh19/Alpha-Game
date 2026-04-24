using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserDailyCheckinRepository : IUserDailyCheckinRepository
{
    public async Task InsertUserDailyCheckinAsync(string userId, UserDailyCheckin userDailyCheckin)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string insertSQL = @"
                INSERT INTO user_daily_checkin 
                    (user_id, daily_checkin_id, status, day, month, year) 
                VALUES
                    (@user_id, @daily_checkin_id, @status, @day, @month, @year);
            ";

                await using var command = new MySqlCommand(insertSQL, connection);
                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddWithValue("@daily_checkin_id", userDailyCheckin.DailyCheckinId);
                command.Parameters.AddWithValue("@day", userDailyCheckin.Day);
                command.Parameters.AddWithValue("@month", userDailyCheckin.Month);
                command.Parameters.AddWithValue("@year", userDailyCheckin.Year);
                command.Parameters.AddWithValue("@status", userDailyCheckin.Status);

                await command.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task UpdateUserDailyCheckinAsync(string userId, string dailyCheckinId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_daily_checkin 
                SET status = @status
                WHERE user_id = @user_id AND daily_checkin_id = @daily_checkin_id;
            ";

                await using var command = new MySqlCommand(updateSQL, connection);
                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddWithValue("@daily_checkin_id", dailyCheckinId);
                command.Parameters.AddWithValue("@status", true);

                await command.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task DeleteUserDailyCheckinAsync(string userId, string dailyCheckinId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string deleteSQL = @"
                DELETE FROM user_daily_checkin 
                WHERE user_id = @user_id AND daily_checkin_id = @daily_checkin_id;
            ";

                await using var command = new MySqlCommand(deleteSQL, connection);
                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddWithValue("@daily_checkin_id", dailyCheckinId);

                await command.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<bool> CheckUserDailyCheckinStatusAsync(string userId, int month, int year)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT COUNT(*)
                FROM daily_checkin dc
                LEFT JOIN user_daily_checkin udc
                    ON dc.id = udc.daily_checkin_id
                    AND udc.user_id = @user_id
                WHERE dc.month = @month
                AND dc.year = @year
                AND (
                        udc.daily_checkin_id IS NULL
                        OR udc.month <> dc.month
                        OR udc.year <> dc.year
                    );
            ";

                await using var command = new MySqlCommand(selectSQL, connection);
                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddWithValue("@month", month);
                command.Parameters.AddWithValue("@year", year);

                int count = Convert.ToInt32(await command.ExecuteScalarAsync());

                return count == 0;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
    public async Task<List<UserDailyCheckin>> GetUserDailyCheckinAsync(string userId)
    {
        List<UserDailyCheckin> userDailyCheckins = new List<UserDailyCheckin>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT dc.*, udc.status
                FROM daily_checkin dc
                LEFT JOIN user_daily_checkin udc
                    ON dc.id = udc.daily_checkin_id
                    AND udc.user_id = @user_id 
                ORDER BY dc.day ASC;
            ";

                await using var command = new MySqlCommand(selectSQL, connection);
                command.Parameters.AddWithValue("@user_id", userId);

                await using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = reader.GetStringSafe("id"),
                        Type = reader.GetStringSafe("type"),
                        ObjectId = reader.GetStringSafe("object_id"),
                        Date = reader.GetDateTime("day"),
                        Quantity = reader.GetIntSafe("quantity")
                    };

                    userDailyCheckins.Add(new UserDailyCheckin
                    {
                        Status = reader.IsDBNull(reader.GetOrdinal("status")) ? false : reader.GetBoolSafe("status"),
                        DailyCheckin = dailyCheckin
                    });
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return userDailyCheckins;
    }
}