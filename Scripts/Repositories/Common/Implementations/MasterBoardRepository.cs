using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class MasterBoardRepository : IMasterBoardRepository
{
    public async Task<List<string>> GetUniqueNameAsync()
    {
        List<string> nameList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            string selectSQL = "SELECT DISTINCT name FROM master_board";
            using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
            using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    nameList.Add(reader.GetString(0));
                }
            }
        }

        return nameList;
    }
}