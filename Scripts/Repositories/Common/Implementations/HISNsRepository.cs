using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class HISNsRepository : IHISNsRepository
{
    public async Task<HISNs> GetHISNsAsync(string id)
    {
        HISNs hisn = new HISNs();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT *
                FROM HISNs
                WHERE user_id = @user_id AND hisn_id = @hisn_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@hisn_id", id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            hisn.Id = reader.GetStringSafe("hisn_id");
                            hisn.Level = reader.GetIntSafe("hisn_level");
                            hisn.Power = reader.GetDoubleSafe("power");
                            hisn.Health = reader.GetDoubleSafe("health");
                            hisn.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            hisn.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            hisn.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            hisn.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            hisn.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            hisn.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            hisn.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            hisn.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            hisn.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            hisn.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            hisn.Speed = reader.GetDoubleSafe("speed");
                            hisn.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            hisn.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            hisn.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            hisn.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            hisn.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            hisn.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            hisn.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            hisn.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            hisn.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            hisn.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            hisn.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            hisn.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            hisn.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            hisn.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            hisn.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            hisn.Tenacity = reader.GetDoubleSafe("tenacity");
                            hisn.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            hisn.ComboRate = reader.GetDoubleSafe("combo_rate");
                            hisn.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            hisn.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            hisn.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            hisn.StunRate = reader.GetDoubleSafe("stun_rate");
                            hisn.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            hisn.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            hisn.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            hisn.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            hisn.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            hisn.Mana = reader.GetDoubleSafe("mana");
                            hisn.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            hisn.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            hisn.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            hisn.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            hisn.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            hisn.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            hisn.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            hisn.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            hisn.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            hisn.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            hisn.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            hisn.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            hisn.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            hisn.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            hisn.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            hisn.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            hisn.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            hisn.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            hisn.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            hisn.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
                return hisn;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task InsertOrUpdateHISNsAsync(string user_id, HISNs HISNs, string id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string checkSQL = @"
            SELECT COUNT(*) FROM HISNs 
            WHERE user_id = @user_id AND hisn_id = @hisn_id";

            await using (var checkCommand = new MySqlCommand(checkSQL, connection))
            {
                checkCommand.Parameters.AddWithValue("@user_id", user_id);
                checkCommand.Parameters.AddWithValue("@hisn_id", id);

                int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                if (count > 0)
                {
                    // -------- UPDATE ----------
                    string updateSQL = @"
                    UPDATE HISNs
                    SET
                        hisn_level = @hisn_level, power = @power, health = @health, mana = @mana, speed = @speed,
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
                    AND hisn_id = @hisn_id;
                ";

                    await using var updateCommand = new MySqlCommand(updateSQL, connection);
                    AddAllParameters(updateCommand, HISNs, user_id, id);

                    await updateCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // -------- INSERT ----------
                    string insertSQL = @"
                    INSERT INTO HISNs (
                    user_id, hisn_id, hisn_level, power, health, mana, speed,
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
                    @user_id, @hisn_id, @hisn_level, @power, @health, @mana, @speed,
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
                    AddAllParameters(insertCommand, HISNs, user_id, id);

                    await insertCommand.ExecuteNonQueryAsync();
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    public async Task<HISNs> GetSumHISNsAsync(string user_id)
    {
        HISNs hisn = new HISNs();
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
                FROM HISNs 
                WHERE user_id = @user_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            hisn.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            hisn.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            hisn.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            hisn.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            hisn.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            hisn.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            hisn.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            hisn.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            hisn.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            hisn.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            hisn.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            hisn.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            hisn.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            hisn.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            hisn.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            hisn.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            hisn.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            hisn.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            hisn.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            hisn.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            hisn.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            hisn.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            hisn.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            hisn.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            hisn.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            hisn.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            hisn.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            hisn.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            hisn.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            hisn.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            hisn.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            hisn.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            hisn.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            hisn.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            hisn.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            hisn.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            hisn.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            hisn.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            hisn.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            hisn.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            hisn.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            hisn.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            hisn.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            hisn.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            hisn.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            hisn.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            hisn.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            hisn.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            hisn.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            hisn.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            hisn.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            hisn.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            hisn.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            hisn.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            hisn.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            hisn.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            hisn.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            hisn.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            hisn.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            hisn.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            hisn.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return hisn;
    }
    private void AddAllParameters(MySqlCommand cmd, HISNs a, string user_id, string type)
    {
        cmd.Parameters.AddWithValue("@user_id", user_id);
        cmd.Parameters.AddWithValue("@hisn_id", type);

        cmd.Parameters.AddWithValue("@hisn_level", a.Level == 0 ? 1 : a.Level);
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
        cmd.Parameters.AddWithValue("@critical_resistance_rate", a.CriticalResistanceRate);
        cmd.Parameters.AddWithValue("@ignore_critical_rate", a.IgnoreCriticalRate);
        cmd.Parameters.AddWithValue("@penetration_resistance_rate", a.PenetrationResistanceRate);
        cmd.Parameters.AddWithValue("@penetration_rate", a.PenetrationRate);
        cmd.Parameters.AddWithValue("@evasion_rate", a.EvasionRate);
        cmd.Parameters.AddWithValue("@damage_absorption_rate", a.DamageAbsorptionRate);
        cmd.Parameters.AddWithValue("@vitality_regeneration_rate", a.VitalityRegenerationRate);
        cmd.Parameters.AddWithValue("@ignore_damage_absorption_rate", a.IgnoreDamageAbsorptionRate);
        cmd.Parameters.AddWithValue("@absorbed_damage_rate", a.AbsorbedDamageRate);
        cmd.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", a.VitalityRegenerationResistanceRate);

        cmd.Parameters.AddWithValue("@accuracy_rate", a.AccuracyRate);
        cmd.Parameters.AddWithValue("@lifesteal_rate", a.LifestealRate);
        cmd.Parameters.AddWithValue("@shield_strength", a.ShieldStrength);
        cmd.Parameters.AddWithValue("@tenacity", a.Tenacity);
        cmd.Parameters.AddWithValue("@resistance_rate", a.ResistanceRate);
        cmd.Parameters.AddWithValue("@combo_rate", a.ComboRate);
        cmd.Parameters.AddWithValue("@reflection_rate", a.ReflectionRate);
        cmd.Parameters.AddWithValue("@ignore_combo_rate", a.IgnoreComboRate);
        cmd.Parameters.AddWithValue("@combo_damage_rate", a.ComboDamageRate);
        cmd.Parameters.AddWithValue("@combo_resistance_rate", a.ComboResistanceRate);
        cmd.Parameters.AddWithValue("@stun_rate", a.StunRate);
        cmd.Parameters.AddWithValue("@ignore_stun_rate", a.IgnoreStunRate);
        cmd.Parameters.AddWithValue("@ignore_reflection_rate", a.IgnoreReflectionRate);
        cmd.Parameters.AddWithValue("@reflection_damage_rate", a.ReflectionDamageRate);
        cmd.Parameters.AddWithValue("@reflection_resistance_rate", a.ReflectionResistanceRate);

        cmd.Parameters.AddWithValue("@mana_regeneration_rate", a.ManaRegenerationRate);
        cmd.Parameters.AddWithValue("@damage_to_different_faction_rate", a.DamageToDifferentFactionRate);
        cmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", a.ResistanceToDifferentFactionRate);
        cmd.Parameters.AddWithValue("@damage_to_same_faction_rate", a.DamageToSameFactionRate);
        cmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", a.ResistanceToSameFactionRate);
        cmd.Parameters.AddWithValue("@normal_damage_rate", a.NormalDamageRate);
        cmd.Parameters.AddWithValue("@normal_resistance_rate", a.NormalResistanceRate);
        cmd.Parameters.AddWithValue("@skill_damage_rate", a.SkillDamageRate);
        cmd.Parameters.AddWithValue("@skill_resistance_rate", a.SkillResistanceRate);

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