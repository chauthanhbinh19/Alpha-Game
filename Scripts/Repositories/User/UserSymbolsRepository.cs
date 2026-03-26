using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserSymbolsRepository : IUserSymbolsRepository
{
    public async Task<List<Symbols>> GetUserSymbolsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Symbols> symbols = new List<Symbols>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT um.*, m.id, m.name, m.image, m.rare, m.description 
                FROM Symbols m
                JOIN user_Symbols um ON m.id = um.Symbol_id
                WHERE um.user_id = @userId 
                  AND (@type = 'All' OR m.type = @type)
                  AND (@rare = 'All' OR m.rare = @rare)
                  AND (@search = '' OR m.name LIKE CONCAT('%', @search, '%'))
                ORDER BY m.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(m.name, '[0-9]+$') AS UNSIGNED), m.name
                LIMIT @limit OFFSET @offset;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@search", search);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@rare", rare);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Symbols symbol = new Symbols
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

                            symbols.Add(symbol);
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

        return symbols;
    }
    public async Task<int> GetUserSymbolsCountAsync(string user_id, string search, string type, string rare)
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
                FROM Symbols m
                JOIN user_Symbols um ON m.id = um.Symbol_id
                WHERE um.user_id = @userId 
                  AND (@type = 'All' OR m.type = @type)
                  AND (@rare = 'All' OR m.rare = @rare)
                  AND (@search = '' OR m.name LIKE CONCAT('%', @search, '%'));
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@search", search);
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
    public async Task<bool> InsertUserSymbolAsync(Symbols symbol, string userId)
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
                    FROM user_Symbols 
                    WHERE user_id = @user_id AND Symbol_id = @Symbol_id;
                ";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@Symbol_id", symbol.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertQuery = @"
                        INSERT INTO user_Symbols (
                            user_id, Symbol_id, rare, level, experiment, star, quality, block, quantity,
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
                            @user_id, @Symbol_id, @rare, @level, @experiment, @star, @quality, @block, @quantity,
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
                            insertCommand.Parameters.AddWithValue("@Symbol_id", symbol.Id);
                            insertCommand.Parameters.AddWithValue("@rare", symbol.Rare);
                            insertCommand.Parameters.AddWithValue("@level", 0);
                            insertCommand.Parameters.AddWithValue("@experiment", 0);
                            insertCommand.Parameters.AddWithValue("@star", 0);
                            insertCommand.Parameters.AddWithValue("@quality", QualityEvaluator.CheckQuality(symbol.Rare));
                            insertCommand.Parameters.AddWithValue("@block", false);
                            insertCommand.Parameters.AddWithValue("@quantity", symbol.Quantity);
                            insertCommand.Parameters.AddWithValue("@power", symbol.Power);
                            insertCommand.Parameters.AddWithValue("@health", symbol.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", symbol.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", symbol.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", symbol.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", symbol.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", symbol.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", symbol.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", symbol.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", symbol.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", symbol.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", symbol.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", symbol.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", symbol.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", symbol.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", symbol.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", symbol.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", symbol.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", symbol.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", symbol.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", symbol.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", symbol.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", symbol.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", symbol.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", symbol.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", symbol.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", symbol.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", symbol.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", symbol.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", symbol.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", symbol.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", symbol.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", symbol.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", symbol.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", symbol.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", symbol.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", symbol.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", symbol.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", symbol.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", symbol.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", symbol.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", symbol.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", symbol.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", symbol.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", symbol.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", symbol.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", symbol.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", symbol.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", symbol.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", symbol.SkillResistanceRate);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateQuery = @"
                        UPDATE user_Symbols
                        SET quantity = @quantity
                        WHERE user_id = @user_id AND Symbol_id = @Symbol_id;
                    ";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", userId);
                            updateCommand.Parameters.AddWithValue("@Symbol_id", symbol.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", symbol.Quantity);

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
    public async Task<bool> UpdateSymbolLevelAsync(Symbols symbol, int cardLevel)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_Symbols
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
                WHERE user_id = @user_id AND Symbol_id = @Symbol_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@Symbol_id", symbol.Id);
                    command.Parameters.AddWithValue("@level", cardLevel);
                    command.Parameters.AddWithValue("@power", symbol.Power);
                    command.Parameters.AddWithValue("@health", symbol.Health);
                    command.Parameters.AddWithValue("@physical_attack", symbol.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", symbol.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", symbol.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", symbol.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", symbol.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", symbol.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", symbol.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", symbol.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", symbol.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", symbol.MentalDefense);
                    command.Parameters.AddWithValue("@speed", symbol.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", symbol.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", symbol.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", symbol.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", symbol.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", symbol.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", symbol.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", symbol.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", symbol.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", symbol.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", symbol.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", symbol.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", symbol.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", symbol.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", symbol.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", symbol.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", symbol.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", symbol.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", symbol.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", symbol.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", symbol.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", symbol.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", symbol.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", symbol.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", symbol.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", symbol.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", symbol.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", symbol.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", symbol.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", symbol.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", symbol.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", symbol.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", symbol.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", symbol.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", symbol.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", symbol.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", symbol.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", symbol.SkillResistanceRate);

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
    public async Task<bool> UpdateSymbolBreakthroughAsync(Symbols symbol, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                UPDATE user_Symbols
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
                WHERE user_id = @user_id AND Symbol_id = @Symbol_id;
            ";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@Symbol_id", symbol.Id);
                    command.Parameters.AddWithValue("@star", star);
                    command.Parameters.AddWithValue("@quantity", quantity);
                    command.Parameters.AddWithValue("@power", symbol.Power);
                    command.Parameters.AddWithValue("@health", symbol.Health);
                    command.Parameters.AddWithValue("@physical_attack", symbol.PhysicalAttack);
                    command.Parameters.AddWithValue("@physical_defense", symbol.PhysicalDefense);
                    command.Parameters.AddWithValue("@magical_attack", symbol.MagicalAttack);
                    command.Parameters.AddWithValue("@magical_defense", symbol.MagicalDefense);
                    command.Parameters.AddWithValue("@chemical_attack", symbol.ChemicalAttack);
                    command.Parameters.AddWithValue("@chemical_defense", symbol.ChemicalDefense);
                    command.Parameters.AddWithValue("@atomic_attack", symbol.AtomicAttack);
                    command.Parameters.AddWithValue("@atomic_defense", symbol.AtomicDefense);
                    command.Parameters.AddWithValue("@mental_attack", symbol.MentalAttack);
                    command.Parameters.AddWithValue("@mental_defense", symbol.MentalDefense);
                    command.Parameters.AddWithValue("@speed", symbol.Speed);
                    command.Parameters.AddWithValue("@critical_damage_rate", symbol.CriticalDamageRate);
                    command.Parameters.AddWithValue("@critical_rate", symbol.CriticalRate);
                    command.Parameters.AddWithValue("@critical_resistance_rate", symbol.CriticalResistanceRate);
                    command.Parameters.AddWithValue("@ignore_critical_rate", symbol.IgnoreCriticalRate);
                    command.Parameters.AddWithValue("@penetration_rate", symbol.PenetrationRate);
                    command.Parameters.AddWithValue("@penetration_resistance_rate", symbol.PenetrationResistanceRate);
                    command.Parameters.AddWithValue("@evasion_rate", symbol.EvasionRate);
                    command.Parameters.AddWithValue("@damage_absorption_rate", symbol.DamageAbsorptionRate);
                    command.Parameters.AddWithValue("@ignore_damage_absorption_rate", symbol.IgnoreDamageAbsorptionRate);
                    command.Parameters.AddWithValue("@absorbed_damage_rate", symbol.AbsorbedDamageRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_rate", symbol.VitalityRegenerationRate);
                    command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", symbol.VitalityRegenerationResistanceRate);
                    command.Parameters.AddWithValue("@accuracy_rate", symbol.AccuracyRate);
                    command.Parameters.AddWithValue("@lifesteal_rate", symbol.LifestealRate);
                    command.Parameters.AddWithValue("@shield_strength", symbol.ShieldStrength);
                    command.Parameters.AddWithValue("@tenacity", symbol.Tenacity);
                    command.Parameters.AddWithValue("@resistance_rate", symbol.ResistanceRate);
                    command.Parameters.AddWithValue("@combo_rate", symbol.ComboRate);
                    command.Parameters.AddWithValue("@ignore_combo_rate", symbol.IgnoreComboRate);
                    command.Parameters.AddWithValue("@combo_damage_rate", symbol.ComboDamageRate);
                    command.Parameters.AddWithValue("@combo_resistance_rate", symbol.ComboResistanceRate);
                    command.Parameters.AddWithValue("@stun_rate", symbol.StunRate);
                    command.Parameters.AddWithValue("@ignore_stun_rate", symbol.IgnoreStunRate);
                    command.Parameters.AddWithValue("@reflection_rate", symbol.ReflectionRate);
                    command.Parameters.AddWithValue("@ignore_reflection_rate", symbol.IgnoreReflectionRate);
                    command.Parameters.AddWithValue("@reflection_damage_rate", symbol.ReflectionDamageRate);
                    command.Parameters.AddWithValue("@reflection_resistance_rate", symbol.ReflectionResistanceRate);
                    command.Parameters.AddWithValue("@mana", symbol.Mana);
                    command.Parameters.AddWithValue("@mana_regeneration_rate", symbol.ManaRegenerationRate);
                    command.Parameters.AddWithValue("@damage_to_different_faction_rate", symbol.DamageToDifferentFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_different_faction_rate", symbol.ResistanceToDifferentFactionRate);
                    command.Parameters.AddWithValue("@damage_to_same_faction_rate", symbol.DamageToSameFactionRate);
                    command.Parameters.AddWithValue("@resistance_to_same_faction_rate", symbol.ResistanceToSameFactionRate);
                    command.Parameters.AddWithValue("@normal_damage_rate", symbol.NormalDamageRate);
                    command.Parameters.AddWithValue("@normal_resistance_rate", symbol.NormalResistanceRate);
                    command.Parameters.AddWithValue("@skill_damage_rate", symbol.SkillDamageRate);
                    command.Parameters.AddWithValue("@skill_resistance_rate", symbol.SkillResistanceRate);

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
    public async Task<Symbols> GetUserSymbolByIdAsync(string user_id, string Id)
    {
        Symbols symbol = null;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"SELECT * FROM user_Symbols 
                             WHERE Symbol_id=@id AND user_id=@user_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", Id);
                    command.Parameters.AddWithValue("@user_id", user_id);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            symbol = new Symbols
                            {
                                Id = reader.GetStringSafe("Symbol_id"),
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

        return symbol;
    }
    public async Task<Symbols> SumPowerUserSymbolsAsync()
    {
        Symbols sumSymbols = new Symbols();
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
                FROM user_Symbols
                WHERE user_id = @user_id;";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);

                    await using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumSymbols.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            sumSymbols.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            sumSymbols.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetDoubleSafe("total_mana");
                            sumSymbols.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            sumSymbols.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            sumSymbols.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            sumSymbols.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            sumSymbols.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            sumSymbols.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            sumSymbols.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            sumSymbols.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            sumSymbols.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            sumSymbols.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            sumSymbols.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            sumSymbols.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            sumSymbols.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            sumSymbols.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            sumSymbols.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            sumSymbols.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            sumSymbols.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            sumSymbols.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            sumSymbols.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            sumSymbols.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            sumSymbols.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            sumSymbols.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            sumSymbols.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            sumSymbols.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            sumSymbols.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            sumSymbols.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            sumSymbols.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            sumSymbols.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            sumSymbols.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            sumSymbols.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            sumSymbols.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            sumSymbols.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            sumSymbols.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            sumSymbols.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            sumSymbols.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            sumSymbols.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            sumSymbols.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            sumSymbols.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            sumSymbols.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            sumSymbols.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            sumSymbols.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            sumSymbols.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            sumSymbols.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            sumSymbols.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            sumSymbols.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            sumSymbols.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            sumSymbols.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
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

        return sumSymbols;
    }
}