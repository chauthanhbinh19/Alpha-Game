using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;

public class AnimeStatsRepository : IAnimeStatsRepository
{
    public AnimeStats GetAnimeStats(string type, string user_id)
    {
        AnimeStats animeStats = new AnimeStats();
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select *
                from anime_stats
                where user_id = @user_id AND rank_type = @type";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // rank.id = reader.GetInt32("id");
                    animeStats.Type = reader.GetString("animeStats_type");
                    animeStats.Level = reader.IsDBNull(reader.GetOrdinal("animeStats_level")) ? 0 : reader.GetInt32("animeStats_level");
                    animeStats.Power = reader.GetDouble("power");
                    animeStats.Health = reader.GetDouble("health");
                    animeStats.PhysicalAttack = reader.GetDouble("physical_attack");
                    animeStats.PhysicalDefense = reader.GetDouble("physical_defense");
                    animeStats.MagicalAttack = reader.GetDouble("magical_attack");
                    animeStats.MagicalDefense = reader.GetDouble("magical_defense");
                    animeStats.ChemicalAttack = reader.GetDouble("chemical_attack");
                    animeStats.ChemicalDefense = reader.GetDouble("chemical_defense");
                    animeStats.AtomicAttack = reader.GetDouble("atomic_attack");
                    animeStats.AtomicDefense = reader.GetDouble("atomic_defense");
                    animeStats.MentalAttack = reader.GetDouble("mental_attack");
                    animeStats.MentalDefense = reader.GetDouble("mental_defense");
                    animeStats.Speed = reader.GetDouble("speed");
                    animeStats.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                    animeStats.CriticalRate = reader.GetDouble("critical_rate");
                    animeStats.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                    animeStats.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                    animeStats.PenetrationRate = reader.GetDouble("penetration_rate");
                    animeStats.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                    animeStats.EvasionRate = reader.GetDouble("evasion_rate");
                    animeStats.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                    animeStats.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                    animeStats.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                    animeStats.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                    animeStats.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                    animeStats.AccuracyRate = reader.GetDouble("accuracy_rate");
                    animeStats.LifestealRate = reader.GetDouble("lifesteal_rate");
                    animeStats.ShieldStrength = reader.GetDouble("shield_strength");
                    animeStats.Tenacity = reader.GetDouble("tenacity");
                    animeStats.ResistanceRate = reader.GetDouble("resistance_rate");
                    animeStats.ComboRate = reader.GetDouble("combo_rate");
                    animeStats.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                    animeStats.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                    animeStats.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                    animeStats.StunRate = reader.GetDouble("stun_rate");
                    animeStats.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                    animeStats.ReflectionRate = reader.GetDouble("reflection_rate");
                    animeStats.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                    animeStats.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                    animeStats.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                    animeStats.Mana = reader.GetFloat("mana");
                    animeStats.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                    animeStats.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                    animeStats.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                    animeStats.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                    animeStats.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                    animeStats.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                    animeStats.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                    animeStats.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                    animeStats.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                    animeStats.PercentAllHealth = reader.GetDouble("percent_all_health");
                    animeStats.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                    animeStats.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                    animeStats.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                    animeStats.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                    animeStats.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                    animeStats.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                    animeStats.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                    animeStats.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                    animeStats.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                    animeStats.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }

        }
        return animeStats;
    }
    public void InsertOrUpdateAnimeStats(AnimeStats animeStats, string type, string user_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM anime_stats 
                WHERE user_id = @user_id AND rank_type = @rank_type";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", user_id);
                    checkCmd.Parameters.AddWithValue("@rank_type", type);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu tồn tại, thực hiện UPDATE
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

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", user_id);
                            updateCmd.Parameters.AddWithValue("@rank_type", type);
                            updateCmd.Parameters.AddWithValue("@rank_level", animeStats.Level);
                            updateCmd.Parameters.AddWithValue("@power", animeStats.Power);
                            updateCmd.Parameters.AddWithValue("@health", animeStats.Health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", animeStats.PhysicalAttack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", animeStats.PhysicalDefense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", animeStats.MagicalAttack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", animeStats.MagicalDefense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", animeStats.ChemicalAttack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", animeStats.ChemicalDefense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", animeStats.AtomicAttack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", animeStats.AtomicDefense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", animeStats.MentalAttack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", animeStats.MentalDefense);
                            updateCmd.Parameters.AddWithValue("@speed", animeStats.Speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage_rate", animeStats.CriticalDamageRate);
                            updateCmd.Parameters.AddWithValue("@critical_rate", animeStats.CriticalRate);
                            updateCmd.Parameters.AddWithValue("@critical_resistance_rate", animeStats.CriticalResistanceRate);
                            updateCmd.Parameters.AddWithValue("@ignore_critical_rate", animeStats.IgnoreCriticalRate);
                            updateCmd.Parameters.AddWithValue("@penetration_rate", animeStats.PenetrationRate);
                            updateCmd.Parameters.AddWithValue("@penetration_resistance_rate", animeStats.PenetrationResistanceRate);
                            updateCmd.Parameters.AddWithValue("@evasion_rate", animeStats.EvasionRate);
                            updateCmd.Parameters.AddWithValue("@damage_absorption_rate", animeStats.DamageAbsorptionRate);
                            updateCmd.Parameters.AddWithValue("@ignore_damage_absorption_rate", animeStats.IgnoreDamageAbsorptionRate);
                            updateCmd.Parameters.AddWithValue("@absorbed_damage_rate", animeStats.AbsorbedDamageRate);
                            updateCmd.Parameters.AddWithValue("@vitality_regeneration_rate", animeStats.VitalityRegenerationRate);
                            updateCmd.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", animeStats.VitalityRegenerationResistanceRate);
                            updateCmd.Parameters.AddWithValue("@accuracy_rate", animeStats.AccuracyRate);
                            updateCmd.Parameters.AddWithValue("@lifesteal_rate", animeStats.LifestealRate);
                            updateCmd.Parameters.AddWithValue("@shield_strength", animeStats.ShieldStrength);
                            updateCmd.Parameters.AddWithValue("@tenacity", animeStats.Tenacity);
                            updateCmd.Parameters.AddWithValue("@resistance_rate", animeStats.ResistanceRate);
                            updateCmd.Parameters.AddWithValue("@combo_rate", animeStats.ComboRate);
                            updateCmd.Parameters.AddWithValue("@ignore_combo_rate", animeStats.IgnoreComboRate);
                            updateCmd.Parameters.AddWithValue("@combo_damage_rate", animeStats.ComboDamageRate);
                            updateCmd.Parameters.AddWithValue("@combo_resistance_rate", animeStats.ComboResistanceRate);
                            updateCmd.Parameters.AddWithValue("@stun_rate", animeStats.StunRate);
                            updateCmd.Parameters.AddWithValue("@ignore_stun_rate", animeStats.IgnoreStunRate);
                            updateCmd.Parameters.AddWithValue("@reflection_rate", animeStats.ReflectionRate);
                            updateCmd.Parameters.AddWithValue("@ignore_reflection_rate", animeStats.IgnoreReflectionRate);
                            updateCmd.Parameters.AddWithValue("@reflection_damage_rate", animeStats.ReflectionDamageRate);
                            updateCmd.Parameters.AddWithValue("@reflection_resistance_rate", animeStats.ReflectionResistanceRate);
                            updateCmd.Parameters.AddWithValue("@mana", animeStats.Mana);
                            updateCmd.Parameters.AddWithValue("@mana_regeneration_rate", animeStats.ManaRegenerationRate);
                            updateCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", animeStats.DamageToDifferentFactionRate);
                            updateCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", animeStats.ResistanceToDifferentFactionRate);
                            updateCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", animeStats.DamageToSameFactionRate);
                            updateCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", animeStats.ResistanceToSameFactionRate);
                            updateCmd.Parameters.AddWithValue("@normal_damage_rate", animeStats.NormalDamageRate);
                            updateCmd.Parameters.AddWithValue("@normal_resistance_rate", animeStats.NormalResistanceRate);
                            updateCmd.Parameters.AddWithValue("@skill_damage_rate", animeStats.SkillDamageRate);
                            updateCmd.Parameters.AddWithValue("@skill_resistance_rate", animeStats.SkillResistanceRate);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", animeStats.PercentAllHealth);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", animeStats.PercentAllPhysicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", animeStats.PercentAllPhysicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", animeStats.PercentAllMagicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", animeStats.PercentAllMagicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", animeStats.PercentAllChemicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", animeStats.PercentAllChemicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", animeStats.PercentAllAtomicAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", animeStats.PercentAllAtomicDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", animeStats.PercentAllMentalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", animeStats.PercentAllMentalDefense);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
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
                        VALUES 
                        (@user_id, @rank_type, @rank_level, @power, @health, @mana, @speed, 
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
                        @percent_all_mental_attack, @percent_all_mental_defense);
                        ";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các tham số như trên
                            insertCmd.Parameters.AddWithValue("@user_id", user_id);
                            insertCmd.Parameters.AddWithValue("@rank_type", type);
                            insertCmd.Parameters.AddWithValue("@rank_level", animeStats.Level == 0 ? 1 : animeStats.Level);
                            insertCmd.Parameters.AddWithValue("@power", animeStats.Power);
                            insertCmd.Parameters.AddWithValue("@health", animeStats.Health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", animeStats.PhysicalAttack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", animeStats.PhysicalDefense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", animeStats.MagicalAttack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", animeStats.MagicalDefense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", animeStats.ChemicalAttack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", animeStats.ChemicalDefense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", animeStats.AtomicAttack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", animeStats.AtomicDefense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", animeStats.MentalAttack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", animeStats.MentalDefense);
                            insertCmd.Parameters.AddWithValue("@speed", animeStats.Speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage_rate", animeStats.CriticalDamageRate);
                            insertCmd.Parameters.AddWithValue("@critical_rate", animeStats.CriticalRate);
                            insertCmd.Parameters.AddWithValue("@critical_resistance_rate", animeStats.CriticalResistanceRate);
                            insertCmd.Parameters.AddWithValue("@ignore_critical_rate", animeStats.IgnoreCriticalRate);
                            insertCmd.Parameters.AddWithValue("@penetration_rate", animeStats.PenetrationRate);
                            insertCmd.Parameters.AddWithValue("@penetration_resistance_rate", animeStats.PenetrationResistanceRate);
                            insertCmd.Parameters.AddWithValue("@evasion_rate", animeStats.EvasionRate);
                            insertCmd.Parameters.AddWithValue("@damage_absorption_rate", animeStats.DamageAbsorptionRate);
                            insertCmd.Parameters.AddWithValue("@ignore_damage_absorption_rate", animeStats.IgnoreDamageAbsorptionRate);
                            insertCmd.Parameters.AddWithValue("@absorbed_damage_rate", animeStats.AbsorbedDamageRate);
                            insertCmd.Parameters.AddWithValue("@vitality_regeneration_rate", animeStats.VitalityRegenerationRate);
                            insertCmd.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", animeStats.VitalityRegenerationResistanceRate);
                            insertCmd.Parameters.AddWithValue("@accuracy_rate", animeStats.AccuracyRate);
                            insertCmd.Parameters.AddWithValue("@lifesteal_rate", animeStats.LifestealRate);
                            insertCmd.Parameters.AddWithValue("@shield_strength", animeStats.ShieldStrength);
                            insertCmd.Parameters.AddWithValue("@tenacity", animeStats.Tenacity);
                            insertCmd.Parameters.AddWithValue("@resistance_rate", animeStats.ResistanceRate);
                            insertCmd.Parameters.AddWithValue("@combo_rate", animeStats.ComboRate);
                            insertCmd.Parameters.AddWithValue("@ignore_combo_rate", animeStats.IgnoreComboRate);
                            insertCmd.Parameters.AddWithValue("@combo_damage_rate", animeStats.ComboDamageRate);
                            insertCmd.Parameters.AddWithValue("@combo_resistance_rate", animeStats.ComboResistanceRate);
                            insertCmd.Parameters.AddWithValue("@stun_rate", animeStats.StunRate);
                            insertCmd.Parameters.AddWithValue("@ignore_stun", animeStats.IgnoreStunRate);
                            insertCmd.Parameters.AddWithValue("@reflection_rate", animeStats.ReflectionRate);
                            insertCmd.Parameters.AddWithValue("@ignore_reflection_rate", animeStats.IgnoreReflectionRate);
                            insertCmd.Parameters.AddWithValue("@reflection_damage_rate", animeStats.ReflectionDamageRate);
                            insertCmd.Parameters.AddWithValue("@reflection_resistance_rate", animeStats.ReflectionResistanceRate);
                            insertCmd.Parameters.AddWithValue("@mana", animeStats.Mana);
                            insertCmd.Parameters.AddWithValue("@mana_regeneration_rate", animeStats.ManaRegenerationRate);
                            insertCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", animeStats.DamageToDifferentFactionRate);
                            insertCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", animeStats.ResistanceToDifferentFactionRate);
                            insertCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", animeStats.DamageToSameFactionRate);
                            insertCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", animeStats.ResistanceToSameFactionRate);
                            insertCmd.Parameters.AddWithValue("@normal_damage_rate", animeStats.NormalDamageRate);
                            insertCmd.Parameters.AddWithValue("@normal_resistance_rate", animeStats.NormalResistanceRate);
                            insertCmd.Parameters.AddWithValue("@skill_damage_rate", animeStats.SkillDamageRate);
                            insertCmd.Parameters.AddWithValue("@skill_resistance_rate", animeStats.SkillResistanceRate);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", animeStats.PercentAllHealth);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", animeStats.PercentAllPhysicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", animeStats.PercentAllPhysicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", animeStats.PercentAllMagicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", animeStats.PercentAllMagicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", animeStats.PercentAllChemicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", animeStats.PercentAllChemicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", animeStats.PercentAllAtomicAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", animeStats.PercentAllAtomicDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", animeStats.PercentAllMentalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", animeStats.PercentAllMentalDefense);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public AnimeStats GetSumAnimeStats(string user_id)
    {
        AnimeStats animeStats = new AnimeStats();
        // int user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
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
                SUM(percent_all_physical_attack) AS percent_all_physical_attack, SUM(percent_all_physical_defense) AS percent_all_physical_defense,
                SUM(percent_all_magical_attack) AS percent_all_magical_attack, SUM(percent_all_magical_defense) AS percent_all_magical_defense,
                SUM(percent_all_chemical_attack) AS percent_all_chemical_attack, SUM(percent_all_chemical_defense) AS percent_all_chemical_defense,
                SUM(percent_all_atomic_attack) AS percent_all_atomic_attack, SUM(percent_all_atomic_defense) AS percent_all_atomic_defense,
                SUM(percent_all_mental_attack) AS percent_all_mental_attack, SUM(percent_all_mental_defense) AS percent_all_mental_defense
            FROM anime_stats 
            WHERE user_id = @user_id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            animeStats.Power = reader.IsDBNull(reader.GetOrdinal("power")) ? 0 : reader.GetDouble("power");
                            animeStats.Health = reader.IsDBNull(reader.GetOrdinal("health")) ? 0 : reader.GetDouble("health");
                            animeStats.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("physical_attack")) ? 0 : reader.GetDouble("physical_attack");
                            animeStats.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("physical_defense")) ? 0 : reader.GetDouble("physical_defense");
                            animeStats.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("magical_attack")) ? 0 : reader.GetDouble("magical_attack");
                            animeStats.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("magical_defense")) ? 0 : reader.GetDouble("magical_defense");
                            animeStats.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("chemical_attack")) ? 0 : reader.GetDouble("chemical_attack");
                            animeStats.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("chemical_defense")) ? 0 : reader.GetDouble("chemical_defense");
                            animeStats.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("atomic_attack")) ? 0 : reader.GetDouble("atomic_attack");
                            animeStats.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("atomic_defense")) ? 0 : reader.GetDouble("atomic_defense");
                            animeStats.MentalAttack = reader.IsDBNull(reader.GetOrdinal("mental_attack")) ? 0 : reader.GetDouble("mental_attack");
                            animeStats.MentalDefense = reader.IsDBNull(reader.GetOrdinal("mental_defense")) ? 0 : reader.GetDouble("mental_defense");
                            animeStats.Speed = reader.IsDBNull(reader.GetOrdinal("speed")) ? 0 : reader.GetDouble("speed");
                            animeStats.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("critical_damage_rate")) ? 0 : reader.GetDouble("critical_damage_rate");
                            animeStats.CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_rate")) ? 0 : reader.GetDouble("critical_rate");
                            animeStats.CriticalRate = reader.IsDBNull(reader.GetOrdinal("critical_resistance_rate")) ? 0 : reader.GetDouble("critical_resistance_rate");
                            animeStats.CriticalRate = reader.IsDBNull(reader.GetOrdinal("ignore_critical_rate")) ? 0 : reader.GetDouble("ignore_critical_rate");
                            animeStats.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_rate")) ? 0 : reader.GetDouble("penetration_rate");
                            animeStats.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("penetration_resistance_rate")) ? 0 : reader.GetDouble("penetration_resistance_rate");
                            animeStats.EvasionRate = reader.IsDBNull(reader.GetOrdinal("evasion_rate")) ? 0 : reader.GetDouble("evasion_rate");
                            animeStats.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("damage_absorption_rate")) ? 0 : reader.GetDouble("damage_absorption_rate");
                            animeStats.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("ignore_damage_absorption_rate");
                            animeStats.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("absorbed_damage_rate")) ? 0 : reader.GetDouble("absorbed_damage_rate");
                            animeStats.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_rate")) ? 0 : reader.GetDouble("vitality_regeneration_rate");
                            animeStats.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("vitality_regeneration_resistance_rate");
                            animeStats.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("accuracy_rate")) ? 0 : reader.GetDouble("accuracy_rate");
                            animeStats.LifestealRate = reader.IsDBNull(reader.GetOrdinal("lifesteal_rate")) ? 0 : reader.GetDouble("lifesteal_rate");
                            animeStats.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("shield_strength")) ? 0 : reader.GetDouble("shield_strength");
                            animeStats.Tenacity = reader.IsDBNull(reader.GetOrdinal("tenacity")) ? 0 : reader.GetDouble("tenacity");
                            animeStats.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("resistance_rate")) ? 0 : reader.GetDouble("resistance_rate");
                            animeStats.ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_rate")) ? 0 : reader.GetDouble("combo_rate");
                            animeStats.ComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_combo_rate")) ? 0 : reader.GetDouble("ignore_combo_rate");
                            animeStats.ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_damage_rate")) ? 0 : reader.GetDouble("combo_damage_rate");
                            animeStats.ComboRate = reader.IsDBNull(reader.GetOrdinal("combo_resistance_rate")) ? 0 : reader.GetDouble("combo_resistance_rate");
                            animeStats.ComboRate = reader.IsDBNull(reader.GetOrdinal("stun_rate")) ? 0 : reader.GetDouble("stun_rate");
                            animeStats.ComboRate = reader.IsDBNull(reader.GetOrdinal("ignore_stun_rate")) ? 0 : reader.GetDouble("ignore_stun_rate");
                            animeStats.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_rate")) ? 0 : reader.GetDouble("reflection_rate");
                            animeStats.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("ignore_reflection_rate")) ? 0 : reader.GetDouble("ignore_reflection_rate");
                            animeStats.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_damage_rate")) ? 0 : reader.GetDouble("reflection_damage_rate");
                            animeStats.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("reflection_resistance_rate")) ? 0 : reader.GetDouble("reflection_resistance_rate");
                            animeStats.Mana = reader.IsDBNull(reader.GetOrdinal("mana")) ? 0 : reader.GetFloat("mana");
                            animeStats.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("mana_regeneration_rate")) ? 0 : reader.GetDouble("mana_regeneration_rate");
                            animeStats.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_different_faction_rate")) ? 0 : reader.GetDouble("damage_to_different_faction_rate");
                            animeStats.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("resistance_to_different_faction_rate");
                            animeStats.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("damage_to_same_faction_rate")) ? 0 : reader.GetDouble("damage_to_same_faction_rate");
                            animeStats.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("resistance_to_same_faction_rate");
                            animeStats.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("normal_damage_rate")) ? 0 : reader.GetDouble("normal_damage_rate");
                            animeStats.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("normal_resistance_rate")) ? 0 : reader.GetDouble("normal_resistance_rate");
                            animeStats.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("skill_damage_rate")) ? 0 : reader.GetDouble("skill_damage_rate");
                            animeStats.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("skill_resistance_rate")) ? 0 : reader.GetDouble("skill_resistance_rate");
                            animeStats.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDouble("percent_all_health");
                            animeStats.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDouble("percent_all_physical_attack");
                            animeStats.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDouble("percent_all_physical_defense");
                            animeStats.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDouble("percent_all_magical_attack");
                            animeStats.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDouble("percent_all_magical_defense");
                            animeStats.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDouble("percent_all_chemical_attack");
                            animeStats.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDouble("percent_all_chemical_defense");
                            animeStats.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDouble("percent_all_atomic_attack");
                            animeStats.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDouble("percent_all_atomic_defense");
                            animeStats.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDouble("percent_all_mental_attack");
                            animeStats.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDouble("percent_all_mental_defense");

                        }
                    }
                }
                connection.Close();
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
        return animeStats;
    }
}