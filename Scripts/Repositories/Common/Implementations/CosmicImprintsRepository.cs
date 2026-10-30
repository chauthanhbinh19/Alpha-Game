using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class CosmicImprintsRepository : ICosmicImprintsRepository
{
    public async Task<CosmicImprints> GetCosmicImprintByIdAsync(string id)
    {
        CosmicImprints hisn = new CosmicImprints();
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
                    hisn = new CosmicImprints{
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
    public async Task<InsertOrUpdateResult<CosmicImprints>> InsertCosmicImprintAsync(CosmicImprints hisn)
    {
        if (hisn == null || string.IsNullOrEmpty(hisn.Id))
        {
            return InsertOrUpdateResult<CosmicImprints>.Failure("Dữ liệu CosmicImprint hoặc ID không hợp lệ.");
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
                ? InsertOrUpdateResult<CosmicImprints>.Inserted(hisn)
                : InsertOrUpdateResult<CosmicImprints>.Failure("Thêm mới CosmicImprint thất bại.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error InsertCosmicImprint: " + ex.Message);
            return InsertOrUpdateResult<CosmicImprints>.Failure("Lỗi Insert: " + ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<CosmicImprints>> UpdateCosmicImprintAsync(CosmicImprints hisn)
    {
        if (hisn == null || string.IsNullOrEmpty(hisn.Id))
        {
            return InsertOrUpdateResult<CosmicImprints>.Failure("Dữ liệu CosmicImprint hoặc ID không hợp lệ.");
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
                ? InsertOrUpdateResult<CosmicImprints>.Updated(hisn)
                : InsertOrUpdateResult<CosmicImprints>.Failure("Không tìm thấy CosmicImprint để cập nhật.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Error UpdateCosmicImprint: " + ex.Message);
            return InsertOrUpdateResult<CosmicImprints>.Failure("Lỗi Update: " + ex.Message);
        }
    }
}