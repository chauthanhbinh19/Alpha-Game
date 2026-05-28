using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class CurrenciesRepository : ICurrenciesRepository
{
    public async Task<List<Currencies>> GetCurrencyListAsync()
    {
        List<Currencies> currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (var connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM currencies";

                await using (var selectCommand = new MySqlCommand(selectSQL, connection))
                await using (var reader = await selectCommand.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Currencies currency = new Currencies
                        {
                            Id = reader.GetStringSafe("id"),
                            Name = reader.GetStringSafe("name"),
                            Image = reader.GetStringSafe("image"),
                        };

                        currencies.Add(currency);
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return currencies;
    }
}