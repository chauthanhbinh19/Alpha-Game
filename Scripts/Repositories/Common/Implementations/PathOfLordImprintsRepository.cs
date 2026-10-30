using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class PathOfLordImprintsRepository : IPathOfLordImprintsRepository
{
    public async Task<PathOfLordImprints> GetPathOfLordImprintByIdAsync(string id)
    {
        PathOfLordImprints hisn = new PathOfLordImprints();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM hisns where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    hisn = new PathOfLordImprints{
                        Id = reader.GetStringSafe("id"),
                        Name = reader.GetStringSafe("name"),
                        BaseMultiplier = reader.GetDoubleSafe("base_multiplier"),
                        MaxLevel = reader.GetIntSafe("max_level"),
                    };
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return hisn;
    }
    public async Task<InsertOrUpdateResult<PathOfLordImprints>> InsertPathOfLordImprintAsync(PathOfLordImprints hisn)
    {
        if (hisn == null || string.IsNullOrEmpty(hisn.Id))
        {
            return InsertOrUpdateResult<PathOfLordImprints>.Failure("Dữ liệu PathOfLordImprint hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string insertSQL = @"INSERT INTO hisns (id, name, base_multiplier, max_level) 
                        VALUES (@id, @name, @base_multiplier, @max_level);";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(insertSQL, connection);

            command.Parameters.AddWithValue("@id", hisn.Id);
            command.Parameters.AddWithValue("@name", hisn.Name);
            command.Parameters.AddWithValue("@base_multiplier", hisn.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hisn.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<PathOfLordImprints>.Inserted(hisn)
                : InsertOrUpdateResult<PathOfLordImprints>.Failure("Thêm mới PathOfLordImprint thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertPathOfLordImprint: " + ex.Message);
            return InsertOrUpdateResult<PathOfLordImprints>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<PathOfLordImprints>> UpdatePathOfLordImprintAsync(PathOfLordImprints hisn)
    {
        if (hisn == null || string.IsNullOrEmpty(hisn.Id))
        {
            return InsertOrUpdateResult<PathOfLordImprints>.Failure("Dữ liệu PathOfLordImprint hoặc ID không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;
        string updateSQL = @"UPDATE hisns 
                        SET name = @name, 
                            base_multiplier = @base_multiplier, 
                            max_level = @max_level 
                        WHERE id = @id;";

        await using MySqlConnection connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();
            await using var command = new MySqlCommand(updateSQL, connection);

            command.Parameters.AddWithValue("@id", hisn.Id);
            command.Parameters.AddWithValue("@name", hisn.Name);
            command.Parameters.AddWithValue("@base_multiplier", hisn.BaseMultiplier);
            command.Parameters.AddWithValue("@max_level", hisn.MaxLevel);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            return rowsAffected > 0
                ? InsertOrUpdateResult<PathOfLordImprints>.Updated(hisn)
                : InsertOrUpdateResult<PathOfLordImprints>.Failure("Không tìm thấy PathOfLordImprint để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdatePathOfLordImprint: " + ex.Message);
            return InsertOrUpdateResult<PathOfLordImprints>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}