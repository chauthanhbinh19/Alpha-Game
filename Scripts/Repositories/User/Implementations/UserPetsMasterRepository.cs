using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserPetsMasterRepository : IUserPetsMasterRepository
{ 
    public async Task<Master> GetPetMasterAsync(string id, string card_id)
    {
        Master master = new Master();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT *
                FROM user_pets_master
                WHERE user_id = @user_id AND master_id = @id AND user_pet_id = @card_id;
            ";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@id", id);
                    selectCommand.Parameters.AddWithValue("@card_id", card_id);

                    await using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            master.Id = reader.GetStringSafe("master_id");
                            master.Level = reader.GetIntSafe("master_level");
                            master.Power = reader.GetDoubleSafe("power");
                            master.Health = reader.GetDoubleSafe("health");
                            master.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            master.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            master.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            master.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            master.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            master.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            master.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            master.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            master.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            master.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            master.Speed = reader.GetDoubleSafe("speed");
                            master.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            master.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            master.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            master.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            master.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            master.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            master.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            master.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            master.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            master.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            master.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            master.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            master.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            master.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            master.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            master.Tenacity = reader.GetDoubleSafe("tenacity");
                            master.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            master.ComboRate = reader.GetDoubleSafe("combo_rate");
                            master.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            master.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            master.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            master.StunRate = reader.GetDoubleSafe("stun_rate");
                            master.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            master.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            master.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            master.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            master.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            master.Mana = reader.GetDoubleSafe("mana");
                            master.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            master.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            master.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            master.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            master.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            master.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            master.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            master.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            master.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            master.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            master.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            master.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            master.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            master.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            master.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            master.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            master.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            master.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            master.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            master.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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

        return master;
    }
    public async Task InsertOrUpdatePetMasterAsync(Master master, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkSQL = @"
                SELECT COUNT(*) 
                FROM user_pets_master 
                WHERE user_id = @user_id AND user_pet_id = @card_id AND master_id = @master_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@card_id", card_id);
                    checkCommand.Parameters.AddWithValue("@master_id", master.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count > 0)
                    {
                        // UPDATE
                        string updateSQL = @"
                        UPDATE user_pets_master
                        SET master_level = @master_level, power = @power, health = @health, 
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
                        WHERE user_id = @user_id AND user_pet_id = @card_id AND master_id = @master_id;
                        ";
                        await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                        {
                            // Thêm tất cả các parameter như cũ
                            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCommand.Parameters.AddWithValue("@card_id", card_id);
                            updateCommand.Parameters.AddWithValue("@master_id", master.Id);
                            updateCommand.Parameters.AddWithValue("@master_level", master.Level);
                            updateCommand.Parameters.AddWithValue("@power", master.Power);
                            updateCommand.Parameters.AddWithValue("@health", master.Health);
                            updateCommand.Parameters.AddWithValue("@physical_attack", master.PhysicalAttack);
                            updateCommand.Parameters.AddWithValue("@physical_defense", master.PhysicalDefense);
                            updateCommand.Parameters.AddWithValue("@magical_attack", master.MagicalAttack);
                            updateCommand.Parameters.AddWithValue("@magical_defense", master.MagicalDefense);
                            updateCommand.Parameters.AddWithValue("@chemical_attack", master.ChemicalAttack);
                            updateCommand.Parameters.AddWithValue("@chemical_defense", master.ChemicalDefense);
                            updateCommand.Parameters.AddWithValue("@atomic_attack", master.AtomicAttack);
                            updateCommand.Parameters.AddWithValue("@atomic_defense", master.AtomicDefense);
                            updateCommand.Parameters.AddWithValue("@mental_attack", master.MentalAttack);
                            updateCommand.Parameters.AddWithValue("@mental_defense", master.MentalDefense);
                            updateCommand.Parameters.AddWithValue("@speed", master.Speed);
                            updateCommand.Parameters.AddWithValue("@critical_damage_rate", master.CriticalDamageRate);
                            updateCommand.Parameters.AddWithValue("@critical_rate", master.CriticalRate);
                            updateCommand.Parameters.AddWithValue("@critical_resistance_rate", master.CriticalResistanceRate);
                            updateCommand.Parameters.AddWithValue("@ignore_critical_rate", master.IgnoreCriticalRate);
                            updateCommand.Parameters.AddWithValue("@penetration_rate", master.PenetrationRate);
                            updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", master.PenetrationResistanceRate);
                            updateCommand.Parameters.AddWithValue("@evasion_rate", master.EvasionRate);
                            updateCommand.Parameters.AddWithValue("@damage_absorption_rate", master.DamageAbsorptionRate);
                            updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", master.IgnoreDamageAbsorptionRate);
                            updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", master.AbsorbedDamageRate);
                            updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", master.VitalityRegenerationRate);
                            updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", master.VitalityRegenerationResistanceRate);
                            updateCommand.Parameters.AddWithValue("@accuracy_rate", master.AccuracyRate);
                            updateCommand.Parameters.AddWithValue("@lifesteal_rate", master.LifestealRate);
                            updateCommand.Parameters.AddWithValue("@shield_strength", master.ShieldStrength);
                            updateCommand.Parameters.AddWithValue("@tenacity", master.Tenacity);
                            updateCommand.Parameters.AddWithValue("@resistance_rate", master.ResistanceRate);
                            updateCommand.Parameters.AddWithValue("@combo_rate", master.ComboRate);
                            updateCommand.Parameters.AddWithValue("@ignore_combo_rate", master.IgnoreComboRate);
                            updateCommand.Parameters.AddWithValue("@combo_damage_rate", master.ComboDamageRate);
                            updateCommand.Parameters.AddWithValue("@combo_resistance_rate", master.ComboResistanceRate);
                            updateCommand.Parameters.AddWithValue("@stun_rate", master.StunRate);
                            updateCommand.Parameters.AddWithValue("@ignore_stun_rate", master.IgnoreStunRate);
                            updateCommand.Parameters.AddWithValue("@reflection_rate", master.ReflectionRate);
                            updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", master.IgnoreReflectionRate);
                            updateCommand.Parameters.AddWithValue("@reflection_damage_rate", master.ReflectionDamageRate);
                            updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", master.ReflectionResistanceRate);
                            updateCommand.Parameters.AddWithValue("@mana", master.Mana);
                            updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", master.ManaRegenerationRate);
                            updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", master.DamageToDifferentFactionRate);
                            updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", master.ResistanceToDifferentFactionRate);
                            updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", master.DamageToSameFactionRate);
                            updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", master.ResistanceToSameFactionRate);
                            updateCommand.Parameters.AddWithValue("@normal_damage_rate", master.NormalDamageRate);
                            updateCommand.Parameters.AddWithValue("@normal_resistance_rate", master.NormalResistanceRate);
                            updateCommand.Parameters.AddWithValue("@skill_damage_rate", master.SkillDamageRate);
                            updateCommand.Parameters.AddWithValue("@skill_resistance_rate", master.SkillResistanceRate);
                            updateCommand.Parameters.AddWithValue("@percent_all_health", master.PercentAllHealth);
                            updateCommand.Parameters.AddWithValue("@percent_all_physical_attack", master.PercentAllPhysicalAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_physical_defense", master.PercentAllPhysicalDefense);
                            updateCommand.Parameters.AddWithValue("@percent_all_magical_attack", master.PercentAllMagicalAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_magical_defense", master.PercentAllMagicalDefense);
                            updateCommand.Parameters.AddWithValue("@percent_all_chemical_attack", master.PercentAllChemicalAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_chemical_defense", master.PercentAllChemicalDefense);
                            updateCommand.Parameters.AddWithValue("@percent_all_atomic_attack", master.PercentAllAtomicAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_atomic_defense", master.PercentAllAtomicDefense);
                            updateCommand.Parameters.AddWithValue("@percent_all_mental_attack", master.PercentAllMentalAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_mental_defense", master.PercentAllMentalDefense);

                            await updateCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // INSERT
                        string insertSQL = @"
                        INSERT INTO user_pets_master 
                        (
                            user_id, user_pet_id, master_id, master_level, 
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
                            @user_id, @card_id, @master_id, @master_level, 
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
                        await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
                        {
                            // Thêm các parameter giống như trên (giữ nguyên)
                            insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCommand.Parameters.AddWithValue("@card_id", card_id);
                            insertCommand.Parameters.AddWithValue("@master_id", master.Id);
                            insertCommand.Parameters.AddWithValue("@master_level", master.Level == 0 ? 1 : master.Level);
                            insertCommand.Parameters.AddWithValue("@power", master.Power);
                            insertCommand.Parameters.AddWithValue("@health", master.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", master.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", master.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", master.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", master.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", master.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", master.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", master.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", master.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", master.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", master.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", master.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", master.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", master.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", master.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", master.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", master.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", master.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", master.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", master.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", master.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", master.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", master.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", master.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", master.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", master.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", master.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", master.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", master.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", master.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", master.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", master.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", master.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", master.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", master.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", master.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", master.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", master.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", master.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", master.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", master.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", master.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", master.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", master.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", master.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", master.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", master.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", master.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", master.SkillResistanceRate);
                            insertCommand.Parameters.AddWithValue("@percent_all_health", master.PercentAllHealth);
                            insertCommand.Parameters.AddWithValue("@percent_all_physical_attack", master.PercentAllPhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_physical_defense", master.PercentAllPhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@percent_all_magical_attack", master.PercentAllMagicalAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_magical_defense", master.PercentAllMagicalDefense);
                            insertCommand.Parameters.AddWithValue("@percent_all_chemical_attack", master.PercentAllChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_chemical_defense", master.PercentAllChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@percent_all_atomic_attack", master.PercentAllAtomicAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_atomic_defense", master.PercentAllAtomicDefense);
                            insertCommand.Parameters.AddWithValue("@percent_all_mental_attack", master.PercentAllMentalAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_mental_defense", master.PercentAllMentalDefense);
                            // ... các tham số khác giống như update
                            await insertCommand.ExecuteNonQueryAsync();
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
    public async Task<Master> GetSumPetsMasterAsync(string user_id, string card_id)
    {
        Master master = new Master();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
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
                FROM user_pets_master 
                WHERE user_id = @user_id AND user_pet_id = @card_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", user_id);
                    selectCommand.Parameters.AddWithValue("@card_id", card_id);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            master.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            master.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            master.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            master.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            master.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            master.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            master.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            master.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            master.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            master.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            master.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            master.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            master.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            master.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            master.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            master.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            master.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            master.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            master.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            master.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            master.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            master.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            master.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            master.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            master.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            master.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            master.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            master.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            master.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            master.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            master.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            master.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            master.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            master.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            master.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            master.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            master.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            master.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            master.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            master.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            master.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            master.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            master.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            master.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            master.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            master.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            master.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            master.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            master.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            master.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            master.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            master.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            master.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            master.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            master.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            master.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            master.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            master.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            master.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            master.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            master.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
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

        return master;
    }
    
}