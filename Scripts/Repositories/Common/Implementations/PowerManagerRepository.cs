using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class PowerManagerRepository : IPowerManagerRepository
{
    public async Task InsertUserStatsAsync(string user_id, PowerManager powerManager)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string insertSQL = @"INSERT INTO user_stats (
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

                using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
                {
                    insertCommand.Parameters.AddWithValue("@userId", user_id);
                    insertCommand.Parameters.AddWithValue("@all_power", powerManager.Power);
                    insertCommand.Parameters.AddWithValue("@all_health", powerManager.Health);
                    insertCommand.Parameters.AddWithValue("@all_physical_attack", powerManager.PhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@all_physical_defense", powerManager.PhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@all_magical_attack", powerManager.MagicalAttack);
                    insertCommand.Parameters.AddWithValue("@all_magical_defense", powerManager.MagicalDefense);
                    insertCommand.Parameters.AddWithValue("@all_chemical_attack", powerManager.ChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@all_chemical_defense", powerManager.ChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@all_atomic_attack", powerManager.AtomicAttack);
                    insertCommand.Parameters.AddWithValue("@all_atomic_defense", powerManager.AtomicDefense);
                    insertCommand.Parameters.AddWithValue("@all_mental_attack", powerManager.MentalAttack);
                    insertCommand.Parameters.AddWithValue("@all_mental_defense", powerManager.MentalDefense);
                    insertCommand.Parameters.AddWithValue("@all_speed", powerManager.Speed);
                    insertCommand.Parameters.AddWithValue("@all_critical_damage_rate", powerManager.CriticalDamageRate);
                    insertCommand.Parameters.AddWithValue("@all_critical_rate", powerManager.CriticalRate);
                    insertCommand.Parameters.AddWithValue("@all_critical_resistance_rate", powerManager.CriticalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@all_ignore_critical_rate", powerManager.IgnoreCriticalRate);
                    insertCommand.Parameters.AddWithValue("@all_penetration_rate", powerManager.PenetrationRate);
                    insertCommand.Parameters.AddWithValue("@all_penetration_resistance_rate", powerManager.PenetrationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@all_evasion_rate", powerManager.EvasionRate);
                    insertCommand.Parameters.AddWithValue("@all_damage_absorption_rate", powerManager.DamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@all_ignore_damage_absorption_rate", powerManager.IgnoreDamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@all_absorbed_damage_rate", powerManager.AbsorbedDamageRate);
                    insertCommand.Parameters.AddWithValue("@all_vitality_regeneration_rate", powerManager.VitalityRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@all_vitality_regeneration_resistance_rate", powerManager.VitalityRegenerationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@all_accuracy_rate", powerManager.AccuracyRate);
                    insertCommand.Parameters.AddWithValue("@all_lifesteal_rate", powerManager.LifestealRate);
                    insertCommand.Parameters.AddWithValue("@all_shield_strength", powerManager.ShieldStrength);
                    insertCommand.Parameters.AddWithValue("@all_tenacity", powerManager.Tenacity);
                    insertCommand.Parameters.AddWithValue("@all_resistance_rate", powerManager.ResistanceRate);
                    insertCommand.Parameters.AddWithValue("@all_combo_rate", powerManager.ComboRate);
                    insertCommand.Parameters.AddWithValue("@all_ignore_combo_rate", powerManager.IgnoreComboRate);
                    insertCommand.Parameters.AddWithValue("@all_combo_damage_rate", powerManager.ComboDamageRate);
                    insertCommand.Parameters.AddWithValue("@all_combo_resistance_rate", powerManager.ComboResistanceRate);
                    insertCommand.Parameters.AddWithValue("@all_stun_rate", powerManager.StunRate);
                    insertCommand.Parameters.AddWithValue("@all_ignore_stun_rate", powerManager.IgnoreStunRate);
                    insertCommand.Parameters.AddWithValue("@all_reflection_rate", powerManager.ReflectionRate);
                    insertCommand.Parameters.AddWithValue("@all_ignore_reflection_rate", powerManager.IgnoreReflectionRate);
                    insertCommand.Parameters.AddWithValue("@all_reflection_damage_rate", powerManager.ReflectionDamageRate);
                    insertCommand.Parameters.AddWithValue("@all_reflection_resistance_rate", powerManager.ReflectionResistanceRate);
                    insertCommand.Parameters.AddWithValue("@all_mana", powerManager.Mana);
                    insertCommand.Parameters.AddWithValue("@all_mana_regeneration_rate", powerManager.ManaRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@all_damage_to_different_faction_rate", powerManager.DamageToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", powerManager.ResistanceToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@all_damage_to_same_faction_rate", powerManager.DamageToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", powerManager.ResistanceToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@all_normal_damage_rate", powerManager.NormalDamageRate);
                    insertCommand.Parameters.AddWithValue("@all_normal_resistance_rate", powerManager.NormalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@all_skill_damage_rate", powerManager.SkillDamageRate);
                    insertCommand.Parameters.AddWithValue("@all_skill_resistance_rate", powerManager.SkillResistanceRate);
                    insertCommand.Parameters.AddWithValue("@percentAllHealth", powerManager.PercentAllHealth);
                    insertCommand.Parameters.AddWithValue("@percentAllPhysicalAttack", powerManager.PercentAllPhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@percentAllPhysicalDefense", powerManager.PercentAllPhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@percentAllMagicalAttack", powerManager.PercentAllMagicalAttack);
                    insertCommand.Parameters.AddWithValue("@percentAllMagicalDefense", powerManager.PercentAllMagicalDefense);
                    insertCommand.Parameters.AddWithValue("@percentAllChemicalAttack", powerManager.PercentAllChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@percentAllChemicalDefense", powerManager.PercentAllChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@percentAllAtomicAttack", powerManager.PercentAllAtomicAttack);
                    insertCommand.Parameters.AddWithValue("@percentAllAtomicDefense", powerManager.PercentAllAtomicDefense);
                    insertCommand.Parameters.AddWithValue("@percentAllMentalAttack", powerManager.PercentAllMentalAttack);
                    insertCommand.Parameters.AddWithValue("@percentAllMentalDefense", powerManager.PercentAllMentalDefense);

                    await insertCommand.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public async Task UpdateUserStatsAsync(string user_id, PowerManager powerManager)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"UPDATE user_stats
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

                using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@userId", user_id);
                    updateCommand.Parameters.AddWithValue("@all_power", powerManager.Power);
                    updateCommand.Parameters.AddWithValue("@all_health", powerManager.Health);
                    updateCommand.Parameters.AddWithValue("@all_physical_attack", powerManager.PhysicalAttack);
                    updateCommand.Parameters.AddWithValue("@all_physical_defense", powerManager.PhysicalDefense);
                    updateCommand.Parameters.AddWithValue("@all_magical_attack", powerManager.MagicalAttack);
                    updateCommand.Parameters.AddWithValue("@all_magical_defense", powerManager.MagicalDefense);
                    updateCommand.Parameters.AddWithValue("@all_chemical_attack", powerManager.ChemicalAttack);
                    updateCommand.Parameters.AddWithValue("@all_chemical_defense", powerManager.ChemicalDefense);
                    updateCommand.Parameters.AddWithValue("@all_atomic_attack", powerManager.AtomicAttack);
                    updateCommand.Parameters.AddWithValue("@all_atomic_defense", powerManager.AtomicDefense);
                    updateCommand.Parameters.AddWithValue("@all_mental_attack", powerManager.MentalAttack);
                    updateCommand.Parameters.AddWithValue("@all_mental_defense", powerManager.MentalDefense);
                    updateCommand.Parameters.AddWithValue("@all_speed", powerManager.Speed);
                    updateCommand.Parameters.AddWithValue("@all_critical_damage_rate", powerManager.CriticalDamageRate);
                    updateCommand.Parameters.AddWithValue("@all_critical_rate", powerManager.CriticalRate);
                    updateCommand.Parameters.AddWithValue("@all_critical_resistance_rate", powerManager.CriticalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@all_ignore_critical_rate", powerManager.IgnoreCriticalRate);
                    updateCommand.Parameters.AddWithValue("@all_penetration_rate", powerManager.PenetrationRate);
                    updateCommand.Parameters.AddWithValue("@all_penetration_resistance_rate", powerManager.PenetrationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@all_evasion_rate", powerManager.EvasionRate);
                    updateCommand.Parameters.AddWithValue("@all_damage_absorption_rate", powerManager.DamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@all_ignore_damage_absorption_rate", powerManager.IgnoreDamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@all_absorbed_damage_rate", powerManager.AbsorbedDamageRate);
                    updateCommand.Parameters.AddWithValue("@all_vitality_regeneration_rate", powerManager.VitalityRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@all_vitality_regeneration_resistance_rate", powerManager.VitalityRegenerationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@all_accuracy_rate", powerManager.AccuracyRate);
                    updateCommand.Parameters.AddWithValue("@all_lifesteal_rate", powerManager.LifestealRate);
                    updateCommand.Parameters.AddWithValue("@all_shield_strength", powerManager.ShieldStrength);
                    updateCommand.Parameters.AddWithValue("@all_tenacity", powerManager.Tenacity);
                    updateCommand.Parameters.AddWithValue("@all_resistance_rate", powerManager.ResistanceRate);
                    updateCommand.Parameters.AddWithValue("@all_combo_rate", powerManager.ComboRate);
                    updateCommand.Parameters.AddWithValue("@all_ignore_combo_rate", powerManager.IgnoreComboRate);
                    updateCommand.Parameters.AddWithValue("@all_combo_damage_rate", powerManager.ComboDamageRate);
                    updateCommand.Parameters.AddWithValue("@all_combo_resistance_rate", powerManager.ComboResistanceRate);
                    updateCommand.Parameters.AddWithValue("@all_stun_rate", powerManager.StunRate);
                    updateCommand.Parameters.AddWithValue("@all_ignore_stun_rate", powerManager.IgnoreStunRate);
                    updateCommand.Parameters.AddWithValue("@all_reflection_rate", powerManager.ReflectionRate);
                    updateCommand.Parameters.AddWithValue("@all_ignore_reflection_rate", powerManager.IgnoreReflectionRate);
                    updateCommand.Parameters.AddWithValue("@all_reflection_damage_rate", powerManager.ReflectionDamageRate);
                    updateCommand.Parameters.AddWithValue("@all_reflection_resistance_rate", powerManager.ReflectionResistanceRate);
                    updateCommand.Parameters.AddWithValue("@all_mana", powerManager.Mana);
                    updateCommand.Parameters.AddWithValue("@all_mana_regeneration_rate", powerManager.ManaRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@all_damage_to_different_faction_rate", powerManager.DamageToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@all_resistance_to_different_faction_rate", powerManager.ResistanceToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@all_damage_to_same_faction_rate", powerManager.DamageToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@all_resistance_to_same_faction_rate", powerManager.ResistanceToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@all_normal_damage_rate", powerManager.NormalDamageRate);
                    updateCommand.Parameters.AddWithValue("@all_normal_resistance_rate", powerManager.NormalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@all_skill_damage_rate", powerManager.SkillDamageRate);
                    updateCommand.Parameters.AddWithValue("@all_skill_resistance_rate", powerManager.SkillResistanceRate);
                    updateCommand.Parameters.AddWithValue("@percentAllHealth", powerManager.PercentAllHealth);
                    updateCommand.Parameters.AddWithValue("@percentAllPhysicalAttack", powerManager.PercentAllPhysicalAttack);
                    updateCommand.Parameters.AddWithValue("@percentAllPhysicalDefense", powerManager.PercentAllPhysicalDefense);
                    updateCommand.Parameters.AddWithValue("@percentAllMagicalAttack", powerManager.PercentAllMagicalAttack);
                    updateCommand.Parameters.AddWithValue("@percentAllMagicalDefense", powerManager.PercentAllMagicalDefense);
                    updateCommand.Parameters.AddWithValue("@percentAllChemicalAttack", powerManager.PercentAllChemicalAttack);
                    updateCommand.Parameters.AddWithValue("@percentAllChemicalDefense", powerManager.PercentAllChemicalDefense);
                    updateCommand.Parameters.AddWithValue("@percentAllAtomicAttack", powerManager.PercentAllAtomicAttack);
                    updateCommand.Parameters.AddWithValue("@percentAllAtomicDefense", powerManager.PercentAllAtomicDefense);
                    updateCommand.Parameters.AddWithValue("@percentAllMentalAttack", powerManager.PercentAllMentalAttack);
                    updateCommand.Parameters.AddWithValue("@percentAllMentalDefense", powerManager.PercentAllMentalDefense);

                    await updateCommand.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error Message: " + ex.Message);
                Debug.LogError("Error Code: " + ex.Number);
                Debug.LogError("SQLState: " + ex.SqlState);
                Debug.LogError("Stack Trace: " + ex.StackTrace);
                Debug.LogError("Inner Exception: " + ex.InnerException);
            }
        }
    }
    public async Task<PowerManager> GetUserStatsAsync(string user_id)
    {
        PowerManager powerManager = new PowerManager();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = "SELECT * FROM USER_STATS WHERE USER_ID=@user_id;";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            powerManager.Power = reader.IsDBNull(reader.GetOrdinal("all_power")) ? 0 : reader.GetDoubleSafe("all_power");
                            powerManager.Health = reader.IsDBNull(reader.GetOrdinal("all_health")) ? 0 : reader.GetDoubleSafe("all_health");
                            powerManager.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("all_physical_attack")) ? 0 : reader.GetDoubleSafe("all_physical_attack");
                            powerManager.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("all_physical_defense")) ? 0 : reader.GetDoubleSafe("all_physical_defense");
                            powerManager.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("all_magical_attack")) ? 0 : reader.GetDoubleSafe("all_magical_attack");
                            powerManager.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("all_magical_defense")) ? 0 : reader.GetDoubleSafe("all_magical_defense");
                            powerManager.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("all_chemical_attack")) ? 0 : reader.GetDoubleSafe("all_chemical_attack");
                            powerManager.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("all_chemical_defense")) ? 0 : reader.GetDoubleSafe("all_chemical_defense");
                            powerManager.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("all_atomic_attack")) ? 0 : reader.GetDoubleSafe("all_atomic_attack");
                            powerManager.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("all_atomic_defense")) ? 0 : reader.GetDoubleSafe("all_atomic_defense");
                            powerManager.MentalAttack = reader.IsDBNull(reader.GetOrdinal("all_mental_attack")) ? 0 : reader.GetDoubleSafe("all_mental_attack");
                            powerManager.MentalDefense = reader.IsDBNull(reader.GetOrdinal("all_mental_defense")) ? 0 : reader.GetDoubleSafe("all_mental_defense");
                            powerManager.Speed = reader.IsDBNull(reader.GetOrdinal("all_speed")) ? 0 : reader.GetDoubleSafe("all_speed");
                            powerManager.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("all_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("all_critical_damage_rate");
                            powerManager.CriticalRate = reader.IsDBNull(reader.GetOrdinal("all_critical_rate")) ? 0 : reader.GetDoubleSafe("all_critical_rate");
                            powerManager.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("all_critical_resistance_rate");
                            powerManager.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("all_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("all_ignore_critical_rate");
                            powerManager.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("all_penetration_rate")) ? 0 : reader.GetDoubleSafe("all_penetration_rate");
                            powerManager.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("all_penetration_resistance_rate");
                            powerManager.EvasionRate = reader.IsDBNull(reader.GetOrdinal("all_evasion_rate")) ? 0 : reader.GetDoubleSafe("all_evasion_rate");
                            powerManager.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("all_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("all_damage_absorption_rate");
                            powerManager.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("all_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("all_ignore_damage_absorption_rate");
                            powerManager.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("all_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("all_absorbed_damage_rate");
                            powerManager.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("all_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("all_vitality_regeneration_rate");
                            powerManager.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("all_vitality_regeneration_resistance_rate");
                            powerManager.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("all_accuracy_rate")) ? 0 : reader.GetDoubleSafe("all_accuracy_rate");
                            powerManager.LifestealRate = reader.IsDBNull(reader.GetOrdinal("all_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("all_lifesteal_rate");
                            powerManager.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("all_shield_strength")) ? 0 : reader.GetDoubleSafe("all_shield_strength");
                            powerManager.Tenacity = reader.IsDBNull(reader.GetOrdinal("all_tenacity")) ? 0 : reader.GetDoubleSafe("all_tenacity");
                            powerManager.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_resistance_rate")) ? 0 : reader.GetDoubleSafe("all_resistance_rate");
                            powerManager.ComboRate = reader.IsDBNull(reader.GetOrdinal("all_combo_rate")) ? 0 : reader.GetDoubleSafe("all_combo_rate");
                            powerManager.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("all_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("all_ignore_combo_rate");
                            powerManager.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("all_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("all_combo_damage_rate");
                            powerManager.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("all_combo_resistance_rate");
                            powerManager.StunRate = reader.IsDBNull(reader.GetOrdinal("all_stun_rate")) ? 0 : reader.GetDoubleSafe("all_stun_rate");
                            powerManager.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("all_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("all_ignore_stun_rate");
                            powerManager.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("all_reflection_rate")) ? 0 : reader.GetDoubleSafe("all_reflection_rate");
                            powerManager.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("all_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("all_ignore_reflection_rate");
                            powerManager.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("all_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("all_reflection_damage_rate");
                            powerManager.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("all_reflection_resistance_rate");
                            powerManager.Mana = reader.IsDBNull(reader.GetOrdinal("all_mana")) ? 0 : reader.GetFloat("all_mana");
                            powerManager.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("all_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("all_mana_regeneration_rate");
                            powerManager.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("all_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("all_damage_to_different_faction_rate");
                            powerManager.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("all_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("all_resistance_to_different_faction_rate");
                            powerManager.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("all_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("all_damage_to_same_faction_rate");
                            powerManager.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("all_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("all_resistance_to_same_faction_rate");
                            powerManager.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("all_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("all_normal_damage_rate");
                            powerManager.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("all_normal_resistance_rate");
                            powerManager.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("all_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("all_skill_damage_rate");
                            powerManager.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("all_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("all_skill_resistance_rate");
                            powerManager.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            powerManager.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            powerManager.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            powerManager.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            powerManager.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            powerManager.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            powerManager.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            powerManager.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            powerManager.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            powerManager.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            powerManager.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
                        }
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