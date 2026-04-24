using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserBuildingsRepository : IUserBuildingsRepository
{
    public async Task<List<Buildings>> GetUserBuildingsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Buildings> Buildings = new List<Buildings>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT um.*, m.id, m.name, m.image, m.rare, m.description 
                FROM Buildings m
                JOIN user_buildings um ON m.id = um.building_id
                WHERE um.user_id = @userId 
            ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    query += " AND m.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND m.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND m.name LIKE CONCAT('%', @search, '%')";
                }

                query += " ORDER BY m.name";
                query += " LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    if (!string.IsNullOrEmpty(type) && type != "All")
                    {
                        command.Parameters.AddWithValue("@type", type);
                    }

                    if (!string.IsNullOrEmpty(rare) && rare != "All")
                    {
                        command.Parameters.AddWithValue("@rare", rare);
                    }

                    if (!string.IsNullOrEmpty(search))
                    {
                        command.Parameters.AddWithValue("@search", search);
                    }
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Buildings Building = new Buildings
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rare = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Star = reader.GetIntSafe("star"),
                                Level = reader.GetIntSafe("level"),
                                Experiment = reader.GetDoubleSafe("experiment"),
                                Quantity = reader.GetDoubleSafe("quantity"),
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
                                Description = reader.GetStringSafe("description")
                            };

                            Buildings.Add(Building);
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

        return Buildings;
    }
    public async Task<int> GetUserBuildingsCountAsync(string user_id, string search, string type, string rare)
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
                FROM Buildings m
                JOIN user_buildings um ON m.id = um.building_id
                WHERE um.user_id = @userId 
            ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    query += " AND m.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    query += " AND m.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query += " AND m.name LIKE CONCAT('%', @search, '%')";
                }

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    if (!string.IsNullOrEmpty(type) && type != "All")
                    {
                        command.Parameters.AddWithValue("@type", type);
                    }

                    if (!string.IsNullOrEmpty(rare) && rare != "All")
                    {
                        command.Parameters.AddWithValue("@rare", rare);
                    }

                    if (!string.IsNullOrEmpty(search))
                    {
                        command.Parameters.AddWithValue("@search", search);
                    }

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
    public async Task<bool> InsertUserBuildingAsync(Buildings building, string userId)
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
                FROM user_buildings 
                WHERE user_id = @user_id AND building_id = @building_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@building_id", building.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"
                        INSERT INTO user_buildings (
                            user_id, building_id, rare, level, experiment, star, quality, block, quantity,
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
                            @user_id, @building_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                            insertCommand.Parameters.AddWithValue("@building_id", building.Id);
                            insertCommand.Parameters.AddWithValue("@rare", building.Rare);
                            insertCommand.Parameters.AddWithValue("@level", 0);
                            insertCommand.Parameters.AddWithValue("@experiment", 0);
                            insertCommand.Parameters.AddWithValue("@star", 0);
                            insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(building.Rare));
                            insertCommand.Parameters.AddWithValue("@block", false);
                            insertCommand.Parameters.AddWithValue("@quantity", building.Quantity);
                            insertCommand.Parameters.AddWithValue("@power", building.Power);
                            insertCommand.Parameters.AddWithValue("@health", building.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", building.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", building.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", building.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", building.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", building.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", building.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", building.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", building.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", building.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", building.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", building.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", building.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", building.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", building.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", building.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", building.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", building.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", building.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", building.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", building.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", building.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", building.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", building.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", building.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", building.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", building.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", building.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", building.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", building.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", building.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", building.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", building.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", building.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", building.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", building.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", building.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", building.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", building.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", building.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", building.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", building.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", building.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", building.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", building.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", building.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", building.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", building.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", building.SkillResistanceRate);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_buildings
                        SET quantity = @quantity
                        WHERE user_id = @user_id AND building_id = @building_id;
                    ";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", userId);
                            updateCommand.Parameters.AddWithValue("@building_id", building.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", building.Quantity);

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
    public async Task<bool> UpdateBuildingLevelAsync(Buildings building, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_buildings
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
                WHERE user_id = @user_id AND building_id = @building_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@building_id", building.Id);
                    command.Parameters.AddWithValue("@level", cardLevel);
                    command.Parameters.AddWithValue("@power", building.Power);
                    command.Parameters.AddWithValue("@health", building.Health);
                    command.Parameters.AddWithValue("@physical_attack", building.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", building.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", building.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", building.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", building.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", building.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", building.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", building.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", building.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", building.MentalDefense);
                    command.Parameters.AddWithValue("@speed", building.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", building.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", building.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", building.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", building.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", building.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", building.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", building.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", building.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", building.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", building.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", building.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", building.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", building.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", building.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", building.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", building.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", building.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", building.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", building.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", building.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", building.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", building.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", building.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", building.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", building.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", building.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", building.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", building.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", building.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", building.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", building.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", building.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", building.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", building.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", building.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", building.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", building.SkillResistanceRate);

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
    public async Task<bool> UpdateBuildingBreakthroughAsync(Buildings building, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_buildings
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
                WHERE user_id = @user_id AND building_id = @building_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@building_id", building.Id);
                    command.Parameters.AddWithValue("@star", star);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@power", building.Power);
                    command.Parameters.AddWithValue("@health", building.Health);
                    command.Parameters.AddWithValue("@physical_attack", building.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", building.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", building.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", building.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", building.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", building.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", building.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", building.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", building.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", building.MentalDefense);
                    command.Parameters.AddWithValue("@speed", building.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", building.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", building.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", building.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", building.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", building.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", building.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", building.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", building.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", building.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", building.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", building.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", building.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", building.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", building.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", building.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", building.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", building.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", building.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", building.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", building.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", building.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", building.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", building.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", building.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", building.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", building.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", building.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", building.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", building.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", building.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", building.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", building.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", building.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", building.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", building.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", building.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", building.SkillResistanceRate);

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
    public async Task<Buildings> GetUserBuildingByIdAsync(string user_id, string Id)
    {
        Buildings Building = null;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT * FROM user_buildings 
                             WHERE building_id=@id AND user_id=@user_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@user_id", user_id);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            Building = new Buildings
                            {
                                Id = reader.GetStringSafe("building_id"),
                                Level = reader.GetIntSafe("level"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Experiment = reader.GetDoubleSafe("experiment"),
                                Star = reader.GetIntSafe("star"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate")
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

        return Building;
    }
    public async Task<Buildings> SumPowerUserBuildingsAsync()
    {
        Buildings sumBuildings = new Buildings();
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
                FROM user_buildings
                WHERE user_id = @user_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumBuildings.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            sumBuildings.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            sumBuildings.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            sumBuildings.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            sumBuildings.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            sumBuildings.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            sumBuildings.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            sumBuildings.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            sumBuildings.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            sumBuildings.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            sumBuildings.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            sumBuildings.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            sumBuildings.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            sumBuildings.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            sumBuildings.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            sumBuildings.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            sumBuildings.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            sumBuildings.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            sumBuildings.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            sumBuildings.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            sumBuildings.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            sumBuildings.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            sumBuildings.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            sumBuildings.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            sumBuildings.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            sumBuildings.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            sumBuildings.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            sumBuildings.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            sumBuildings.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            sumBuildings.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            sumBuildings.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            sumBuildings.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            sumBuildings.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            sumBuildings.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            sumBuildings.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            sumBuildings.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            sumBuildings.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            sumBuildings.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            sumBuildings.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            sumBuildings.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            sumBuildings.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            sumBuildings.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            sumBuildings.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            sumBuildings.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            sumBuildings.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            sumBuildings.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            sumBuildings.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            sumBuildings.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            sumBuildings.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            sumBuildings.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
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

        return sumBuildings;
    }
}