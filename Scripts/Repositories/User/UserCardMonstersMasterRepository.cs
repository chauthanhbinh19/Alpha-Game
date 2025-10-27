using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySql.Data.MySqlClient;
using System.Xml.Linq;
public class UserCardMonstersMasterRepository : IUserCardMonstersMasterRepository
{
    public Master GetCardMonstersMaster(string type, string card_id)
    {
        Master master = new Master();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"select *
                from user_card_monsters_master
                where user_id = @user_id AND master_type = @type AND user_card_monster_id = @card_id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                command.Parameters.AddWithValue("@type", type);
                command.Parameters.AddWithValue("@card_id", card_id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // master.id = reader.GetString("id");
                    master.Type = reader.GetString("master_type");
                    master.Level = reader.GetInt32("master_level");
                    master.Power = reader.GetDouble("power");
                    master.Power = reader.GetDouble("power");
                    master.Health = reader.GetDouble("health");
                    master.PhysicalAttack = reader.GetDouble("physical_attack");
                    master.PhysicalDefense = reader.GetDouble("physical_defense");
                    master.MagicalAttack = reader.GetDouble("magical_attack");
                    master.MagicalDefense = reader.GetDouble("magical_defense");
                    master.ChemicalAttack = reader.GetDouble("chemical_attack");
                    master.ChemicalDefense = reader.GetDouble("chemical_defense");
                    master.AtomicAttack = reader.GetDouble("atomic_attack");
                    master.AtomicDefense = reader.GetDouble("atomic_defense");
                    master.MentalAttack = reader.GetDouble("mental_attack");
                    master.MentalDefense = reader.GetDouble("mental_defense");
                    master.Speed = reader.GetDouble("speed");
                    master.CriticalDamageRate = reader.GetDouble("critical_damage_rate");
                    master.CriticalRate = reader.GetDouble("critical_rate");
                    master.CriticalResistanceRate = reader.GetDouble("critical_resistance_rate");
                    master.IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate");
                    master.PenetrationRate = reader.GetDouble("penetration_rate");
                    master.PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate");
                    master.EvasionRate = reader.GetDouble("evasion_rate");
                    master.DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate");
                    master.IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate");
                    master.AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate");
                    master.VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate");
                    master.VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate");
                    master.AccuracyRate = reader.GetDouble("accuracy_rate");
                    master.LifestealRate = reader.GetDouble("lifesteal_rate");
                    master.ShieldStrength = reader.GetDouble("shield_strength");
                    master.Tenacity = reader.GetDouble("tenacity");
                    master.ResistanceRate = reader.GetDouble("resistance_rate");
                    master.ComboRate = reader.GetDouble("combo_rate");
                    master.IgnoreComboRate = reader.GetDouble("ignore_combo_rate");
                    master.ComboDamageRate = reader.GetDouble("combo_damage_rate");
                    master.ComboResistanceRate = reader.GetDouble("combo_resistance_rate");
                    master.StunRate = reader.GetDouble("stun_rate");
                    master.IgnoreStunRate = reader.GetDouble("ignore_stun_rate");
                    master.ReflectionRate = reader.GetDouble("reflection_rate");
                    master.IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate");
                    master.ReflectionDamageRate = reader.GetDouble("reflection_damage_rate");
                    master.ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate");
                    master.Mana = reader.GetFloat("mana");
                    master.ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate");
                    master.DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate");
                    master.ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate");
                    master.DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate");
                    master.ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate");
                    master.NormalDamageRate = reader.GetDouble("normal_damage_rate");
                    master.NormalResistanceRate = reader.GetDouble("normal_resistance_rate");
                    master.SkillDamageRate = reader.GetDouble("skill_damage_rate");
                    master.SkillResistanceRate = reader.GetDouble("skill_resistance_rate");
                    master.PercentAllHealth = reader.GetDouble("percent_all_health");
                    master.PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack");
                    master.PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense");
                    master.PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack");
                    master.PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense");
                    master.PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack");
                    master.PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense");
                    master.PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack");
                    master.PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense");
                    master.PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack");
                    master.PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense");
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }
        return master;
    }
    public void InsertOrUpdateCardMonstersMaster(Master master, string type, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string checkQuery = @"
                SELECT COUNT(*) FROM user_card_monsters_master 
                WHERE user_id = @user_id AND user_card_monster_id = @card_id AND master_type = @master_type";

                using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@card_id", card_id);
                    checkCmd.Parameters.AddWithValue("@master_type", type);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Nếu tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_card_monsters_master
                        SET master_level = @master_level, power = @power, health = @health, mana = @mana, speed = @speed,  
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
                            combo_rate = @comboRate, ignore_combo_rate = @ignore_combo_rate, combo_damage_rate = @combo_damage_rate, combo_resistance_rate = @combo_resistance_rate,
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
                        WHERE user_id = @user_id AND user_card_monster_id = @card_id AND master_type = @master_type;
                        ";

                        using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@master_type", type);
                            updateCmd.Parameters.AddWithValue("@master_level", master.Level);
                            updateCmd.Parameters.AddWithValue("@power", master.Power);
                            updateCmd.Parameters.AddWithValue("@health", master.Health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", master.PhysicalAttack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", master.PhysicalDefense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", master.MagicalAttack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", master.MagicalDefense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", master.ChemicalAttack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", master.ChemicalDefense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", master.AtomicAttack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", master.AtomicDefense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", master.MentalAttack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", master.MentalDefense);
                            updateCmd.Parameters.AddWithValue("@speed", master.Speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage_rate", master.CriticalDamageRate);
                            updateCmd.Parameters.AddWithValue("@critical_rate", master.CriticalRate);
                            updateCmd.Parameters.AddWithValue("@critical_resistance_rate", master.CriticalResistanceRate);
                            updateCmd.Parameters.AddWithValue("@ignore_critical_rate", master.IgnoreCriticalRate);
                            updateCmd.Parameters.AddWithValue("@penetration_rate", master.PenetrationRate);
                            updateCmd.Parameters.AddWithValue("@penetration_resistance_rate", master.PenetrationResistanceRate);
                            updateCmd.Parameters.AddWithValue("@evasion_rate", master.EvasionRate);
                            updateCmd.Parameters.AddWithValue("@damage_absorption_rate", master.DamageAbsorptionRate);
                            updateCmd.Parameters.AddWithValue("@ignore_damage_absorption_rate", master.IgnoreDamageAbsorptionRate);
                            updateCmd.Parameters.AddWithValue("@absorbed_damage_rate", master.AbsorbedDamageRate);
                            updateCmd.Parameters.AddWithValue("@vitality_regeneration_rate", master.VitalityRegenerationRate);
                            updateCmd.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", master.VitalityRegenerationResistanceRate);
                            updateCmd.Parameters.AddWithValue("@accuracy_rate", master.AccuracyRate);
                            updateCmd.Parameters.AddWithValue("@lifesteal_rate", master.LifestealRate);
                            updateCmd.Parameters.AddWithValue("@shield_strength", master.ShieldStrength);
                            updateCmd.Parameters.AddWithValue("@tenacity", master.Tenacity);
                            updateCmd.Parameters.AddWithValue("@resistance_rate", master.ResistanceRate);
                            updateCmd.Parameters.AddWithValue("@combo_rate", master.ComboRate);
                            updateCmd.Parameters.AddWithValue("@ignore_combo_rate", master.IgnoreComboRate);
                            updateCmd.Parameters.AddWithValue("@combo_damage_rate", master.ComboDamageRate);
                            updateCmd.Parameters.AddWithValue("@combo_resistance_rate", master.ComboResistanceRate);
                            updateCmd.Parameters.AddWithValue("@stun_rate", master.StunRate);
                            updateCmd.Parameters.AddWithValue("@ignore_stun_rate", master.IgnoreStunRate);
                            updateCmd.Parameters.AddWithValue("@reflection_rate", master.ReflectionRate);
                            updateCmd.Parameters.AddWithValue("@ignore_reflection_rate", master.IgnoreReflectionRate);
                            updateCmd.Parameters.AddWithValue("@reflection_damage_rate", master.ReflectionDamageRate);
                            updateCmd.Parameters.AddWithValue("@reflection_resistance_rate", master.ReflectionResistanceRate);
                            updateCmd.Parameters.AddWithValue("@mana", master.Mana);
                            updateCmd.Parameters.AddWithValue("@mana_regeneration_rate", master.ManaRegenerationRate);
                            updateCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", master.DamageToDifferentFactionRate);
                            updateCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", master.ResistanceToDifferentFactionRate);
                            updateCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", master.DamageToSameFactionRate);
                            updateCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", master.ResistanceToSameFactionRate);
                            updateCmd.Parameters.AddWithValue("@normal_damage_rate", master.NormalDamageRate);
                            updateCmd.Parameters.AddWithValue("@normal_resistance_rate", master.NormalResistanceRate);
                            updateCmd.Parameters.AddWithValue("@skill_damage_rate", master.SkillDamageRate);
                            updateCmd.Parameters.AddWithValue("@skill_resistance_rate", master.SkillResistanceRate);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", master.PercentAllHealth);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", master.PercentAllPhysicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", master.PercentAllPhysicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", master.PercentAllMagicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", master.PercentAllMagicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", master.PercentAllChemicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", master.PercentAllChemicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", master.PercentAllAtomicAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", master.PercentAllAtomicDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", master.PercentAllMentalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", master.PercentAllMentalDefense);

                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                        INSERT INTO user_card_monsters_master 
                        (user_id, user_pet_id, master_type, master_level, power, health, 
                        physical_attack, physical_defense, magical_attack, magical_defense,
                        chemical_attack, chemical_defense, atomic_attack, atomic_defense, mental_attack, mental_defense,
                        speed, critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                        penetration_rate, penetration_resistance_rate,
                        evasion_rate, damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                        vitality_regeneration_rate, vitality_regeneration_resistance_rate,
                        accuracy_rate, lifesteal_rate, shield_strength, tenacity, resistance_rate,
                        combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                        stun_rate, ignore_stun_rate,
                        reflection_rate, ignore_reflection_rate, reflection_damage_rate, reflection_resistance_rate,
                        mana, mana_regeneration_rate,
                        damage_to_different_faction_rate, resistance_to_different_faction_rate,
                        damage_to_same_faction_rate, resistance_to_same_faction_rate,
                        normal_damage_rate, normal_resistance_rate,
                        skill_damage_rate, skill_resistance_rate
                        percent_all_health, percent_all_physical_attack, percent_all_physical_defense, 
                        percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, percent_all_atomic_defense, 
                        percent_all_mental_attack, percent_all_mental_defense) 
                        VALUES 
                        (@user_id, @card_id, @master_type, @master_level, @power, @health, 
                        @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                        @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                        @speed, @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                        @penetration_rate, @penetration_resistance_rate,
                        @evasion_rate, @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                        @vitality_regeneration_rate, @vitality_regeneration_resistance_rate,
                        @accuracy_rate, @lifesteal_rate, @shield_strength, @tenacity, @resistance_rate,
                        @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                        @stun_rate, @ignore_stun_rate,
                        @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate, @reflection_resistance_rate,
                        @mana, @mana_regeneration_rate,
                        @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                        @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                        @normal_damage_rate, @normal_resistance_rate,
                        @skill_damage_rate, @skill_resistance_rate
                        @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense, 
                        @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, 
                        @percent_all_chemical_defense, @percent_all_atomic_attack, @percent_all_atomic_defense, 
                        @percent_all_mental_attack, @percent_all_mental_defense);
                        ";

                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các tham số như trên
                            insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCmd.Parameters.AddWithValue("@card_id", card_id);
                            insertCmd.Parameters.AddWithValue("@master_type", master.Type);
                            insertCmd.Parameters.AddWithValue("@master_level", master.Level == 0 ? 1 : master.Level);
                            insertCmd.Parameters.AddWithValue("@power", master.Power);
                            insertCmd.Parameters.AddWithValue("@health", master.Health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", master.PhysicalAttack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", master.PhysicalDefense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", master.MagicalAttack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", master.MagicalDefense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", master.ChemicalAttack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", master.ChemicalDefense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", master.AtomicAttack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", master.AtomicDefense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", master.MentalAttack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", master.MentalDefense);
                            insertCmd.Parameters.AddWithValue("@speed", master.Speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage_rate", master.CriticalDamageRate);
                            insertCmd.Parameters.AddWithValue("@critical_rate", master.CriticalRate);
                            insertCmd.Parameters.AddWithValue("@critical_resistance_rate", master.CriticalResistanceRate);
                            insertCmd.Parameters.AddWithValue("@ignore_critical_rate", master.IgnoreCriticalRate);
                            insertCmd.Parameters.AddWithValue("@penetration_rate", master.PenetrationRate);
                            insertCmd.Parameters.AddWithValue("@penetration_resistance_rate", master.PenetrationResistanceRate);
                            insertCmd.Parameters.AddWithValue("@evasion_rate", master.EvasionRate);
                            insertCmd.Parameters.AddWithValue("@damage_absorption_rate", master.DamageAbsorptionRate);
                            insertCmd.Parameters.AddWithValue("@ignore_damage_absorption_rate", master.IgnoreDamageAbsorptionRate);
                            insertCmd.Parameters.AddWithValue("@absorbed_damage_rate", master.AbsorbedDamageRate);
                            insertCmd.Parameters.AddWithValue("@vitality_regeneration_rate", master.VitalityRegenerationRate);
                            insertCmd.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", master.VitalityRegenerationResistanceRate);
                            insertCmd.Parameters.AddWithValue("@accuracy_rate", master.AccuracyRate);
                            insertCmd.Parameters.AddWithValue("@lifesteal_rate", master.LifestealRate);
                            insertCmd.Parameters.AddWithValue("@shield_strength", master.ShieldStrength);
                            insertCmd.Parameters.AddWithValue("@tenacity", master.Tenacity);
                            insertCmd.Parameters.AddWithValue("@resistance_rate", master.ResistanceRate);
                            insertCmd.Parameters.AddWithValue("@combo_rate", master.ComboRate);
                            insertCmd.Parameters.AddWithValue("@ignore_combo_rate", master.IgnoreComboRate);
                            insertCmd.Parameters.AddWithValue("@combo_damage_rate", master.ComboDamageRate);
                            insertCmd.Parameters.AddWithValue("@combo_resistance_rate", master.ComboResistanceRate);
                            insertCmd.Parameters.AddWithValue("@stun_rate", master.StunRate);
                            insertCmd.Parameters.AddWithValue("@ignore_stun_rate", master.IgnoreStunRate);
                            insertCmd.Parameters.AddWithValue("@reflection_rate", master.ReflectionRate);
                            insertCmd.Parameters.AddWithValue("@ignore_reflection_rate", master.IgnoreReflectionRate);
                            insertCmd.Parameters.AddWithValue("@reflection_damage_rate", master.ReflectionDamageRate);
                            insertCmd.Parameters.AddWithValue("@reflection_resistance_rate", master.ReflectionResistanceRate);
                            insertCmd.Parameters.AddWithValue("@mana", master.Mana);
                            insertCmd.Parameters.AddWithValue("@mana_regeneration_rate", master.ManaRegenerationRate);
                            insertCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", master.DamageToDifferentFactionRate);
                            insertCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", master.ResistanceToDifferentFactionRate);
                            insertCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", master.DamageToSameFactionRate);
                            insertCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", master.ResistanceToSameFactionRate);
                            insertCmd.Parameters.AddWithValue("@normal_damage_rate", master.NormalDamageRate);
                            insertCmd.Parameters.AddWithValue("@normal_resistance_rate", master.NormalResistanceRate);
                            insertCmd.Parameters.AddWithValue("@skill_damage_rate", master.SkillDamageRate);
                            insertCmd.Parameters.AddWithValue("@skill_resistance_rate", master.SkillResistanceRate);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", master.PercentAllHealth);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", master.PercentAllPhysicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", master.PercentAllPhysicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", master.PercentAllMagicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", master.PercentAllMagicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", master.PercentAllChemicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", master.PercentAllChemicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", master.PercentAllAtomicAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", master.PercentAllAtomicDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", master.PercentAllMentalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", master.PercentAllMentalDefense);
                            insertCmd.ExecuteNonQuery();
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
                connection.Close();
            }
        }
    }
    public Master GetSumCardMonstersMaster(string user_id, string card_id)
    {
        Master master = new Master();
        // string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                string query = @"SELECT 
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
                SUM(percent_all_physical_attack) AS percent_all_physical_attack, SUM(percent_all_physical_defense) AS percent_all_physical_defense,
                SUM(percent_all_magical_attack) AS percent_all_magical_attack, SUM(percent_all_magical_defense) AS percent_all_magical_defense,
                SUM(percent_all_chemical_attack) AS percent_all_chemical_attack, SUM(percent_all_chemical_defense) AS percent_all_chemical_defense,
                SUM(percent_all_atomic_attack) AS percent_all_atomic_attack, SUM(percent_all_atomic_defense) AS percent_all_atomic_defense,
                SUM(percent_all_mental_attack) AS percent_all_mental_attack, SUM(percent_all_mental_defense) AS percent_all_mental_defense
            FROM user_card_monsters_master 
            WHERE user_id = @user_id AND user_card_monster_id = @card_id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_id", card_id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            master.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                            master.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                            master.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                            master.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                            master.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                            master.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                            master.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                            master.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                            master.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                            master.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                            master.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                            master.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                            master.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                            master.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                            master.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                            master.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                            master.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                            master.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                            master.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                            master.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                            master.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                            master.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                            master.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                            master.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                            master.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                            master.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                            master.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                            master.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                            master.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                            master.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                            master.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                            master.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                            master.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                            master.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                            master.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                            master.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                            master.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                            master.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                            master.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                            master.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                            master.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            master.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                            master.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                            master.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                            master.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                            master.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                            master.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                            master.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                            master.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                            master.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
                            master.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDouble("percent_all_health");
                            master.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDouble("percent_all_physical_attack");
                            master.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDouble("percent_all_physical_defense");
                            master.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDouble("percent_all_magical_attack");
                            master.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDouble("percent_all_magical_defense");
                            master.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDouble("percent_all_chemical_attack");
                            master.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDouble("percent_all_chemical_defense");
                            master.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDouble("percent_all_atomic_attack");
                            master.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDouble("percent_all_atomic_defense");
                            master.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDouble("percent_all_mental_attack");
                            master.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDouble("percent_all_mental_defense");
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
                connection.Close();
            }
        }
        return master;
    }
}