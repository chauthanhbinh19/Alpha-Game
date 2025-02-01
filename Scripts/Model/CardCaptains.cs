using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class CardCaptains
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
    public Currency currency { get; set; }
    public CardCaptains()
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
        percent_all_health = -1;
        percent_all_physical_attack = -1;
        percent_all_physical_defense = -1;
        percent_all_magical_attack = -1;
        percent_all_magical_defense = -1;
        percent_all_chemical_attack = -1;
        percent_all_chemical_defense = -1;
        percent_all_atomic_attack = -1;
        percent_all_atomic_defense = -1;
        percent_all_mental_attack = -1;
        percent_all_mental_defense = -1;
    }
    public List<CardCaptains> GetFinalPower(List<CardCaptains> CardCaptainsList)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        foreach (var c in CardCaptainsList)
        {
            c.all_power = powerManager.GetFinalCardCaptainsPower(c);
            c.all_health = c.all_health + powerManager.health + c.all_health * powerManager.percent_all_health/100;
            c.all_physical_attack = c.all_physical_attack + powerManager.physical_attack + c.physical_attack * powerManager.percent_all_physical_attack/100;
            c.all_physical_defense = c.all_physical_defense + powerManager.physical_defense + c.physical_defense * powerManager.percent_all_physical_defense/100;
            c.all_magical_attack = c.all_magical_attack + powerManager.magical_attack + c.magical_attack * powerManager.percent_all_magical_attack/100;
            c.all_magical_defense = c.all_magical_defense + powerManager.magical_defense + c.magical_defense * powerManager.percent_all_magical_defense/100;
            c.all_chemical_attack = c.all_chemical_attack + powerManager.chemical_attack + c.chemical_attack * powerManager.percent_all_chemical_attack/100;
            c.all_chemical_defense = c.all_chemical_defense + powerManager.chemical_defense + c.chemical_defense * powerManager.percent_all_chemical_defense/100;
            c.all_atomic_attack = c.all_atomic_attack + powerManager.atomic_attack + c.atomic_attack * powerManager.percent_all_atomic_attack/100;
            c.all_atomic_defense = c.all_atomic_defense + powerManager.atomic_defense + c.atomic_defense * powerManager.percent_all_atomic_defense/100;
            c.all_mental_attack = c.all_mental_attack + powerManager.mental_attack + c.mental_attack * powerManager.percent_all_mental_attack/100;
            c.all_mental_defense = c.all_mental_defense + powerManager.mental_defense + c.mental_defense * powerManager.percent_all_mental_defense/100;
            c.all_speed = c.all_speed + powerManager.speed;
            c.all_critical_damage = c.all_critical_damage + powerManager.critical_damage;
            c.all_critical_rate = c.all_critical_rate + powerManager.critical_rate;
            c.all_armor_penetration = c.all_armor_penetration + powerManager.armor_penetration;
            c.all_avoid = c.all_avoid + powerManager.avoid;
            c.all_absorbs_damage = c.all_absorbs_damage + powerManager.absorbs_damage;
            c.all_regenerate_vitality = c.all_regenerate_vitality + powerManager.regenerate_vitality;
            c.all_accuracy = c.all_accuracy + powerManager.accuracy;
            c.all_mana = c.all_mana + powerManager.mana;
        }
        return CardCaptainsList;
    }
    public CardCaptains GetNewLevelPower(CardCaptains c, double coefficient)
    {
        CardCaptains orginCard = new CardCaptains();
        orginCard = orginCard.GetCardCaptainsById(c.id);
        CardCaptains cardCaptains = new CardCaptains
        {
            id = c.id,
            health = c.health + orginCard.health * coefficient,
            physical_attack = c.physical_attack + orginCard.physical_attack * coefficient,
            physical_defense = c.physical_defense + orginCard.physical_defense * coefficient,
            magical_attack = c.magical_attack + orginCard.magical_attack * coefficient,
            magical_defense = c.magical_defense + orginCard.magical_defense * coefficient,
            chemical_attack = c.chemical_attack + orginCard.chemical_attack * coefficient,
            chemical_defense = c.chemical_defense + orginCard.chemical_defense * coefficient,
            atomic_attack = c.atomic_attack + orginCard.atomic_attack * coefficient,
            atomic_defense = c.atomic_defense + orginCard.atomic_defense * coefficient,
            mental_attack = c.mental_attack + orginCard.mental_attack * coefficient,
            mental_defense = c.mental_defense + orginCard.mental_defense * coefficient,
            speed = c.speed + orginCard.speed * coefficient,
            critical_damage = c.critical_damage + orginCard.critical_damage * coefficient,
            critical_rate = c.critical_rate + orginCard.critical_rate * coefficient,
            armor_penetration = c.armor_penetration + orginCard.armor_penetration * coefficient,
            avoid = c.avoid + orginCard.avoid * coefficient,
            absorbs_damage = c.absorbs_damage + orginCard.absorbs_damage * coefficient,
            regenerate_vitality = c.regenerate_vitality + orginCard.regenerate_vitality * coefficient,
            accuracy = c.accuracy + orginCard.accuracy * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient
        };
        cardCaptains.power = 0.5 * (
            cardCaptains.health +
            cardCaptains.physical_attack +
            cardCaptains.physical_defense +
            cardCaptains.magical_attack +
            cardCaptains.magical_defense +
            cardCaptains.chemical_attack +
            cardCaptains.chemical_defense +
            cardCaptains.atomic_attack +
            cardCaptains.atomic_defense +
            cardCaptains.mental_attack +
            cardCaptains.mental_defense +
            cardCaptains.speed +
            cardCaptains.critical_damage +
            cardCaptains.critical_rate +
            cardCaptains.armor_penetration +
            cardCaptains.avoid +
            cardCaptains.absorbs_damage +
            cardCaptains.regenerate_vitality +
            cardCaptains.accuracy +
            cardCaptains.mana
        );
        return cardCaptains;
    }
    public CardCaptains GetNewBreakthroughPower(CardCaptains c, double coefficient)
    {
        CardCaptains orginCard = new CardCaptains();
        orginCard = orginCard.GetCardCaptainsById(c.id);
        CardCaptains cardCaptains = new CardCaptains
        {
            id = c.id,
            health = c.health + orginCard.health * coefficient,
            physical_attack = c.physical_attack + orginCard.physical_attack * coefficient,
            physical_defense = c.physical_defense + orginCard.physical_defense * coefficient,
            magical_attack = c.magical_attack + orginCard.magical_attack * coefficient,
            magical_defense = c.magical_defense + orginCard.magical_defense * coefficient,
            chemical_attack = c.chemical_attack + orginCard.chemical_attack * coefficient,
            chemical_defense = c.chemical_defense + orginCard.chemical_defense * coefficient,
            atomic_attack = c.atomic_attack + orginCard.atomic_attack * coefficient,
            atomic_defense = c.atomic_defense + orginCard.atomic_defense * coefficient,
            mental_attack = c.mental_attack + orginCard.mental_attack * coefficient,
            mental_defense = c.mental_defense + orginCard.mental_defense * coefficient,
            speed = c.speed + orginCard.speed * coefficient,
            critical_damage = c.critical_damage + orginCard.critical_damage * coefficient,
            critical_rate = c.critical_rate + orginCard.critical_rate * coefficient,
            armor_penetration = c.armor_penetration + orginCard.armor_penetration * coefficient,
            avoid = c.avoid + orginCard.avoid * coefficient,
            absorbs_damage = c.absorbs_damage + orginCard.absorbs_damage * coefficient,
            regenerate_vitality = c.regenerate_vitality + orginCard.regenerate_vitality * coefficient,
            accuracy = c.accuracy + orginCard.accuracy * coefficient,
            mana = c.mana + orginCard.mana * (float)coefficient
        };
        cardCaptains.power = 0.5 * (
            cardCaptains.health +
            cardCaptains.physical_attack +
            cardCaptains.physical_defense +
            cardCaptains.magical_attack +
            cardCaptains.magical_defense +
            cardCaptains.chemical_attack +
            cardCaptains.chemical_defense +
            cardCaptains.atomic_attack +
            cardCaptains.atomic_defense +
            cardCaptains.mental_attack +
            cardCaptains.mental_defense +
            cardCaptains.speed +
            cardCaptains.critical_damage +
            cardCaptains.critical_rate +
            cardCaptains.armor_penetration +
            cardCaptains.avoid +
            cardCaptains.absorbs_damage +
            cardCaptains.regenerate_vitality +
            cardCaptains.accuracy +
            cardCaptains.mana
        );
        return cardCaptains;
    }
    public static List<string> GetUniqueCardCaptainsTypes()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "Select distinct type from card_captains";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(0));
            }
        }
        return typeList;
    }
    public List<CardCaptains> GetCardCaptains(string type, int pageSize, int offset)
    {
        List<CardCaptains> CardCaptainsList = new List<CardCaptains>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from card_captains where type= @type 
                ORDER BY card_captains.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(card_captains.name, '[0-9]+$') AS UNSIGNED), card_captains.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardCaptains captain = new CardCaptains
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

                    CardCaptainsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardCaptainsList;
    }
    public int GetCardCaptainsCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_captains where type= @type";
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
    public List<CardCaptains> GetCardCaptainsCollection(string type, int pageSize, int offset)
    {
        List<CardCaptains> CardCaptainsList = new List<CardCaptains>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT c.*, CASE WHEN cg.card_captain_id IS NULL THEN 'block' WHEN cg.status = 'pending' THEN 'pending' WHEN cg.status = 'available' THEN 'available' END AS status
                FROM card_captains c LEFT JOIN card_captains_gallery cg ON c.id = cg.card_captain_id and cg.user_id = @userId where c.type=@type 
                ORDER BY c.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardCaptains captain = new CardCaptains
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
                        status = reader.GetString("status")
                    };

                    CardCaptainsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardCaptainsList;
    }
    public List<CardCaptains> GetUserCardCaptains(string type, int pageSize, int offset)
    {
        List<CardCaptains> CardCaptainsList = new List<CardCaptains>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.*, fcc.*
                FROM user_card_captains uc
                LEFT JOIN card_captains c ON c.id = uc.card_captain_id 
                LEFT JOIN fact_card_captains fcc ON fcc.user_id = uc.user_id AND fcc.user_card_captain_id = uc.card_captain_id
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
                    CardCaptains captain = new CardCaptains
                    {
                        id = reader.GetInt32("card_captain_id"),
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

                    CardCaptainsList.Add(captain);
                }
                CardCaptainsList = GetFinalPower(CardCaptainsList);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardCaptainsList;
    }
    public List<CardCaptains> GetUserCardCaptainsTeam( int teamId)
    {
        List<CardCaptains> CardCaptainsList = new List<CardCaptains>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.*, fcc.*
                FROM user_card_captains uc
                LEFT JOIN card_captains c ON c.id = uc.card_captain_id 
                LEFT JOIN fact_card_captains fcc ON fcc.user_id = uc.user_id AND fcc.user_card_captain_id = uc.card_captain_id
                WHERE uc.user_id = @userId AND fcc.team_id=@team_id
                ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                command.Parameters.AddWithValue("@team_id", teamId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardCaptains captain = new CardCaptains
                    {
                        id = reader.GetInt32("card_captain_id"),
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

                    CardCaptainsList.Add(captain);
                }
                CardCaptainsList = GetFinalPower(CardCaptainsList);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardCaptainsList;
    }
    public Dictionary<string, int> GetUniqueCardCaptainTypesTeam(int teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_card_captains uc
            LEFT JOIN card_captains c ON uc.card_captain_id = c.id 
            LEFT JOIN fact_card_captains fch ON fch.user_id = uc.user_id AND fch.user_card_captain_id = uc.card_captain_id
            WHERE uc.user_id =@userId and fch.team_id=@team_id
            group by c.type, c.type";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@userId", User.CurrentUserId);
            command.Parameters.AddWithValue("@team_id", teamId);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string type = reader["type"].ToString();
                int number = Convert.ToInt32(reader["number"]);

                result[type] = number;
            }
        }
        return result;
    }
    public bool UpdateTeamFactCardCaptains(int? team_id,string position, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update fact_card_captains set team_id=@team_id, position=@position where user_id=@user_id 
                and user_card_captain_id=@user_card_captain_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_captain_id", card_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public int GetUserCardCaptainsCount(string type)
    {
        int count = 0;
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_captains c, user_card_captains uc where c.id=uc.card_captain_id and uc.user_id=@userId and c.type= @type";
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
    public List<CardCaptains> GetCardCaptainsRandom(string type, int pageSize)
    {
        List<CardCaptains> CardCaptainsList = new List<CardCaptains>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_captains where type= @type ORDER BY RAND() limit @limit";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardCaptains captain = new CardCaptains
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

                    CardCaptainsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardCaptainsList;
    }
    public List<CardCaptains> GetAllCardCaptains(string type)
    {
        List<CardCaptains> CardCaptainsList = new List<CardCaptains>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_captains where type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardCaptains captain = new CardCaptains
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

                    CardCaptainsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardCaptainsList;
    }
    public bool InsertUserCardCaptains(CardCaptains CardCaptains)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_captains
                WHERE user_id = @user_id AND card_captain_id = @card_captain_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_captain_id", CardCaptains.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_card_captains (
                    user_id, card_captain_id, level, experiment, star, block, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage, critical_rate, 
                    armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana
                ) VALUES (
                    @user_id, @card_captain_id, @level, @experiment, @star, @block, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, @critical_rate, 
                    @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@card_captain_id", CardCaptains.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", 0);
                    command.Parameters.AddWithValue("@power", CardCaptains.power);
                    command.Parameters.AddWithValue("@health", CardCaptains.health);
                    command.Parameters.AddWithValue("@physical_attack", CardCaptains.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", CardCaptains.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", CardCaptains.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", CardCaptains.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", CardCaptains.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", CardCaptains.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", CardCaptains.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", CardCaptains.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", CardCaptains.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", CardCaptains.mental_defense);
                    command.Parameters.AddWithValue("@speed", CardCaptains.speed);
                    command.Parameters.AddWithValue("@critical_damage", CardCaptains.critical_damage);
                    command.Parameters.AddWithValue("@critical_rate", CardCaptains.critical_rate);
                    command.Parameters.AddWithValue("@armor_penetration", CardCaptains.armor_penetration);
                    command.Parameters.AddWithValue("@avoid", CardCaptains.avoid);
                    command.Parameters.AddWithValue("@absorbs_damage", CardCaptains.absorbs_damage);
                    command.Parameters.AddWithValue("@regenerate_vitality", CardCaptains.regenerate_vitality);
                    command.Parameters.AddWithValue("@accuracy", CardCaptains.accuracy);
                    command.Parameters.AddWithValue("@mana", CardCaptains.mana);
                    MySqlDataReader reader = command.ExecuteReader();
                    InsertFactCardCaptains(CardCaptains);
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_card_captains
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND card_captain_id = @card_captain_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@card_captain_id", CardCaptains.id);

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
    public bool UpdateCardCaptainsLevel(CardCaptains cardCaptains, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_captains
                SET level = @level,
                    power = @power, health = @health, physical_attack = @physicalAttack,
                    physical_defense = @physicalDefense, magical_attack = @magicalAttack,
                    magical_defense = @magicalDefense, chemical_attack = @chemicalAttack,
                    chemical_defense = @chemicalDefense, atomic_attack = @atomicAttack,
                    atomic_defense = @atomicDefense, mental_attack = @mentalAttack,
                    mental_defense = @mentalDefense, speed = @speed, critical_damage = @criticalDamage,
                    critical_rate = @criticalRate, armor_penetration = @armorPenetration,
                    avoid = @avoid, absorbs_damage = @absorbsDamage, regenerate_vitality = @regenerateVitality, 
                    accuracy = @accuracy, mana = @mana
                WHERE 
                    user_id = @user_id AND card_captain_id = @card_captain_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_captain_id", cardCaptains.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", cardCaptains.power);
                command.Parameters.AddWithValue("@health", cardCaptains.health);
                command.Parameters.AddWithValue("@physicalAttack", cardCaptains.physical_attack);
                command.Parameters.AddWithValue("@physicalDefense", cardCaptains.physical_defense);
                command.Parameters.AddWithValue("@magicalAttack", cardCaptains.magical_attack);
                command.Parameters.AddWithValue("@magicalDefense", cardCaptains.magical_defense);
                command.Parameters.AddWithValue("@chemicalAttack", cardCaptains.chemical_attack);
                command.Parameters.AddWithValue("@chemicalDefense", cardCaptains.chemical_defense);
                command.Parameters.AddWithValue("@atomicAttack", cardCaptains.atomic_attack);
                command.Parameters.AddWithValue("@atomicDefense", cardCaptains.atomic_defense);
                command.Parameters.AddWithValue("@mentalAttack", cardCaptains.mental_attack);
                command.Parameters.AddWithValue("@mentalDefense", cardCaptains.mental_defense);
                command.Parameters.AddWithValue("@speed", cardCaptains.speed);
                command.Parameters.AddWithValue("@criticalDamage", cardCaptains.critical_damage);
                command.Parameters.AddWithValue("@criticalRate", cardCaptains.critical_rate);
                command.Parameters.AddWithValue("@armorPenetration", cardCaptains.armor_penetration);
                command.Parameters.AddWithValue("@avoid", cardCaptains.avoid);
                command.Parameters.AddWithValue("@absorbsDamage", cardCaptains.absorbs_damage);
                command.Parameters.AddWithValue("@regenerateVitality", cardCaptains.regenerate_vitality);
                command.Parameters.AddWithValue("@accuracy", cardCaptains.accuracy);
                command.Parameters.AddWithValue("@mana", cardCaptains.mana);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateCardCaptainsBreakthrough(CardCaptains cardCaptains, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_captains
                SET star = @star, quantity=@quantity,
                    power = @power, health = @health, physical_attack = @physicalAttack,
                    physical_defense = @physicalDefense, magical_attack = @magicalAttack,
                    magical_defense = @magicalDefense, chemical_attack = @chemicalAttack,
                    chemical_defense = @chemicalDefense, atomic_attack = @atomicAttack,
                    atomic_defense = @atomicDefense, mental_attack = @mentalAttack,
                    mental_defense = @mentalDefense, speed = @speed, critical_damage = @criticalDamage,
                    critical_rate = @criticalRate, armor_penetration = @armorPenetration,
                    avoid = @avoid, absorbs_damage = @absorbsDamage, regenerate_vitality = @regenerateVitality, 
                    accuracy = @accuracy, mana = @mana
                WHERE 
                    user_id = @user_id AND card_captain_id = @card_captain_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_captain_id", cardCaptains.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", cardCaptains.power);
                command.Parameters.AddWithValue("@health", cardCaptains.health);
                command.Parameters.AddWithValue("@physicalAttack", cardCaptains.physical_attack);
                command.Parameters.AddWithValue("@physicalDefense", cardCaptains.physical_defense);
                command.Parameters.AddWithValue("@magicalAttack", cardCaptains.magical_attack);
                command.Parameters.AddWithValue("@magicalDefense", cardCaptains.magical_defense);
                command.Parameters.AddWithValue("@chemicalAttack", cardCaptains.chemical_attack);
                command.Parameters.AddWithValue("@chemicalDefense", cardCaptains.chemical_defense);
                command.Parameters.AddWithValue("@atomicAttack", cardCaptains.atomic_attack);
                command.Parameters.AddWithValue("@atomicDefense", cardCaptains.atomic_defense);
                command.Parameters.AddWithValue("@mentalAttack", cardCaptains.mental_attack);
                command.Parameters.AddWithValue("@mentalDefense", cardCaptains.mental_defense);
                command.Parameters.AddWithValue("@speed", cardCaptains.speed);
                command.Parameters.AddWithValue("@criticalDamage", cardCaptains.critical_damage);
                command.Parameters.AddWithValue("@criticalRate", cardCaptains.critical_rate);
                command.Parameters.AddWithValue("@armorPenetration", cardCaptains.armor_penetration);
                command.Parameters.AddWithValue("@avoid", cardCaptains.avoid);
                command.Parameters.AddWithValue("@absorbsDamage", cardCaptains.absorbs_damage);
                command.Parameters.AddWithValue("@regenerateVitality", cardCaptains.regenerate_vitality);
                command.Parameters.AddWithValue("@accuracy", cardCaptains.accuracy);
                command.Parameters.AddWithValue("@mana", cardCaptains.mana);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool InsertFactCardCaptains(CardCaptains cardCaptains)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO fact_card_captains (
                    user_id, user_card_captain_id, all_power,
                    all_health, all_physical_attack, all_physical_defense, all_magical_attack, all_magical_defense,
                    all_chemical_attack, all_chemical_defense, all_atomic_attack, all_atomic_defense,
                    all_mental_attack, all_mental_defense, all_speed, all_critical_damage, all_critical_rate,
                    all_armor_penetration, all_avoid, all_absorbs_damage, all_regenerate_vitality, all_accuracy, all_mana
                ) VALUES (
                    @user_id, @user_card_captain_id, @all_power,
                    @all_health, @all_physical_attack, @all_physical_defense, @all_magical_attack, @all_magical_defense,
                    @all_chemical_attack, @all_chemical_defense, @all_atomic_attack, @all_atomic_defense,
                    @all_mental_attack, @all_mental_defense, @all_speed, @all_critical_damage, @all_critical_rate,
                    @all_armor_penetration, @all_avoid, @all_absorbs_damage, @all_regenerate_vitality, @all_accuracy, @all_mana
                );";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.id);
                command.Parameters.AddWithValue("@all_power", cardCaptains.power);
                command.Parameters.AddWithValue("@all_health", cardCaptains.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardCaptains.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardCaptains.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardCaptains.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardCaptains.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardCaptains.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardCaptains.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardCaptains.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardCaptains.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardCaptains.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardCaptains.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardCaptains.speed);
                command.Parameters.AddWithValue("@all_critical_damage", cardCaptains.critical_damage);
                command.Parameters.AddWithValue("@all_critical_rate", cardCaptains.critical_rate);
                command.Parameters.AddWithValue("@all_armor_penetration", cardCaptains.armor_penetration);
                command.Parameters.AddWithValue("@all_avoid", cardCaptains.avoid);
                command.Parameters.AddWithValue("@all_absorbs_damage", cardCaptains.absorbs_damage);
                command.Parameters.AddWithValue("@all_regenerate_vitality", cardCaptains.regenerate_vitality);
                command.Parameters.AddWithValue("@all_accuracy", cardCaptains.accuracy);
                command.Parameters.AddWithValue("@all_mana", cardCaptains.mana);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateFactCardCaptains(CardCaptains cardCaptains)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE fact_card_captains
                SET 
                    all_power = @all_power, all_health = @all_health, all_physical_attack = @all_physical_attack,
                    all_physical_defense = @all_physical_defense, all_magical_attack = @all_magical_attack,
                    all_magical_defense = @all_magical_defense, all_chemical_attack = @all_chemical_attack,
                    all_chemical_defense = @all_chemical_defense, all_atomic_attack = @all_atomic_attack,
                    all_atomic_defense = @all_atomic_defense, all_mental_attack = @all_mental_attack,
                    all_mental_defense = @all_mental_defense, all_speed = @all_speed, all_critical_damage = @all_critical_damage,
                    all_critical_rate = @all_critical_rate, all_armor_penetration = @all_armor_penetration,
                    all_avoid = @all_avoid, all_absorbs_damage = @all_absorbs_damage, 
                    all_regenerate_vitality = @all_regenerate_vitality, 
                    all_accuracy = @all_accuracy, all_mana = @all_mana
                WHERE 
                    user_id = @user_id AND user_card_captain_id = @user_card_captain_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_captain_id", cardCaptains.id);
                command.Parameters.AddWithValue("@all_power", cardCaptains.power);
                command.Parameters.AddWithValue("@all_health", cardCaptains.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardCaptains.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardCaptains.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardCaptains.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardCaptains.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardCaptains.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardCaptains.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardCaptains.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardCaptains.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardCaptains.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardCaptains.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardCaptains.speed);
                command.Parameters.AddWithValue("@all_critical_damage", cardCaptains.critical_damage);
                command.Parameters.AddWithValue("@all_critical_rate", cardCaptains.critical_rate);
                command.Parameters.AddWithValue("@all_armor_penetration", cardCaptains.armor_penetration);
                command.Parameters.AddWithValue("@all_avoid", cardCaptains.avoid);
                command.Parameters.AddWithValue("@all_absorbs_damage", cardCaptains.absorbs_damage);
                command.Parameters.AddWithValue("@all_regenerate_vitality", cardCaptains.regenerate_vitality);
                command.Parameters.AddWithValue("@all_accuracy", cardCaptains.accuracy);
                command.Parameters.AddWithValue("@all_mana", cardCaptains.mana);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public CardCaptains GetCardCaptainsById(int Id)
    {
        CardCaptains captain = new CardCaptains();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_captains where card_captains.id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    captain = new CardCaptains
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
    public CardCaptains GetUserCardCaptainsById(int Id)
    {
        CardCaptains card = new CardCaptains();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_card_captains where user_card_captains.card_captain_id=@id 
                and user_card_captains.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CardCaptains
                    {
                        id = reader.GetInt32("card_captain_id"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
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
                        mana = reader.GetFloat("mana")
                    };
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return card;
    }
    public void InsertCardCaptainsGallery(int Id)
    {
        CardCaptains CaptainFromDB = GetCardCaptainsById(Id);
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
                FROM card_captains_gallery 
                WHERE user_id = @user_id AND card_captain_id = @card_captain_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_captain_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO card_captains_gallery (
                        user_id, card_captain_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
                        magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, speed, critical_damage, critical_rate, armor_penetration, avoid, 
                        absorbs_damage, regenerate_vitality, accuracy, mana, percent_all_health, percent_all_physical_attack, 
                        percent_all_physical_defense, percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, percent_all_mental_attack, 
                        percent_all_mental_defense
                    ) VALUES (
                        @user_id, @card_captain_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
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
                    command.Parameters.AddWithValue("@card_captain_id", Id);
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
    public void UpdateStatusCardCaptainsGallery(int Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update card_captain_gallery set status=@status where user_id=@user_id and card_captain_id=@card_captain_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_captain_id", Id);
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
    public List<CardCaptains> GetCardCaptainsWithPrice(string type, int pageSize, int offset)
    {
        List<CardCaptains> CardCaptainsList = new List<CardCaptains>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select c.*, ct.price, cu.image as currency_image, cu.id as currency_id
                from card_captains c, card_captain_trade ct, currency cu
                where c.id=ct.card_captain_id and ct.currency_id = cu.id and c.type =@type
                ORDER BY c.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name limit @limit offset @offset;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardCaptains captain = new CardCaptains
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
                    captain.currency = new Currency
                    {
                        id = reader.GetInt32("currency_id"),
                        image = reader.GetString("currency_image"),
                        quantity = reader.GetInt32("price")
                    };

                    CardCaptainsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardCaptainsList;
    }
    public int GetCardCaptainsWithPriceCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*)
                from card_captains c, card_captain_trade ct, currency cu
                where c.id=ct.card_captain_id and ct.currency_id = cu.id and c.type =@type;";
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
    public CardCaptains SumPowerCardCaptainsGallery()
    {
        CardCaptains sumCardCaptains = new CardCaptains();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                SUM(power) AS total_power, SUM(health) AS total_health, SUM(physical_attack) AS total_physical_attack,
                SUM(physical_defense) AS total_physical_defense, SUM(magical_attack) AS total_magical_attack, SUM(magical_defense) AS total_magical_defense,
                SUM(chemical_attack) AS total_chemical_attack, SUM(chemical_defense) AS total_chemical_defense, SUM(atomic_attack) AS total_atomic_attack,
                SUM(atomic_defense) AS total_atomic_defense, SUM(mental_attack) AS total_mental_attack, SUM(mental_defense) AS total_mental_defense,
                SUM(speed) AS total_speed, SUM(critical_damage) AS total_critical_damage, SUM(critical_rate) AS total_critical_rate,
                SUM(armor_penetration) AS total_armor_penetration, SUM(avoid) AS total_avoid, SUM(absorbs_damage) AS total_absorbs_damage,
                SUM(regenerate_vitality) AS total_regenerate_vitality, SUM(accuracy) AS total_accuracy, SUM(mana) AS total_mana,    
                SUM(percent_all_health) AS total_percent_all_health, SUM(percent_all_physical_attack) AS total_percent_all_physical_attack,
                SUM(percent_all_physical_defense) AS total_percent_all_physical_defense, SUM(percent_all_magical_attack) AS total_percent_all_magical_attack,
                SUM(percent_all_magical_defense) AS total_percent_all_magical_defense, SUM(percent_all_chemical_attack) AS total_percent_all_chemical_attack,
                SUM(percent_all_chemical_defense) AS total_percent_all_chemical_defense, SUM(percent_all_atomic_attack) AS total_percent_all_atomic_attack,
                SUM(percent_all_atomic_defense) AS total_percent_all_atomic_defense, SUM(percent_all_mental_attack) AS total_percent_all_mental_attack,
                SUM(percent_all_mental_defense) AS total_percent_all_mental_defense
                FROM card_captains_gallery where user_id=@user_id and status = 'available';";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumCardCaptains.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumCardCaptains.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumCardCaptains.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumCardCaptains.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumCardCaptains.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumCardCaptains.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumCardCaptains.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumCardCaptains.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumCardCaptains.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumCardCaptains.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumCardCaptains.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumCardCaptains.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumCardCaptains.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumCardCaptains.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                        sumCardCaptains.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumCardCaptains.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                        sumCardCaptains.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                        sumCardCaptains.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                        sumCardCaptains.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                        sumCardCaptains.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                        sumCardCaptains.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetInt32("total_mana");
                        sumCardCaptains.percent_all_health = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                        sumCardCaptains.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                        sumCardCaptains.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                        sumCardCaptains.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                        sumCardCaptains.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                        sumCardCaptains.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                        sumCardCaptains.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                        sumCardCaptains.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                        sumCardCaptains.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                        sumCardCaptains.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                        sumCardCaptains.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumCardCaptains;
    }
}
