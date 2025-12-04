using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserCardGeneralsMasterRepository : IUserCardGeneralsMasterRepository
{
    public async Task<Master> GetCardGeneralMasterAsync(string type, string card_id)
    {
        Master Master = new Master();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT *
                FROM user_card_generals_Master
                WHERE user_id = @user_id AND Master_type = @type AND user_card_general_id = @card_id;
            ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@card_id", card_id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Master.Type = reader.GetString("Master_type");
                            Master.Level = reader.GetInt32("Master_level");
                            Master.Power = reader.GetDouble("power");
                            Master.Health = reader.GetDouble("health");
                            Master.PhysicalAttack = reader.GetDouble("physical_attack");
                            Master.PhysicalDefense = reader.GetDouble("physical_defense");
                            Master.MagicalAttack = reader.GetDouble("magical_attack");
                            Master.MagicalDefense = reader.GetDouble("magical_defense");
                            Master.ChemicalAttack = reader.GetDouble("chemical_attack");
                            Master.ChemicalDefense = reader.GetDouble("chemical_defense");
                            Master.AtomicAttack = reader.GetDouble("atomic_attack");
                            Master.AtomicDefense = reader.GetDouble("atomic_defense");
                            Master.MentalAttack = reader.GetDouble("mental_attack");
                            Master.MentalDefense = reader.GetDouble("mental_defense");
                            Master.Speed = reader.GetDouble("speed");
                            Master.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                            Master.CriticalRate = reader.GetDouble("critical_rate");
                            Master.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                            Master.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                            Master.PenetrationRate = reader.GetDouble("penetration_rate");
                            Master.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                            Master.EvasionRate = reader.GetDouble("evasion_rate");
                            Master.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                            Master.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                            Master.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                            Master.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                            Master.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                            Master.AccuracyRate = reader.GetDouble("accuracy_rate");
                            Master.LifestealRate = reader.GetDouble("lifesteal_rate");
                            Master.ShieldStrength = reader.GetDouble("shield_strength");
                            Master.Tenacity = reader.GetDouble("tenacity");
                            Master.ResistanceRate = reader.GetDouble("resistance_rate");
                            Master.ComboRate = reader.GetDouble("combo_rate");
                            Master.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                            Master.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                            Master.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                            Master.StunRate = reader.GetDouble("stun_rate");
                            Master.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                            Master.ReflectionRate = reader.GetDouble("reflection_rate");
                            Master.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                            Master.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                            Master.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                            Master.Mana = reader.GetDouble("mana");
                            Master.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                            Master.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                            Master.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                            Master.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                            Master.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                            Master.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                            Master.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                            Master.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                            Master.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                            Master.PercentAllHealth = reader.GetDouble("percent_all_health");
                            Master.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                            Master.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                            Master.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                            Master.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                            Master.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                            Master.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                            Master.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                            Master.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                            Master.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                            Master.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
                        }
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

        return Master;
    }
    public async Task InsertOrUpdateCardGeneralMasterAsync(Master Master, string type, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkQuery = @"
                SELECT COUNT(*) 
                FROM user_card_generals_Master 
                WHERE user_id = @user_id AND user_card_general_id = @card_id AND Master_type = @Master_type;
            ";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@card_id", card_id);
                    checkCmd.Parameters.AddWithValue("@Master_type", type);

                    int count = Convert.ToInt32(await checkCmd.ExecuteScalarAsync());

                    if (count > 0)
                    {
                        // UPDATE
                        string updateQuery = @"
                        UPDATE user_card_generals_master
                        SET Master_level = @Master_level, power = @power, health = @health, 
                            physical_attack = @physical_attack, physical_defense = @physical_defense, 
                            magical_attack = @magical_attack, magical_defense = @magical_defense, 
                            chemical_attack = @chemical_attack, chemical_defense = @chemical_defense, 
                            atomic_attack = @atomic_attack, atomic_defense = @atomic_defense, 
                            mental_attack = @mental_attack, mental_defense = @mental_defense, 
                            speed = @speed, critical_damage_rate = @critical_damage_rate, 
                            critical_rate = @critical_rate, critical_resistance_rate = @critical_resistance_rate, ignore_critical_rate = @ignore_critical_rate,
                            penetration_rate = @penetration_rate, penetration_resistance_rate = @penetration_resistance_rate,
                            evasion_rate = @evasion_rate, damage_absorption_rate = @damage_absorption_rate, 
                            ignore_damage_absorption_rate = @ignore_damage_absorption_rate, absorbed_damage_rate = @absorbed_damage_rate,
                            vitality_regeneration_rate = @vitality_regeneration_rate, vitality_regeneration_resistance_rate = @vitality_regeneration_resistance_rate, 
                            accuracy_rate = @accuracy_rate, lifesteal_rate = @lifesteal_rate, shield_strength = @shield_strength, 
                            tenacity = @tenacity, resistance_rate = @resistance_rate, 
                            combo_rate = @combo_rate, ignore_combo_rate = @ignore_combo_rate, combo_damage_rate = @combo_damage_rate, combo_resistance_rate = @combo_resistance_rate,
                            stun_rate = @stun_rate, ignore_stun_rate = @ignore_stun_rate,
                            reflection_rate = @reflection_rate, ignore_reflection_rate = @ignore_reflection_rate, 
                            reflection_damage_rate = @reflection_damage_rate, reflection_resistance_rate = @reflection_resistance_rate,
                            mana = @mana, mana_regeneration_rate = @mana_regeneration_rate, 
                            damage_to_different_faction_rate = @damage_to_different_faction_rate, 
                            resistance_to_different_faction_rate = @resistance_to_different_faction_rate, 
                            damage_to_same_faction_rate = @damage_to_same_faction_rate, 
                            resistance_to_same_faction_rate = @resistance_to_same_faction_rate,
                            normal_damage_rate = @normal_damage_rate, normal_resistance_rate = @normal_resistance_rate,
                            skill_damage_rate = @skill_damage_rate, skill_resistance_rate = @skill_resistance_rate,  
                            percent_all_health = @percent_all_health, percent_all_physical_attack = @percent_all_physical_attack,  
                            percent_all_physical_defense = @percent_all_physical_defense, percent_all_magical_attack = @percent_all_magical_attack,  
                            percent_all_magical_defense = @percent_all_magical_defense, percent_all_chemical_attack = @percent_all_chemical_attack,  
                            percent_all_chemical_defense = @percent_all_chemical_defense, percent_all_atomic_attack = @percent_all_atomic_attack,  
                            percent_all_atomic_defense = @percent_all_atomic_defense, percent_all_mental_attack = @percent_all_mental_attack,  
                            percent_all_mental_defense = @percent_all_mental_defense
                        WHERE user_id = @user_id AND user_card_general_id = @card_id AND Master_type = @Master_type;
                        ";
                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            // Thêm tất cả các parameter như cũ
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@Master_type", type);
                            updateCmd.Parameters.AddWithValue("@Master_level", Master.Level);
                            updateCmd.Parameters.AddWithValue("@power", Master.Power);
                            updateCmd.Parameters.AddWithValue("@health", Master.Health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", Master.PhysicalAttack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", Master.PhysicalDefense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", Master.MagicalAttack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", Master.MagicalDefense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", Master.ChemicalAttack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", Master.ChemicalDefense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", Master.AtomicAttack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", Master.AtomicDefense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", Master.MentalAttack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", Master.MentalDefense);
                            updateCmd.Parameters.AddWithValue("@speed", Master.Speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage_rate", Master.CriticalDamageRate);
                            updateCmd.Parameters.AddWithValue("@critical_rate", Master.CriticalRate);
                            updateCmd.Parameters.AddWithValue("@critical_resistance_rate", Master.CriticalResistanceRate);
                            updateCmd.Parameters.AddWithValue("@ignore_critical_rate", Master.IgnoreCriticalRate);
                            updateCmd.Parameters.AddWithValue("@penetration_rate", Master.PenetrationRate);
                            updateCmd.Parameters.AddWithValue("@penetration_resistance_rate", Master.PenetrationResistanceRate);
                            updateCmd.Parameters.AddWithValue("@evasion_rate", Master.EvasionRate);
                            updateCmd.Parameters.AddWithValue("@damage_absorption_rate", Master.DamageAbsorptionRate);
                            updateCmd.Parameters.AddWithValue("@ignore_damage_absorption_rate", Master.IgnoreDamageAbsorptionRate);
                            updateCmd.Parameters.AddWithValue("@absorbed_damage_rate", Master.AbsorbedDamageRate);
                            updateCmd.Parameters.AddWithValue("@vitality_regeneration_rate", Master.VitalityRegenerationRate);
                            updateCmd.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", Master.VitalityRegenerationResistanceRate);
                            updateCmd.Parameters.AddWithValue("@accuracy_rate", Master.AccuracyRate);
                            updateCmd.Parameters.AddWithValue("@lifesteal_rate", Master.LifestealRate);
                            updateCmd.Parameters.AddWithValue("@shield_strength", Master.ShieldStrength);
                            updateCmd.Parameters.AddWithValue("@tenacity", Master.Tenacity);
                            updateCmd.Parameters.AddWithValue("@resistance_rate", Master.ResistanceRate);
                            updateCmd.Parameters.AddWithValue("@combo_rate", Master.ComboRate);
                            updateCmd.Parameters.AddWithValue("@ignore_combo_rate", Master.IgnoreComboRate);
                            updateCmd.Parameters.AddWithValue("@combo_damage_rate", Master.ComboDamageRate);
                            updateCmd.Parameters.AddWithValue("@combo_resistance_rate", Master.ComboResistanceRate);
                            updateCmd.Parameters.AddWithValue("@stun_rate", Master.StunRate);
                            updateCmd.Parameters.AddWithValue("@ignore_stun_rate", Master.IgnoreStunRate);
                            updateCmd.Parameters.AddWithValue("@reflection_rate", Master.ReflectionRate);
                            updateCmd.Parameters.AddWithValue("@ignore_reflection_rate", Master.IgnoreReflectionRate);
                            updateCmd.Parameters.AddWithValue("@reflection_damage_rate", Master.ReflectionDamageRate);
                            updateCmd.Parameters.AddWithValue("@reflection_resistance_rate", Master.ReflectionResistanceRate);
                            updateCmd.Parameters.AddWithValue("@mana", Master.Mana);
                            updateCmd.Parameters.AddWithValue("@mana_regeneration_rate", Master.ManaRegenerationRate);
                            updateCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", Master.DamageToDifferentFactionRate);
                            updateCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", Master.ResistanceToDifferentFactionRate);
                            updateCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", Master.DamageToSameFactionRate);
                            updateCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", Master.ResistanceToSameFactionRate);
                            updateCmd.Parameters.AddWithValue("@normal_damage_rate", Master.NormalDamageRate);
                            updateCmd.Parameters.AddWithValue("@normal_resistance_rate", Master.NormalResistanceRate);
                            updateCmd.Parameters.AddWithValue("@skill_damage_rate", Master.SkillDamageRate);
                            updateCmd.Parameters.AddWithValue("@skill_resistance_rate", Master.SkillResistanceRate);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", Master.PercentAllHealth);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", Master.PercentAllPhysicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", Master.PercentAllPhysicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", Master.PercentAllMagicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", Master.PercentAllMagicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", Master.PercentAllChemicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", Master.PercentAllChemicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", Master.PercentAllAtomicAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", Master.PercentAllAtomicDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", Master.PercentAllMentalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", Master.PercentAllMentalDefense);

                            await updateCmd.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // INSERT
                        string insertQuery = @"
                        INSERT INTO user_card_generals_master 
                        (
                            user_id, user_card_general_id, Master_type, Master_level, 
                            power, health, mana, speed, 
                            physical_attack, physical_defense, magical_attack, magical_defense, 
                            chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                            mental_attack, mental_defense, 
                            critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                            penetration_rate, penetration_resistance_rate,
                            evasion_rate, 
                            damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                            vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                            accuracy_rate, lifesteal_rate, 
                            shield_strength, tenacity, resistance_rate, 
                            combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                            stun_rate, ignore_stun_rate,
                            reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                            mana_regeneration_rate, 
                            damage_to_different_faction_rate, resistance_to_different_faction_rate, 
                            damage_to_same_faction_rate, resistance_to_same_faction_rate, 
                            normal_damage_rate, normal_resistance_rate, 
                            skill_damage_rate, skill_resistance_rate,
                            percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                            percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                            percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                            percent_all_mental_attack, percent_all_mental_defense
                        )
                        VALUES 
                        (
                            @user_id, @card_id, @Master_type, @Master_level, 
                            @power, @health, @mana, @speed, 
                            @physical_attack, @physical_defense, @magical_attack, @magical_defense, 
                            @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, 
                            @mental_attack, @mental_defense, 
                            @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                            @penetration_rate, @penetration_resistance_rate,
                            @evasion_rate, 
                            @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                            @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                            @accuracy_rate, @lifesteal_rate, 
                            @shield_strength, @tenacity, @resistance_rate, 
                            @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                            @stun_rate, @ignore_stun_rate,
                            @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                            @mana_regeneration_rate, 
                            @damage_to_different_faction_rate, @resistance_to_different_faction_rate, 
                            @damage_to_same_faction_rate, @resistance_to_same_faction_rate, 
                            @normal_damage_rate, @normal_resistance_rate, 
                            @skill_damage_rate, @skill_resistance_rate,
                            @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                            @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                            @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                            @percent_all_mental_attack, @percent_all_mental_defense
                        );
                        ";
                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các parameter giống như trên (giữ nguyên)
                            insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCmd.Parameters.AddWithValue("@card_id", card_id);
                            insertCmd.Parameters.AddWithValue("@Master_type", type);
                            insertCmd.Parameters.AddWithValue("@Master_level", Master.Level == 0 ? 1 : Master.Level);
                            insertCmd.Parameters.AddWithValue("@power", Master.Power);
                            insertCmd.Parameters.AddWithValue("@health", Master.Health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", Master.PhysicalAttack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", Master.PhysicalDefense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", Master.MagicalAttack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", Master.MagicalDefense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", Master.ChemicalAttack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", Master.ChemicalDefense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", Master.AtomicAttack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", Master.AtomicDefense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", Master.MentalAttack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", Master.MentalDefense);
                            insertCmd.Parameters.AddWithValue("@speed", Master.Speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage_rate", Master.CriticalDamageRate);
                            insertCmd.Parameters.AddWithValue("@critical_rate", Master.CriticalRate);
                            insertCmd.Parameters.AddWithValue("@critical_resistance_rate", Master.CriticalResistanceRate);
                            insertCmd.Parameters.AddWithValue("@ignore_critical_rate", Master.IgnoreCriticalRate);
                            insertCmd.Parameters.AddWithValue("@penetration_rate", Master.PenetrationRate);
                            insertCmd.Parameters.AddWithValue("@penetration_resistance_rate", Master.PenetrationResistanceRate);
                            insertCmd.Parameters.AddWithValue("@evasion_rate", Master.EvasionRate);
                            insertCmd.Parameters.AddWithValue("@damage_absorption_rate", Master.DamageAbsorptionRate);
                            insertCmd.Parameters.AddWithValue("@ignore_damage_absorption_rate", Master.IgnoreDamageAbsorptionRate);
                            insertCmd.Parameters.AddWithValue("@absorbed_damage_rate", Master.AbsorbedDamageRate);
                            insertCmd.Parameters.AddWithValue("@vitality_regeneration_rate", Master.VitalityRegenerationRate);
                            insertCmd.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", Master.VitalityRegenerationResistanceRate);
                            insertCmd.Parameters.AddWithValue("@accuracy_rate", Master.AccuracyRate);
                            insertCmd.Parameters.AddWithValue("@lifesteal_rate", Master.LifestealRate);
                            insertCmd.Parameters.AddWithValue("@shield_strength", Master.ShieldStrength);
                            insertCmd.Parameters.AddWithValue("@tenacity", Master.Tenacity);
                            insertCmd.Parameters.AddWithValue("@resistance_rate", Master.ResistanceRate);
                            insertCmd.Parameters.AddWithValue("@combo_rate", Master.ComboRate);
                            insertCmd.Parameters.AddWithValue("@ignore_combo_rate", Master.IgnoreComboRate);
                            insertCmd.Parameters.AddWithValue("@combo_damage_rate", Master.ComboDamageRate);
                            insertCmd.Parameters.AddWithValue("@combo_resistance_rate", Master.ComboResistanceRate);
                            insertCmd.Parameters.AddWithValue("@stun_rate", Master.StunRate);
                            insertCmd.Parameters.AddWithValue("@ignore_stun_rate", Master.IgnoreStunRate);
                            insertCmd.Parameters.AddWithValue("@reflection_rate", Master.ReflectionRate);
                            insertCmd.Parameters.AddWithValue("@ignore_reflection_rate", Master.IgnoreReflectionRate);
                            insertCmd.Parameters.AddWithValue("@reflection_damage_rate", Master.ReflectionDamageRate);
                            insertCmd.Parameters.AddWithValue("@reflection_resistance_rate", Master.ReflectionResistanceRate);
                            insertCmd.Parameters.AddWithValue("@mana", Master.Mana);
                            insertCmd.Parameters.AddWithValue("@mana_regeneration_rate", Master.ManaRegenerationRate);
                            insertCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", Master.DamageToDifferentFactionRate);
                            insertCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", Master.ResistanceToDifferentFactionRate);
                            insertCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", Master.DamageToSameFactionRate);
                            insertCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", Master.ResistanceToSameFactionRate);
                            insertCmd.Parameters.AddWithValue("@normal_damage_rate", Master.NormalDamageRate);
                            insertCmd.Parameters.AddWithValue("@normal_resistance_rate", Master.NormalResistanceRate);
                            insertCmd.Parameters.AddWithValue("@skill_damage_rate", Master.SkillDamageRate);
                            insertCmd.Parameters.AddWithValue("@skill_resistance_rate", Master.SkillResistanceRate);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", Master.PercentAllHealth);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", Master.PercentAllPhysicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", Master.PercentAllPhysicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", Master.PercentAllMagicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", Master.PercentAllMagicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", Master.PercentAllChemicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", Master.PercentAllChemicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", Master.PercentAllAtomicAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", Master.PercentAllAtomicDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", Master.PercentAllMentalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", Master.PercentAllMentalDefense);
                            // ... các tham số khác giống như update
                            await insertCmd.ExecuteNonQueryAsync();
                        }
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
    }
    public async Task<Master> GetSumCardGeneralsMasterAsync(string user_id, string card_id)
    {
        Master Master = new Master();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
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
                FROM user_card_generals_master 
                WHERE user_id = @user_id AND user_card_general_id = @card_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_id", card_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Master.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                            Master.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                            Master.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDouble("total_mana");
                            Master.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                            Master.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                            Master.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                            Master.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                            Master.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                            Master.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                            Master.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                            Master.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                            Master.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                            Master.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                            Master.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                            Master.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                            Master.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                            Master.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                            Master.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                            Master.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                            Master.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                            Master.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                            Master.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                            Master.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                            Master.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                            Master.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                            Master.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                            Master.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                            Master.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                            Master.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                            Master.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                            Master.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                            Master.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                            Master.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                            Master.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                            Master.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                            Master.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                            Master.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                            Master.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                            Master.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                            Master.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                            Master.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                            Master.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                            Master.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                            Master.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                            Master.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                            Master.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                            Master.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                            Master.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                            Master.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                            Master.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
                            Master.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDouble("percent_all_health");
                            Master.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDouble("percent_all_physical_attack");
                            Master.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDouble("percent_all_physical_defense");
                            Master.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDouble("percent_all_magical_attack");
                            Master.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDouble("percent_all_magical_defense");
                            Master.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDouble("percent_all_chemical_attack");
                            Master.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDouble("percent_all_chemical_defense");
                            Master.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDouble("percent_all_atomic_attack");
                            Master.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDouble("percent_all_atomic_defense");
                            Master.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDouble("percent_all_mental_attack");
                            Master.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDouble("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return Master;
    }
}