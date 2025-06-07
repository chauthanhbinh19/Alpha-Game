using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserItemsRepository : IUserItemsRepository
{
    public Items GetUserItemByName(string itemName)
    {
        Items items = new Items();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = @"SELECT i.id as itemId, i.name AS itemName, i.image as itemImage,
               IFNULL(ui.quantity, 0) AS quantity
            FROM items i 
            LEFT JOIN user_items ui ON ui.item_id = i.id AND ui.user_id = @userId
            where i.name=@itemName";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", User.CurrentUserId);
            command.Parameters.AddWithValue("@itemName", itemName);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read()) // Nếu có dữ liệu
            {
                items.id = reader.GetString("itemID");
                items.name = reader["itemName"]?.ToString() ?? string.Empty; // Gán giá trị cột 'name'
                items.image = reader["itemImage"]?.ToString() ?? string.Empty;
                items.quantity = reader["quantity"] != DBNull.Value
                                 ? Convert.ToInt32(reader["quantity"])
                                 : 0; // Gán giá trị cột 'quantity'
            }

        }
        return items;
    }
    public Items InsertUserItems(Items items, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = @"select count(*) from user_items where user_id=@user_id and item_id=@item_id";
            MySqlCommand checkCommand = new MySqlCommand(query, connection);
            checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            checkCommand.Parameters.AddWithValue("@item_id", items.id);
            int count = Convert.ToInt32(checkCommand.ExecuteScalar());
            if (count == 0)
            {
                string insertQuery = @"insert into user_items (user_id, item_id, quantity) values
                (@user_id, @item_id, @quantity)";
                MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                insertCommand.Parameters.AddWithValue("@item_id", items.id);
                insertCommand.Parameters.AddWithValue("@quantity", quantity);
                insertCommand.ExecuteNonQuery();
            }
            else
            {
                UpdateUserItemsQuantity(items);
            }

        }
        return items;
    }
    public Items UpdateUserItemsQuantity(Items items)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = @"Update user_items set quantity=@quantity 
            where user_id=@user_id and item_id=@item_id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            command.Parameters.AddWithValue("@item_id", items.id);
            command.Parameters.AddWithValue("@quantity", items.quantity);
            command.ExecuteNonQuery();

        }
        return items;
    }
    public Items UpdateUserItemsQuantity(Items items, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = @"Update user_items set quantity=@quantity 
            where user_id=@user_id and item_id=@item_id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            command.Parameters.AddWithValue("@item_id", items.id);
            command.Parameters.AddWithValue("@quantity", quantity);
            command.ExecuteNonQuery();

        }
        return items;
    }
}