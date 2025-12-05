using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserMagicFormationCirlcesRepository : IUserMagicFormationCirclesRepository
{
    public async Task<List<MagicFormationCircles>> GetUserMagicFormationCirclesAsync(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<MagicFormationCircles> magicFormationCircles = new List<MagicFormationCircles>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT um.*, m.id, m.name, m.image, m.rare, m.description 
                FROM magic_formation_circles m
                JOIN user_magic_formation_circles um ON m.id = um.mfc_id
                WHERE um.user_id = @userId 
                  AND m.type = @type 
                  AND (@rare = 'All' OR m.rare = @rare)
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name
                LIMIT @limit OFFSET @offset;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@rare", rare);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            MagicFormationCircles MagicFormationCircle = new MagicFormationCircles
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

                            magicFormationCircles.Add(MagicFormationCircle);
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

        return magicFormationCircles;
    }
    public async Task<int> GetUserMagicFormationCirclesCountAsync(string user_id, string type, string rare)
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
                FROM magic_formation_circles m
                JOIN user_magic_formation_circles um ON m.id = um.mfc_id
                WHERE um.user_id = @userId 
                  AND m.type = @type 
                  AND (@rare = 'All' OR m.rare = @rare);
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@type", type);
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
    public async Task<bool> InsertUserMagicFormationCircleAsync(MagicFormationCircles MagicFormationCircle, string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM user_magic_formation_circles 
                WHERE user_id = @user_id AND mfc_id = @mfc_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@mfc_id", MagicFormationCircle.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"
                        INSERT INTO user_magic_formation_circles (
                            user_id, mfc_id, rare, level, experiment, star, quality, block, quantity,
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
                            @user_id, @mfc_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                        );
                    ";

                        await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@user_id", userId);
                            insertCommand.Parameters.AddWithValue("@mfc_id", MagicFormationCircle.Id);
                            insertCommand.Parameters.AddWithValue("@rare", MagicFormationCircle.Rare);
                            insertCommand.Parameters.AddWithValue("@level", 0);
                            insertCommand.Parameters.AddWithValue("@experiment", 0);
                            insertCommand.Parameters.AddWithValue("@star", 0);
                            insertCommand.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(MagicFormationCircle.Rare));
                            insertCommand.Parameters.AddWithValue("@block", false);
                            insertCommand.Parameters.AddWithValue("@quantity", MagicFormationCircle.Quantity);
                            insertCommand.Parameters.AddWithValue("@power", MagicFormationCircle.Power);
                            insertCommand.Parameters.AddWithValue("@health", MagicFormationCircle.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", MagicFormationCircle.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", MagicFormationCircle.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", MagicFormationCircle.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", MagicFormationCircle.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", MagicFormationCircle.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", MagicFormationCircle.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", MagicFormationCircle.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", MagicFormationCircle.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", MagicFormationCircle.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", MagicFormationCircle.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", MagicFormationCircle.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", MagicFormationCircle.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", MagicFormationCircle.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", MagicFormationCircle.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", MagicFormationCircle.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", MagicFormationCircle.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", MagicFormationCircle.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", MagicFormationCircle.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", MagicFormationCircle.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", MagicFormationCircle.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", MagicFormationCircle.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", MagicFormationCircle.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", MagicFormationCircle.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", MagicFormationCircle.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", MagicFormationCircle.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", MagicFormationCircle.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", MagicFormationCircle.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", MagicFormationCircle.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", MagicFormationCircle.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", MagicFormationCircle.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", MagicFormationCircle.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", MagicFormationCircle.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", MagicFormationCircle.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", MagicFormationCircle.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", MagicFormationCircle.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", MagicFormationCircle.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", MagicFormationCircle.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", MagicFormationCircle.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", MagicFormationCircle.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", MagicFormationCircle.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", MagicFormationCircle.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", MagicFormationCircle.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", MagicFormationCircle.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", MagicFormationCircle.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", MagicFormationCircle.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", MagicFormationCircle.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", MagicFormationCircle.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", MagicFormationCircle.SkillResistanceRate);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_magic_formation_circles
                        SET quantity = @quantity
                        WHERE user_id = @user_id AND mfc_id = @mfc_id;
                    ";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", userId);
                            updateCommand.Parameters.AddWithValue("@mfc_id", MagicFormationCircle.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", MagicFormationCircle.Quantity);

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
    public async Task<bool> UpdateMagicFormationCircleLevelAsync(MagicFormationCircles MagicFormationCircle, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_magic_formation_circles
                SET 
                    level = @level, power = @power, health = @health, 
                    physical_attack = @physical_attack, physical_defense = @physical_defense, 
                    magical_attack = @magical_attack, magical_defense = @magical_defense, 
                    chemical_attack = @chemical_attack, chemical_defense = @chemical_defense, 
                    atomic_attack = @atomic_attack, atomic_defense = @atomic_defense, 
                    mental_attack = @mental_attack, mental_defense = @mental_defense, 
                    speed = @speed, critical_damage_rate = @critical_damage_rate, 
                    critical_rate = @critical_rate, critical_resistance_rate = @critical_resistance_rate, 
                    ignore_critical_rate = @ignore_critical_rate,
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
                    skill_damage_rate = @skill_damage_rate, skill_resistance_rate = @skill_resistance_rate
                WHERE user_id = @user_id AND mfc_id = @mfc_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@mfc_id", MagicFormationCircle.Id);
                    command.Parameters.AddWithValue("@level", cardLevel);
                    command.Parameters.AddWithValue("@power", MagicFormationCircle.Power);
                    command.Parameters.AddWithValue("@health", MagicFormationCircle.Health);
                    command.Parameters.AddWithValue("@physical_attack", MagicFormationCircle.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", MagicFormationCircle.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", MagicFormationCircle.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", MagicFormationCircle.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", MagicFormationCircle.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", MagicFormationCircle.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", MagicFormationCircle.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", MagicFormationCircle.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", MagicFormationCircle.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", MagicFormationCircle.MentalDefense);
                    command.Parameters.AddWithValue("@speed", MagicFormationCircle.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", MagicFormationCircle.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", MagicFormationCircle.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", MagicFormationCircle.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", MagicFormationCircle.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", MagicFormationCircle.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", MagicFormationCircle.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", MagicFormationCircle.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", MagicFormationCircle.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", MagicFormationCircle.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", MagicFormationCircle.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", MagicFormationCircle.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", MagicFormationCircle.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", MagicFormationCircle.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", MagicFormationCircle.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", MagicFormationCircle.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", MagicFormationCircle.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", MagicFormationCircle.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", MagicFormationCircle.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", MagicFormationCircle.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", MagicFormationCircle.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", MagicFormationCircle.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", MagicFormationCircle.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", MagicFormationCircle.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", MagicFormationCircle.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", MagicFormationCircle.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", MagicFormationCircle.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", MagicFormationCircle.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", MagicFormationCircle.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", MagicFormationCircle.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", MagicFormationCircle.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", MagicFormationCircle.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", MagicFormationCircle.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", MagicFormationCircle.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", MagicFormationCircle.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", MagicFormationCircle.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", MagicFormationCircle.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", MagicFormationCircle.SkillResistanceRate);

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
    public async Task<bool> UpdateMagicFormationCircleBreakthroughAsync(MagicFormationCircles MagicFormationCircle, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_magic_formation_circles
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
                    skill_damage_rate = @skill_damage_rate, skill_resistance_rate = @skill_resistance_rate
                WHERE user_id = @user_id AND mfc_id = @mfc_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@mfc_id", MagicFormationCircle.Id);
                    command.Parameters.AddWithValue("@star", star);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@power", MagicFormationCircle.Power);
                    command.Parameters.AddWithValue("@health", MagicFormationCircle.Health);
                    command.Parameters.AddWithValue("@physical_attack", MagicFormationCircle.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", MagicFormationCircle.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", MagicFormationCircle.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", MagicFormationCircle.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", MagicFormationCircle.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", MagicFormationCircle.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", MagicFormationCircle.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", MagicFormationCircle.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", MagicFormationCircle.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", MagicFormationCircle.MentalDefense);
                    command.Parameters.AddWithValue("@speed", MagicFormationCircle.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", MagicFormationCircle.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", MagicFormationCircle.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", MagicFormationCircle.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", MagicFormationCircle.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", MagicFormationCircle.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", MagicFormationCircle.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", MagicFormationCircle.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", MagicFormationCircle.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", MagicFormationCircle.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", MagicFormationCircle.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", MagicFormationCircle.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", MagicFormationCircle.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", MagicFormationCircle.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", MagicFormationCircle.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", MagicFormationCircle.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", MagicFormationCircle.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", MagicFormationCircle.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", MagicFormationCircle.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", MagicFormationCircle.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", MagicFormationCircle.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", MagicFormationCircle.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", MagicFormationCircle.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", MagicFormationCircle.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", MagicFormationCircle.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", MagicFormationCircle.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", MagicFormationCircle.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", MagicFormationCircle.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", MagicFormationCircle.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", MagicFormationCircle.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", MagicFormationCircle.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", MagicFormationCircle.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", MagicFormationCircle.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", MagicFormationCircle.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", MagicFormationCircle.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", MagicFormationCircle.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", MagicFormationCircle.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", MagicFormationCircle.SkillResistanceRate);

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
    public async Task<MagicFormationCircles> GetUserMagicFormationCircleByIdAsync(string user_id, string Id)
    {
        MagicFormationCircles magicFormationCircle = null;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT * FROM user_magic_formation_circles
                             WHERE mfc_id=@id AND user_id=@user_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@user_id", user_id);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            magicFormationCircle = new MagicFormationCircles
                            {
                                Id = reader.GetString("mfc_id"),
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
                                SkillResistanceRate = reader.GetDouble("skill_resistance_rate")
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

        return magicFormationCircle;
    }
    public async Task<MagicFormationCircles> SumPowerUserMagicFormationCirclesAsync()
    {
        MagicFormationCircles sumMagicFormationCircles = new MagicFormationCircles();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT 
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
                FROM user_magic_formation_circles
                WHERE user_id = @user_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumMagicFormationCircles.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                            sumMagicFormationCircles.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                            sumMagicFormationCircles.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDouble("total_mana");
                            sumMagicFormationCircles.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                            sumMagicFormationCircles.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                            sumMagicFormationCircles.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                            sumMagicFormationCircles.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                            sumMagicFormationCircles.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                            sumMagicFormationCircles.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                            sumMagicFormationCircles.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                            sumMagicFormationCircles.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                            sumMagicFormationCircles.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                            sumMagicFormationCircles.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                            sumMagicFormationCircles.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                            sumMagicFormationCircles.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                            sumMagicFormationCircles.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                            sumMagicFormationCircles.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                            sumMagicFormationCircles.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                            sumMagicFormationCircles.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                            sumMagicFormationCircles.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                            sumMagicFormationCircles.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                            sumMagicFormationCircles.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                            sumMagicFormationCircles.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                            sumMagicFormationCircles.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                            sumMagicFormationCircles.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                            sumMagicFormationCircles.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                            sumMagicFormationCircles.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                            sumMagicFormationCircles.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                            sumMagicFormationCircles.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                            sumMagicFormationCircles.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                            sumMagicFormationCircles.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                            sumMagicFormationCircles.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                            sumMagicFormationCircles.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                            sumMagicFormationCircles.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                            sumMagicFormationCircles.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                            sumMagicFormationCircles.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                            sumMagicFormationCircles.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                            sumMagicFormationCircles.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                            sumMagicFormationCircles.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                            sumMagicFormationCircles.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                            sumMagicFormationCircles.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                            sumMagicFormationCircles.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                            sumMagicFormationCircles.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                            sumMagicFormationCircles.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                            sumMagicFormationCircles.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                            sumMagicFormationCircles.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                            sumMagicFormationCircles.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                            sumMagicFormationCircles.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                            sumMagicFormationCircles.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                            sumMagicFormationCircles.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
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

        return sumMagicFormationCircles;
    }
}