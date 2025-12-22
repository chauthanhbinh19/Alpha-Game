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

                string currencyQuery = @"
                INSERT INTO user_daily_checkin 
                    (user_id, daily_checkin_id, status, day, month, year) 
                VALUES
                    (@user_id, @daily_checkin_id, @status, @day, @month, @year);
            ";

                await using var currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@user_id", userId);
                currencyCommand.Parameters.AddWithValue("@daily_checkin_id", userDailyCheckin.DailyCheckinId);
                currencyCommand.Parameters.AddWithValue("@day", userDailyCheckin.Day);
                currencyCommand.Parameters.AddWithValue("@month", userDailyCheckin.Month);
                currencyCommand.Parameters.AddWithValue("@year", userDailyCheckin.Year);
                currencyCommand.Parameters.AddWithValue("@status", userDailyCheckin.Status);

                await currencyCommand.ExecuteNonQueryAsync();
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

                string currencyQuery = @"
                UPDATE user_daily_checkin 
                SET status = @status
                WHERE user_id = @user_id AND daily_checkin_id = @daily_checkin_id;
            ";

                await using var currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@user_id", userId);
                currencyCommand.Parameters.AddWithValue("@daily_checkin_id", dailyCheckinId);
                currencyCommand.Parameters.AddWithValue("@status", true);

                await currencyCommand.ExecuteNonQueryAsync();
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

                string currencyQuery = @"
                DELETE FROM user_daily_checkin 
                WHERE user_id = @user_id AND daily_checkin_id = @daily_checkin_id;
            ";

                await using var currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@user_id", userId);
                currencyCommand.Parameters.AddWithValue("@daily_checkin_id", dailyCheckinId);

                await currencyCommand.ExecuteNonQueryAsync();
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

                string currencyQuery = @"
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

                await using var currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@user_id", userId);
                currencyCommand.Parameters.AddWithValue("@month", month);
                currencyCommand.Parameters.AddWithValue("@year", year);

                int count = Convert.ToInt32(await currencyCommand.ExecuteScalarAsync());

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

                string currencyQuery = @"
                SELECT dc.*, udc.status
                FROM daily_checkin dc
                LEFT JOIN user_daily_checkin udc
                    ON dc.id = udc.daily_checkin_id
                    AND udc.user_id = @user_id 
                ORDER BY dc.day ASC;
            ";

                await using var currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@user_id", userId);

                await using var currencyReader = await currencyCommand.ExecuteReaderAsync();
                while (await currencyReader.ReadAsync())
                {
                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        Id = currencyReader.GetStringSafe("id"),
                        Type = currencyReader.GetStringSafe("type"),
                        ObjectId = currencyReader.GetStringSafe("object_id"),
                        Date = currencyReader.GetDateTime("day"),
                        Quantity = currencyReader.GetIntSafe("quantity")
                    };

                    userDailyCheckins.Add(new UserDailyCheckin
                    {
                        Status = currencyReader.IsDBNull(currencyReader.GetOrdinal("status")) ? false : currencyReader.GetBoolSafe("status"),
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