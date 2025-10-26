using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
public class ItemsRepository : IItemsRepository
{
    public List<string> GetUniqueItemId()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "Select distinct id from items";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(0));
            }
            connection.Close();
        }
        return typeList;
    }
    public List<string> GetUniqueItemTypes()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "Select distinct type from items order by type asc";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(0));
            }
            connection.Close();
        }
        return typeList;
    }
    public List<Items> GetItems()
    {
        List<Items> items = new List<Items>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"Select * from items 
            ORDER BY id REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(id, '[0-9]+$') AS UNSIGNED), id";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Items item = new Items
                {
                    Id = reader.GetString("id"),
                    Name = reader.GetString("name"),
                    Image = reader.GetString("image"),
                    Type = reader.GetString("type"),
                    Price = reader.GetInt32("price"),
                };
                items.Add(item);
            }
            connection.Close();
        }
        return items;
    }
}