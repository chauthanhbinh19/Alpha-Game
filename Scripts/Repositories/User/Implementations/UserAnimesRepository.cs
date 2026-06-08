using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserAnimesRepository : IUserAnimesRepository
{
    public async Task<UserAnimes> GetUserAnimesAsync(string id)
    {
        UserAnimes userAnime = new UserAnimes();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT *
                FROM user_animes
                WHERE user_id = @user_id AND anime_id = @anime_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@anime_id", id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userAnime.Id = reader.GetStringSafe("anime_id");
                            userAnime.Level = reader.GetIntSafe("anime_level");
                            userAnime.Power = reader.GetDoubleSafe("power");
                            userAnime.Health = reader.GetDoubleSafe("health");
                            userAnime.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            userAnime.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            userAnime.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            userAnime.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            userAnime.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            userAnime.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            userAnime.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            userAnime.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            userAnime.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            userAnime.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            userAnime.Speed = reader.GetDoubleSafe("speed");
                            userAnime.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            userAnime.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            userAnime.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            userAnime.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            userAnime.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            userAnime.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            userAnime.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            userAnime.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            userAnime.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            userAnime.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            userAnime.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            userAnime.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            userAnime.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            userAnime.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            userAnime.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            userAnime.Tenacity = reader.GetDoubleSafe("tenacity");
                            userAnime.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            userAnime.ComboRate = reader.GetDoubleSafe("combo_rate");
                            userAnime.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            userAnime.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            userAnime.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            userAnime.StunRate = reader.GetDoubleSafe("stun_rate");
                            userAnime.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            userAnime.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            userAnime.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            userAnime.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            userAnime.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            userAnime.Mana = reader.GetDoubleSafe("mana");
                            userAnime.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            userAnime.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            userAnime.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            userAnime.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            userAnime.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            userAnime.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            userAnime.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            userAnime.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            userAnime.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            userAnime.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            userAnime.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            userAnime.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            userAnime.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            userAnime.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            userAnime.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            userAnime.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            userAnime.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            userAnime.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            userAnime.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            userAnime.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
                return userAnime;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task InsertOrUpdateUserAnimesAsync(string userId, UserAnimes userAnime, string id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string checkSQL = @"
            SELECT COUNT(*) FROM user_animes 
            WHERE user_id = @user_id AND anime_id = @anime_id";

            await using (var checkCommand = new MySqlCommand(checkSQL, connection))
            {
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                checkCommand.Parameters.AddWithValue("@anime_id", id);

                int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                if (count > 0)
                {
                    // -------- UPDATE ----------
                    string updateSQL = @"
                    UPDATE user_animes
                    SET
                        anime_level = @anime_level, power = @power, health = @health, mana = @mana, speed = @speed,
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
                    AND anime_id = @anime_id;
                ";

                    await using var updateCommand = new MySqlCommand(updateSQL, connection);
                    AddAllParameters(updateCommand, userAnime, userId, id);

                    await updateCommand.ExecuteNonQueryAsync();
                }
                else
                {
                    // -------- INSERT ----------
                    string insertSQL = @"
                    INSERT INTO user_animes (
                    user_id, anime_id, anime_level, power, health, mana, speed,
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
                    @user_id, @anime_id, @anime_level, @power, @health, @mana, @speed,
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
                    AddAllParameters(insertCommand, userAnime, userId, id);

                    await insertCommand.ExecuteNonQueryAsync();
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    public async Task<UserAnimes> GetSumUserAnimesAsync(string userId)
    {
        UserAnimes userAnimes = new UserAnimes();
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
                FROM user_animes 
                WHERE user_id = @user_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userAnimes.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            userAnimes.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            userAnimes.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            userAnimes.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            userAnimes.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            userAnimes.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            userAnimes.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            userAnimes.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            userAnimes.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            userAnimes.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            userAnimes.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            userAnimes.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            userAnimes.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            userAnimes.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            userAnimes.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            userAnimes.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            userAnimes.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            userAnimes.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            userAnimes.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            userAnimes.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            userAnimes.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            userAnimes.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            userAnimes.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            userAnimes.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            userAnimes.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            userAnimes.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            userAnimes.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            userAnimes.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            userAnimes.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            userAnimes.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            userAnimes.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            userAnimes.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            userAnimes.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            userAnimes.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            userAnimes.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            userAnimes.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            userAnimes.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            userAnimes.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            userAnimes.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            userAnimes.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            userAnimes.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            userAnimes.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            userAnimes.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            userAnimes.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            userAnimes.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            userAnimes.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            userAnimes.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            userAnimes.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            userAnimes.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            userAnimes.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            userAnimes.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            userAnimes.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            userAnimes.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            userAnimes.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            userAnimes.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            userAnimes.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            userAnimes.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            userAnimes.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            userAnimes.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            userAnimes.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            userAnimes.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return userAnimes;
    }
    private void AddAllParameters(MySqlCommand command, UserAnimes userAnime, string userId, string type)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@anime_id", type);

        command.Parameters.AddWithValue("@anime_level", userAnime.Level == 0 ? 1 : userAnime.Level);
        command.Parameters.AddWithValue("@power", userAnime.Power);
        command.Parameters.AddWithValue("@health", userAnime.Health);
        command.Parameters.AddWithValue("@mana", userAnime.Mana);
        command.Parameters.AddWithValue("@speed", userAnime.Speed);

        command.Parameters.AddWithValue("@physical_attack", userAnime.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", userAnime.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", userAnime.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", userAnime.MagicalDefense);

        command.Parameters.AddWithValue("@chemical_attack", userAnime.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", userAnime.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", userAnime.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", userAnime.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", userAnime.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", userAnime.MentalDefense);

        command.Parameters.AddWithValue("@critical_damage_rate", userAnime.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", userAnime.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", userAnime.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", userAnime.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", userAnime.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@penetration_rate", userAnime.PenetrationRate);
        command.Parameters.AddWithValue("@evasion_rate", userAnime.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", userAnime.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", userAnime.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", userAnime.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", userAnime.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", userAnime.VitalityRegenerationResistanceRate);

        command.Parameters.AddWithValue("@accuracy_rate", userAnime.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", userAnime.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", userAnime.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", userAnime.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", userAnime.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", userAnime.ComboRate);
        command.Parameters.AddWithValue("@reflection_rate", userAnime.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", userAnime.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", userAnime.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", userAnime.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", userAnime.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", userAnime.IgnoreStunRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", userAnime.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", userAnime.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", userAnime.ReflectionResistanceRate);

        command.Parameters.AddWithValue("@mana_regeneration_rate", userAnime.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", userAnime.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", userAnime.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", userAnime.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", userAnime.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", userAnime.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", userAnime.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", userAnime.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", userAnime.SkillResistanceRate);

        command.Parameters.AddWithValue("@percent_all_health", userAnime.PercentAllHealth);
        command.Parameters.AddWithValue("@percent_all_physical_attack", userAnime.PercentAllPhysicalAttack);
        command.Parameters.AddWithValue("@percent_all_physical_defense", userAnime.PercentAllPhysicalDefense);
        command.Parameters.AddWithValue("@percent_all_magical_attack", userAnime.PercentAllMagicalAttack);
        command.Parameters.AddWithValue("@percent_all_magical_defense", userAnime.PercentAllMagicalDefense);
        command.Parameters.AddWithValue("@percent_all_chemical_attack", userAnime.PercentAllChemicalAttack);
        command.Parameters.AddWithValue("@percent_all_chemical_defense", userAnime.PercentAllChemicalDefense);
        command.Parameters.AddWithValue("@percent_all_atomic_attack", userAnime.PercentAllAtomicAttack);
        command.Parameters.AddWithValue("@percent_all_atomic_defense", userAnime.PercentAllAtomicDefense);
        command.Parameters.AddWithValue("@percent_all_mental_attack", userAnime.PercentAllMentalAttack);
        command.Parameters.AddWithValue("@percent_all_mental_defense", userAnime.PercentAllMentalDefense);
    }
}