using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserBooksRankRepository : IUserBooksRankRepository
{
    public async Task<Rank> GetBookRankAsync(string id, string card_id)
    {
        Rank rank = new Rank();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                    SELECT *
                    FROM user_books_Rank
                    WHERE user_id = @user_id AND rank_id = @id AND user_book_id = @card_id;
                ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@card_id", card_id);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            rank.Id = reader.GetStringSafe("rank_id");
                            rank.Level = reader.GetIntSafe("Rank_level");
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
    public async Task InsertOrUpdateBookRankAsync(Rank rank, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkQuery = @"
                SELECT COUNT(*) 
                FROM user_books_Rank 
                WHERE user_id = @user_id AND user_book_id = @card_id AND rank_id = @rank_id;
            ";

                await using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@card_id", card_id);
                    checkCmd.Parameters.AddWithValue("@rank_id", rank.Id);

                    int count = Convert.ToInt32(await checkCmd.ExecuteScalarAsync());

                    if (count > 0)
                    {
                        // UPDATE
                        string updateQuery = @"
                        UPDATE user_books_Rank
                        SET Rank_level = @Rank_level, power = @power, health = @health, 
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
                        WHERE user_id = @user_id AND user_book_id = @card_id AND rank_id = @rank_id;
                        ";
                        await using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            // Thêm tất cả các parameter như cũ
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@rank_id", rank.Id);
                            updateCmd.Parameters.AddWithValue("@Rank_level", rank.Level);
                            updateCmd.Parameters.AddWithValue("@power", rank.Power);
                            updateCmd.Parameters.AddWithValue("@health", rank.Health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", rank.PhysicalAttack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", rank.PhysicalDefense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", rank.MagicalAttack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", rank.MagicalDefense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", rank.ChemicalAttack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", rank.ChemicalDefense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", rank.AtomicAttack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", rank.AtomicDefense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", rank.MentalAttack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", rank.MentalDefense);
                            updateCmd.Parameters.AddWithValue("@speed", rank.Speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage_rate", rank.CriticalDamageRate);
                            updateCmd.Parameters.AddWithValue("@critical_rate", rank.CriticalRate);
                            updateCmd.Parameters.AddWithValue("@critical_resistance_rate", rank.CriticalResistanceRate);
                            updateCmd.Parameters.AddWithValue("@ignore_critical_rate", rank.IgnoreCriticalRate);
                            updateCmd.Parameters.AddWithValue("@penetration_rate", rank.PenetrationRate);
                            updateCmd.Parameters.AddWithValue("@penetration_resistance_rate", rank.PenetrationResistanceRate);
                            updateCmd.Parameters.AddWithValue("@evasion_rate", rank.EvasionRate);
                            updateCmd.Parameters.AddWithValue("@damage_absorption_rate", rank.DamageAbsorptionRate);
                            updateCmd.Parameters.AddWithValue("@ignore_damage_absorption_rate", rank.IgnoreDamageAbsorptionRate);
                            updateCmd.Parameters.AddWithValue("@absorbed_damage_rate", rank.AbsorbedDamageRate);
                            updateCmd.Parameters.AddWithValue("@vitality_regeneration_rate", rank.VitalityRegenerationRate);
                            updateCmd.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", rank.VitalityRegenerationResistanceRate);
                            updateCmd.Parameters.AddWithValue("@accuracy_rate", rank.AccuracyRate);
                            updateCmd.Parameters.AddWithValue("@lifesteal_rate", rank.LifestealRate);
                            updateCmd.Parameters.AddWithValue("@shield_strength", rank.ShieldStrength);
                            updateCmd.Parameters.AddWithValue("@tenacity", rank.Tenacity);
                            updateCmd.Parameters.AddWithValue("@resistance_rate", rank.ResistanceRate);
                            updateCmd.Parameters.AddWithValue("@combo_rate", rank.ComboRate);
                            updateCmd.Parameters.AddWithValue("@ignore_combo_rate", rank.IgnoreComboRate);
                            updateCmd.Parameters.AddWithValue("@combo_damage_rate", rank.ComboDamageRate);
                            updateCmd.Parameters.AddWithValue("@combo_resistance_rate", rank.ComboResistanceRate);
                            updateCmd.Parameters.AddWithValue("@stun_rate", rank.StunRate);
                            updateCmd.Parameters.AddWithValue("@ignore_stun_rate", rank.IgnoreStunRate);
                            updateCmd.Parameters.AddWithValue("@reflection_rate", rank.ReflectionRate);
                            updateCmd.Parameters.AddWithValue("@ignore_reflection_rate", rank.IgnoreReflectionRate);
                            updateCmd.Parameters.AddWithValue("@reflection_damage_rate", rank.ReflectionDamageRate);
                            updateCmd.Parameters.AddWithValue("@reflection_resistance_rate", rank.ReflectionResistanceRate);
                            updateCmd.Parameters.AddWithValue("@mana", rank.Mana);
                            updateCmd.Parameters.AddWithValue("@mana_regeneration_rate", rank.ManaRegenerationRate);
                            updateCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", rank.DamageToDifferentFactionRate);
                            updateCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", rank.ResistanceToDifferentFactionRate);
                            updateCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", rank.DamageToSameFactionRate);
                            updateCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", rank.ResistanceToSameFactionRate);
                            updateCmd.Parameters.AddWithValue("@normal_damage_rate", rank.NormalDamageRate);
                            updateCmd.Parameters.AddWithValue("@normal_resistance_rate", rank.NormalResistanceRate);
                            updateCmd.Parameters.AddWithValue("@skill_damage_rate", rank.SkillDamageRate);
                            updateCmd.Parameters.AddWithValue("@skill_resistance_rate", rank.SkillResistanceRate);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", rank.PercentAllHealth);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.PercentAllPhysicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.PercentAllPhysicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.PercentAllMagicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.PercentAllMagicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.PercentAllChemicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.PercentAllChemicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.PercentAllAtomicAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.PercentAllAtomicDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.PercentAllMentalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.PercentAllMentalDefense);

                            await updateCmd.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // INSERT
                        string insertQuery = @"
                        INSERT INTO user_books_Rank 
                        (
                            user_id, user_book_id, rank_id, Rank_level, 
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
                            @user_id, @card_id, @rank_id, @Rank_level, 
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
                        await using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, connection))
                        {
                            // Thêm các parameter giống như trên (giữ nguyên)
                            insertCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCmd.Parameters.AddWithValue("@card_id", card_id);
                            insertCmd.Parameters.AddWithValue("@rank_id", rank.Id);
                            insertCmd.Parameters.AddWithValue("@Rank_level", rank.Level == 0 ? 1 : rank.Level);
                            insertCmd.Parameters.AddWithValue("@power", rank.Power);
                            insertCmd.Parameters.AddWithValue("@health", rank.Health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", rank.PhysicalAttack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", rank.PhysicalDefense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", rank.MagicalAttack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", rank.MagicalDefense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", rank.ChemicalAttack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", rank.ChemicalDefense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", rank.AtomicAttack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", rank.AtomicDefense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", rank.MentalAttack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", rank.MentalDefense);
                            insertCmd.Parameters.AddWithValue("@speed", rank.Speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage_rate", rank.CriticalDamageRate);
                            insertCmd.Parameters.AddWithValue("@critical_rate", rank.CriticalRate);
                            insertCmd.Parameters.AddWithValue("@critical_resistance_rate", rank.CriticalResistanceRate);
                            insertCmd.Parameters.AddWithValue("@ignore_critical_rate", rank.IgnoreCriticalRate);
                            insertCmd.Parameters.AddWithValue("@penetration_rate", rank.PenetrationRate);
                            insertCmd.Parameters.AddWithValue("@penetration_resistance_rate", rank.PenetrationResistanceRate);
                            insertCmd.Parameters.AddWithValue("@evasion_rate", rank.EvasionRate);
                            insertCmd.Parameters.AddWithValue("@damage_absorption_rate", rank.DamageAbsorptionRate);
                            insertCmd.Parameters.AddWithValue("@ignore_damage_absorption_rate", rank.IgnoreDamageAbsorptionRate);
                            insertCmd.Parameters.AddWithValue("@absorbed_damage_rate", rank.AbsorbedDamageRate);
                            insertCmd.Parameters.AddWithValue("@vitality_regeneration_rate", rank.VitalityRegenerationRate);
                            insertCmd.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", rank.VitalityRegenerationResistanceRate);
                            insertCmd.Parameters.AddWithValue("@accuracy_rate", rank.AccuracyRate);
                            insertCmd.Parameters.AddWithValue("@lifesteal_rate", rank.LifestealRate);
                            insertCmd.Parameters.AddWithValue("@shield_strength", rank.ShieldStrength);
                            insertCmd.Parameters.AddWithValue("@tenacity", rank.Tenacity);
                            insertCmd.Parameters.AddWithValue("@resistance_rate", rank.ResistanceRate);
                            insertCmd.Parameters.AddWithValue("@combo_rate", rank.ComboRate);
                            insertCmd.Parameters.AddWithValue("@ignore_combo_rate", rank.IgnoreComboRate);
                            insertCmd.Parameters.AddWithValue("@combo_damage_rate", rank.ComboDamageRate);
                            insertCmd.Parameters.AddWithValue("@combo_resistance_rate", rank.ComboResistanceRate);
                            insertCmd.Parameters.AddWithValue("@stun_rate", rank.StunRate);
                            insertCmd.Parameters.AddWithValue("@ignore_stun_rate", rank.IgnoreStunRate);
                            insertCmd.Parameters.AddWithValue("@reflection_rate", rank.ReflectionRate);
                            insertCmd.Parameters.AddWithValue("@ignore_reflection_rate", rank.IgnoreReflectionRate);
                            insertCmd.Parameters.AddWithValue("@reflection_damage_rate", rank.ReflectionDamageRate);
                            insertCmd.Parameters.AddWithValue("@reflection_resistance_rate", rank.ReflectionResistanceRate);
                            insertCmd.Parameters.AddWithValue("@mana", rank.Mana);
                            insertCmd.Parameters.AddWithValue("@mana_regeneration_rate", rank.ManaRegenerationRate);
                            insertCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", rank.DamageToDifferentFactionRate);
                            insertCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", rank.ResistanceToDifferentFactionRate);
                            insertCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", rank.DamageToSameFactionRate);
                            insertCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", rank.ResistanceToSameFactionRate);
                            insertCmd.Parameters.AddWithValue("@normal_damage_rate", rank.NormalDamageRate);
                            insertCmd.Parameters.AddWithValue("@normal_resistance_rate", rank.NormalResistanceRate);
                            insertCmd.Parameters.AddWithValue("@skill_damage_rate", rank.SkillDamageRate);
                            insertCmd.Parameters.AddWithValue("@skill_resistance_rate", rank.SkillResistanceRate);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", rank.PercentAllHealth);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", rank.PercentAllPhysicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", rank.PercentAllPhysicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", rank.PercentAllMagicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", rank.PercentAllMagicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", rank.PercentAllChemicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", rank.PercentAllChemicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", rank.PercentAllAtomicAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", rank.PercentAllAtomicDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", rank.PercentAllMentalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", rank.PercentAllMentalDefense);
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
    public async Task<Rank> GetSumBooksRankAsync(string user_id, string card_id)
    {
        Rank rank = new Rank();
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
                FROM user_books_Rank 
                WHERE user_id = @user_id AND user_book_id = @card_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_id", card_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
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