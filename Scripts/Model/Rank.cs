using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class Rank
{
    public int id { get; set; }
    public string type { get; set; }
    public int level { get; set; }
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
    public Rank()
    {

    }
    public Rank GetCardHeroesRank(string type, int card_id)
    {
        Rank rank = new Rank();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select *
                from user_card_heroes_rank
                where user_id = @user_id AND rank_type = @type AND user_card_hero_id = @card_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@card_id", card_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // rank.id = reader.GetInt32("id");
                    rank.type = reader.GetString("rank_type");
                    rank.level = reader.IsDBNull(reader.GetOrdinal("rank_level")) ? 0 : reader.GetInt32("rank_level");
                    rank.power = reader.GetDouble("power");
                    rank.health = reader.GetDouble("health");
                    rank.physical_attack = reader.GetDouble("physical_attack");
                    rank.physical_defense = reader.GetDouble("physical_defense");
                    rank.magical_attack = reader.GetDouble("magical_attack");
                    rank.magical_defense = reader.GetDouble("magical_defense");
                    rank.chemical_attack = reader.GetDouble("chemical_attack");
                    rank.chemical_defense = reader.GetDouble("chemical_defense");
                    rank.atomic_attack = reader.GetDouble("atomic_attack");
                    rank.atomic_defense = reader.GetDouble("atomic_defense");
                    rank.mental_attack = reader.GetDouble("mental_attack");
                    rank.mental_defense = reader.GetDouble("mental_defense");
                    rank.speed = reader.GetDouble("speed");
                    rank.critical_damage = reader.GetDouble("critical_damage");
                    rank.critical_rate = reader.GetDouble("critical_rate");
                    rank.armor_penetration = reader.GetDouble("armor_penetration");
                    rank.avoid = reader.GetDouble("avoid");
                    rank.absorbs_damage = reader.GetDouble("absorbs_damage");
                    rank.regenerate_vitality = reader.GetDouble("regenerate_vitality");
                    rank.accuracy = reader.GetDouble("accuracy");
                    rank.mana = reader.GetFloat("mana");
                    rank.percent_all_health = reader.GetDouble("percent_all_health");
                    rank.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    rank.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    rank.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    rank.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    rank.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    rank.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    rank.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    rank.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    rank.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    rank.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return rank;
    }
    public Rank GetCardCaptainsRank(string type, int card_id)
    {
        Rank rank = new Rank();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select *
                from user_card_captains_rank
                where user_id = @user_id AND rank_type = @type AND user_card_captain_id = @card_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@card_id", card_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // rank.id = reader.GetInt32("id");
                    rank.type = reader.GetString("rank_type");
                    rank.level = reader.GetInt32("rank_level");
                    rank.power = reader.GetDouble("power");
                    rank.health = reader.GetDouble("health");
                    rank.physical_attack = reader.GetDouble("physical_attack");
                    rank.physical_defense = reader.GetDouble("physical_defense");
                    rank.magical_attack = reader.GetDouble("magical_attack");
                    rank.magical_defense = reader.GetDouble("magical_defense");
                    rank.chemical_attack = reader.GetDouble("chemical_attack");
                    rank.chemical_defense = reader.GetDouble("chemical_defense");
                    rank.atomic_attack = reader.GetDouble("atomic_attack");
                    rank.atomic_defense = reader.GetDouble("atomic_defense");
                    rank.mental_attack = reader.GetDouble("mental_attack");
                    rank.mental_defense = reader.GetDouble("mental_defense");
                    rank.speed = reader.GetDouble("speed");
                    rank.critical_damage = reader.GetDouble("critical_damage");
                    rank.critical_rate = reader.GetDouble("critical_rate");
                    rank.armor_penetration = reader.GetDouble("armor_penetration");
                    rank.avoid = reader.GetDouble("avoid");
                    rank.absorbs_damage = reader.GetDouble("absorbs_damage");
                    rank.regenerate_vitality = reader.GetDouble("regenerate_vitality");
                    rank.accuracy = reader.GetDouble("accuracy");
                    rank.mana = reader.GetFloat("mana");
                    rank.percent_all_health = reader.GetDouble("percent_all_health");
                    rank.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    rank.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    rank.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    rank.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    rank.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    rank.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    rank.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    rank.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    rank.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    rank.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return rank;
    }
    public Rank GetCardColonelsRank(string type, int card_id)
    {
        Rank rank = new Rank();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select *
                from user_card_colonels_rank
                where user_id = @user_id AND rank_type = @type AND user_card_colonel_id = @card_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@card_id", card_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // rank.id = reader.GetInt32("id");
                    rank.type = reader.GetString("rank_type");
                    rank.level = reader.GetInt32("rank_level");
                    rank.power = reader.GetDouble("power");
                    rank.health = reader.GetDouble("health");
                    rank.physical_attack = reader.GetDouble("physical_attack");
                    rank.physical_defense = reader.GetDouble("physical_defense");
                    rank.magical_attack = reader.GetDouble("magical_attack");
                    rank.magical_defense = reader.GetDouble("magical_defense");
                    rank.chemical_attack = reader.GetDouble("chemical_attack");
                    rank.chemical_defense = reader.GetDouble("chemical_defense");
                    rank.atomic_attack = reader.GetDouble("atomic_attack");
                    rank.atomic_defense = reader.GetDouble("atomic_defense");
                    rank.mental_attack = reader.GetDouble("mental_attack");
                    rank.mental_defense = reader.GetDouble("mental_defense");
                    rank.speed = reader.GetDouble("speed");
                    rank.critical_damage = reader.GetDouble("critical_damage");
                    rank.critical_rate = reader.GetDouble("critical_rate");
                    rank.armor_penetration = reader.GetDouble("armor_penetration");
                    rank.avoid = reader.GetDouble("avoid");
                    rank.absorbs_damage = reader.GetDouble("absorbs_damage");
                    rank.regenerate_vitality = reader.GetDouble("regenerate_vitality");
                    rank.accuracy = reader.GetDouble("accuracy");
                    rank.mana = reader.GetFloat("mana");
                    rank.percent_all_health = reader.GetDouble("percent_all_health");
                    rank.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    rank.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    rank.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    rank.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    rank.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    rank.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    rank.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    rank.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    rank.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    rank.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return rank;
    }
    public Rank GetCardGeneralsRank(string type, int card_id)
    {
        Rank rank = new Rank();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select *
                from user_card_generals_rank
                where user_id = @user_id AND rank_type = @type AND user_card_general_id = @card_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@card_id", card_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // rank.id = reader.GetInt32("id");
                    rank.type = reader.GetString("rank_type");
                    rank.level = reader.GetInt32("rank_level");
                    rank.power = reader.GetDouble("power");
                    rank.health = reader.GetDouble("health");
                    rank.physical_attack = reader.GetDouble("physical_attack");
                    rank.physical_defense = reader.GetDouble("physical_defense");
                    rank.magical_attack = reader.GetDouble("magical_attack");
                    rank.magical_defense = reader.GetDouble("magical_defense");
                    rank.chemical_attack = reader.GetDouble("chemical_attack");
                    rank.chemical_defense = reader.GetDouble("chemical_defense");
                    rank.atomic_attack = reader.GetDouble("atomic_attack");
                    rank.atomic_defense = reader.GetDouble("atomic_defense");
                    rank.mental_attack = reader.GetDouble("mental_attack");
                    rank.mental_defense = reader.GetDouble("mental_defense");
                    rank.speed = reader.GetDouble("speed");
                    rank.critical_damage = reader.GetDouble("critical_damage");
                    rank.critical_rate = reader.GetDouble("critical_rate");
                    rank.armor_penetration = reader.GetDouble("armor_penetration");
                    rank.avoid = reader.GetDouble("avoid");
                    rank.absorbs_damage = reader.GetDouble("absorbs_damage");
                    rank.regenerate_vitality = reader.GetDouble("regenerate_vitality");
                    rank.accuracy = reader.GetDouble("accuracy");
                    rank.mana = reader.GetFloat("mana");
                    rank.percent_all_health = reader.GetDouble("percent_all_health");
                    rank.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    rank.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    rank.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    rank.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    rank.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    rank.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    rank.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    rank.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    rank.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    rank.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return rank;
    }
    public Rank GetCardAdmiralsRank(string type, int card_id)
    {
        Rank rank = new Rank();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select *
                from user_card_admirals_rank
                where user_id = @user_id AND rank_type = @type AND user_card_admiral_id = @card_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@card_id", card_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // rank.id = reader.GetInt32("id");
                    rank.type = reader.GetString("rank_type");
                    rank.level = reader.GetInt32("rank_level");
                    rank.power = reader.GetDouble("power");
                    rank.health = reader.GetDouble("health");
                    rank.physical_attack = reader.GetDouble("physical_attack");
                    rank.physical_defense = reader.GetDouble("physical_defense");
                    rank.magical_attack = reader.GetDouble("magical_attack");
                    rank.magical_defense = reader.GetDouble("magical_defense");
                    rank.chemical_attack = reader.GetDouble("chemical_attack");
                    rank.chemical_defense = reader.GetDouble("chemical_defense");
                    rank.atomic_attack = reader.GetDouble("atomic_attack");
                    rank.atomic_defense = reader.GetDouble("atomic_defense");
                    rank.mental_attack = reader.GetDouble("mental_attack");
                    rank.mental_defense = reader.GetDouble("mental_defense");
                    rank.speed = reader.GetDouble("speed");
                    rank.critical_damage = reader.GetDouble("critical_damage");
                    rank.critical_rate = reader.GetDouble("critical_rate");
                    rank.armor_penetration = reader.GetDouble("armor_penetration");
                    rank.avoid = reader.GetDouble("avoid");
                    rank.absorbs_damage = reader.GetDouble("absorbs_damage");
                    rank.regenerate_vitality = reader.GetDouble("regenerate_vitality");
                    rank.accuracy = reader.GetDouble("accuracy");
                    rank.mana = reader.GetFloat("mana");
                    rank.percent_all_health = reader.GetDouble("percent_all_health");
                    rank.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    rank.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    rank.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    rank.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    rank.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    rank.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    rank.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    rank.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    rank.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    rank.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return rank;
    }
    public Rank GetCardMonstersRank(string type, int card_id)
    {
        Rank rank = new Rank();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select *
                from user_card_monsters_rank
                where user_id = @user_id AND rank_type = @type AND user_card_monster_id = @card_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@card_id", card_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // rank.id = reader.GetInt32("id");
                    rank.type = reader.GetString("rank_type");
                    rank.level = reader.GetInt32("rank_level");
                    rank.power = reader.GetDouble("power");
                    rank.health = reader.GetDouble("health");
                    rank.physical_attack = reader.GetDouble("physical_attack");
                    rank.physical_defense = reader.GetDouble("physical_defense");
                    rank.magical_attack = reader.GetDouble("magical_attack");
                    rank.magical_defense = reader.GetDouble("magical_defense");
                    rank.chemical_attack = reader.GetDouble("chemical_attack");
                    rank.chemical_defense = reader.GetDouble("chemical_defense");
                    rank.atomic_attack = reader.GetDouble("atomic_attack");
                    rank.atomic_defense = reader.GetDouble("atomic_defense");
                    rank.mental_attack = reader.GetDouble("mental_attack");
                    rank.mental_defense = reader.GetDouble("mental_defense");
                    rank.speed = reader.GetDouble("speed");
                    rank.critical_damage = reader.GetDouble("critical_damage");
                    rank.critical_rate = reader.GetDouble("critical_rate");
                    rank.armor_penetration = reader.GetDouble("armor_penetration");
                    rank.avoid = reader.GetDouble("avoid");
                    rank.absorbs_damage = reader.GetDouble("absorbs_damage");
                    rank.regenerate_vitality = reader.GetDouble("regenerate_vitality");
                    rank.accuracy = reader.GetDouble("accuracy");
                    rank.mana = reader.GetFloat("mana");
                    rank.percent_all_health = reader.GetDouble("percent_all_health");
                    rank.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    rank.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    rank.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    rank.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    rank.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    rank.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    rank.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    rank.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    rank.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    rank.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return rank;
    }
    public Rank GetCardMilitaryRank(string type, int card_id)
    {
        Rank rank = new Rank();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select *
                from user_card_military_rank
                where user_id = @user_id AND rank_type = @type AND user_card_military_id = @card_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@card_id", card_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // rank.id = reader.GetInt32("id");
                    rank.type = reader.GetString("rank_type");
                    rank.level = reader.GetInt32("rank_level");
                    rank.power = reader.GetDouble("power");
                    rank.health = reader.GetDouble("health");
                    rank.physical_attack = reader.GetDouble("physical_attack");
                    rank.physical_defense = reader.GetDouble("physical_defense");
                    rank.magical_attack = reader.GetDouble("magical_attack");
                    rank.magical_defense = reader.GetDouble("magical_defense");
                    rank.chemical_attack = reader.GetDouble("chemical_attack");
                    rank.chemical_defense = reader.GetDouble("chemical_defense");
                    rank.atomic_attack = reader.GetDouble("atomic_attack");
                    rank.atomic_defense = reader.GetDouble("atomic_defense");
                    rank.mental_attack = reader.GetDouble("mental_attack");
                    rank.mental_defense = reader.GetDouble("mental_defense");
                    rank.speed = reader.GetDouble("speed");
                    rank.critical_damage = reader.GetDouble("critical_damage");
                    rank.critical_rate = reader.GetDouble("critical_rate");
                    rank.armor_penetration = reader.GetDouble("armor_penetration");
                    rank.avoid = reader.GetDouble("avoid");
                    rank.absorbs_damage = reader.GetDouble("absorbs_damage");
                    rank.regenerate_vitality = reader.GetDouble("regenerate_vitality");
                    rank.accuracy = reader.GetDouble("accuracy");
                    rank.mana = reader.GetFloat("mana");
                    rank.percent_all_health = reader.GetDouble("percent_all_health");
                    rank.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    rank.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    rank.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    rank.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    rank.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    rank.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    rank.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    rank.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    rank.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    rank.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return rank;
    }
    public Rank GetCardSpellRank(string type, int card_id)
    {
        Rank rank = new Rank();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select *
                from user_card_spell_rank
                where user_id = @user_id AND rank_type = @type AND user_card_spell_id = @card_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@card_id", card_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // rank.id = reader.GetInt32("id");
                    rank.type = reader.GetString("rank_type");
                    rank.level = reader.GetInt32("rank_level");
                    rank.power = reader.GetDouble("power");
                    rank.health = reader.GetDouble("health");
                    rank.physical_attack = reader.GetDouble("physical_attack");
                    rank.physical_defense = reader.GetDouble("physical_defense");
                    rank.magical_attack = reader.GetDouble("magical_attack");
                    rank.magical_defense = reader.GetDouble("magical_defense");
                    rank.chemical_attack = reader.GetDouble("chemical_attack");
                    rank.chemical_defense = reader.GetDouble("chemical_defense");
                    rank.atomic_attack = reader.GetDouble("atomic_attack");
                    rank.atomic_defense = reader.GetDouble("atomic_defense");
                    rank.mental_attack = reader.GetDouble("mental_attack");
                    rank.mental_defense = reader.GetDouble("mental_defense");
                    rank.speed = reader.GetDouble("speed");
                    rank.critical_damage = reader.GetDouble("critical_damage");
                    rank.critical_rate = reader.GetDouble("critical_rate");
                    rank.armor_penetration = reader.GetDouble("armor_penetration");
                    rank.avoid = reader.GetDouble("avoid");
                    rank.absorbs_damage = reader.GetDouble("absorbs_damage");
                    rank.regenerate_vitality = reader.GetDouble("regenerate_vitality");
                    rank.accuracy = reader.GetDouble("accuracy");
                    rank.mana = reader.GetFloat("mana");
                    rank.percent_all_health = reader.GetDouble("percent_all_health");
                    rank.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    rank.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    rank.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    rank.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    rank.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    rank.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    rank.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    rank.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    rank.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    rank.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return rank;
    }
    public Rank GetBooksRank(string type, int card_id)
    {
        Rank rank = new Rank();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select *
                from user_books_rank
                where user_id = @user_id AND rank_type = @type AND user_book_id = @card_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@card_id", card_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // rank.id = reader.GetInt32("id");
                    rank.type = reader.GetString("rank_type");
                    rank.level = reader.GetInt32("rank_level");
                    rank.power = reader.GetDouble("power");
                    rank.health = reader.GetDouble("health");
                    rank.physical_attack = reader.GetDouble("physical_attack");
                    rank.physical_defense = reader.GetDouble("physical_defense");
                    rank.magical_attack = reader.GetDouble("magical_attack");
                    rank.magical_defense = reader.GetDouble("magical_defense");
                    rank.chemical_attack = reader.GetDouble("chemical_attack");
                    rank.chemical_defense = reader.GetDouble("chemical_defense");
                    rank.atomic_attack = reader.GetDouble("atomic_attack");
                    rank.atomic_defense = reader.GetDouble("atomic_defense");
                    rank.mental_attack = reader.GetDouble("mental_attack");
                    rank.mental_defense = reader.GetDouble("mental_defense");
                    rank.speed = reader.GetDouble("speed");
                    rank.critical_damage = reader.GetDouble("critical_damage");
                    rank.critical_rate = reader.GetDouble("critical_rate");
                    rank.armor_penetration = reader.GetDouble("armor_penetration");
                    rank.avoid = reader.GetDouble("avoid");
                    rank.absorbs_damage = reader.GetDouble("absorbs_damage");
                    rank.regenerate_vitality = reader.GetDouble("regenerate_vitality");
                    rank.accuracy = reader.GetDouble("accuracy");
                    rank.mana = reader.GetFloat("mana");
                    rank.percent_all_health = reader.GetDouble("percent_all_health");
                    rank.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    rank.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    rank.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    rank.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    rank.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    rank.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    rank.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    rank.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    rank.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    rank.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return rank;
    }
    public Rank GetPetsRank(string type, int card_id)
    {
        Rank rank = new Rank();
        int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select *
                from user_pets_rank
                where user_id = @user_id AND rank_type = @type AND user_pet_id = @card_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@card_id", card_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // rank.id = reader.GetInt32("id");
                    rank.type = reader.GetString("rank_type");
                    rank.level = reader.GetInt32("rank_level");
                    rank.power = reader.GetDouble("power");
                    rank.health = reader.GetDouble("health");
                    rank.physical_attack = reader.GetDouble("physical_attack");
                    rank.physical_defense = reader.GetDouble("physical_defense");
                    rank.magical_attack = reader.GetDouble("magical_attack");
                    rank.magical_defense = reader.GetDouble("magical_defense");
                    rank.chemical_attack = reader.GetDouble("chemical_attack");
                    rank.chemical_defense = reader.GetDouble("chemical_defense");
                    rank.atomic_attack = reader.GetDouble("atomic_attack");
                    rank.atomic_defense = reader.GetDouble("atomic_defense");
                    rank.mental_attack = reader.GetDouble("mental_attack");
                    rank.mental_defense = reader.GetDouble("mental_defense");
                    rank.speed = reader.GetDouble("speed");
                    rank.critical_damage = reader.GetDouble("critical_damage");
                    rank.critical_rate = reader.GetDouble("critical_rate");
                    rank.armor_penetration = reader.GetDouble("armor_penetration");
                    rank.avoid = reader.GetDouble("avoid");
                    rank.absorbs_damage = reader.GetDouble("absorbs_damage");
                    rank.regenerate_vitality = reader.GetDouble("regenerate_vitality");
                    rank.accuracy = reader.GetDouble("accuracy");
                    rank.mana = reader.GetFloat("mana");
                    rank.percent_all_health = reader.GetDouble("percent_all_health");
                    rank.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    rank.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    rank.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    rank.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    rank.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    rank.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    rank.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    rank.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    rank.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    rank.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return rank;
    }
    public void InsertOrUpdateCardHeroesRank(Rank rank, string type, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_heroes_rank 
                WHERE user_id = @user_id AND user_card_hero_id = @user_card_hero_id AND rank_type = @rank_type";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@user_card_hero_id", card_id);
                    checkCmd.Parameters.AddWithValue("@rank_type", type);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_card_heroes_rank
                        SET rank_level = @rank_level, 
                            power = @power, health = @health, physical_attack = @physical_attack, 
                            physical_defense = @physical_defense, magical_attack = @magical_attack, 
                            magical_defense = @magical_defense, chemical_attack = @chemical_attack, 
                            chemical_defense = @chemical_defense, atomic_attack = @atomic_attack, 
                            atomic_defense = @atomic_defense, mental_attack = @mental_attack, 
                            mental_defense = @mental_defense, speed = @speed, critical_damage = @critical_damage, 
                            critical_rate = @critical_rate, armor_penetration = @armor_penetration, 
                            avoid = @avoid, absorbs_damage = @absorbs_damage, regenerate_vitality = @regenerate_vitality, 
                            accuracy = @accuracy, mana = @mana, percent_all_health = @percent_all_health, 
                            percent_all_physical_attack = @percent_all_physical_attack, 
                            percent_all_physical_defense = @percent_all_physical_defense, 
                            percent_all_magical_attack = @percent_all_magical_attack, 
                            percent_all_magical_defense = @percent_all_magical_defense, 
                            percent_all_chemical_attack = @percent_all_chemical_attack, 
                            percent_all_chemical_defense = @percent_all_chemical_defense, 
                            percent_all_atomic_attack = @percent_all_atomic_attack, 
                            percent_all_atomic_defense = @percent_all_atomic_defense, 
                            percent_all_mental_attack = @percent_all_mental_attack, 
                            percent_all_mental_defense = @percent_all_mental_defense
                        WHERE user_id = @user_id AND user_card_hero_id = @card_id AND rank_type = @rank_type";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@rank_type", type);
                            updateCmd.Parameters.AddWithValue("@rank_level", 1);
                            updateCmd.Parameters.AddWithValue("@power", rank.power);
                            updateCmd.Parameters.AddWithValue("@health", rank.health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            updateCmd.Parameters.AddWithValue("@speed", rank.speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            updateCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            updateCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            updateCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            updateCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            updateCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            updateCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            updateCmd.Parameters.AddWithValue("@mana", rank.mana);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                        INSERT INTO user_card_heroes_rank 
                        (user_id, user_card_hero_id, rank_type, rank_level, power, health, physical_attack, 
                        physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, 
                        atomic_attack, atomic_defense, mental_attack, mental_defense, speed, critical_damage, 
                        critical_rate, armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense) 
                        VALUES 
                        (@user_id, @card_id, @rank_type, @rank_level, @power, @health, @physical_attack, 
                        @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, 
                        @atomic_attack, @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, 
                        @critical_rate, @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense)";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các tham số như trên
                            insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCmd.Parameters.AddWithValue("@card_id", card_id);
                            insertCmd.Parameters.AddWithValue("@rank_type", type);
                            insertCmd.Parameters.AddWithValue("@rank_level", rank.level);
                            insertCmd.Parameters.AddWithValue("@power", rank.power);
                            insertCmd.Parameters.AddWithValue("@health", rank.health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            insertCmd.Parameters.AddWithValue("@speed", rank.speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            insertCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            insertCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            insertCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            insertCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            insertCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            insertCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            insertCmd.Parameters.AddWithValue("@mana", rank.mana);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public void InsertOrUpdateCardCaptainsRank(Rank rank, string type, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_captains_rank 
                WHERE user_id = @user_id AND user_card_captain_id = @card_id AND rank_type = @rank_type";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@card_id", card_id);
                    checkCmd.Parameters.AddWithValue("@rank_type", type);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_card_captains_rank
                        SET rank_level = @rank_level, 
                            power = @power, health = @health, physical_attack = @physical_attack, 
                            physical_defense = @physical_defense, magical_attack = @magical_attack, 
                            magical_defense = @magical_defense, chemical_attack = @chemical_attack, 
                            chemical_defense = @chemical_defense, atomic_attack = @atomic_attack, 
                            atomic_defense = @atomic_defense, mental_attack = @mental_attack, 
                            mental_defense = @mental_defense, speed = @speed, critical_damage = @critical_damage, 
                            critical_rate = @critical_rate, armor_penetration = @armor_penetration, 
                            avoid = @avoid, absorbs_damage = @absorbs_damage, regenerate_vitality = @regenerate_vitality, 
                            accuracy = @accuracy, mana = @mana, percent_all_health = @percent_all_health, 
                            percent_all_physical_attack = @percent_all_physical_attack, 
                            percent_all_physical_defense = @percent_all_physical_defense, 
                            percent_all_magical_attack = @percent_all_magical_attack, 
                            percent_all_magical_defense = @percent_all_magical_defense, 
                            percent_all_chemical_attack = @percent_all_chemical_attack, 
                            percent_all_chemical_defense = @percent_all_chemical_defense, 
                            percent_all_atomic_attack = @percent_all_atomic_attack, 
                            percent_all_atomic_defense = @percent_all_atomic_defense, 
                            percent_all_mental_attack = @percent_all_mental_attack, 
                            percent_all_mental_defense = @percent_all_mental_defense
                        WHERE user_id = @user_id AND user_card_captain_id = @card_id AND rank_type = @rank_type";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@rank_type", type);
                            updateCmd.Parameters.AddWithValue("@rank_level", 1);
                            updateCmd.Parameters.AddWithValue("@power", rank.power);
                            updateCmd.Parameters.AddWithValue("@health", rank.health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            updateCmd.Parameters.AddWithValue("@speed", rank.speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            updateCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            updateCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            updateCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            updateCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            updateCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            updateCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            updateCmd.Parameters.AddWithValue("@mana", rank.mana);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                        INSERT INTO user_card_captains_rank 
                        (user_id, user_card_captain_id, rank_type, rank_level, power, health, physical_attack, 
                        physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, 
                        atomic_attack, atomic_defense, mental_attack, mental_defense, speed, critical_damage, 
                        critical_rate, armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense) 
                        VALUES 
                        (@user_id, @card_id, @rank_type, @rank_level, @power, @health, @physical_attack, 
                        @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, 
                        @atomic_attack, @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, 
                        @critical_rate, @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense)";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các tham số như trên
                            insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCmd.Parameters.AddWithValue("@card_id", card_id);
                            insertCmd.Parameters.AddWithValue("@rank_type", type);
                            insertCmd.Parameters.AddWithValue("@rank_level", rank.level);
                            insertCmd.Parameters.AddWithValue("@power", rank.power);
                            insertCmd.Parameters.AddWithValue("@health", rank.health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            insertCmd.Parameters.AddWithValue("@speed", rank.speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            insertCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            insertCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            insertCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            insertCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            insertCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            insertCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            insertCmd.Parameters.AddWithValue("@mana", rank.mana);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public void InsertOrUpdateCardColonelsRank(Rank rank, string type, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_colonels_rank 
                WHERE user_id = @user_id AND user_card_colonel_id = @card_id AND rank_type = @rank_type";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@card_id", card_id);
                    checkCmd.Parameters.AddWithValue("@rank_type", type);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_card_colonels_rank
                        SET rank_level = @rank_level, 
                            power = @power, health = @health, physical_attack = @physical_attack, 
                            physical_defense = @physical_defense, magical_attack = @magical_attack, 
                            magical_defense = @magical_defense, chemical_attack = @chemical_attack, 
                            chemical_defense = @chemical_defense, atomic_attack = @atomic_attack, 
                            atomic_defense = @atomic_defense, mental_attack = @mental_attack, 
                            mental_defense = @mental_defense, speed = @speed, critical_damage = @critical_damage, 
                            critical_rate = @critical_rate, armor_penetration = @armor_penetration, 
                            avoid = @avoid, absorbs_damage = @absorbs_damage, regenerate_vitality = @regenerate_vitality, 
                            accuracy = @accuracy, mana = @mana, percent_all_health = @percent_all_health, 
                            percent_all_physical_attack = @percent_all_physical_attack, 
                            percent_all_physical_defense = @percent_all_physical_defense, 
                            percent_all_magical_attack = @percent_all_magical_attack, 
                            percent_all_magical_defense = @percent_all_magical_defense, 
                            percent_all_chemical_attack = @percent_all_chemical_attack, 
                            percent_all_chemical_defense = @percent_all_chemical_defense, 
                            percent_all_atomic_attack = @percent_all_atomic_attack, 
                            percent_all_atomic_defense = @percent_all_atomic_defense, 
                            percent_all_mental_attack = @percent_all_mental_attack, 
                            percent_all_mental_defense = @percent_all_mental_defense
                        WHERE user_id = @user_id AND user_card_colonel_id = @card_id AND rank_type = @rank_type";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@rank_type", type);
                            updateCmd.Parameters.AddWithValue("@rank_level", 1);
                            updateCmd.Parameters.AddWithValue("@power", rank.power);
                            updateCmd.Parameters.AddWithValue("@health", rank.health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            updateCmd.Parameters.AddWithValue("@speed", rank.speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            updateCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            updateCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            updateCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            updateCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            updateCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            updateCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            updateCmd.Parameters.AddWithValue("@mana", rank.mana);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                        INSERT INTO user_card_colonels_rank 
                        (user_id, user_card_colonel_id, rank_type, rank_level, power, health, physical_attack, 
                        physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, 
                        atomic_attack, atomic_defense, mental_attack, mental_defense, speed, critical_damage, 
                        critical_rate, armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense) 
                        VALUES 
                        (@user_id, @card_id, @rank_type, @rank_level, @power, @health, @physical_attack, 
                        @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, 
                        @atomic_attack, @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, 
                        @critical_rate, @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense)";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các tham số như trên
                            insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCmd.Parameters.AddWithValue("@card_id", card_id);
                            insertCmd.Parameters.AddWithValue("@rank_type", type);
                            insertCmd.Parameters.AddWithValue("@rank_level", rank.level);
                            insertCmd.Parameters.AddWithValue("@power", rank.power);
                            insertCmd.Parameters.AddWithValue("@health", rank.health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            insertCmd.Parameters.AddWithValue("@speed", rank.speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            insertCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            insertCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            insertCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            insertCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            insertCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            insertCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            insertCmd.Parameters.AddWithValue("@mana", rank.mana);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public void InsertOrUpdateCardGeneralsRank(Rank rank, string type, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_generals_rank 
                WHERE user_id = @user_id AND user_card_general_id = @card_id AND rank_type = @rank_type";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@card_id", card_id);
                    checkCmd.Parameters.AddWithValue("@rank_type", type);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_card_generals_rank
                        SET rank_level = @rank_level, 
                            power = @power, health = @health, physical_attack = @physical_attack, 
                            physical_defense = @physical_defense, magical_attack = @magical_attack, 
                            magical_defense = @magical_defense, chemical_attack = @chemical_attack, 
                            chemical_defense = @chemical_defense, atomic_attack = @atomic_attack, 
                            atomic_defense = @atomic_defense, mental_attack = @mental_attack, 
                            mental_defense = @mental_defense, speed = @speed, critical_damage = @critical_damage, 
                            critical_rate = @critical_rate, armor_penetration = @armor_penetration, 
                            avoid = @avoid, absorbs_damage = @absorbs_damage, regenerate_vitality = @regenerate_vitality, 
                            accuracy = @accuracy, mana = @mana, percent_all_health = @percent_all_health, 
                            percent_all_physical_attack = @percent_all_physical_attack, 
                            percent_all_physical_defense = @percent_all_physical_defense, 
                            percent_all_magical_attack = @percent_all_magical_attack, 
                            percent_all_magical_defense = @percent_all_magical_defense, 
                            percent_all_chemical_attack = @percent_all_chemical_attack, 
                            percent_all_chemical_defense = @percent_all_chemical_defense, 
                            percent_all_atomic_attack = @percent_all_atomic_attack, 
                            percent_all_atomic_defense = @percent_all_atomic_defense, 
                            percent_all_mental_attack = @percent_all_mental_attack, 
                            percent_all_mental_defense = @percent_all_mental_defense
                        WHERE user_id = @user_id AND user_card_general_id = @card_id AND rank_type = @rank_type";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@rank_type", type);
                            updateCmd.Parameters.AddWithValue("@rank_level", 1);
                            updateCmd.Parameters.AddWithValue("@power", rank.power);
                            updateCmd.Parameters.AddWithValue("@health", rank.health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            updateCmd.Parameters.AddWithValue("@speed", rank.speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            updateCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            updateCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            updateCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            updateCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            updateCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            updateCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            updateCmd.Parameters.AddWithValue("@mana", rank.mana);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                        INSERT INTO user_card_generals_rank 
                        (user_id, user_card_general_id, rank_type, rank_level, power, health, physical_attack, 
                        physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, 
                        atomic_attack, atomic_defense, mental_attack, mental_defense, speed, critical_damage, 
                        critical_rate, armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense) 
                        VALUES 
                        (@user_id, @card_id, @rank_type, @rank_level, @power, @health, @physical_attack, 
                        @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, 
                        @atomic_attack, @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, 
                        @critical_rate, @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense)";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các tham số như trên
                            insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCmd.Parameters.AddWithValue("@card_id", card_id);
                            insertCmd.Parameters.AddWithValue("@rank_type", type);
                            insertCmd.Parameters.AddWithValue("@rank_level", rank.level);
                            insertCmd.Parameters.AddWithValue("@power", rank.power);
                            insertCmd.Parameters.AddWithValue("@health", rank.health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            insertCmd.Parameters.AddWithValue("@speed", rank.speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            insertCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            insertCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            insertCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            insertCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            insertCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            insertCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            insertCmd.Parameters.AddWithValue("@mana", rank.mana);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public void InsertOrUpdateCardAdmiralsRank(Rank rank, string type, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_admirals_rank 
                WHERE user_id = @user_id AND user_card_admiral_id = @card_id AND rank_type = @rank_type";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@card_id", card_id);
                    checkCmd.Parameters.AddWithValue("@rank_type", type);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_card_admirals_rank
                        SET rank_level = @rank_level, 
                            power = @power, health = @health, physical_attack = @physical_attack, 
                            physical_defense = @physical_defense, magical_attack = @magical_attack, 
                            magical_defense = @magical_defense, chemical_attack = @chemical_attack, 
                            chemical_defense = @chemical_defense, atomic_attack = @atomic_attack, 
                            atomic_defense = @atomic_defense, mental_attack = @mental_attack, 
                            mental_defense = @mental_defense, speed = @speed, critical_damage = @critical_damage, 
                            critical_rate = @critical_rate, armor_penetration = @armor_penetration, 
                            avoid = @avoid, absorbs_damage = @absorbs_damage, regenerate_vitality = @regenerate_vitality, 
                            accuracy = @accuracy, mana = @mana, percent_all_health = @percent_all_health, 
                            percent_all_physical_attack = @percent_all_physical_attack, 
                            percent_all_physical_defense = @percent_all_physical_defense, 
                            percent_all_magical_attack = @percent_all_magical_attack, 
                            percent_all_magical_defense = @percent_all_magical_defense, 
                            percent_all_chemical_attack = @percent_all_chemical_attack, 
                            percent_all_chemical_defense = @percent_all_chemical_defense, 
                            percent_all_atomic_attack = @percent_all_atomic_attack, 
                            percent_all_atomic_defense = @percent_all_atomic_defense, 
                            percent_all_mental_attack = @percent_all_mental_attack, 
                            percent_all_mental_defense = @percent_all_mental_defense
                        WHERE user_id = @user_id AND user_card_admiral_id = @card_id AND rank_type = @rank_type";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@rank_type", type);
                            updateCmd.Parameters.AddWithValue("@rank_level", 1);
                            updateCmd.Parameters.AddWithValue("@power", rank.power);
                            updateCmd.Parameters.AddWithValue("@health", rank.health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            updateCmd.Parameters.AddWithValue("@speed", rank.speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            updateCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            updateCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            updateCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            updateCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            updateCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            updateCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            updateCmd.Parameters.AddWithValue("@mana", rank.mana);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                        INSERT INTO user_card_admirals_rank 
                        (user_id, user_card_admiral_id, rank_type, rank_level, power, health, physical_attack, 
                        physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, 
                        atomic_attack, atomic_defense, mental_attack, mental_defense, speed, critical_damage, 
                        critical_rate, armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense) 
                        VALUES 
                        (@user_id, @card_id, @rank_type, @rank_level, @power, @health, @physical_attack, 
                        @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, 
                        @atomic_attack, @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, 
                        @critical_rate, @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense)";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các tham số như trên
                            insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCmd.Parameters.AddWithValue("@card_id", card_id);
                            insertCmd.Parameters.AddWithValue("@rank_type", rank.type);
                            insertCmd.Parameters.AddWithValue("@rank_level", rank.level);
                            insertCmd.Parameters.AddWithValue("@power", rank.power);
                            insertCmd.Parameters.AddWithValue("@health", rank.health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            insertCmd.Parameters.AddWithValue("@speed", rank.speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            insertCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            insertCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            insertCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            insertCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            insertCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            insertCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            insertCmd.Parameters.AddWithValue("@mana", rank.mana);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public void InsertOrUpdateCardMonstersRank(Rank rank, string type, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_monsters_rank 
                WHERE user_id = @user_id AND user_card_monster_id = @card_id AND rank_type = @rank_type";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@card_id", card_id);
                    checkCmd.Parameters.AddWithValue("@rank_type", type);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_card_monsters_rank
                        SET rank_level = @rank_level, 
                            power = @power, health = @health, physical_attack = @physical_attack, 
                            physical_defense = @physical_defense, magical_attack = @magical_attack, 
                            magical_defense = @magical_defense, chemical_attack = @chemical_attack, 
                            chemical_defense = @chemical_defense, atomic_attack = @atomic_attack, 
                            atomic_defense = @atomic_defense, mental_attack = @mental_attack, 
                            mental_defense = @mental_defense, speed = @speed, critical_damage = @critical_damage, 
                            critical_rate = @critical_rate, armor_penetration = @armor_penetration, 
                            avoid = @avoid, absorbs_damage = @absorbs_damage, regenerate_vitality = @regenerate_vitality, 
                            accuracy = @accuracy, mana = @mana, percent_all_health = @percent_all_health, 
                            percent_all_physical_attack = @percent_all_physical_attack, 
                            percent_all_physical_defense = @percent_all_physical_defense, 
                            percent_all_magical_attack = @percent_all_magical_attack, 
                            percent_all_magical_defense = @percent_all_magical_defense, 
                            percent_all_chemical_attack = @percent_all_chemical_attack, 
                            percent_all_chemical_defense = @percent_all_chemical_defense, 
                            percent_all_atomic_attack = @percent_all_atomic_attack, 
                            percent_all_atomic_defense = @percent_all_atomic_defense, 
                            percent_all_mental_attack = @percent_all_mental_attack, 
                            percent_all_mental_defense = @percent_all_mental_defense
                        WHERE user_id = @user_id AND user_card_monster_id = @card_id AND rank_type = @rank_type";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@rank_type", type);
                            updateCmd.Parameters.AddWithValue("@rank_level", 1);
                            updateCmd.Parameters.AddWithValue("@power", rank.power);
                            updateCmd.Parameters.AddWithValue("@health", rank.health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            updateCmd.Parameters.AddWithValue("@speed", rank.speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            updateCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            updateCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            updateCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            updateCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            updateCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            updateCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            updateCmd.Parameters.AddWithValue("@mana", rank.mana);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                        INSERT INTO user_card_monsters_rank 
                        (user_id, user_card_monster_id, rank_type, rank_level, power, health, physical_attack, 
                        physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, 
                        atomic_attack, atomic_defense, mental_attack, mental_defense, speed, critical_damage, 
                        critical_rate, armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense) 
                        VALUES 
                        (@user_id, @card_id, @rank_type, @rank_level, @power, @health, @physical_attack, 
                        @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, 
                        @atomic_attack, @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, 
                        @critical_rate, @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense)";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các tham số như trên
                            insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCmd.Parameters.AddWithValue("@card_id", card_id);
                            insertCmd.Parameters.AddWithValue("@rank_type", rank.type);
                            insertCmd.Parameters.AddWithValue("@rank_level", rank.level);
                            insertCmd.Parameters.AddWithValue("@power", rank.power);
                            insertCmd.Parameters.AddWithValue("@health", rank.health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            insertCmd.Parameters.AddWithValue("@speed", rank.speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            insertCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            insertCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            insertCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            insertCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            insertCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            insertCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            insertCmd.Parameters.AddWithValue("@mana", rank.mana);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public void InsertOrUpdateCardMilitaryRank(Rank rank, string type, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_military_rank 
                WHERE user_id = @user_id AND user_card_military_id = @card_id AND rank_type = @rank_type";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@card_id", card_id);
                    checkCmd.Parameters.AddWithValue("@rank_type", type);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_card_military_rank
                        SET rank_level = @rank_level, 
                            power = @power, health = @health, physical_attack = @physical_attack, 
                            physical_defense = @physical_defense, magical_attack = @magical_attack, 
                            magical_defense = @magical_defense, chemical_attack = @chemical_attack, 
                            chemical_defense = @chemical_defense, atomic_attack = @atomic_attack, 
                            atomic_defense = @atomic_defense, mental_attack = @mental_attack, 
                            mental_defense = @mental_defense, speed = @speed, critical_damage = @critical_damage, 
                            critical_rate = @critical_rate, armor_penetration = @armor_penetration, 
                            avoid = @avoid, absorbs_damage = @absorbs_damage, regenerate_vitality = @regenerate_vitality, 
                            accuracy = @accuracy, mana = @mana, percent_all_health = @percent_all_health, 
                            percent_all_physical_attack = @percent_all_physical_attack, 
                            percent_all_physical_defense = @percent_all_physical_defense, 
                            percent_all_magical_attack = @percent_all_magical_attack, 
                            percent_all_magical_defense = @percent_all_magical_defense, 
                            percent_all_chemical_attack = @percent_all_chemical_attack, 
                            percent_all_chemical_defense = @percent_all_chemical_defense, 
                            percent_all_atomic_attack = @percent_all_atomic_attack, 
                            percent_all_atomic_defense = @percent_all_atomic_defense, 
                            percent_all_mental_attack = @percent_all_mental_attack, 
                            percent_all_mental_defense = @percent_all_mental_defense
                        WHERE user_id = @user_id AND user_card_military_id = @card_id AND rank_type = @rank_type";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@rank_type", type);
                            updateCmd.Parameters.AddWithValue("@rank_level", 1);
                            updateCmd.Parameters.AddWithValue("@power", rank.power);
                            updateCmd.Parameters.AddWithValue("@health", rank.health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            updateCmd.Parameters.AddWithValue("@speed", rank.speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            updateCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            updateCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            updateCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            updateCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            updateCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            updateCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            updateCmd.Parameters.AddWithValue("@mana", rank.mana);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                        INSERT INTO user_card_military_rank 
                        (user_id, user_card_military_id, rank_type, rank_level, power, health, physical_attack, 
                        physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, 
                        atomic_attack, atomic_defense, mental_attack, mental_defense, speed, critical_damage, 
                        critical_rate, armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense) 
                        VALUES 
                        (@user_id, @card_id, @rank_type, @rank_level, @power, @health, @physical_attack, 
                        @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, 
                        @atomic_attack, @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, 
                        @critical_rate, @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense)";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các tham số như trên
                            insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCmd.Parameters.AddWithValue("@card_id", card_id);
                            insertCmd.Parameters.AddWithValue("@rank_type", type);
                            insertCmd.Parameters.AddWithValue("@rank_level", rank.level);
                            insertCmd.Parameters.AddWithValue("@power", rank.power);
                            insertCmd.Parameters.AddWithValue("@health", rank.health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            insertCmd.Parameters.AddWithValue("@speed", rank.speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            insertCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            insertCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            insertCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            insertCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            insertCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            insertCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            insertCmd.Parameters.AddWithValue("@mana", rank.mana);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public void InsertOrUpdateCardSpellRank(Rank rank, string type, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_spell_rank 
                WHERE user_id = @user_id AND user_card_spell_id = @card_id AND rank_type = @rank_type";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@card_id", card_id);
                    checkCmd.Parameters.AddWithValue("@rank_type", type);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_card_spell_rank
                        SET rank_level = @rank_level, 
                            power = @power, health = @health, physical_attack = @physical_attack, 
                            physical_defense = @physical_defense, magical_attack = @magical_attack, 
                            magical_defense = @magical_defense, chemical_attack = @chemical_attack, 
                            chemical_defense = @chemical_defense, atomic_attack = @atomic_attack, 
                            atomic_defense = @atomic_defense, mental_attack = @mental_attack, 
                            mental_defense = @mental_defense, speed = @speed, critical_damage = @critical_damage, 
                            critical_rate = @critical_rate, armor_penetration = @armor_penetration, 
                            avoid = @avoid, absorbs_damage = @absorbs_damage, regenerate_vitality = @regenerate_vitality, 
                            accuracy = @accuracy, mana = @mana, percent_all_health = @percent_all_health, 
                            percent_all_physical_attack = @percent_all_physical_attack, 
                            percent_all_physical_defense = @percent_all_physical_defense, 
                            percent_all_magical_attack = @percent_all_magical_attack, 
                            percent_all_magical_defense = @percent_all_magical_defense, 
                            percent_all_chemical_attack = @percent_all_chemical_attack, 
                            percent_all_chemical_defense = @percent_all_chemical_defense, 
                            percent_all_atomic_attack = @percent_all_atomic_attack, 
                            percent_all_atomic_defense = @percent_all_atomic_defense, 
                            percent_all_mental_attack = @percent_all_mental_attack, 
                            percent_all_mental_defense = @percent_all_mental_defense
                        WHERE user_id = @user_id AND user_card_spell_id = @card_id AND rank_type = @rank_type";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@rank_type", type);
                            updateCmd.Parameters.AddWithValue("@rank_level", 1);
                            updateCmd.Parameters.AddWithValue("@power", rank.power);
                            updateCmd.Parameters.AddWithValue("@health", rank.health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            updateCmd.Parameters.AddWithValue("@speed", rank.speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            updateCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            updateCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            updateCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            updateCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            updateCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            updateCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            updateCmd.Parameters.AddWithValue("@mana", rank.mana);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                        INSERT INTO user_card_spell_rank 
                        (user_id, user_card_spell_id, rank_type, rank_level, power, health, physical_attack, 
                        physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, 
                        atomic_attack, atomic_defense, mental_attack, mental_defense, speed, critical_damage, 
                        critical_rate, armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense) 
                        VALUES 
                        (@user_id, @card_id, @rank_type, @rank_level, @power, @health, @physical_attack, 
                        @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, 
                        @atomic_attack, @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, 
                        @critical_rate, @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense)";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các tham số như trên
                            insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCmd.Parameters.AddWithValue("@card_id", card_id);
                            insertCmd.Parameters.AddWithValue("@rank_type", type);
                            insertCmd.Parameters.AddWithValue("@rank_level", rank.level);
                            insertCmd.Parameters.AddWithValue("@power", rank.power);
                            insertCmd.Parameters.AddWithValue("@health", rank.health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            insertCmd.Parameters.AddWithValue("@speed", rank.speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            insertCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            insertCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            insertCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            insertCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            insertCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            insertCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            insertCmd.Parameters.AddWithValue("@mana", rank.mana);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public void InsertOrUpdateBooksRank(Rank rank, string type, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM user_books_rank 
                WHERE user_id = @user_id AND user_book_id = @card_id AND rank_type = @rank_type";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@card_id", card_id);
                    checkCmd.Parameters.AddWithValue("@rank_type", type);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_books_rank
                        SET rank_level = @rank_level, 
                            power = @power, health = @health, physical_attack = @physical_attack, 
                            physical_defense = @physical_defense, magical_attack = @magical_attack, 
                            magical_defense = @magical_defense, chemical_attack = @chemical_attack, 
                            chemical_defense = @chemical_defense, atomic_attack = @atomic_attack, 
                            atomic_defense = @atomic_defense, mental_attack = @mental_attack, 
                            mental_defense = @mental_defense, speed = @speed, critical_damage = @critical_damage, 
                            critical_rate = @critical_rate, armor_penetration = @armor_penetration, 
                            avoid = @avoid, absorbs_damage = @absorbs_damage, regenerate_vitality = @regenerate_vitality, 
                            accuracy = @accuracy, mana = @mana, percent_all_health = @percent_all_health, 
                            percent_all_physical_attack = @percent_all_physical_attack, 
                            percent_all_physical_defense = @percent_all_physical_defense, 
                            percent_all_magical_attack = @percent_all_magical_attack, 
                            percent_all_magical_defense = @percent_all_magical_defense, 
                            percent_all_chemical_attack = @percent_all_chemical_attack, 
                            percent_all_chemical_defense = @percent_all_chemical_defense, 
                            percent_all_atomic_attack = @percent_all_atomic_attack, 
                            percent_all_atomic_defense = @percent_all_atomic_defense, 
                            percent_all_mental_attack = @percent_all_mental_attack, 
                            percent_all_mental_defense = @percent_all_mental_defense
                        WHERE user_id = @user_id AND user_book_id = @card_id AND rank_type = @rank_type";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@rank_type", type);
                            updateCmd.Parameters.AddWithValue("@rank_level", 1);
                            updateCmd.Parameters.AddWithValue("@power", rank.power);
                            updateCmd.Parameters.AddWithValue("@health", rank.health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            updateCmd.Parameters.AddWithValue("@speed", rank.speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            updateCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            updateCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            updateCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            updateCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            updateCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            updateCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            updateCmd.Parameters.AddWithValue("@mana", rank.mana);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                        INSERT INTO user_books_rank 
                        (user_id, user_book_id, rank_type, rank_level, power, health, physical_attack, 
                        physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, 
                        atomic_attack, atomic_defense, mental_attack, mental_defense, speed, critical_damage, 
                        critical_rate, armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense) 
                        VALUES 
                        (@user_id, @card_id, @rank_type, @rank_level, @power, @health, @physical_attack, 
                        @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, 
                        @atomic_attack, @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, 
                        @critical_rate, @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense)";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các tham số như trên
                            insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCmd.Parameters.AddWithValue("@card_id", card_id);
                            insertCmd.Parameters.AddWithValue("@rank_type", type);
                            insertCmd.Parameters.AddWithValue("@rank_level", rank.level);
                            insertCmd.Parameters.AddWithValue("@power", rank.power);
                            insertCmd.Parameters.AddWithValue("@health", rank.health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            insertCmd.Parameters.AddWithValue("@speed", rank.speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            insertCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            insertCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            insertCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            insertCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            insertCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            insertCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            insertCmd.Parameters.AddWithValue("@mana", rank.mana);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public void InsertOrUpdatePetsRank(Rank rank, string type, int card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM user_pets_rank 
                WHERE user_id = @user_id AND user_pet_id = @card_id AND rank_type = @rank_type";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@card_id", card_id);
                    checkCmd.Parameters.AddWithValue("@rank_type", type);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_pets_rank
                        SET rank_level = @rank_level, 
                            power = @power, health = @health, physical_attack = @physical_attack, 
                            physical_defense = @physical_defense, magical_attack = @magical_attack, 
                            magical_defense = @magical_defense, chemical_attack = @chemical_attack, 
                            chemical_defense = @chemical_defense, atomic_attack = @atomic_attack, 
                            atomic_defense = @atomic_defense, mental_attack = @mental_attack, 
                            mental_defense = @mental_defense, speed = @speed, critical_damage = @critical_damage, 
                            critical_rate = @critical_rate, armor_penetration = @armor_penetration, 
                            avoid = @avoid, absorbs_damage = @absorbs_damage, regenerate_vitality = @regenerate_vitality, 
                            accuracy = @accuracy, mana = @mana, percent_all_health = @percent_all_health, 
                            percent_all_physical_attack = @percent_all_physical_attack, 
                            percent_all_physical_defense = @percent_all_physical_defense, 
                            percent_all_magical_attack = @percent_all_magical_attack, 
                            percent_all_magical_defense = @percent_all_magical_defense, 
                            percent_all_chemical_attack = @percent_all_chemical_attack, 
                            percent_all_chemical_defense = @percent_all_chemical_defense, 
                            percent_all_atomic_attack = @percent_all_atomic_attack, 
                            percent_all_atomic_defense = @percent_all_atomic_defense, 
                            percent_all_mental_attack = @percent_all_mental_attack, 
                            percent_all_mental_defense = @percent_all_mental_defense
                        WHERE user_id = @user_id AND user_pet_id = @card_id AND rank_type = @rank_type";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@rank_type", type);
                            updateCmd.Parameters.AddWithValue("@rank_level", 1);
                            updateCmd.Parameters.AddWithValue("@power", rank.power);
                            updateCmd.Parameters.AddWithValue("@health", rank.health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            updateCmd.Parameters.AddWithValue("@speed", rank.speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            updateCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            updateCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            updateCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            updateCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            updateCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            updateCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            updateCmd.Parameters.AddWithValue("@mana", rank.mana);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                        INSERT INTO user_pets_rank 
                        (user_id, user_pet_id, rank_type, rank_level, power, health, physical_attack, 
                        physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, 
                        atomic_attack, atomic_defense, mental_attack, mental_defense, speed, critical_damage, 
                        critical_rate, armor_penetration, avoid, absorbs_damage, regenerate_vitality, accuracy, mana, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense) 
                        VALUES 
                        (@user_id, @card_id, @rank_type, @rank_level, @power, @health, @physical_attack, 
                        @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, 
                        @atomic_attack, @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage, 
                        @critical_rate, @armor_penetration, @avoid, @absorbs_damage, @regenerate_vitality, @accuracy, @mana, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense)";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các tham số như trên
                            insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCmd.Parameters.AddWithValue("@card_id", card_id);
                            insertCmd.Parameters.AddWithValue("@rank_type", type);
                            insertCmd.Parameters.AddWithValue("@rank_level", rank.level);
                            insertCmd.Parameters.AddWithValue("@power", rank.power);
                            insertCmd.Parameters.AddWithValue("@health", rank.health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", rank.physical_attack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", rank.physical_defense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", rank.magical_defense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", rank.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", rank.chemical_defense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", rank.atomic_attack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", rank.atomic_defense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", rank.mental_attack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", rank.mental_defense);
                            insertCmd.Parameters.AddWithValue("@speed", rank.speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage", rank.critical_damage);
                            insertCmd.Parameters.AddWithValue("@critical_rate", rank.critical_rate);
                            insertCmd.Parameters.AddWithValue("@armor_penetration", rank.armor_penetration);
                            insertCmd.Parameters.AddWithValue("@avoid", rank.avoid);
                            insertCmd.Parameters.AddWithValue("@absorbs_damage", rank.absorbs_damage);
                            insertCmd.Parameters.AddWithValue("@regenerate_vitality", rank.regenerate_vitality);
                            insertCmd.Parameters.AddWithValue("@accuracy", rank.accuracy);
                            insertCmd.Parameters.AddWithValue("@mana", rank.mana);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", rank.percent_all_health);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.percent_all_physical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.percent_all_physical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.percent_all_magical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.percent_all_magical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.percent_all_chemical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.percent_all_chemical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.percent_all_atomic_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.percent_all_atomic_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.percent_all_mental_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.percent_all_mental_defense);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
}
