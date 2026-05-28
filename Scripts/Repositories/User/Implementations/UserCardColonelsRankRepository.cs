using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserCardColonelsRankRepository : IUserCardColonelsRankRepository
{
    public async Task<Rank> GetCardColonelRankAsync(string id, string cardId)
    {
        Rank rank = new Rank();
        string userId = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT *
                FROM user_card_colonels_Rank
                WHERE user_id = @user_id AND rank_id = @id AND user_card_colonel_id = @card_id;
            ";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);
                    selectCommand.Parameters.AddWithValue("@id", id);
                    selectCommand.Parameters.AddWithValue("@card_id", cardId);

                    await using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            rank.Id = reader.GetStringSafe("rank_id");
                            rank.Level = reader.GetIntSafe("rank_level");
                            rank.Power = reader.GetDoubleSafe("power");
                            rank.Health = reader.GetDoubleSafe("health");
                            rank.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            rank.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            rank.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            rank.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            rank.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            rank.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            rank.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            rank.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            rank.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            rank.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            rank.Speed = reader.GetDoubleSafe("speed");
                            rank.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            rank.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            rank.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            rank.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            rank.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            rank.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            rank.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            rank.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            rank.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            rank.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            rank.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            rank.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            rank.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            rank.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            rank.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            rank.Tenacity = reader.GetDoubleSafe("tenacity");
                            rank.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            rank.ComboRate = reader.GetDoubleSafe("combo_rate");
                            rank.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            rank.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            rank.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            rank.StunRate = reader.GetDoubleSafe("stun_rate");
                            rank.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            rank.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            rank.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            rank.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            rank.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            rank.Mana = reader.GetDoubleSafe("mana");
                            rank.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            rank.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            rank.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            rank.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            rank.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            rank.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            rank.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            rank.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            rank.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            rank.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            rank.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            rank.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            rank.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            rank.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            rank.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            rank.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            rank.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            rank.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            rank.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            rank.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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

        return rank;
    }
    public async Task InsertOrUpdateCardColonelRankAsync(Rank rank, string cardId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkSQL = @"
                SELECT COUNT(*) 
                FROM user_card_colonels_Rank 
                WHERE user_id = @user_id AND user_card_colonel_id = @card_id AND rank_id = @rank_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@card_id", cardId);
                    checkCommand.Parameters.AddWithValue("@rank_id", rank.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count > 0)
                    {
                        // UPDATE
                        string updateSQL = @"
                        UPDATE user_card_colonels_Rank
                        SET rank_level = @rank_level, power = @power, health = @health, 
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
                        WHERE user_id = @user_id AND user_card_colonel_id = @card_id AND rank_id = @rank_id;
                        ";
                        await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                        {
                            // Thêm tất cả các parameter như cũ
                            updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCommand.Parameters.AddWithValue("@card_id", cardId);
                            updateCommand.Parameters.AddWithValue("@rank_id", rank.Id);
                            updateCommand.Parameters.AddWithValue("@rank_level", rank.Level);
                            updateCommand.Parameters.AddWithValue("@power", rank.Power);
                            updateCommand.Parameters.AddWithValue("@health", rank.Health);
                            updateCommand.Parameters.AddWithValue("@physical_attack", rank.PhysicalAttack);
                            updateCommand.Parameters.AddWithValue("@physical_defense", rank.PhysicalDefense);
                            updateCommand.Parameters.AddWithValue("@magical_attack", rank.MagicalAttack);
                            updateCommand.Parameters.AddWithValue("@magical_defense", rank.MagicalDefense);
                            updateCommand.Parameters.AddWithValue("@chemical_attack", rank.ChemicalAttack);
                            updateCommand.Parameters.AddWithValue("@chemical_defense", rank.ChemicalDefense);
                            updateCommand.Parameters.AddWithValue("@atomic_attack", rank.AtomicAttack);
                            updateCommand.Parameters.AddWithValue("@atomic_defense", rank.AtomicDefense);
                            updateCommand.Parameters.AddWithValue("@mental_attack", rank.MentalAttack);
                            updateCommand.Parameters.AddWithValue("@mental_defense", rank.MentalDefense);
                            updateCommand.Parameters.AddWithValue("@speed", rank.Speed);
                            updateCommand.Parameters.AddWithValue("@critical_damage_rate", rank.CriticalDamageRate);
                            updateCommand.Parameters.AddWithValue("@critical_rate", rank.CriticalRate);
                            updateCommand.Parameters.AddWithValue("@critical_resistance_rate", rank.CriticalResistanceRate);
                            updateCommand.Parameters.AddWithValue("@ignore_critical_rate", rank.IgnoreCriticalRate);
                            updateCommand.Parameters.AddWithValue("@penetration_rate", rank.PenetrationRate);
                            updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", rank.PenetrationResistanceRate);
                            updateCommand.Parameters.AddWithValue("@evasion_rate", rank.EvasionRate);
                            updateCommand.Parameters.AddWithValue("@damage_absorption_rate", rank.DamageAbsorptionRate);
                            updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", rank.IgnoreDamageAbsorptionRate);
                            updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", rank.AbsorbedDamageRate);
                            updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", rank.VitalityRegenerationRate);
                            updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", rank.VitalityRegenerationResistanceRate);
                            updateCommand.Parameters.AddWithValue("@accuracy_rate", rank.AccuracyRate);
                            updateCommand.Parameters.AddWithValue("@lifesteal_rate", rank.LifestealRate);
                            updateCommand.Parameters.AddWithValue("@shield_strength", rank.ShieldStrength);
                            updateCommand.Parameters.AddWithValue("@tenacity", rank.Tenacity);
                            updateCommand.Parameters.AddWithValue("@resistance_rate", rank.ResistanceRate);
                            updateCommand.Parameters.AddWithValue("@combo_rate", rank.ComboRate);
                            updateCommand.Parameters.AddWithValue("@ignore_combo_rate", rank.IgnoreComboRate);
                            updateCommand.Parameters.AddWithValue("@combo_damage_rate", rank.ComboDamageRate);
                            updateCommand.Parameters.AddWithValue("@combo_resistance_rate", rank.ComboResistanceRate);
                            updateCommand.Parameters.AddWithValue("@stun_rate", rank.StunRate);
                            updateCommand.Parameters.AddWithValue("@ignore_stun_rate", rank.IgnoreStunRate);
                            updateCommand.Parameters.AddWithValue("@reflection_rate", rank.ReflectionRate);
                            updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", rank.IgnoreReflectionRate);
                            updateCommand.Parameters.AddWithValue("@reflection_damage_rate", rank.ReflectionDamageRate);
                            updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", rank.ReflectionResistanceRate);
                            updateCommand.Parameters.AddWithValue("@mana", rank.Mana);
                            updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", rank.ManaRegenerationRate);
                            updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", rank.DamageToDifferentFactionRate);
                            updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", rank.ResistanceToDifferentFactionRate);
                            updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", rank.DamageToSameFactionRate);
                            updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", rank.ResistanceToSameFactionRate);
                            updateCommand.Parameters.AddWithValue("@normal_damage_rate", rank.NormalDamageRate);
                            updateCommand.Parameters.AddWithValue("@normal_resistance_rate", rank.NormalResistanceRate);
                            updateCommand.Parameters.AddWithValue("@skill_damage_rate", rank.SkillDamageRate);
                            updateCommand.Parameters.AddWithValue("@skill_resistance_rate", rank.SkillResistanceRate);
                            updateCommand.Parameters.AddWithValue("@percent_all_health", rank.PercentAllHealth);
                            updateCommand.Parameters.AddWithValue("@percent_all_physical_attack", rank.PercentAllPhysicalAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_physical_defense", rank.PercentAllPhysicalDefense);
                            updateCommand.Parameters.AddWithValue("@percent_all_magical_attack", rank.PercentAllMagicalAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_magical_defense", rank.PercentAllMagicalDefense);
                            updateCommand.Parameters.AddWithValue("@percent_all_chemical_attack", rank.PercentAllChemicalAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_chemical_defense", rank.PercentAllChemicalDefense);
                            updateCommand.Parameters.AddWithValue("@percent_all_atomic_attack", rank.PercentAllAtomicAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_atomic_defense", rank.PercentAllAtomicDefense);
                            updateCommand.Parameters.AddWithValue("@percent_all_mental_attack", rank.PercentAllMentalAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_mental_defense", rank.PercentAllMentalDefense);

                            await updateCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // INSERT
                        string insertSQL = @"
                        INSERT INTO user_card_colonels_Rank 
                        (
                            user_id, user_card_colonel_id, rank_id, rank_level, 
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
                            @user_id, @card_id, @rank_id, @rank_level, 
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
                            insertCommand.Parameters.AddWithValue("@card_id", cardId);
                            insertCommand.Parameters.AddWithValue("@rank_id", rank.Id);
                            insertCommand.Parameters.AddWithValue("@rank_level", rank.Level == 0 ? 1 : rank.Level);
                            insertCommand.Parameters.AddWithValue("@power", rank.Power);
                            insertCommand.Parameters.AddWithValue("@health", rank.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", rank.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", rank.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", rank.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", rank.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", rank.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", rank.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", rank.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", rank.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", rank.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", rank.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", rank.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", rank.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", rank.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", rank.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", rank.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", rank.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", rank.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", rank.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", rank.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", rank.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", rank.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", rank.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", rank.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", rank.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", rank.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", rank.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", rank.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", rank.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", rank.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", rank.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", rank.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", rank.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", rank.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", rank.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", rank.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", rank.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", rank.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", rank.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", rank.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", rank.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", rank.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", rank.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", rank.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", rank.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", rank.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", rank.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", rank.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", rank.SkillResistanceRate);
                            insertCommand.Parameters.AddWithValue("@percent_all_health", rank.PercentAllHealth);
                            insertCommand.Parameters.AddWithValue("@percent_all_physical_attack", rank.PercentAllPhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_physical_defense", rank.PercentAllPhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@percent_all_magical_attack", rank.PercentAllMagicalAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_magical_defense", rank.PercentAllMagicalDefense);
                            insertCommand.Parameters.AddWithValue("@percent_all_chemical_attack", rank.PercentAllChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_chemical_defense", rank.PercentAllChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@percent_all_atomic_attack", rank.PercentAllAtomicAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_atomic_defense", rank.PercentAllAtomicDefense);
                            insertCommand.Parameters.AddWithValue("@percent_all_mental_attack", rank.PercentAllMentalAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_mental_defense", rank.PercentAllMentalDefense);
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
    public async Task<Rank> GetSumCardColonelsRankAsync(string userId, string cardId)
    {
        Rank rank = new Rank();
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
                FROM user_card_colonels_Rank 
                WHERE user_id = @user_id AND user_card_colonel_id = @card_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);
                    selectCommand.Parameters.AddWithValue("@card_id", cardId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            rank.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            rank.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            rank.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            rank.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            rank.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            rank.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            rank.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            rank.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            rank.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            rank.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            rank.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            rank.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            rank.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            rank.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            rank.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            rank.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            rank.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            rank.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            rank.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            rank.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            rank.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            rank.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            rank.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            rank.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            rank.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            rank.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            rank.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            rank.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            rank.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            rank.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            rank.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            rank.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            rank.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            rank.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            rank.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            rank.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            rank.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            rank.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            rank.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            rank.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            rank.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            rank.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            rank.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            rank.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            rank.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            rank.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            rank.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            rank.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            rank.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            rank.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            rank.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            rank.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            rank.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            rank.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            rank.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            rank.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            rank.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            rank.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            rank.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            rank.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            rank.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
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

        return rank;
    }
}