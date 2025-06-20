using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
public class PowerManagerRepository : IPowerManagerRepository
{
    public void InsertUserStats(string user_id, PowerManager powerManager)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"INSERT INTO user_stats (
                        user_id, all_power, all_health, all_physical_attack, all_physical_defense,
                        all_magical_attack, all_magical_defense, all_chemical_attack, all_chemical_defense,
                        all_atomic_attack, all_atomic_defense, all_mental_attack, all_mental_defense,
                        all_speed, all_critical_damage_rate, all_critical_rate, all_critical_resistance_rate, all_ignore_critical_rate,
                        all_penetration_rate, all_penetration_resistance_rate, 
                        all_evasion_rate, all_damage_absorption_rate, all_ignore_damage_absorption_rate, all_absorbed_damage_rate,
                        all_vitality_regeneration_rate, all_vitality_regeneration_resistance_rate, all_accuracy_rate, 
                        all_lifesteal_rate, all_shield_strength, all_tenacity, all_resistance_rate, 
                        all_combo_rate, all_ignore_combo_rate, all_combo_damage_rate, all_combo_resistance_rate, 
                        all_stun_rate, all_ignore_stun_rate,
                        all_reflection_rate, all_ignore_reflection_rate, all_reflection_damage_rate, all_reflection_resistance_rate,
                        all_mana, all_mana_regeneration_rate, all_damage_to_different_faction_rate, 
                        all_resistance_to_different_faction_rate, all_damage_to_same_faction_rate, all_resistance_to_same_faction_rate,
                        all_normal_damage_rate, all_normal_resistance_rate, 
                        all_skill_damage_rate, all_skill_resistance_rate,
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense,
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack,
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense,
                        percent_all_mental_attack, percent_all_mental_defense
                    )
                    VALUES (
                        @userId, @all_power, @all_health, @all_physical_attack, @all_physical_defense,
                        @all_magical_attack, @all_magical_defense, @all_chemical_attack, @all_chemical_defense,
                        @all_atomic_attack, @all_atomic_defense, @all_mental_attack, @all_mental_defense,
                        @all_speed, @all_critical_damage_rate, @all_critical_rate, @all_critical_resistance_rate, @all_ignore_critical_rate,
                        @all_penetration_rate, @all_penetration_resistance_rate, 
                        @all_evasion_rate, @all_damage_absorption_rate, @all_ignore_damage_absorption_rate, @all_absorbed_damage_rate,
                        @all_vitality_regeneration_rate, @all_vitality_regeneration_resistance_rate, @all_accuracy_rate, 
                        @all_lifesteal_rate, @all_shield_strength, @all_tenacity, @all_resistance_rate, 
                        @all_combo_rate, @all_ignore_combo_rate, @all_combo_damage_rate, @all_combo_resistance_rate, 
                        @all_stun_rate, @all_ignore_stun_rate,
                        @all_reflection_rate, @all_ignore_reflection_rate, @all_reflection_damage_rate, @all_reflection_resistance_rate,
                        @all_mana, @all_mana_regeneration_rate, @all_damage_to_different_faction_rate, 
                        @all_resistance_to_different_faction_rate, @all_damage_to_same_faction_rate, @all_resistance_to_same_faction_rate,
                        @all_normal_damage_rate, @all_normal_resistance_rate,
                        @all_skill_damage_rate, @all_skill_resistance_rate,
                        @percentAllHealth, @percentAllPhysicalAttack, @percentAllPhysicalDefense,
                        @percentAllMagicalAttack, @percentAllMagicalDefense, @percentAllChemicalAttack,
                        @percentAllChemicalDefense, @percentAllAtomicAttack, @percentAllAtomicDefense,
                        @percentAllMentalAttack, @percentAllMentalDefense
                    );";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@all_power", powerManager.power);
                command.Parameters.AddWithValue("@all_health", powerManager.health);
                command.Parameters.AddWithValue("@all_physical_attack", powerManager.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", powerManager.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", powerManager.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", powerManager.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", powerManager.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", powerManager.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", powerManager.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", powerManager.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", powerManager.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", powerManager.mental_defense);
                command.Parameters.AddWithValue("@all_speed", powerManager.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", powerManager.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", powerManager.critical_rate);
                command.Parameters.AddWithValue("@all_critical_resistance_rate", powerManager.critical_resistance_rate);
                command.Parameters.AddWithValue("@all_ignore_critical_rate", powerManager.ignore_critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", powerManager.penetration_rate);
                command.Parameters.AddWithValue("@all_penetration_resistance_rate", powerManager.penetration_resistance_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", powerManager.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", powerManager.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_ignore_damage_absorption_rate", powerManager.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@all_absorbed_damage_rate", powerManager.absorbed_damage_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", powerManager.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_resistance_rate", powerManager.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", powerManager.accuracy_rate);
                command.Parameters.AddWithValue("@all_lifesteal_rate", powerManager.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", powerManager.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", powerManager.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", powerManager.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", powerManager.combo_rate);
                command.Parameters.AddWithValue("@all_ignore_combo_rate", powerManager.ignore_combo_rate);
                command.Parameters.AddWithValue("@all_combo_damage_rate", powerManager.combo_damage_rate);
                command.Parameters.AddWithValue("@all_combo_resistance_rate", powerManager.combo_resistance_rate);
                command.Parameters.AddWithValue("@all_stun_rate", powerManager.stun_rate);
                command.Parameters.AddWithValue("@all_ignore_stun_rate", powerManager.ignore_stun_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", powerManager.reflection_rate);
                command.Parameters.AddWithValue("@all_ignore_reflection_rate", powerManager.ignore_reflection_rate);
                command.Parameters.AddWithValue("@all_reflection_damage_rate", powerManager.reflection_damage_rate);
                command.Parameters.AddWithValue("@all_reflection_resistance_rate", powerManager.reflection_resistance_rate);
                command.Parameters.AddWithValue("@all_mana", powerManager.mana);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", powerManager.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", powerManager.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", powerManager.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", powerManager.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", powerManager.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_normal_damage_rate", powerManager.normal_damage_rate);
                command.Parameters.AddWithValue("@all_normal_resistance_rate", powerManager.normal_resistance_rate);
                command.Parameters.AddWithValue("@all_skill_damage_rate", powerManager.skill_damage_rate);
                command.Parameters.AddWithValue("@all_skill_resistance_rate", powerManager.skill_resistance_rate);
                command.Parameters.AddWithValue("@percentAllHealth", powerManager.percent_all_health);
                command.Parameters.AddWithValue("@percentAllPhysicalAttack", powerManager.percent_all_physical_attack);
                command.Parameters.AddWithValue("@percentAllPhysicalDefense", powerManager.percent_all_physical_defense);
                command.Parameters.AddWithValue("@percentAllMagicalAttack", powerManager.percent_all_magical_attack);
                command.Parameters.AddWithValue("@percentAllMagicalDefense", powerManager.percent_all_magical_defense);
                command.Parameters.AddWithValue("@percentAllChemicalAttack", powerManager.percent_all_chemical_attack);
                command.Parameters.AddWithValue("@percentAllChemicalDefense", powerManager.percent_all_chemical_defense);
                command.Parameters.AddWithValue("@percentAllAtomicAttack", powerManager.percent_all_atomic_attack);
                command.Parameters.AddWithValue("@percentAllAtomicDefense", powerManager.percent_all_atomic_defense);
                command.Parameters.AddWithValue("@percentAllMentalAttack", powerManager.percent_all_mental_attack);
                command.Parameters.AddWithValue("@percentAllMentalDefense", powerManager.percent_all_mental_defense);
                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public void UpdateUserStats(string user_id, PowerManager powerManager)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"UPDATE user_stats
                SET 
                    all_power = @all_power, all_health = @all_health, 
                    all_physical_attack = @all_physical_attack, all_physical_defense = @all_physical_defense, 
                    all_magical_attack = @all_magical_attack, all_magical_defense = @all_magical_defense, 
                    all_chemical_attack = @all_chemical_attack, all_chemical_defense = @all_chemical_defense, 
                    all_atomic_attack = @all_atomic_attack, all_atomic_defense = @all_atomic_defense, 
                    all_mental_attack = @all_mental_attack, all_mental_defense = @all_mental_defense, 
                    all_speed = @all_speed, all_critical_damage_rate = @all_critical_damage_rate, 
                    all_critical_rate = @all_critical_rate, all_critical_resistance_rate = @all_critical_resistance_rate, all_ignore_critical_rate = @all_ignore_critical_rate,
                    all_penetration_rate = @all_penetration_rate, all_penetration_resistance_rate = @all_penetration_resistance_rate,
                    all_evasion_rate = @all_evasion_rate, all_damage_absorption_rate = @all_damage_absorption_rate, 
                    all_ignore_damage_absorption_rate = @all_ignore_damage_absorption_rate, all_absorbed_damage_rate = @all_absorbed_damage_rate,
                    all_vitality_regeneration_rate = @all_vitality_regeneration_rate, all_vitality_regeneration_resistance_rate = @all_vitality_regeneration_resistance_rate, 
                    all_accuracy_rate = @all_accuracy_rate, all_lifesteal_rate = @all_lifesteal_rate, all_shield_strength = @all_shield_strength, 
                    all_tenacity = @all_tenacity, all_resistance_rate = @all_resistance_rate, 
                    all_combo_rate = @all_combo_rate, all_ignore_combo_rate = @all_ignore_combo_rate, all_combo_damage_rate = @all_combo_damage_rate, all_combo_resistance_rate = @all_combo_resistance_rate,
                    all_stun_rate = @all_stun_rate, all_ignore_stun_rate = @all_ignore_stun_rate,
                    all_reflection_rate = @all_reflection_rate, all_ignore_reflection_rate = @all_ignore_reflection_rate, 
                    all_reflection_damage_rate = @all_reflection_damage_rate, all_reflection_resistance_rate = @all_reflection_resistance_rate,
                    all_mana = @all_mana, all_mana_regeneration_rate = @all_mana_regeneration_rate, 
                    all_damage_to_different_faction_rate = @all_damage_to_different_faction_rate, 
                    all_resistance_to_different_faction_rate = @all_resistance_to_different_faction_rate, 
                    all_damage_to_same_faction_rate = @all_damage_to_same_faction_rate, 
                    all_resistance_to_same_faction_rate = @all_resistance_to_same_faction_rate,
                    all_normal_damage_rate = @all_normal_damage_rate, all_normal_resistance_rate = @all_normal_resistance_rate,
                    all_skill_damage_rate = @all_skill_damage_rate, all_skill_resistance_rate = @all_skill_resistance_rate,
                    percent_all_health = @percentAllHealth, 
                    percent_all_physical_attack = @percentAllPhysicalAttack, percent_all_physical_defense = @percentAllPhysicalDefense,
                    percent_all_magical_attack = @percentAllMagicalAttack, percent_all_magical_defense = @percentAllMagicalDefense, 
                    percent_all_chemical_attack = @percentAllChemicalAttack, percent_all_chemical_defense = @percentAllChemicalDefense, 
                    percent_all_atomic_attack = @percentAllAtomicAttack, percent_all_atomic_defense = @percentAllAtomicDefense,
                    percent_all_mental_attack = @percentAllMentalAttack, percent_all_mental_defense = @percentAllMentalDefense
                WHERE user_id = @userId;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", user_id);
                command.Parameters.AddWithValue("@all_power", powerManager.power);
                command.Parameters.AddWithValue("@all_health", powerManager.health);
                command.Parameters.AddWithValue("@all_physical_attack", powerManager.physical_attack);
                command.Parameters.AddWithValue("@all_physical_defense", powerManager.physical_defense);
                command.Parameters.AddWithValue("@all_magical_attack", powerManager.magical_attack);
                command.Parameters.AddWithValue("@all_magical_defense", powerManager.magical_defense);
                command.Parameters.AddWithValue("@all_chemical_attack", powerManager.chemical_attack);
                command.Parameters.AddWithValue("@all_chemical_defense", powerManager.chemical_defense);
                command.Parameters.AddWithValue("@all_atomic_attack", powerManager.atomic_attack);
                command.Parameters.AddWithValue("@all_atomic_defense", powerManager.atomic_defense);
                command.Parameters.AddWithValue("@all_mental_attack", powerManager.mental_attack);
                command.Parameters.AddWithValue("@all_mental_defense", powerManager.mental_defense);
                command.Parameters.AddWithValue("@all_speed", powerManager.speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", powerManager.critical_damage_rate);
                command.Parameters.AddWithValue("@all_critical_rate", powerManager.critical_rate);
                command.Parameters.AddWithValue("@all_critical_resistance_rate", powerManager.critical_resistance_rate);
                command.Parameters.AddWithValue("@all_ignore_critical_rate", powerManager.ignore_critical_rate);
                command.Parameters.AddWithValue("@all_penetration_rate", powerManager.penetration_rate);
                command.Parameters.AddWithValue("@all_penetration_resistance_rate", powerManager.penetration_resistance_rate);
                command.Parameters.AddWithValue("@all_evasion_rate", powerManager.evasion_rate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", powerManager.damage_absorption_rate);
                command.Parameters.AddWithValue("@all_ignore_damage_absorption_rate", powerManager.ignore_damage_absorption_rate);
                command.Parameters.AddWithValue("@all_absorbed_damage_rate", powerManager.absorbed_damage_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", powerManager.vitality_regeneration_rate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_resistance_rate", powerManager.vitality_regeneration_resistance_rate);
                command.Parameters.AddWithValue("@all_accuracy_rate", powerManager.accuracy_rate);
                command.Parameters.AddWithValue("@all_lifesteal_rate", powerManager.lifesteal_rate);
                command.Parameters.AddWithValue("@all_shield_strength", powerManager.shield_strength);
                command.Parameters.AddWithValue("@all_tenacity", powerManager.tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", powerManager.resistance_rate);
                command.Parameters.AddWithValue("@all_combo_rate", powerManager.combo_rate);
                command.Parameters.AddWithValue("@all_ignore_combo_rate", powerManager.ignore_combo_rate);
                command.Parameters.AddWithValue("@all_combo_damage_rate", powerManager.combo_damage_rate);
                command.Parameters.AddWithValue("@all_combo_resistance_rate", powerManager.combo_resistance_rate);
                command.Parameters.AddWithValue("@all_stun_rate", powerManager.stun_rate);
                command.Parameters.AddWithValue("@all_ignore_stun_rate", powerManager.ignore_stun_rate);
                command.Parameters.AddWithValue("@all_reflection_rate", powerManager.reflection_rate);
                command.Parameters.AddWithValue("@all_ignore_reflection_rate", powerManager.ignore_reflection_rate);
                command.Parameters.AddWithValue("@all_reflection_damage_rate", powerManager.reflection_damage_rate);
                command.Parameters.AddWithValue("@all_reflection_resistance_rate", powerManager.reflection_resistance_rate);
                command.Parameters.AddWithValue("@all_mana", powerManager.mana);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", powerManager.mana_regeneration_rate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", powerManager.damage_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", powerManager.resistance_to_different_faction_rate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", powerManager.damage_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", powerManager.resistance_to_same_faction_rate);
                command.Parameters.AddWithValue("@all_normal_damage_rate", powerManager.normal_damage_rate);
                command.Parameters.AddWithValue("@all_normal_resistance_rate", powerManager.normal_resistance_rate);
                command.Parameters.AddWithValue("@all_skill_damage_rate", powerManager.skill_damage_rate);
                command.Parameters.AddWithValue("@all_skill_resistance_rate", powerManager.skill_resistance_rate);
                command.Parameters.AddWithValue("@percentAllHealth", powerManager.percent_all_health);
                command.Parameters.AddWithValue("@percentAllPhysicalAttack", powerManager.percent_all_physical_attack);
                command.Parameters.AddWithValue("@percentAllPhysicalDefense", powerManager.percent_all_physical_defense);
                command.Parameters.AddWithValue("@percentAllMagicalAttack", powerManager.percent_all_magical_attack);
                command.Parameters.AddWithValue("@percentAllMagicalDefense", powerManager.percent_all_magical_defense);
                command.Parameters.AddWithValue("@percentAllChemicalAttack", powerManager.percent_all_chemical_attack);
                command.Parameters.AddWithValue("@percentAllChemicalDefense", powerManager.percent_all_chemical_defense);
                command.Parameters.AddWithValue("@percentAllAtomicAttack", powerManager.percent_all_atomic_attack);
                command.Parameters.AddWithValue("@percentAllAtomicDefense", powerManager.percent_all_atomic_defense);
                command.Parameters.AddWithValue("@percentAllMentalAttack", powerManager.percent_all_mental_attack);
                command.Parameters.AddWithValue("@percentAllMentalDefense", powerManager.percent_all_mental_defense);

                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error Message: " + ex.Message);
                Debug.LogError("Error Code: " + ex.Number); // Mã lỗi MySQL (rất hữu ích)
                Debug.LogError("SQLState: " + ex.SqlState); // Chuẩn SQL state code
                Debug.LogError("Stack Trace: " + ex.StackTrace); // Xem lỗi nằm dòng nào
                Debug.LogError("Inner Exception: " + ex.InnerException); // Nếu có exception lồng nhau
            }
        }
    }
    public PowerManager GetUserStats(string user_id)
    {
        PowerManager powerManager = new PowerManager();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"
                SELECT * FROM USER_STATS WHERE USER_ID=@user_id;";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        powerManager.power = reader.IsDBNull(reader.GetOrdinal("all_power")) ? 0 : reader.GetDouble("all_power");
                        powerManager.health = reader.IsDBNull(reader.GetOrdinal("all_health")) ? 0 : reader.GetDouble("all_health");
                        powerManager.physical_attack = reader.IsDBNull(reader.GetOrdinal("all_physical_attack")) ? 0 : reader.GetDouble("all_physical_attack");
                        powerManager.physical_defense = reader.IsDBNull(reader.GetOrdinal("all_physical_defense")) ? 0 : reader.GetDouble("all_physical_defense");
                        powerManager.magical_attack = reader.IsDBNull(reader.GetOrdinal("all_magical_attack")) ? 0 : reader.GetDouble("all_magical_attack");
                        powerManager.magical_defense = reader.IsDBNull(reader.GetOrdinal("all_magical_defense")) ? 0 : reader.GetDouble("all_magical_defense");
                        powerManager.chemical_attack = reader.IsDBNull(reader.GetOrdinal("all_chemical_attack")) ? 0 : reader.GetDouble("all_chemical_attack");
                        powerManager.chemical_defense = reader.IsDBNull(reader.GetOrdinal("all_chemical_defense")) ? 0 : reader.GetDouble("all_chemical_defense");
                        powerManager.atomic_attack = reader.IsDBNull(reader.GetOrdinal("all_atomic_attack")) ? 0 : reader.GetDouble("all_atomic_attack");
                        powerManager.atomic_defense = reader.IsDBNull(reader.GetOrdinal("all_atomic_defense")) ? 0 : reader.GetDouble("all_atomic_defense");
                        powerManager.mental_attack = reader.IsDBNull(reader.GetOrdinal("all_mental_attack")) ? 0 : reader.GetDouble("all_mental_attack");
                        powerManager.mental_defense = reader.IsDBNull(reader.GetOrdinal("all_mental_defense")) ? 0 : reader.GetDouble("all_mental_defense");
                        powerManager.speed = reader.IsDBNull(reader.GetOrdinal("all_speed")) ? 0 : reader.GetDouble("all_speed");
                        powerManager.critical_damage_rate = reader.IsDBNull(reader.GetOrdinal("all_critical_damage_rate")) ? 0 : reader.GetDouble("all_critical_damage_rate");
                        powerManager.critical_rate = reader.IsDBNull(reader.GetOrdinal("all_critical_rate")) ? 0 : reader.GetDouble("all_critical_rate");
                        powerManager.critical_resistance_rate = reader.IsDBNull(reader.GetOrdinal("all_critical_resistance_rate")) ? 0 : reader.GetDouble("all_critical_resistance_rate");
                        powerManager.ignore_critical_rate = reader.IsDBNull(reader.GetOrdinal("all_ignore_critical_rate")) ? 0 : reader.GetDouble("all_ignore_critical_rate");
                        powerManager.penetration_rate = reader.IsDBNull(reader.GetOrdinal("all_penetration_rate")) ? 0 : reader.GetDouble("all_penetration_rate");
                        powerManager.penetration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("all_penetration_resistance_rate")) ? 0 : reader.GetDouble("all_penetration_resistance_rate");
                        powerManager.evasion_rate = reader.IsDBNull(reader.GetOrdinal("all_evasion_rate")) ? 0 : reader.GetDouble("all_evasion_rate");
                        powerManager.damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("all_damage_absorption_rate")) ? 0 : reader.GetDouble("all_damage_absorption_rate");
                        powerManager.ignore_damage_absorption_rate = reader.IsDBNull(reader.GetOrdinal("all_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("all_ignore_damage_absorption_rate");
                        powerManager.absorbed_damage_rate = reader.IsDBNull(reader.GetOrdinal("all_absorbed_damage_rate")) ? 0 : reader.GetDouble("all_absorbed_damage_rate");
                        powerManager.vitality_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("all_vitality_regeneration_rate")) ? 0 : reader.GetDouble("all_vitality_regeneration_rate");
                        powerManager.vitality_regeneration_resistance_rate = reader.IsDBNull(reader.GetOrdinal("all_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("all_vitality_regeneration_resistance_rate");
                        powerManager.accuracy_rate = reader.IsDBNull(reader.GetOrdinal("all_accuracy_rate")) ? 0 : reader.GetDouble("all_accuracy_rate");
                        powerManager.lifesteal_rate = reader.IsDBNull(reader.GetOrdinal("all_lifesteal_rate")) ? 0 : reader.GetDouble("all_lifesteal_rate");
                        powerManager.shield_strength = reader.IsDBNull(reader.GetOrdinal("all_shield_strength")) ? 0 : reader.GetDouble("all_shield_strength");
                        powerManager.tenacity = reader.IsDBNull(reader.GetOrdinal("all_tenacity")) ? 0 : reader.GetDouble("all_tenacity");
                        powerManager.resistance_rate = reader.IsDBNull(reader.GetOrdinal("all_resistance_rate")) ? 0 : reader.GetDouble("all_resistance_rate");
                        powerManager.combo_rate = reader.IsDBNull(reader.GetOrdinal("all_combo_rate")) ? 0 : reader.GetDouble("all_combo_rate");
                        powerManager.ignore_combo_rate = reader.IsDBNull(reader.GetOrdinal("all_ignore_combo_rate")) ? 0 : reader.GetDouble("all_ignore_combo_rate");
                        powerManager.combo_damage_rate = reader.IsDBNull(reader.GetOrdinal("all_combo_damage_rate")) ? 0 : reader.GetDouble("all_combo_damage_rate");
                        powerManager.combo_resistance_rate = reader.IsDBNull(reader.GetOrdinal("all_combo_resistance_rate")) ? 0 : reader.GetDouble("all_combo_resistance_rate");
                        powerManager.stun_rate = reader.IsDBNull(reader.GetOrdinal("all_stun_rate")) ? 0 : reader.GetDouble("all_stun_rate");
                        powerManager.ignore_stun_rate = reader.IsDBNull(reader.GetOrdinal("all_ignore_stun_rate")) ? 0 : reader.GetDouble("all_ignore_stun_rate");
                        powerManager.reflection_rate = reader.IsDBNull(reader.GetOrdinal("all_reflection_rate")) ? 0 : reader.GetDouble("all_reflection_rate");
                        powerManager.ignore_reflection_rate = reader.IsDBNull(reader.GetOrdinal("all_ignore_reflection_rate")) ? 0 : reader.GetDouble("all_ignore_reflection_rate");
                        powerManager.reflection_damage_rate = reader.IsDBNull(reader.GetOrdinal("all_reflection_damage_rate")) ? 0 : reader.GetDouble("all_reflection_damage_rate");
                        powerManager.reflection_resistance_rate = reader.IsDBNull(reader.GetOrdinal("all_reflection_resistance_rate")) ? 0 : reader.GetDouble("all_reflection_resistance_rate");
                        powerManager.mana = reader.IsDBNull(reader.GetOrdinal("all_mana")) ? 0 : reader.GetFloat("all_mana");
                        powerManager.mana_regeneration_rate = reader.IsDBNull(reader.GetOrdinal("all_mana_regeneration_rate")) ? 0 : reader.GetDouble("all_mana_regeneration_rate");
                        powerManager.damage_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("all_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("all_damage_to_different_faction_rate");
                        powerManager.resistance_to_different_faction_rate = reader.IsDBNull(reader.GetOrdinal("all_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("all_resistance_to_different_faction_rate");
                        powerManager.damage_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("all_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("all_damage_to_same_faction_rate");
                        powerManager.resistance_to_same_faction_rate = reader.IsDBNull(reader.GetOrdinal("all_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("all_resistance_to_same_faction_rate");
                        powerManager.normal_damage_rate = reader.IsDBNull(reader.GetOrdinal("all_normal_damage_rate")) ? 0 : reader.GetDouble("all_normal_damage_rate");
                        powerManager.normal_resistance_rate = reader.IsDBNull(reader.GetOrdinal("all_normal_resistance_rate")) ? 0 : reader.GetDouble("all_normal_resistance_rate");
                        powerManager.skill_damage_rate = reader.IsDBNull(reader.GetOrdinal("all_skill_damage_rate")) ? 0 : reader.GetDouble("all_skill_damage_rate");
                        powerManager.skill_resistance_rate = reader.IsDBNull(reader.GetOrdinal("all_skill_resistance_rate")) ? 0 : reader.GetDouble("all_skill_resistance_rate");
                        powerManager.percent_all_health = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDouble("percent_all_health");
                        powerManager.percent_all_physical_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDouble("percent_all_physical_attack");
                        powerManager.percent_all_physical_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDouble("percent_all_physical_defense");
                        powerManager.percent_all_magical_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDouble("percent_all_magical_attack");
                        powerManager.percent_all_magical_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDouble("percent_all_magical_defense");
                        powerManager.percent_all_chemical_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDouble("percent_all_chemical_attack");
                        powerManager.percent_all_chemical_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDouble("percent_all_chemical_defense");
                        powerManager.percent_all_atomic_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDouble("percent_all_atomic_attack");
                        powerManager.percent_all_atomic_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDouble("percent_all_atomic_defense");
                        powerManager.percent_all_mental_attack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDouble("percent_all_mental_attack");
                        powerManager.percent_all_mental_defense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDouble("percent_all_mental_defense");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return powerManager;
    }
}