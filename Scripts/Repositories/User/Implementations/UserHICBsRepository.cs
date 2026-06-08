using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserHICBsRepository : IUserHICBsRepository
{
    public async Task<UserHICBs> GetUserHICBsAsync(string id)
    {
        UserHICBs userHICB = new UserHICBs();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT *
                FROM user_hicbs
                WHERE user_id = @user_id AND hicb_id = @hicb_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@hicb_id", id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userHICB.Id = reader.GetStringSafe("hicb_id");
                            userHICB.Level = reader.GetIntSafe("hicb_level");
                            userHICB.Power = reader.GetDoubleSafe("power");
                            userHICB.Health = reader.GetDoubleSafe("health");
                            userHICB.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            userHICB.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            userHICB.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            userHICB.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            userHICB.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            userHICB.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            userHICB.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            userHICB.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            userHICB.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            userHICB.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            userHICB.Speed = reader.GetDoubleSafe("speed");
                            userHICB.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            userHICB.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            userHICB.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            userHICB.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            userHICB.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            userHICB.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            userHICB.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            userHICB.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            userHICB.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            userHICB.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            userHICB.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            userHICB.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            userHICB.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            userHICB.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            userHICB.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            userHICB.Tenacity = reader.GetDoubleSafe("tenacity");
                            userHICB.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            userHICB.ComboRate = reader.GetDoubleSafe("combo_rate");
                            userHICB.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            userHICB.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            userHICB.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            userHICB.StunRate = reader.GetDoubleSafe("stun_rate");
                            userHICB.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            userHICB.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            userHICB.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            userHICB.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            userHICB.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            userHICB.Mana = reader.GetDoubleSafe("mana");
                            userHICB.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            userHICB.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            userHICB.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            userHICB.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            userHICB.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            userHICB.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            userHICB.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            userHICB.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            userHICB.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            userHICB.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            userHICB.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            userHICB.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            userHICB.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            userHICB.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            userHICB.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            userHICB.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            userHICB.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            userHICB.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            userHICB.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            userHICB.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
                return userHICB;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task InsertOrUpdateUserHICBsAsync(string userId, UserHICBs userHICB, string id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string checkSQL = @"
            SELECT COUNT(*) FROM user_hicbs 
            WHERE user_id = @user_id AND hicb_id = @hicb_id";

            await using (var checkCommand = new MySqlCommand(checkSQL, connection))
            {
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@hicb_id", id);

                int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                if (count > 0)
                {
                    // -------- UPDATE ----------
                    string updateSQL = @"
                    UPDATE user_hicbs
                    SET
                        hicb_level = @hicb_level, power = @power, health = @health, mana = @mana, speed = @speed,
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
                    AND hicb_id = @hicb_id;
                ";

                    await using var updateCommand = new MySqlCommand(updateSQL, connection);
                    AddAllParameters(updateCommand, userHICB, userId, id);

                    await updateCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // -------- INSERT ----------
                    string insertSQL = @"
                    INSERT INTO user_hicbs (
                    user_id, hicb_id, hicb_level, power, health, mana, speed,
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
                    @user_id, @hicb_id, @hicb_level, @power, @health, @mana, @speed,
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
                    AddAllParameters(insertCommand, userHICB, userId, id);

                    await insertCommand.ExecuteNonQueryAsync();
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    public async Task<UserHICBs> GetSumUserHICBsAsync(string userId)
    {
        UserHICBs userHICBs = new UserHICBs();
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
                FROM user_hicbs 
                WHERE user_id = @user_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userHICBs.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            userHICBs.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            userHICBs.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            userHICBs.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            userHICBs.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            userHICBs.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            userHICBs.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            userHICBs.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            userHICBs.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            userHICBs.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            userHICBs.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            userHICBs.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            userHICBs.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            userHICBs.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            userHICBs.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            userHICBs.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            userHICBs.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            userHICBs.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            userHICBs.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            userHICBs.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            userHICBs.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            userHICBs.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            userHICBs.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            userHICBs.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            userHICBs.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            userHICBs.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            userHICBs.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            userHICBs.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            userHICBs.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            userHICBs.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            userHICBs.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            userHICBs.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            userHICBs.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            userHICBs.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            userHICBs.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            userHICBs.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            userHICBs.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            userHICBs.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            userHICBs.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            userHICBs.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            userHICBs.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            userHICBs.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            userHICBs.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            userHICBs.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            userHICBs.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            userHICBs.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            userHICBs.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            userHICBs.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            userHICBs.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            userHICBs.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            userHICBs.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            userHICBs.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            userHICBs.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            userHICBs.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            userHICBs.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            userHICBs.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            userHICBs.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            userHICBs.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            userHICBs.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            userHICBs.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            userHICBs.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return userHICBs;
    }
    private void AddAllParameters(MySqlCommand command, UserHICBs userHICB, string userId, string type)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@hicb_id", type);

        command.Parameters.AddWithValue("@hicb_level", userHICB.Level == 0 ? 1 : userHICB.Level);
        command.Parameters.AddWithValue("@power", userHICB.Power);
        command.Parameters.AddWithValue("@health", userHICB.Health);
        command.Parameters.AddWithValue("@mana", userHICB.Mana);
        command.Parameters.AddWithValue("@speed", userHICB.Speed);

        command.Parameters.AddWithValue("@physical_attack", userHICB.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", userHICB.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", userHICB.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", userHICB.MagicalDefense);

        command.Parameters.AddWithValue("@chemical_attack", userHICB.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", userHICB.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", userHICB.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", userHICB.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", userHICB.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", userHICB.MentalDefense);

        command.Parameters.AddWithValue("@critical_damage_rate", userHICB.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", userHICB.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", userHICB.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", userHICB.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", userHICB.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@penetration_rate", userHICB.PenetrationRate);
        command.Parameters.AddWithValue("@evasion_rate", userHICB.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", userHICB.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", userHICB.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", userHICB.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", userHICB.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", userHICB.VitalityRegenerationResistanceRate);

        command.Parameters.AddWithValue("@accuracy_rate", userHICB.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", userHICB.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", userHICB.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", userHICB.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", userHICB.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", userHICB.ComboRate);
        command.Parameters.AddWithValue("@reflection_rate", userHICB.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", userHICB.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", userHICB.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", userHICB.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", userHICB.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", userHICB.IgnoreStunRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", userHICB.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", userHICB.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", userHICB.ReflectionResistanceRate);

        command.Parameters.AddWithValue("@mana_regeneration_rate", userHICB.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", userHICB.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", userHICB.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", userHICB.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", userHICB.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", userHICB.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", userHICB.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", userHICB.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", userHICB.SkillResistanceRate);

        command.Parameters.AddWithValue("@percent_all_health", userHICB.PercentAllHealth);
        command.Parameters.AddWithValue("@percent_all_physical_attack", userHICB.PercentAllPhysicalAttack);
        command.Parameters.AddWithValue("@percent_all_physical_defense", userHICB.PercentAllPhysicalDefense);
        command.Parameters.AddWithValue("@percent_all_magical_attack", userHICB.PercentAllMagicalAttack);
        command.Parameters.AddWithValue("@percent_all_magical_defense", userHICB.PercentAllMagicalDefense);
        command.Parameters.AddWithValue("@percent_all_chemical_attack", userHICB.PercentAllChemicalAttack);
        command.Parameters.AddWithValue("@percent_all_chemical_defense", userHICB.PercentAllChemicalDefense);
        command.Parameters.AddWithValue("@percent_all_atomic_attack", userHICB.PercentAllAtomicAttack);
        command.Parameters.AddWithValue("@percent_all_atomic_defense", userHICB.PercentAllAtomicDefense);
        command.Parameters.AddWithValue("@percent_all_mental_attack", userHICB.PercentAllMentalAttack);
        command.Parameters.AddWithValue("@percent_all_mental_defense", userHICB.PercentAllMentalDefense);
    }
}