using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class UserTitlesRepository : IUserTitlesRepository
{
    public List<Titles> GetUserTitles(string user_id, int pageSize, int offset)
    {
        List<Titles> titlesList = new List<Titles>();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select ut.*, t.id, t.name, t.image, t.rare, t.description from Titles t, user_titles ut where t.id=ut.title_id and ut.user_id=@userId 
                ORDER BY t.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(t.name, '[0-9]+$') AS UNSIGNED), t.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Titles title = new Titles
                    {
                        id = reader.GetString("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
                        star = reader.GetInt32("star"),
                        level = reader.GetInt32("level"),
                        experiment = reader.GetInt32("experiment"),
                        quantity = reader.GetInt32("quantity"),
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
                        critical_damage_rate = reader.GetDouble("critical_damage_rate"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        // percent_all_health = reader.GetDouble("percent_all_health"),
                        // percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack"),
                        // percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense"),
                        // percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack"),
                        // percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense"),
                        // percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack"),
                        // percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense"),
                        // percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack"),
                        // percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense"),
                        // percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack"),
                        // percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense"),
                        description = reader.GetString("description")
                    };

                    titlesList.Add(title);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return titlesList;
    }
    public int GetUserTitlesCount(string user_id)
    {
        int count = 0;
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from Titles t, user_titles ut where t.id=ut.title_id and ut.user_id=@userId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
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
    public bool InsertUserTitles(Titles titles)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_titles 
                WHERE user_id = @user_id AND title_id = @title_id;";

                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@title_id", titles.id);

                int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                if (count == 0)
                {
                    string query = @"
                INSERT INTO user_titles (
                    user_id, title_id, level, experiment, star, quality, block, quantity, power, health, physical_attack, 
                    physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, 
                    atomic_defense, mental_attack, mental_defense, speed, critical_damage_rate, critical_rate, 
                    penetration_rate, evasion_rate, damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, 
                    lifesteal_rate, shield_strength, tenacity, resistance_rate, combo_rate, reflection_rate, 
                    mana, mana_regeneration_rate, damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate, damage_to_same_faction_rate, resistance_to_same_faction_rate
                ) VALUES (
                    @user_id, @title_id, @level, @experiment, @star, @quality, @block, @quantity, @power, @health, @physical_attack, 
                    @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, 
                    @atomic_defense, @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate, 
                    @penetration_rate, @evasion_rate, @damage_absorption_rate, @vitality_regeneration_rate, @accuracy_rate, 
                    @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate, @combo_rate, @reflection_rate, 
                    @mana, @mana_regeneration_rate, @damage_to_different_faction_rate, 
                    @resistance_to_different_faction_rate, @damage_to_same_faction_rate, @resistance_to_same_faction_rate
                );";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@title_id", titles.id);
                    command.Parameters.AddWithValue("@level", 0);
                    command.Parameters.AddWithValue("@experiment", 0);
                    command.Parameters.AddWithValue("@star", 0);
                    command.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(titles.rare));
                    command.Parameters.AddWithValue("@block", false);
                    command.Parameters.AddWithValue("@quantity", 0);
                    command.Parameters.AddWithValue("@power", titles.power);
                    command.Parameters.AddWithValue("@health", titles.health);
                    command.Parameters.AddWithValue("@physical_attack", titles.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", titles.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", titles.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", titles.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", titles.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", titles.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", titles.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", titles.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", titles.mental_attack);
                    command.Parameters.AddWithValue("@mental_defense", titles.mental_defense);
                    command.Parameters.AddWithValue("@speed", titles.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", titles.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", titles.critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", titles.penetration_rate);
                    command.Parameters.AddWithValue("@evasion_rate", titles.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", titles.damage_absorption_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", titles.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", titles.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", titles.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", titles.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", titles.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", titles.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", titles.combo_rate);
                    command.Parameters.AddWithValue("@reflection_rate", titles.reflection_rate);
                    command.Parameters.AddWithValue("@mana", titles.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", titles.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", titles.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", titles.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", titles.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", titles.resistance_to_same_faction_rate);
                    MySqlDataReader reader = command.ExecuteReader();
                }
                else
                {
                    // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                    string updateQuery = @"
                    UPDATE user_titles
                    SET quantity = quantity + 1
                    WHERE user_id = @user_id AND title_id = @title_id;";

                    MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    updateCommand.Parameters.AddWithValue("@title_id", titles.id);

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
    public bool UpdateTitlesLevel(Titles titles, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_titles
                SET 
                    level = @level, power = @power, health = @health, 
                    physical_attack = @physical_attack, physical_defense = @physical_defense, 
                    magical_attack = @magical_attack, magical_defense = @magical_defense, 
                    chemical_attack = @chemical_attack, chemical_defense = @chemical_defense, 
                    atomic_attack = @atomic_attack, atomic_defense = @atomic_defense, 
                    mental_attack = @mental_attack, mental_defense = @mental_defense, 
                    speed = @speed, critical_damage_rate = @critical_damage_rate, 
                    critical_rate = @critical_rate, penetration_rate = @penetration_rate, 
                    evasion_rate = @evasion_rate, damage_absorption_rate = @damage_absorption_rate, 
                    vitality_regeneration_rate = @vitality_regeneration_rate, accuracy_rate = @accuracy_rate, 
                    lifesteal_rate = @lifesteal_rate, shield_strength = @shield_strength, 
                    tenacity = @tenacity, resistance_rate = @resistance_rate, combo_rate = @combo_rate, 
                    reflection_rate = @reflection_rate, mana = @mana, mana_regeneration_rate = @mana_regeneration_rate, 
                    damage_to_different_faction_rate = @damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate = @resistance_to_different_faction_rate, 
                    damage_to_same_faction_rate = @damage_to_same_faction_rate, 
                    resistance_to_same_faction_rate = @resistance_to_same_faction_rate
                WHERE user_id = @user_id AND title_id = @title_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@title_id", titles.id);
                command.Parameters.AddWithValue("@level", cardLevel);
                command.Parameters.AddWithValue("@power", titles.power);
                command.Parameters.AddWithValue("@health", titles.health);
                command.Parameters.AddWithValue("@physical_attack", titles.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", titles.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", titles.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", titles.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", titles.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", titles.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", titles.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", titles.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", titles.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", titles.mental_defense);
                command.Parameters.AddWithValue("@speed", titles.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", titles.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", titles.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", titles.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", titles.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", titles.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", titles.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", titles.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", titles.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", titles.shield_strength);
                command.Parameters.AddWithValue("@tenacity", titles.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", titles.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", titles.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", titles.reflection_rate);
                command.Parameters.AddWithValue("@mana", titles.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", titles.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", titles.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", titles.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", titles.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", titles.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public bool UpdateTitlesBreakthrough(Titles titles, int star, int quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                UPDATE user_titles
                SET 
                    star = @star, quantity = @quantity, power=@power, health = @health, 
                    physical_attack = @physical_attack, physical_defense = @physical_defense, 
                    magical_attack = @magical_attack, magical_defense = @magical_defense, 
                    chemical_attack = @chemical_attack, chemical_defense = @chemical_defense, 
                    atomic_attack = @atomic_attack, atomic_defense = @atomic_defense, 
                    mental_attack = @mental_attack, mental_defense = @mental_defense, 
                    speed = @speed, critical_damage_rate = @critical_damage_rate, 
                    critical_rate = @critical_rate, penetration_rate = @penetration_rate, 
                    evasion_rate = @evasion_rate, damage_absorption_rate = @damage_absorption_rate, 
                    vitality_regeneration_rate = @vitality_regeneration_rate, accuracy_rate = @accuracy_rate, 
                    lifesteal_rate = @lifesteal_rate, shield_strength = @shield_strength, 
                    tenacity = @tenacity, resistance_rate = @resistance_rate, combo_rate = @combo_rate, 
                    reflection_rate = @reflection_rate, mana = @mana, mana_regeneration_rate = @mana_regeneration_rate, 
                    damage_to_different_faction_rate = @damage_to_different_faction_rate, 
                    resistance_to_different_faction_rate = @resistance_to_different_faction_rate, 
                    damage_to_same_faction_rate = @damage_to_same_faction_rate, 
                    resistance_to_same_faction_rate = @resistance_to_same_faction_rate
                WHERE user_id = @user_id AND title_id = @title_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@title_id", titles.id);
                command.Parameters.AddWithValue("@star", star);
                command.Parameters.AddWithValue("@quantity", quantity);
                command.Parameters.AddWithValue("@power", titles.power);
                command.Parameters.AddWithValue("@health", titles.health);
                command.Parameters.AddWithValue("@physical_attack", titles.physical_attack);
                command.Parameters.AddWithValue("@physical_defense", titles.physical_defense);
                command.Parameters.AddWithValue("@magical_attack", titles.magical_attack);
                command.Parameters.AddWithValue("@magical_defense", titles.magical_defense);
                command.Parameters.AddWithValue("@chemical_attack", titles.chemical_attack);
                command.Parameters.AddWithValue("@chemical_defense", titles.chemical_defense);
                command.Parameters.AddWithValue("@atomic_attack", titles.atomic_attack);
                command.Parameters.AddWithValue("@atomic_defense", titles.atomic_defense);
                command.Parameters.AddWithValue("@mental_attack", titles.mental_attack);
                command.Parameters.AddWithValue("@mental_defense", titles.mental_defense);
                command.Parameters.AddWithValue("@speed", titles.speed);
                command.Parameters.AddWithValue("@critical_damage_rate", titles.critical_damage_rate);
                command.Parameters.AddWithValue("@critical_rate", titles.critical_rate);
                command.Parameters.AddWithValue("@penetration_rate", titles.penetration_rate);
                command.Parameters.AddWithValue("@evasion_rate", titles.evasion_rate);
                command.Parameters.AddWithValue("@damage_absorption_rate", titles.damage_absorption_rate);
                command.Parameters.AddWithValue("@vitality_regeneration_rate", titles.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@accuracy_rate", titles.accuracy_rate);
                command.Parameters.AddWithValue("@lifesteal_rate", titles.lifesteal_rate);
                command.Parameters.AddWithValue("@shield_strength", titles.shield_strength);
                command.Parameters.AddWithValue("@tenacity", titles.tenacity);
                command.Parameters.AddWithValue("@resistance_rate", titles.resistance_rate);
                command.Parameters.AddWithValue("@combo_rate", titles.combo_rate);
                command.Parameters.AddWithValue("@reflection_rate", titles.reflection_rate);
                command.Parameters.AddWithValue("@mana", titles.mana);
                command.Parameters.AddWithValue("@mana_regeneration_rate", titles.mana_regeneration_rate);
                command.Parameters.AddWithValue("@damage_to_different_faction_rate", titles.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_different_faction_rate", titles.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@damage_to_same_faction_rate", titles.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@resistance_to_same_faction_rate", titles.resistance_to_same_faction_rate);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return true;
    }
    public Titles GetUserTitlesById(string user_id, string Id)
    {
        Titles card = new Titles();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"Select * from user_titles where user_titles.title_id=@id 
                and user_titles.user_id=@user_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@user_id", user_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    card = new Titles
                    {
                        id = reader.GetString("title_id"),
                        level = reader.GetInt32("level"),
                        quality = reader.GetInt32("quality"),
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
                        critical_damage_rate = reader.GetDouble("critical_damage_rate"),
                        critical_rate = reader.GetDouble("critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
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
    public Titles SumPowerUserTitles()
    {
        Titles sumTitles = new Titles();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                SUM(power * (1 + quality / 10.0)) AS total_power, SUM(health * (1 + quality / 10.0)) AS total_health, SUM(mana * (1 + quality / 10.0)) AS total_mana, 
                SUM(physical_attack * (1 + quality / 10.0)) AS total_physical_attack, SUM(physical_defense * (1 + quality / 10.0)) AS total_physical_defense, 
                SUM(magical_attack * (1 + quality / 10.0)) AS total_magical_attack, SUM(magical_defense * (1 + quality / 10.0)) AS total_magical_defense, 
                SUM(chemical_attack * (1 + quality / 10.0)) AS total_chemical_attack, SUM(chemical_defense * (1 + quality / 10.0)) AS total_chemical_defense, 
                SUM(atomic_attack * (1 + quality / 10.0)) AS total_atomic_attack, SUM(atomic_defense * (1 + quality / 10.0)) AS total_atomic_defense, 
                SUM(mental_attack * (1 + quality / 10.0)) AS total_mental_attack, SUM(mental_defense * (1 + quality / 10.0)) AS total_mental_defense, 
                SUM(speed * (1 + quality / 10.0)) AS total_speed, SUM(critical_damage_rate * (1 + quality / 10.0)) AS total_critical_damage_rate, 
                SUM(critical_rate * (1 + quality / 10.0)) AS total_critical_rate, SUM(penetration_rate * (1 + quality / 10.0)) AS total_penetration_rate, 
                SUM(evasion_rate * (1 + quality / 10.0)) AS total_evasion_rate, SUM(damage_absorption_rate * (1 + quality / 10.0)) AS total_damage_absorption_rate, 
                SUM(vitality_regeneration_rate * (1 + quality / 10.0)) AS total_vitality_regeneration_rate, SUM(accuracy_rate * (1 + quality / 10.0)) AS total_accuracy_rate, 
                SUM(lifesteal_rate * (1 + quality / 10.0)) AS total_lifesteal_rate, SUM(shield_strength * (1 + quality / 10.0)) AS total_shield_strength, 
                SUM(tenacity * (1 + quality / 10.0)) AS total_tenacity, SUM(resistance_rate * (1 + quality / 10.0)) AS total_resistance_rate, 
                SUM(combo_rate * (1 + quality / 10.0)) AS total_combo_rate, SUM(reflection_rate * (1 + quality / 10.0)) AS total_reflection_rate, 
                SUM(mana_regeneration_rate * (1 + quality / 10.0)) AS total_mana_regeneration_rate, 
                SUM(damage_to_different_faction_rate * (1 + quality / 10.0)) AS total_damage_to_different_faction_rate, 
                SUM(resistance_to_different_faction_rate * (1 + quality / 10.0)) AS total_resistance_to_different_faction_rate, 
                SUM(damage_to_same_faction_rate * (1 + quality / 10.0)) AS total_damage_to_same_faction_rate, 
                SUM(resistance_to_same_faction_rate * (1 + quality / 10.0)) AS total_resistance_to_same_faction_rate
            FROM user_titles
            WHERE user_id = @user_id;
            ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumTitles.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumTitles.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumTitles.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumTitles.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumTitles.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumTitles.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumTitles.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumTitles.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumTitles.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumTitles.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumTitles.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumTitles.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumTitles.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumTitles.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                        sumTitles.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumTitles.penetration_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumTitles.evasion_rate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumTitles.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumTitles.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumTitles.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumTitles.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumTitles.shield_strength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumTitles.tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumTitles.resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumTitles.combo_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumTitles.reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumTitles.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumTitles.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumTitles.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumTitles.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumTitles.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumTitles.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumTitles;
    }
}