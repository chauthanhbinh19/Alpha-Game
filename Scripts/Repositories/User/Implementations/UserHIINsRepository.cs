using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserHIINsRepository : IUserHIINsRepository
{
    public async Task<UserHIINs> GetUserHIINsAsync(string id)
    {
        UserHIINs userHIIN = new UserHIINs();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT *
                FROM user_hiins
                WHERE user_id = @user_id AND hiin_id = @hiin_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@hiin_id", id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userHIIN.Id = reader.GetStringSafe("hiin_id");
                            userHIIN.Level = reader.GetIntSafe("hiin_level");
                            userHIIN.Power = reader.GetDoubleSafe("power");
                            userHIIN.Health = reader.GetDoubleSafe("health");
                            userHIIN.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            userHIIN.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            userHIIN.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            userHIIN.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            userHIIN.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            userHIIN.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            userHIIN.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            userHIIN.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            userHIIN.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            userHIIN.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            userHIIN.Speed = reader.GetDoubleSafe("speed");
                            userHIIN.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            userHIIN.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            userHIIN.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            userHIIN.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            userHIIN.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            userHIIN.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            userHIIN.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            userHIIN.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            userHIIN.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            userHIIN.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            userHIIN.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            userHIIN.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            userHIIN.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            userHIIN.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            userHIIN.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            userHIIN.Tenacity = reader.GetDoubleSafe("tenacity");
                            userHIIN.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            userHIIN.ComboRate = reader.GetDoubleSafe("combo_rate");
                            userHIIN.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            userHIIN.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            userHIIN.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            userHIIN.StunRate = reader.GetDoubleSafe("stun_rate");
                            userHIIN.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            userHIIN.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            userHIIN.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            userHIIN.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            userHIIN.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            userHIIN.Mana = reader.GetDoubleSafe("mana");
                            userHIIN.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            userHIIN.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            userHIIN.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            userHIIN.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            userHIIN.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            userHIIN.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            userHIIN.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            userHIIN.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            userHIIN.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            userHIIN.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            userHIIN.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            userHIIN.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            userHIIN.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            userHIIN.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            userHIIN.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            userHIIN.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            userHIIN.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            userHIIN.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            userHIIN.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            userHIIN.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
                return userHIIN;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task InsertOrUpdateUserHIINsAsync(string userId, UserHIINs userHIIN, string id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string checkSQL = @"
            SELECT COUNT(*) FROM user_hiins 
            WHERE user_id = @user_id AND hiin_id = @hiin_id";

            await using (var checkCommand = new MySqlCommand(checkSQL, connection))
            {
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@hiin_id", id);

                int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                if (count > 0)
                {
                    // -------- UPDATE ----------
                    string updateSQL = @"
                    UPDATE user_hiins
                    SET
                        hiin_level = @hiin_level, power = @power, health = @health, mana = @mana, speed = @speed,
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
                    AND hiin_id = @hiin_id;
                ";

                    await using var updateCommand = new MySqlCommand(updateSQL, connection);
                    AddAllParameters(updateCommand, userHIIN, userId, id);

                    await updateCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // -------- INSERT ----------
                    string insertSQL = @"
                    INSERT INTO user_hiins (
                    user_id, hiin_id, hiin_level, power, health, mana, speed,
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
                    @user_id, @hiin_id, @hiin_level, @power, @health, @mana, @speed,
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
                    AddAllParameters(insertCommand, userHIIN, userId, id);

                    await insertCommand.ExecuteNonQueryAsync();
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    public async Task<UserHIINs> GetSumUserHIINsAsync(string userId)
    {
        UserHIINs userHIINs = new UserHIINs();
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
                FROM user_hiins 
                WHERE user_id = @user_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userHIINs.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            userHIINs.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            userHIINs.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            userHIINs.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            userHIINs.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            userHIINs.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            userHIINs.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            userHIINs.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            userHIINs.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            userHIINs.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            userHIINs.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            userHIINs.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            userHIINs.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            userHIINs.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            userHIINs.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            userHIINs.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            userHIINs.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            userHIINs.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            userHIINs.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            userHIINs.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            userHIINs.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            userHIINs.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            userHIINs.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            userHIINs.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            userHIINs.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            userHIINs.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            userHIINs.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            userHIINs.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            userHIINs.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            userHIINs.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            userHIINs.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            userHIINs.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            userHIINs.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            userHIINs.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            userHIINs.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            userHIINs.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            userHIINs.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            userHIINs.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            userHIINs.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            userHIINs.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            userHIINs.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            userHIINs.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            userHIINs.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            userHIINs.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            userHIINs.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            userHIINs.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            userHIINs.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            userHIINs.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            userHIINs.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            userHIINs.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            userHIINs.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            userHIINs.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            userHIINs.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            userHIINs.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            userHIINs.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            userHIINs.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            userHIINs.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            userHIINs.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            userHIINs.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            userHIINs.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            userHIINs.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return userHIINs;
    }
    private void AddAllParameters(MySqlCommand command, UserHIINs userHIIN, string userId, string type)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@hiin_id", type);

        command.Parameters.AddWithValue("@hiin_level", userHIIN.Level == 0 ? 1 : userHIIN.Level);
        command.Parameters.AddWithValue("@power", userHIIN.Power);
        command.Parameters.AddWithValue("@health", userHIIN.Health);
        command.Parameters.AddWithValue("@mana", userHIIN.Mana);
        command.Parameters.AddWithValue("@speed", userHIIN.Speed);

        command.Parameters.AddWithValue("@physical_attack", userHIIN.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", userHIIN.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", userHIIN.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", userHIIN.MagicalDefense);

        command.Parameters.AddWithValue("@chemical_attack", userHIIN.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", userHIIN.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", userHIIN.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", userHIIN.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", userHIIN.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", userHIIN.MentalDefense);

        command.Parameters.AddWithValue("@critical_damage_rate", userHIIN.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", userHIIN.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", userHIIN.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", userHIIN.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", userHIIN.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@penetration_rate", userHIIN.PenetrationRate);
        command.Parameters.AddWithValue("@evasion_rate", userHIIN.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", userHIIN.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", userHIIN.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", userHIIN.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", userHIIN.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", userHIIN.VitalityRegenerationResistanceRate);

        command.Parameters.AddWithValue("@accuracy_rate", userHIIN.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", userHIIN.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", userHIIN.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", userHIIN.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", userHIIN.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", userHIIN.ComboRate);
        command.Parameters.AddWithValue("@reflection_rate", userHIIN.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", userHIIN.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", userHIIN.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", userHIIN.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", userHIIN.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", userHIIN.IgnoreStunRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", userHIIN.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", userHIIN.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", userHIIN.ReflectionResistanceRate);

        command.Parameters.AddWithValue("@mana_regeneration_rate", userHIIN.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", userHIIN.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", userHIIN.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", userHIIN.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", userHIIN.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", userHIIN.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", userHIIN.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", userHIIN.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", userHIIN.SkillResistanceRate);

        command.Parameters.AddWithValue("@percent_all_health", userHIIN.PercentAllHealth);
        command.Parameters.AddWithValue("@percent_all_physical_attack", userHIIN.PercentAllPhysicalAttack);
        command.Parameters.AddWithValue("@percent_all_physical_defense", userHIIN.PercentAllPhysicalDefense);
        command.Parameters.AddWithValue("@percent_all_magical_attack", userHIIN.PercentAllMagicalAttack);
        command.Parameters.AddWithValue("@percent_all_magical_defense", userHIIN.PercentAllMagicalDefense);
        command.Parameters.AddWithValue("@percent_all_chemical_attack", userHIIN.PercentAllChemicalAttack);
        command.Parameters.AddWithValue("@percent_all_chemical_defense", userHIIN.PercentAllChemicalDefense);
        command.Parameters.AddWithValue("@percent_all_atomic_attack", userHIIN.PercentAllAtomicAttack);
        command.Parameters.AddWithValue("@percent_all_atomic_defense", userHIIN.PercentAllAtomicDefense);
        command.Parameters.AddWithValue("@percent_all_mental_attack", userHIIN.PercentAllMentalAttack);
        command.Parameters.AddWithValue("@percent_all_mental_defense", userHIIN.PercentAllMentalDefense);
    }
}