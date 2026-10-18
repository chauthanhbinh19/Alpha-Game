using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine; // Hoặc namespace chứa Debug trong Server của bạn

public class GameGrowthConfigRepository : IGameGrowthConfigRepository
{
    /// <summary>
    /// Lấy cấu hình cụm chứa level hiện tại bằng MySqlDataReader
    /// </summary>
    public async Task<GameGrowthConfig> GetConfigAsync(string objectType, string growthType, int currentLevel)
    {
        GameGrowthConfig config = null;
        string connectionString = DatabaseConfig.ConnectionString;

        const string sql = @"
            SELECT 
                id,
                object_type,
                growth_type,
                min_level,
                max_level,
                formula_type,
                base_exp,
                base_multiplier,
                power_factor,
                linear_factor,
                step_size
            FROM game_growth_config
            WHERE object_type = @ObjectType 
              AND growth_type = @GrowthType 
              AND @CurrentLevel BETWEEN min_level AND max_level
            LIMIT 1;";

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ObjectType", objectType);
            command.Parameters.AddWithValue("@GrowthType", growthType);
            command.Parameters.AddWithValue("@CurrentLevel", currentLevel);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                config = MapReaderToGameGrowthConfig(reader);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error GetConfigAsync ({objectType}, {growthType}, {currentLevel}): " + ex.Message);
        }

        return config;
    }

    /// <summary>
    /// Lấy danh sách cấu hình của 1 ObjectType bằng MySqlDataReader
    /// </summary>
    public async Task<IEnumerable<GameGrowthConfig>> GetConfigsByObjectTypeAsync(string objectType)
    {
        var configList = new List<GameGrowthConfig>();
        string connectionString = DatabaseConfig.ConnectionString;

        const string sql = @"
            SELECT 
                id,
                object_type,
                growth_type,
                min_level,
                max_level,
                formula_type,
                base_exp,
                base_multiplier,
                power_factor,
                linear_factor,
                step_size
            FROM game_growth_config
            WHERE object_type = @ObjectType;";

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@ObjectType", objectType);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                configList.Add(MapReaderToGameGrowthConfig(reader));
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error GetConfigsByObjectTypeAsync ({objectType}): " + ex.Message);
        }

        return configList;
    }

    /// <summary>
    /// Lấy toàn bộ cấu hình bằng MySqlDataReader (Warm-up Cache)
    /// </summary>
    public async Task<IEnumerable<GameGrowthConfig>> GetAllConfigsAsync()
    {
        var configList = new List<GameGrowthConfig>();
        string connectionString = DatabaseConfig.ConnectionString;

        const string sql = @"
            SELECT 
                id,
                object_type,
                growth_type,
                min_level,
                max_level,
                formula_type,
                base_exp,
                base_multiplier,
                power_factor,
                linear_factor,
                step_size
            FROM game_growth_config;";

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using var command = new MySqlCommand(sql, connection);
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                configList.Add(MapReaderToGameGrowthConfig(reader));
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error GetAllConfigsAsync: " + ex.Message);
        }

        return configList;
    }

    /// <summary>
    /// Helper đọc từng cột bằng GetString, GetInt32, GetDouble với kiểm tra DBNull an toàn
    /// </summary>
    private static GameGrowthConfig MapReaderToGameGrowthConfig(MySqlDataReader reader)
    {
        return new GameGrowthConfig
        {
            Id = reader.IsDBNull(reader.GetOrdinal("id")) ? string.Empty : reader.GetString(reader.GetOrdinal("id")),
            ObjectType = reader.IsDBNull(reader.GetOrdinal("object_type")) ? string.Empty : reader.GetString(reader.GetOrdinal("object_type")),
            GrowthType = reader.IsDBNull(reader.GetOrdinal("growth_type")) ? string.Empty : reader.GetString(reader.GetOrdinal("growth_type")),
            MinLevel = reader.IsDBNull(reader.GetOrdinal("min_level")) ? 0 : reader.GetInt32(reader.GetOrdinal("min_level")),
            MaxLevel = reader.IsDBNull(reader.GetOrdinal("max_level")) ? 0 : reader.GetInt32(reader.GetOrdinal("max_level")),
            FormulaType = reader.IsDBNull(reader.GetOrdinal("formula_type")) ? string.Empty : reader.GetString(reader.GetOrdinal("formula_type")),
            BaseExp = reader.IsDBNull(reader.GetOrdinal("base_exp")) ? 0.0 : reader.GetDouble(reader.GetOrdinal("base_exp")),
            BaseMultiplier = reader.IsDBNull(reader.GetOrdinal("base_multiplier")) ? 0.0 : reader.GetDouble(reader.GetOrdinal("base_multiplier")),
            PowerFactor = reader.IsDBNull(reader.GetOrdinal("power_factor")) ? 0.0 : reader.GetDouble(reader.GetOrdinal("power_factor")),
            LinearFactor = reader.IsDBNull(reader.GetOrdinal("linear_factor")) ? 0.0 : reader.GetDouble(reader.GetOrdinal("linear_factor")),
            StepSize = reader.IsDBNull(reader.GetOrdinal("step_size")) ? 0 : reader.GetInt32(reader.GetOrdinal("step_size"))
        };
    }
}