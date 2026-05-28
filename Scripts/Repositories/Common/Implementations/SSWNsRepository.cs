using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class SSWNsRepository : ISSWNsRepository
{
    public async Task<SSWNs> GetSSWNByIdAsync(string id)
    {
        SSWNs sswn = new SSWNs();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM sswns where id = @id";

                await using var selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@id", id);
                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    sswn = new SSWNs{
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

        return sswn;
    }
}