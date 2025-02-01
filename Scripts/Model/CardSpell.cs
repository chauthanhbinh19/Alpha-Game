using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
using UnityEditor.Scripting;

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
    public float mana {get; set;}
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
    public double percent_all_accuracy { get; set; }
    public float percent_all_mana { get; set; }
    public double all_power { get; set; }
    public double all_percent_health { get; set; }
    public double all_percent_physical_attack { get; set; }
    public double all_percent_physical_defense { get; set; }
    public double all_percent_magical_attack { get; set; }
    public double all_percent_magical_defense { get; set; }
    public double all_percent_chemical_attack { get; set; }
    public double all_percent_chemical_defense { get; set; }
    public double all_percent_atomic_attack { get; set; }
    public double all_percent_atomic_defense { get; set; }
    public double all_percent_mental_attack { get; set; }
    public double all_percent_mental_defense { get; set; }
    public double all_percent_speed { get; set; }
    public double all_percent_critical_damage { get; set; }
    public double all_percent_critical_rate { get; set; }
    public double all_percent_armor_penetration { get; set; }
    public double all_percent_avoid { get; set; }
    public double all_percent_absorbs_damage { get; set; }
    public double all_percent_regenerate_vitality { get; set; }
    public double all_percent_accuracy { get; set; }
    public float all_percent_mana { get; set; }
    public string description { get; set; }
    public string status { get; set; }
    public int team_id { get; set; }
    public Currency currency { get; set; }
    public CardSpell()
    {
        id = -1;
        star = -1;
        level = -1;
        experiment = -1;
        quantity = -1;
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
        mana = -1;
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
        percent_all_speed = -1;
        percent_all_critical_damage = -1;
        percent_all_critical_rate = -1;
        percent_all_armor_penetration = -1;
        percent_all_avoid = -1;
        percent_all_absorbs_damage = -1;
        percent_all_regenerate_vitality = -1;
        percent_all_accuracy = -1;
        percent_all_mana = -1;
        all_power = -1;
        all_percent_health = -1;
        all_percent_physical_attack = -1;
        all_percent_physical_defense = -1;
        all_percent_magical_attack = -1;
        all_percent_magical_defense = -1;
        all_percent_chemical_attack = -1;
        all_percent_chemical_defense = -1;
        all_percent_atomic_attack = -1;
        all_percent_atomic_defense = -1;
        all_percent_mental_attack = -1;
        all_percent_mental_defense = -1;
        all_percent_speed = -1;
        all_percent_critical_damage = -1;
        all_percent_critical_rate = -1;
        all_percent_armor_penetration = -1;
        all_percent_avoid = -1;
        all_percent_absorbs_damage = -1;
        all_percent_regenerate_vitality = -1;
        all_percent_accuracy = -1;
        all_percent_mana = -1;
        team_id = -1;
    }
    public CardSpell GetNewLevelPower(CardSpell c, double coefficient)
    {
        coefficient = coefficient * 0.001;
        CardSpell orginCard = new CardSpell();
        orginCard = orginCard.GetCardSpellById(c.id);
        CardSpell cardHeroes = new CardSpell
        {
            id = c.id,
            percent_all_health = c.percent_all_health + orginCard.percent_all_health * coefficient,
            percent_all_physical_attack = c.percent_all_physical_attack + orginCard.percent_all_physical_attack * coefficient,
            percent_all_physical_defense = c.percent_all_physical_defense + orginCard.percent_all_physical_defense * coefficient,
            percent_all_magical_attack = c.percent_all_magical_attack + orginCard.percent_all_magical_attack * coefficient,
            percent_all_magical_defense = c.percent_all_magical_defense + orginCard.percent_all_magical_defense * coefficient,
            percent_all_chemical_attack = c.percent_all_chemical_attack + orginCard.percent_all_chemical_attack * coefficient,
            percent_all_chemical_defense = c.percent_all_chemical_defense + orginCard.percent_all_chemical_defense * coefficient,
            percent_all_atomic_attack = c.percent_all_atomic_attack + orginCard.percent_all_atomic_attack * coefficient,
            percent_all_atomic_defense = c.percent_all_atomic_defense + orginCard.percent_all_atomic_defense * coefficient,
            percent_all_mental_attack = c.percent_all_mental_attack + orginCard.percent_all_mental_attack * coefficient,
            percent_all_mental_defense = c.percent_all_mental_defense + orginCard.percent_all_mental_defense * coefficient,
            percent_all_speed = c.percent_all_speed + orginCard.percent_all_speed * coefficient,
            percent_all_critical_damage = c.percent_all_critical_damage + orginCard.percent_all_critical_damage * coefficient,
            percent_all_critical_rate = c.percent_all_critical_rate + orginCard.percent_all_critical_rate * coefficient,
            percent_all_armor_penetration = c.percent_all_armor_penetration + orginCard.percent_all_armor_penetration * coefficient,
            percent_all_avoid = c.percent_all_avoid + orginCard.percent_all_avoid * coefficient,
            percent_all_absorbs_damage = c.percent_all_absorbs_damage + orginCard.percent_all_absorbs_damage * coefficient,
            percent_all_regenerate_vitality = c.percent_all_regenerate_vitality + orginCard.percent_all_regenerate_vitality * coefficient,
            percent_all_accuracy = c.percent_all_accuracy + orginCard.percent_all_accuracy * coefficient,
            percent_all_mana = c.mana + orginCard.mana * (float)coefficient
        };
        cardHeroes.power = 0.5 * (
            cardHeroes.percent_all_health +
            cardHeroes.percent_all_physical_attack +
            cardHeroes.percent_all_physical_defense +
            cardHeroes.percent_all_magical_attack +
            cardHeroes.percent_all_magical_defense +
            cardHeroes.percent_all_chemical_attack +
            cardHeroes.percent_all_chemical_defense +
            cardHeroes.percent_all_atomic_attack +
            cardHeroes.percent_all_atomic_defense +
            cardHeroes.percent_all_mental_attack +
            cardHeroes.percent_all_mental_defense +
            cardHeroes.percent_all_speed +
            cardHeroes.percent_all_critical_damage +
            cardHeroes.percent_all_critical_rate +
            cardHeroes.percent_all_armor_penetration +
            cardHeroes.percent_all_avoid +
            cardHeroes.percent_all_absorbs_damage +
            cardHeroes.percent_all_regenerate_vitality +
            cardHeroes.percent_all_accuracy +
            cardHeroes.percent_all_mana
        );
        return cardHeroes;
    }
    public CardSpell GetNewBreakthroughPower(CardSpell c, double coefficient)
    {
        coefficient = coefficient * 0.001;
        CardSpell orginCard = new CardSpell();
        orginCard = orginCard.GetCardSpellById(c.id);
        CardSpell cardHeroes = new CardSpell
        {
            id = c.id,
            percent_all_health = c.percent_all_health + orginCard.percent_all_health * coefficient,
            percent_all_physical_attack = c.percent_all_physical_attack + orginCard.percent_all_physical_attack * coefficient,
            percent_all_physical_defense = c.percent_all_physical_defense + orginCard.percent_all_physical_defense * coefficient,
            percent_all_magical_attack = c.percent_all_magical_attack + orginCard.percent_all_magical_attack * coefficient,
            percent_all_magical_defense = c.percent_all_magical_defense + orginCard.percent_all_magical_defense * coefficient,
            percent_all_chemical_attack = c.percent_all_chemical_attack + orginCard.percent_all_chemical_attack * coefficient,
            percent_all_chemical_defense = c.percent_all_chemical_defense + orginCard.percent_all_chemical_defense * coefficient,
            percent_all_atomic_attack = c.percent_all_atomic_attack + orginCard.percent_all_atomic_attack * coefficient,
            percent_all_atomic_defense = c.percent_all_atomic_defense + orginCard.percent_all_atomic_defense * coefficient,
            percent_all_mental_attack = c.percent_all_mental_attack + orginCard.percent_all_mental_attack * coefficient,
            percent_all_mental_defense = c.percent_all_mental_defense + orginCard.percent_all_mental_defense * coefficient,
            percent_all_speed = c.percent_all_speed + orginCard.percent_all_speed * coefficient,
            percent_all_critical_damage = c.percent_all_critical_damage + orginCard.percent_all_critical_damage * coefficient,
            percent_all_critical_rate = c.percent_all_critical_rate + orginCard.percent_all_critical_rate * coefficient,
            percent_all_armor_penetration = c.percent_all_armor_penetration + orginCard.percent_all_armor_penetration * coefficient,
            percent_all_avoid = c.percent_all_avoid + orginCard.percent_all_avoid * coefficient,
            percent_all_absorbs_damage = c.percent_all_absorbs_damage + orginCard.percent_all_absorbs_damage * coefficient,
            percent_all_regenerate_vitality = c.percent_all_regenerate_vitality + orginCard.percent_all_regenerate_vitality * coefficient,
            percent_all_accuracy = c.percent_all_accuracy + orginCard.percent_all_accuracy * coefficient,
            percent_all_mana = c.mana + orginCard.mana * (float)coefficient
        };
        cardHeroes.power = 0.5 * (
            cardHeroes.percent_all_health +
            cardHeroes.percent_all_physical_attack +
            cardHeroes.percent_all_physical_defense +
            cardHeroes.percent_all_magical_attack +
            cardHeroes.percent_all_magical_defense +
            cardHeroes.percent_all_chemical_attack +
            cardHeroes.percent_all_chemical_defense +
            cardHeroes.percent_all_atomic_attack +
            cardHeroes.percent_all_atomic_defense +
            cardHeroes.percent_all_mental_attack +
            cardHeroes.percent_all_mental_defense +
            cardHeroes.percent_all_speed +
            cardHeroes.percent_all_critical_damage +
            cardHeroes.percent_all_critical_rate +
            cardHeroes.percent_all_armor_penetration +
            cardHeroes.percent_all_avoid +
            cardHeroes.percent_all_absorbs_damage +
            cardHeroes.percent_all_regenerate_vitality +
            cardHeroes.percent_all_accuracy +
            cardHeroes.percent_all_mana
        );
        return cardHeroes;
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
                string query = @"Select us.*, s.* , fcs.*
                FROM user_card_spell us
                LEFT JOIN card_spell s ON us.card_spell_id = s.id 
                LEFT JOIN fact_card_spell fcs ON fcs.user_id = us.user_id AND fcs.user_card_spell_id = us.card_spell_id
                WHERE us.user_id = @userId AND s.type = @type
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name 
                limit @limit offset @offset";
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
                        team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? -1 : reader.GetInt32("team_id"),
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),
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
                        all_power = reader.GetDouble("all_power"),
                        all_percent_health = reader.GetDouble("percent_all_health"),
                        all_percent_physical_attack = reader.GetDouble("all_percent_physical_attack"),
                        all_percent_physical_defense = reader.GetDouble("all_percent_physical_defense"),
                        all_percent_magical_attack = reader.GetDouble("all_percent_magical_attack"),
                        all_percent_magical_defense = reader.GetDouble("all_percent_magical_defense"),
                        all_percent_chemical_attack = reader.GetDouble("all_percent_chemical_attack"),
                        all_percent_chemical_defense = reader.GetDouble("all_percent_chemical_defense"),
                        all_percent_atomic_attack = reader.GetDouble("all_percent_atomic_attack"),
                        all_percent_atomic_defense = reader.GetDouble("all_percent_atomic_defense"),
                        all_percent_mental_attack = reader.GetDouble("all_percent_mental_attack"),
                        all_percent_mental_defense = reader.GetDouble("all_percent_mental_defense"),
                        all_percent_speed = reader.GetDouble("all_percent_speed"),
                        all_percent_critical_damage = reader.GetDouble("all_percent_critical_damage"),
                        all_percent_critical_rate = reader.GetDouble("all_percent_critical_rate"),
                        all_percent_armor_penetration = reader.GetDouble("all_percent_armor_penetration"),
                        all_percent_avoid = reader.GetDouble("all_percent_avoid"),
                        all_percent_absorbs_damage = reader.GetDouble("all_percent_absorbs_damage"),
                        all_percent_regenerate_vitality = reader.GetDouble("all_percent_regenerate_vitality"),
                        all_percent_accuracy = reader.GetDouble("all_percent_accuracy"),
                        all_percent_mana = reader.GetFloat("all_percent_mana"),
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
    public List<CardSpell> GetUserCardSpellTeam(int teamId)
    {
        List<CardSpell> CardSpellList = new List<CardSpell>();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select us.*, s.*, fcs.* 
                FROM user_card_spell us
                LEFT JOIN card_spell s ON us.card_spell_id = s.id 
                LEFT JOIN fact_card_spell fcs ON fcs.user_id = us.user_id AND fcs.user_card_spell_id = us.card_spell_id
                WHERE us.user_id = @userId AND fcs.team_id = @team_id
                ORDER BY s.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(s.name, '[0-9]+$') AS UNSIGNED), s.name ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@team_id", teamId);
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
                        team_id = reader.IsDBNull(reader.GetOrdinal("team_id")) ? -1 : reader.GetInt32("team_id"),
                        position = reader.IsDBNull(reader.GetOrdinal("position")) ? null : reader.GetString("position"),
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
                        all_power = reader.GetDouble("all_power"),
                        all_percent_health = reader.GetDouble("percent_all_health"),
                        all_percent_physical_attack = reader.GetDouble("all_percent_physical_attack"),
                        all_percent_physical_defense = reader.GetDouble("all_percent_physical_defense"),
                        all_percent_magical_attack = reader.GetDouble("all_percent_magical_attack"),
                        all_percent_magical_defense = reader.GetDouble("all_percent_magical_defense"),
                        all_percent_chemical_attack = reader.GetDouble("all_percent_chemical_attack"),
                        all_percent_chemical_defense = reader.GetDouble("all_percent_chemical_defense"),
                        all_percent_atomic_attack = reader.GetDouble("all_percent_atomic_attack"),
                        all_percent_atomic_defense = reader.GetDouble("all_percent_atomic_defense"),
                        all_percent_mental_attack = reader.GetDouble("all_percent_mental_attack"),
                        all_percent_mental_defense = reader.GetDouble("all_percent_mental_defense"),
                        all_percent_speed = reader.GetDouble("all_percent_speed"),
                        all_percent_critical_damage = reader.GetDouble("all_percent_critical_damage"),
                        all_percent_critical_rate = reader.GetDouble("all_percent_critical_rate"),
                        all_percent_armor_penetration = reader.GetDouble("all_percent_armor_penetration"),
                        all_percent_avoid = reader.GetDouble("all_percent_avoid"),
                        all_percent_absorbs_damage = reader.GetDouble("all_percent_absorbs_damage"),
                        all_percent_regenerate_vitality = reader.GetDouble("all_percent_regenerate_vitality"),
                        all_percent_accuracy = reader.GetDouble("all_percent_accuracy"),
                        all_percent_mana = reader.GetFloat("all_percent_mana"),
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
    public Dictionary<string, int> GetUniqueCardSpellTypesTeam(int teamId)
    {
        var result = new Dictionary<string, int>();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            connection.Open();

            string query = @"SELECT distinct c.type, count(c.type) as number
            FROM user_card_spell uc
            LEFT JOIN card_spell c ON uc.card_spell_id = c.id 
            LEFT JOIN fact_card_spell fch ON fch.user_id = uc.user_id AND fch.user_card_spell_id = uc.card_spell_id
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
    public bool UpdateTeamFactCardSpell(int? team_id,string position, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                Update fact_card_spell set team_id=@team_id, position=@position where user_id=@user_id 
                and user_card_spell_id=@user_card_spell_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@team_id", team_id);
                command.Parameters.AddWithValue("@position", position);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_spell_id", card_id);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
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
                    command.Parameters.AddWithValue("@quantity", 0);
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
                    InsertFactCardSpell(CardSpell);
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
    public bool UpdateCardSpellLevel(CardSpell cardSpell, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_spell
                SET level = @level,
                    power = @power, percent_all_health = @health, percent_all_physical_attack = @physicalAttack,
                    percent_all_physical_defense = @physicalDefense, percent_all_magical_attack = @magicalAttack,
                    percent_all_magical_defense = @magicalDefense, percent_all_chemical_attack = @chemicalAttack,
                    percent_all_chemical_defense = @chemicalDefense, percent_all_atomic_attack = @atomicAttack,
                    percent_all_atomic_defense = @atomicDefense, percent_all_mental_attack = @mentalAttack,
                    percent_all_mental_defense = @mentalDefense, percent_all_speed = @speed, percent_all_critical_damage = @criticalDamage,
                    percent_all_critical_rate = @criticalRate, percent_all_armor_penetration = @armorPenetration,
                    percent_all_avoid = @avoid, percent_all_absorbs_damage = @absorbsDamage, percent_all_regenerate_vitality = @regenerateVitality, 
                    percent_all_accuracy = @accuracy, percent_all_mana = @mana
                WHERE 
                    user_id = @user_id AND card_spell_id = @card_spell_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_spell_id", cardSpell.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", cardSpell.power);
                command.Parameters.AddWithValue("@health", cardSpell.health);
                command.Parameters.AddWithValue("@physicalAttack", cardSpell.physical_attack);
                command.Parameters.AddWithValue("@physicalDefense", cardSpell.physical_defense);
                command.Parameters.AddWithValue("@magicalAttack", cardSpell.magical_attack);
                command.Parameters.AddWithValue("@magicalDefense", cardSpell.magical_defense);
                command.Parameters.AddWithValue("@chemicalAttack", cardSpell.chemical_attack);
                command.Parameters.AddWithValue("@chemicalDefense", cardSpell.chemical_defense);
                command.Parameters.AddWithValue("@atomicAttack", cardSpell.atomic_attack);
                command.Parameters.AddWithValue("@atomicDefense", cardSpell.atomic_defense);
                command.Parameters.AddWithValue("@mentalAttack", cardSpell.mental_attack);
                command.Parameters.AddWithValue("@mentalDefense", cardSpell.mental_defense);
                command.Parameters.AddWithValue("@speed", cardSpell.speed);
                command.Parameters.AddWithValue("@criticalDamage", cardSpell.critical_damage);
                command.Parameters.AddWithValue("@criticalRate", cardSpell.critical_rate);
                command.Parameters.AddWithValue("@armorPenetration", cardSpell.armor_penetration);
                command.Parameters.AddWithValue("@avoid", cardSpell.avoid);
                command.Parameters.AddWithValue("@absorbsDamage", cardSpell.absorbs_damage);
                command.Parameters.AddWithValue("@regenerateVitality", cardSpell.regenerate_vitality);
                command.Parameters.AddWithValue("@accuracy", cardSpell.accuracy);
                command.Parameters.AddWithValue("@mana", cardSpell.mana);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateCardSpellBreakthrough(CardSpell cardSpell, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_card_spell
                SET star = @star, quantity=@quantity,
                    power = @power, percent_all_health = @health, percent_all_physical_attack = @physicalAttack,
                    percent_all_physical_defense = @physicalDefense, percent_all_magical_attack = @magicalAttack,
                    percent_all_magical_defense = @magicalDefense, percent_all_chemical_attack = @chemicalAttack,
                    percent_all_chemical_defense = @chemicalDefense, percent_all_atomic_attack = @atomicAttack,
                    percent_all_atomic_defense = @atomicDefense, percent_all_mental_attack = @mentalAttack,
                    percent_all_mental_defense = @mentalDefense, percent_all_speed = @speed, percent_all_critical_damage = @criticalDamage,
                    percent_all_critical_rate = @criticalRate, percent_all_armor_penetration = @armorPenetration,
                    percent_all_avoid = @avoid, percent_all_absorbs_damage = @absorbsDamage, percent_all_regenerate_vitality = @regenerateVitality, 
                    percent_all_accuracy = @accuracy, percent_all_mana = @mana
                WHERE 
                    user_id = @user_id AND card_spell_id = @card_spell_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@card_spell_id", cardSpell.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", cardSpell.power);
                command.Parameters.AddWithValue("@health", cardSpell.health);
                command.Parameters.AddWithValue("@physicalAttack", cardSpell.physical_attack);
                command.Parameters.AddWithValue("@physicalDefense", cardSpell.physical_defense);
                command.Parameters.AddWithValue("@magicalAttack", cardSpell.magical_attack);
                command.Parameters.AddWithValue("@magicalDefense", cardSpell.magical_defense);
                command.Parameters.AddWithValue("@chemicalAttack", cardSpell.chemical_attack);
                command.Parameters.AddWithValue("@chemicalDefense", cardSpell.chemical_defense);
                command.Parameters.AddWithValue("@atomicAttack", cardSpell.atomic_attack);
                command.Parameters.AddWithValue("@atomicDefense", cardSpell.atomic_defense);
                command.Parameters.AddWithValue("@mentalAttack", cardSpell.mental_attack);
                command.Parameters.AddWithValue("@mentalDefense", cardSpell.mental_defense);
                command.Parameters.AddWithValue("@speed", cardSpell.speed);
                command.Parameters.AddWithValue("@criticalDamage", cardSpell.critical_damage);
                command.Parameters.AddWithValue("@criticalRate", cardSpell.critical_rate);
                command.Parameters.AddWithValue("@armorPenetration", cardSpell.armor_penetration);
                command.Parameters.AddWithValue("@avoid", cardSpell.avoid);
                command.Parameters.AddWithValue("@absorbsDamage", cardSpell.absorbs_damage);
                command.Parameters.AddWithValue("@regenerateVitality", cardSpell.regenerate_vitality);
                command.Parameters.AddWithValue("@accuracy", cardSpell.accuracy);
                command.Parameters.AddWithValue("@mana", cardSpell.mana);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool InsertFactCardSpell(CardSpell CardSpell)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                    INSERT INTO fact_card_spell (
                        user_id, user_card_spell_id, all_power,
                        all_percent_health, all_percent_physical_attack, all_percent_physical_defense,
                        all_percent_magical_attack, all_percent_magical_defense, all_percent_chemical_attack,
                        all_percent_chemical_defense, all_percent_atomic_attack, all_percent_atomic_defense,
                        all_percent_mental_attack, all_percent_mental_defense, all_percent_speed,
                        all_percent_critical_damage, all_percent_critical_rate, all_percent_armor_penetration,
                        all_percent_avoid, all_percent_absorbs_damage, all_percent_regenerate_vitality,
                        all_percent_accuracy, all_percent_mana
                    ) VALUES (
                        @user_id, @user_card_spell_id, @power,
                        @all_percent_health, @all_percent_physical_attack, @all_percent_physical_defense,
                        @all_percent_magical_attack, @all_percent_magical_defense, @all_percent_chemical_attack,
                        @all_percent_chemical_defense, @all_percent_atomic_attack, @all_percent_atomic_defense,
                        @all_percent_mental_attack, @all_percent_mental_defense, @all_percent_speed,
                        @all_percent_critical_damage, @all_percent_critical_rate, @all_percent_armor_penetration,
                        @all_percent_avoid, @all_percent_absorbs_damage, @all_percent_regenerate_vitality,
                        @all_percent_accuracy, @all_percent_mana
                    );
                    ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_spell_id", CardSpell.id);
                command.Parameters.AddWithValue("@power", CardSpell.power);
                command.Parameters.AddWithValue("@all_percent_health", CardSpell.percent_all_health);
                command.Parameters.AddWithValue("@all_percent_physical_attack", CardSpell.percent_all_physical_attack);
                command.Parameters.AddWithValue("@all_percent_physical_defense", CardSpell.percent_all_physical_defense);
                command.Parameters.AddWithValue("@all_percent_magical_attack", CardSpell.percent_all_magical_attack);
                command.Parameters.AddWithValue("@all_percent_magical_defense", CardSpell.percent_all_magical_defense);
                command.Parameters.AddWithValue("@all_percent_chemical_attack", CardSpell.percent_all_chemical_attack);
                command.Parameters.AddWithValue("@all_percent_chemical_defense", CardSpell.percent_all_chemical_defense);
                command.Parameters.AddWithValue("@all_percent_atomic_attack", CardSpell.percent_all_atomic_attack);
                command.Parameters.AddWithValue("@all_percent_atomic_defense", CardSpell.percent_all_atomic_defense);
                command.Parameters.AddWithValue("@all_percent_mental_attack", CardSpell.percent_all_mental_attack);
                command.Parameters.AddWithValue("@all_percent_mental_defense", CardSpell.percent_all_mental_defense);
                command.Parameters.AddWithValue("@all_percent_speed", CardSpell.percent_all_speed);
                command.Parameters.AddWithValue("@all_percent_critical_damage", CardSpell.percent_all_critical_damage);
                command.Parameters.AddWithValue("@all_percent_critical_rate", CardSpell.percent_all_critical_rate);
                command.Parameters.AddWithValue("@all_percent_armor_penetration", CardSpell.percent_all_armor_penetration);
                command.Parameters.AddWithValue("@all_percent_avoid", CardSpell.percent_all_avoid);
                command.Parameters.AddWithValue("@all_percent_absorbs_damage", CardSpell.percent_all_absorbs_damage);
                command.Parameters.AddWithValue("@all_percent_regenerate_vitality", CardSpell.percent_all_regenerate_vitality);
                command.Parameters.AddWithValue("@all_percent_accuracy", CardSpell.percent_all_accuracy);
                command.Parameters.AddWithValue("@all_percent_mana", CardSpell.percent_all_mana);
                command.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return true;
    }
    public bool UpdateFactCardSpell(CardSpell cardSpell)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE fact_card_spell
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
                    user_id = @user_id AND user_card_spell_id = @user_card_spell_id;;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_spell_id", cardSpell.id);
                command.Parameters.AddWithValue("@all_power", cardSpell.power);
                command.Parameters.AddWithValue("@all_health", cardSpell.health);
                command.Parameters.AddWithValue("@all_physical_attack", cardSpell.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", cardSpell.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", cardSpell.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", cardSpell.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", cardSpell.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", cardSpell.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", cardSpell.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", cardSpell.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", cardSpell.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", cardSpell.mental_defense);
                command.Parameters.AddWithValue("@all_speed", cardSpell.speed);
                command.Parameters.AddWithValue("@all_critical_damage", cardSpell.critical_damage);
                command.Parameters.AddWithValue("@all_critical_rate", cardSpell.critical_rate);
                command.Parameters.AddWithValue("@all_armor_penetration", cardSpell.armor_penetration);
                command.Parameters.AddWithValue("@all_avoid", cardSpell.avoid);
                command.Parameters.AddWithValue("@all_absorbs_damage", cardSpell.absorbs_damage);
                command.Parameters.AddWithValue("@all_regenerate_vitality", cardSpell.regenerate_vitality);
                command.Parameters.AddWithValue("@all_accuracy", cardSpell.accuracy);
                command.Parameters.AddWithValue("@all_mana", cardSpell.mana);
                command.ExecuteNonQuery();
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
    public CardSpell GetUserCardSpellById(int Id)
    {
        CardSpell card = new CardSpell();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_card_spell where card_spell_id=@id 
                and user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new CardSpell
                    {
                        id = reader.GetInt32("card_spell_id"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        star = reader.GetInt32("star"),
                        power = reader.GetDouble("power"),
                        health = reader.GetDouble("percent_all_health"),
                        physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        speed = reader.GetDouble("percent_all_speed"),
                        critical_damage = reader.GetDouble("percent_all_critical_damage"),
                        critical_rate = reader.GetDouble("percent_all_critical_rate"),
                        armor_penetration = reader.GetDouble("percent_all_armor_penetration"),
                        avoid = reader.GetDouble("percent_all_avoid"),
                        absorbs_damage = reader.GetDouble("percent_all_absorbs_damage"),
                        regenerate_vitality = reader.GetDouble("percent_all_regenerate_vitality"),
                        accuracy = reader.GetDouble("percent_all_accuracy"),
                        mana = reader.GetFloat("percent_all_mana")
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
                    CardSpell.currency = new Currency
                    {
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
    public CardSpell SumPowerCardSpellGallery()
    {
        CardSpell sumCardSpell = new CardSpell();
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
                FROM card_spell_gallery where user_id=@user_id and status = 'available';";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumCardSpell.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumCardSpell.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumCardSpell.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumCardSpell.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumCardSpell.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumCardSpell.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumCardSpell.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumCardSpell.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumCardSpell.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumCardSpell.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumCardSpell.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumCardSpell.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumCardSpell.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumCardSpell.critical_damage = reader.IsDBNull(reader.GetOrdinal("total_critical_damage")) ? 0 : reader.GetDouble("total_critical_damage");
                        sumCardSpell.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumCardSpell.armor_penetration = reader.IsDBNull(reader.GetOrdinal("total_armor_penetration")) ? 0 : reader.GetDouble("total_armor_penetration");
                        sumCardSpell.avoid = reader.IsDBNull(reader.GetOrdinal("total_avoid")) ? 0 : reader.GetDouble("total_avoid");
                        sumCardSpell.absorbs_damage = reader.IsDBNull(reader.GetOrdinal("total_absorbs_damage")) ? 0 : reader.GetDouble("total_absorbs_damage");
                        sumCardSpell.regenerate_vitality = reader.IsDBNull(reader.GetOrdinal("total_regenerate_vitality")) ? 0 : reader.GetDouble("total_regenerate_vitality");
                        sumCardSpell.accuracy = reader.IsDBNull(reader.GetOrdinal("total_accuracy")) ? 0 : reader.GetDouble("total_accuracy");
                        sumCardSpell.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetInt32("total_mana");
                        sumCardSpell.percent_all_health = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                        sumCardSpell.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                        sumCardSpell.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                        sumCardSpell.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                        sumCardSpell.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                        sumCardSpell.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                        sumCardSpell.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                        sumCardSpell.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                        sumCardSpell.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                        sumCardSpell.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                        sumCardSpell.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumCardSpell;
    }
}
