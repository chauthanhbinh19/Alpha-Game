using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public static class MySqlDataReaderExtensions
{
    // ==================== DOUBLE ====================
    public static double GetDoubleSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.GetDoubleSafe(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetDoubleSafe (string) FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }

    public static double GetDoubleSafe(this MySqlDataReader reader, int ordinal)
    {
        try
        {
            if (reader.IsDBNull(ordinal)) return 0d;
            return reader.GetDouble(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetDoubleSafe (index) FAILED | Ordinal: {ordinal} | Message: {ex.Message}");
            throw;
        }
    }


    // ==================== INT ====================
    public static int GetIntSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.GetIntSafe(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetIntSafe (string) FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }

    public static int GetIntSafe(this MySqlDataReader reader, int ordinal)
    {
        try
        {
            if (reader.IsDBNull(ordinal)) return 0;
            return reader.GetInt32(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetIntSafe (index) FAILED | Ordinal: {ordinal} | Message: {ex.Message}");
            throw;
        }
    }


    // ==================== LONG ====================
    public static long GetLongSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.GetLongSafe(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetLongSafe (string) FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }

    public static long GetLongSafe(this MySqlDataReader reader, int ordinal)
    {
        try
        {
            if (reader.IsDBNull(ordinal)) return 0L;
            return reader.GetInt64(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetLongSafe (index) FAILED | Ordinal: {ordinal} | Message: {ex.Message}");
            throw;
        }
    }


    // ==================== BOOL ====================
    public static bool GetBoolSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.GetBoolSafe(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetBoolSafe (string) FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }

    public static bool GetBoolSafe(this MySqlDataReader reader, int ordinal)
    {
        try
        {
            if (reader.IsDBNull(ordinal)) return false;
            return reader.GetBoolean(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetBoolSafe (index) FAILED | Ordinal: {ordinal} | Message: {ex.Message}");
            throw;
        }
    }


    // ==================== STRING ====================
    public static string GetStringSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.GetStringSafe(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetStringSafe (string) FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }

    public static string GetStringSafe(this MySqlDataReader reader, int ordinal)
    {
        try
        {
            if (reader.IsDBNull(ordinal)) return null;
            return reader.GetString(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetStringSafe (index) FAILED | Ordinal: {ordinal} | Message: {ex.Message}");
            throw;
        }
    }
}
