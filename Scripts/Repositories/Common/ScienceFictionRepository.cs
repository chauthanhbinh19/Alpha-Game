using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class ScienceFictionRepository : IScienceFictionRepository
{
    public async Task<ScienceFiction> GetScienceFictionAsync(string type)
    {
        ScienceFiction scienceFiction = new ScienceFiction();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT *
                FROM science_fiction
                WHERE user_id = @user_id AND rank_type = @type;
            ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@type", type);

                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            scienceFiction.Type = reader.GetString("rank_type");
                            scienceFiction.Level = reader.GetInt32("rank_level");
                            scienceFiction.Power = reader.GetDouble("power");
                            scienceFiction.Health = reader.GetDouble("health");
                            scienceFiction.PhysicalAttack = reader.GetDouble("physical_attack");
                            scienceFiction.PhysicalDefense = reader.GetDouble("physical_defense");
                            scienceFiction.MagicalAttack = reader.GetDouble("magical_attack");
                            scienceFiction.MagicalDefense = reader.GetDouble("magical_defense");
                            scienceFiction.ChemicalAttack = reader.GetDouble("chemical_attack");
                            scienceFiction.ChemicalDefense = reader.GetDouble("chemical_defense");
                            scienceFiction.AtomicAttack = reader.GetDouble("atomic_attack");
                            scienceFiction.AtomicDefense = reader.GetDouble("atomic_defense");
                            scienceFiction.MentalAttack = reader.GetDouble("mental_attack");
                            scienceFiction.MentalDefense = reader.GetDouble("mental_defense");
                            scienceFiction.Speed = reader.GetDouble("speed");
                            scienceFiction.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            scienceFiction.CriticalRate = reader.GetDouble("critical_rate");
                            scienceFiction.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            scienceFiction.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            scienceFiction.PenetrationRate = reader.GetDouble("penetration_rate");
                            scienceFiction.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            scienceFiction.EvasionRate = reader.GetDouble("evasion_rate");
                            scienceFiction.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            scienceFiction.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            scienceFiction.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            scienceFiction.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            scienceFiction.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            scienceFiction.AccuracyRate = reader.GetDouble("accuracy_rate");
                            scienceFiction.LifestealRate = reader.GetDouble("lifesteal_rate");
                            scienceFiction.ShieldStrength = reader.GetDouble("shield_strength");
                            scienceFiction.Tenacity = reader.GetDouble("tenacity");
                            scienceFiction.ResistanceRate = reader.GetDouble("resistance_rate");
                            scienceFiction.ComboRate = reader.GetDouble("combo_rate");
                            scienceFiction.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            scienceFiction.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            scienceFiction.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            scienceFiction.StunRate = reader.GetDouble("stun_rate");
                            scienceFiction.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            scienceFiction.ReflectionRate = reader.GetDouble("reflection_rate");
                            scienceFiction.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            scienceFiction.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            scienceFiction.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            scienceFiction.Mana = reader.GetFloat("mana");
                            scienceFiction.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            scienceFiction.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            scienceFiction.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            scienceFiction.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            scienceFiction.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            scienceFiction.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            scienceFiction.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            scienceFiction.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            scienceFiction.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            scienceFiction.PercentAllHealth = reader.GetDouble("percent_all_health");
                            scienceFiction.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            scienceFiction.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            scienceFiction.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            scienceFiction.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            scienceFiction.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            scienceFiction.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            scienceFiction.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            scienceFiction.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            scienceFiction.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            scienceFiction.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return scienceFiction;
    }
    public async Task InsertOrUpdateScienceFictionAsync(string user_id, ScienceFiction scienceFiction, string type)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string checkQuery = @"
            SELECT COUNT(*) FROM science_fiction 
            WHERE user_id = @user_id AND rank_type = @rank_type";

            await using (var checkCmd = new MySqlCommand(checkQuery, connection))
            {
                checkCmd.Parameters.AddWithValue("@user_id", user_id);
                checkCmd.Parameters.AddWithValue("@rank_type", type);

                int count = Convert.ToInt32(await checkCmd.ExecuteScalarAsync());

                if (count > 0)
                {
                    // -------- UPDATE ----------
                    string updateQuery = @"
                    UPDATE science_fiction
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

                    await using var updateCmd = new MySqlCommand(updateQuery, connection);
                    AddAllParameters(updateCmd, scienceFiction, user_id, type);

                    await updateCmd.ExecuteNonQueryAsync();
                }
                else
                {
                    // -------- INSERT ----------
                    string insertQuery = @"
                    INSERT INTO science_fiction
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
                    VALUES (
                        @user_id, @rank_type, @rank_level, @power, @health, @mana, @speed,
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
                        @percent_all_mental_attack, @percent_all_mental_defense
                    );
                ";

                    await using var insertCmd = new MySqlCommand(insertQuery, connection);
                    AddAllParameters(insertCmd, scienceFiction, user_id, type);

                    await insertCmd.ExecuteNonQueryAsync();
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    public async Task<ScienceFiction> GetSumScienceFictionAsync(string user_id)
    {
        ScienceFiction scienceFiction = new ScienceFiction();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
                    SUM(power) AS total_power,
                    SUM(health) AS total_health,
                    SUM(mana) AS total_mana,
                    SUM(physical_attack) AS total_physical_attack,
                    SUM(physical_defense) AS total_physical_defense,
                    SUM(magical_attack) AS total_magical_attack,
                    SUM(magical_defense) AS total_magical_defense,
                    SUM(chemical_attack) AS total_chemical_attack,
                    SUM(chemical_defense) AS total_chemical_defense,
                    SUM(atomic_attack) AS total_atomic_attack,
                    SUM(atomic_defense) AS total_atomic_defense,
                    SUM(mental_attack) AS total_mental_attack,
                    SUM(mental_defense) AS total_mental_defense,
                    SUM(speed) AS total_speed,
                    SUM(critical_damage_rate) AS total_critical_damage_rate,
                    SUM(critical_rate) AS total_critical_rate,
                    SUM(critical_resistance_rate) AS total_critical_resistance_rate,
                    SUM(ignore_critical_rate) AS total_ignore_critical_rate,
                    SUM(penetration_rate) AS total_penetration_rate,
                    SUM(penetration_resistance_rate) AS total_penetration_resistance_rate,
                    SUM(evasion_rate) AS total_evasion_rate,
                    SUM(damage_absorption_rate) AS total_damage_absorption_rate,
                    SUM(ignore_damage_absorption_rate) AS total_ignore_damage_absorption_rate,
                    SUM(absorbed_damage_rate) AS total_absorbed_damage_rate,
                    SUM(vitality_regeneration_rate) AS total_vitality_regeneration_rate,
                    SUM(vitality_regeneration_resistance_rate) AS total_vitality_regeneration_resistance_rate,
                    SUM(accuracy_rate) AS total_accuracy_rate,
                    SUM(lifesteal_rate) AS total_lifesteal_rate,
                    SUM(shield_strength) AS total_shield_strength,
                    SUM(tenacity) AS total_tenacity,
                    SUM(resistance_rate) AS total_resistance_rate,
                    SUM(combo_rate) AS total_combo_rate,
                    SUM(ignore_combo_rate) AS total_ignore_combo_rate,
                    SUM(combo_damage_rate) AS total_combo_damage_rate,
                    SUM(combo_resistance_rate) AS total_combo_resistance_rate,
                    SUM(stun_rate) AS total_stun_rate,
                    SUM(ignore_stun_rate) AS total_ignore_stun_rate,
                    SUM(reflection_rate) AS total_reflection_rate,
                    SUM(ignore_reflection_rate) AS total_ignore_reflection_rate,
                    SUM(reflection_damage_rate) AS total_reflection_damage_rate,
                    SUM(reflection_resistance_rate) AS total_reflection_resistance_rate,
                    SUM(mana_regeneration_rate) AS total_mana_regeneration_rate,
                    SUM(damage_to_different_faction_rate) AS total_damage_to_different_faction_rate,
                    SUM(resistance_to_different_faction_rate) AS total_resistance_to_different_faction_rate,
                    SUM(damage_to_same_faction_rate) AS total_damage_to_same_faction_rate,
                    SUM(resistance_to_same_faction_rate) AS total_resistance_to_same_faction_rate,
                    SUM(normal_damage_rate) AS total_normal_damage_rate,
                    SUM(normal_resistance_rate) AS total_normal_resistance_rate,
                    SUM(skill_damage_rate) AS total_skill_damage_rate,
                    SUM(skill_resistance_rate) AS total_skill_resistance_rate,
                    SUM(percent_all_health) AS percent_all_health,
                    SUM(percent_all_physical_attack) AS percent_all_physical_attack,
                    SUM(percent_all_physical_defense) AS percent_all_physical_defense,
                    SUM(percent_all_magical_attack) AS percent_all_magical_attack,
                    SUM(percent_all_magical_defense) AS percent_all_magical_defense,
                    SUM(percent_all_chemical_attack) AS percent_all_chemical_attack,
                    SUM(percent_all_chemical_defense) AS percent_all_chemical_defense,
                    SUM(percent_all_atomic_attack) AS percent_all_atomic_attack,
                    SUM(percent_all_atomic_defense) AS percent_all_atomic_defense,
                    SUM(percent_all_mental_attack) AS percent_all_mental_attack,
                    SUM(percent_all_mental_defense) AS percent_all_mental_defense
                FROM science_fiction 
                WHERE user_id = @user_id;
            ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);

                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            scienceFiction.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                            scienceFiction.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                            scienceFiction.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            scienceFiction.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                            scienceFiction.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                            scienceFiction.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                            scienceFiction.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                            scienceFiction.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                            scienceFiction.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                            scienceFiction.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                            scienceFiction.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                            scienceFiction.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                            scienceFiction.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                            scienceFiction.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                            scienceFiction.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                            scienceFiction.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                            scienceFiction.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                            scienceFiction.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                            scienceFiction.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                            scienceFiction.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                            scienceFiction.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                            scienceFiction.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                            scienceFiction.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                            scienceFiction.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                            scienceFiction.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                            scienceFiction.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                            scienceFiction.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                            scienceFiction.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                            scienceFiction.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                            scienceFiction.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                            scienceFiction.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                            scienceFiction.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                            scienceFiction.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                            scienceFiction.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                            scienceFiction.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                            scienceFiction.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                            scienceFiction.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                            scienceFiction.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                            scienceFiction.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                            scienceFiction.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                            scienceFiction.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                            scienceFiction.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                            scienceFiction.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                            scienceFiction.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                            scienceFiction.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                            scienceFiction.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                            scienceFiction.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                            scienceFiction.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                            scienceFiction.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                            scienceFiction.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
                            scienceFiction.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDouble("percent_all_health");
                            scienceFiction.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDouble("percent_all_physical_attack");
                            scienceFiction.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDouble("percent_all_physical_defense");
                            scienceFiction.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDouble("percent_all_magical_attack");
                            scienceFiction.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDouble("percent_all_magical_defense");
                            scienceFiction.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDouble("percent_all_chemical_attack");
                            scienceFiction.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDouble("percent_all_chemical_defense");
                            scienceFiction.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDouble("percent_all_atomic_attack");
                            scienceFiction.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDouble("percent_all_atomic_defense");
                            scienceFiction.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDouble("percent_all_mental_attack");
                            scienceFiction.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDouble("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return scienceFiction;
    }
    private void AddAllParameters(MySqlCommand cmd, ScienceFiction a, string user_id, string type)
    {
        cmd.Parameters.AddWithValue("@user_id", user_id);
        cmd.Parameters.AddWithValue("@rank_type", type);

        cmd.Parameters.AddWithValue("@rank_level", a.Level == 0 ? 1 : a.Level);
        cmd.Parameters.AddWithValue("@power", a.Power);
        cmd.Parameters.AddWithValue("@health", a.Health);
        cmd.Parameters.AddWithValue("@mana", a.Mana);
        cmd.Parameters.AddWithValue("@speed", a.Speed);

        cmd.Parameters.AddWithValue("@physical_attack", a.PhysicalAttack);
        cmd.Parameters.AddWithValue("@physical_defense", a.PhysicalDefense);
        cmd.Parameters.AddWithValue("@magical_attack", a.MagicalAttack);
        cmd.Parameters.AddWithValue("@magical_defense", a.MagicalDefense);

        cmd.Parameters.AddWithValue("@chemical_attack", a.ChemicalAttack);
        cmd.Parameters.AddWithValue("@chemical_defense", a.ChemicalDefense);
        cmd.Parameters.AddWithValue("@atomic_attack", a.AtomicAttack);
        cmd.Parameters.AddWithValue("@atomic_defense", a.AtomicDefense);
        cmd.Parameters.AddWithValue("@mental_attack", a.MentalAttack);
        cmd.Parameters.AddWithValue("@mental_defense", a.MentalDefense);

        cmd.Parameters.AddWithValue("@critical_damage_rate", a.CriticalDamageRate);
        cmd.Parameters.AddWithValue("@critical_rate", a.CriticalRate);
        cmd.Parameters.AddWithValue("@penetration_rate", a.PenetrationRate);
        cmd.Parameters.AddWithValue("@evasion_rate", a.EvasionRate);
        cmd.Parameters.AddWithValue("@damage_absorption_rate", a.DamageAbsorptionRate);
        cmd.Parameters.AddWithValue("@vitality_regeneration_rate", a.VitalityRegenerationRate);

        cmd.Parameters.AddWithValue("@accuracy_rate", a.AccuracyRate);
        cmd.Parameters.AddWithValue("@lifesteal_rate", a.LifestealRate);
        cmd.Parameters.AddWithValue("@shield_strength", a.ShieldStrength);
        cmd.Parameters.AddWithValue("@tenacity", a.Tenacity);
        cmd.Parameters.AddWithValue("@resistance_rate", a.ResistanceRate);
        cmd.Parameters.AddWithValue("@combo_rate", a.ComboRate);
        cmd.Parameters.AddWithValue("@reflection_rate", a.ReflectionRate);

        cmd.Parameters.AddWithValue("@mana_regeneration_rate", a.ManaRegenerationRate);
        cmd.Parameters.AddWithValue("@damage_to_different_faction_rate", a.DamageToDifferentFactionRate);
        cmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", a.ResistanceToDifferentFactionRate);
        cmd.Parameters.AddWithValue("@damage_to_same_faction_rate", a.DamageToSameFactionRate);
        cmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", a.ResistanceToSameFactionRate);

        cmd.Parameters.AddWithValue("@percent_all_health", a.PercentAllHealth);
        cmd.Parameters.AddWithValue("@percent_all_physical_attack", a.PercentAllPhysicalAttack);
        cmd.Parameters.AddWithValue("@percent_all_physical_defense", a.PercentAllPhysicalDefense);
        cmd.Parameters.AddWithValue("@percent_all_magical_attack", a.PercentAllMagicalAttack);
        cmd.Parameters.AddWithValue("@percent_all_magical_defense", a.PercentAllMagicalDefense);
        cmd.Parameters.AddWithValue("@percent_all_chemical_attack", a.PercentAllChemicalAttack);
        cmd.Parameters.AddWithValue("@percent_all_chemical_defense", a.PercentAllChemicalDefense);
        cmd.Parameters.AddWithValue("@percent_all_atomic_attack", a.PercentAllAtomicAttack);
        cmd.Parameters.AddWithValue("@percent_all_atomic_defense", a.PercentAllAtomicDefense);
        cmd.Parameters.AddWithValue("@percent_all_mental_attack", a.PercentAllMentalAttack);
        cmd.Parameters.AddWithValue("@percent_all_mental_defense", a.PercentAllMentalDefense);
    }
}