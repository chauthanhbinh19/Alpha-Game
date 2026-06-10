using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserCardSpellsMasterRepository : IUserCardSpellsMasterRepository
{
    public async Task<Master> GetCardSpellMasterAsync(string id, string cardId)
    {
        Master master = new Master();
        string userId = User.CurrentUserId;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT *
                FROM user_card_spells_master
                WHERE user_id = @user_id AND master_id = @id AND user_card_spell_id = @card_id;
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
    public async Task InsertOrUpdateCardSpellMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string checkSQL = @"
                SELECT COUNT(*) 
                FROM user_card_spells_master 
                WHERE user_id = @user_id AND user_card_spell_id = @card_id AND master_id = @master_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@card_id", cardId);
                    checkCommand.Parameters.AddWithValue("@master_id", userMaster.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count > 0)
                    {
                        // UPDATE
                        string updateSQL = @"
                        UPDATE user_card_spells_master
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
                        WHERE user_id = @user_id AND user_card_spell_id = @card_id AND master_id = @master_id;
                        ";
                        await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                        {
                            // Thêm tất cả các parameter như cũ
                            updateCommand.Parameters.AddWithValue("@user_id", userId);
                            updateCommand.Parameters.AddWithValue("@card_id", cardId);
                            updateCommand.Parameters.AddWithValue("@master_id", userMaster.Id);
                            updateCommand.Parameters.AddWithValue("@master_level", userMaster.Level);
                            updateCommand.Parameters.AddWithValue("@power", userMaster.Power);
                            updateCommand.Parameters.AddWithValue("@health", userMaster.Health);
                            updateCommand.Parameters.AddWithValue("@physical_attack", userMaster.PhysicalAttack);
                            updateCommand.Parameters.AddWithValue("@physical_defense", userMaster.PhysicalDefense);
                            updateCommand.Parameters.AddWithValue("@magical_attack", userMaster.MagicalAttack);
                            updateCommand.Parameters.AddWithValue("@magical_defense", userMaster.MagicalDefense);
                            updateCommand.Parameters.AddWithValue("@chemical_attack", userMaster.ChemicalAttack);
                            updateCommand.Parameters.AddWithValue("@chemical_defense", userMaster.ChemicalDefense);
                            updateCommand.Parameters.AddWithValue("@atomic_attack", userMaster.AtomicAttack);
                            updateCommand.Parameters.AddWithValue("@atomic_defense", userMaster.AtomicDefense);
                            updateCommand.Parameters.AddWithValue("@mental_attack", userMaster.MentalAttack);
                            updateCommand.Parameters.AddWithValue("@mental_defense", userMaster.MentalDefense);
                            updateCommand.Parameters.AddWithValue("@speed", userMaster.Speed);
                            updateCommand.Parameters.AddWithValue("@critical_damage_rate", userMaster.CriticalDamageRate);
                            updateCommand.Parameters.AddWithValue("@critical_rate", userMaster.CriticalRate);
                            updateCommand.Parameters.AddWithValue("@critical_resistance_rate", userMaster.CriticalResistanceRate);
                            updateCommand.Parameters.AddWithValue("@ignore_critical_rate", userMaster.IgnoreCriticalRate);
                            updateCommand.Parameters.AddWithValue("@penetration_rate", userMaster.PenetrationRate);
                            updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", userMaster.PenetrationResistanceRate);
                            updateCommand.Parameters.AddWithValue("@evasion_rate", userMaster.EvasionRate);
                            updateCommand.Parameters.AddWithValue("@damage_absorption_rate", userMaster.DamageAbsorptionRate);
                            updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", userMaster.IgnoreDamageAbsorptionRate);
                            updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", userMaster.AbsorbedDamageRate);
                            updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", userMaster.VitalityRegenerationRate);
                            updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", userMaster.VitalityRegenerationResistanceRate);
                            updateCommand.Parameters.AddWithValue("@accuracy_rate", userMaster.AccuracyRate);
                            updateCommand.Parameters.AddWithValue("@lifesteal_rate", userMaster.LifestealRate);
                            updateCommand.Parameters.AddWithValue("@shield_strength", userMaster.ShieldStrength);
                            updateCommand.Parameters.AddWithValue("@tenacity", userMaster.Tenacity);
                            updateCommand.Parameters.AddWithValue("@resistance_rate", userMaster.ResistanceRate);
                            updateCommand.Parameters.AddWithValue("@combo_rate", userMaster.ComboRate);
                            updateCommand.Parameters.AddWithValue("@ignore_combo_rate", userMaster.IgnoreComboRate);
                            updateCommand.Parameters.AddWithValue("@combo_damage_rate", userMaster.ComboDamageRate);
                            updateCommand.Parameters.AddWithValue("@combo_resistance_rate", userMaster.ComboResistanceRate);
                            updateCommand.Parameters.AddWithValue("@stun_rate", userMaster.StunRate);
                            updateCommand.Parameters.AddWithValue("@ignore_stun_rate", userMaster.IgnoreStunRate);
                            updateCommand.Parameters.AddWithValue("@reflection_rate", userMaster.ReflectionRate);
                            updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", userMaster.IgnoreReflectionRate);
                            updateCommand.Parameters.AddWithValue("@reflection_damage_rate", userMaster.ReflectionDamageRate);
                            updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", userMaster.ReflectionResistanceRate);
                            updateCommand.Parameters.AddWithValue("@mana", userMaster.Mana);
                            updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", userMaster.ManaRegenerationRate);
                            updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", userMaster.DamageToDifferentFactionRate);
                            updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", userMaster.ResistanceToDifferentFactionRate);
                            updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", userMaster.DamageToSameFactionRate);
                            updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", userMaster.ResistanceToSameFactionRate);
                            updateCommand.Parameters.AddWithValue("@normal_damage_rate", userMaster.NormalDamageRate);
                            updateCommand.Parameters.AddWithValue("@normal_resistance_rate", userMaster.NormalResistanceRate);
                            updateCommand.Parameters.AddWithValue("@skill_damage_rate", userMaster.SkillDamageRate);
                            updateCommand.Parameters.AddWithValue("@skill_resistance_rate", userMaster.SkillResistanceRate);
                            updateCommand.Parameters.AddWithValue("@percent_all_health", userMaster.PercentAllHealth);
                            updateCommand.Parameters.AddWithValue("@percent_all_physical_attack", userMaster.PercentAllPhysicalAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_physical_defense", userMaster.PercentAllPhysicalDefense);
                            updateCommand.Parameters.AddWithValue("@percent_all_magical_attack", userMaster.PercentAllMagicalAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_magical_defense", userMaster.PercentAllMagicalDefense);
                            updateCommand.Parameters.AddWithValue("@percent_all_chemical_attack", userMaster.PercentAllChemicalAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_chemical_defense", userMaster.PercentAllChemicalDefense);
                            updateCommand.Parameters.AddWithValue("@percent_all_atomic_attack", userMaster.PercentAllAtomicAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_atomic_defense", userMaster.PercentAllAtomicDefense);
                            updateCommand.Parameters.AddWithValue("@percent_all_mental_attack", userMaster.PercentAllMentalAttack);
                            updateCommand.Parameters.AddWithValue("@percent_all_mental_defense", userMaster.PercentAllMentalDefense);

                            await updateCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // INSERT
                        string insertSQL = @"
                        INSERT INTO user_card_spells_master 
                        (
                            user_id, user_card_spell_id, master_id, master_level, 
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
                            insertCommand.Parameters.AddWithValue("@user_id", userId);
                            insertCommand.Parameters.AddWithValue("@card_id", cardId);
                            insertCommand.Parameters.AddWithValue("@master_id", userMaster.Id);
                            insertCommand.Parameters.AddWithValue("@master_level", userMaster.Level == 0 ? 1 : userMaster.Level);
                            insertCommand.Parameters.AddWithValue("@power", userMaster.Power);
                            insertCommand.Parameters.AddWithValue("@health", userMaster.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", userMaster.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", userMaster.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", userMaster.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", userMaster.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", userMaster.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", userMaster.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", userMaster.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", userMaster.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", userMaster.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", userMaster.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", userMaster.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", userMaster.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", userMaster.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", userMaster.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", userMaster.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", userMaster.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", userMaster.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", userMaster.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", userMaster.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", userMaster.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", userMaster.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", userMaster.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", userMaster.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", userMaster.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", userMaster.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", userMaster.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", userMaster.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", userMaster.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", userMaster.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", userMaster.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", userMaster.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", userMaster.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", userMaster.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", userMaster.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", userMaster.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", userMaster.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", userMaster.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", userMaster.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", userMaster.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", userMaster.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", userMaster.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", userMaster.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", userMaster.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", userMaster.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", userMaster.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", userMaster.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", userMaster.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", userMaster.SkillResistanceRate);
                            insertCommand.Parameters.AddWithValue("@percent_all_health", userMaster.PercentAllHealth);
                            insertCommand.Parameters.AddWithValue("@percent_all_physical_attack", userMaster.PercentAllPhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_physical_defense", userMaster.PercentAllPhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@percent_all_magical_attack", userMaster.PercentAllMagicalAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_magical_defense", userMaster.PercentAllMagicalDefense);
                            insertCommand.Parameters.AddWithValue("@percent_all_chemical_attack", userMaster.PercentAllChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_chemical_defense", userMaster.PercentAllChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@percent_all_atomic_attack", userMaster.PercentAllAtomicAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_atomic_defense", userMaster.PercentAllAtomicDefense);
                            insertCommand.Parameters.AddWithValue("@percent_all_mental_attack", userMaster.PercentAllMentalAttack);
                            insertCommand.Parameters.AddWithValue("@percent_all_mental_defense", userMaster.PercentAllMentalDefense);
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
    public async Task<Master> GetSumCardSpellsMasterAsync(string userId, string cardId)
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
                FROM user_card_spells_master 
                WHERE user_id = @user_id AND user_card_spell_id = @card_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);
                    selectCommand.Parameters.AddWithValue("@card_id", cardId);

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