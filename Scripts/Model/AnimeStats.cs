using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;
using System;

public class AnimeStats
{
    private string type1;
    private int level1;
    private double power1;
    private double health1;
    private double physical_attack1;
    private double physical_defense1;
    private double magical_attack1;
    private double magical_defense1;
    private double chemical_attack1;
    private double chemical_defense1;
    private double atomic_attack1;
    private double atomic_defense1;
    private double mental_attack1;
    private double mental_defense1;
    private double speed1;
    private double critical_damage_rate1;
    private double critical_rate1;
    private double penetration_rate1;
    private double evasion_rate1;
    private double damage_absorption_rate1;
    private double vitality_regeneration_rate1;
    private double accuracy_rate1;
    private double lifesteal_rate1;
    private float mana1;
    private double mana_regeneration_rate1;
    private double shield_strength1;
    private double tenacity1;
    private double resistance_rate1;
    private double combo_rate1;
    private double reflection_rate1;
    private double damage_to_different_faction_rate1;
    private double resistance_to_different_faction_rate1;
    private double damage_to_same_faction_rate1;
    private double resistance_to_same_faction_rate1;
    private double percent_all_health1;
    private double percent_all_physical_attack1;
    private double percent_all_physical_defense1;
    private double percent_all_magical_attack1;
    private double percent_all_magical_defense1;
    private double percent_all_chemical_attack1;
    private double percent_all_chemical_defense1;
    private double percent_all_atomic_attack1;
    private double percent_all_atomic_defense1;
    private double percent_all_mental_attack1;
    private double percent_all_mental_defense1;

