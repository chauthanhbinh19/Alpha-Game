using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserHITNsRepository : IUserHITNsRepository
{
    public async Task<UserHITNs> GetUserHITNsAsync(string id)
    {
        UserHITNs userHITN = new UserHITNs();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT *
                FROM user_hitns
                WHERE user_id = @user_id AND hitn_id = @hitn_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@hitn_id", id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userHITN.Id = reader.GetStringSafe("hitn_id");
                            userHITN.Level = reader.GetIntSafe("hitn_level");
                            userHITN.Power = reader.GetDoubleSafe("power");
                            userHITN.Health = reader.GetDoubleSafe("health");
                            userHITN.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            userHITN.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            userHITN.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            userHITN.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            userHITN.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            userHITN.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            userHITN.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            userHITN.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            userHITN.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            userHITN.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            userHITN.Speed = reader.GetDoubleSafe("speed");
                            userHITN.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            userHITN.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            userHITN.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            userHITN.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            userHITN.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            userHITN.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            userHITN.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            userHITN.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            userHITN.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            userHITN.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            userHITN.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            userHITN.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            userHITN.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            userHITN.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            userHITN.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            userHITN.Tenacity = reader.GetDoubleSafe("tenacity");
                            userHITN.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            userHITN.ComboRate = reader.GetDoubleSafe("combo_rate");
                            userHITN.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            userHITN.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            userHITN.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            userHITN.StunRate = reader.GetDoubleSafe("stun_rate");
                            userHITN.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            userHITN.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            userHITN.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            userHITN.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            userHITN.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            userHITN.Mana = reader.GetDoubleSafe("mana");
                            userHITN.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            userHITN.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            userHITN.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            userHITN.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            userHITN.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            userHITN.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            userHITN.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            userHITN.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            userHITN.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            userHITN.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            userHITN.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            userHITN.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            userHITN.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            userHITN.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            userHITN.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            userHITN.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            userHITN.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            userHITN.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            userHITN.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            userHITN.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
                return userHITN;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task InsertOrUpdateUserHITNsAsync(string userId, UserHITNs userHITN, string id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string checkSQL = @"
            SELECT COUNT(*) FROM user_hitns 
            WHERE user_id = @user_id AND hitn_id = @hitn_id";

            await using (var checkCommand = new MySqlCommand(checkSQL, connection))
            {
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@hitn_id", id);

                int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                if (count > 0)
                {
                    // -------- UPDATE ----------
                    string updateSQL = @"
                    UPDATE user_hitns
                    SET
                        hitn_level = @hitn_level, power = @power, health = @health, mana = @mana, speed = @speed,
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
                    AND hitn_id = @hitn_id;
                ";

                    await using var updateCommand = new MySqlCommand(updateSQL, connection);
                    AddAllParameters(updateCommand, userHITN, userId, id);

                    await updateCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // -------- INSERT ----------
                    string insertSQL = @"
                    INSERT INTO user_hitns (
                    user_id, hitn_id, hitn_level, power, health, mana, speed,
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
                    @user_id, @hitn_id, @hitn_level, @power, @health, @mana, @speed,
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
                    AddAllParameters(insertCommand, userHITN, userId, id);

                    await insertCommand.ExecuteNonQueryAsync();
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    public async Task<UserHITNs> GetSumUserHITNsAsync(string userId)
    {
        UserHITNs userHITNs = new UserHITNs();
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
                FROM user_hitns 
                WHERE user_id = @user_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userHITNs.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            userHITNs.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            userHITNs.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            userHITNs.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            userHITNs.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            userHITNs.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            userHITNs.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            userHITNs.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            userHITNs.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            userHITNs.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            userHITNs.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            userHITNs.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            userHITNs.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            userHITNs.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            userHITNs.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            userHITNs.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            userHITNs.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            userHITNs.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            userHITNs.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            userHITNs.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            userHITNs.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            userHITNs.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            userHITNs.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            userHITNs.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            userHITNs.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            userHITNs.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            userHITNs.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            userHITNs.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            userHITNs.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            userHITNs.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            userHITNs.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            userHITNs.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            userHITNs.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            userHITNs.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            userHITNs.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            userHITNs.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            userHITNs.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            userHITNs.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            userHITNs.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            userHITNs.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            userHITNs.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            userHITNs.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            userHITNs.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            userHITNs.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            userHITNs.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            userHITNs.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            userHITNs.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            userHITNs.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            userHITNs.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            userHITNs.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            userHITNs.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            userHITNs.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            userHITNs.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            userHITNs.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            userHITNs.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            userHITNs.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            userHITNs.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            userHITNs.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            userHITNs.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            userHITNs.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            userHITNs.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return userHITNs;
    }
    private void AddAllParameters(MySqlCommand command, UserHITNs userHITN, string userId, string type)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@hitn_id", type);

        command.Parameters.AddWithValue("@hitn_level", userHITN.Level == 0 ? 1 : userHITN.Level);
        command.Parameters.AddWithValue("@power", userHITN.Power);
        command.Parameters.AddWithValue("@health", userHITN.Health);
        command.Parameters.AddWithValue("@mana", userHITN.Mana);
        command.Parameters.AddWithValue("@speed", userHITN.Speed);

        command.Parameters.AddWithValue("@physical_attack", userHITN.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", userHITN.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", userHITN.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", userHITN.MagicalDefense);

        command.Parameters.AddWithValue("@chemical_attack", userHITN.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", userHITN.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", userHITN.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", userHITN.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", userHITN.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", userHITN.MentalDefense);

        command.Parameters.AddWithValue("@critical_damage_rate", userHITN.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", userHITN.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", userHITN.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", userHITN.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", userHITN.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@penetration_rate", userHITN.PenetrationRate);
        command.Parameters.AddWithValue("@evasion_rate", userHITN.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", userHITN.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", userHITN.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", userHITN.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", userHITN.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", userHITN.VitalityRegenerationResistanceRate);

        command.Parameters.AddWithValue("@accuracy_rate", userHITN.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", userHITN.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", userHITN.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", userHITN.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", userHITN.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", userHITN.ComboRate);
        command.Parameters.AddWithValue("@reflection_rate", userHITN.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", userHITN.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", userHITN.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", userHITN.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", userHITN.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", userHITN.IgnoreStunRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", userHITN.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", userHITN.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", userHITN.ReflectionResistanceRate);

        command.Parameters.AddWithValue("@mana_regeneration_rate", userHITN.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", userHITN.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", userHITN.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", userHITN.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", userHITN.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", userHITN.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", userHITN.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", userHITN.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", userHITN.SkillResistanceRate);

        command.Parameters.AddWithValue("@percent_all_health", userHITN.PercentAllHealth);
        command.Parameters.AddWithValue("@percent_all_physical_attack", userHITN.PercentAllPhysicalAttack);
        command.Parameters.AddWithValue("@percent_all_physical_defense", userHITN.PercentAllPhysicalDefense);
        command.Parameters.AddWithValue("@percent_all_magical_attack", userHITN.PercentAllMagicalAttack);
        command.Parameters.AddWithValue("@percent_all_magical_defense", userHITN.PercentAllMagicalDefense);
        command.Parameters.AddWithValue("@percent_all_chemical_attack", userHITN.PercentAllChemicalAttack);
        command.Parameters.AddWithValue("@percent_all_chemical_defense", userHITN.PercentAllChemicalDefense);
        command.Parameters.AddWithValue("@percent_all_atomic_attack", userHITN.PercentAllAtomicAttack);
        command.Parameters.AddWithValue("@percent_all_atomic_defense", userHITN.PercentAllAtomicDefense);
        command.Parameters.AddWithValue("@percent_all_mental_attack", userHITN.PercentAllMentalAttack);
        command.Parameters.AddWithValue("@percent_all_mental_defense", userHITN.PercentAllMentalDefense);
    }
}