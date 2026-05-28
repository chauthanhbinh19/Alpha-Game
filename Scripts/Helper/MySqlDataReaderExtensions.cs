using MySqlConnector;
using System;
using System.Data;
using UnityEngine;

public static class MySqlDataReaderExtensions
{
    public static double GetDoubleSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            if (reader.IsDBNull(column)) return 0d;

            object value = reader[column];
            return Convert.ToDouble(value);
        }
        catch (Exception ex)
        {
            Debug.LogError(
                $"GetDoubleSafe FAILED | Column: {column} | " +
                $"Value: {reader[column]} | " +
                $"RuntimeType: {reader[column]?.GetType()} | " +
                $"Message: {ex.Message}"
            );
            throw;
        }
    }

    public static int GetIntSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            if (reader.IsDBNull(column)) return 0;

            object value = reader[column];
            return Convert.ToInt32(value);
        }
        catch (Exception ex)
        {
            Debug.LogError(
                $"GetIntSafe FAILED | Column: {column} | " +
                $"Value: {reader[column]} | " +
                $"RuntimeType: {reader[column]?.GetType()} | " +
                $"Message: {ex.Message}"
            );
            throw;
        }
    }

    public static long GetLongSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            if (reader.IsDBNull(column)) return 0L;

            object value = reader[column];
            return Convert.ToInt64(value);
        }
        catch (Exception ex)
        {
            Debug.LogError(
                $"GetLongSafe FAILED | Column: {column} | " +
                $"Value: {reader[column]} | " +
                $"RuntimeType: {reader[column]?.GetType()} | " +
                $"Message: {ex.Message}"
            );
            throw;
        }
    }

    public static bool GetBoolSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            if (reader.IsDBNull(column)) return false;

            object value = reader[column];
            return Convert.ToBoolean(value);
        }
        catch (Exception ex)
        {
            Debug.LogError(
                $"GetBoolSafe FAILED | Column: {column} | " +
                $"Value: {reader[column]} | " +
                $"RuntimeType: {reader[column]?.GetType()} | " +
                $"Message: {ex.Message}"
            );
            throw;
        }
    }

    public static string GetStringSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            if (reader.IsDBNull(column)) return null;

            return reader[column].ToString();
        }
        catch (Exception ex)
        {
            Debug.LogError(
                $"GetStringSafe FAILED | Column: {column} | " +
                $"Message: {ex.Message}"
            );
            throw;
        }
    }
}
