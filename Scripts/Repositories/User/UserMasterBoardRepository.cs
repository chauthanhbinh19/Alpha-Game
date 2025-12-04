using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
public class UserMasterBoardRepository : IUserMasterBoardRepository
{
    public async Task<List<MasterBoard>> GetUserMasterBoardAsync(string user_id, string name)
    {
        List<MasterBoard> masterBoards = new List<MasterBoard>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT mb.*, 
                       CASE WHEN umb.name IS NULL THEN 'block' ELSE 'available' END AS status
                FROM master_board mb
                LEFT JOIN user_master_board umb 
                       ON mb.name = umb.name AND mb.node_id = umb.node_id AND umb.user_id = @userId
                WHERE mb.name = @name";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@name", name);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            MasterBoard masterBoard = new MasterBoard
                            {
                                Id = reader.GetString("node_id"),
                                Name = reader.GetString("name"),
                                RankLevel = reader.GetString("rank_level"),
                                Type = reader.GetString("type"),
                                PositionX = reader.GetInt32("position_x"),
                                PositionY = reader.GetInt32("position_y"),
                                Status = reader.GetString("status"),
                                Power = reader.GetDouble("power"),
                                Health = reader.GetDouble("health"),
                                PhysicalAttack = reader.GetDouble("physical_attack"),
                                PhysicalDefense = reader.GetDouble("physical_defense"),
                                MagicalAttack = reader.GetDouble("magical_attack"),
                                MagicalDefense = reader.GetDouble("magical_defense"),
                                ChemicalAttack = reader.GetDouble("chemical_attack"),
                                ChemicalDefense = reader.GetDouble("chemical_defense"),
                                AtomicAttack = reader.GetDouble("atomic_attack"),
                                AtomicDefense = reader.GetDouble("atomic_defense"),
                                MentalAttack = reader.GetDouble("mental_attack"),
                                MentalDefense = reader.GetDouble("mental_defense"),
                                Speed = reader.GetDouble("speed"),
                                CriticalDamageRate = reader.GetDouble("critical_damage_rate"),
                                CriticalRate = reader.GetDouble("critical_rate"),
                                CriticalResistanceRate = reader.GetDouble("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.GetDouble("ignore_critical_rate"),
                                PenetrationRate = reader.GetDouble("penetration_rate"),
                                PenetrationResistanceRate = reader.GetDouble("penetration_resistance_rate"),
                                EvasionRate = reader.GetDouble("evasion_rate"),
                                DamageAbsorptionRate = reader.GetDouble("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.GetDouble("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.GetDouble("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.GetDouble("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.GetDouble("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.GetDouble("accuracy_rate"),
                                LifestealRate = reader.GetDouble("lifesteal_rate"),
                                ShieldStrength = reader.GetDouble("shield_strength"),
                                Tenacity = reader.GetDouble("tenacity"),
                                ResistanceRate = reader.GetDouble("resistance_rate"),
                                ComboRate = reader.GetDouble("combo_rate"),
                                IgnoreComboRate = reader.GetDouble("ignore_combo_rate"),
                                ComboDamageRate = reader.GetDouble("combo_damage_rate"),
                                ComboResistanceRate = reader.GetDouble("combo_resistance_rate"),
                                StunRate = reader.GetDouble("stun_rate"),
                                IgnoreStunRate = reader.GetDouble("ignore_stun_rate"),
                                ReflectionRate = reader.GetDouble("reflection_rate"),
                                IgnoreReflectionRate = reader.GetDouble("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.GetDouble("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.GetDouble("reflection_resistance_rate"),
                                Mana = reader.GetFloat("mana"),
                                ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                                NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                                SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                                SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                                PercentAllHealth = reader.GetDouble("percent_all_health"),
                                PercentAllPhysicalAttack = reader.GetDouble("percent_all_physical_attack"),
                                PercentAllPhysicalDefense = reader.GetDouble("percent_all_physical_defense"),
                                PercentAllMagicalAttack = reader.GetDouble("percent_all_magical_attack"),
                                PercentAllMagicalDefense = reader.GetDouble("percent_all_magical_defense"),
                                PercentAllChemicalAttack = reader.GetDouble("percent_all_chemical_attack"),
                                PercentAllChemicalDefense = reader.GetDouble("percent_all_chemical_defense"),
                                PercentAllAtomicAttack = reader.GetDouble("percent_all_atomic_attack"),
                                PercentAllAtomicDefense = reader.GetDouble("percent_all_atomic_defense"),
                                PercentAllMentalAttack = reader.GetDouble("percent_all_mental_attack"),
                                PercentAllMentalDefense = reader.GetDouble("percent_all_mental_defense"),
                            };

                            masterBoards.Add(masterBoard);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return masterBoards;
    }
    public async Task InsertUserMasterBoardAsync(string user_id, MasterBoard masterBoard)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                INSERT INTO user_master_board (
                    user_id, name, node_id, rank_level,
                    power, health, physical_attack, physical_defense, magical_attack, magical_defense,
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
                    skill_damage_rate, skill_resistance_rate, 
                    percent_all_health, percent_all_physical_attack, percent_all_physical_defense,
                    percent_all_magical_attack, percent_all_magical_defense,
                    percent_all_chemical_attack, percent_all_chemical_defense,
                    percent_all_atomic_attack, percent_all_atomic_defense,
                    percent_all_mental_attack, percent_all_mental_defense
                )
                VALUES (
                    @user_id, @name, @node_id, @rank_level,
                    @power, @health, @physical_attack, @physical_defense, @magical_attack, @magical_defense,
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
                    @skill_damage_rate, @skill_resistance_rate,
                    @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense,
                    @percent_all_magical_attack, @percent_all_magical_defense,
                    @percent_all_chemical_attack, @percent_all_chemical_defense,
                    @percent_all_atomic_attack, @percent_all_atomic_defense,
                    @percent_all_mental_attack, @percent_all_mental_defense
                )";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@name", masterBoard.Name);
                    command.Parameters.AddWithValue("@node_id", masterBoard.Id);
                    command.Parameters.AddWithValue("@rank_level", masterBoard.RankLevel);

                    command.Parameters.AddWithValue("@power", masterBoard.Power);
                    command.Parameters.AddWithValue("@health", masterBoard.Health);
                    command.Parameters.AddWithValue("@physical_attack", masterBoard.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", masterBoard.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", masterBoard.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", masterBoard.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", masterBoard.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", masterBoard.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", masterBoard.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", masterBoard.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", masterBoard.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", masterBoard.MentalDefense);
                    command.Parameters.AddWithValue("@speed", masterBoard.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", masterBoard.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", masterBoard.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", masterBoard.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", masterBoard.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", masterBoard.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", masterBoard.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", masterBoard.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", masterBoard.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", masterBoard.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", masterBoard.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", masterBoard.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", masterBoard.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", masterBoard.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", masterBoard.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", masterBoard.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", masterBoard.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", masterBoard.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", masterBoard.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", masterBoard.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", masterBoard.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", masterBoard.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", masterBoard.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", masterBoard.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", masterBoard.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", masterBoard.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", masterBoard.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", masterBoard.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", masterBoard.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", masterBoard.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", masterBoard.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", masterBoard.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", masterBoard.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", masterBoard.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", masterBoard.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", masterBoard.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", masterBoard.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", masterBoard.SkillResistanceRate);

                    command.Parameters.AddWithValue("@percent_all_health", masterBoard.PercentAllHealth);
                    command.Parameters.AddWithValue("@percent_all_physical_attack", masterBoard.PercentAllPhysicalAttack);
                    command.Parameters.AddWithValue("@percent_all_physical_defense", masterBoard.PercentAllPhysicalDefense);
                    command.Parameters.AddWithValue("@percent_all_magical_attack", masterBoard.PercentAllMagicalAttack);
                    command.Parameters.AddWithValue("@percent_all_magical_defense", masterBoard.PercentAllMagicalDefense);
                    command.Parameters.AddWithValue("@percent_all_chemical_attack", masterBoard.PercentAllChemicalAttack);
                    command.Parameters.AddWithValue("@percent_all_chemical_defense", masterBoard.PercentAllChemicalDefense);
                    command.Parameters.AddWithValue("@percent_all_atomic_attack", masterBoard.PercentAllAtomicAttack);
                    command.Parameters.AddWithValue("@percent_all_atomic_defense", masterBoard.PercentAllAtomicDefense);
                    command.Parameters.AddWithValue("@percent_all_mental_attack", masterBoard.PercentAllMentalAttack);
                    command.Parameters.AddWithValue("@percent_all_mental_defense", masterBoard.PercentAllMentalDefense);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
    public async Task UpdateUserMasterBoardAsync(string user_id, MasterBoard masterBoard)
    {
        int multiplier = QualityEvaluator.CheckQuality(masterBoard.RankLevel);
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_master_board SET
                rank_level = @rank_level,
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
                percent_all_health = @percent_all_health,
                percent_all_physical_attack = @percent_all_physical_attack,
                percent_all_physical_defense = @percent_all_physical_defense,
                percent_all_magical_attack = @percent_all_magical_attack,
                percent_all_magical_defense = @percent_all_magical_defense,
                percent_all_chemical_attack = @percent_all_chemical_attack,
                percent_all_chemical_defense = @percent_all_chemical_defense,
                percent_all_atomic_attack = @percent_all_atomic_attack,
                percent_all_atomic_defense = @percent_all_atomic_defense,
                percent_all_mental_attack = @percent_all_mental_attack,
                percent_all_mental_defense = @percent_all_mental_defense
            WHERE
                user_id = @user_id AND name = @name AND node_id = @node_id
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", user_id);
                    command.Parameters.AddWithValue("@name", masterBoard.Name);
                    command.Parameters.AddWithValue("@node_id", masterBoard.Id);
                    command.Parameters.AddWithValue("@rank_level", masterBoard.RankLevel);

                    // Các giá trị số nhân với multiplier
                    command.Parameters.AddWithValue("@power", masterBoard.Power);
                    command.Parameters.AddWithValue("@health", masterBoard.Health * multiplier);
                    command.Parameters.AddWithValue("@physical_attack", masterBoard.PhysicalAttack * multiplier);
                    command.Parameters.AddWithValue("@physical_defense", masterBoard.PhysicalDefense * multiplier);
                    command.Parameters.AddWithValue("@magical_attack", masterBoard.MagicalAttack * multiplier);
                    command.Parameters.AddWithValue("@magical_defense", masterBoard.MagicalDefense * multiplier);
                    command.Parameters.AddWithValue("@chemical_attack", masterBoard.ChemicalAttack * multiplier);
                    command.Parameters.AddWithValue("@chemical_defense", masterBoard.ChemicalDefense * multiplier);
                    command.Parameters.AddWithValue("@atomic_attack", masterBoard.AtomicAttack * multiplier);
                    command.Parameters.AddWithValue("@atomic_defense", masterBoard.AtomicDefense * multiplier);
                    command.Parameters.AddWithValue("@mental_attack", masterBoard.MentalAttack * multiplier);
                    command.Parameters.AddWithValue("@mental_defense", masterBoard.MentalDefense * multiplier);
                    command.Parameters.AddWithValue("@speed", masterBoard.Speed * multiplier);
                    command.Parameters.AddWithValue("@critical_damage_rate", masterBoard.CriticalDamageRate * multiplier);
                    command.Parameters.AddWithValue("@critical_rate", masterBoard.CriticalRate * multiplier);
                    command.Parameters.AddWithValue("@critical_resistance_rate", masterBoard.CriticalResistanceRate * multiplier);
                    command.Parameters.AddWithValue("@ignore_critical_rate", masterBoard.IgnoreCriticalRate * multiplier);
                    command.Parameters.AddWithValue("@penetration_rate", masterBoard.PenetrationRate * multiplier);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", masterBoard.PenetrationResistanceRate * multiplier);
                    command.Parameters.AddWithValue("@evasion_rate", masterBoard.EvasionRate * multiplier);
                    command.Parameters.AddWithValue("@damage_absorption_rate", masterBoard.DamageAbsorptionRate * multiplier);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", masterBoard.IgnoreDamageAbsorptionRate * multiplier);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", masterBoard.AbsorbedDamageRate * multiplier);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", masterBoard.VitalityRegenerationRate * multiplier);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", masterBoard.VitalityRegenerationResistanceRate * multiplier);
                    command.Parameters.AddWithValue("@accuracy_rate", masterBoard.AccuracyRate * multiplier);
                    command.Parameters.AddWithValue("@lifesteal_rate", masterBoard.LifestealRate * multiplier);
                    command.Parameters.AddWithValue("@shield_strength", masterBoard.ShieldStrength * multiplier);
                    command.Parameters.AddWithValue("@tenacity", masterBoard.Tenacity * multiplier);
                    command.Parameters.AddWithValue("@resistance_rate", masterBoard.ResistanceRate * multiplier);
                    command.Parameters.AddWithValue("@combo_rate", masterBoard.ComboRate * multiplier);
                    command.Parameters.AddWithValue("@ignore_combo_rate", masterBoard.IgnoreComboRate * multiplier);
                    command.Parameters.AddWithValue("@combo_damage_rate", masterBoard.ComboDamageRate * multiplier);
                    command.Parameters.AddWithValue("@combo_resistance_rate", masterBoard.ComboResistanceRate * multiplier);
                    command.Parameters.AddWithValue("@stun_rate", masterBoard.StunRate * multiplier);
                    command.Parameters.AddWithValue("@ignore_stun_rate", masterBoard.IgnoreStunRate * multiplier);
                    command.Parameters.AddWithValue("@reflection_rate", masterBoard.ReflectionRate * multiplier);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", masterBoard.IgnoreReflectionRate * multiplier);
                    command.Parameters.AddWithValue("@reflection_damage_rate", masterBoard.ReflectionDamageRate * multiplier);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", masterBoard.ReflectionResistanceRate * multiplier);
                    command.Parameters.AddWithValue("@mana", masterBoard.Mana * multiplier);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", masterBoard.ManaRegenerationRate * multiplier);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", masterBoard.DamageToDifferentFactionRate * multiplier);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", masterBoard.ResistanceToDifferentFactionRate * multiplier);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", masterBoard.DamageToSameFactionRate * multiplier);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", masterBoard.ResistanceToSameFactionRate * multiplier);
                    command.Parameters.AddWithValue("@normal_damage_rate", masterBoard.NormalDamageRate * multiplier);
                    command.Parameters.AddWithValue("@normal_resistance_rate", masterBoard.NormalResistanceRate * multiplier);
                    command.Parameters.AddWithValue("@skill_damage_rate", masterBoard.SkillDamageRate * multiplier);
                    command.Parameters.AddWithValue("@skill_resistance_rate", masterBoard.SkillResistanceRate * multiplier);

                    command.Parameters.AddWithValue("@percent_all_health", masterBoard.PercentAllHealth * multiplier);
                    command.Parameters.AddWithValue("@percent_all_physical_attack", masterBoard.PercentAllPhysicalAttack * multiplier);
                    command.Parameters.AddWithValue("@percent_all_physical_defense", masterBoard.PercentAllPhysicalDefense * multiplier);
                    command.Parameters.AddWithValue("@percent_all_magical_attack", masterBoard.PercentAllMagicalAttack * multiplier);
                    command.Parameters.AddWithValue("@percent_all_magical_defense", masterBoard.PercentAllMagicalDefense * multiplier);
                    command.Parameters.AddWithValue("@percent_all_chemical_attack", masterBoard.PercentAllChemicalAttack * multiplier);
                    command.Parameters.AddWithValue("@percent_all_chemical_defense", masterBoard.PercentAllChemicalDefense * multiplier);
                    command.Parameters.AddWithValue("@percent_all_atomic_attack", masterBoard.PercentAllAtomicAttack * multiplier);
                    command.Parameters.AddWithValue("@percent_all_atomic_defense", masterBoard.PercentAllAtomicDefense * multiplier);
                    command.Parameters.AddWithValue("@percent_all_mental_attack", masterBoard.PercentAllMentalAttack * multiplier);
                    command.Parameters.AddWithValue("@percent_all_mental_defense", masterBoard.PercentAllMentalDefense * multiplier);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }
    }
}