    public string type { get => type1; set => type1 = value; }
    public int level { get => level1; set => level1 = value; }
    public double power { get => power1; set => power1 = value; }
    public double health { get => health1; set => health1 = value; }
    public double physical_attack { get => physical_attack1; set => physical_attack1 = value; }
    public double physical_defense { get => physical_defense1; set => physical_defense1 = value; }
    public double magical_attack { get => magical_attack1; set => magical_attack1 = value; }
    public double magical_defense { get => magical_defense1; set => magical_defense1 = value; }
    public double chemical_attack { get => chemical_attack1; set => chemical_attack1 = value; }
    public double chemical_defense { get => chemical_defense1; set => chemical_defense1 = value; }
    public double atomic_attack { get => atomic_attack1; set => atomic_attack1 = value; }
    public double atomic_defense { get => atomic_defense1; set => atomic_defense1 = value; }
    public double mental_attack { get => mental_attack1; set => mental_attack1 = value; }
    public double mental_defense { get => mental_defense1; set => mental_defense1 = value; }
    public double speed { get => speed1; set => speed1 = value; }
    public double critical_damage_rate { get => critical_damage_rate1; set => critical_damage_rate1 = value; }
    public double critical_rate { get => critical_rate1; set => critical_rate1 = value; }
    public double penetration_rate { get => penetration_rate1; set => penetration_rate1 = value; }
    public double evasion_rate { get => evasion_rate1; set => evasion_rate1 = value; }
    public double damage_absorption_rate { get => damage_absorption_rate1; set => damage_absorption_rate1 = value; }
    public double vitality_regeneration_rate { get => vitality_regeneration_rate1; set => vitality_regeneration_rate1 = value; }
    public double accuracy_rate { get => accuracy_rate1; set => accuracy_rate1 = value; }
    public double lifesteal_rate { get => lifesteal_rate1; set => lifesteal_rate1 = value; }
    public float mana { get => mana1; set => mana1 = value; }
    public double mana_regeneration_rate { get => mana_regeneration_rate1; set => mana_regeneration_rate1 = value; }
    public double shield_strength { get => shield_strength1; set => shield_strength1 = value; }
    public double tenacity { get => tenacity1; set => tenacity1 = value; }
    public double resistance_rate { get => resistance_rate1; set => resistance_rate1 = value; }
    public double combo_rate { get => combo_rate1; set => combo_rate1 = value; }
    public double reflection_rate { get => reflection_rate1; set => reflection_rate1 = value; }
    public double damage_to_different_faction_rate { get => damage_to_different_faction_rate1; set => damage_to_different_faction_rate1 = value; }
    public double resistance_to_different_faction_rate { get => resistance_to_different_faction_rate1; set => resistance_to_different_faction_rate1 = value; }
    public double damage_to_same_faction_rate { get => damage_to_same_faction_rate1; set => damage_to_same_faction_rate1 = value; }
    public double resistance_to_same_faction_rate { get => resistance_to_same_faction_rate1; set => resistance_to_same_faction_rate1 = value; }
    public double percent_all_health { get => percent_all_health1; set => percent_all_health1 = value; }
    public double percent_all_physical_attack { get => percent_all_physical_attack1; set => percent_all_physical_attack1 = value; }
    public double percent_all_physical_defense { get => percent_all_physical_defense1; set => percent_all_physical_defense1 = value; }
    public double percent_all_magical_attack { get => percent_all_magical_attack1; set => percent_all_magical_attack1 = value; }
    public double percent_all_magical_defense { get => percent_all_magical_defense1; set => percent_all_magical_defense1 = value; }
    public double percent_all_chemical_attack { get => percent_all_chemical_attack1; set => percent_all_chemical_attack1 = value; }
    public double percent_all_chemical_defense { get => percent_all_chemical_defense1; set => percent_all_chemical_defense1 = value; }
    public double percent_all_atomic_attack { get => percent_all_atomic_attack1; set => percent_all_atomic_attack1 = value; }
    public double percent_all_atomic_defense { get => percent_all_atomic_defense1; set => percent_all_atomic_defense1 = value; }
    public double percent_all_mental_attack { get => percent_all_mental_attack1; set => percent_all_mental_attack1 = value; }
    public double percent_all_mental_defense { get => percent_all_mental_defense1; set => percent_all_mental_defense1 = value; }
    public AnimeStats()
    {

    }
    public AnimeStats GetAnimeStats(string type, int user_id)
    {
        AnimeStats animeStats = new AnimeStats();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select *
                from anime_stats
                where user_id = @user_id AND rank_type = @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // rank.id = reader.GetInt32("id");
                    animeStats.type = reader.GetString("animeStats_type");
                    animeStats.level = reader.IsDBNull(reader.GetOrdinal("animeStats_level")) ? 0 : reader.GetInt32("animeStats_level");
                    animeStats.power = reader.GetDouble("power");
                    animeStats.health = reader.GetDouble("health");
                    animeStats.physical_attack = reader.GetDouble("physical_attack");
                    animeStats.physical_defense = reader.GetDouble("physical_defense");
                    animeStats.magical_attack = reader.GetDouble("magical_attack");
                    animeStats.magical_defense = reader.GetDouble("magical_defense");
                    animeStats.chemical_attack = reader.GetDouble("chemical_attack");
                    animeStats.chemical_defense = reader.GetDouble("chemical_defense");
                    animeStats.atomic_attack = reader.GetDouble("atomic_attack");
                    animeStats.atomic_defense = reader.GetDouble("atomic_defense");
                    animeStats.mental_attack = reader.GetDouble("mental_attack");
                    animeStats.mental_defense = reader.GetDouble("mental_defense");
                    animeStats.speed = reader.GetDouble("speed");
                    animeStats.critical_damage_rate = reader.GetDouble("critical_damage_rate");
                    animeStats.critical_rate = reader.GetDouble("critical_rate");
                    animeStats.penetration_rate = reader.GetDouble("penetration_rate");
                    animeStats.evasion_rate = reader.GetDouble("evasion_rate");
                    animeStats.damage_absorption_rate = reader.GetDouble("damage_absorption_rate");
                    animeStats.vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate");
                    animeStats.accuracy_rate = reader.GetDouble("accuracy_rate");
                    animeStats.lifesteal_rate = reader.GetDouble("lifesteal_rate");
                    animeStats.shield_strength = reader.GetDouble("shield_strength");
                    animeStats.tenacity = reader.GetDouble("tenacity");
                    animeStats.resistance_rate = reader.GetDouble("resistance_rate");
                    animeStats.combo_rate = reader.GetDouble("combo_rate");
                    animeStats.reflection_rate = reader.GetDouble("reflection_rate");
                    animeStats.mana = reader.GetFloat("mana");
                    animeStats.mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate");
                    animeStats.damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate");
                    animeStats.resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate");
                    animeStats.damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate");
                    animeStats.resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate");
                    animeStats.percent_all_health = reader.GetDouble("percent_all_health");
                    animeStats.percent_all_physical_attack = reader.GetDouble("percent_all_physical_attack");
                    animeStats.percent_all_physical_defense = reader.GetDouble("percent_all_physical_defense");
                    animeStats.percent_all_magical_attack = reader.GetDouble("percent_all_magical_attack");
                    animeStats.percent_all_magical_defense = reader.GetDouble("percent_all_magical_defense");
                    animeStats.percent_all_chemical_attack = reader.GetDouble("percent_all_chemical_attack");
                    animeStats.percent_all_chemical_defense = reader.GetDouble("percent_all_chemical_defense");
                    animeStats.percent_all_atomic_attack = reader.GetDouble("percent_all_atomic_attack");
                    animeStats.percent_all_atomic_defense = reader.GetDouble("percent_all_atomic_defense");
                    animeStats.percent_all_mental_attack = reader.GetDouble("percent_all_mental_attack");
                    animeStats.percent_all_mental_defense = reader.GetDouble("percent_all_mental_defense");
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return animeStats;
    }
    public void InsertOrUpdateAnimeStats(AnimeStats animeStats, string type, int user_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM anime_stats 
                WHERE user_id = @user_id AND rank_type = @rank_type";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", user_id);
                    checkCmd.Parameters.AddWithValue("@rank_type", type);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE anime_stats
                        SET rank_level = @rank_level, power = @power, health = @health, mana = @mana, speed = @speed,  
                            physical_attack = @physical_attack, physical_defense = @physical_defense,  
                            magical_attack = @magical_attack, magical_defense = @magical_defense,  
                            chemical_attack = @chemical_attack, chemical_defense = @chemical_defense,  
                            atomic_attack = @atomic_attack, atomic_defense = @atomic_defense,  
                            mental_attack = @mental_attack, mental_defense = @mental_defense,  
                            critical_damage_rate = @critical_damage_rate, critical_rate = @critical_rate,  
                            penetration_rate = @penetration_rate, evasion_rate = @evasion_rate,  
                            damage_absorption_rate = @damage_absorption_rate, vitality_regeneration_rate = @vitality_regeneration_rate,  
                            accuracy_rate = @accuracy_rate, lifesteal_rate = @lifesteal_rate,  
                            shield_strength = @shield_strength, tenacity = @tenacity, resistance_rate = @resistance_rate,  
                            combo_rate = @combo_rate, reflection_rate = @reflection_rate,  
                            mana_regeneration_rate = @mana_regeneration_rate,  
                            damage_to_different_faction_rate = @damage_to_different_faction_rate,  
                            resistance_to_different_faction_rate = @resistance_to_different_faction_rate,  
                            damage_to_same_faction_rate = @damage_to_same_faction_rate,  
                            resistance_to_same_faction_rate = @resistance_to_same_faction_rate,  
                            percent_all_health = @percent_all_health, percent_all_physical_attack = @percent_all_physical_attack,  
                            percent_all_physical_defense = @percent_all_physical_defense, percent_all_magical_attack = @percent_all_magical_attack,  
                            percent_all_magical_defense = @percent_all_magical_defense, percent_all_chemical_attack = @percent_all_chemical_attack,  
                            percent_all_chemical_defense = @percent_all_chemical_defense, percent_all_atomic_attack = @percent_all_atomic_attack,  
                            percent_all_atomic_defense = @percent_all_atomic_defense, percent_all_mental_attack = @percent_all_mental_attack,  
                            percent_all_mental_defense = @percent_all_mental_defense
                        WHERE user_id = @user_id AND rank_type = @rank_type;
                        ";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", user_id);
                            updateCmd.Parameters.AddWithValue("@rank_type", type);
                            updateCmd.Parameters.AddWithValue("@rank_level", animeStats.level);
                            updateCmd.Parameters.AddWithValue("@power", animeStats.power);
                            updateCmd.Parameters.AddWithValue("@health", animeStats.health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", animeStats.physical_attack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", animeStats.physical_defense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", animeStats.magical_attack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", animeStats.magical_defense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", animeStats.chemical_attack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", animeStats.chemical_defense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", animeStats.atomic_attack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", animeStats.atomic_defense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", animeStats.mental_attack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", animeStats.mental_defense);
                            updateCmd.Parameters.AddWithValue("@speed", animeStats.speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage_rate", animeStats.critical_damage_rate);
                            updateCmd.Parameters.AddWithValue("@critical_rate", animeStats.critical_rate);
                            updateCmd.Parameters.AddWithValue("@penetration_rate", animeStats.penetration_rate);
                            updateCmd.Parameters.AddWithValue("@evasion_rate", animeStats.evasion_rate);
                            updateCmd.Parameters.AddWithValue("@damage_absorption_rate", animeStats.damage_absorption_rate);
                            updateCmd.Parameters.AddWithValue("@vitality_regeneration_rate", animeStats.vitality_regeneration_rate);
                            updateCmd.Parameters.AddWithValue("@accuracy_rate", animeStats.accuracy_rate);
                            updateCmd.Parameters.AddWithValue("@lifesteal_rate", animeStats.lifesteal_rate);
                            updateCmd.Parameters.AddWithValue("@shield_strength", animeStats.shield_strength);
                            updateCmd.Parameters.AddWithValue("@tenacity", animeStats.tenacity);
                            updateCmd.Parameters.AddWithValue("@resistance_rate", animeStats.resistance_rate);
                            updateCmd.Parameters.AddWithValue("@combo_rate", animeStats.combo_rate);
                            updateCmd.Parameters.AddWithValue("@reflection_rate", animeStats.reflection_rate);
                            updateCmd.Parameters.AddWithValue("@mana", animeStats.mana);
                            updateCmd.Parameters.AddWithValue("@mana_regeneration_rate", animeStats.mana_regeneration_rate);
                            updateCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", animeStats.damage_to_different_faction_rate);
                            updateCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", animeStats.resistance_to_different_faction_rate);
                            updateCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", animeStats.damage_to_same_faction_rate);
                            updateCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", animeStats.resistance_to_same_faction_rate);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", animeStats.percent_all_health);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", animeStats.percent_all_physical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", animeStats.percent_all_physical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", animeStats.percent_all_magical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", animeStats.percent_all_magical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", animeStats.percent_all_chemical_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", animeStats.percent_all_chemical_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", animeStats.percent_all_atomic_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", animeStats.percent_all_atomic_defense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", animeStats.percent_all_mental_attack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", animeStats.percent_all_mental_defense);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                        INSERT INTO anime_stats
                        (user_id, rank_type, rank_level, power, health, mana, speed, 
                        physical_attack, physical_defense, magical_attack, magical_defense, 
                        chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, 
                        critical_damage_rate, critical_rate, penetration_rate, evasion_rate, 
                        damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, lifesteal_rate, 
                        shield_strength, tenacity, resistance_rate, combo_rate, reflection_rate, 
                        mana_regeneration_rate, damage_to_different_faction_rate, resistance_to_different_faction_rate, 
                        damage_to_same_faction_rate, resistance_to_same_faction_rate, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense) 
                        VALUES 
                        (@user_id, @rank_type, @rank_level, @power, @health, @mana, @speed, 
                        @physical_attack, @physical_defense, @magical_attack, @magical_defense, 
                        @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, 
                        @mental_attack, @mental_defense, 
                        @critical_damage_rate, @critical_rate, @penetration_rate, @evasion_rate, 
                        @damage_absorption_rate, @vitality_regeneration_rate, @accuracy_rate, @lifesteal_rate, 
                        @shield_strength, @tenacity, @resistance_rate, @combo_rate, @reflection_rate, 
                        @mana_regeneration_rate, @damage_to_different_faction_rate, @resistance_to_different_faction_rate, 
                        @damage_to_same_faction_rate, @resistance_to_same_faction_rate, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense);
                        ";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các tham số như trên
                            insertCmd.Parameters.AddWithValue("@user_id", user_id);
                            insertCmd.Parameters.AddWithValue("@rank_type", type);
                            insertCmd.Parameters.AddWithValue("@rank_level", animeStats.level == 0 ? 1 : animeStats.level);
                            insertCmd.Parameters.AddWithValue("@power", animeStats.power);
                            insertCmd.Parameters.AddWithValue("@health", animeStats.health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", animeStats.physical_attack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", animeStats.physical_defense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", animeStats.magical_attack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", animeStats.magical_defense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", animeStats.chemical_attack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", animeStats.chemical_defense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", animeStats.atomic_attack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", animeStats.atomic_defense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", animeStats.mental_attack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", animeStats.mental_defense);
                            insertCmd.Parameters.AddWithValue("@speed", animeStats.speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage_rate", animeStats.critical_damage_rate);
                            insertCmd.Parameters.AddWithValue("@critical_rate", animeStats.critical_rate);
                            insertCmd.Parameters.AddWithValue("@penetration_rate", animeStats.penetration_rate);
                            insertCmd.Parameters.AddWithValue("@evasion_rate", animeStats.evasion_rate);
                            insertCmd.Parameters.AddWithValue("@damage_absorption_rate", animeStats.damage_absorption_rate);
                            insertCmd.Parameters.AddWithValue("@vitality_regeneration_rate", animeStats.vitality_regeneration_rate);
                            insertCmd.Parameters.AddWithValue("@accuracy_rate", animeStats.accuracy_rate);
                            insertCmd.Parameters.AddWithValue("@lifesteal_rate", animeStats.lifesteal_rate);
                            insertCmd.Parameters.AddWithValue("@shield_strength", animeStats.shield_strength);
                            insertCmd.Parameters.AddWithValue("@tenacity", animeStats.tenacity);
                            insertCmd.Parameters.AddWithValue("@resistance_rate", animeStats.resistance_rate);
                            insertCmd.Parameters.AddWithValue("@combo_rate", animeStats.combo_rate);
                            insertCmd.Parameters.AddWithValue("@reflection_rate", animeStats.reflection_rate);
                            insertCmd.Parameters.AddWithValue("@mana", animeStats.mana);
                            insertCmd.Parameters.AddWithValue("@mana_regeneration_rate", animeStats.mana_regeneration_rate);
                            insertCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", animeStats.damage_to_different_faction_rate);
                            insertCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", animeStats.resistance_to_different_faction_rate);
                            insertCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", animeStats.damage_to_same_faction_rate);
                            insertCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", animeStats.resistance_to_same_faction_rate);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", animeStats.percent_all_health);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", animeStats.percent_all_physical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", animeStats.percent_all_physical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", animeStats.percent_all_magical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", animeStats.percent_all_magical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", animeStats.percent_all_chemical_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", animeStats.percent_all_chemical_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", animeStats.percent_all_atomic_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", animeStats.percent_all_atomic_defense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", animeStats.percent_all_mental_attack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", animeStats.percent_all_mental_defense);
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
    public AnimeStats GetSumAnimeStats(int user_id)
    {
        AnimeStats animeStats = new AnimeStats();
        // int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                SUM(power) AS power, SUM(health) AS health,
                SUM(physical_attack) AS physical_attack, SUM(physical_defense) AS physical_defense,
                SUM(magical_attack) AS magical_attack, SUM(magical_defense) AS magical_defense,
                SUM(chemical_attack) AS chemical_attack, SUM(chemical_defense) AS chemical_defense,
                SUM(atomic_attack) AS atomic_attack, SUM(atomic_defense) AS atomic_defense,
                SUM(mental_attack) AS mental_attack, SUM(mental_defense) AS mental_defense,
                SUM(speed) AS speed, SUM(critical_damage_rate) AS critical_damage_rate,
                SUM(critical_rate) AS critical_rate, SUM(penetration_rate) AS penetration_rate,
                SUM(evasion_rate) AS evasion_rate, SUM(damage_absorption_rate) AS damage_absorption_rate,
                SUM(vitality_regeneration_rate) AS vitality_regeneration_rate, SUM(accuracy_rate) AS accuracy_rate,
                SUM(lifesteal_rate) AS lifesteal_rate, SUM(shield_strength) AS shield_strength, 
                SUM(tenacity) AS tenacity, SUM(resistance_rate) AS resistance_rate,
                SUM(combo_rate) AS combo_rate, SUM(reflection_rate) AS reflection_rate,
                SUM(mana) AS mana, SUM(mana_regeneration_rate) AS mana_regeneration_rate,
                SUM(damage_to_different_faction_rate) AS damage_to_different_faction_rate, 
                SUM(resistance_to_different_faction_rate) AS resistance_to_different_faction_rate,
                SUM(damage_to_same_faction_rate) AS damage_to_same_faction_rate, 
                SUM(resistance_to_same_faction_rate) AS resistance_to_same_faction_rate,
                SUM(percent_all_health) AS percent_all_health,
                SUM(percent_all_physical_attack) AS percent_all_physical_attack, SUM(percent_all_physical_defense) AS percent_all_physical_defense,
                SUM(percent_all_magical_attack) AS percent_all_magical_attack, SUM(percent_all_magical_defense) AS percent_all_magical_defense,
                SUM(percent_all_chemical_attack) AS percent_all_chemical_attack, SUM(percent_all_chemical_defense) AS percent_all_chemical_defense,
                SUM(percent_all_atomic_attack) AS percent_all_atomic_attack, SUM(percent_all_atomic_defense) AS percent_all_atomic_defense,
                SUM(percent_all_mental_attack) AS percent_all_mental_attack, SUM(percent_all_mental_defense) AS percent_all_mental_defense
            FROM anime_stats 
            WHERE user_id = @user_id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            animeStats.power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                            animeStats.health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                            animeStats.physical_attack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                            animeStats.physical_defense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                            animeStats.magical_attack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                            animeStats.magical_defense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                            animeStats.chemical_attack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                            animeStats.chemical_defense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                            animeStats.atomic_attack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                            animeStats.atomic_defense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                            animeStats.mental_attack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                            animeStats.mental_defense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                            animeStats.speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                            animeStats.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                            animeStats.critical_rate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                            animeStats.penetration_rate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                            animeStats.evasion_rate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                            animeStats.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                            animeStats.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                            animeStats.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                            animeStats.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                            animeStats.shield_strength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                            animeStats.tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                            animeStats.resistance_rate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                            animeStats.combo_rate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                            animeStats.reflection_rate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                            animeStats.mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                            animeStats.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                            animeStats.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                            animeStats.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                            animeStats.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                            animeStats.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                            animeStats.percent_all_health = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDouble("percent_all_health");
                            animeStats.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDouble("percent_all_physical_attack");
                            animeStats.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDouble("percent_all_physical_defense");
                            animeStats.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDouble("percent_all_magical_attack");
                            animeStats.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDouble("percent_all_magical_defense");
                            animeStats.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDouble("percent_all_chemical_attack");
                            animeStats.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDouble("percent_all_chemical_defense");
                            animeStats.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDouble("percent_all_atomic_attack");
                            animeStats.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDouble("percent_all_atomic_defense");
                            animeStats.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDouble("percent_all_mental_attack");
                            animeStats.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDouble("percent_all_mental_defense");

                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return animeStats;
    }
    
}
