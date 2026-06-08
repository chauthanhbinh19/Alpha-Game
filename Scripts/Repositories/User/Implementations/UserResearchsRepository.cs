using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserResearchsRepository : IUserResearchsRepository
{
    public async Task<UserResearchs> GetUserResearchsAsync(string id)
    {
        UserResearchs userResearch = new UserResearchs();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT *
                FROM user_researchs
                WHERE user_id = @user_id AND research_id = @research_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@research_id", id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userResearch.Id = reader.GetStringSafe("research_id");
                            userResearch.Level = reader.GetIntSafe("research_level");
                            userResearch.Power = reader.GetDoubleSafe("power");
                            userResearch.Health = reader.GetDoubleSafe("health");
                            userResearch.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            userResearch.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            userResearch.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            userResearch.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            userResearch.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            userResearch.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            userResearch.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            userResearch.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            userResearch.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            userResearch.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            userResearch.Speed = reader.GetDoubleSafe("speed");
                            userResearch.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            userResearch.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            userResearch.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            userResearch.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            userResearch.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            userResearch.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            userResearch.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            userResearch.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            userResearch.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            userResearch.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            userResearch.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            userResearch.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            userResearch.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            userResearch.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            userResearch.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            userResearch.Tenacity = reader.GetDoubleSafe("tenacity");
                            userResearch.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            userResearch.ComboRate = reader.GetDoubleSafe("combo_rate");
                            userResearch.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            userResearch.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            userResearch.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            userResearch.StunRate = reader.GetDoubleSafe("stun_rate");
                            userResearch.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            userResearch.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            userResearch.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            userResearch.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            userResearch.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            userResearch.Mana = reader.GetDoubleSafe("mana");
                            userResearch.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            userResearch.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            userResearch.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            userResearch.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            userResearch.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            userResearch.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            userResearch.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            userResearch.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            userResearch.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            userResearch.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            userResearch.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            userResearch.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            userResearch.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            userResearch.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            userResearch.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            userResearch.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            userResearch.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            userResearch.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            userResearch.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            userResearch.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
                return userResearch;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task InsertOrUpdateUserResearchsAsync(string userId, UserResearchs userResearch, string id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string checkSQL = @"
            SELECT COUNT(*) FROM user_researchs 
            WHERE user_id = @user_id AND research_id = @research_id";

            await using (var checkCommand = new MySqlCommand(checkSQL, connection))
            {
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@research_id", id);

                int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                if (count > 0)
                {
                    // -------- UPDATE ----------
                    string updateSQL = @"
                    UPDATE user_researchs
                    SET
                        research_level = @research_level, power = @power, health = @health, mana = @mana, speed = @speed,
                        physical_attack = @physical_attack, physical_defense = @physical_defense,
                        magical_attack = @magical_attack, magical_defense = @magical_defense,
                        chemical_attack = @chemical_attack, chemical_defense = @chemical_defense,
                        atomic_attack = @atomic_attack, atomic_defense = @atomic_defense,
                        mental_attack = @mental_attack, mental_defense = @mental_defense,
                        critical_damage_rate = @critical_damage_rate, critical_rate = @critical_rate,
                        critical_resistance_rate = @critical_resistance_rate, ignore_critical_rate = @ignore_critical_rate,
                        penetration_rate = @penetration_rate, penetration_resistance_rate = @penetration_resistance_rate,
                        evasion_rate = @evasion_rate, damage_absorption_rate = @damage_absorption_rate,
                        ignore_damage_absorption_rate = @ignore_damage_absorption_rate, absorbed_damage_rate = @absorbed_damage_rate,
                        vitality_regeneration_rate = @vitality_regeneration_rate,
                        vitality_regeneration_resistance_rate = @vitality_regeneration_resistance_rate,
                        accuracy_rate = @accuracy_rate, lifesteal_rate = @lifesteal_rate,
                        shield_strength = @shield_strength, tenacity = @tenacity,
                        resistance_rate = @resistance_rate, combo_rate = @combo_rate,
                        ignore_combo_rate = @ignore_combo_rate, combo_damage_rate = @combo_damage_rate,
                        combo_resistance_rate = @combo_resistance_rate, stun_rate = @stun_rate,
                        ignore_stun_rate = @ignore_stun_rate,
                        reflection_rate = @reflection_rate,
                        ignore_reflection_rate = @ignore_reflection_rate,
                        reflection_damage_rate = @reflection_damage_rate,
                        reflection_resistance_rate = @reflection_resistance_rate,
                        mana_regeneration_rate = @mana_regeneration_rate,
                        damage_to_different_faction_rate = @damage_to_different_faction_rate,
                        resistance_to_different_faction_rate = @resistance_to_different_faction_rate,
                        damage_to_same_faction_rate = @damage_to_same_faction_rate,
                        resistance_to_same_faction_rate = @resistance_to_same_faction_rate,
                        normal_damage_rate = @normal_damage_rate,
                        normal_resistance_rate = @normal_resistance_rate,
                        skill_damage_rate = @skill_damage_rate,
                        skill_resistance_rate = @skill_resistance_rate,
                        percent_all_health = @percent_all_health,
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
                    WHERE user_id = @user_id
                    AND research_id = @research_id;
                ";

                    await using var updateCommand = new MySqlCommand(updateSQL, connection);
                    AddAllParameters(updateCommand, userResearch, userId, id);

                    await updateCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // -------- INSERT ----------
                    string insertSQL = @"
                    INSERT INTO user_researchs (
                    user_id, research_id, research_level, power, health, mana, speed,
                    physical_attack, physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense,
                    atomic_attack, atomic_defense, mental_attack, mental_defense,
                    critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                    penetration_rate, penetration_resistance_rate, evasion_rate,
                    damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                    vitality_regeneration_rate, vitality_regeneration_resistance_rate, accuracy_rate, lifesteal_rate,
                    shield_strength, tenacity, resistance_rate,
                    combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                    stun_rate, ignore_stun_rate,
                    reflection_rate, ignore_reflection_rate,
                    reflection_damage_rate, reflection_resistance_rate,
                    mana_regeneration_rate,
                    damage_to_different_faction_rate, resistance_to_different_faction_rate,
                    damage_to_same_faction_rate, resistance_to_same_faction_rate,
                    normal_damage_rate, normal_resistance_rate,
                    skill_damage_rate, skill_resistance_rate,
                    percent_all_health,
                    percent_all_physical_attack, percent_all_physical_defense,
                    percent_all_magical_attack, percent_all_magical_defense,
                    percent_all_chemical_attack, percent_all_chemical_defense,
                    percent_all_atomic_attack, percent_all_atomic_defense,
                    percent_all_mental_attack, percent_all_mental_defense
                )
                VALUES (
                    @user_id, @research_id, @research_level, @power, @health, @mana, @speed,
                    @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                    @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                    @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                    @penetration_rate, @penetration_resistance_rate, @evasion_rate,
                    @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                    @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                    @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                    @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate, @stun_rate, @ignore_stun_rate,
                    @reflection_rate, @ignore_reflection_rate,
                    @reflection_damage_rate, @reflection_resistance_rate, @mana_regeneration_rate,
                    @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                    @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                    @normal_damage_rate, @normal_resistance_rate,
                    @skill_damage_rate, @skill_resistance_rate,
                    @percent_all_health,
                    @percent_all_physical_attack, @percent_all_physical_defense,
                    @percent_all_magical_attack, @percent_all_magical_defense,
                    @percent_all_chemical_attack, @percent_all_chemical_defense,
                    @percent_all_atomic_attack, @percent_all_atomic_defense,
                    @percent_all_mental_attack, @percent_all_mental_defense
                );
                ";

                    await using var insertCommand = new MySqlCommand(insertSQL, connection);
                    AddAllParameters(insertCommand, userResearch, userId, id);

                    await insertCommand.ExecuteNonQueryAsync();
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    public async Task<UserResearchs> GetSumUserResearchsAsync(string userId)
    {
        UserResearchs userResearchs = new UserResearchs();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
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
                FROM user_researchs 
                WHERE user_id = @user_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userResearchs.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            userResearchs.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            userResearchs.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            userResearchs.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            userResearchs.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            userResearchs.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            userResearchs.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            userResearchs.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            userResearchs.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            userResearchs.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            userResearchs.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            userResearchs.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            userResearchs.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            userResearchs.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            userResearchs.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            userResearchs.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            userResearchs.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            userResearchs.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            userResearchs.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            userResearchs.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            userResearchs.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            userResearchs.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            userResearchs.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            userResearchs.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            userResearchs.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            userResearchs.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            userResearchs.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            userResearchs.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            userResearchs.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            userResearchs.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            userResearchs.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            userResearchs.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            userResearchs.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            userResearchs.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            userResearchs.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            userResearchs.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            userResearchs.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            userResearchs.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            userResearchs.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            userResearchs.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            userResearchs.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            userResearchs.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            userResearchs.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            userResearchs.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            userResearchs.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            userResearchs.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            userResearchs.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            userResearchs.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            userResearchs.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            userResearchs.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            userResearchs.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            userResearchs.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            userResearchs.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            userResearchs.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            userResearchs.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            userResearchs.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            userResearchs.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            userResearchs.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            userResearchs.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            userResearchs.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            userResearchs.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return userResearchs;
    }
    private void AddAllParameters(MySqlCommand command, UserResearchs userResearch, string userId, string type)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@research_id", type);

        command.Parameters.AddWithValue("@research_level", userResearch.Level == 0 ? 1 : userResearch.Level);
        command.Parameters.AddWithValue("@power", userResearch.Power);
        command.Parameters.AddWithValue("@health", userResearch.Health);
        command.Parameters.AddWithValue("@mana", userResearch.Mana);
        command.Parameters.AddWithValue("@speed", userResearch.Speed);

        command.Parameters.AddWithValue("@physical_attack", userResearch.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", userResearch.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", userResearch.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", userResearch.MagicalDefense);

        command.Parameters.AddWithValue("@chemical_attack", userResearch.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", userResearch.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", userResearch.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", userResearch.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", userResearch.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", userResearch.MentalDefense);

        command.Parameters.AddWithValue("@critical_damage_rate", userResearch.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", userResearch.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", userResearch.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", userResearch.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", userResearch.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@penetration_rate", userResearch.PenetrationRate);
        command.Parameters.AddWithValue("@evasion_rate", userResearch.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", userResearch.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", userResearch.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", userResearch.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", userResearch.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", userResearch.VitalityRegenerationResistanceRate);

        command.Parameters.AddWithValue("@accuracy_rate", userResearch.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", userResearch.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", userResearch.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", userResearch.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", userResearch.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", userResearch.ComboRate);
        command.Parameters.AddWithValue("@reflection_rate", userResearch.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", userResearch.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", userResearch.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", userResearch.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", userResearch.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", userResearch.IgnoreStunRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", userResearch.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", userResearch.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", userResearch.ReflectionResistanceRate);

        command.Parameters.AddWithValue("@mana_regeneration_rate", userResearch.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", userResearch.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", userResearch.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", userResearch.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", userResearch.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", userResearch.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", userResearch.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", userResearch.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", userResearch.SkillResistanceRate);

        command.Parameters.AddWithValue("@percent_all_health", userResearch.PercentAllHealth);
        command.Parameters.AddWithValue("@percent_all_physical_attack", userResearch.PercentAllPhysicalAttack);
        command.Parameters.AddWithValue("@percent_all_physical_defense", userResearch.PercentAllPhysicalDefense);
        command.Parameters.AddWithValue("@percent_all_magical_attack", userResearch.PercentAllMagicalAttack);
        command.Parameters.AddWithValue("@percent_all_magical_defense", userResearch.PercentAllMagicalDefense);
        command.Parameters.AddWithValue("@percent_all_chemical_attack", userResearch.PercentAllChemicalAttack);
        command.Parameters.AddWithValue("@percent_all_chemical_defense", userResearch.PercentAllChemicalDefense);
        command.Parameters.AddWithValue("@percent_all_atomic_attack", userResearch.PercentAllAtomicAttack);
        command.Parameters.AddWithValue("@percent_all_atomic_defense", userResearch.PercentAllAtomicDefense);
        command.Parameters.AddWithValue("@percent_all_mental_attack", userResearch.PercentAllMentalAttack);
        command.Parameters.AddWithValue("@percent_all_mental_defense", userResearch.PercentAllMentalDefense);
    }
}