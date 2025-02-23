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
    public Items()
    {

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
            LEFT JOIN user_items ui ON ui.item_id = i.id AND ui.user_id = 1
            where i.name=@itemName";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", User.CurrentUserId);
            command.Parameters.AddWithValue("@itemName", itemName);
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read()) // Nếu có dữ liệu
            {
                items.id = reader.GetInt32("itemID");
                items.name = reader["itemName"]?.ToString() ?? string.Empty; // Gán giá trị cột 'name'
                items.image = reader["itemImage"]?.ToString() ?? string.Empty;
                items.quantity = reader["quantity"] != DBNull.Value
                                 ? Convert.ToInt32(reader["quantity"])
                                 : 0; // Gán giá trị cột 'quantity'
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
    public int GetItemExp(string item)
    {
        int expPerBottle = 0;
        if (item.Equals("Exp Bottle lv1"))
        {
            expPerBottle = 100;
        }
        else if (item.Equals("Exp Bottle lv2"))
        {
            expPerBottle = 500;
        }
        else if (item.Equals("Exp Bottle lv3"))
        {
            expPerBottle = 1000;
        }
        else if (item.Equals("Exp Bottle lv4"))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals("Exp Bottle lv5"))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals("Exp Bottle lv6"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 1"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 2"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 3"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 4"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 5"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 6"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 7"))
        {
            expPerBottle = 100000;
        }
        else if (item.Equals("Affinity 8"))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals("Affinity 9"))
        {
            expPerBottle = 50000;
        }
        else if (item.Equals("Affinity 10"))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals("Affinity 11"))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals("Affinity 12"))
        {
            expPerBottle = 10000;
        }
        else if (item.Equals("Affinity 13"))
        {
            expPerBottle = 1000;
        }
        else if (item.Equals("Affinity 14"))
        {
            expPerBottle = 1000;
        }
        return expPerBottle;
    }
    public List<Items> GetItemForLevel(string type)
    {
        Items item = new Items();
        List<Items> items = new List<Items>();
        switch (type)
        {
            case "CardHeroes":
                items.Add(item.GetUserItemByName("Exp Bottle lv1"));
                items.Add(item.GetUserItemByName("Exp Bottle lv2"));
                items.Add(item.GetUserItemByName("Exp Bottle lv3"));
                items.Add(item.GetUserItemByName("Exp Bottle lv4"));
                items.Add(item.GetUserItemByName("Exp Bottle lv5"));
                items.Add(item.GetUserItemByName("Exp Bottle lv6"));
                break;
            case "Books":
                items.Add(item.GetUserItemByName("Exp Books"));
                break;
            case "CardCaptains":
                items.Add(item.GetUserItemByName("Exp Bottle lv1"));
                items.Add(item.GetUserItemByName("Exp Bottle lv2"));
                items.Add(item.GetUserItemByName("Exp Bottle lv3"));
                items.Add(item.GetUserItemByName("Exp Bottle lv4"));
                items.Add(item.GetUserItemByName("Exp Bottle lv5"));
                items.Add(item.GetUserItemByName("Exp Bottle lv6"));
                break;
            case "Pets":
                items.Add(item.GetUserItemByName("Exp Pets"));
                break;
            case "CollaborationEquipment":
                items.Add(item.GetUserItemByName("Exp Collaboration Equipments"));
                break;
            case "CardMilitary":
                items.Add(item.GetUserItemByName("Exp Card Military"));
                break;
            case "CardSpell":
                items.Add(item.GetUserItemByName("Exp Spell"));
                break;
            case "Collaboration":
                items.Add(item.GetUserItemByName("Exp Collaborations"));
                break;
            case "CardMonsters":
                items.Add(item.GetUserItemByName("Exp Card Monsters"));
                break;
            case "Equipments":
                items.Add(item.GetUserItemByName("Exp Equipments"));
                break;
            case "Medals":
                items.Add(item.GetUserItemByName("Exp Medals"));
                break;
            case "Skills":
                items.Add(item.GetUserItemByName("Exp Skills"));
                break;
            case "Symbols":
                items.Add(item.GetUserItemByName("Exp Symbols"));
                break;
            case "Titles":
                items.Add(item.GetUserItemByName("Exp Titles"));
                break;
            case "MagicFormationCircle":
                items.Add(item.GetUserItemByName("Exp Magic Formation Circle"));
                break;
            case "Relics":
                items.Add(item.GetUserItemByName("Exp Relics"));
                break;
            case "CardColonels":
                items.Add(item.GetUserItemByName("Exp Bottle lv1"));
                items.Add(item.GetUserItemByName("Exp Bottle lv2"));
                items.Add(item.GetUserItemByName("Exp Bottle lv3"));
                items.Add(item.GetUserItemByName("Exp Bottle lv4"));
                items.Add(item.GetUserItemByName("Exp Bottle lv5"));
                items.Add(item.GetUserItemByName("Exp Bottle lv6"));
                break;
            case "CardGenerals":
                items.Add(item.GetUserItemByName("Exp Bottle lv1"));
                items.Add(item.GetUserItemByName("Exp Bottle lv2"));
                items.Add(item.GetUserItemByName("Exp Bottle lv3"));
                items.Add(item.GetUserItemByName("Exp Bottle lv4"));
                items.Add(item.GetUserItemByName("Exp Bottle lv5"));
                items.Add(item.GetUserItemByName("Exp Bottle lv6"));
                break;
            case "CardAdmirals":
                items.Add(item.GetUserItemByName("Exp Bottle lv1"));
                items.Add(item.GetUserItemByName("Exp Bottle lv2"));
                items.Add(item.GetUserItemByName("Exp Bottle lv3"));
                items.Add(item.GetUserItemByName("Exp Bottle lv4"));
                items.Add(item.GetUserItemByName("Exp Bottle lv5"));
                items.Add(item.GetUserItemByName("Exp Bottle lv6"));
                break;
            case "Achievements":
                items.Add(item.GetUserItemByName("Exp Achievements"));
                break;
            default:
                items.Add(item.GetUserItemByName("Exp Bottle lv1"));
                items.Add(item.GetUserItemByName("Exp Bottle lv2"));
                items.Add(item.GetUserItemByName("Exp Bottle lv3"));
                items.Add(item.GetUserItemByName("Exp Bottle lv4"));
                items.Add(item.GetUserItemByName("Exp Bottle lv5"));
                items.Add(item.GetUserItemByName("Exp Bottle lv6"));
                break;
        }
        return items;
    }
    public List<Items> GetItemForBreakthourgh(string type)
    {
        Items item = new Items();
        List<Items> items = new List<Items>();
        switch (type)
        {
            case "CardHeroes":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "Books":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "CardCaptains":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "Pets":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "CollaborationEquipment":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "CardMilitary":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "CardSpell":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "Collaboration":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "CardMonsters":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "Equipments":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "Medals":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "Skills":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "Symbols":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "Titles":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "MagicFormationCircle":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "Relics":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "CardColonels":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "CardGenerals":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "CardAdmirals":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            case "Achievements":
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
            default:
                items.Add(item.GetUserItemByName("Breakthrough Token"));
                break;
        }
        return items;
    }
    public List<Items> GetItemForRank(string type)
    {
        Items item = new Items();
        List<Items> items = new List<Items>();
        switch (type)
        {
            case "Affinity":
                items.Add(item.GetUserItemByName("Affinity 1"));
                items.Add(item.GetUserItemByName("Affinity 2"));
                items.Add(item.GetUserItemByName("Affinity 3"));
                items.Add(item.GetUserItemByName("Affinity 4"));
                items.Add(item.GetUserItemByName("Affinity 5"));
                items.Add(item.GetUserItemByName("Affinity 6"));
                items.Add(item.GetUserItemByName("Affinity 7"));
                items.Add(item.GetUserItemByName("Affinity 8"));
                items.Add(item.GetUserItemByName("Affinity 9"));
                items.Add(item.GetUserItemByName("Affinity 10"));
                items.Add(item.GetUserItemByName("Affinity 11"));
                items.Add(item.GetUserItemByName("Affinity 12"));
                items.Add(item.GetUserItemByName("Affinity 13"));
                items.Add(item.GetUserItemByName("Affinity 14"));
                break;
            default:
                items.Add(item.GetUserItemByName("Affinity 1"));
                break;
        }
        return items;
    }
}
