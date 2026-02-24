using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class EquipmentTypeRepository : IEquipmentTypeRepository
{
    public async Task<EquipmentType> GetEquipmentTypeByNameAsync(string type)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string query = @"
            SELECT type, slot_value, can_use_border_effect
            FROM equipment_type
            WHERE type = @type
            LIMIT 1;";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@type", type);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new EquipmentType
                {
                    Type = reader.GetStringSafe("type"),
                    SlotValue = reader.GetIntSafe("slot_value"),
                    CanUseBorderEffect = reader.GetBoolSafe("can_use_border_effect"),
                };
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }

        return null;
    }
}