using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class CardColonels
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
    public double health { get; set; }
    public double physical_attack { get; set; }
    public double physical_defense { get; set; }
    public double magical_attack { get; set; }
    public double magical_defense { get; set; }
    public double chemical_attack { get; set; }
    public double chemical_defense { get; set; }
    public double atomic_attack { get; set; }
    public double atomic_defense { get; set; }
    public double mental_attack { get; set; }
    public double mental_defense { get; set; }
    public double speed { get; set; }
    public double critical_damage { get; set; }
    public double critical_rate { get; set; }
    public double armor_penetration { get; set; }
    public double avoid { get; set; }
    public double absorbs_damage { get; set; }
    public double regenerate_vitality { get; set; }
    public double accuracy { get; set; }
    public float mana { get; set; }
    public double all_power { get; set; }
    public double all_health { get; set; }
    public double all_physical_attack { get; set; }
    public double all_physical_defense { get; set; }
    public double all_magical_attack { get; set; }
    public double all_magical_defense { get; set; }
    public double all_chemical_attack { get; set; }
    public double all_chemical_defense { get; set; }
    public double all_atomic_attack { get; set; }
    public double all_atomic_defense { get; set; }
    public double all_mental_attack { get; set; }
    public double all_mental_defense { get; set; }
    public double all_speed { get; set; }
    public double all_critical_damage { get; set; }
    public double all_critical_rate { get; set; }
    public double all_armor_penetration { get; set; }
    public double all_avoid { get; set; }
    public double all_absorbs_damage { get; set; }
    public double all_regenerate_vitality { get; set; }
    public double all_accuracy { get; set; }
    public float all_mana { get; set; }
    public string description { get; set; }
    public string status { get; set; }
    public int team_id { get; set; }
    public Currency currency { get; set; }
    public CardColonels()
    {
        power = -1;
        health = -1;
        physical_attack = -1;
        physical_defense = -1;
        magical_attack = -1;
        magical_defense = -1;
        chemical_attack = -1;
        chemical_defense = -1;
        atomic_attack = -1;
        atomic_defense = -1;
        mental_attack = -1;
        mental_defense = -1;
        speed = -1;
        critical_damage = -1;
        critical_rate = -1;
        armor_penetration = -1;
        avoid = -1;
        absorbs_damage = -1;
        regenerate_vitality = -1;
        accuracy = -1;
        all_power = -1;
        all_health = -1;
        all_physical_attack = -1;
        all_physical_defense = -1;
        all_magical_attack = -1;
        all_magical_defense = -1;
        all_chemical_attack = -1;
        all_chemical_defense = -1;
        all_atomic_attack = -1;
        all_atomic_defense = -1;
        all_mental_attack = -1;
        all_mental_defense = -1;
        all_speed = -1;
        all_critical_damage = -1;
        all_critical_rate = -1;
        all_armor_penetration = -1;
        all_avoid = -1;
        all_absorbs_damage = -1;
        all_regenerate_vitality = -1;
        all_accuracy = -1;
        team_id=-1;
    }
    public static List<string> GetUniqueCardColonelsTypes()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "Select distinct type from card_colonels";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(0));
            }
        }
        return typeList;
    }
    public List<CardColonels> GetCardColonels(string type,int pageSize, int offset)
    {
        List<CardColonels> CardColonelsList = new List<CardColonels>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from card_colonels where type= @type 
                ORDER BY card_colonels.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(card_colonels.name, '[0-9]+$') AS UNSIGNED), card_colonels.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardColonels captain = new CardColonels
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description")
                    };

                    CardColonelsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardColonelsList;
    }
    public int GetCardColonelsCount(string type){
        int count =0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_colonels where type= @type";
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
    public List<CardColonels> GetCardColonelsCollection(string type,int pageSize, int offset)
    {
        List<CardColonels> CardColonelsList = new List<CardColonels>();
        int user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT c.*, CASE WHEN cg.card_colonel_id IS NULL THEN 'block' WHEN cg.status = 'pending' THEN 'pending' WHEN cg.status = 'available' THEN 'available' END AS status
                FROM card_colonels c LEFT JOIN card_colonels_gallery cg ON c.id = cg.card_colonel_id and cg.user_id = @userId where c.type=@type 
                ORDER BY c.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardColonels captain = new CardColonels
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description"),
                        status=reader.GetString("status")
                    };

                    CardColonelsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardColonelsList;
    }
    public List<CardColonels> GetUserCardColonels(string type,int pageSize, int offset)
    {
        List<CardColonels> CardColonelsList = new List<CardColonels>();
        int user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.*, fcc.*
                FROM user_card_colonels uc
                LEFT JOIN card_colonels c ON c.id = uc.card_colonel_id 
                LEFT JOIN fact_card_colonels fcc ON fcc.user_id = uc.user_id AND fcc.user_card_colonel_id = uc.card_colonel_id
                WHERE uc.user_id = @userId AND c.type = @type
                ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name
                LIMIT @limit OFFSET @offset;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardColonels captain = new CardColonels
                    {
                        id = reader.GetInt32("card_colonel_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
                        block = reader.GetBoolean("block"),
                        team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? -1 : reader.GetInt32("team_id"),
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description"),
                        all_power = reader.GetDouble("all_power"),
                        all_health = reader.GetDouble("all_health"),
                        all_physical_attack = reader.GetDouble("all_physical_attack"),
                        all_physical_defense = reader.GetDouble("all_physical_defense"),
                        all_magical_attack = reader.GetDouble("all_magical_attack"),
                        all_magical_defense = reader.GetDouble("all_magical_defense"),
                        all_chemical_attack = reader.GetDouble("all_chemical_attack"),
                        all_chemical_defense = reader.GetDouble("all_chemical_defense"),
                        all_atomic_attack = reader.GetDouble("all_atomic_attack"),
                        all_atomic_defense = reader.GetDouble("all_atomic_defense"),
                        all_mental_attack = reader.GetDouble("all_mental_attack"),
                        all_mental_defense = reader.GetDouble("all_mental_defense"),
                        all_speed = reader.GetDouble("all_speed"),
                        all_critical_damage = reader.GetDouble("all_critical_damage"),
                        all_critical_rate = reader.GetDouble("all_critical_rate"),
                        all_armor_penetration = reader.GetDouble("all_armor_penetration"),
                        all_avoid = reader.GetDouble("all_avoid"),
                        all_absorbs_damage = reader.GetDouble("all_absorbs_damage"),
                        all_regenerate_vitality = reader.GetDouble("all_regenerate_vitality"),
                        all_accuracy = reader.GetDouble("all_accuracy"),
                        all_mana = reader.GetFloat("all_mana"),
                    };

                    CardColonelsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardColonelsList;
    }
    public List<CardColonels> GetUserCardColonelsTeam(int teamId)
    {
        List<CardColonels> CardColonelsList = new List<CardColonels>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.*, fcc.*
                FROM user_card_colonels uc
                LEFT JOIN card_colonels c ON c.id = uc.card_colonel_id 
                LEFT JOIN fact_card_colonels fcc ON fcc.user_id = uc.user_id AND fcc.user_card_colonel_id = uc.card_colonel_id
                WHERE uc.user_id = @userId AND fcc.team_id=@team_id
                ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                command.Parameters.AddWithValue("@team_id", teamId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardColonels captain = new CardColonels
                    {
                        id = reader.GetInt32("card_colonel_id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
                        block = reader.GetBoolean("block"),
                        team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? -1 : reader.GetInt32("team_id"),
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description"),
                        all_power = reader.GetDouble("all_power"),
                        all_health = reader.GetDouble("all_health"),
                        all_physical_attack = reader.GetDouble("all_physical_attack"),
                        all_physical_defense = reader.GetDouble("all_physical_defense"),
                        all_magical_attack = reader.GetDouble("all_magical_attack"),
                        all_magical_defense = reader.GetDouble("all_magical_defense"),
                        all_chemical_attack = reader.GetDouble("all_chemical_attack"),
                        all_chemical_defense = reader.GetDouble("all_chemical_defense"),
                        all_atomic_attack = reader.GetDouble("all_atomic_attack"),
                        all_atomic_defense = reader.GetDouble("all_atomic_defense"),
                        all_mental_attack = reader.GetDouble("all_mental_attack"),
                        all_mental_defense = reader.GetDouble("all_mental_defense"),
                        all_speed = reader.GetDouble("all_speed"),
                        all_critical_damage = reader.GetDouble("all_critical_damage"),
                        all_critical_rate = reader.GetDouble("all_critical_rate"),
                        all_armor_penetration = reader.GetDouble("all_armor_penetration"),
                        all_avoid = reader.GetDouble("all_avoid"),
                        all_absorbs_damage = reader.GetDouble("all_absorbs_damage"),
                        all_regenerate_vitality = reader.GetDouble("all_regenerate_vitality"),
                        all_accuracy = reader.GetDouble("all_accuracy"),
                        all_mana = reader.GetFloat("all_mana"),
                    };

                    CardColonelsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardColonelsList;
    }
    public int GetUserCardColonelsCount(string type){
        int count =0;
        int user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_colonels c, user_card_colonels uc where c.id=uc.card_colonel_id and uc.user_id=@userId and c.type= @type";
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
    public List<CardColonels> GetCardColonelsRandom(string type,int pageSize)
    {
        List<CardColonels> CardColonelsList = new List<CardColonels>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_colonels where type= @type ORDER BY RAND() limit @limit";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardColonels captain = new CardColonels
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description")
                    };

                    CardColonelsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardColonelsList;
    }
    public List<CardColonels> GetAllCardColonels(string type)
    {
        List<CardColonels> CardColonelsList = new List<CardColonels>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_colonels where type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardColonels captain = new CardColonels
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description")
                    };

                    CardColonelsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardColonelsList;
    }
    public bool InsertUserCardColonels(CardColonels CardColonels)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_colonels
                WHERE user_id = @user_id AND card_colonel_id = @card_colonel_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_colonel_id", CardColonels.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_card_colonels (
                    user_id, card_colonel_id, level, experiment, star, block, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage, critical_rate, 
                    armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana
                ) VALUES (
                    @user_id, @card_colonel_id, @level, @experiment, @star, @block, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, @critical_rate, 
                    @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@card_colonel_id", CardColonels.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", 1);
                    command.Parameters.AddWithValue("@power", CardColonels.power);
                    command.Parameters.AddWithValue("@health", CardColonels.health);
                    command.Parameters.AddWithValue("@physical_attack", CardColonels.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", CardColonels.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", CardColonels.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", CardColonels.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", CardColonels.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", CardColonels.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", CardColonels.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", CardColonels.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", CardColonels.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", CardColonels.mental_defense);
                    command.Parameters.AddWithValue("@speed", CardColonels.speed);
                    command.Parameters.AddWithValue("@critical_damage", CardColonels.critical_damage);
                    command.Parameters.AddWithValue("@critical_rate", CardColonels.critical_rate);
                    command.Parameters.AddWithValue("@armor_penetration", CardColonels.armor_penetration);
                    command.Parameters.AddWithValue("@avoid", CardColonels.avoid);
                    command.Parameters.AddWithValue("@absorbs_damage", CardColonels.absorbs_damage);
                    command.Parameters.AddWithValue("@regenerate_vitality", CardColonels.regenerate_vitality);
                    command.Parameters.AddWithValue("@accuracy", CardColonels.accuracy);
                    command.Parameters.AddWithValue("@mana", CardColonels.mana);
                    MySqlDataReader reader = command.ExecuteReader();
                    InsertFactCardColonels(CardColonels);
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_card_colonels
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND card_colonel_id = @card_colonel_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@card_colonel_id", CardColonels.id);

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
    public bool InsertFactCardColonels(CardColonels cardColonels)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO fact_card_colonels (
                    user_id, user_card_colonel_id, all_power,
                    all_health, all_physical_attack, all_physical_defense, all_magical_attack, all_magical_defense,
                    all_chemical_attack, all_chemical_defense, all_atomic_attack, all_atomic_defense,
                    all_mental_attack, all_mental_defense, all_speed, all_critical_damage, all_critical_rate,
                    all_armor_penetration, all_avoid, all_absorbs_damage, all_regenerate_vitality, all_accuracy, all_mana
                ) VALUES (
                    @user_id, @user_card_colonel_id, @all_power,
                    @all_health, @all_physical_attack, @all_physical_defense, @all_magical_attack, @all_magical_defense,
                    @all_chemical_attack, @all_chemical_defense, @all_atomic_attack, @all_atomic_defense,
                    @all_mental_attack, @all_mental_defense, @all_speed, @all_critical_damage, @all_critical_rate,
                    @all_armor_penetration, @all_avoid, @all_absorbs_damage, @all_regenerate_vitality, @all_accuracy, @all_mana
                );";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_colonel_id", cardColonels.id);
                command.Parameters.AddWithValue("@all_power", cardColonels.power);
                command.Parameters.AddWithValue("@all_health", cardColonels.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardColonels.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardColonels.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardColonels.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardColonels.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardColonels.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardColonels.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardColonels.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardColonels.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardColonels.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardColonels.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardColonels.speed);
                command.Parameters.AddWithValue("@all_critical_damage", cardColonels.critical_damage);
                command.Parameters.AddWithValue("@all_critical_rate", cardColonels.critical_rate);
                command.Parameters.AddWithValue("@all_armor_penetration", cardColonels.armor_penetration);
                command.Parameters.AddWithValue("@all_avoid", cardColonels.avoid);
                command.Parameters.AddWithValue("@all_absorbs_damage", cardColonels.absorbs_damage);
                command.Parameters.AddWithValue("@all_regenerate_vitality", cardColonels.regenerate_vitality);
                command.Parameters.AddWithValue("@all_accuracy", cardColonels.accuracy);
                command.Parameters.AddWithValue("@all_mana", cardColonels.mana);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateTeamFactCardColonels(int? team_id,string position, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update fact_card_colonels set team_id=@team_id, position=@position where user_id=@user_id 
                and user_card_colonel_id=@user_card_colonel_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_colonel_id", card_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public CardColonels GetCardColonelsById(int Id)
    {
        CardColonels captain = new CardColonels();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_colonels where card_colonels.id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    captain = new CardColonels
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description")
                    };
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return captain;
    }
    public void InsertCardColonelsGallery(int Id)
    {
        CardColonels CaptainFromDB = GetCardColonelsById(Id);
        int percent = 0;
        if (CaptainFromDB.rare.Equals("LG"))
        {
            percent = 20;
        }
        else if (CaptainFromDB.rare.Equals("UR"))
        {
            percent = 10;
        }
        else if (CaptainFromDB.rare.Equals("SSR"))
        {
            percent = 5;
        }
        else if (CaptainFromDB.rare.Equals("SR"))
        {
            percent = 2;
        }
        else if (CaptainFromDB.rare.Equals("MR"))
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
                FROM card_colonels_gallery 
                WHERE user_id = @user_id AND card_colonel_id = @card_colonel_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_colonel_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO card_colonels_gallery (
                        user_id, card_colonel_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
                        magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, speed, critical_damage, critical_rate, armor_penetration, avoid, 
                        absorbs_damage, regenerate_vitality, accuracy, mana, percent_all_health, percent_all_physical_attack, 
                        percent_all_physical_defense, percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, percent_all_mental_attack, 
                        percent_all_mental_defense
                    ) VALUES (
                        @user_id, @card_colonel_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
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
                    command.Parameters.AddWithValue("@card_colonel_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);
                    command.Parameters.AddWithValue("@power", CaptainFromDB.power);
                    command.Parameters.AddWithValue("@health", CaptainFromDB.health);
                    command.Parameters.AddWithValue("@physical_attack", CaptainFromDB.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", CaptainFromDB.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", CaptainFromDB.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", CaptainFromDB.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", CaptainFromDB.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", CaptainFromDB.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", CaptainFromDB.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", CaptainFromDB.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", CaptainFromDB.magical_attack);
                    command.Parameters.AddWithValue("@mental_defense", CaptainFromDB.magical_defense);
                    command.Parameters.AddWithValue("@speed", CaptainFromDB.speed);
                    command.Parameters.AddWithValue("@critical_damage", CaptainFromDB.critical_damage);
                    command.Parameters.AddWithValue("@critical_rate", CaptainFromDB.critical_rate);
                    command.Parameters.AddWithValue("@armor_penetration", CaptainFromDB.armor_penetration);
                    command.Parameters.AddWithValue("@avoid", CaptainFromDB.avoid);
                    command.Parameters.AddWithValue("@absorbs_damage", CaptainFromDB.absorbs_damage);
                    command.Parameters.AddWithValue("@regenerate_vitality", CaptainFromDB.regenerate_vitality);
                    command.Parameters.AddWithValue("@accuracy", CaptainFromDB.accuracy);
                    command.Parameters.AddWithValue("@mana", CaptainFromDB.mana);
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
    public void UpdateStatusCardColonelsGallery(int Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update card_colonels_gallery set status=@status where user_id=@user_id and card_colonel_id=@card_colonel_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_colonel_id", Id);
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
    public List<CardColonels> GetCardColonelsWithPrice(string type,int pageSize, int offset)
    {
        List<CardColonels> CardColonelsList = new List<CardColonels>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select c.*, ct.price, cu.image as currency_image, cu.id as currency_id
                from card_colonels c, card_colonel_trade ct, currency cu
                where c.id=ct.card_colonel_id and ct.currency_id = cu.id and c.type =@type
                ORDER BY c.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name limit @limit offset @offset;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardColonels captain = new CardColonels
                    {
                        id = reader.GetInt32("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        type = reader.GetString("type"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("health"),
                        physical_attack = reader.GetDouble("physical_attack"),
                        physical_defense = reader.GetDouble("physical_defense"),
                        magical_attack = reader.GetDouble("magical_attack"),
                        magical_defense = reader.GetDouble("magical_defense"),
                        chemical_attack = reader.GetDouble("chemical_attack"),
                        chemical_defense = reader.GetDouble("chemical_defense"),
                        atomic_attack = reader.GetDouble("atomic_attack"),
                        atomic_defense = reader.GetDouble("atomic_defense"),
                        mental_attack = reader.GetDouble("mental_attack"),
                        mental_defense = reader.GetDouble("mental_defense"),
                        speed = reader.GetDouble("speed"),
                        critical_damage = reader.GetDouble("critical_damage"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        armor_penetration = reader.GetDouble("armor_penetration"),
                        avoid = reader.GetDouble("avoid"),
                        absorbs_damage = reader.GetDouble("absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("regenerate_vitality"),
                        accuracy = reader.GetDouble("accuracy"),
                        mana = reader.GetFloat("mana"),
                        description = reader.GetString("description")
                    };
                    captain.currency = new Currency{
                        id = reader.GetInt32("currency_id"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("price")
                    };

                    CardColonelsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardColonelsList;
    }
    public int GetCardColonelsWithPriceCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*)
                from card_colonels c, card_colonel_trade ct, currency cu
                where c.id=ct.card_colonel_id and ct.currency_id = cu.id and c.type =@type;";
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
