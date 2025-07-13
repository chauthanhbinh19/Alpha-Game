using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
public class CurrencyRepository : ICurrencyRepository
{
    public List<Currency> GetCurrencyList()
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from currency";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Currency currency = new Currency
                    {
                        id = reader.GetString("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                    };

                    currencies.Add(currency);
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