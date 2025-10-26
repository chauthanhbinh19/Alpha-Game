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
                command.Parameters.AddWithValue("@all_power", powerManager.Power);
                command.Parameters.AddWithValue("@all_health", powerManager.Health);
                command.Parameters.AddWithValue("@all_physical_attack", powerManager.PhysicalAttack);
                command.Parameters.AddWithValue("@all_physical_defense", powerManager.PhysicalDefense);
                command.Parameters.AddWithValue("@all_magical_attack", powerManager.MagicalAttack);
                command.Parameters.AddWithValue("@all_magical_defense", powerManager.MagicalDefense);
                command.Parameters.AddWithValue("@all_chemical_attack", powerManager.ChemicalAttack);
                command.Parameters.AddWithValue("@all_chemical_defense", powerManager.ChemicalDefense);
                command.Parameters.AddWithValue("@all_atomic_attack", powerManager.AtomicAttack);
                command.Parameters.AddWithValue("@all_atomic_defense", powerManager.AtomicDefense);
                command.Parameters.AddWithValue("@all_mental_attack", powerManager.MentalAttack);
                command.Parameters.AddWithValue("@all_mental_defense", powerManager.MentalDefense);
                command.Parameters.AddWithValue("@all_speed", powerManager.Speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", powerManager.CriticalDamageRate);
                command.Parameters.AddWithValue("@all_critical_rate", powerManager.CriticalRate);
                command.Parameters.AddWithValue("@all_critical_resistance_rate", powerManager.CriticalResistanceRate);
                command.Parameters.AddWithValue("@all_ignore_critical_rate", powerManager.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@all_penetration_rate", powerManager.PenetrationRate);
                command.Parameters.AddWithValue("@all_penetration_resistance_rate", powerManager.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@all_evasion_rate", powerManager.EvasionRate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", powerManager.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@all_ignore_damage_absorption_rate", powerManager.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@all_absorbed_damage_rate", powerManager.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", powerManager.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_resistance_rate", powerManager.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@all_accuracy_rate", powerManager.AccuracyRate);
                command.Parameters.AddWithValue("@all_lifesteal_rate", powerManager.LifestealRate);
                command.Parameters.AddWithValue("@all_shield_strength", powerManager.ShieldStrength);
                command.Parameters.AddWithValue("@all_tenacity", powerManager.Tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", powerManager.ResistanceRate);
                command.Parameters.AddWithValue("@all_combo_rate", powerManager.ComboRate);
                command.Parameters.AddWithValue("@all_ignore_combo_rate", powerManager.IgnoreComboRate);
                command.Parameters.AddWithValue("@all_combo_damage_rate", powerManager.ComboDamageRate);
                command.Parameters.AddWithValue("@all_combo_resistance_rate", powerManager.ComboResistanceRate);
                command.Parameters.AddWithValue("@all_stun_rate", powerManager.StunRate);
                command.Parameters.AddWithValue("@all_ignore_stun_rate", powerManager.IgnoreStunRate);
                command.Parameters.AddWithValue("@all_reflection_rate", powerManager.ReflectionRate);
                command.Parameters.AddWithValue("@all_ignore_reflection_rate", powerManager.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@all_reflection_damage_rate", powerManager.ReflectionDamageRate);
                command.Parameters.AddWithValue("@all_reflection_resistance_rate", powerManager.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@all_mana", powerManager.Mana);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", powerManager.ManaRegenerationRate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", powerManager.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", powerManager.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", powerManager.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", powerManager.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@all_normal_damage_rate", powerManager.NormalDamageRate);
                command.Parameters.AddWithValue("@all_normal_resistance_rate", powerManager.NormalResistanceRate);
                command.Parameters.AddWithValue("@all_skill_damage_rate", powerManager.SkillDamageRate);
                command.Parameters.AddWithValue("@all_skill_resistance_rate", powerManager.SkillResistanceRate);
                command.Parameters.AddWithValue("@percentAllHealth", powerManager.PercentAllHealth);
                command.Parameters.AddWithValue("@percentAllPhysicalAttack", powerManager.PercentAllPhysicalAttack);
                command.Parameters.AddWithValue("@percentAllPhysicalDefense", powerManager.PercentAllPhysicalDefense);
                command.Parameters.AddWithValue("@percentAllMagicalAttack", powerManager.PercentAllMagicalAttack);
                command.Parameters.AddWithValue("@percentAllMagicalDefense", powerManager.PercentAllMagicalDefense);
                command.Parameters.AddWithValue("@percentAllChemicalAttack", powerManager.PercentAllChemicalAttack);
                command.Parameters.AddWithValue("@percentAllChemicalDefense", powerManager.PercentAllChemicalDefense);
                command.Parameters.AddWithValue("@percentAllAtomicAttack", powerManager.PercentAllAtomicAttack);
                command.Parameters.AddWithValue("@percentAllAtomicDefense", powerManager.PercentAllAtomicDefense);
                command.Parameters.AddWithValue("@percentAllMentalAttack", powerManager.PercentAllMentalAttack);
                command.Parameters.AddWithValue("@percentAllMentalDefense", powerManager.PercentAllMentalDefense);
                command.ExecuteNonQuery();
                connection.Close();
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
                command.Parameters.AddWithValue("@all_power", powerManager.Power);
                command.Parameters.AddWithValue("@all_health", powerManager.Health);
                command.Parameters.AddWithValue("@all_physical_attack", powerManager.PhysicalAttack);
                command.Parameters.AddWithValue("@all_physical_defense", powerManager.PhysicalDefense);
                command.Parameters.AddWithValue("@all_magical_attack", powerManager.MagicalAttack);
                command.Parameters.AddWithValue("@all_magical_defense", powerManager.MagicalDefense);
                command.Parameters.AddWithValue("@all_chemical_attack", powerManager.ChemicalAttack);
                command.Parameters.AddWithValue("@all_chemical_defense", powerManager.ChemicalDefense);
                command.Parameters.AddWithValue("@all_atomic_attack", powerManager.AtomicAttack);
                command.Parameters.AddWithValue("@all_atomic_defense", powerManager.AtomicDefense);
                command.Parameters.AddWithValue("@all_mental_attack", powerManager.MentalAttack);
                command.Parameters.AddWithValue("@all_mental_defense", powerManager.MentalDefense);
                command.Parameters.AddWithValue("@all_speed", powerManager.Speed);
                command.Parameters.AddWithValue("@all_critical_damage_rate", powerManager.CriticalDamageRate);
                command.Parameters.AddWithValue("@all_critical_rate", powerManager.CriticalRate);
                command.Parameters.AddWithValue("@all_critical_resistance_rate", powerManager.CriticalResistanceRate);
                command.Parameters.AddWithValue("@all_ignore_critical_rate", powerManager.IgnoreCriticalRate);
                command.Parameters.AddWithValue("@all_penetration_rate", powerManager.PenetrationRate);
                command.Parameters.AddWithValue("@all_penetration_resistance_rate", powerManager.PenetrationResistanceRate);
                command.Parameters.AddWithValue("@all_evasion_rate", powerManager.EvasionRate);
                command.Parameters.AddWithValue("@all_damage_absorption_rate", powerManager.DamageAbsorptionRate);
                command.Parameters.AddWithValue("@all_ignore_damage_absorption_rate", powerManager.IgnoreDamageAbsorptionRate);
                command.Parameters.AddWithValue("@all_absorbed_damage_rate", powerManager.AbsorbedDamageRate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_rate", powerManager.VitalityRegenerationRate);
                command.Parameters.AddWithValue("@all_vitality_regeneration_resistance_rate", powerManager.VitalityRegenerationResistanceRate);
                command.Parameters.AddWithValue("@all_accuracy_rate", powerManager.AccuracyRate);
                command.Parameters.AddWithValue("@all_lifesteal_rate", powerManager.LifestealRate);
                command.Parameters.AddWithValue("@all_shield_strength", powerManager.ShieldStrength);
                command.Parameters.AddWithValue("@all_tenacity", powerManager.Tenacity);
                command.Parameters.AddWithValue("@all_resistance_rate", powerManager.ResistanceRate);
                command.Parameters.AddWithValue("@all_combo_rate", powerManager.ComboRate);
                command.Parameters.AddWithValue("@all_ignore_combo_rate", powerManager.IgnoreComboRate);
                command.Parameters.AddWithValue("@all_combo_damage_rate", powerManager.ComboDamageRate);
                command.Parameters.AddWithValue("@all_combo_resistance_rate", powerManager.ComboResistanceRate);
                command.Parameters.AddWithValue("@all_stun_rate", powerManager.StunRate);
                command.Parameters.AddWithValue("@all_ignore_stun_rate", powerManager.IgnoreStunRate);
                command.Parameters.AddWithValue("@all_reflection_rate", powerManager.ReflectionRate);
                command.Parameters.AddWithValue("@all_ignore_reflection_rate", powerManager.IgnoreReflectionRate);
                command.Parameters.AddWithValue("@all_reflection_damage_rate", powerManager.ReflectionDamageRate);
                command.Parameters.AddWithValue("@all_reflection_resistance_rate", powerManager.ReflectionResistanceRate);
                command.Parameters.AddWithValue("@all_mana", powerManager.Mana);
                command.Parameters.AddWithValue("@all_mana_regeneration_rate", powerManager.ManaRegenerationRate);
                command.Parameters.AddWithValue("@all_damage_to_different_faction_rate", powerManager.DamageToDifferentFactionRate);
                command.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", powerManager.ResistanceToDifferentFactionRate);
                command.Parameters.AddWithValue("@all_damage_to_same_faction_rate", powerManager.DamageToSameFactionRate);
                command.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", powerManager.ResistanceToSameFactionRate);
                command.Parameters.AddWithValue("@all_normal_damage_rate", powerManager.NormalDamageRate);
                command.Parameters.AddWithValue("@all_normal_resistance_rate", powerManager.NormalResistanceRate);
                command.Parameters.AddWithValue("@all_skill_damage_rate", powerManager.SkillDamageRate);
                command.Parameters.AddWithValue("@all_skill_resistance_rate", powerManager.SkillResistanceRate);
                command.Parameters.AddWithValue("@percentAllHealth", powerManager.PercentAllHealth);
                command.Parameters.AddWithValue("@percentAllPhysicalAttack", powerManager.PercentAllPhysicalAttack);
                command.Parameters.AddWithValue("@percentAllPhysicalDefense", powerManager.PercentAllPhysicalDefense);
                command.Parameters.AddWithValue("@percentAllMagicalAttack", powerManager.PercentAllMagicalAttack);
                command.Parameters.AddWithValue("@percentAllMagicalDefense", powerManager.PercentAllMagicalDefense);
                command.Parameters.AddWithValue("@percentAllChemicalAttack", powerManager.PercentAllChemicalAttack);
                command.Parameters.AddWithValue("@percentAllChemicalDefense", powerManager.PercentAllChemicalDefense);
                command.Parameters.AddWithValue("@percentAllAtomicAttack", powerManager.PercentAllAtomicAttack);
                command.Parameters.AddWithValue("@percentAllAtomicDefense", powerManager.PercentAllAtomicDefense);
                command.Parameters.AddWithValue("@percentAllMentalAttack", powerManager.PercentAllMentalAttack);
                command.Parameters.AddWithValue("@percentAllMentalDefense", powerManager.PercentAllMentalDefense);

                command.ExecuteNonQuery();
                connection.Close();
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
                        powerManager.Power = reader.IsDBNull(reader.GetOrdinal("all_power")) ? 0 : reader.GetDouble("all_power");
                        powerManager.Health = reader.IsDBNull(reader.GetOrdinal("all_health")) ? 0 : reader.GetDouble("all_health");
                        powerManager.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("all_physical_attack")) ? 0 : reader.GetDouble("all_physical_attack");
                        powerManager.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("all_physical_defense")) ? 0 : reader.GetDouble("all_physical_defense");
                        powerManager.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("all_magical_attack")) ? 0 : reader.GetDouble("all_magical_attack");
                        powerManager.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("all_magical_defense")) ? 0 : reader.GetDouble("all_magical_defense");
                        powerManager.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("all_chemical_attack")) ? 0 : reader.GetDouble("all_chemical_attack");
                        powerManager.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("all_chemical_defense")) ? 0 : reader.GetDouble("all_chemical_defense");
                        powerManager.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("all_atomic_attack")) ? 0 : reader.GetDouble("all_atomic_attack");
                        powerManager.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("all_atomic_defense")) ? 0 : reader.GetDouble("all_atomic_defense");
                        powerManager.MentalAttack = reader.IsDBNull(reader.GetOrdinal("all_mental_attack")) ? 0 : reader.GetDouble("all_mental_attack");
                        powerManager.MentalDefense = reader.IsDBNull(reader.GetOrdinal("all_mental_defense")) ? 0 : reader.GetDouble("all_mental_defense");
                        powerManager.Speed = reader.IsDBNull(reader.GetOrdinal("all_speed")) ? 0 : reader.GetDouble("all_speed");
                        powerManager.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("all_critical_damage_rate")) ? 0 : reader.GetDouble("all_critical_damage_rate");
                        powerManager.CriticalRate = reader.IsDBNull(reader.GetOrdinal("all_critical_rate")) ? 0 : reader.GetDouble("all_critical_rate");
                        powerManager.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_critical_resistance_rate")) ? 0 : reader.GetDouble("all_critical_resistance_rate");
                        powerManager.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("all_ignore_critical_rate")) ? 0 : reader.GetDouble("all_ignore_critical_rate");
                        powerManager.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("all_penetration_rate")) ? 0 : reader.GetDouble("all_penetration_rate");
                        powerManager.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_penetration_resistance_rate")) ? 0 : reader.GetDouble("all_penetration_resistance_rate");
                        powerManager.EvasionRate = reader.IsDBNull(reader.GetOrdinal("all_evasion_rate")) ? 0 : reader.GetDouble("all_evasion_rate");
                        powerManager.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("all_damage_absorption_rate")) ? 0 : reader.GetDouble("all_damage_absorption_rate");
                        powerManager.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("all_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("all_ignore_damage_absorption_rate");
                        powerManager.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("all_absorbed_damage_rate")) ? 0 : reader.GetDouble("all_absorbed_damage_rate");
                        powerManager.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("all_vitality_regeneration_rate")) ? 0 : reader.GetDouble("all_vitality_regeneration_rate");
                        powerManager.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("all_vitality_regeneration_resistance_rate");
                        powerManager.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("all_accuracy_rate")) ? 0 : reader.GetDouble("all_accuracy_rate");
                        powerManager.LifestealRate = reader.IsDBNull(reader.GetOrdinal("all_lifesteal_rate")) ? 0 : reader.GetDouble("all_lifesteal_rate");
                        powerManager.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("all_shield_strength")) ? 0 : reader.GetDouble("all_shield_strength");
                        powerManager.Tenacity = reader.IsDBNull(reader.GetOrdinal("all_tenacity")) ? 0 : reader.GetDouble("all_tenacity");
                        powerManager.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_resistance_rate")) ? 0 : reader.GetDouble("all_resistance_rate");
                        powerManager.ComboRate = reader.IsDBNull(reader.GetOrdinal("all_combo_rate")) ? 0 : reader.GetDouble("all_combo_rate");
                        powerManager.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("all_ignore_combo_rate")) ? 0 : reader.GetDouble("all_ignore_combo_rate");
                        powerManager.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("all_combo_damage_rate")) ? 0 : reader.GetDouble("all_combo_damage_rate");
                        powerManager.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_combo_resistance_rate")) ? 0 : reader.GetDouble("all_combo_resistance_rate");
                        powerManager.StunRate = reader.IsDBNull(reader.GetOrdinal("all_stun_rate")) ? 0 : reader.GetDouble("all_stun_rate");
                        powerManager.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("all_ignore_stun_rate")) ? 0 : reader.GetDouble("all_ignore_stun_rate");
                        powerManager.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("all_reflection_rate")) ? 0 : reader.GetDouble("all_reflection_rate");
                        powerManager.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("all_ignore_reflection_rate")) ? 0 : reader.GetDouble("all_ignore_reflection_rate");
                        powerManager.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("all_reflection_damage_rate")) ? 0 : reader.GetDouble("all_reflection_damage_rate");
                        powerManager.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_reflection_resistance_rate")) ? 0 : reader.GetDouble("all_reflection_resistance_rate");
                        powerManager.Mana = reader.IsDBNull(reader.GetOrdinal("all_mana")) ? 0 : reader.GetFloat("all_mana");
                        powerManager.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("all_mana_regeneration_rate")) ? 0 : reader.GetDouble("all_mana_regeneration_rate");
                        powerManager.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("all_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("all_damage_to_different_faction_rate");
                        powerManager.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("all_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("all_resistance_to_different_faction_rate");
                        powerManager.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("all_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("all_damage_to_same_faction_rate");
                        powerManager.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("all_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("all_resistance_to_same_faction_rate");
                        powerManager.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("all_normal_damage_rate")) ? 0 : reader.GetDouble("all_normal_damage_rate");
                        powerManager.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_normal_resistance_rate")) ? 0 : reader.GetDouble("all_normal_resistance_rate");
                        powerManager.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("all_skill_damage_rate")) ? 0 : reader.GetDouble("all_skill_damage_rate");
                        powerManager.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_skill_resistance_rate")) ? 0 : reader.GetDouble("all_skill_resistance_rate");
                        powerManager.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDouble("percent_all_health");
                        powerManager.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDouble("percent_all_physical_attack");
                        powerManager.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDouble("percent_all_physical_defense");
                        powerManager.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDouble("percent_all_magical_attack");
                        powerManager.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDouble("percent_all_magical_defense");
                        powerManager.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDouble("percent_all_chemical_attack");
                        powerManager.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDouble("percent_all_chemical_defense");
                        powerManager.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDouble("percent_all_atomic_attack");
                        powerManager.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDouble("percent_all_atomic_defense");
                        powerManager.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDouble("percent_all_mental_attack");
                        powerManager.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDouble("percent_all_mental_defense");
                    }
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return powerManager;
    }
}