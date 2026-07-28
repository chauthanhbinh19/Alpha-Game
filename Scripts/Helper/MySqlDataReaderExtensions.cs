using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public static class MySqlDataReaderExtensions
{
    // ==========================================
    // 1. NHÓM ĐỌC THEO INDEX (ORDINAL) - TỐC ĐỘ CAO
    // ==========================================

    public static double GetDoubleSafe(this MySqlDataReader reader, int ordinal)
    {
        if (ordinal < 0 || reader.IsDBNull(ordinal)) return 0d;

        // Convert.ToDouble giúp ép kiểu an toàn dù DB trả về int, float hay decimal
        return Convert.ToDouble(reader.GetValue(ordinal));
    }

    public static string GetStringSafe(this MySqlDataReader reader, int ordinal)
    {
        if (ordinal < 0 || reader.IsDBNull(ordinal)) return string.Empty;

        return reader.GetString(ordinal);
    }

    public static int GetIntSafe(this MySqlDataReader reader, int ordinal)
    {
        if (ordinal < 0 || reader.IsDBNull(ordinal)) return 0;

        return Convert.ToInt32(reader.GetValue(ordinal));
    }

    public static bool GetBooleanSafe(this MySqlDataReader reader, int ordinal)
    {
        if (ordinal < 0 || reader.IsDBNull(ordinal)) return false;

        return Convert.ToBoolean(reader.GetValue(ordinal));
    }


    // ==========================================
    // 2. NHÓM ĐỌC THEO TÊN CỘT (STRING) - OVERLOAD
    // ==========================================

    public static double GetDoubleSafe(this MySqlDataReader reader, string column)
    {
        int ordinal = GetOrdinalSafe(reader, column);
        return reader.GetDoubleSafe(ordinal);
    }

    public static string GetStringSafe(this MySqlDataReader reader, string column)
    {
        int ordinal = GetOrdinalSafe(reader, column);
        return reader.GetStringSafe(ordinal);
    }

    public static int GetIntSafe(this MySqlDataReader reader, string column)
    {
        int ordinal = GetOrdinalSafe(reader, column);
        return reader.GetIntSafe(ordinal);
    }

    public static bool GetBoolSafe(this MySqlDataReader reader, string column)
    {
        int ordinal = GetOrdinalSafe(reader, column);
        return reader.GetBooleanSafe(ordinal);
    }


    // ==========================================
    // 3. HÀM TÌM ORDINAL AN TOÀN (TRÁNH CRASH)
    // ==========================================
    private static int GetOrdinalSafe(MySqlDataReader reader, string column)
    {
        try
        {
            return reader.GetOrdinal(column);
        }
        catch
        {
            // Nếu lỡ gõ sai tên cột hoặc SELECT thiếu, trả về -1 chứ không làm sập game/app
            UnityEngine.Debug.LogWarning($"[DB Warning] Cột '{column}' không tồn tại trong kết quả Query!");
            return -1;
        }
    }
}
