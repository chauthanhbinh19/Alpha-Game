using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserCollaborationEquipmentsRepository : IUserCollaborationEquipmentsRepository
{
    public async Task<List<CollaborationEquipments>> GetUserCollaborationEquipmentsAsync(string user_id, string type, int pageSize, int offset, string rare)
    {
        List<CollaborationEquipments> CollaborationEquipments = new List<CollaborationEquipments>();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT um.*, m.id, m.name, m.image, m.rare, m.description 
                FROM collaboration_equipments m
                JOIN user_collaboration_equipments um ON m.id = um.collaboration_equipment_id
                WHERE um.user_id = @userId 
                  AND m.type = @type 
                  AND (@rare = 'All' OR m.rare = @rare)
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name
                LIMIT @limit OFFSET @offset;
            ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@rare", rare);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            CollaborationEquipments CollaborationEquipment = new CollaborationEquipments
                            {
                                Id = reader.GetString("id"),
                                Name = reader.GetString("name"),
                                Image = reader.GetString("image"),
                                Rare = reader.GetString("rare"),
                                Quality = reader.GetInt32("quality"),
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
                                Description = reader.GetString("description")
                            };

                            CollaborationEquipments.Add(CollaborationEquipment);
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

        return CollaborationEquipments;
    }
    public async Task<int> GetUserCollaborationEquipmentsCountAsync(string user_id, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT COUNT(*) 
                FROM collaboration_equipments m
                JOIN user_collaboration_equipments um ON m.id = um.collaboration_equipment_id
                WHERE um.user_id = @userId 
                  AND m.type = @type 
                  AND (@rare = 'All' OR m.rare = @rare);
            ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
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
    public async Task<bool> InsertUserCollaborationEquipmentAsync(CollaborationEquipments CollaborationEquipment, string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM user_collaboration_equipments 
                WHERE user_id = @user_id AND collaboration_equipment_id = @collaboration_equipment_id;
            ";

                using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@collaboration_equipment_id", CollaborationEquipment.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"
                        INSERT INTO user_collaboration_equipments (
                            user_id, collaboration_equipment_id, rare, level, experiment, star, quality, block, quantity,
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
                            @user_id, @collaboration_equipment_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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

                        using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@user_id", userId);
                            insertCommand.Parameters.AddWithValue("@collaboration_equipment_id", CollaborationEquipment.Id);
                            insertCommand.Parameters.AddWithValue("@rare", CollaborationEquipment.Rare);
                            insertCommand.Parameters.AddWithValue("@level", 0);
                            insertCommand.Parameters.AddWithValue("@experiment", 0);
                            insertCommand.Parameters.AddWithValue("@star", 0);
                            insertCommand.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(CollaborationEquipment.Rare));
                            insertCommand.Parameters.AddWithValue("@block", false);
                            insertCommand.Parameters.AddWithValue("@quantity", CollaborationEquipment.Quantity);
                            insertCommand.Parameters.AddWithValue("@power", CollaborationEquipment.Power);
                            insertCommand.Parameters.AddWithValue("@health", CollaborationEquipment.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", CollaborationEquipment.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", CollaborationEquipment.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", CollaborationEquipment.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", CollaborationEquipment.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", CollaborationEquipment.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", CollaborationEquipment.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", CollaborationEquipment.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", CollaborationEquipment.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", CollaborationEquipment.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", CollaborationEquipment.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", CollaborationEquipment.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", CollaborationEquipment.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", CollaborationEquipment.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", CollaborationEquipment.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", CollaborationEquipment.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", CollaborationEquipment.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", CollaborationEquipment.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", CollaborationEquipment.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", CollaborationEquipment.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", CollaborationEquipment.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", CollaborationEquipment.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", CollaborationEquipment.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", CollaborationEquipment.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", CollaborationEquipment.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", CollaborationEquipment.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", CollaborationEquipment.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", CollaborationEquipment.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", CollaborationEquipment.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", CollaborationEquipment.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", CollaborationEquipment.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", CollaborationEquipment.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", CollaborationEquipment.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", CollaborationEquipment.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", CollaborationEquipment.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", CollaborationEquipment.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", CollaborationEquipment.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", CollaborationEquipment.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", CollaborationEquipment.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", CollaborationEquipment.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", CollaborationEquipment.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", CollaborationEquipment.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", CollaborationEquipment.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", CollaborationEquipment.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", CollaborationEquipment.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", CollaborationEquipment.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", CollaborationEquipment.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", CollaborationEquipment.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", CollaborationEquipment.SkillResistanceRate);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_collaboration_equipments
                        SET quantity = @quantity
                        WHERE user_id = @user_id AND collaboration_equipment_id = @collaboration_equipment_id;
                    ";

                        using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", userId);
                            updateCommand.Parameters.AddWithValue("@collaboration_equipment_id", CollaborationEquipment.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", CollaborationEquipment.Quantity);

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
    public async Task<bool> UpdateCollaborationEquipmentLevelAsync(CollaborationEquipments CollaborationEquipment, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_collaboration_equipments
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
                WHERE user_id = @user_id AND collaboration_equipment_id = @collaboration_equipment_id;
            ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@collaboration_equipment_id", CollaborationEquipment.Id);
                    command.Parameters.AddWithValue("@level", cardLevel);
                    command.Parameters.AddWithValue("@power", CollaborationEquipment.Power);
                    command.Parameters.AddWithValue("@health", CollaborationEquipment.Health);
                    command.Parameters.AddWithValue("@physical_attack", CollaborationEquipment.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", CollaborationEquipment.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", CollaborationEquipment.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", CollaborationEquipment.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", CollaborationEquipment.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", CollaborationEquipment.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", CollaborationEquipment.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", CollaborationEquipment.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", CollaborationEquipment.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", CollaborationEquipment.MentalDefense);
                    command.Parameters.AddWithValue("@speed", CollaborationEquipment.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", CollaborationEquipment.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", CollaborationEquipment.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", CollaborationEquipment.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", CollaborationEquipment.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", CollaborationEquipment.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", CollaborationEquipment.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", CollaborationEquipment.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", CollaborationEquipment.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", CollaborationEquipment.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", CollaborationEquipment.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", CollaborationEquipment.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", CollaborationEquipment.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", CollaborationEquipment.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", CollaborationEquipment.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", CollaborationEquipment.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", CollaborationEquipment.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", CollaborationEquipment.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", CollaborationEquipment.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", CollaborationEquipment.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", CollaborationEquipment.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", CollaborationEquipment.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", CollaborationEquipment.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", CollaborationEquipment.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", CollaborationEquipment.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", CollaborationEquipment.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", CollaborationEquipment.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", CollaborationEquipment.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", CollaborationEquipment.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", CollaborationEquipment.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", CollaborationEquipment.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CollaborationEquipment.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", CollaborationEquipment.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CollaborationEquipment.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", CollaborationEquipment.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", CollaborationEquipment.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", CollaborationEquipment.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", CollaborationEquipment.SkillResistanceRate);

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
    public async Task<bool> UpdateCollaborationEquipmentBreakthroughAsync(CollaborationEquipments CollaborationEquipment, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_collaboration_equipments
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
                WHERE user_id = @user_id AND collaboration_equipment_id = @collaboration_equipment_id;
            ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@collaboration_equipment_id", CollaborationEquipment.Id);
                    command.Parameters.AddWithValue("@star", star);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@power", CollaborationEquipment.Power);
                    command.Parameters.AddWithValue("@health", CollaborationEquipment.Health);
                    command.Parameters.AddWithValue("@physical_attack", CollaborationEquipment.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", CollaborationEquipment.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", CollaborationEquipment.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", CollaborationEquipment.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", CollaborationEquipment.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", CollaborationEquipment.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", CollaborationEquipment.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", CollaborationEquipment.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", CollaborationEquipment.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", CollaborationEquipment.MentalDefense);
                    command.Parameters.AddWithValue("@speed", CollaborationEquipment.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", CollaborationEquipment.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", CollaborationEquipment.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", CollaborationEquipment.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", CollaborationEquipment.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", CollaborationEquipment.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", CollaborationEquipment.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", CollaborationEquipment.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", CollaborationEquipment.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", CollaborationEquipment.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", CollaborationEquipment.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", CollaborationEquipment.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", CollaborationEquipment.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", CollaborationEquipment.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", CollaborationEquipment.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", CollaborationEquipment.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", CollaborationEquipment.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", CollaborationEquipment.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", CollaborationEquipment.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", CollaborationEquipment.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", CollaborationEquipment.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", CollaborationEquipment.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", CollaborationEquipment.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", CollaborationEquipment.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", CollaborationEquipment.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", CollaborationEquipment.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", CollaborationEquipment.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", CollaborationEquipment.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", CollaborationEquipment.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", CollaborationEquipment.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", CollaborationEquipment.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", CollaborationEquipment.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", CollaborationEquipment.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", CollaborationEquipment.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", CollaborationEquipment.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", CollaborationEquipment.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", CollaborationEquipment.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", CollaborationEquipment.SkillResistanceRate);

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
    public async Task<CollaborationEquipments> GetUserCollaborationEquipmentByIdAsync(string user_id, string Id)
    {
        CollaborationEquipments collaborationEquipment = null;
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT * FROM user_collaboration_equipments
                             WHERE collaboration_equipment_id=@id AND user_id=@user_id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@user_id", user_id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            collaborationEquipment = new CollaborationEquipments
                            {
                                Id = reader.GetString("collaboration_equipment_id"),
                                Level = reader.GetInt32("level"),
                                Quality = reader.GetInt32("quality"),
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
                                Mana = reader.GetFloat("mana"),
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

        return collaborationEquipment;
    }
    public async Task<CollaborationEquipments> SumPowerUserCollaborationEquipmentsAsync()
    {
        CollaborationEquipments sumCollaborationEquipments = new CollaborationEquipments();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
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
                FROM user_collaboration_equipments
                WHERE user_id = @user_id;";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumCollaborationEquipments.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDouble("total_power");
                            sumCollaborationEquipments.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDouble("total_health");
                            sumCollaborationEquipments.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            sumCollaborationEquipments.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDouble("total_physical_attack");
                            sumCollaborationEquipments.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDouble("total_physical_defense");
                            sumCollaborationEquipments.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDouble("total_magical_attack");
                            sumCollaborationEquipments.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDouble("total_magical_defense");
                            sumCollaborationEquipments.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDouble("total_chemical_attack");
                            sumCollaborationEquipments.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDouble("total_chemical_defense");
                            sumCollaborationEquipments.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDouble("total_atomic_attack");
                            sumCollaborationEquipments.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDouble("total_atomic_defense");
                            sumCollaborationEquipments.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDouble("total_mental_attack");
                            sumCollaborationEquipments.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDouble("total_mental_defense");
                            sumCollaborationEquipments.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDouble("total_speed");
                            sumCollaborationEquipments.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDouble("total_critical_damage_rate");
                            sumCollaborationEquipments.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDouble("total_critical_rate");
                            sumCollaborationEquipments.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDouble("total_critical_resistance_rate");
                            sumCollaborationEquipments.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDouble("total_ignore_critical_rate");
                            sumCollaborationEquipments.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDouble("total_penetration_rate");
                            sumCollaborationEquipments.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDouble("total_penetration_resistance_rate");
                            sumCollaborationEquipments.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDouble("total_evasion_rate");
                            sumCollaborationEquipments.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDouble("total_damage_absorption_rate");
                            sumCollaborationEquipments.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDouble("total_ignore_damage_absorption_rate");
                            sumCollaborationEquipments.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDouble("total_absorbed_damage_rate");
                            sumCollaborationEquipments.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_rate");
                            sumCollaborationEquipments.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDouble("total_vitality_regeneration_resistance_rate");
                            sumCollaborationEquipments.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDouble("total_accuracy_rate");
                            sumCollaborationEquipments.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDouble("total_lifesteal_rate");
                            sumCollaborationEquipments.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDouble("total_shield_strength");
                            sumCollaborationEquipments.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDouble("total_tenacity");
                            sumCollaborationEquipments.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDouble("total_resistance_rate");
                            sumCollaborationEquipments.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDouble("total_combo_rate");
                            sumCollaborationEquipments.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDouble("total_ignore_combo_rate");
                            sumCollaborationEquipments.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDouble("total_combo_damage_rate");
                            sumCollaborationEquipments.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDouble("total_combo_resistance_rate");
                            sumCollaborationEquipments.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDouble("total_stun_rate");
                            sumCollaborationEquipments.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDouble("total_ignore_stun_rate");
                            sumCollaborationEquipments.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDouble("total_reflection_rate");
                            sumCollaborationEquipments.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDouble("total_ignore_reflection_rate");
                            sumCollaborationEquipments.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDouble("total_reflection_damage_rate");
                            sumCollaborationEquipments.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDouble("total_reflection_resistance_rate");
                            sumCollaborationEquipments.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDouble("total_mana_regeneration_rate");
                            sumCollaborationEquipments.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_different_faction_rate");
                            sumCollaborationEquipments.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_different_faction_rate");
                            sumCollaborationEquipments.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDouble("total_damage_to_same_faction_rate");
                            sumCollaborationEquipments.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDouble("total_resistance_to_same_faction_rate");
                            sumCollaborationEquipments.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDouble("total_normal_damage_rate");
                            sumCollaborationEquipments.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDouble("total_normal_resistance_rate");
                            sumCollaborationEquipments.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDouble("total_skill_damage_rate");
                            sumCollaborationEquipments.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDouble("total_skill_resistance_rate");
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

        return sumCollaborationEquipments;
    }
}