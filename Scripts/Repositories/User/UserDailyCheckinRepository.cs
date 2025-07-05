using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserDailyCheckinRepository : IUserDailyCheckinRepository
{
    public void InsertUserDailyCheckin(string userId, UserDailyCheckin userDailyCheckin)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string currencyQuery = @"
                Insert into user_daily_checkin (user_id, daily_checkin_id, status, day, month, year) values
                (@user_id, @daily_checkin_id, @status, @day, @month, @year);
                ";
                MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@user_id", userId);
                currencyCommand.Parameters.AddWithValue("@daily_checkin_id", userDailyCheckin.daily_checkin_id);
                currencyCommand.Parameters.AddWithValue("@day", userDailyCheckin.day);
                currencyCommand.Parameters.AddWithValue("@month", userDailyCheckin.month);
                currencyCommand.Parameters.AddWithValue("@year", userDailyCheckin.year);
                currencyCommand.Parameters.AddWithValue("@status", userDailyCheckin.status);
                currencyCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public void UpdateUserDailyCheckin(string userId, string dailyCheckinId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string currencyQuery = @"
                Update user_daily_checkin 
                Set status = @status
                where user_id = @user_id and daily_checkin_id = @daily_checkin_id;
                ";
                MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@user_id", userId);
                currencyCommand.Parameters.AddWithValue("@daily_checkin_id", dailyCheckinId);
                currencyCommand.Parameters.AddWithValue("@status", true);
                currencyCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public void DeleteUserDailyCheckin(string userId, string dailyCheckinId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string currencyQuery = @"
                Delete from user_daily_checkin 
                where user_id = @user_id and daily_checkin_id = @daily_checkin_id;
                ";
                MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@user_id", userId);
                currencyCommand.Parameters.AddWithValue("@daily_checkin_id", dailyCheckinId);
                currencyCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public bool CheckUserDailyCheckinStatus(string userId, int month, int year)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string currencyQuery = @"
                SELECT *
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
                    )
                ORDER BY dc.day ASC;
                ";
                MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@user_id", userId);
                currencyCommand.Parameters.AddWithValue("@month", month);
                currencyCommand.Parameters.AddWithValue("@year", year);
                int count = Convert.ToInt32(currencyCommand.ExecuteScalar());
                if (count == 0)
                {
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return false;
    }
    public List<UserDailyCheckin> GetUserDailyCheckin(string userId)
    {
        List<UserDailyCheckin> userDailyCheckins = new List<UserDailyCheckin>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string currencyQuery = @"
                SELECT dc.*, udc.status
                FROM daily_checkin dc
                LEFT JOIN user_daily_checkin udc
                    ON dc.id = udc.daily_checkin_id
                    AND udc.user_id = @user_id 
                ORDER BY dc.day ASC;";
                MySqlCommand currencyCommand = new MySqlCommand(currencyQuery, connection);
                currencyCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                MySqlDataReader currencyReader = currencyCommand.ExecuteReader();
                while (currencyReader.Read())
                {
                    DailyCheckin dailyCheckin = new DailyCheckin
                    {
                        id = currencyReader.GetString("id"),
                        type = currencyReader.GetString("type"),
                        object_id = currencyReader.GetString("object_id"),
                        date = currencyReader.GetDateTime("day"),
                        quantity = currencyReader.GetInt32("quantity")
                    };
                    userDailyCheckins.Add(new UserDailyCheckin
                    {
                        status = currencyReader.GetBoolean("status"),
                        DailyCheckin = dailyCheckin
                    });
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return userDailyCheckins;
    }
}