using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserItemsRepository : IUserItemsRepository
{
    public List<Items> GetUserItems(string user_id, string type, int pageSize, int offset)
    {
        List<Items> Items = new List<Items>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select ui.*, i.id, i.name, i.image
                from items i, user_items ui 
                where i.id=ui.item_id and ui.user_id= @userId and i.type= @type
                ORDER BY i.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(i.name, '[0-9]+$') AS UNSIGNED), i.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Items item = new Items
                    {
                        Id = reader.GetString("id"),
                        Name = reader.GetString("name"),
                        Image = reader.GetString("image"),
                        Quantity = reader.GetDouble("quantity"),

                    };

                    Items.Add(item);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        return Items;
    }
    public int GetUserItemCount(string user_id, string type)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select count(*) from items i, user_items ui 
                where i.id=ui.item_id and ui.user_id=@userId and i.type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                count = Convert.ToInt32(command.ExecuteScalar());

                return count;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        return count;
    }
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
                items.Id = reader.GetString("itemID");
                items.Name = reader["itemName"]?.ToString() ?? string.Empty; // Gán giá trị cột 'name'
                items.Image = reader["itemImage"]?.ToString() ?? string.Empty;
                items.Quantity = reader["quantity"] != DBNull.Value
                                 ? Convert.ToDouble(reader["quantity"])
                                 : 0; // Gán giá trị cột 'quantity'
            }
            connection.Close();

        }
        return items;
    }
    public bool InsertUserItems(Items items, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*) from user_items where user_id=@user_id and item_id=@item_id";
                MySqlCommand checkCommand = new MySqlCommand(query, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@item_id", items.Id);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string insertQuery = @"insert into user_items (user_id, item_id, quantity) values
                (@user_id, @item_id, @quantity)";
                    MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    insertCommand.Parameters.AddWithValue("@item_id", items.Id);
                    insertCommand.Parameters.AddWithValue("@quantity", quantity);
                    insertCommand.ExecuteNonQuery();
                }
                else
                {
                    items.Quantity = quantity;
                    UpdateUserItemsQuantity(items);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

        }
        return true;
    }
    public Items UpdateUserItemsQuantity(Items items)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = @"Update user_items set quantity = quantity + @quantity
            where user_id=@user_id and item_id=@item_id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            command.Parameters.AddWithValue("@item_id", items.Id);
            command.Parameters.AddWithValue("@quantity", items.Quantity);
            command.ExecuteNonQuery();

        }
        return items;
    }
    public Items UpdateUserItemsQuantity(Items items, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = @"Update user_items set quantity=@quantity 
            where user_id=@user_id and item_id=@item_id";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
            command.Parameters.AddWithValue("@item_id", items.Id);
            command.Parameters.AddWithValue("@quantity", quantity);
            command.ExecuteNonQuery();
            connection.Close();
        }
        return items;
    }
}