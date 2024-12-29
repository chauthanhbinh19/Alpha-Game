using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Currency
{
    public int id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public int quantity { get; set; }
    public List<Currency> GetEquipmentsCurrency(string type)
    {
        List<Currency> currencies = new List<Currency>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                            from equipments e, currency c, equipment_trade et, user_currency uc
                            where e.id=et.equipment_id and c.id=et.currency_id and e.type=@type and uc.currency_id=c.id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Currency currency= new Currency{
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                    currencies.Add(currency);
                };  
                return currencies;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currencies;
    }
    public Currency GetEquipmentsPrice(string type, int equipment_id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, et.price 
                            from equipments e, currency c, equipment_trade et, user_currency uc
                            where e.id=et.equipment_id and c.id=et.currency_id and e.type=@type and uc.currency_id=c.id
                            and e.id=@equipment_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@equipment_id", equipment_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency= new Currency{
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("price"),
                    };
                };  
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
    public Currency GetUserEquipmentsPrice(string type, int equipment_id)
    {
        Currency currency = new Currency();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select distinct c.id, c.image , c.name, uc.quantity 
                            from equipments e, currency c, equipment_trade et, user_currency uc
                            where e.id=et.equipment_id and c.id=et.currency_id and e.type=@type and uc.currency_id=c.id
                            and e.id=@equipment_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@equipment_id", equipment_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    currency= new Currency{
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        quantity = reader.GetInt32("quantity"),
                    };
                };  
                return currency;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return currency;
    }
}