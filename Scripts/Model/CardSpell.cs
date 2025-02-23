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
    public Currency currency { get; set; }
    public CardSpell()
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
        team_id = -1;
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
    public List<CardSpell> GetFinalPower(List<CardSpell> CardSpellList)
    {
        PowerManager powerManager = new PowerManager();
        powerManager = powerManager.GetUserStats();
        foreach (var c in CardSpellList)
        {
            CardSpell card = new CardSpell();
            card = card.GetUserCardSpellById(c.id);
            c.all_power = powerManager.GetFinalCardSpellPower(c);
            c.all_health = c.all_health + powerManager.health + card.health * powerManager.percent_all_health / 100;
            c.all_physical_attack = c.all_physical_attack + powerManager.physical_attack + card.physical_attack * powerManager.percent_all_physical_attack / 100;
            c.all_physical_defense = c.all_physical_defense + powerManager.physical_defense + card.physical_defense * powerManager.percent_all_physical_defense / 100;
            c.all_magical_attack = c.all_magical_attack + powerManager.magical_attack + card.magical_attack * powerManager.percent_all_magical_attack / 100;
            c.all_magical_defense = c.all_magical_defense + powerManager.magical_defense + card.magical_defense * powerManager.percent_all_magical_defense / 100;
            c.all_chemical_attack = c.all_chemical_attack + powerManager.chemical_attack + card.chemical_attack * powerManager.percent_all_chemical_attack / 100;
            c.all_chemical_defense = c.all_chemical_defense + powerManager.chemical_defense + card.chemical_defense * powerManager.percent_all_chemical_defense / 100;
            c.all_atomic_attack = c.all_atomic_attack + powerManager.atomic_attack + card.atomic_attack * powerManager.percent_all_atomic_attack / 100;
            c.all_atomic_defense = c.all_atomic_defense + powerManager.atomic_defense + card.atomic_defense * powerManager.percent_all_atomic_defense / 100;
            c.all_mental_attack = c.all_mental_attack + powerManager.mental_attack + card.mental_attack * powerManager.percent_all_mental_attack / 100;
            c.all_mental_defense = c.all_mental_defense + powerManager.mental_defense + card.mental_defense * powerManager.percent_all_mental_defense / 100;
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
        return CardSpellList;
    }
    public List<CardSpell> GetAllEquipmentPower(List<CardSpell> CardSpellList)
    {
        Equipments equipments = new Equipments();
        foreach (var c in CardSpellList)
        {
            equipments = equipments.GetAllEquipmentsByCardSpellId(c.id);
            c.all_health = c.all_health + equipments.health + equipments.special_health;
            c.all_physical_attack = c.all_physical_attack + equipments.physical_attack + equipments.special_physical_attack;
            c.all_physical_defense = c.all_physical_defense + equipments.physical_defense + equipments.special_physical_defense;
            c.all_magical_attack = c.all_magical_attack + equipments.magical_attack + equipments.special_magical_attack;
            c.all_magical_defense = c.all_magical_defense + equipments.magical_defense + equipments.special_magical_defense;
            c.all_chemical_attack = c.all_chemical_attack + equipments.chemical_attack + equipments.special_chemical_attack;
            c.all_chemical_defense = c.all_chemical_defense + equipments.chemical_defense + equipments.special_chemical_defense;
            c.all_atomic_attack = c.all_atomic_attack + equipments.atomic_attack + equipments.special_atomic_attack;
            c.all_atomic_defense = c.all_atomic_defense + equipments.atomic_defense + equipments.special_atomic_defense;
            c.all_mental_attack = c.all_mental_attack + equipments.mental_attack + equipments.special_mental_attack;
            c.all_mental_defense = c.all_mental_defense + equipments.mental_defense + equipments.special_mental_defense;
            c.all_speed = c.all_speed + equipments.speed;
            c.all_critical_damage = c.all_critical_damage + equipments.critical_damage;
            c.all_critical_rate = c.all_critical_rate + equipments.critical_rate;
            c.all_armor_penetration = c.all_armor_penetration + equipments.armor_penetration;
            c.all_avoid = c.all_avoid + equipments.avoid;
            c.all_absorbs_damage = c.all_absorbs_damage + equipments.absorbs_damage;
            c.all_regenerate_vitality = c.all_regenerate_vitality + equipments.regenerate_vitality;
            c.all_accuracy = c.all_accuracy + equipments.accuracy;
            c.all_mana = c.all_mana + equipments.mana;

            c.all_power = Math.Floor(0.5 * (
            c.all_health +
            c.all_physical_attack +
            c.all_physical_defense +
            c.all_magical_attack +
            c.all_magical_defense +
            c.all_chemical_attack +
            c.all_chemical_defense +
            c.all_atomic_attack +
            c.all_atomic_defense +
            c.all_mental_attack +
            c.all_mental_defense +
            c.all_speed +
            c.all_critical_damage +
            c.all_critical_rate +
            c.all_armor_penetration +
            c.all_avoid +
            c.all_absorbs_damage +
            c.all_regenerate_vitality +
            c.all_accuracy +
            c.all_mana)
        );
        }
        return CardSpellList;
    }
    public List<CardSpell> GetAllRankPower(List<CardSpell> CardSpellList)
    {
        Rank rank = new Rank();
        foreach (var c in CardSpellList)
        {
            CardSpell card = new CardSpell();
            card = card.GetUserCardSpellById(c.id);
            rank = rank.GetSumCardSpellRank(c.id);
            c.all_health = c.all_health + rank.health + card.health * rank.percent_all_health/100;
            c.all_physical_attack = c.all_physical_attack + rank.physical_attack + card.physical_attack * rank.percent_all_physical_attack/100;
            c.all_physical_defense = c.all_physical_defense + rank.physical_defense + card.physical_defense * rank.percent_all_physical_defense/100;
            c.all_magical_attack = c.all_magical_attack + rank.magical_attack + card.magical_attack * rank.percent_all_magical_attack/100;
            c.all_magical_defense = c.all_magical_defense + rank.magical_defense + card.magical_defense * rank.percent_all_magical_defense/100;
            c.all_chemical_attack = c.all_chemical_attack + rank.chemical_attack + card.chemical_attack * rank.percent_all_chemical_attack/100;
            c.all_chemical_defense = c.all_chemical_defense + rank.chemical_defense + card.chemical_defense * rank.percent_all_chemical_defense/100;
            c.all_atomic_attack = c.all_atomic_attack + rank.atomic_attack + card.atomic_attack * rank.percent_all_atomic_attack/100;
            c.all_atomic_defense = c.all_atomic_defense + rank.atomic_defense + card.atomic_defense * rank.percent_all_atomic_defense/100;
            c.all_mental_attack = c.all_mental_attack + rank.mental_attack + card.mental_attack * rank.percent_all_mental_attack/100;
            c.all_mental_defense = c.all_mental_defense + rank.mental_defense + card.mental_defense * rank.percent_all_mental_defense/100;
            c.all_speed = c.all_speed + rank.speed;
            c.all_critical_damage = c.all_critical_damage + rank.critical_damage;
            c.all_critical_rate = c.all_critical_rate + rank.critical_rate;
            c.all_armor_penetration = c.all_armor_penetration + rank.armor_penetration;
            c.all_avoid = c.all_avoid + rank.avoid;
            c.all_absorbs_damage = c.all_absorbs_damage + rank.absorbs_damage;
            c.all_regenerate_vitality = c.all_regenerate_vitality + rank.regenerate_vitality;
            c.all_accuracy = c.all_accuracy + rank.accuracy;
            c.all_mana = c.all_mana + rank.mana;

            c.all_power = Math.Floor(0.5 * (
            c.all_health +
            c.all_physical_attack +
            c.all_physical_defense +
            c.all_magical_attack +
            c.all_magical_defense +
            c.all_chemical_attack +
            c.all_chemical_defense +
            c.all_atomic_attack +
            c.all_atomic_defense +
            c.all_mental_attack +
            c.all_mental_defense +
            c.all_speed +
            c.all_critical_damage +
            c.all_critical_rate +
            c.all_armor_penetration +
            c.all_avoid +
            c.all_absorbs_damage +
            c.all_regenerate_vitality +
            c.all_accuracy +
            c.all_mana)
        );
        }
        return CardSpellList;
    }
    public CardSpell GetNewLevelPower(CardSpell c, double coefficient)
    {
        CardSpell orginCard = new CardSpell();
        orginCard = orginCard.GetCardSpellById(c.id);
        CardSpell cardSpell = new CardSpell
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
        cardSpell.power = 0.5 * (
            cardSpell.health +
            cardSpell.physical_attack +
            cardSpell.physical_defense +
            cardSpell.magical_attack +
            cardSpell.magical_defense +
            cardSpell.chemical_attack +
            cardSpell.chemical_defense +
            cardSpell.atomic_attack +
            cardSpell.atomic_defense +
            cardSpell.mental_attack +
            cardSpell.mental_defense +
            cardSpell.speed +
            cardSpell.critical_damage +
            cardSpell.critical_rate +
            cardSpell.armor_penetration +
            cardSpell.avoid +
            cardSpell.absorbs_damage +
            cardSpell.regenerate_vitality +
            cardSpell.accuracy +
            cardSpell.mana
        );
        return cardSpell;
    }
    public CardSpell GetNewBreakthroughPower(CardSpell c, double coefficient)
    {
        CardSpell orginCard = new CardSpell();
        orginCard = orginCard.GetCardSpellById(c.id);
        CardSpell cardSpell = new CardSpell
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
        cardSpell.power = 0.5 * (
            cardSpell.health +
            cardSpell.physical_attack +
            cardSpell.physical_defense +
            cardSpell.magical_attack +
            cardSpell.magical_defense +
            cardSpell.chemical_attack +
            cardSpell.chemical_defense +
            cardSpell.atomic_attack +
            cardSpell.atomic_defense +
            cardSpell.mental_attack +
            cardSpell.mental_defense +
            cardSpell.speed +
            cardSpell.critical_damage +
            cardSpell.critical_rate +
            cardSpell.armor_penetration +
            cardSpell.avoid +
            cardSpell.absorbs_damage +
            cardSpell.regenerate_vitality +
            cardSpell.accuracy +
            cardSpell.mana
        );
        return cardSpell;
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

                    CardSpellList.Add(CardSpell);
                }
                CardSpellList = GetFinalPower(CardSpellList);
                CardSpellList = GetAllEquipmentPower(CardSpellList);
                CardSpellList = GetAllRankPower(CardSpellList);
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

                    CardSpellList.Add(CardSpell);
                }
                CardSpellList = GetFinalPower(CardSpellList);
                CardSpellList = GetAllEquipmentPower(CardSpellList);
                CardSpellList = GetAllRankPower(CardSpellList);
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
                    user_id, card_spell_id, level, experiment, star, block, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage, critical_rate, 
                    armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana
                ) VALUES (
                    @user_id, @card_spell_id, @level, @experiment, @star, @block, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, @critical_rate, 
                    @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana
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
                    command.Parameters.AddWithValue("@health", CardSpell.health);
                    command.Parameters.AddWithValue("@physical_attack", CardSpell.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", CardSpell.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", CardSpell.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", CardSpell.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", CardSpell.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", CardSpell.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", CardSpell.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", CardSpell.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", CardSpell.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", CardSpell.mental_defense);
                    command.Parameters.AddWithValue("@speed", CardSpell.speed);
                    command.Parameters.AddWithValue("@critical_damage", CardSpell.critical_damage);
                    command.Parameters.AddWithValue("@critical_rate", CardSpell.critical_rate);
                    command.Parameters.AddWithValue("@armor_penetration", CardSpell.armor_penetration);
                    command.Parameters.AddWithValue("@avoid", CardSpell.avoid);
                    command.Parameters.AddWithValue("@absorbs_damage", CardSpell.absorbs_damage);
                    command.Parameters.AddWithValue("@regenerate_vitality", CardSpell.regenerate_vitality);
                    command.Parameters.AddWithValue("@accuracy", CardSpell.accuracy);
                    command.Parameters.AddWithValue("@mana", CardSpell.mana);
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
                    user_id = @user_id AND card_spell_id = @card_spell_id;";
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
                    user_id = @user_id AND card_spell_id = @card_spell_id;";
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
                    all_health, all_physical_attack, all_physical_defense, all_magical_attack, all_magical_defense,
                    all_chemical_attack, all_chemical_defense, all_atomic_attack, all_atomic_defense,
                    all_mental_attack, all_mental_defense, all_speed, all_critical_damage, all_critical_rate,
                    all_armor_penetration, all_avoid, all_absorbs_damage, all_regenerate_vitality, all_accuracy, all_mana
                ) VALUES (
                    @user_id, @user_card_spell_id, @all_power,
                    @all_health, @all_physical_attack, @all_physical_defense, @all_magical_attack, @all_magical_defense,
                    @all_chemical_attack, @all_chemical_defense, @all_atomic_attack, @all_atomic_defense,
                    @all_mental_attack, @all_mental_defense, @all_speed, @all_critical_damage, @all_critical_rate,
                    @all_armor_penetration, @all_avoid, @all_absorbs_damage, @all_regenerate_vitality, @all_accuracy, @all_mana
                );
                    ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@user_card_spell_id", CardSpell.id);
                command.Parameters.AddWithValue("@all_power", CardSpell.power);
                command.Parameters.AddWithValue("@all_health", CardSpell.health);
                command.Parameters.AddWithValue("@all_physical_attack", CardSpell.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", CardSpell.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", CardSpell.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", CardSpell.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", CardSpell.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", CardSpell.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", CardSpell.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", CardSpell.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", CardSpell.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", CardSpell.mental_defense);
                command.Parameters.AddWithValue("@all_speed", CardSpell.speed);
                command.Parameters.AddWithValue("@all_critical_damage", CardSpell.critical_damage);
                command.Parameters.AddWithValue("@all_critical_rate", CardSpell.critical_rate);
                command.Parameters.AddWithValue("@all_armor_penetration", CardSpell.armor_penetration);
                command.Parameters.AddWithValue("@all_avoid", CardSpell.avoid);
                command.Parameters.AddWithValue("@all_absorbs_damage", CardSpell.absorbs_damage);
                command.Parameters.AddWithValue("@all_regenerate_vitality", CardSpell.regenerate_vitality);
                command.Parameters.AddWithValue("@all_accuracy", CardSpell.accuracy);
                command.Parameters.AddWithValue("@all_mana", CardSpell.mana);
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
                    user_id = @user_id AND user_card_spell_id = @user_card_spell_id;";
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
