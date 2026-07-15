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
            // 1. Tìm vị trí index của cột từ tên chuỗi (Chỉ tìm 1 lần)
            int ordinal = reader.GetOrdinal(column); 

            if (reader.IsDBNull(ordinal)) return 0d;

            // 2. Đọc trực tiếp double từ RAM mà KHÔNG BỊ BOXING (không tạo rác object)
            return reader.GetDouble(ordinal); 
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetDoubleSafe FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }

    public static int GetIntSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            int ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal)) return 0;

            // Đọc trực tiếp kiểu int32, cực kỳ nhanh
            return reader.GetInt32(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetIntSafe FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }

    public static long GetLongSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            int ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal)) return 0L;

            return reader.GetInt64(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetLongSafe FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }

    public static bool GetBoolSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            int ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal)) return false;

            return reader.GetBoolean(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetBoolSafe FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }

    public static string GetStringSafe(this MySqlDataReader reader, string column)
    {
        try
        {
            int ordinal = reader.GetOrdinal(column);
            if (reader.IsDBNull(ordinal)) return null;

            return reader.GetString(ordinal);
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"GetStringSafe FAILED | Column: {column} | Message: {ex.Message}");
            throw;
        }
    }
}
