using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserVehiclesRepository : IUserVehiclesRepository
{
    public async Task<List<Vehicles>> GetUserVehiclesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> vehicles = new List<Vehicles>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT um.*, m.id, m.name, m.image, m.rare, m.description 
                FROM vehicles m
                JOIN user_vehicles um ON m.id = um.vehicle_id
                WHERE um.user_id = @userId";

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

                query += @"
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name
                LIMIT @limit OFFSET @offset";

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
                            Vehicles vehicle = new Vehicles
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

                            vehicles.Add(vehicle);
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

        return vehicles;
    }
    public async Task<int> GetUserVehiclesCountAsync(string user_id, string search, string type, string rare)
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
                FROM vehicles m
                JOIN user_vehicles um ON m.id = um.vehicle_id
                WHERE um.user_id = @userId";

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
    public async Task<bool> InsertUserVehicleAsync(Vehicles vehicle, string userId)
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
                FROM user_vehicles 
                WHERE user_id = @user_id AND vehicle_id = @vehicle_id;
            ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@vehicle_id", vehicle.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"
                        INSERT INTO user_vehicles (
                            user_id, vehicle_id, rare, level, experiment, star, quality, block, quantity,
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
                            @user_id, @vehicle_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                            insertCommand.Parameters.AddWithValue("@vehicle_id", vehicle.Id);
                            insertCommand.Parameters.AddWithValue("@rare", vehicle.Rare);
                            insertCommand.Parameters.AddWithValue("@level", 0);
                            insertCommand.Parameters.AddWithValue("@experiment", 0);
                            insertCommand.Parameters.AddWithValue("@star", 0);
                            insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(vehicle.Rare));
                            insertCommand.Parameters.AddWithValue("@block", false);
                            insertCommand.Parameters.AddWithValue("@quantity", vehicle.Quantity);
                            insertCommand.Parameters.AddWithValue("@power", vehicle.Power);
                            insertCommand.Parameters.AddWithValue("@health", vehicle.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", vehicle.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", vehicle.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", vehicle.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", vehicle.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", vehicle.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", vehicle.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", vehicle.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", vehicle.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", vehicle.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", vehicle.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", vehicle.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", vehicle.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", vehicle.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", vehicle.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", vehicle.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", vehicle.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", vehicle.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", vehicle.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", vehicle.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", vehicle.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", vehicle.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", vehicle.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", vehicle.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", vehicle.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", vehicle.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", vehicle.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", vehicle.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", vehicle.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", vehicle.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", vehicle.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", vehicle.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", vehicle.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", vehicle.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", vehicle.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", vehicle.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", vehicle.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", vehicle.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", vehicle.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", vehicle.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", vehicle.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", vehicle.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", vehicle.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", vehicle.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", vehicle.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", vehicle.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", vehicle.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", vehicle.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", vehicle.SkillResistanceRate);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_vehicles
                        SET quantity = @quantity
                        WHERE user_id = @user_id AND vehicle_id = @vehicle_id;
                    ";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", userId);
                            updateCommand.Parameters.AddWithValue("@vehicle_id", vehicle.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", vehicle.Quantity);

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
    public async Task<bool> UpdateVehicleLevelAsync(Vehicles vehicle, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_vehicles
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
                WHERE user_id = @user_id AND vehicle_id = @vehicle_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@vehicle_id", vehicle.Id);
                    command.Parameters.AddWithValue("@level", cardLevel);
                    command.Parameters.AddWithValue("@power", vehicle.Power);
                    command.Parameters.AddWithValue("@health", vehicle.Health);
                    command.Parameters.AddWithValue("@physical_attack", vehicle.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", vehicle.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", vehicle.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", vehicle.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", vehicle.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", vehicle.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", vehicle.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", vehicle.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", vehicle.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", vehicle.MentalDefense);
                    command.Parameters.AddWithValue("@speed", vehicle.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", vehicle.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", vehicle.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", vehicle.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", vehicle.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", vehicle.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", vehicle.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", vehicle.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", vehicle.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", vehicle.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", vehicle.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", vehicle.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", vehicle.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", vehicle.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", vehicle.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", vehicle.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", vehicle.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", vehicle.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", vehicle.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", vehicle.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", vehicle.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", vehicle.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", vehicle.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", vehicle.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", vehicle.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", vehicle.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", vehicle.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", vehicle.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", vehicle.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", vehicle.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", vehicle.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", vehicle.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", vehicle.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", vehicle.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", vehicle.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", vehicle.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", vehicle.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", vehicle.SkillResistanceRate);

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
    public async Task<bool> UpdateVehicleBreakthroughAsync(Vehicles vehicle, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_vehicles
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
                WHERE user_id = @user_id AND vehicle_id = @vehicle_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@vehicle_id", vehicle.Id);
                    command.Parameters.AddWithValue("@star", star);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@power", vehicle.Power);
                    command.Parameters.AddWithValue("@health", vehicle.Health);
                    command.Parameters.AddWithValue("@physical_attack", vehicle.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", vehicle.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", vehicle.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", vehicle.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", vehicle.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", vehicle.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", vehicle.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", vehicle.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", vehicle.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", vehicle.MentalDefense);
                    command.Parameters.AddWithValue("@speed", vehicle.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", vehicle.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", vehicle.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", vehicle.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", vehicle.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", vehicle.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", vehicle.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", vehicle.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", vehicle.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", vehicle.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", vehicle.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", vehicle.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", vehicle.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", vehicle.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", vehicle.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", vehicle.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", vehicle.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", vehicle.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", vehicle.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", vehicle.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", vehicle.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", vehicle.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", vehicle.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", vehicle.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", vehicle.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", vehicle.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", vehicle.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", vehicle.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", vehicle.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", vehicle.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", vehicle.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", vehicle.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", vehicle.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", vehicle.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", vehicle.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", vehicle.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", vehicle.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", vehicle.SkillResistanceRate);

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
    public async Task<Vehicles> GetUserVehicleByIdAsync(string user_id, string Id)
    {
        Vehicles vehicle = null;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT * FROM user_vehicles 
                             WHERE vehicle_id=@id AND user_id=@user_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@user_id", user_id);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            vehicle = new Vehicles
                            {
                                Id = reader.GetStringSafe("vehicle_id"),
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

        return vehicle;
    }
    public async Task<Vehicles> SumPowerUserVehiclesAsync()
    {
        Vehicles sumVehicles = new Vehicles();
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
                FROM user_vehicles
                WHERE user_id = @user_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumVehicles.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            sumVehicles.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            sumVehicles.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            sumVehicles.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            sumVehicles.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            sumVehicles.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            sumVehicles.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            sumVehicles.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            sumVehicles.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            sumVehicles.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            sumVehicles.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            sumVehicles.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            sumVehicles.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            sumVehicles.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            sumVehicles.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            sumVehicles.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            sumVehicles.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            sumVehicles.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            sumVehicles.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            sumVehicles.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            sumVehicles.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            sumVehicles.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            sumVehicles.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            sumVehicles.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            sumVehicles.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            sumVehicles.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            sumVehicles.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            sumVehicles.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            sumVehicles.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            sumVehicles.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            sumVehicles.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            sumVehicles.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            sumVehicles.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            sumVehicles.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            sumVehicles.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            sumVehicles.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            sumVehicles.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            sumVehicles.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            sumVehicles.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            sumVehicles.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            sumVehicles.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            sumVehicles.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            sumVehicles.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            sumVehicles.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            sumVehicles.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            sumVehicles.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            sumVehicles.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            sumVehicles.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            sumVehicles.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            sumVehicles.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
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

        return sumVehicles;
    }
}