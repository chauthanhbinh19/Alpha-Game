using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Items
{
    public int id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public int price { get; set; }
    public string description { get; set; }
    public int quantity { get; set; }
    public Items(){
        
    }
    public Items GetUserItemByName(string itemName){
        Items items = new Items();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = @"SELECT i.name AS itemName, i.image as itemImage,
               IFNULL(ui.quantity, 0) AS quantity
            FROM items i 
            LEFT JOIN user_items ui ON ui.item_id = i.id AND ui.user_id = 1
            where i.name=@itemName";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", User.CurrentUserId);
            command.Parameters.AddWithValue("@itemName", itemName);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read()) // Nếu có dữ liệu
            {
                items.name = reader["itemName"]?.ToString() ?? string.Empty; // Gán giá trị cột 'name'
                items.image = reader["itemImage"]?.ToString() ?? string.Empty;
                items.quantity = reader["quantity"] != DBNull.Value 
                                 ? Convert.ToInt32(reader["quantity"]) 
                                 : 0; // Gán giá trị cột 'quantity'
            }
            
        }
        return items;
    }
}
