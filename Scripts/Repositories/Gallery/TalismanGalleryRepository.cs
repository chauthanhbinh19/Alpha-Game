using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class TalismanGalleryRepository : ITalismanGalleryRepository
{
    public List<Talisman> GetTalismanCollection(string type, int pageSize, int offset)
    {
        List<Talisman> talismanList = new List<Talisman>();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT m.*, CASE WHEN mg.talisman_id IS NULL THEN 'block' WHEN mg.status = 'pending' THEN 'pending' WHEN mg.status = 'available' THEN 'available' END AS status 
                FROM talisman m LEFT JOIN talisman_gallery mg ON m.id = mg.talisman_id and mg.user_id = @userId where m.type=@type 
                ORDER BY m.name REGEXP '[0-9]+$',CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name limit @limit offset @offset";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@limit", pageSize);
                command.Parameters.AddWithValue("@offset", offset);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Talisman talisman = new Talisman
                    {
                        id = reader.GetString("id"),
                        name = reader.GetString("name"),
                        image = reader.GetString("image"),
                        rare = reader.GetString("rare"),
                        quality = reader.GetInt32("quality"),
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
                        critical_resistance_rate = reader.GetDouble("critical_resistance_rate"),
                        ignore_critical_rate = reader.GetDouble("ignore_critical_rate"),
                        penetration_rate = reader.GetDouble("penetration_rate"),
                        penetration_resistance_rate = reader.GetDouble("penetration_resistance_rate"),
                        evasion_rate = reader.GetDouble("evasion_rate"),
                        damage_absorption_rate = reader.GetDouble("damage_absorption_rate"),
                        ignore_damage_absorption_rate = reader.GetDouble("ignore_damage_absorption_rate"),
                        absorbed_damage_rate = reader.GetDouble("absorbed_damage_rate"),
                        vitality_regeneration_rate = reader.GetDouble("vitality_regeneration_rate"),
                        vitality_regeneration_resistance_rate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                        accuracy_rate = reader.GetDouble("accuracy_rate"),
                        lifesteal_rate = reader.GetDouble("lifesteal_rate"),
                        shield_strength = reader.GetDouble("shield_strength"),
                        tenacity = reader.GetDouble("tenacity"),
                        resistance_rate = reader.GetDouble("resistance_rate"),
                        combo_rate = reader.GetDouble("combo_rate"),
                        ignore_combo_rate = reader.GetDouble("ignore_combo_rate"),
                        combo_damage_rate = reader.GetDouble("combo_damage_rate"),
                        combo_resistance_rate = reader.GetDouble("combo_resistance_rate"),
                        stun_rate = reader.GetDouble("stun_rate"),
                        ignore_stun_rate = reader.GetDouble("ignore_stun_rate"),
                        reflection_rate = reader.GetDouble("reflection_rate"),
                        ignore_reflection_rate = reader.GetDouble("ignore_reflection_rate"),
                        reflection_damage_rate = reader.GetDouble("reflection_damage_rate"),
                        reflection_resistance_rate = reader.GetDouble("reflection_resistance_rate"),
                        mana = reader.GetFloat("mana"),
                        mana_regeneration_rate = reader.GetDouble("mana_regeneration_rate"),
                        damage_to_different_faction_rate = reader.GetDouble("damage_to_different_faction_rate"),
                        resistance_to_different_faction_rate = reader.GetDouble("resistance_to_different_faction_rate"),
                        damage_to_same_faction_rate = reader.GetDouble("damage_to_same_faction_rate"),
                        resistance_to_same_faction_rate = reader.GetDouble("resistance_to_same_faction_rate"),
                        normal_damage_rate = reader.GetDouble("normal_damage_rate"),
                        normal_resistance_rate = reader.GetDouble("normal_resistance_rate"),
                        skill_damage_rate = reader.GetDouble("skill_damage_rate"),
                        skill_resistance_rate = reader.GetDouble("skill_resistance_rate"),
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
                        description = reader.GetString("description"),
                        status = reader.GetString("status"),
                    };

                    talismanList.Add(talisman);
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return talismanList;
    }
    public int GetTalismanCount(string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "Select count(*) from talisman where type =@type";
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
    public void InsertTalismanGallery(string Id, Talisman TalismanFromDB)
    {
        // Talisman TalismanFromDB = GetTalismanById(Id);
        int percent = QualityEvaluator.CheckQuality(TalismanFromDB.type);
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Kiểm tra bản ghi đã tồn tại
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM talisman_gallery 
                WHERE user_id = @user_id AND talisman_id = @talisman_id;
                ";
                MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);
                checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                checkCommand.Parameters.AddWithValue("@talisman_id", Id);

                int recordCount = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (recordCount == 0)
                {
                    string query = @"
                    INSERT INTO talisman_gallery (
                        user_id, talisman_id, status, current_star, temp_star, power, health, physical_attack, physical_defense, 
                        magical_attack, magical_defense, chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate, 
                        penetration_rate, penetration_resistance_rate, evasion_rate, 
                        damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate, vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                        accuracy_rate, lifesteal_rate, shield_strength, tenacity, 
                        resistance_rate, combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate, stun_rate, ignore_stun_rate, 
                        reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate, mana, mana_regeneration_rate, 
                        damage_to_different_faction_rate, resistance_to_different_faction_rate, 
                        damage_to_same_faction_rate, resistance_to_same_faction_rate, 
                        normal_damage_rate, normal_resistance_rate, 
                        skill_damage_rate, skill_resistance_rate, 
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense
                    ) VALUES (
                        @user_id, @talisman_id, @status, @current_star, @temp_star, @power, @health, @physical_attack, @physical_defense, 
                        @magical_attack, @magical_defense, @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, 
                        @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate, 
                        @penetration_rate, @penetration_resistance_rate, @evasion_rate, 
                        @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate, @vitality_regeneration_rate, @vitality_regeneration_resistance_rate, 
                        @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, 
                        @resistance_rate, @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate, @stun_rate, @ignore_stun_rate, 
                        @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate, @mana, @mana_regeneration_rate, 
                        @damage_to_different_faction_rate, @resistance_to_different_faction_rate, 
                        @damage_to_same_faction_rate, @resistance_to_same_faction_rate, 
                        @normal_damage_rate, @normal_resistance_rate, 
                        @skill_damage_rate, @skill_resistance_rate, 
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense
                    );
                    ";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@talisman_id", Id);
                    command.Parameters.AddWithValue("@status", "pending");
                    command.Parameters.AddWithValue("@current_star", 0);
                    command.Parameters.AddWithValue("@temp_star", 0);
                    command.Parameters.AddWithValue("@power", TalismanFromDB.power);
                    command.Parameters.AddWithValue("@health", TalismanFromDB.health);
                    command.Parameters.AddWithValue("@physical_attack", TalismanFromDB.physical_attack);
                    command.Parameters.AddWithValue("@physical_defense", TalismanFromDB.physical_defense);
                    command.Parameters.AddWithValue("@magical_attack", TalismanFromDB.magical_attack);
                    command.Parameters.AddWithValue("@magical_defense", TalismanFromDB.magical_defense);
                    command.Parameters.AddWithValue("@chemical_attack", TalismanFromDB.chemical_attack);
                    command.Parameters.AddWithValue("@chemical_defense", TalismanFromDB.chemical_defense);
                    command.Parameters.AddWithValue("@atomic_attack", TalismanFromDB.atomic_attack);
                    command.Parameters.AddWithValue("@atomic_defense", TalismanFromDB.atomic_defense);
                    command.Parameters.AddWithValue("@mental_attack", TalismanFromDB.magical_attack);
                    command.Parameters.AddWithValue("@mental_defense", TalismanFromDB.magical_defense);
                    command.Parameters.AddWithValue("@speed", TalismanFromDB.speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", TalismanFromDB.critical_damage_rate);
                    command.Parameters.AddWithValue("@critical_rate", TalismanFromDB.critical_rate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", TalismanFromDB.critical_resistance_rate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", TalismanFromDB.ignore_critical_rate);
                    command.Parameters.AddWithValue("@penetration_rate", TalismanFromDB.penetration_rate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", TalismanFromDB.penetration_resistance_rate);
                    command.Parameters.AddWithValue("@evasion_rate", TalismanFromDB.evasion_rate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", TalismanFromDB.damage_absorption_rate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", TalismanFromDB.ignore_damage_absorption_rate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", TalismanFromDB.absorbed_damage_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", TalismanFromDB.vitality_regeneration_rate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", TalismanFromDB.vitality_regeneration_resistance_rate);
                    command.Parameters.AddWithValue("@accuracy_rate", TalismanFromDB.accuracy_rate);
                    command.Parameters.AddWithValue("@lifesteal_rate", TalismanFromDB.lifesteal_rate);
                    command.Parameters.AddWithValue("@shield_strength", TalismanFromDB.shield_strength);
                    command.Parameters.AddWithValue("@tenacity", TalismanFromDB.tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", TalismanFromDB.resistance_rate);
                    command.Parameters.AddWithValue("@combo_rate", TalismanFromDB.combo_rate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", TalismanFromDB.ignore_combo_rate);
                    command.Parameters.AddWithValue("@combo_damage_rate", TalismanFromDB.combo_damage_rate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", TalismanFromDB.combo_resistance_rate);
                    command.Parameters.AddWithValue("@stun_rate", TalismanFromDB.stun_rate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", TalismanFromDB.ignore_stun_rate);
                    command.Parameters.AddWithValue("@reflection_rate", TalismanFromDB.reflection_rate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", TalismanFromDB.ignore_reflection_rate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", TalismanFromDB.reflection_damage_rate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", TalismanFromDB.reflection_resistance_rate);
                    command.Parameters.AddWithValue("@mana", TalismanFromDB.mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", TalismanFromDB.mana_regeneration_rate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", TalismanFromDB.damage_to_different_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", TalismanFromDB.resistance_to_different_faction_rate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", TalismanFromDB.damage_to_same_faction_rate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", TalismanFromDB.resistance_to_same_faction_rate);
                    command.Parameters.AddWithValue("@normal_damage_rate", TalismanFromDB.normal_damage_rate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", TalismanFromDB.normal_resistance_rate);
                    command.Parameters.AddWithValue("@skill_damage_rate", TalismanFromDB.skill_damage_rate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", TalismanFromDB.skill_resistance_rate);
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
    public void UpdateStatusTalismanGallery(string Id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = "update talisman_gallery set status=@status where user_id=@user_id and talisman_id=@talisman_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                command.Parameters.AddWithValue("@talisman_id", Id);
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
    public Talisman SumPowerTalismanGallery()
    {
        Talisman sumTalisman = new Talisman();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
                SUM(power) AS total_power, SUM(health) AS total_health, SUM(mana) AS total_mana, 
                SUM(physical_attack) AS total_physical_attack, SUM(physical_defense) AS total_physical_defense, 
                SUM(magical_attack) AS total_magical_attack, SUM(magical_defense) AS total_magical_defense, 
                SUM(chemical_attack) AS total_chemical_attack, SUM(chemical_defense) AS total_chemical_defense, 
                SUM(atomic_attack) AS total_atomic_attack, SUM(atomic_defense) AS total_atomic_defense, 
                SUM(mental_attack) AS total_mental_attack, SUM(mental_defense) AS total_mental_defense, 
                SUM(speed) AS total_speed, SUM(critical_damage_rate) AS total_critical_damage_rate, 
                SUM(critical_rate) AS total_critical_rate, SUM(critical_resistance_rate) AS total_critical_resistance_rate,
                SUM(ignore_critical_rate) AS total_ignore_critical_rate,
                SUM(penetration_rate) AS total_penetration_rate, SUM(penetration_resistance_rate) AS total_penetration_resistance_rate, 
                SUM(evasion_rate) AS total_evasion_rate, SUM(damage_absorption_rate) AS total_damage_absorption_rate, 
                SUM(ignore_damage_absorption_rate) AS total_ignore_damage_absorption_rate, SUM(absorbed_damage_rate) AS total_absorbed_damage_rate, 
                SUM(vitality_regeneration_rate) AS total_vitality_regeneration_rate, SUM(vitality_regeneration_resistance_rate) AS total_vitality_regeneration_resistance_rate,
                SUM(accuracy_rate) AS total_accuracy_rate, 
                SUM(lifesteal_rate) AS total_lifesteal_rate, SUM(shield_strength) AS total_shield_strength, 
                SUM(tenacity) AS total_tenacity, SUM(resistance_rate) AS total_resistance_rate, 
                SUM(combo_rate) AS total_combo_rate, SUM(ignore_combo_rate) AS total_ignore_combo_rate, SUM(combo_damage_rate) AS total_combo_damage_rate, 
                SUM(combo_resistance_rate) AS total_combo_resistance_rate, SUM(stun_rate) AS total_stun_rate, SUM(ignore_stun_rate) AS total_ignore_stun_rate, 
                SUM(reflection_rate) AS total_reflection_rate, SUM(ignore_reflection_rate) AS total_ignore_reflection_rate, 
                SUM(reflection_damage_rate) AS total_reflection_damage_rate, SUM(reflection_resistance_rate) AS total_reflection_resistance_rate, 
                SUM(mana_regeneration_rate) AS total_mana_regeneration_rate, 
                SUM(damage_to_different_faction_rate) AS total_damage_to_different_faction_rate, 
                SUM(resistance_to_different_faction_rate) AS total_resistance_to_different_faction_rate, 
                SUM(damage_to_same_faction_rate) AS total_damage_to_same_faction_rate, 
                SUM(resistance_to_same_faction_rate) AS total_resistance_to_same_faction_rate, 
                SUM(normal_damage_rate) AS total_normal_damage_rate, SUM(normal_resistance_rate) AS total_normal_resistance_rate, 
                SUM(skill_damage_rate) AS total_skill_damage_rate, SUM(skill_resistance_rate) AS total_skill_resistance_rate, 
                SUM(percent_all_health) AS total_percent_all_health, 
                SUM(percent_all_physical_attack) AS total_percent_all_physical_attack, 
                SUM(percent_all_physical_defense) AS total_percent_all_physical_defense, 
                SUM(percent_all_magical_attack) AS total_percent_all_magical_attack, 
                SUM(percent_all_magical_defense) AS total_percent_all_magical_defense, 
                SUM(percent_all_chemical_attack) AS total_percent_all_chemical_attack, 
                SUM(percent_all_chemical_defense) AS total_percent_all_chemical_defense, 
                SUM(percent_all_atomic_attack) AS total_percent_all_atomic_attack, 
                SUM(percent_all_atomic_defense) AS total_percent_all_atomic_defense, 
                SUM(percent_all_mental_attack) AS total_percent_all_mental_attack, 
                SUM(percent_all_mental_defense) AS total_percent_all_mental_defense 
            FROM talisman_gallery 
            WHERE user_id = @user_id AND status = 'available';";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        sumTalisman.power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                        sumTalisman.health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                        sumTalisman.physical_attack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                        sumTalisman.physical_defense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                        sumTalisman.magical_attack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                        sumTalisman.magical_defense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                        sumTalisman.chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                        sumTalisman.chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                        sumTalisman.atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                        sumTalisman.atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                        sumTalisman.mental_attack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                        sumTalisman.mental_defense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                        sumTalisman.speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                        sumTalisman.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                        sumTalisman.critical_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                        sumTalisman.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                        sumTalisman.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                        sumTalisman.penetration_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                        sumTalisman.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                        sumTalisman.evasion_rate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                        sumTalisman.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                        sumTalisman.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                        sumTalisman.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                        sumTalisman.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                        sumTalisman.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                        sumTalisman.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                        sumTalisman.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                        sumTalisman.shield_strength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                        sumTalisman.tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                        sumTalisman.resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                        sumTalisman.combo_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                        sumTalisman.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                        sumTalisman.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                        sumTalisman.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                        sumTalisman.stun_rate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                        sumTalisman.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                        sumTalisman.reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                        sumTalisman.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                        sumTalisman.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                        sumTalisman.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                        sumTalisman.mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                        sumTalisman.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                        sumTalisman.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                        sumTalisman.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                        sumTalisman.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                        sumTalisman.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                        sumTalisman.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                        sumTalisman.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                        sumTalisman.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                        sumTalisman.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
                        sumTalisman.percent_all_health = reader.IsDBNull(reader.GetOrdinal("total_percent_all_health")) ? 0 : reader.GetDouble("total_percent_all_health");
                        sumTalisman.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_attack")) ? 0 : reader.GetDouble("total_percent_all_physical_attack");
                        sumTalisman.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_physical_defense")) ? 0 : reader.GetDouble("total_percent_all_physical_defense");
                        sumTalisman.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_attack")) ? 0 : reader.GetDouble("total_percent_all_magical_attack");
                        sumTalisman.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_magical_defense")) ? 0 : reader.GetDouble("total_percent_all_magical_defense");
                        sumTalisman.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_attack")) ? 0 : reader.GetDouble("total_percent_all_chemical_attack");
                        sumTalisman.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_chemical_defense")) ? 0 : reader.GetDouble("total_percent_all_chemical_defense");
                        sumTalisman.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_attack")) ? 0 : reader.GetDouble("total_percent_all_atomic_attack");
                        sumTalisman.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_atomic_defense")) ? 0 : reader.GetDouble("total_percent_all_atomic_defense");
                        sumTalisman.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_attack")) ? 0 : reader.GetDouble("total_percent_all_mental_attack");
                        sumTalisman.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("total_percent_all_mental_defense")) ? 0 : reader.GetDouble("total_percent_all_mental_defense");
                    }
                }

            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return sumTalisman;
    }
}