using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class CardAdmirals
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
    public CardAdmirals()
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
    public List<CardAdmirals> GetFinalPower(List<CardAdmirals> CardAdmiralsList)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        foreach (var c in CardAdmiralsList)
        {
            c.all_power = powerManager.GetFinalCardAdmiralsPower(c);
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
        return CardAdmiralsList;
    }
    public CardAdmirals GetNewLevelPower(CardAdmirals c, double coefficient)
    {
        CardAdmirals orginCard = new CardAdmirals();
        orginCard = orginCard.GetCardAdmiralsById(c.id);
        CardAdmirals cardAdmirals = new CardAdmirals
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
        cardAdmirals.power = 0.5 * (
            cardAdmirals.health +
            cardAdmirals.physical_attack +
            cardAdmirals.physical_defense +
            cardAdmirals.magical_attack +
            cardAdmirals.magical_defense +
            cardAdmirals.chemical_attack +
            cardAdmirals.chemical_defense +
            cardAdmirals.atomic_attack +
            cardAdmirals.atomic_defense +
            cardAdmirals.mental_attack +
            cardAdmirals.mental_defense +
            cardAdmirals.speed +
            cardAdmirals.critical_damage +
            cardAdmirals.critical_rate +
            cardAdmirals.armor_penetration +
            cardAdmirals.avoid +
            cardAdmirals.absorbs_damage +
            cardAdmirals.regenerate_vitality +
            cardAdmirals.accuracy +
            cardAdmirals.mana
        );
        return cardAdmirals;
    }
    public CardAdmirals GetNewBreakthroughPower(CardAdmirals c, double coefficient)
    {
        CardAdmirals orginCard = new CardAdmirals();
        orginCard = orginCard.GetCardAdmiralsById(c.id);
        CardAdmirals cardAdmirals = new CardAdmirals
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
        cardAdmirals.power = 0.5 * (
            cardAdmirals.health +
            cardAdmirals.physical_attack +
            cardAdmirals.physical_defense +
            cardAdmirals.magical_attack +
            cardAdmirals.magical_defense +
            cardAdmirals.chemical_attack +
            cardAdmirals.chemical_defense +
            cardAdmirals.atomic_attack +
            cardAdmirals.atomic_defense +
            cardAdmirals.mental_attack +
            cardAdmirals.mental_defense +
            cardAdmirals.speed +
            cardAdmirals.critical_damage +
            cardAdmirals.critical_rate +
            cardAdmirals.armor_penetration +
            cardAdmirals.avoid +
            cardAdmirals.absorbs_damage +
            cardAdmirals.regenerate_vitality +
            cardAdmirals.accuracy +
            cardAdmirals.mana
        );
        return cardAdmirals;
    }
    public static List<string> GetUniqueCardAdmiralsTypes()
    {
        List<string> typeList = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = "Select distinct type from card_admirals";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                typeList.Add(reader.GetString(0));
            }
        }
        return typeList;
    }
    public List<CardAdmirals> GetCardAdmirals(string type,int pageSize, int offset)
    {
        List<CardAdmirals> CardAdmiralsList = new List<CardAdmirals>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from card_admirals where type= @type 
                ORDER BY card_admirals.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(card_admirals.name, '[0-9]+$') AS UNSIGNED), card_admirals.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardAdmirals captain = new CardAdmirals
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

                    CardAdmiralsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardAdmiralsList;
    }
    public int GetCardAdmiralsCount(string type){
        int count =0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_admirals where type= @type";
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
    public List<CardAdmirals> GetCardAdmiralsCollection(string type,int pageSize, int offset)
    {
        List<CardAdmirals> CardAdmiralsList = new List<CardAdmirals>();
        int user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT c.*, CASE WHEN cg.card_admiral_id IS NULL THEN 'block' WHEN cg.status = 'pending' THEN 'pending' WHEN cg.status = 'available' THEN 'available' END AS status
                FROM card_admirals c LEFT JOIN card_admirals_gallery cg ON c.id = cg.card_admiral_id and cg.user_id = @userId where c.type=@type 
                ORDER BY c.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardAdmirals captain = new CardAdmirals
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

                    CardAdmiralsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardAdmiralsList;
    }
    public List<CardAdmirals> GetUserCardAdmirals(string type,int pageSize, int offset)
    {
        List<CardAdmirals> CardAdmiralsList = new List<CardAdmirals>();
        int user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.*, fca.*
                FROM user_card_admirals uc
                LEFT JOIN card_admirals c ON c.id = uc.card_admiral_id 
                LEFT JOIN fact_card_admirals fca ON fca.user_id = uc.user_id AND fca.user_card_admiral_id = uc.card_admiral_id
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
                    CardAdmirals captain = new CardAdmirals
                    {
                        id = reader.GetInt32("card_admiral_id"),
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

                    CardAdmiralsList.Add(captain);
                }
                CardAdmiralsList = GetFinalPower(CardAdmiralsList);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardAdmiralsList;
    }
    public List<CardAdmirals> GetUserCardAdmiralsTeam(int teamId)
    {
        List<CardAdmirals> CardAdmiralsList = new List<CardAdmirals>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT uc.*, c.*, fca.*
                FROM user_card_admirals uc
                LEFT JOIN card_admirals c ON c.id = uc.card_admiral_id 
                LEFT JOIN fact_card_admirals fca ON fca.user_id = uc.user_id AND fca.user_card_admiral_id = uc.card_admiral_id
                WHERE uc.user_id = @userId AND fca.team_id=@team_id
                ORDER BY c.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name;
                ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                command.Parameters.AddWithValue("@team_id", teamId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardAdmirals captain = new CardAdmirals
                    {
                        id = reader.GetInt32("card_admiral_id"),
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

                    CardAdmiralsList.Add(captain);
                }
                CardAdmiralsList = GetFinalPower(CardAdmiralsList);
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardAdmiralsList;
    }
    public Dictionary<string, int> GetUniqueCardAdmiralTypesTeam(int teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_card_admirals uc
            LEFT JOIN card_admirals c ON uc.card_admiral_id = c.id 
            LEFT JOIN fact_card_admirals fch ON fch.user_id = uc.user_id AND fch.user_card_admiral_id = uc.card_admiral_id
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
    public bool UpdateTeamFactCardAdmirals(int? team_id,string position, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update fact_card_admirals set team_id=@team_id, position=@position where user_id=@user_id 
                and user_card_admiral_id=@user_card_admiral_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_admiral_id", card_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public int GetUserCardAdmiralsCount(string type){
        int count =0;
        int user_id=User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from card_admirals c, user_card_admirals uc where c.id=uc.card_admiral_id and uc.user_id=@userId and c.type= @type";
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
    public List<CardAdmirals> GetCardAdmiralsRandom(string type,int pageSize)
    {
        List<CardAdmirals> CardAdmiralsList = new List<CardAdmirals>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_admirals where type= @type ORDER BY RAND() limit @limit";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardAdmirals captain = new CardAdmirals
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

                    CardAdmiralsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardAdmiralsList;
    }
    public List<CardAdmirals> GetAllCardAdmirals(string type)
    {
        List<CardAdmirals> CardAdmiralsList = new List<CardAdmirals>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_admirals where type= @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardAdmirals captain = new CardAdmirals
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

                    CardAdmiralsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardAdmiralsList;
    }
    public bool InsertUserCardAdmirals(CardAdmirals CardAdmirals)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_admirals
                WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_admiral_id", CardAdmirals.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_card_admirals (
                    user_id, card_admiral_id, level, experiment, star, block, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage, critical_rate, 
                    armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana
                ) VALUES (
                    @user_id, @card_admiral_id, @level, @experiment, @star, @block, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, @critical_rate, 
                    @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@card_admiral_id", CardAdmirals.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", 0);
                    command.Parameters.AddWithValue("@power", CardAdmirals.power);
                    command.Parameters.AddWithValue("@health", CardAdmirals.health);
                    command.Parameters.AddWithValue("@physical_attack", CardAdmirals.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", CardAdmirals.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", CardAdmirals.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", CardAdmirals.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", CardAdmirals.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", CardAdmirals.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", CardAdmirals.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", CardAdmirals.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", CardAdmirals.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", CardAdmirals.mental_defense);
                    command.Parameters.AddWithValue("@speed", CardAdmirals.speed);
                    command.Parameters.AddWithValue("@critical_damage", CardAdmirals.critical_damage);
                    command.Parameters.AddWithValue("@critical_rate", CardAdmirals.critical_rate);
                    command.Parameters.AddWithValue("@armor_penetration", CardAdmirals.armor_penetration);
                    command.Parameters.AddWithValue("@avoid", CardAdmirals.avoid);
                    command.Parameters.AddWithValue("@absorbs_damage", CardAdmirals.absorbs_damage);
                    command.Parameters.AddWithValue("@regenerate_vitality", CardAdmirals.regenerate_vitality);
                    command.Parameters.AddWithValue("@accuracy", CardAdmirals.accuracy);
                    command.Parameters.AddWithValue("@mana", CardAdmirals.mana);
                    MySqlDataReader reader = command.ExecuteReader();
                    InsertFactCardAdmirals(CardAdmirals);
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_card_admirals
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@card_admiral_id", CardAdmirals.id);

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
    public bool UpdateCardAdmiralsLevel(CardAdmirals cardAdmirals, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_admirals
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
                    user_id = @user_id AND card_admiral_id = @card_admiral_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_admiral_id", cardAdmirals.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", cardAdmirals.power);
                command.Parameters.AddWithValue("@health", cardAdmirals.health);
                command.Parameters.AddWithValue("@physicalAttack", cardAdmirals.physical_attack);
                command.Parameters.AddWithValue("@physicalDefense", cardAdmirals.physical_defense);
                command.Parameters.AddWithValue("@magicalAttack", cardAdmirals.magical_attack);
                command.Parameters.AddWithValue("@magicalDefense", cardAdmirals.magical_defense);
                command.Parameters.AddWithValue("@chemicalAttack", cardAdmirals.chemical_attack);
                command.Parameters.AddWithValue("@chemicalDefense", cardAdmirals.chemical_defense);
                command.Parameters.AddWithValue("@atomicAttack", cardAdmirals.atomic_attack);
                command.Parameters.AddWithValue("@atomicDefense", cardAdmirals.atomic_defense);
                command.Parameters.AddWithValue("@mentalAttack", cardAdmirals.mental_attack);
                command.Parameters.AddWithValue("@mentalDefense", cardAdmirals.mental_defense);
                command.Parameters.AddWithValue("@speed", cardAdmirals.speed);
                command.Parameters.AddWithValue("@criticalDamage", cardAdmirals.critical_damage);
                command.Parameters.AddWithValue("@criticalRate", cardAdmirals.critical_rate);
                command.Parameters.AddWithValue("@armorPenetration", cardAdmirals.armor_penetration);
                command.Parameters.AddWithValue("@avoid", cardAdmirals.avoid);
                command.Parameters.AddWithValue("@absorbsDamage", cardAdmirals.absorbs_damage);
                command.Parameters.AddWithValue("@regenerateVitality", cardAdmirals.regenerate_vitality);
                command.Parameters.AddWithValue("@accuracy", cardAdmirals.accuracy);
                command.Parameters.AddWithValue("@mana", cardAdmirals.mana);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateCardAdmiralsBreakthrough(CardAdmirals cardAdmirals, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_admirals
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
                    user_id = @user_id AND card_admiral_id = @card_admiral_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_admiral_id", cardAdmirals.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", cardAdmirals.power);
                command.Parameters.AddWithValue("@health", cardAdmirals.health);
                command.Parameters.AddWithValue("@physicalAttack", cardAdmirals.physical_attack);
                command.Parameters.AddWithValue("@physicalDefense", cardAdmirals.physical_defense);
                command.Parameters.AddWithValue("@magicalAttack", cardAdmirals.magical_attack);
                command.Parameters.AddWithValue("@magicalDefense", cardAdmirals.magical_defense);
                command.Parameters.AddWithValue("@chemicalAttack", cardAdmirals.chemical_attack);
                command.Parameters.AddWithValue("@chemicalDefense", cardAdmirals.chemical_defense);
                command.Parameters.AddWithValue("@atomicAttack", cardAdmirals.atomic_attack);
                command.Parameters.AddWithValue("@atomicDefense", cardAdmirals.atomic_defense);
                command.Parameters.AddWithValue("@mentalAttack", cardAdmirals.mental_attack);
                command.Parameters.AddWithValue("@mentalDefense", cardAdmirals.mental_defense);
                command.Parameters.AddWithValue("@speed", cardAdmirals.speed);
                command.Parameters.AddWithValue("@criticalDamage", cardAdmirals.critical_damage);
                command.Parameters.AddWithValue("@criticalRate", cardAdmirals.critical_rate);
                command.Parameters.AddWithValue("@armorPenetration", cardAdmirals.armor_penetration);
                command.Parameters.AddWithValue("@avoid", cardAdmirals.avoid);
                command.Parameters.AddWithValue("@absorbsDamage", cardAdmirals.absorbs_damage);
                command.Parameters.AddWithValue("@regenerateVitality", cardAdmirals.regenerate_vitality);
                command.Parameters.AddWithValue("@accuracy", cardAdmirals.accuracy);
                command.Parameters.AddWithValue("@mana", cardAdmirals.mana);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool InsertFactCardAdmirals(CardAdmirals cardAdmirals)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                INSERT INTO fact_card_admirals (
                    user_id, user_card_admiral_id, all_power,
                    all_health, all_physical_attack, all_physical_defense, all_magical_attack, all_magical_defense,
                    all_chemical_attack, all_chemical_defense, all_atomic_attack, all_atomic_defense,
                    all_mental_attack, all_mental_defense, all_speed, all_critical_damage, all_critical_rate,
                    all_armor_penetration, all_avoid, all_absorbs_damage, all_regenerate_vitality, all_accuracy, all_mana
                ) VALUES (
                    @user_id, @user_card_admiral_id, @all_power,
                    @all_health, @all_physical_attack, @all_physical_defense, @all_magical_attack, @all_magical_defense,
                    @all_chemical_attack, @all_chemical_defense, @all_atomic_attack, @all_atomic_defense,
                    @all_mental_attack, @all_mental_defense, @all_speed, @all_critical_damage, @all_critical_rate,
                    @all_armor_penetration, @all_avoid, @all_absorbs_damage, @all_regenerate_vitality, @all_accuracy, @all_mana
                );";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.id);
                command.Parameters.AddWithValue("@all_power", cardAdmirals.power);
                command.Parameters.AddWithValue("@all_health", cardAdmirals.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardAdmirals.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardAdmirals.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardAdmirals.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardAdmirals.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardAdmirals.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardAdmirals.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardAdmirals.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardAdmirals.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardAdmirals.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardAdmirals.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardAdmirals.speed);
                command.Parameters.AddWithValue("@all_critical_damage", cardAdmirals.critical_damage);
                command.Parameters.AddWithValue("@all_critical_rate", cardAdmirals.critical_rate);
                command.Parameters.AddWithValue("@all_armor_penetration", cardAdmirals.armor_penetration);
                command.Parameters.AddWithValue("@all_avoid", cardAdmirals.avoid);
                command.Parameters.AddWithValue("@all_absorbs_damage", cardAdmirals.absorbs_damage);
                command.Parameters.AddWithValue("@all_regenerate_vitality", cardAdmirals.regenerate_vitality);
                command.Parameters.AddWithValue("@all_accuracy", cardAdmirals.accuracy);
                command.Parameters.AddWithValue("@all_mana", cardAdmirals.mana);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateFactCardAdmirals(CardAdmirals cardAdmirals)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE fact_card_admirals
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
                    user_id = @user_id AND user_card_admiral_id = @user_card_admiral_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_admiral_id", cardAdmirals.id);
                command.Parameters.AddWithValue("@all_power", cardAdmirals.power);
                command.Parameters.AddWithValue("@all_health", cardAdmirals.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardAdmirals.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardAdmirals.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardAdmirals.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardAdmirals.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardAdmirals.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardAdmirals.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardAdmirals.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardAdmirals.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardAdmirals.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardAdmirals.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardAdmirals.speed);
                command.Parameters.AddWithValue("@all_critical_damage", cardAdmirals.critical_damage);
                command.Parameters.AddWithValue("@all_critical_rate", cardAdmirals.critical_rate);
                command.Parameters.AddWithValue("@all_armor_penetration", cardAdmirals.armor_penetration);
                command.Parameters.AddWithValue("@all_avoid", cardAdmirals.avoid);
                command.Parameters.AddWithValue("@all_absorbs_damage", cardAdmirals.absorbs_damage);
                command.Parameters.AddWithValue("@all_regenerate_vitality", cardAdmirals.regenerate_vitality);
                command.Parameters.AddWithValue("@all_accuracy", cardAdmirals.accuracy);
                command.Parameters.AddWithValue("@all_mana", cardAdmirals.mana);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public CardAdmirals GetCardAdmiralsById(int Id)
    {
        CardAdmirals captain = new CardAdmirals();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select * from card_admirals where card_admirals.id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    captain = new CardAdmirals
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
    public CardAdmirals GetUserCardAdmiralsById(int Id)
    {
        CardAdmirals card = new CardAdmirals();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_card_admirals where user_card_admirals.card_admiral_id=@id 
                and user_card_admirals.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CardAdmirals
                    {
                        id = reader.GetInt32("card_admiral_id"),
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
    public void InsertCardAdmiralsGallery(int Id)
    {
        CardAdmirals CaptainFromDB = GetCardAdmiralsById(Id);
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
                FROM card_admirals_gallery 
                WHERE user_id = @user_id AND card_admiral_id = @card_admiral_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@card_admiral_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO card_admirals_gallery (
                        user_id, card_admiral_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
                        magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, speed, critical_damage, critical_rate, armor_penetration, avoid, 
                        absorbs_damage, regenerate_vitality, accuracy, mana, percent_all_health, percent_all_physical_attack, 
                        percent_all_physical_defense, percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, percent_all_mental_attack, 
                        percent_all_mental_defense
                    ) VALUES (
                        @user_id, @card_admiral_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
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
                    command.Parameters.AddWithValue("@card_admiral_id", Id);
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
    public void UpdateStatusCardAdmiralsGallery(int Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update card_admirals_gallery set status=@status where user_id=@user_id and card_admirals_id=@card_admirals_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_admirals_id", Id);
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
    public List<CardAdmirals> GetCardAdmiralsWithPrice(string type,int pageSize, int offset)
    {
        List<CardAdmirals> CardAdmiralsList = new List<CardAdmirals>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select c.*, ct.price, cu.image as currency_image, cu.id as currency_id
                from card_admirals c, card_admiral_trade ct, currency cu
                where c.id=ct.card_admiral_id and ct.currency_id = cu.id and c.type =@type
                ORDER BY c.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(c.name, '[0-9]+$') AS UNSIGNED), c.name limit @limit offset @offset;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CardAdmirals captain = new CardAdmirals
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

                    CardAdmiralsList.Add(captain);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return CardAdmiralsList;
    }
    public int GetCardAdmiralsWithPriceCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select count(*)
                from card_admirals c, card_admiral_trade ct, currency cu
                where c.id=ct.card_admiral_id and ct.currency_id = cu.id and c.type =@type;";
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
    public CardAdmirals SumPowerCardCaptainsGallery()
    {
        CardAdmirals sumCardAdmirals = new CardAdmirals();
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
                FROM card_admirals_gallery where user_id=@user_id and status = 'available';";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumCardAdmirals.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumCardAdmirals.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumCardAdmirals.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumCardAdmirals.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumCardAdmirals.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumCardAdmirals.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumCardAdmirals.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumCardAdmirals.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumCardAdmirals.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumCardAdmirals.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumCardAdmirals.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumCardAdmirals.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumCardAdmirals.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumCardAdmirals.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                        sumCardAdmirals.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumCardAdmirals.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                        sumCardAdmirals.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                        sumCardAdmirals.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                        sumCardAdmirals.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                        sumCardAdmirals.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                        sumCardAdmirals.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetInt32("total_mana");
                        sumCardAdmirals.percent_all_health = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                        sumCardAdmirals.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                        sumCardAdmirals.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                        sumCardAdmirals.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                        sumCardAdmirals.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                        sumCardAdmirals.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                        sumCardAdmirals.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                        sumCardAdmirals.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                        sumCardAdmirals.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                        sumCardAdmirals.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                        sumCardAdmirals.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumCardAdmirals;
    }
}
