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

                string selectSQL = @"
                SELECT mb.*, 
                       CASE WHEN umb.name IS NULL THEN 'block' ELSE 'available' END AS status
                FROM master_board mb
                LEFT JOIN user_master_board umb 
                       ON mb.name = umb.name AND mb.node_id = umb.node_id AND umb.user_id = @userId
                WHERE mb.name = @name";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", user_id);
                    selectCommand.Parameters.AddWithValue("@name", name);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            MasterBoard masterBoard = new MasterBoard
                            {
                                Id = reader.GetStringSafe("node_id"),
                                Name = reader.GetStringSafe("name"),
                                RankLevel = reader.GetStringSafe("rank_level"),
                                Type = reader.GetStringSafe("type"),
                                PositionX = reader.GetIntSafe("position_x"),
                                PositionY = reader.GetIntSafe("position_y"),
                                Status = reader.GetStringSafe("status"),
                                Power = reader.GetDoubleSafe("power"),
                                Health = reader.GetDoubleSafe("health"),
                                PhysicalAttack = reader.GetDoubleSafe("physical_attack"),
                                PhysicalDefense = reader.GetDoubleSafe("physical_defense"),
                                MagicalAttack = reader.GetDoubleSafe("magical_attack"),
                                MagicalDefense = reader.GetDoubleSafe("magical_defense"),
                                ChemicalAttack = reader.GetDoubleSafe("chemical_attack"),
                                ChemicalDefense = reader.GetDoubleSafe("chemical_defense"),
                                AtomicAttack = reader.GetDoubleSafe("atomic_attack"),
                                AtomicDefense = reader.GetDoubleSafe("atomic_defense"),
                                MentalAttack = reader.GetDoubleSafe("mental_attack"),
                                MentalDefense = reader.GetDoubleSafe("mental_defense"),
                                Speed = reader.GetDoubleSafe("speed"),
                                CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate"),
                                CriticalRate = reader.GetDoubleSafe("critical_rate"),
                                CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate"),
                                IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate"),
                                PenetrationRate = reader.GetDoubleSafe("penetration_rate"),
                                PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate"),
                                EvasionRate = reader.GetDoubleSafe("evasion_rate"),
                                DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate"),
                                IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate"),
                                AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate"),
                                VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate"),
                                VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate"),
                                AccuracyRate = reader.GetDoubleSafe("accuracy_rate"),
                                LifestealRate = reader.GetDoubleSafe("lifesteal_rate"),
                                ShieldStrength = reader.GetDoubleSafe("shield_strength"),
                                Tenacity = reader.GetDoubleSafe("tenacity"),
                                ResistanceRate = reader.GetDoubleSafe("resistance_rate"),
                                ComboRate = reader.GetDoubleSafe("combo_rate"),
                                IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate"),
                                ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate"),
                                ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate"),
                                StunRate = reader.GetDoubleSafe("stun_rate"),
                                IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate"),
                                ReflectionRate = reader.GetDoubleSafe("reflection_rate"),
                                IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate"),
                                ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate"),
                                ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate"),
                                Mana = reader.GetDoubleSafe("mana"),
                                ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate"),
                                NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate"),
                                SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate"),
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
                                PercentAllHealth = reader.GetDoubleSafe("percent_all_health"),
                                PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack"),
                                PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense"),
                                PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack"),
                                PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense"),
                                PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack"),
                                PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense"),
                                PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack"),
                                PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense"),
                                PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack"),
                                PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense"),
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
            finally
            {
                await connection.CloseAsync();
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

                string insertSQL = @"
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

                await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
                {
                    insertCommand.Parameters.AddWithValue("@user_id", user_id);
                    insertCommand.Parameters.AddWithValue("@name", masterBoard.Name);
                    insertCommand.Parameters.AddWithValue("@node_id", masterBoard.Id);
                    insertCommand.Parameters.AddWithValue("@rank_level", masterBoard.RankLevel);

                    insertCommand.Parameters.AddWithValue("@power", masterBoard.Power);
                    insertCommand.Parameters.AddWithValue("@health", masterBoard.Health);
                    insertCommand.Parameters.AddWithValue("@physical_attack", masterBoard.PhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@physical_defense", masterBoard.PhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@magical_attack", masterBoard.MagicalAttack);
                    insertCommand.Parameters.AddWithValue("@magical_defense", masterBoard.MagicalDefense);
                    insertCommand.Parameters.AddWithValue("@chemical_attack", masterBoard.ChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@chemical_defense", masterBoard.ChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@atomic_attack", masterBoard.AtomicAttack);
                    insertCommand.Parameters.AddWithValue("@atomic_defense", masterBoard.AtomicDefense);
                    insertCommand.Parameters.AddWithValue("@mental_attack", masterBoard.MentalAttack);
                    insertCommand.Parameters.AddWithValue("@mental_defense", masterBoard.MentalDefense);
                    insertCommand.Parameters.AddWithValue("@speed", masterBoard.Speed);
                    insertCommand.Parameters.AddWithValue("@critical_damage_rate", masterBoard.CriticalDamageRate);
                    insertCommand.Parameters.AddWithValue("@critical_rate", masterBoard.CriticalRate);
                    insertCommand.Parameters.AddWithValue("@critical_resistance_rate", masterBoard.CriticalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@ignore_critical_rate", masterBoard.IgnoreCriticalRate);
                    insertCommand.Parameters.AddWithValue("@penetration_rate", masterBoard.PenetrationRate);
                    insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", masterBoard.PenetrationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@evasion_rate", masterBoard.EvasionRate);
                    insertCommand.Parameters.AddWithValue("@damage_absorption_rate", masterBoard.DamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", masterBoard.IgnoreDamageAbsorptionRate);
                    insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", masterBoard.AbsorbedDamageRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", masterBoard.VitalityRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", masterBoard.VitalityRegenerationResistanceRate);
                    insertCommand.Parameters.AddWithValue("@accuracy_rate", masterBoard.AccuracyRate);
                    insertCommand.Parameters.AddWithValue("@lifesteal_rate", masterBoard.LifestealRate);
                    insertCommand.Parameters.AddWithValue("@shield_strength", masterBoard.ShieldStrength);
                    insertCommand.Parameters.AddWithValue("@tenacity", masterBoard.Tenacity);
                    insertCommand.Parameters.AddWithValue("@resistance_rate", masterBoard.ResistanceRate);
                    insertCommand.Parameters.AddWithValue("@combo_rate", masterBoard.ComboRate);
                    insertCommand.Parameters.AddWithValue("@ignore_combo_rate", masterBoard.IgnoreComboRate);
                    insertCommand.Parameters.AddWithValue("@combo_damage_rate", masterBoard.ComboDamageRate);
                    insertCommand.Parameters.AddWithValue("@combo_resistance_rate", masterBoard.ComboResistanceRate);
                    insertCommand.Parameters.AddWithValue("@stun_rate", masterBoard.StunRate);
                    insertCommand.Parameters.AddWithValue("@ignore_stun_rate", masterBoard.IgnoreStunRate);
                    insertCommand.Parameters.AddWithValue("@reflection_rate", masterBoard.ReflectionRate);
                    insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", masterBoard.IgnoreReflectionRate);
                    insertCommand.Parameters.AddWithValue("@reflection_damage_rate", masterBoard.ReflectionDamageRate);
                    insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", masterBoard.ReflectionResistanceRate);
                    insertCommand.Parameters.AddWithValue("@mana", masterBoard.Mana);
                    insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", masterBoard.ManaRegenerationRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", masterBoard.DamageToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", masterBoard.ResistanceToDifferentFactionRate);
                    insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", masterBoard.DamageToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", masterBoard.ResistanceToSameFactionRate);
                    insertCommand.Parameters.AddWithValue("@normal_damage_rate", masterBoard.NormalDamageRate);
                    insertCommand.Parameters.AddWithValue("@normal_resistance_rate", masterBoard.NormalResistanceRate);
                    insertCommand.Parameters.AddWithValue("@skill_damage_rate", masterBoard.SkillDamageRate);
                    insertCommand.Parameters.AddWithValue("@skill_resistance_rate", masterBoard.SkillResistanceRate);

                    insertCommand.Parameters.AddWithValue("@percent_all_health", masterBoard.PercentAllHealth);
                    insertCommand.Parameters.AddWithValue("@percent_all_physical_attack", masterBoard.PercentAllPhysicalAttack);
                    insertCommand.Parameters.AddWithValue("@percent_all_physical_defense", masterBoard.PercentAllPhysicalDefense);
                    insertCommand.Parameters.AddWithValue("@percent_all_magical_attack", masterBoard.PercentAllMagicalAttack);
                    insertCommand.Parameters.AddWithValue("@percent_all_magical_defense", masterBoard.PercentAllMagicalDefense);
                    insertCommand.Parameters.AddWithValue("@percent_all_chemical_attack", masterBoard.PercentAllChemicalAttack);
                    insertCommand.Parameters.AddWithValue("@percent_all_chemical_defense", masterBoard.PercentAllChemicalDefense);
                    insertCommand.Parameters.AddWithValue("@percent_all_atomic_attack", masterBoard.PercentAllAtomicAttack);
                    insertCommand.Parameters.AddWithValue("@percent_all_atomic_defense", masterBoard.PercentAllAtomicDefense);
                    insertCommand.Parameters.AddWithValue("@percent_all_mental_attack", masterBoard.PercentAllMentalAttack);
                    insertCommand.Parameters.AddWithValue("@percent_all_mental_defense", masterBoard.PercentAllMentalDefense);

                    await insertCommand.ExecuteNonQueryAsync();
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
    public async Task UpdateUserMasterBoardAsync(string user_id, MasterBoard masterBoard)
    {
        int multiplier = QualityEvaluatorHelper.CheckQuality(masterBoard.RankLevel);
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
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

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", user_id);
                    updateCommand.Parameters.AddWithValue("@name", masterBoard.Name);
                    updateCommand.Parameters.AddWithValue("@node_id", masterBoard.Id);
                    updateCommand.Parameters.AddWithValue("@rank_level", masterBoard.RankLevel);

                    // Các giá trị số nhân với multiplier
                    updateCommand.Parameters.AddWithValue("@power", masterBoard.Power);
                    updateCommand.Parameters.AddWithValue("@health", masterBoard.Health * multiplier);
                    updateCommand.Parameters.AddWithValue("@physical_attack", masterBoard.PhysicalAttack * multiplier);
                    updateCommand.Parameters.AddWithValue("@physical_defense", masterBoard.PhysicalDefense * multiplier);
                    updateCommand.Parameters.AddWithValue("@magical_attack", masterBoard.MagicalAttack * multiplier);
                    updateCommand.Parameters.AddWithValue("@magical_defense", masterBoard.MagicalDefense * multiplier);
                    updateCommand.Parameters.AddWithValue("@chemical_attack", masterBoard.ChemicalAttack * multiplier);
                    updateCommand.Parameters.AddWithValue("@chemical_defense", masterBoard.ChemicalDefense * multiplier);
                    updateCommand.Parameters.AddWithValue("@atomic_attack", masterBoard.AtomicAttack * multiplier);
                    updateCommand.Parameters.AddWithValue("@atomic_defense", masterBoard.AtomicDefense * multiplier);
                    updateCommand.Parameters.AddWithValue("@mental_attack", masterBoard.MentalAttack * multiplier);
                    updateCommand.Parameters.AddWithValue("@mental_defense", masterBoard.MentalDefense * multiplier);
                    updateCommand.Parameters.AddWithValue("@speed", masterBoard.Speed * multiplier);
                    updateCommand.Parameters.AddWithValue("@critical_damage_rate", masterBoard.CriticalDamageRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@critical_rate", masterBoard.CriticalRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@critical_resistance_rate", masterBoard.CriticalResistanceRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@ignore_critical_rate", masterBoard.IgnoreCriticalRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@penetration_rate", masterBoard.PenetrationRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", masterBoard.PenetrationResistanceRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@evasion_rate", masterBoard.EvasionRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@damage_absorption_rate", masterBoard.DamageAbsorptionRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", masterBoard.IgnoreDamageAbsorptionRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", masterBoard.AbsorbedDamageRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", masterBoard.VitalityRegenerationRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", masterBoard.VitalityRegenerationResistanceRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@accuracy_rate", masterBoard.AccuracyRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@lifesteal_rate", masterBoard.LifestealRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@shield_strength", masterBoard.ShieldStrength * multiplier);
                    updateCommand.Parameters.AddWithValue("@tenacity", masterBoard.Tenacity * multiplier);
                    updateCommand.Parameters.AddWithValue("@resistance_rate", masterBoard.ResistanceRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@combo_rate", masterBoard.ComboRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@ignore_combo_rate", masterBoard.IgnoreComboRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@combo_damage_rate", masterBoard.ComboDamageRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@combo_resistance_rate", masterBoard.ComboResistanceRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@stun_rate", masterBoard.StunRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@ignore_stun_rate", masterBoard.IgnoreStunRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@reflection_rate", masterBoard.ReflectionRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", masterBoard.IgnoreReflectionRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@reflection_damage_rate", masterBoard.ReflectionDamageRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", masterBoard.ReflectionResistanceRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@mana", masterBoard.Mana * multiplier);
                    updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", masterBoard.ManaRegenerationRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", masterBoard.DamageToDifferentFactionRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", masterBoard.ResistanceToDifferentFactionRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", masterBoard.DamageToSameFactionRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", masterBoard.ResistanceToSameFactionRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@normal_damage_rate", masterBoard.NormalDamageRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@normal_resistance_rate", masterBoard.NormalResistanceRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@skill_damage_rate", masterBoard.SkillDamageRate * multiplier);
                    updateCommand.Parameters.AddWithValue("@skill_resistance_rate", masterBoard.SkillResistanceRate * multiplier);

                    updateCommand.Parameters.AddWithValue("@percent_all_health", masterBoard.PercentAllHealth * multiplier);
                    updateCommand.Parameters.AddWithValue("@percent_all_physical_attack", masterBoard.PercentAllPhysicalAttack * multiplier);
                    updateCommand.Parameters.AddWithValue("@percent_all_physical_defense", masterBoard.PercentAllPhysicalDefense * multiplier);
                    updateCommand.Parameters.AddWithValue("@percent_all_magical_attack", masterBoard.PercentAllMagicalAttack * multiplier);
                    updateCommand.Parameters.AddWithValue("@percent_all_magical_defense", masterBoard.PercentAllMagicalDefense * multiplier);
                    updateCommand.Parameters.AddWithValue("@percent_all_chemical_attack", masterBoard.PercentAllChemicalAttack * multiplier);
                    updateCommand.Parameters.AddWithValue("@percent_all_chemical_defense", masterBoard.PercentAllChemicalDefense * multiplier);
                    updateCommand.Parameters.AddWithValue("@percent_all_atomic_attack", masterBoard.PercentAllAtomicAttack * multiplier);
                    updateCommand.Parameters.AddWithValue("@percent_all_atomic_defense", masterBoard.PercentAllAtomicDefense * multiplier);
                    updateCommand.Parameters.AddWithValue("@percent_all_mental_attack", masterBoard.PercentAllMentalAttack * multiplier);
                    updateCommand.Parameters.AddWithValue("@percent_all_mental_defense", masterBoard.PercentAllMentalDefense * multiplier);

                    await updateCommand.ExecuteNonQueryAsync();
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
}