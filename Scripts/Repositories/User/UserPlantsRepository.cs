using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserPlantsRepository : IUserPlantsRepository
{
    public async Task<List<Plants>> GetUserPlantsAsync(string user_id, int pageSize, int offset, string rare)
    {
        List<Plants> Plants = new List<Plants>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT ut.*, t.id, t.name, t.image, t.rare, t.description
                FROM Plants t
                INNER JOIN user_Plants ut ON t.id = ut.Plant_id
                WHERE ut.user_id = @userId AND (@rare = 'All' OR t.rare = @rare)
                ORDER BY t.name REGEXP '[0-9]+$',
                         CAST(REGEXP_SUBSTR(t.name, '[0-9]+$') AS UNSIGNED),
                         t.name
                LIMIT @limit OFFSET @offset;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@rare", rare);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Plants Plant = new Plants
                            {
                                Id = reader.GetString("id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
                                Quality = reader.GetDouble("quality"),
                                Star = reader.GetInt32("star"),
                                Level = reader.GetInt32("level"),
                                Experiment = reader.GetDouble("experiment"),
                                Quantity = reader.GetDouble("quantity"),
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
                                Mana = reader.GetDouble("mana"),
                                ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                                NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                                SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                                SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                                Description = reader.GetString("description")
                            };

                            Plants.Add(Plant);
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

        return Plants;
    }
    public async Task<int> GetUserPlantsCountAsync(string user_id, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT COUNT(*) 
                FROM Plants t
                INNER JOIN user_Plants ut ON t.id = ut.Plant_id
                WHERE ut.user_id = @userId AND (@rare = 'All' OR t.rare = @rare);
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@rare", rare);

                    object result = await command.ExecuteScalarAsync();
                    count = Convert.ToInt32(result);
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

        return count;
    }
    public async Task<bool> InsertUserPlantAsync(Plants Plants, string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) FROM user_Plants 
                WHERE user_id = @user_id AND Plant_id = @Plant_id;";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@Plant_id", Plants.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"
                        INSERT INTO user_Plants (
                            user_id, Plant_id, rare, level, experiment, star, quality, block, quantity,
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
                            skill_damage_rate, skill_resistance_rate
                        ) VALUES (
                            @user_id, @Plant_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                            @skill_damage_rate, @skill_resistance_rate
                        );";

                        await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@user_id", userId);
                            insertCommand.Parameters.AddWithValue("@Plant_id", Plants.Id);
                            insertCommand.Parameters.AddWithValue("@rare", Plants.Rare);
                            insertCommand.Parameters.AddWithValue("@level", 0);
                            insertCommand.Parameters.AddWithValue("@experiment", 0);
                            insertCommand.Parameters.AddWithValue("@star", 0);
                            insertCommand.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(Plants.Rare));
                            insertCommand.Parameters.AddWithValue("@block", false);
                            insertCommand.Parameters.AddWithValue("@quantity", Plants.Quantity);
                            insertCommand.Parameters.AddWithValue("@power", Plants.Power);
                            insertCommand.Parameters.AddWithValue("@health", Plants.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", Plants.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", Plants.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", Plants.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", Plants.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", Plants.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", Plants.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", Plants.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", Plants.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", Plants.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", Plants.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", Plants.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", Plants.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", Plants.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", Plants.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", Plants.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", Plants.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", Plants.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", Plants.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", Plants.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", Plants.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", Plants.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", Plants.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", Plants.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", Plants.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", Plants.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", Plants.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", Plants.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", Plants.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", Plants.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", Plants.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", Plants.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", Plants.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", Plants.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", Plants.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", Plants.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", Plants.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", Plants.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", Plants.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", Plants.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", Plants.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", Plants.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", Plants.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", Plants.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", Plants.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", Plants.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", Plants.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", Plants.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", Plants.SkillResistanceRate);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_Plants
                        SET quantity = @quantity
                        WHERE user_id = @user_id AND Plant_id = @Plant_id;";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", userId);
                            updateCommand.Parameters.AddWithValue("@Plant_id", Plants.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", Plants.Quantity);

                            await updateCommand.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<bool> UpdatePlantLevelAsync(Plants Plants, int PlantLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string query = @"
                UPDATE user_Plants
                SET 
                    level = @level, power = @power, health = @health, 
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
                    skill_damage_rate = @skill_damage_rate, skill_resistance_rate = @skill_resistance_rate
                WHERE user_id = @user_id AND Plant_id = @Plant_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@Plant_id", Plants.Id);
                    command.Parameters.AddWithValue("@level", PlantLevel);
                    command.Parameters.AddWithValue("@power", Plants.Power);
                    command.Parameters.AddWithValue("@health", Plants.Health);
                    command.Parameters.AddWithValue("@physical_attack", Plants.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", Plants.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", Plants.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", Plants.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", Plants.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", Plants.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", Plants.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", Plants.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", Plants.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", Plants.MentalDefense);
                    command.Parameters.AddWithValue("@speed", Plants.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", Plants.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", Plants.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", Plants.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", Plants.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", Plants.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", Plants.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", Plants.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", Plants.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", Plants.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", Plants.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", Plants.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", Plants.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", Plants.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", Plants.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", Plants.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", Plants.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", Plants.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", Plants.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", Plants.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", Plants.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", Plants.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", Plants.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", Plants.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", Plants.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", Plants.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", Plants.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", Plants.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", Plants.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", Plants.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", Plants.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", Plants.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", Plants.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", Plants.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", Plants.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", Plants.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", Plants.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", Plants.SkillResistanceRate);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return true;
    }
    public async Task<bool> UpdatePlantBreakthroughAsync(Plants Plants, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string query = @"
                UPDATE user_Plants
                SET 
                    star = @star, quantity = @quantity, power=@power, health = @health, 
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
                    skill_damage_rate = @skill_damage_rate, skill_resistance_rate = @skill_resistance_rate
                WHERE user_id = @user_id AND Plant_id = @Plant_id;";
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@Plant_id", Plants.Id);
                    command.Parameters.AddWithValue("@star", star);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@power", Plants.Power);
                    command.Parameters.AddWithValue("@health", Plants.Health);
                    command.Parameters.AddWithValue("@physical_attack", Plants.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", Plants.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", Plants.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", Plants.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", Plants.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", Plants.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", Plants.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", Plants.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", Plants.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", Plants.MentalDefense);
                    command.Parameters.AddWithValue("@speed", Plants.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", Plants.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", Plants.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", Plants.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", Plants.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", Plants.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", Plants.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", Plants.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", Plants.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", Plants.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", Plants.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", Plants.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", Plants.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", Plants.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", Plants.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", Plants.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", Plants.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", Plants.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", Plants.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", Plants.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", Plants.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", Plants.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", Plants.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", Plants.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", Plants.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", Plants.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", Plants.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", Plants.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", Plants.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", Plants.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", Plants.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", Plants.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", Plants.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", Plants.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", Plants.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", Plants.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", Plants.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", Plants.SkillResistanceRate);

                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
        return true;
    }
    public async Task<Plants> GetUserPlantByIdAsync(string user_id, string Id)
    {
        Plants Plant = new Plants();
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string query = @"Select * from user_Plants where user_Plants.Plant_id=@id 
                and user_Plants.user_id=@user_id";
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@user_id", user_id);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Plant = new Plants
                            {
                                Id = reader.GetString("Plant_id"),
                                Level = reader.GetInt32("level"),
                                Quality = reader.GetDouble("quality"),
                                Experiment = reader.GetDouble("experiment"),
                                Star = reader.GetInt32("star"),
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
                                Mana = reader.GetDouble("mana"),
                                ManaRegenerationRate = reader.GetDouble("mana_regeneration_rate"),
                                DamageToDifferentFactionRate = reader.GetDouble("damage_to_different_faction_rate"),
                                ResistanceToDifferentFactionRate = reader.GetDouble("resistance_to_different_faction_rate"),
                                DamageToSameFactionRate = reader.GetDouble("damage_to_same_faction_rate"),
                                ResistanceToSameFactionRate = reader.GetDouble("resistance_to_same_faction_rate"),
                                NormalDamageRate = reader.GetDouble("normal_damage_rate"),
                                NormalResistanceRate = reader.GetDouble("normal_resistance_rate"),
                                SkillDamageRate = reader.GetDouble("skill_damage_rate"),
                                SkillResistanceRate = reader.GetDouble("skill_resistance_rate"),
                            };
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
        return Plant;
    }
    public async Task<Plants> SumPowerUserPlantsAsync()
    {
        Plants sumPlants = new Plants();
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string query = @"SELECT 
                SUM(power * (1 + quality / 10.0)) AS total_power,
                SUM(health * (1 + quality / 10.0)) AS total_health,
                SUM(mana * (1 + quality / 10.0)) AS total_mana,
                SUM(physical_attack * (1 + quality / 10.0)) AS total_physical_attack,
                SUM(physical_defense * (1 + quality / 10.0)) AS total_physical_defense,
                SUM(magical_attack * (1 + quality / 10.0)) AS total_magical_attack,
                SUM(magical_defense * (1 + quality / 10.0)) AS total_magical_defense,
                SUM(chemical_attack * (1 + quality / 10.0)) AS total_chemical_attack,
                SUM(chemical_defense * (1 + quality / 10.0)) AS total_chemical_defense,
                SUM(atomic_attack * (1 + quality / 10.0)) AS total_atomic_attack,
                SUM(atomic_defense * (1 + quality / 10.0)) AS total_atomic_defense,
                SUM(mental_attack * (1 + quality / 10.0)) AS total_mental_attack,
                SUM(mental_defense * (1 + quality / 10.0)) AS total_mental_defense,
                SUM(speed * (1 + quality / 10.0)) AS total_speed,
                SUM(critical_damage_rate * (1 + quality / 10.0)) AS total_critical_damage_rate,
                SUM(critical_rate * (1 + quality / 10.0)) AS total_critical_rate,
                SUM(critical_resistance_rate * (1 + quality / 10.0)) AS total_critical_resistance_rate,
                SUM(ignore_critical_rate * (1 + quality / 10.0)) AS total_ignore_critical_rate,
                SUM(penetration_rate * (1 + quality / 10.0)) AS total_penetration_rate,
                SUM(penetration_resistance_rate * (1 + quality / 10.0)) AS total_penetration_resistance_rate,
                SUM(evasion_rate * (1 + quality / 10.0)) AS total_evasion_rate,
                SUM(damage_absorption_rate * (1 + quality / 10.0)) AS total_damage_absorption_rate,
                SUM(ignore_damage_absorption_rate * (1 + quality / 10.0)) AS total_ignore_damage_absorption_rate,
                SUM(absorbed_damage_rate * (1 + quality / 10.0)) AS total_absorbed_damage_rate,
                SUM(vitality_regeneration_rate * (1 + quality / 10.0)) AS total_vitality_regeneration_rate,
                SUM(vitality_regeneration_resistance_rate * (1 + quality / 10.0)) AS total_vitality_regeneration_resistance_rate,
                SUM(accuracy_rate * (1 + quality / 10.0)) AS total_accuracy_rate,
                SUM(lifesteal_rate * (1 + quality / 10.0)) AS total_lifesteal_rate,
                SUM(shield_strength * (1 + quality / 10.0)) AS total_shield_strength,
                SUM(tenacity * (1 + quality / 10.0)) AS total_tenacity,
                SUM(resistance_rate * (1 + quality / 10.0)) AS total_resistance_rate,
                SUM(combo_rate * (1 + quality / 10.0)) AS total_combo_rate,
                SUM(ignore_combo_rate * (1 + quality / 10.0)) AS total_ignore_combo_rate,
                SUM(combo_damage_rate * (1 + quality / 10.0)) AS total_combo_damage_rate,
                SUM(combo_resistance_rate * (1 + quality / 10.0)) AS total_combo_resistance_rate,
                SUM(stun_rate * (1 + quality / 10.0)) AS total_stun_rate,
                SUM(ignore_stun_rate * (1 + quality / 10.0)) AS total_ignore_stun_rate,
                SUM(reflection_rate * (1 + quality / 10.0)) AS total_reflection_rate,
                SUM(ignore_reflection_rate * (1 + quality / 10.0)) AS total_ignore_reflection_rate,
                SUM(reflection_damage_rate * (1 + quality / 10.0)) AS total_reflection_damage_rate,
                SUM(reflection_resistance_rate * (1 + quality / 10.0)) AS total_reflection_resistance_rate,
                SUM(mana_regeneration_rate * (1 + quality / 10.0)) AS total_mana_regeneration_rate,
                SUM(damage_to_different_faction_rate * (1 + quality / 10.0)) AS total_damage_to_different_faction_rate,
                SUM(resistance_to_different_faction_rate * (1 + quality / 10.0)) AS total_resistance_to_different_faction_rate,
                SUM(damage_to_same_faction_rate * (1 + quality / 10.0)) AS total_damage_to_same_faction_rate,
                SUM(resistance_to_same_faction_rate * (1 + quality / 10.0)) AS total_resistance_to_same_faction_rate,
                SUM(normal_damage_rate * (1 + quality / 10.0)) AS total_normal_damage_rate,
                SUM(normal_resistance_rate * (1 + quality / 10.0)) AS total_normal_resistance_rate,
                SUM(skill_damage_rate * (1 + quality / 10.0)) AS total_skill_damage_rate,
                SUM(skill_resistance_rate * (1 + quality / 10.0)) AS total_skill_resistance_rate
            FROM user_Plants
            WHERE user_id = @user_id;
            ";
                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumPlants.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                            sumPlants.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                            sumPlants.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                            sumPlants.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                            sumPlants.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                            sumPlants.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                            sumPlants.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                            sumPlants.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                            sumPlants.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                            sumPlants.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                            sumPlants.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                            sumPlants.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                            sumPlants.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                            sumPlants.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                            sumPlants.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                            sumPlants.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                            sumPlants.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                            sumPlants.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                            sumPlants.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                            sumPlants.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                            sumPlants.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                            sumPlants.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                            sumPlants.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                            sumPlants.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                            sumPlants.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                            sumPlants.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                            sumPlants.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                            sumPlants.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                            sumPlants.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                            sumPlants.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                            sumPlants.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                            sumPlants.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                            sumPlants.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                            sumPlants.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                            sumPlants.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                            sumPlants.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                            sumPlants.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                            sumPlants.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                            sumPlants.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                            sumPlants.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                            sumPlants.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDouble("total_mana");
                            sumPlants.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                            sumPlants.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                            sumPlants.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                            sumPlants.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                            sumPlants.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                            sumPlants.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                            sumPlants.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                            sumPlants.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                            sumPlants.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
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
        return sumPlants;
    }
}