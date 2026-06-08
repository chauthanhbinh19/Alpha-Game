using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserHIHNsRepository : IUserHIHNsRepository
{
    public async Task<UserHIHNs> GetUserHIHNsAsync(string id)
    {
        UserHIHNs userHIHN = new UserHIHNs();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT *
                FROM user_hihns
                WHERE user_id = @user_id AND hihn_id = @hihn_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@hihn_id", id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userHIHN.Id = reader.GetStringSafe("hihn_id");
                            userHIHN.Level = reader.GetIntSafe("hihn_level");
                            userHIHN.Power = reader.GetDoubleSafe("power");
                            userHIHN.Health = reader.GetDoubleSafe("health");
                            userHIHN.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            userHIHN.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            userHIHN.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            userHIHN.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            userHIHN.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            userHIHN.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            userHIHN.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            userHIHN.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            userHIHN.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            userHIHN.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            userHIHN.Speed = reader.GetDoubleSafe("speed");
                            userHIHN.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            userHIHN.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            userHIHN.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            userHIHN.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            userHIHN.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            userHIHN.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            userHIHN.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            userHIHN.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            userHIHN.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            userHIHN.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            userHIHN.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            userHIHN.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            userHIHN.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            userHIHN.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            userHIHN.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            userHIHN.Tenacity = reader.GetDoubleSafe("tenacity");
                            userHIHN.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            userHIHN.ComboRate = reader.GetDoubleSafe("combo_rate");
                            userHIHN.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            userHIHN.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            userHIHN.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            userHIHN.StunRate = reader.GetDoubleSafe("stun_rate");
                            userHIHN.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            userHIHN.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            userHIHN.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            userHIHN.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            userHIHN.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            userHIHN.Mana = reader.GetDoubleSafe("mana");
                            userHIHN.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            userHIHN.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            userHIHN.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            userHIHN.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            userHIHN.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            userHIHN.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            userHIHN.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            userHIHN.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            userHIHN.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            userHIHN.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            userHIHN.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            userHIHN.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            userHIHN.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            userHIHN.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            userHIHN.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            userHIHN.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            userHIHN.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            userHIHN.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            userHIHN.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            userHIHN.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
                return userHIHN;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task InsertOrUpdateUserHIHNsAsync(string userId, UserHIHNs userHIHN, string id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string checkSQL = @"
            SELECT COUNT(*) FROM user_hihns 
            WHERE user_id = @user_id AND hihn_id = @hihn_id";

            await using (var checkCommand = new MySqlCommand(checkSQL, connection))
            {
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@hihn_id", id);

                int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                if (count > 0)
                {
                    // -------- UPDATE ----------
                    string updateSQL = @"
                    UPDATE user_hihns
                    SET
                        hihn_level = @hihn_level, power = @power, health = @health, mana = @mana, speed = @speed,
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
                    AND hihn_id = @hihn_id;
                ";

                    await using var updateCommand = new MySqlCommand(updateSQL, connection);
                    AddAllParameters(updateCommand, userHIHN, userId, id);

                    await updateCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // -------- INSERT ----------
                    string insertSQL = @"
                    INSERT INTO user_hihns (
                    user_id, hihn_id, hihn_level, power, health, mana, speed,
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
                    @user_id, @hihn_id, @hihn_level, @power, @health, @mana, @speed,
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
                    AddAllParameters(insertCommand, userHIHN, userId, id);

                    await insertCommand.ExecuteNonQueryAsync();
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    public async Task<UserHIHNs> GetSumUserHIHNsAsync(string userId)
    {
        UserHIHNs userHIHNs = new UserHIHNs();
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
                FROM user_hihns 
                WHERE user_id = @user_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userHIHNs.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            userHIHNs.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            userHIHNs.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            userHIHNs.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            userHIHNs.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            userHIHNs.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            userHIHNs.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            userHIHNs.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            userHIHNs.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            userHIHNs.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            userHIHNs.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            userHIHNs.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            userHIHNs.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            userHIHNs.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            userHIHNs.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            userHIHNs.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            userHIHNs.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            userHIHNs.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            userHIHNs.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            userHIHNs.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            userHIHNs.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            userHIHNs.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            userHIHNs.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            userHIHNs.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            userHIHNs.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            userHIHNs.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            userHIHNs.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            userHIHNs.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            userHIHNs.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            userHIHNs.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            userHIHNs.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            userHIHNs.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            userHIHNs.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            userHIHNs.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            userHIHNs.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            userHIHNs.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            userHIHNs.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            userHIHNs.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            userHIHNs.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            userHIHNs.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            userHIHNs.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            userHIHNs.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            userHIHNs.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            userHIHNs.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            userHIHNs.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            userHIHNs.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            userHIHNs.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            userHIHNs.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            userHIHNs.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            userHIHNs.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            userHIHNs.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            userHIHNs.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            userHIHNs.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            userHIHNs.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            userHIHNs.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            userHIHNs.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            userHIHNs.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            userHIHNs.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            userHIHNs.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            userHIHNs.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            userHIHNs.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return userHIHNs;
    }
    private void AddAllParameters(MySqlCommand command, UserHIHNs userHIHN, string userId, string type)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@hihn_id", type);

        command.Parameters.AddWithValue("@hihn_level", userHIHN.Level == 0 ? 1 : userHIHN.Level);
        command.Parameters.AddWithValue("@power", userHIHN.Power);
        command.Parameters.AddWithValue("@health", userHIHN.Health);
        command.Parameters.AddWithValue("@mana", userHIHN.Mana);
        command.Parameters.AddWithValue("@speed", userHIHN.Speed);

        command.Parameters.AddWithValue("@physical_attack", userHIHN.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", userHIHN.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", userHIHN.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", userHIHN.MagicalDefense);

        command.Parameters.AddWithValue("@chemical_attack", userHIHN.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", userHIHN.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", userHIHN.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", userHIHN.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", userHIHN.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", userHIHN.MentalDefense);

        command.Parameters.AddWithValue("@critical_damage_rate", userHIHN.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", userHIHN.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", userHIHN.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", userHIHN.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", userHIHN.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@penetration_rate", userHIHN.PenetrationRate);
        command.Parameters.AddWithValue("@evasion_rate", userHIHN.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", userHIHN.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", userHIHN.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", userHIHN.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", userHIHN.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", userHIHN.VitalityRegenerationResistanceRate);

        command.Parameters.AddWithValue("@accuracy_rate", userHIHN.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", userHIHN.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", userHIHN.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", userHIHN.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", userHIHN.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", userHIHN.ComboRate);
        command.Parameters.AddWithValue("@reflection_rate", userHIHN.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", userHIHN.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", userHIHN.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", userHIHN.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", userHIHN.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", userHIHN.IgnoreStunRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", userHIHN.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", userHIHN.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", userHIHN.ReflectionResistanceRate);

        command.Parameters.AddWithValue("@mana_regeneration_rate", userHIHN.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", userHIHN.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", userHIHN.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", userHIHN.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", userHIHN.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", userHIHN.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", userHIHN.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", userHIHN.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", userHIHN.SkillResistanceRate);

        command.Parameters.AddWithValue("@percent_all_health", userHIHN.PercentAllHealth);
        command.Parameters.AddWithValue("@percent_all_physical_attack", userHIHN.PercentAllPhysicalAttack);
        command.Parameters.AddWithValue("@percent_all_physical_defense", userHIHN.PercentAllPhysicalDefense);
        command.Parameters.AddWithValue("@percent_all_magical_attack", userHIHN.PercentAllMagicalAttack);
        command.Parameters.AddWithValue("@percent_all_magical_defense", userHIHN.PercentAllMagicalDefense);
        command.Parameters.AddWithValue("@percent_all_chemical_attack", userHIHN.PercentAllChemicalAttack);
        command.Parameters.AddWithValue("@percent_all_chemical_defense", userHIHN.PercentAllChemicalDefense);
        command.Parameters.AddWithValue("@percent_all_atomic_attack", userHIHN.PercentAllAtomicAttack);
        command.Parameters.AddWithValue("@percent_all_atomic_defense", userHIHN.PercentAllAtomicDefense);
        command.Parameters.AddWithValue("@percent_all_mental_attack", userHIHN.PercentAllMentalAttack);
        command.Parameters.AddWithValue("@percent_all_mental_defense", userHIHN.PercentAllMentalDefense);
    }
}