using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class CardSpell
{
    public int id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public string rare { get; set; }
    public string type { get; set; }
    public int star { get; set; }
    public int level { get; set; }
    public int experiment { get; set; }
    public int quantity { get; set; }
    public bool block { get; set; }
    public string position { get; set; }
    public double power { get; set; }
    public double percent_all_health { get; set; }
    public double percent_all_physical_attack { get; set; }
    public double percent_all_physical_defense { get; set; }
    public double percent_all_magical_attack { get; set; }
    public double percent_all_magical_defense { get; set; }
    public double percent_all_chemical_attack { get; set; }
    public double percent_all_chemical_defense { get; set; }
    public double percent_all_atomic_attack { get; set; }
    public double percent_all_atomic_defense { get; set; }
    public double percent_all_mental_attack { get; set; }
    public double percent_all_mental_defense { get; set; }
    public double percent_all_speed { get; set; }
    public double percent_all_critical_damage { get; set; }
    public double percent_all_critical_rate { get; set; }
    public double percent_all_armor_penetration { get; set; }
    public double percent_all_avoid { get; set; }
    public double percent_all_absorbs_damage { get; set; }
    public double percent_all_regenerate_vitality { get; set; }
    public double percent_all_accuracy{ get; set; }
    public float percent_all_mana { get; set; }
    public string description { get; set; }
    public string status { get; set; }
    public int team_id { get; set; }
    public Currency currency { get; set; }
    public CardSpell()
    {

    }
    public static List<string> GetUniqueCardSpellTypes()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "Select distinct type from card_spell";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(0));
            }
        }
        return typeList;
    }
    public List<CardSpell> GetCardSpell(string type, int pageSize, int offset)
    {
        List<CardSpell> CardSpellList = new List<CardSpell>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from card_spell where type= @type 
                ORDER BY card_spell.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(card_spell.name, '[0-9]+$') AS UNSIGNED), card_spell.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardSpell CardSpell = new CardSpell
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        percent_all_health = reader.GetDouble("percent_all_health"),
                        percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        percent_all_speed = reader.GetDouble("percent_all_speed"),
                        percent_all_critical_damage = reader.GetDouble("percent_all_critical_damage"),
                        percent_all_critical_rate = reader.GetDouble("percent_all_critical_rate"),
                        percent_all_armor_penetration = reader.GetDouble("percent_all_armor_penetration"),
                        percent_all_avoid = reader.GetDouble("percent_all_avoid"),
                        percent_all_absorbs_damage = reader.GetDouble("percent_all_absorbs_damage"),
                        percent_all_regenerate_vitality = reader.GetDouble("percent_all_regenerate_vitality"),
                        percent_all_accuracy = reader.GetDouble("percent_all_accuracy"),
                        percent_all_mana = reader.GetFloat("percent_all_mana"),
                        description = reader.GetString("description")
                    };

                    CardSpellList.Add(CardSpell);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardSpellList;
    }
    public int GetCardSpellCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_spell where type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                count = Convert.ToInt32(command.ExecuteScalar());

                return count;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return count;
    }
    public List<CardSpell> GetCardSpellCollection(string type, int pageSize, int offset)
    {
        List<CardSpell> CardSpellList = new List<CardSpell>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT s.*, CASE WHEN sg.card_spell_id IS NULL THEN 'block' WHEN sg.status = 'pending' THEN 'pending' WHEN sg.status = 'available' THEN 'available' END AS status 
                FROM card_spell s LEFT JOIN card_spell_gallery sg ON s.id = sg.card_spell_id and sg.user_id = @userId where s.type=@type 
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardSpell CardSpell = new CardSpell
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        percent_all_health = reader.GetDouble("percent_all_health"),
                        percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        percent_all_speed = reader.GetDouble("percent_all_speed"),
                        percent_all_critical_damage = reader.GetDouble("percent_all_critical_damage"),
                        percent_all_critical_rate = reader.GetDouble("percent_all_critical_rate"),
                        percent_all_armor_penetration = reader.GetDouble("percent_all_armor_penetration"),
                        percent_all_avoid = reader.GetDouble("percent_all_avoid"),
                        percent_all_absorbs_damage = reader.GetDouble("percent_all_absorbs_damage"),
                        percent_all_regenerate_vitality = reader.GetDouble("percent_all_regenerate_vitality"),
                        percent_all_accuracy = reader.GetDouble("percent_all_accuracy"),
                        percent_all_mana = reader.GetFloat("percent_all_mana"),
                        description = reader.GetString("description"),
                        status = reader.GetString("status"),
                    };

                    CardSpellList.Add(CardSpell);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardSpellList;
    }
    public List<CardSpell> GetUserCardSpell(string type, int pageSize, int offset)
    {
        List<CardSpell> CardSpellList = new List<CardSpell>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select us.*, s.* from card_spell s, user_card_spell us where s.id=us.card_spell_id and us.user_id=@userId and s.type= @type 
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardSpell CardSpell = new CardSpell
                    {
                        id = reader.GetInt32("card_spell_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
                        block = reader.GetBoolean("block"),
                        power = reader.GetDouble("power"),
                        percent_all_health = reader.GetDouble("percent_all_health"),
                        percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        percent_all_speed = reader.GetDouble("percent_all_speed"),
                        percent_all_critical_damage = reader.GetDouble("percent_all_critical_damage"),
                        percent_all_critical_rate = reader.GetDouble("percent_all_critical_rate"),
                        percent_all_armor_penetration = reader.GetDouble("percent_all_armor_penetration"),
                        percent_all_avoid = reader.GetDouble("percent_all_avoid"),
                        percent_all_absorbs_damage = reader.GetDouble("percent_all_absorbs_damage"),
                        percent_all_regenerate_vitality = reader.GetDouble("percent_all_regenerate_vitality"),
                        percent_all_accuracy = reader.GetDouble("percent_all_accuracy"),
                        percent_all_mana = reader.GetFloat("percent_all_mana"),
                        description = reader.GetString("description")
                    };

                    CardSpellList.Add(CardSpell);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardSpellList;
    }
    public int GetUserCardSpellCount(string type)
    {
        int count = 0;
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_spell s, user_card_spell us where s.id=us.card_spell_id and us.user_id=@userId and s.type= @type";
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
        }
        return count;
    }
    public List<CardSpell> GetCardSpellRandom(string type, int pageSize)
    {
        List<CardSpell> CardSpellList = new List<CardSpell>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_spell where type= @type ORDER BY RAND() limit @limit";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardSpell CardSpell = new CardSpell
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        percent_all_health = reader.GetDouble("percent_all_health"),
                        percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        percent_all_speed = reader.GetDouble("percent_all_speed"),
                        percent_all_critical_damage = reader.GetDouble("percent_all_critical_damage"),
                        percent_all_critical_rate = reader.GetDouble("percent_all_critical_rate"),
                        percent_all_armor_penetration = reader.GetDouble("percent_all_armor_penetration"),
                        percent_all_avoid = reader.GetDouble("percent_all_avoid"),
                        percent_all_absorbs_damage = reader.GetDouble("percent_all_absorbs_damage"),
                        percent_all_regenerate_vitality = reader.GetDouble("percent_all_regenerate_vitality"),
                        percent_all_accuracy = reader.GetDouble("percent_all_accuracy"),
                        percent_all_mana = reader.GetFloat("percent_all_mana"),
                        description = reader.GetString("description")
                    };

                    CardSpellList.Add(CardSpell);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardSpellList;
    }
    public List<CardSpell> GetAllCardSpell(string type)
    {
        List<CardSpell> CardSpellList = new List<CardSpell>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_spell where type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardSpell CardSpell = new CardSpell
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        percent_all_health = reader.GetDouble("percent_all_health"),
                        percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        percent_all_speed = reader.GetDouble("percent_all_speed"),
                        percent_all_critical_damage = reader.GetDouble("percent_all_critical_damage"),
                        percent_all_critical_rate = reader.GetDouble("percent_all_critical_rate"),
                        percent_all_armor_penetration = reader.GetDouble("percent_all_armor_penetration"),
                        percent_all_avoid = reader.GetDouble("percent_all_avoid"),
                        percent_all_absorbs_damage = reader.GetDouble("percent_all_absorbs_damage"),
                        percent_all_regenerate_vitality = reader.GetDouble("percent_all_regenerate_vitality"),
                        percent_all_accuracy = reader.GetDouble("percent_all_accuracy"),
                        percent_all_mana = reader.GetFloat("percent_all_mana"),
                        description = reader.GetString("description")
                    };

                    CardSpellList.Add(CardSpell);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardSpellList;
    }
    public bool InsertUserCardSpell(CardSpell CardSpell)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_spell
                WHERE user_id = @user_id AND card_spell_id = @card_spell_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_spell_id", CardSpell.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                    INSERT INTO user_card_spell (
                        user_id, card_spell_id, level, experiment, star, block, quantity, power,
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense,
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack,
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense,
                        percent_all_mental_attack, percent_all_mental_defense, percent_all_speed,
                        percent_all_critical_damage, percent_all_critical_rate, percent_all_armor_penetration,
                        percent_all_avoid, percent_all_absorbs_damage, percent_all_regenerate_vitality,
                        percent_all_accuracy, percent_all_mana
                    ) VALUES (
                        @user_id, @card_spell_id, @level, @experiment, @star, @block, @quantity, @power,
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense,
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack,
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense,
                        @percent_all_mental_attack, @percent_all_mental_defense, @percent_all_speed,
                        @percent_all_critical_damage, @percent_all_critical_rate, @percent_all_armor_penetration,
                        @percent_all_avoid, @percent_all_absorbs_damage, @percent_all_regenerate_vitality,
                        @percent_all_accuracy, @percent_all_mana
                    );
                    ";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@card_spell_id", CardSpell.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", 1);
                    command.Parameters.AddWithValue("@power", CardSpell.power);
                    command.Parameters.AddWithValue("@percent_all_health", CardSpell.percent_all_health);
                    command.Parameters.AddWithValue("@percent_all_physical_attack", CardSpell.percent_all_physical_attack);
                    command.Parameters.AddWithValue("@percent_all_physical_defense", CardSpell.percent_all_physical_defense);
                    command.Parameters.AddWithValue("@percent_all_magical_attack", CardSpell.percent_all_magical_attack);
                    command.Parameters.AddWithValue("@percent_all_magical_defense", CardSpell.percent_all_magical_defense);
                    command.Parameters.AddWithValue("@percent_all_chemical_attack", CardSpell.percent_all_chemical_attack);
                    command.Parameters.AddWithValue("@percent_all_chemical_defense", CardSpell.percent_all_chemical_defense);
                    command.Parameters.AddWithValue("@percent_all_atomic_attack", CardSpell.percent_all_atomic_attack);
                    command.Parameters.AddWithValue("@percent_all_atomic_defense", CardSpell.percent_all_atomic_defense);
                    command.Parameters.AddWithValue("@percent_all_mental_attack", CardSpell.percent_all_mental_attack);
                    command.Parameters.AddWithValue("@percent_all_mental_defense", CardSpell.percent_all_mental_defense);
                    command.Parameters.AddWithValue("@percent_all_speed", CardSpell.percent_all_speed);
                    command.Parameters.AddWithValue("@percent_all_critical_damage", CardSpell.percent_all_critical_damage);
                    command.Parameters.AddWithValue("@percent_all_critical_rate", CardSpell.percent_all_critical_rate);
                    command.Parameters.AddWithValue("@percent_all_armor_penetration", CardSpell.percent_all_armor_penetration);
                    command.Parameters.AddWithValue("@percent_all_avoid", CardSpell.percent_all_avoid);
                    command.Parameters.AddWithValue("@percent_all_absorbs_damage", CardSpell.percent_all_absorbs_damage);
                    command.Parameters.AddWithValue("@percent_all_regenerate_vitality", CardSpell.percent_all_regenerate_vitality);
                    command.Parameters.AddWithValue("@percent_all_accuracy", CardSpell.percent_all_accuracy);
                    command.Parameters.AddWithValue("@percent_all_mana", CardSpell.percent_all_mana);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_card_spell
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND card_spell_id = @card_spell_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@card_spell_id", CardSpell.id);

                    updateCommand.ExecuteNonQuery();
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return true;
    }
    public CardSpell GetCardSpellById(int Id)
    {
        CardSpell CardSpell = new CardSpell();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_spell where card_spell.id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardSpell = new CardSpell
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        description = reader.GetString("description")
                    };
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardSpell;
    }
    public void InsertCardSpellGallery(int Id)
    {
        CardSpell CardSpellFromDB = GetCardSpellById(Id);
        int percent = 0;
        if (CardSpellFromDB.rare.Equals("LG"))
        {
            percent = 20;
        }
        else if (CardSpellFromDB.rare.Equals("UR"))
        {
            percent = 10;
        }
        else if (CardSpellFromDB.rare.Equals("SSR"))
        {
            percent = 5;
        }
        else if (CardSpellFromDB.rare.Equals("SR"))
        {
            percent = 2;
        }
        else if (CardSpellFromDB.rare.Equals("MR"))
        {
            percent = 30;
        }
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra bản ghi đã tồn tại
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM card_spell_gallery 
                WHERE user_id = @user_id AND card_spell_id = @card_spell_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_spell_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO card_spell_gallery (
                        user_id, card_spell_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
                        magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, speed, critical_damage, critical_rate, armor_penetration, avoid, 
                        absorbs_damage, regenerate_vitality, accuracy, mana, percent_all_health, percent_all_physical_attack, 
                        percent_all_physical_defense, percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, percent_all_mental_attack, 
                        percent_all_mental_defense
                    ) VALUES (
                        @user_id, @card_spell_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
                        @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, 
                        @mental_attack, @mental_defense, @speed, @critical_damage, @critical_rate, @armor_penetration, @avoid, 
                        @absorbs_damage, @regenerate_vitality, @accuracy, @mana, @percent_all_health, @percent_all_physical_attack, 
                        @percent_all_physical_defense, @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, @percent_all_mental_attack, 
                        @percent_all_mental_defense
                    );
                    ";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@card_spell_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);
                    command.Parameters.AddWithValue("@power", 195500120);
                    command.Parameters.AddWithValue("@health", 50000000);
                    command.Parameters.AddWithValue("@physical_attack", 10000000);
                    command.Parameters.AddWithValue("@physical_defense", 5000000);
                    command.Parameters.AddWithValue("@magical_attack", 10000000);
                    command.Parameters.AddWithValue("@magical_defense", 5000000);
                    command.Parameters.AddWithValue("@chemical_attack", 10000000);
                    command.Parameters.AddWithValue("@chemical_defense", 5000000);
                    command.Parameters.AddWithValue("@atomic_attack", 10000000);
                    command.Parameters.AddWithValue("@atomic_defense", 5000000);
                    command.Parameters.AddWithValue("@mental_attack", 10000000);
                    command.Parameters.AddWithValue("@mental_defense", 5000000);
                    command.Parameters.AddWithValue("@speed", 10000000);
                    command.Parameters.AddWithValue("@critical_damage", 1000000);
                    command.Parameters.AddWithValue("@critical_rate", 50);
                    command.Parameters.AddWithValue("@armor_penetration", 5000000);
                    command.Parameters.AddWithValue("@avoid", 50);
                    command.Parameters.AddWithValue("@absorbs_damage", 10000000);
                    command.Parameters.AddWithValue("@regenerate_vitality", 5000000);
                    command.Parameters.AddWithValue("@accuracy", 50);
                    command.Parameters.AddWithValue("@mana", 1000);
                    command.Parameters.AddWithValue("@percent_all_health", percent);
                    command.Parameters.AddWithValue("@percent_all_physical_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_physical_defense", percent);
                    command.Parameters.AddWithValue("@percent_all_magical_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_magical_defense", percent);
                    command.Parameters.AddWithValue("@percent_all_chemical_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_chemical_defense", percent);
                    command.Parameters.AddWithValue("@percent_all_atomic_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_atomic_defense", percent);
                    command.Parameters.AddWithValue("@percent_all_mental_attack", percent);
                    command.Parameters.AddWithValue("@percent_all_mental_defense", percent);
                    command.ExecuteNonQuery();
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
    }
    public void UpdateStatusCardSpellGallery(int Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update card_spell_gallery set status=@status where user_id=@user_id and card_spell_id=@card_spell_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@CardSpell_id", Id);
                command.Parameters.AddWithValue("@status", "available");
                command.ExecuteNonQuery();
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
    }
    public List<CardSpell> GetCardSpellWithPrice(string type, int pageSize, int offset)
    {
        List<CardSpell> CardSpellList = new List<CardSpell>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select s.*, st.price, cu.image as currency_image, cu.id as currency_id
                from card_spell s, card_spell_trade st, currency cu
                where s.id=st.card_spell_id and st.currency_id = cu.id and s.type =@type
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardSpell CardSpell = new CardSpell
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        percent_all_health = reader.GetDouble("percent_all_health"),
                        percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        percent_all_speed = reader.GetDouble("percent_all_speed"),
                        percent_all_critical_damage = reader.GetDouble("percent_all_critical_damage"),
                        percent_all_critical_rate = reader.GetDouble("percent_all_critical_rate"),
                        percent_all_armor_penetration = reader.GetDouble("percent_all_armor_penetration"),
                        percent_all_avoid = reader.GetDouble("percent_all_avoid"),
                        percent_all_absorbs_damage = reader.GetDouble("percent_all_absorbs_damage"),
                        percent_all_regenerate_vitality = reader.GetDouble("percent_all_regenerate_vitality"),
                        percent_all_accuracy = reader.GetDouble("percent_all_accuracy"),
                        percent_all_mana = reader.GetFloat("percent_all_mana"),
                        description = reader.GetString("description")
                    };
                    CardSpell.currency = new Currency{
                        id = reader.GetInt32("currency_id"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("price")
                    };

                    CardSpellList.Add(CardSpell);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardSpellList;
    }
    public int GetCardSpellWithPriceCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*)
                from card_spell s, card_spell_trade st, currency cu
                where s.id=st.card_spell_id and st.currency_id = cu.id and s.type =@type;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                count = Convert.ToInt32(command.ExecuteScalar());

                return count;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return count;
    }
}
