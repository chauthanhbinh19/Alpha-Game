using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserCardCaptainsRankRepository : IUserCardCaptainsRankRepository
{
    public async Task<Rank> GetCardCaptainRankAsync(string type, string card_id)
    {
        Rank Rank = new Rank();
        string user_id = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT *
                FROM user_card_Captains_Rank
                WHERE user_id = @user_id AND Rank_type = @type AND user_card_Captain_id = @card_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@card_id", card_id);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Rank.Type = reader.GetStringSafe("Rank_type");
                            Rank.Level = reader.GetIntSafe("Rank_level");
                            Rank.Power = reader.GetDoubleSafe("power");
                            Rank.Health = reader.GetDoubleSafe("health");
                            Rank.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            Rank.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            Rank.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            Rank.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            Rank.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            Rank.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            Rank.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            Rank.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            Rank.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            Rank.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            Rank.Speed = reader.GetDoubleSafe("speed");
                            Rank.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            Rank.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            Rank.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            Rank.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            Rank.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            Rank.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            Rank.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            Rank.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            Rank.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            Rank.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            Rank.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            Rank.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            Rank.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            Rank.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            Rank.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            Rank.Tenacity = reader.GetDoubleSafe("tenacity");
                            Rank.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            Rank.ComboRate = reader.GetDoubleSafe("combo_rate");
                            Rank.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            Rank.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            Rank.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            Rank.StunRate = reader.GetDoubleSafe("stun_rate");
                            Rank.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            Rank.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            Rank.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            Rank.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            Rank.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            Rank.Mana = reader.GetDoubleSafe("mana");
                            Rank.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            Rank.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            Rank.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            Rank.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            Rank.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            Rank.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            Rank.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            Rank.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            Rank.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            Rank.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            Rank.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            Rank.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            Rank.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            Rank.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            Rank.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            Rank.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            Rank.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            Rank.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            Rank.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            Rank.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
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

        return Rank;
    }
    public async Task InsertOrUpdateCardCaptainRankAsync(Rank Rank, string type, string card_id)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkQuery = @"
                SELECT COUNT(*) 
                FROM user_card_Captains_Rank 
                WHERE user_id = @user_id AND user_card_Captain_id = @card_id AND Rank_type = @Rank_type;
            ";

                await using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection))
                {
                    checkCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCmd.Parameters.AddWithValue("@card_id", card_id);
                    checkCmd.Parameters.AddWithValue("@Rank_type", type);

                    int count = Convert.ToInt32(await checkCmd.ExecuteScalarAsync());

                    if (count > 0)
                    {
                        // UPDATE
                        string updateQuery = @"
                        UPDATE user_card_Captains_Rank
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
                        WHERE user_id = @user_id AND user_card_Captain_id = @card_id AND Rank_type = @Rank_type;
                        ";
                        await using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, connection))
                        {
                            // Thêm tất cả các parameter như cũ
                            updateCmd.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            updateCmd.Parameters.AddWithValue("@card_id", card_id);
                            updateCmd.Parameters.AddWithValue("@Rank_type", type);
                            updateCmd.Parameters.AddWithValue("@Rank_level", Rank.Level);
                            updateCmd.Parameters.AddWithValue("@power", Rank.Power);
                            updateCmd.Parameters.AddWithValue("@health", Rank.Health);
                            updateCmd.Parameters.AddWithValue("@physical_attack", Rank.PhysicalAttack);
                            updateCmd.Parameters.AddWithValue("@physical_defense", Rank.PhysicalDefense);
                            updateCmd.Parameters.AddWithValue("@magical_attack", Rank.MagicalAttack);
                            updateCmd.Parameters.AddWithValue("@magical_defense", Rank.MagicalDefense);
                            updateCmd.Parameters.AddWithValue("@chemical_attack", Rank.ChemicalAttack);
                            updateCmd.Parameters.AddWithValue("@chemical_defense", Rank.ChemicalDefense);
                            updateCmd.Parameters.AddWithValue("@atomic_attack", Rank.AtomicAttack);
                            updateCmd.Parameters.AddWithValue("@atomic_defense", Rank.AtomicDefense);
                            updateCmd.Parameters.AddWithValue("@mental_attack", Rank.MentalAttack);
                            updateCmd.Parameters.AddWithValue("@mental_defense", Rank.MentalDefense);
                            updateCmd.Parameters.AddWithValue("@speed", Rank.Speed);
                            updateCmd.Parameters.AddWithValue("@critical_damage_rate", Rank.CriticalDamageRate);
                            updateCmd.Parameters.AddWithValue("@critical_rate", Rank.CriticalRate);
                            updateCmd.Parameters.AddWithValue("@critical_resistance_rate", Rank.CriticalResistanceRate);
                            updateCmd.Parameters.AddWithValue("@ignore_critical_rate", Rank.IgnoreCriticalRate);
                            updateCmd.Parameters.AddWithValue("@penetration_rate", Rank.PenetrationRate);
                            updateCmd.Parameters.AddWithValue("@penetration_resistance_rate", Rank.PenetrationResistanceRate);
                            updateCmd.Parameters.AddWithValue("@evasion_rate", Rank.EvasionRate);
                            updateCmd.Parameters.AddWithValue("@damage_absorption_rate", Rank.DamageAbsorptionRate);
                            updateCmd.Parameters.AddWithValue("@ignore_damage_absorption_rate", Rank.IgnoreDamageAbsorptionRate);
                            updateCmd.Parameters.AddWithValue("@absorbed_damage_rate", Rank.AbsorbedDamageRate);
                            updateCmd.Parameters.AddWithValue("@vitality_regeneration_rate", Rank.VitalityRegenerationRate);
                            updateCmd.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", Rank.VitalityRegenerationResistanceRate);
                            updateCmd.Parameters.AddWithValue("@accuracy_rate", Rank.AccuracyRate);
                            updateCmd.Parameters.AddWithValue("@lifesteal_rate", Rank.LifestealRate);
                            updateCmd.Parameters.AddWithValue("@shield_strength", Rank.ShieldStrength);
                            updateCmd.Parameters.AddWithValue("@tenacity", Rank.Tenacity);
                            updateCmd.Parameters.AddWithValue("@resistance_rate", Rank.ResistanceRate);
                            updateCmd.Parameters.AddWithValue("@combo_rate", Rank.ComboRate);
                            updateCmd.Parameters.AddWithValue("@ignore_combo_rate", Rank.IgnoreComboRate);
                            updateCmd.Parameters.AddWithValue("@combo_damage_rate", Rank.ComboDamageRate);
                            updateCmd.Parameters.AddWithValue("@combo_resistance_rate", Rank.ComboResistanceRate);
                            updateCmd.Parameters.AddWithValue("@stun_rate", Rank.StunRate);
                            updateCmd.Parameters.AddWithValue("@ignore_stun_rate", Rank.IgnoreStunRate);
                            updateCmd.Parameters.AddWithValue("@reflection_rate", Rank.ReflectionRate);
                            updateCmd.Parameters.AddWithValue("@ignore_reflection_rate", Rank.IgnoreReflectionRate);
                            updateCmd.Parameters.AddWithValue("@reflection_damage_rate", Rank.ReflectionDamageRate);
                            updateCmd.Parameters.AddWithValue("@reflection_resistance_rate", Rank.ReflectionResistanceRate);
                            updateCmd.Parameters.AddWithValue("@mana", Rank.Mana);
                            updateCmd.Parameters.AddWithValue("@mana_regeneration_rate", Rank.ManaRegenerationRate);
                            updateCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", Rank.DamageToDifferentFactionRate);
                            updateCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", Rank.ResistanceToDifferentFactionRate);
                            updateCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", Rank.DamageToSameFactionRate);
                            updateCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", Rank.ResistanceToSameFactionRate);
                            updateCmd.Parameters.AddWithValue("@normal_damage_rate", Rank.NormalDamageRate);
                            updateCmd.Parameters.AddWithValue("@normal_resistance_rate", Rank.NormalResistanceRate);
                            updateCmd.Parameters.AddWithValue("@skill_damage_rate", Rank.SkillDamageRate);
                            updateCmd.Parameters.AddWithValue("@skill_resistance_rate", Rank.SkillResistanceRate);
                            updateCmd.Parameters.AddWithValue("@percent_all_health", Rank.PercentAllHealth);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_attack", Rank.PercentAllPhysicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_physical_defense", Rank.PercentAllPhysicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_attack", Rank.PercentAllMagicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_magical_defense", Rank.PercentAllMagicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_attack", Rank.PercentAllChemicalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_chemical_defense", Rank.PercentAllChemicalDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_attack", Rank.PercentAllAtomicAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_atomic_defense", Rank.PercentAllAtomicDefense);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_attack", Rank.PercentAllMentalAttack);
                            updateCmd.Parameters.AddWithValue("@percent_all_mental_defense", Rank.PercentAllMentalDefense);

                            await updateCmd.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // INSERT
                        string insertQuery = @"
                        INSERT INTO user_card_Captains_Rank 
                        (
                            user_id, user_card_Captain_id, Rank_type, Rank_level, 
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
                            @user_id, @card_id, @Rank_type, @Rank_level, 
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
                            insertCmd.Parameters.AddWithValue("@Rank_type", type);
                            insertCmd.Parameters.AddWithValue("@Rank_level", Rank.Level == 0 ? 1 : Rank.Level);
                            insertCmd.Parameters.AddWithValue("@power", Rank.Power);
                            insertCmd.Parameters.AddWithValue("@health", Rank.Health);
                            insertCmd.Parameters.AddWithValue("@physical_attack", Rank.PhysicalAttack);
                            insertCmd.Parameters.AddWithValue("@physical_defense", Rank.PhysicalDefense);
                            insertCmd.Parameters.AddWithValue("@magical_attack", Rank.MagicalAttack);
                            insertCmd.Parameters.AddWithValue("@magical_defense", Rank.MagicalDefense);
                            insertCmd.Parameters.AddWithValue("@chemical_attack", Rank.ChemicalAttack);
                            insertCmd.Parameters.AddWithValue("@chemical_defense", Rank.ChemicalDefense);
                            insertCmd.Parameters.AddWithValue("@atomic_attack", Rank.AtomicAttack);
                            insertCmd.Parameters.AddWithValue("@atomic_defense", Rank.AtomicDefense);
                            insertCmd.Parameters.AddWithValue("@mental_attack", Rank.MentalAttack);
                            insertCmd.Parameters.AddWithValue("@mental_defense", Rank.MentalDefense);
                            insertCmd.Parameters.AddWithValue("@speed", Rank.Speed);
                            insertCmd.Parameters.AddWithValue("@critical_damage_rate", Rank.CriticalDamageRate);
                            insertCmd.Parameters.AddWithValue("@critical_rate", Rank.CriticalRate);
                            insertCmd.Parameters.AddWithValue("@critical_resistance_rate", Rank.CriticalResistanceRate);
                            insertCmd.Parameters.AddWithValue("@ignore_critical_rate", Rank.IgnoreCriticalRate);
                            insertCmd.Parameters.AddWithValue("@penetration_rate", Rank.PenetrationRate);
                            insertCmd.Parameters.AddWithValue("@penetration_resistance_rate", Rank.PenetrationResistanceRate);
                            insertCmd.Parameters.AddWithValue("@evasion_rate", Rank.EvasionRate);
                            insertCmd.Parameters.AddWithValue("@damage_absorption_rate", Rank.DamageAbsorptionRate);
                            insertCmd.Parameters.AddWithValue("@ignore_damage_absorption_rate", Rank.IgnoreDamageAbsorptionRate);
                            insertCmd.Parameters.AddWithValue("@absorbed_damage_rate", Rank.AbsorbedDamageRate);
                            insertCmd.Parameters.AddWithValue("@vitality_regeneration_rate", Rank.VitalityRegenerationRate);
                            insertCmd.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", Rank.VitalityRegenerationResistanceRate);
                            insertCmd.Parameters.AddWithValue("@accuracy_rate", Rank.AccuracyRate);
                            insertCmd.Parameters.AddWithValue("@lifesteal_rate", Rank.LifestealRate);
                            insertCmd.Parameters.AddWithValue("@shield_strength", Rank.ShieldStrength);
                            insertCmd.Parameters.AddWithValue("@tenacity", Rank.Tenacity);
                            insertCmd.Parameters.AddWithValue("@resistance_rate", Rank.ResistanceRate);
                            insertCmd.Parameters.AddWithValue("@combo_rate", Rank.ComboRate);
                            insertCmd.Parameters.AddWithValue("@ignore_combo_rate", Rank.IgnoreComboRate);
                            insertCmd.Parameters.AddWithValue("@combo_damage_rate", Rank.ComboDamageRate);
                            insertCmd.Parameters.AddWithValue("@combo_resistance_rate", Rank.ComboResistanceRate);
                            insertCmd.Parameters.AddWithValue("@stun_rate", Rank.StunRate);
                            insertCmd.Parameters.AddWithValue("@ignore_stun_rate", Rank.IgnoreStunRate);
                            insertCmd.Parameters.AddWithValue("@reflection_rate", Rank.ReflectionRate);
                            insertCmd.Parameters.AddWithValue("@ignore_reflection_rate", Rank.IgnoreReflectionRate);
                            insertCmd.Parameters.AddWithValue("@reflection_damage_rate", Rank.ReflectionDamageRate);
                            insertCmd.Parameters.AddWithValue("@reflection_resistance_rate", Rank.ReflectionResistanceRate);
                            insertCmd.Parameters.AddWithValue("@mana", Rank.Mana);
                            insertCmd.Parameters.AddWithValue("@mana_regeneration_rate", Rank.ManaRegenerationRate);
                            insertCmd.Parameters.AddWithValue("@damage_to_different_faction_rate", Rank.DamageToDifferentFactionRate);
                            insertCmd.Parameters.AddWithValue("@resistance_to_different_faction_rate", Rank.ResistanceToDifferentFactionRate);
                            insertCmd.Parameters.AddWithValue("@damage_to_same_faction_rate", Rank.DamageToSameFactionRate);
                            insertCmd.Parameters.AddWithValue("@resistance_to_same_faction_rate", Rank.ResistanceToSameFactionRate);
                            insertCmd.Parameters.AddWithValue("@normal_damage_rate", Rank.NormalDamageRate);
                            insertCmd.Parameters.AddWithValue("@normal_resistance_rate", Rank.NormalResistanceRate);
                            insertCmd.Parameters.AddWithValue("@skill_damage_rate", Rank.SkillDamageRate);
                            insertCmd.Parameters.AddWithValue("@skill_resistance_rate", Rank.SkillResistanceRate);
                            insertCmd.Parameters.AddWithValue("@percent_all_health", Rank.PercentAllHealth);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_attack", Rank.PercentAllPhysicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_physical_defense", Rank.PercentAllPhysicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_attack", Rank.PercentAllMagicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_magical_defense", Rank.PercentAllMagicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_attack", Rank.PercentAllChemicalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_chemical_defense", Rank.PercentAllChemicalDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_attack", Rank.PercentAllAtomicAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_atomic_defense", Rank.PercentAllAtomicDefense);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_attack", Rank.PercentAllMentalAttack);
                            insertCmd.Parameters.AddWithValue("@percent_all_mental_defense", Rank.PercentAllMentalDefense);
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
    public async Task<Rank> GetSumCardCaptainsRankAsync(string user_id, string card_id)
    {
        Rank Rank = new Rank();
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
                FROM user_card_Captains_Rank 
                WHERE user_id = @user_id AND user_card_Captain_id = @card_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@card_id", card_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Rank.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            Rank.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            Rank.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            Rank.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            Rank.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            Rank.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            Rank.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            Rank.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            Rank.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            Rank.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            Rank.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            Rank.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            Rank.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            Rank.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            Rank.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            Rank.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            Rank.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            Rank.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            Rank.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            Rank.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            Rank.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            Rank.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            Rank.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            Rank.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            Rank.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            Rank.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            Rank.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            Rank.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            Rank.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            Rank.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            Rank.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            Rank.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            Rank.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            Rank.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            Rank.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            Rank.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            Rank.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            Rank.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            Rank.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            Rank.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            Rank.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            Rank.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            Rank.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            Rank.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            Rank.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            Rank.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            Rank.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            Rank.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            Rank.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            Rank.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            Rank.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            Rank.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            Rank.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            Rank.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            Rank.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            Rank.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            Rank.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            Rank.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            Rank.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            Rank.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            Rank.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
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

        return Rank;
    }
}