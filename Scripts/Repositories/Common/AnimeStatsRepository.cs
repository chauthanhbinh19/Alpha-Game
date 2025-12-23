using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class AnimeStatsRepository : IAnimeStatsRepository
{
    public async Task<AnimeStats> GetAnimeStatsAsync(string type, string user_id)
    {
        AnimeStats animeStats = new AnimeStats();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnector.MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT *
            FROM anime_stats
            WHERE user_id = @user_id AND rank_type = @type";

            await using var command = new MySqlConnector.MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);
            command.Parameters.AddWithValue("@type", type);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                animeStats.Type = reader.GetStringSafe("animeStats_type");
                animeStats.Level = reader.IsDBNull(reader.GetOrdinal("animeStats_level")) ? 0 : reader.GetIntSafe("animeStats_level");
                animeStats.Power = reader.GetDoubleSafe("power");
                animeStats.Health = reader.GetDoubleSafe("health");
                animeStats.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                animeStats.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                animeStats.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                animeStats.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                animeStats.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                animeStats.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                animeStats.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                animeStats.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                animeStats.MentalAttack = reader.GetDoubleSafe("mental_attack");
                animeStats.MentalDefense = reader.GetDoubleSafe("mental_defense");
                animeStats.Speed = reader.GetDoubleSafe("speed");
                animeStats.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                animeStats.CriticalRate = reader.GetDoubleSafe("critical_rate");
                animeStats.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                animeStats.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                animeStats.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                animeStats.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                animeStats.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                animeStats.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                animeStats.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                animeStats.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                animeStats.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                animeStats.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                animeStats.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                animeStats.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                animeStats.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                animeStats.Tenacity = reader.GetDoubleSafe("tenacity");
                animeStats.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                animeStats.ComboRate = reader.GetDoubleSafe("combo_rate");
                animeStats.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                animeStats.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                animeStats.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                animeStats.StunRate = reader.GetDoubleSafe("stun_rate");
                animeStats.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                animeStats.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                animeStats.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                animeStats.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                animeStats.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                animeStats.Mana = reader.GetDoubleSafe("mana");
                animeStats.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                animeStats.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                animeStats.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                animeStats.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                animeStats.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                animeStats.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                animeStats.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                animeStats.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                animeStats.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                animeStats.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                animeStats.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                animeStats.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                animeStats.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                animeStats.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                animeStats.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                animeStats.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                animeStats.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                animeStats.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                animeStats.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                animeStats.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
            }
        }
        catch (MySqlConnector.MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
        finally
        {
            await connection.CloseAsync();
        }

        return animeStats;
    }
    public async Task InsertOrUpdateAnimeStatsAsync(AnimeStats animeStats, string type, string user_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        try
        {
            await connection.OpenAsync();

            string checkQuery = @"
            SELECT COUNT(*) FROM anime_stats 
            WHERE user_id = @user_id AND rank_type = @rank_type";

            await using (var checkCmd = new MySqlCommand(checkQuery, connection))
            {
                checkCmd.Parameters.AddWithValue("@user_id", user_id);
                checkCmd.Parameters.AddWithValue("@rank_type", type);

                int count = Convert.ToInt32(await checkCmd.ExecuteScalarAsync());

                if (count > 0)
                {
                    // -------- UPDATE ----------
                    string updateQuery = @"
                    UPDATE anime_stats
                    SET rank_level = @rank_level, power = @power, health = @health, mana = @mana, speed = @speed,  
                        physical_attack = @physical_attack, physical_defense = @physical_defense,  
                        magical_attack = @magical_attack, magical_defense = @magical_defense,  
                        chemical_attack = @chemical_attack, chemical_defense = @chemical_defense,  
                        atomic_attack = @atomic_attack, atomic_defense = @atomic_defense,  
                        mental_attack = @mental_attack, mental_defense = @mental_defense,  
                        critical_damage_rate = @critical_damage_rate, critical_rate = @critical_rate,  
                        penetration_rate = @penetration_rate, evasion_rate = @evasion_rate,  
                        damage_absorption_rate = @damage_absorption_rate, vitality_regeneration_rate = @vitality_regeneration_rate,  
                        accuracy_rate = @accuracy_rate, lifesteal_rate = @lifesteal_rate,  
                        shield_strength = @shield_strength, tenacity = @tenacity, resistance_rate = @resistance_rate,  
                        combo_rate = @combo_rate, reflection_rate = @reflection_rate,  
                        mana_regeneration_rate = @mana_regeneration_rate,  
                        damage_to_different_faction_rate = @damage_to_different_faction_rate,  
                        resistance_to_different_faction_rate = @resistance_to_different_faction_rate,  
                        damage_to_same_faction_rate = @damage_to_same_faction_rate,  
                        resistance_to_same_faction_rate = @resistance_to_same_faction_rate,  
                        percent_all_health = @percent_all_health, percent_all_physical_attack = @percent_all_physical_attack,  
                        percent_all_physical_defense = @percent_all_physical_defense, percent_all_magical_attack = @percent_all_magical_attack,  
                        percent_all_magical_defense = @percent_all_magical_defense, percent_all_chemical_attack = @percent_all_chemical_attack,  
                        percent_all_chemical_defense = @percent_all_chemical_defense, percent_all_atomic_attack = @percent_all_atomic_attack,  
                        percent_all_atomic_defense = @percent_all_atomic_defense, percent_all_mental_attack = @percent_all_mental_attack,  
                        percent_all_mental_defense = @percent_all_mental_defense
                    WHERE user_id = @user_id AND rank_type = @rank_type;
                ";

                    await using var updateCmd = new MySqlCommand(updateQuery, connection);
                    AddAllParameters(updateCmd, animeStats, user_id, type);

                    await updateCmd.ExecuteNonQueryAsync();
                }
                else
                {
                    // -------- INSERT ----------
                    string insertQuery = @"
                    INSERT INTO anime_stats
                    (user_id, rank_type, rank_level, power, health, mana, speed, 
                     physical_attack, physical_defense, magical_attack, magical_defense, 
                     chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                     mental_attack, mental_defense, 
                     critical_damage_rate, critical_rate, penetration_rate, evasion_rate, 
                     damage_absorption_rate, vitality_regeneration_rate, accuracy_rate, lifesteal_rate, 
                     shield_strength, tenacity, resistance_rate, combo_rate, reflection_rate, 
                     mana_regeneration_rate, damage_to_different_faction_rate, resistance_to_different_faction_rate, 
                     damage_to_same_faction_rate, resistance_to_same_faction_rate, 
                     percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                     percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                     percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                     percent_all_mental_attack, percent_all_mental_defense)
                    VALUES (
                        @user_id, @rank_type, @rank_level, @power, @health, @mana, @speed,
                        @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                        @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense,
                        @mental_attack, @mental_defense,
                        @critical_damage_rate, @critical_rate, @penetration_rate, @evasion_rate,
                        @damage_absorption_rate, @vitality_regeneration_rate, @accuracy_rate, @lifesteal_rate,
                        @shield_strength, @tenacity, @resistance_rate, @combo_rate, @reflection_rate,
                        @mana_regeneration_rate, @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                        @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense,
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack,
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense,
                        @percent_all_mental_attack, @percent_all_mental_defense
                    );
                ";

                    await using var insertCmd = new MySqlCommand(insertQuery, connection);
                    AddAllParameters(insertCmd, animeStats, user_id, type);

                    await insertCmd.ExecuteNonQueryAsync();
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
        finally
        {
            await connection.CloseAsync();
        }
    }
    public async Task<AnimeStats> GetSumAnimeStatsAsync(string user_id)
    {
        AnimeStats animeStats = new AnimeStats();
        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string query = @"
            SELECT 
                SUM(power) AS power,
                SUM(health) AS health,
                SUM(mana) AS mana,
                SUM(physical_attack) AS physical_attack,
                SUM(physical_defense) AS physical_defense,
                SUM(magical_attack) AS magical_attack,
                SUM(magical_defense) AS magical_defense,
                SUM(chemical_attack) AS chemical_attack,
                SUM(chemical_defense) AS chemical_defense,
                SUM(atomic_attack) AS atomic_attack,
                SUM(atomic_defense) AS atomic_defense,
                SUM(mental_attack) AS mental_attack,
                SUM(mental_defense) AS mental_defense,
                SUM(speed) AS speed,
                SUM(critical_damage_rate) AS critical_damage_rate,
                SUM(critical_rate) AS critical_rate,
                SUM(critical_resistance_rate) AS critical_resistance_rate,
                SUM(ignore_critical_rate) AS ignore_critical_rate,
                SUM(penetration_rate) AS penetration_rate,
                SUM(penetration_resistance_rate) AS penetration_resistance_rate,
                SUM(evasion_rate) AS evasion_rate,
                SUM(damage_absorption_rate) AS damage_absorption_rate,
                SUM(ignore_damage_absorption_rate) AS ignore_damage_absorption_rate,
                SUM(absorbed_damage_rate) AS absorbed_damage_rate,
                SUM(vitality_regeneration_rate) AS vitality_regeneration_rate,
                SUM(vitality_regeneration_resistance_rate) AS vitality_regeneration_resistance_rate,
                SUM(accuracy_rate) AS accuracy_rate,
                SUM(lifesteal_rate) AS lifesteal_rate,
                SUM(shield_strength) AS shield_strength,
                SUM(tenacity) AS tenacity,
                SUM(resistance_rate) AS resistance_rate,
                SUM(combo_rate) AS combo_rate,
                SUM(ignore_combo_rate) AS ignore_combo_rate,
                SUM(combo_damage_rate) AS combo_damage_rate,
                SUM(combo_resistance_rate) AS combo_resistance_rate,
                SUM(stun_rate) AS stun_rate,
                SUM(ignore_stun_rate) AS ignore_stun_rate,
                SUM(reflection_rate) AS reflection_rate,
                SUM(ignore_reflection_rate) AS ignore_reflection_rate,
                SUM(reflection_damage_rate) AS reflection_damage_rate,
                SUM(reflection_resistance_rate) AS reflection_resistance_rate,
                SUM(mana_regeneration_rate) AS mana_regeneration_rate,
                SUM(damage_to_different_faction_rate) AS damage_to_different_faction_rate,
                SUM(resistance_to_different_faction_rate) AS resistance_to_different_faction_rate,
                SUM(damage_to_same_faction_rate) AS damage_to_same_faction_rate,
                SUM(resistance_to_same_faction_rate) AS resistance_to_same_faction_rate,
                SUM(normal_damage_rate) AS normal_damage_rate,
                SUM(normal_resistance_rate) AS normal_resistance_rate,
                SUM(skill_damage_rate) AS skill_damage_rate,
                SUM(skill_resistance_rate) AS skill_resistance_rate,
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
            FROM anime_stats 
            WHERE user_id = @user_id";

            await using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@user_id", user_id);

            await using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                animeStats.Power = GetDoubleSafe(reader, "power");
                animeStats.Health = GetDoubleSafe(reader, "health");
                animeStats.Mana = GetDoubleSafe(reader, "mana");

                animeStats.PhysicalAttack = GetDoubleSafe(reader, "physical_attack");
                animeStats.PhysicalDefense = GetDoubleSafe(reader, "physical_defense");
                animeStats.MagicalAttack = GetDoubleSafe(reader, "magical_attack");
                animeStats.MagicalDefense = GetDoubleSafe(reader, "magical_defense");

                animeStats.ChemicalAttack = GetDoubleSafe(reader, "chemical_attack");
                animeStats.ChemicalDefense = GetDoubleSafe(reader, "chemical_defense");

                animeStats.AtomicAttack = GetDoubleSafe(reader, "atomic_attack");
                animeStats.AtomicDefense = GetDoubleSafe(reader, "atomic_defense");

                animeStats.MentalAttack = GetDoubleSafe(reader, "mental_attack");
                animeStats.MentalDefense = GetDoubleSafe(reader, "mental_defense");

                animeStats.Speed = GetDoubleSafe(reader, "speed");

                animeStats.CriticalDamageRate = GetDoubleSafe(reader, "critical_damage_rate");
                animeStats.CriticalRate = GetDoubleSafe(reader, "critical_rate");
                animeStats.CriticalResistanceRate = GetDoubleSafe(reader, "critical_resistance_rate");
                animeStats.IgnoreCriticalRate = GetDoubleSafe(reader, "ignore_critical_rate");

                animeStats.PenetrationRate = GetDoubleSafe(reader, "penetration_rate");
                animeStats.PenetrationResistanceRate = GetDoubleSafe(reader, "penetration_resistance_rate");

                animeStats.EvasionRate = GetDoubleSafe(reader, "evasion_rate");
                animeStats.DamageAbsorptionRate = GetDoubleSafe(reader, "damage_absorption_rate");
                animeStats.IgnoreDamageAbsorptionRate = GetDoubleSafe(reader, "ignore_damage_absorption_rate");
                animeStats.AbsorbedDamageRate = GetDoubleSafe(reader, "absorbed_damage_rate");

                animeStats.VitalityRegenerationRate = GetDoubleSafe(reader, "vitality_regeneration_rate");
                animeStats.VitalityRegenerationResistanceRate = GetDoubleSafe(reader, "vitality_regeneration_resistance_rate");

                animeStats.AccuracyRate = GetDoubleSafe(reader, "accuracy_rate");
                animeStats.LifestealRate = GetDoubleSafe(reader, "lifesteal_rate");
                animeStats.ShieldStrength = GetDoubleSafe(reader, "shield_strength");
                animeStats.Tenacity = GetDoubleSafe(reader, "tenacity");
                animeStats.ResistanceRate = GetDoubleSafe(reader, "resistance_rate");

                animeStats.ComboRate = GetDoubleSafe(reader, "combo_rate");
                animeStats.IgnoreComboRate = GetDoubleSafe(reader, "ignore_combo_rate");
                animeStats.ComboDamageRate = GetDoubleSafe(reader, "combo_damage_rate");
                animeStats.ComboResistanceRate = GetDoubleSafe(reader, "combo_resistance_rate");

                animeStats.StunRate = GetDoubleSafe(reader, "stun_rate");
                animeStats.IgnoreStunRate = GetDoubleSafe(reader, "ignore_stun_rate");

                animeStats.ReflectionRate = GetDoubleSafe(reader, "reflection_rate");
                animeStats.IgnoreReflectionRate = GetDoubleSafe(reader, "ignore_reflection_rate");
                animeStats.ReflectionDamageRate = GetDoubleSafe(reader, "reflection_damage_rate");
                animeStats.ReflectionResistanceRate = GetDoubleSafe(reader, "reflection_resistance_rate");

                animeStats.ManaRegenerationRate = GetDoubleSafe(reader, "mana_regeneration_rate");

                animeStats.DamageToDifferentFactionRate = GetDoubleSafe(reader, "damage_to_different_faction_rate");
                animeStats.ResistanceToDifferentFactionRate = GetDoubleSafe(reader, "resistance_to_different_faction_rate");

                animeStats.DamageToSameFactionRate = GetDoubleSafe(reader, "damage_to_same_faction_rate");
                animeStats.ResistanceToSameFactionRate = GetDoubleSafe(reader, "resistance_to_same_faction_rate");

                animeStats.NormalDamageRate = GetDoubleSafe(reader, "normal_damage_rate");
                animeStats.NormalResistanceRate = GetDoubleSafe(reader, "normal_resistance_rate");

                animeStats.SkillDamageRate = GetDoubleSafe(reader, "skill_damage_rate");
                animeStats.SkillResistanceRate = GetDoubleSafe(reader, "skill_resistance_rate");

                animeStats.PercentAllHealth = GetDoubleSafe(reader, "percent_all_health");
                animeStats.PercentAllPhysicalAttack = GetDoubleSafe(reader, "percent_all_physical_attack");
                animeStats.PercentAllPhysicalDefense = GetDoubleSafe(reader, "percent_all_physical_defense");
                animeStats.PercentAllMagicalAttack = GetDoubleSafe(reader, "percent_all_magical_attack");
                animeStats.PercentAllMagicalDefense = GetDoubleSafe(reader, "percent_all_magical_defense");
                animeStats.PercentAllChemicalAttack = GetDoubleSafe(reader, "percent_all_chemical_attack");
                animeStats.PercentAllChemicalDefense = GetDoubleSafe(reader, "percent_all_chemical_defense");
                animeStats.PercentAllAtomicAttack = GetDoubleSafe(reader, "percent_all_atomic_attack");
                animeStats.PercentAllAtomicDefense = GetDoubleSafe(reader, "percent_all_atomic_defense");
                animeStats.PercentAllMentalAttack = GetDoubleSafe(reader, "percent_all_mental_attack");
                animeStats.PercentAllMentalDefense = GetDoubleSafe(reader, "percent_all_mental_defense");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
        finally
        {
            await connection.CloseAsync();
        }

        return animeStats;
    }
    private double GetDoubleSafe(MySqlDataReader reader, string column)
    {
        return reader.IsDBNull(reader.GetOrdinal(column))
            ? 0
            : reader.GetDoubleSafe(column);
    }
    private void AddAllParameters(MySqlCommand cmd, AnimeStats a, string user_id, string type)
    {
        cmd.Parameters.AddWithValue("@user_id", user_id);
        cmd.Parameters.AddWithValue("@rank_type", type);

        cmd.Parameters.AddWithValue("@rank_level", a.Level == 0 ? 1 : a.Level);
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
        cmd.Parameters.AddWithValue("@penetration_rate", a.PenetrationRate);
        cmd.Parameters.AddWithValue("@evasion_rate", a.EvasionRate);
        cmd.Parameters.AddWithValue("@damage_absorption_rate", a.DamageAbsorptionRate);
        cmd.Parameters.AddWithValue("@vitality_regeneration_rate", a.VitalityRegenerationRate);

        cmd.Parameters.AddWithValue("@accuracy_rate", a.AccuracyRate);
        cmd.Parameters.AddWithValue("@lifesteal_rate", a.LifestealRate);
        cmd.Parameters.AddWithValue("@shield_strength", a.ShieldStrength);
        cmd.Parameters.AddWithValue("@tenacity", a.Tenacity);
        cmd.Parameters.AddWithValue("@resistance_rate", a.ResistanceRate);
        cmd.Parameters.AddWithValue("@combo_rate", a.ComboRate);
        cmd.Parameters.AddWithValue("@reflection_rate", a.ReflectionRate);

        cmd.Parameters.AddWithValue("@mana_regeneration_rate", a.ManaRegenerationRate);
        cmd.Parameters.AddWithValue("@damage_to_different_faction_rate", a.DamageToDifferentFactionRate);
        cmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", a.ResistanceToDifferentFactionRate);
        cmd.Parameters.AddWithValue("@damage_to_same_faction_rate", a.DamageToSameFactionRate);
        cmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", a.ResistanceToSameFactionRate);

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