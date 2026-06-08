using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserArchivesRepository : IUserArchivesRepository
{
    public async Task<UserArchives> GetUserArchivesAsync(string id)
    {
        UserArchives userArchive = new UserArchives();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT *
                FROM user_archives
                WHERE user_id = @user_id AND archive_id = @archive_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@archive_id", id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userArchive.Id = reader.GetStringSafe("archive_id");
                            userArchive.Level = reader.GetIntSafe("archive_level");
                            userArchive.Power = reader.GetDoubleSafe("power");
                            userArchive.Health = reader.GetDoubleSafe("health");
                            userArchive.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            userArchive.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            userArchive.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            userArchive.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            userArchive.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            userArchive.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            userArchive.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            userArchive.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            userArchive.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            userArchive.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            userArchive.Speed = reader.GetDoubleSafe("speed");
                            userArchive.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            userArchive.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            userArchive.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            userArchive.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            userArchive.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            userArchive.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            userArchive.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            userArchive.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            userArchive.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            userArchive.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            userArchive.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            userArchive.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            userArchive.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            userArchive.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            userArchive.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            userArchive.Tenacity = reader.GetDoubleSafe("tenacity");
                            userArchive.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            userArchive.ComboRate = reader.GetDoubleSafe("combo_rate");
                            userArchive.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            userArchive.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            userArchive.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            userArchive.StunRate = reader.GetDoubleSafe("stun_rate");
                            userArchive.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            userArchive.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            userArchive.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            userArchive.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            userArchive.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            userArchive.Mana = reader.GetDoubleSafe("mana");
                            userArchive.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            userArchive.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            userArchive.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            userArchive.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            userArchive.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            userArchive.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            userArchive.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            userArchive.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            userArchive.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            userArchive.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            userArchive.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            userArchive.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            userArchive.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            userArchive.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            userArchive.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            userArchive.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            userArchive.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            userArchive.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            userArchive.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            userArchive.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
                return userArchive;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task InsertOrUpdateUserArchivesAsync(string userId, UserArchives userArchives, string id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT COUNT(*) FROM user_archives 
            WHERE user_id = @user_id AND archive_id = @archive_id";

            await using (var selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@user_id", userId);
                selectCommand.Parameters.AddWithValue("@archive_id", id);

                int count = Convert.ToInt32(await selectCommand.ExecuteScalarAsync());

                if (count > 0)
                {
                    // -------- UPDATE ----------
                    string updateSQL = @"
                    UPDATE user_archives
                    SET
                        archive_level = @archive_level, power = @power, health = @health, mana = @mana, speed = @speed,
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
                    AND archive_id = @archive_id;
                ";

                    await using var updateCommand = new MySqlCommand(updateSQL, connection);
                    AddAllParameters(updateCommand, userArchives, userId, id);

                    await updateCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // -------- INSERT ----------
                    string insertSQL = @"
                    INSERT INTO user_archives (
                    user_id, archive_id, archive_level, power, health, mana, speed,
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
                    @user_id, @archive_id, @archive_level, @power, @health, @mana, @speed,
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
                    AddAllParameters(insertCommand, userArchives, userId, id);

                    await insertCommand.ExecuteNonQueryAsync();
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    public async Task<UserArchives> GetSumUserArchivesAsync(string userId)
    {
        UserArchives userArchives = new UserArchives();
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
                FROM user_archives 
                WHERE user_id = @user_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userArchives.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            userArchives.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            userArchives.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            userArchives.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            userArchives.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            userArchives.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            userArchives.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            userArchives.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            userArchives.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            userArchives.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            userArchives.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            userArchives.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            userArchives.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            userArchives.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            userArchives.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            userArchives.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            userArchives.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            userArchives.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            userArchives.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            userArchives.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            userArchives.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            userArchives.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            userArchives.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            userArchives.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            userArchives.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            userArchives.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            userArchives.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            userArchives.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            userArchives.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            userArchives.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            userArchives.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            userArchives.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            userArchives.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            userArchives.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            userArchives.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            userArchives.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            userArchives.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            userArchives.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            userArchives.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            userArchives.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            userArchives.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            userArchives.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            userArchives.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            userArchives.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            userArchives.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            userArchives.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            userArchives.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            userArchives.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            userArchives.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            userArchives.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            userArchives.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            userArchives.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            userArchives.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            userArchives.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            userArchives.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            userArchives.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            userArchives.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            userArchives.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            userArchives.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            userArchives.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            userArchives.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return userArchives;
    }
    private void AddAllParameters(MySqlCommand command, UserArchives userArchive, string userId, string type)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@archive_id", type);

        command.Parameters.AddWithValue("@archive_level", userArchive.Level == 0 ? 1 : userArchive.Level);
        command.Parameters.AddWithValue("@power", userArchive.Power);
        command.Parameters.AddWithValue("@health", userArchive.Health);
        command.Parameters.AddWithValue("@mana", userArchive.Mana);
        command.Parameters.AddWithValue("@speed", userArchive.Speed);

        command.Parameters.AddWithValue("@physical_attack", userArchive.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", userArchive.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", userArchive.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", userArchive.MagicalDefense);

        command.Parameters.AddWithValue("@chemical_attack", userArchive.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", userArchive.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", userArchive.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", userArchive.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", userArchive.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", userArchive.MentalDefense);

        command.Parameters.AddWithValue("@critical_damage_rate", userArchive.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", userArchive.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", userArchive.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", userArchive.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", userArchive.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@penetration_rate", userArchive.PenetrationRate);
        command.Parameters.AddWithValue("@evasion_rate", userArchive.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", userArchive.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", userArchive.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", userArchive.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", userArchive.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", userArchive.VitalityRegenerationResistanceRate);

        command.Parameters.AddWithValue("@accuracy_rate", userArchive.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", userArchive.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", userArchive.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", userArchive.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", userArchive.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", userArchive.ComboRate);
        command.Parameters.AddWithValue("@reflection_rate", userArchive.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", userArchive.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", userArchive.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", userArchive.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", userArchive.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", userArchive.IgnoreStunRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", userArchive.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", userArchive.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", userArchive.ReflectionResistanceRate);

        command.Parameters.AddWithValue("@mana_regeneration_rate", userArchive.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", userArchive.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", userArchive.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", userArchive.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", userArchive.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", userArchive.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", userArchive.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", userArchive.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", userArchive.SkillResistanceRate);

        command.Parameters.AddWithValue("@percent_all_health", userArchive.PercentAllHealth);
        command.Parameters.AddWithValue("@percent_all_physical_attack", userArchive.PercentAllPhysicalAttack);
        command.Parameters.AddWithValue("@percent_all_physical_defense", userArchive.PercentAllPhysicalDefense);
        command.Parameters.AddWithValue("@percent_all_magical_attack", userArchive.PercentAllMagicalAttack);
        command.Parameters.AddWithValue("@percent_all_magical_defense", userArchive.PercentAllMagicalDefense);
        command.Parameters.AddWithValue("@percent_all_chemical_attack", userArchive.PercentAllChemicalAttack);
        command.Parameters.AddWithValue("@percent_all_chemical_defense", userArchive.PercentAllChemicalDefense);
        command.Parameters.AddWithValue("@percent_all_atomic_attack", userArchive.PercentAllAtomicAttack);
        command.Parameters.AddWithValue("@percent_all_atomic_defense", userArchive.PercentAllAtomicDefense);
        command.Parameters.AddWithValue("@percent_all_mental_attack", userArchive.PercentAllMentalAttack);
        command.Parameters.AddWithValue("@percent_all_mental_defense", userArchive.PercentAllMentalDefense);
    }
}