using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserHICAsRepository : IUserHICAsRepository
{
    public async Task<UserHICAs> GetUserHICAsAsync(string id)
    {
        UserHICAs userHICA = new UserHICAs();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT *
                FROM user_hicas
                WHERE user_id = @user_id AND hica_id = @hica_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@hica_id", id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userHICA.Id = reader.GetStringSafe("hica_id");
                            userHICA.Level = reader.GetIntSafe("hica_level");
                            userHICA.Power = reader.GetDoubleSafe("power");
                            userHICA.Health = reader.GetDoubleSafe("health");
                            userHICA.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            userHICA.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            userHICA.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            userHICA.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            userHICA.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            userHICA.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            userHICA.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            userHICA.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            userHICA.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            userHICA.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            userHICA.Speed = reader.GetDoubleSafe("speed");
                            userHICA.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            userHICA.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            userHICA.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            userHICA.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            userHICA.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            userHICA.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            userHICA.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            userHICA.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            userHICA.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            userHICA.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            userHICA.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            userHICA.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            userHICA.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            userHICA.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            userHICA.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            userHICA.Tenacity = reader.GetDoubleSafe("tenacity");
                            userHICA.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            userHICA.ComboRate = reader.GetDoubleSafe("combo_rate");
                            userHICA.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            userHICA.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            userHICA.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            userHICA.StunRate = reader.GetDoubleSafe("stun_rate");
                            userHICA.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            userHICA.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            userHICA.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            userHICA.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            userHICA.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            userHICA.Mana = reader.GetDoubleSafe("mana");
                            userHICA.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            userHICA.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            userHICA.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            userHICA.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            userHICA.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            userHICA.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            userHICA.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            userHICA.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            userHICA.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            userHICA.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            userHICA.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            userHICA.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            userHICA.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            userHICA.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            userHICA.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            userHICA.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            userHICA.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            userHICA.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            userHICA.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            userHICA.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
                return userHICA;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task InsertOrUpdateUserHICAsAsync(string userId, UserHICAs userHICA, string id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string checkSQL = @"
            SELECT COUNT(*) FROM user_hicas 
            WHERE user_id = @user_id AND hica_id = @hica_id";

            await using (var checkCommand = new MySqlCommand(checkSQL, connection))
            {
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@hica_id", id);

                int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                if (count > 0)
                {
                    // -------- UPDATE ----------
                    string updateSQL = @"
                    UPDATE user_hicas
                    SET
                        hica_level = @hica_level, power = @power, health = @health, mana = @mana, speed = @speed,
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
                    AND hica_id = @hica_id;
                ";

                    await using var updateCommand = new MySqlCommand(updateSQL, connection);
                    AddAllParameters(updateCommand, userHICA, userId, id);

                    await updateCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // -------- INSERT ----------
                    string insertSQL = @"
                    INSERT INTO user_hicas (
                    user_id, hica_id, hica_level, power, health, mana, speed,
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
                    @user_id, @hica_id, @hica_level, @power, @health, @mana, @speed,
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
                    AddAllParameters(insertCommand, userHICA, userId, id);

                    await insertCommand.ExecuteNonQueryAsync();
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    public async Task<UserHICAs> GetSumUserHICAsAsync(string userId)
    {
        UserHICAs userHICAs = new UserHICAs();
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
                FROM user_hicas 
                WHERE user_id = @user_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userHICAs.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            userHICAs.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            userHICAs.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            userHICAs.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            userHICAs.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            userHICAs.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            userHICAs.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            userHICAs.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            userHICAs.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            userHICAs.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            userHICAs.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            userHICAs.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            userHICAs.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            userHICAs.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            userHICAs.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            userHICAs.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            userHICAs.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            userHICAs.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            userHICAs.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            userHICAs.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            userHICAs.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            userHICAs.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            userHICAs.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            userHICAs.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            userHICAs.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            userHICAs.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            userHICAs.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            userHICAs.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            userHICAs.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            userHICAs.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            userHICAs.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            userHICAs.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            userHICAs.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            userHICAs.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            userHICAs.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            userHICAs.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            userHICAs.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            userHICAs.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            userHICAs.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            userHICAs.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            userHICAs.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            userHICAs.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            userHICAs.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            userHICAs.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            userHICAs.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            userHICAs.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            userHICAs.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            userHICAs.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            userHICAs.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            userHICAs.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            userHICAs.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            userHICAs.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            userHICAs.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            userHICAs.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            userHICAs.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            userHICAs.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            userHICAs.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            userHICAs.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            userHICAs.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            userHICAs.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            userHICAs.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return userHICAs;
    }
    private void AddAllParameters(MySqlCommand command, UserHICAs userHICA, string userId, string type)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@hica_id", type);

        command.Parameters.AddWithValue("@hica_level", userHICA.Level == 0 ? 1 : userHICA.Level);
        command.Parameters.AddWithValue("@power", userHICA.Power);
        command.Parameters.AddWithValue("@health", userHICA.Health);
        command.Parameters.AddWithValue("@mana", userHICA.Mana);
        command.Parameters.AddWithValue("@speed", userHICA.Speed);

        command.Parameters.AddWithValue("@physical_attack", userHICA.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", userHICA.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", userHICA.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", userHICA.MagicalDefense);

        command.Parameters.AddWithValue("@chemical_attack", userHICA.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", userHICA.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", userHICA.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", userHICA.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", userHICA.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", userHICA.MentalDefense);

        command.Parameters.AddWithValue("@critical_damage_rate", userHICA.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", userHICA.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", userHICA.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", userHICA.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", userHICA.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@penetration_rate", userHICA.PenetrationRate);
        command.Parameters.AddWithValue("@evasion_rate", userHICA.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", userHICA.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", userHICA.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", userHICA.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", userHICA.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", userHICA.VitalityRegenerationResistanceRate);

        command.Parameters.AddWithValue("@accuracy_rate", userHICA.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", userHICA.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", userHICA.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", userHICA.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", userHICA.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", userHICA.ComboRate);
        command.Parameters.AddWithValue("@reflection_rate", userHICA.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", userHICA.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", userHICA.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", userHICA.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", userHICA.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", userHICA.IgnoreStunRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", userHICA.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", userHICA.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", userHICA.ReflectionResistanceRate);

        command.Parameters.AddWithValue("@mana_regeneration_rate", userHICA.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", userHICA.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", userHICA.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", userHICA.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", userHICA.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", userHICA.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", userHICA.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", userHICA.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", userHICA.SkillResistanceRate);

        command.Parameters.AddWithValue("@percent_all_health", userHICA.PercentAllHealth);
        command.Parameters.AddWithValue("@percent_all_physical_attack", userHICA.PercentAllPhysicalAttack);
        command.Parameters.AddWithValue("@percent_all_physical_defense", userHICA.PercentAllPhysicalDefense);
        command.Parameters.AddWithValue("@percent_all_magical_attack", userHICA.PercentAllMagicalAttack);
        command.Parameters.AddWithValue("@percent_all_magical_defense", userHICA.PercentAllMagicalDefense);
        command.Parameters.AddWithValue("@percent_all_chemical_attack", userHICA.PercentAllChemicalAttack);
        command.Parameters.AddWithValue("@percent_all_chemical_defense", userHICA.PercentAllChemicalDefense);
        command.Parameters.AddWithValue("@percent_all_atomic_attack", userHICA.PercentAllAtomicAttack);
        command.Parameters.AddWithValue("@percent_all_atomic_defense", userHICA.PercentAllAtomicDefense);
        command.Parameters.AddWithValue("@percent_all_mental_attack", userHICA.PercentAllMentalAttack);
        command.Parameters.AddWithValue("@percent_all_mental_defense", userHICA.PercentAllMentalDefense);
    }
}