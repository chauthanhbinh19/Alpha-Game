using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserModulesRepository : IUserModulesRepository
{
    public async Task<UserModules> GetUserModulesAsync(string id)
    {
        UserModules universe = new UserModules();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT *
                FROM user_universes
                WHERE user_id = @user_id AND universe_id = @universe_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@universe_id", id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            universe.Id = reader.GetStringSafe("Module_id");
                            universe.Level = reader.GetIntSafe("Module_level");
                            universe.Power = reader.GetDoubleSafe("power");
                            universe.Health = reader.GetDoubleSafe("health");
                            universe.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            universe.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            universe.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            universe.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            universe.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            universe.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            universe.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            universe.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            universe.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            universe.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            universe.Speed = reader.GetDoubleSafe("speed");
                            universe.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            universe.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            universe.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            universe.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            universe.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            universe.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            universe.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            universe.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            universe.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            universe.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            universe.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            universe.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            universe.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            universe.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            universe.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            universe.Tenacity = reader.GetDoubleSafe("tenacity");
                            universe.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            universe.ComboRate = reader.GetDoubleSafe("combo_rate");
                            universe.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            universe.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            universe.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            universe.StunRate = reader.GetDoubleSafe("stun_rate");
                            universe.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            universe.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            universe.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            universe.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            universe.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            universe.Mana = reader.GetDoubleSafe("mana");
                            universe.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            universe.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            universe.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            universe.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            universe.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            universe.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            universe.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            universe.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            universe.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            universe.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            universe.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            universe.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            universe.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            universe.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            universe.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            universe.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            universe.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            universe.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            universe.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            universe.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
                return universe;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task InsertOrUpdateUserModulesAsync(string user_id, UserModules Modules, string id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string checkSQL = @"
            INSERT INTO user_card_heroes_module (
                user_id,
                user_card_hero_id,
                module_id,
                current_level,
                current_multiplier
            )
            VALUES (
                @user_id,
                @user_card_hero_id,
                @module_id,
                @current_level,
                @current_multiplier
            ) AS new
            ON DUPLICATE KEY UPDATE
                current_level = new.current_level,
                current_multiplier = new.current_multiplier;";

            await using (var insertOrUpdateCommand = new MySqlCommand(checkSQL, connection))
            {
                insertOrUpdateCommand.Parameters.AddWithValue("@user_id", user_id);
                insertOrUpdateCommand.Parameters.AddWithValue("@Module_id", id);

                await insertOrUpdateCommand.ExecuteNonQueryAsync();
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    public async Task<UserModules> GetSumUserModulesAsync(string user_id)
    {
        UserModules modules = new UserModules();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT 
                    SUM(current_multiplier) AS total_multiplier,
                WHERE user_id = @user_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            modules.Power = reader.IsDBNull(reader.GetOrdinal("total_multiplier")) ? 0 : reader.GetDoubleSafe("total_multiplier");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return modules;
    }
}