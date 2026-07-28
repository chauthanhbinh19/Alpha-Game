using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserWeaponsRepository : IUserWeaponsRepository
{
    public async Task<List<Weapons>> GetUserWeaponsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Weapons> weapons = new List<Weapons>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT ut.*, t.id, t.name, t.image, t.rare, t.type, t.description
                FROM Weapons t
                INNER JOIN user_weapons ut ON t.id = ut.weapon_id
                WHERE ut.user_id = @userId";

                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND m.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND t.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND t.name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += @"
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);

                    if (!string.IsNullOrEmpty(type) && type != "All")
                    {
                        selectCommand.Parameters.AddWithValue("@type", type);
                    }

                    if (!string.IsNullOrEmpty(rare) && rare != "All")
                    {
                        selectCommand.Parameters.AddWithValue("@rare", rare);
                    }

                    if (!string.IsNullOrEmpty(search))
                    {
                        selectCommand.Parameters.AddWithValue("@search", search);
                    }

                    selectCommand.Parameters.AddWithValue("@limit", pageSize);
                    selectCommand.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Weapons weapon = new Weapons
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rarity = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Type = reader.GetStringSafe("type"),
                                Star = reader.GetIntSafe("star"),
                                Level = reader.GetIntSafe("level"),
                                Experience = reader.GetDoubleSafe("experience"),
                                Quantity = reader.GetIntSafe("quantity"),
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

                            weapons.Add(weapon);
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

        return weapons;
    }
    public async Task<int> GetUserWeaponsCountAsync(string userId, string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT COUNT(*) 
                FROM Weapons t
                INNER JOIN user_weapons ut ON t.id = ut.weapon_id
                WHERE ut.user_id = @userId";

                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND m.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND t.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND t.name LIKE CONCAT('%', @search, '%')";
                }

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);

                    if (!string.IsNullOrEmpty(type) && type != "All")
                    {
                        selectCommand.Parameters.AddWithValue("@type", type);
                    }

                    if (!string.IsNullOrEmpty(rare) && rare != "All")
                    {
                        selectCommand.Parameters.AddWithValue("@rare", rare);
                    }

                    if (!string.IsNullOrEmpty(search))
                    {
                        selectCommand.Parameters.AddWithValue("@search", search);
                    }

                    object result = await selectCommand.ExecuteScalarAsync();
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
    public async Task<bool> InsertUserWeaponAsync(Weapons weapon, string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) FROM user_weapons 
                WHERE user_id = @user_id AND weapon_id = @weapon_id;";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@weapon_id", weapon.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertSQL = @"
                        INSERT INTO user_weapons (
                            user_id, weapon_id, rare, level, experience, star, quality, block, quantity,
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
                            @user_id, @weapon_id, @rare, @level, @experience, @star, @quality, @block, @quantity,
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

                        await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@user_id", userId);
                            insertCommand.Parameters.AddWithValue("@weapon_id", weapon.Id);
                            insertCommand.Parameters.AddWithValue("@rare", weapon.Rarity);
                            insertCommand.Parameters.AddWithValue("@level", 0);
                            insertCommand.Parameters.AddWithValue("@experience", 0);
                            insertCommand.Parameters.AddWithValue("@star", 0);
                            insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(weapon.Rarity));
                            insertCommand.Parameters.AddWithValue("@block", false);
                            insertCommand.Parameters.AddWithValue("@quantity", weapon.Quantity);
                            insertCommand.Parameters.AddWithValue("@power", weapon.Power);
                            insertCommand.Parameters.AddWithValue("@health", weapon.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", weapon.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", weapon.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", weapon.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", weapon.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", weapon.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", weapon.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", weapon.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", weapon.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", weapon.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", weapon.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", weapon.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", weapon.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", weapon.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", weapon.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", weapon.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", weapon.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", weapon.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", weapon.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", weapon.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", weapon.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", weapon.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", weapon.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", weapon.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", weapon.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", weapon.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", weapon.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", weapon.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", weapon.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", weapon.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", weapon.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", weapon.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", weapon.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", weapon.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", weapon.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", weapon.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", weapon.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", weapon.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", weapon.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", weapon.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", weapon.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", weapon.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", weapon.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", weapon.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", weapon.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", weapon.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", weapon.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", weapon.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", weapon.SkillResistanceRate);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateSQL = @"
                        UPDATE user_weapons
                        SET quantity = @quantity
                        WHERE user_id = @user_id AND weapon_id = @weapon_id;";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", userId);
                            updateCommand.Parameters.AddWithValue("@weapon_id", weapon.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", weapon.Quantity);

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
    public async Task<bool> InsertOrUpdateUserWeaponsBatchAsync(string userId, List<Weapons> weapons)
    {
        if (weapons == null || weapons.Count == 0)
            return true;

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 500; // vì nhiều column → giảm size

            for (int i = 0; i < weapons.Count; i += batchSize)
            {
                var batch = weapons.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
                INSERT INTO user_weapons (
                    user_id, weapon_id, rare, level, experience, star, quality, block, quantity,
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
                ) VALUES ");

                for (int j = 0; j < batch.Count; j++)
                {
                    var c = batch[j];

                    stringBuilder.Append($@"
                    (@user_id, @weapon_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
                    @power_{j}, @health_{j}, @physical_attack_{j}, @physical_defense_{j}, @magical_attack_{j}, @magical_defense_{j},
                    @chemical_attack_{j}, @chemical_defense_{j}, @atomic_attack_{j}, @atomic_defense_{j}, @mental_attack_{j}, @mental_defense_{j},
                    @speed_{j}, @critical_damage_rate_{j}, @critical_rate_{j}, @critical_resistance_rate_{j}, @ignore_critical_rate_{j},
                    @penetration_rate_{j}, @penetration_resistance_rate_{j},
                    @evasion_rate_{j}, @damage_absorption_rate_{j}, @ignore_damage_absorption_rate_{j}, @absorbed_damage_rate_{j},
                    @vitality_regeneration_rate_{j}, @vitality_regeneration_resistance_rate_{j},
                    @accuracy_rate_{j}, @lifesteal_rate_{j}, @shield_strength_{j}, @tenacity_{j}, @resistance_rate_{j},
                    @combo_rate_{j}, @ignore_combo_rate_{j}, @combo_damage_rate_{j}, @combo_resistance_rate_{j},
                    @stun_rate_{j}, @ignore_stun_rate_{j},
                    @reflection_rate_{j}, @ignore_reflection_rate_{j}, @reflection_damage_rate_{j}, @reflection_resistance_rate_{j},
                    @mana_{j}, @mana_regeneration_rate_{j},
                    @damage_to_different_faction_rate_{j}, @resistance_to_different_faction_rate_{j},
                    @damage_to_same_faction_rate_{j}, @resistance_to_same_faction_rate_{j},
                    @normal_damage_rate_{j}, @normal_resistance_rate_{j},
                    @skill_damage_rate_{j}, @skill_resistance_rate_{j}
                    ),");

                    parameters.AddRange(new[]
                    {
                        new MySqlParameter($"@weapon_id_{j}", c.Id),
                        new MySqlParameter($"@rare_{j}", c.Rarity),
                        new MySqlParameter($"@quality_{j}", QualityEvaluatorHelper.CheckQuality(c.Rarity)),
                        new MySqlParameter($"@quantity_{j}", c.Quantity),
                        new MySqlParameter($"@power_{j}", c.Power),
                        new MySqlParameter($"@health_{j}", c.Health),
                        new MySqlParameter($"@physical_attack_{j}", c.PhysicalAttack),
                        new MySqlParameter($"@physical_defense_{j}", c.PhysicalDefense),
                        new MySqlParameter($"@magical_attack_{j}", c.MagicalAttack),
                        new MySqlParameter($"@magical_defense_{j}", c.MagicalDefense),
                        new MySqlParameter($"@chemical_attack_{j}", c.ChemicalAttack),
                        new MySqlParameter($"@chemical_defense_{j}", c.ChemicalDefense),
                        new MySqlParameter($"@atomic_attack_{j}", c.AtomicAttack),
                        new MySqlParameter($"@atomic_defense_{j}", c.AtomicDefense),
                        new MySqlParameter($"@mental_attack_{j}", c.MentalAttack),
                        new MySqlParameter($"@mental_defense_{j}", c.MentalDefense),
                        new MySqlParameter($"@speed_{j}", c.Speed),
                        new MySqlParameter($"@critical_damage_rate_{j}", c.CriticalDamageRate),
                        new MySqlParameter($"@critical_rate_{j}", c.CriticalRate),
                        new MySqlParameter($"@critical_resistance_rate_{j}", c.CriticalResistanceRate),
                        new MySqlParameter($"@ignore_critical_rate_{j}", c.IgnoreCriticalRate),
                        new MySqlParameter($"@penetration_rate_{j}", c.PenetrationRate),
                        new MySqlParameter($"@penetration_resistance_rate_{j}", c.PenetrationResistanceRate),
                        new MySqlParameter($"@evasion_rate_{j}", c.EvasionRate),
                        new MySqlParameter($"@damage_absorption_rate_{j}", c.DamageAbsorptionRate),
                        new MySqlParameter($"@ignore_damage_absorption_rate_{j}", c.IgnoreDamageAbsorptionRate),
                        new MySqlParameter($"@absorbed_damage_rate_{j}", c.AbsorbedDamageRate),
                        new MySqlParameter($"@vitality_regeneration_rate_{j}", c.VitalityRegenerationRate),
                        new MySqlParameter($"@vitality_regeneration_resistance_rate_{j}", c.VitalityRegenerationResistanceRate),
                        new MySqlParameter($"@accuracy_rate_{j}", c.AccuracyRate),
                        new MySqlParameter($"@lifesteal_rate_{j}", c.LifestealRate),
                        new MySqlParameter($"@shield_strength_{j}", c.ShieldStrength),
                        new MySqlParameter($"@tenacity_{j}", c.Tenacity),
                        new MySqlParameter($"@resistance_rate_{j}", c.ResistanceRate),
                        new MySqlParameter($"@combo_rate_{j}", c.ComboRate),
                        new MySqlParameter($"@ignore_combo_rate_{j}", c.IgnoreComboRate),
                        new MySqlParameter($"@combo_damage_rate_{j}", c.ComboDamageRate),
                        new MySqlParameter($"@combo_resistance_rate_{j}", c.ComboResistanceRate),
                        new MySqlParameter($"@stun_rate_{j}", c.StunRate),
                        new MySqlParameter($"@ignore_stun_rate_{j}", c.IgnoreStunRate),
                        new MySqlParameter($"@reflection_rate_{j}", c.ReflectionRate),
                        new MySqlParameter($"@ignore_reflection_rate_{j}", c.IgnoreReflectionRate),
                        new MySqlParameter($"@reflection_damage_rate_{j}", c.ReflectionDamageRate),
                        new MySqlParameter($"@reflection_resistance_rate_{j}", c.ReflectionResistanceRate),
                        new MySqlParameter($"@mana_{j}", c.Mana),
                        new MySqlParameter($"@mana_regeneration_rate_{j}", c.ManaRegenerationRate),
                        new MySqlParameter($"@damage_to_different_faction_rate_{j}", c.DamageToDifferentFactionRate),
                        new MySqlParameter($"@resistance_to_different_faction_rate_{j}", c.ResistanceToDifferentFactionRate),
                        new MySqlParameter($"@damage_to_same_faction_rate_{j}", c.DamageToSameFactionRate),
                        new MySqlParameter($"@resistance_to_same_faction_rate_{j}", c.ResistanceToSameFactionRate),
                        new MySqlParameter($"@normal_damage_rate_{j}", c.NormalDamageRate),
                        new MySqlParameter($"@normal_resistance_rate_{j}", c.NormalResistanceRate),
                        new MySqlParameter($"@skill_damage_rate_{j}", c.SkillDamageRate),
                        new MySqlParameter($"@skill_resistance_rate_{j}", c.SkillResistanceRate),
                });
                }

                stringBuilder.Length--; // remove dấu ,

                stringBuilder.Append(@"
                ON DUPLICATE KEY UPDATE
                    quantity = COALESCE(user_weapons.quantity, 0) + VALUES(quantity);
                ");

                await using var command = new MySqlCommand(stringBuilder.ToString(), connection, (MySqlTransaction)transaction);

                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddRange(parameters.ToArray());

                await command.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError("Batch Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<bool> UpdateUserWeaponLevelAsync(string userId, Weapons weapon)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_weapons
                SET 
                    level = @level, experience = @experience
                WHERE user_id = @user_id AND weapon_id = @weapon_id;
            ";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@weapon_id", weapon.Id);
                    updateCommand.Parameters.AddWithValue("@level", weapon.Level);
                    updateCommand.Parameters.AddWithValue("@experience", weapon.Experience);

                    await updateCommand.ExecuteNonQueryAsync();
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
    public async Task<bool> UpdateUserWeaponStarAsync(string userId, Weapons weapon)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_weapons
                SET 
                    star = @star, quantity = @quantity
                WHERE user_id = @user_id AND weapon_id = @weapon_id;
            ";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@weapon_id", weapon.Id);
                    updateCommand.Parameters.AddWithValue("@star", weapon.Star);
                    updateCommand.Parameters.AddWithValue("@quantity", weapon.Quantity);

                    await updateCommand.ExecuteNonQueryAsync();
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
    public async Task<bool> UpdateUserWeaponBreakthroughAsync(string userId, Weapons weapon, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string updateSQL = @"
                UPDATE user_weapons
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
                WHERE user_id = @user_id AND weapon_id = @weapon_id;";
                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@weapon_id", weapon.Id);
                    updateCommand.Parameters.AddWithValue("@star", star);
                    updateCommand.Parameters.AddWithValue("@quantity", quantity);
                    updateCommand.Parameters.AddWithValue("@power", weapon.Power);
                    updateCommand.Parameters.AddWithValue("@health", weapon.Health);
                    updateCommand.Parameters.AddWithValue("@physical_attack", weapon.PhysicalAttack);
                    updateCommand.Parameters.AddWithValue("@physical_defense", weapon.PhysicalDefense);
                    updateCommand.Parameters.AddWithValue("@magical_attack", weapon.MagicalAttack);
                    updateCommand.Parameters.AddWithValue("@magical_defense", weapon.MagicalDefense);
                    updateCommand.Parameters.AddWithValue("@chemical_attack", weapon.ChemicalAttack);
                    updateCommand.Parameters.AddWithValue("@chemical_defense", weapon.ChemicalDefense);
                    updateCommand.Parameters.AddWithValue("@atomic_attack", weapon.AtomicAttack);
                    updateCommand.Parameters.AddWithValue("@atomic_defense", weapon.AtomicDefense);
                    updateCommand.Parameters.AddWithValue("@mental_attack", weapon.MentalAttack);
                    updateCommand.Parameters.AddWithValue("@mental_defense", weapon.MentalDefense);
                    updateCommand.Parameters.AddWithValue("@speed", weapon.Speed);
                    updateCommand.Parameters.AddWithValue("@critical_damage_rate", weapon.CriticalDamageRate);
                    updateCommand.Parameters.AddWithValue("@critical_rate", weapon.CriticalRate);
                    updateCommand.Parameters.AddWithValue("@critical_resistance_rate", weapon.CriticalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@ignore_critical_rate", weapon.IgnoreCriticalRate);
                    updateCommand.Parameters.AddWithValue("@penetration_rate", weapon.PenetrationRate);
                    updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", weapon.PenetrationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@evasion_rate", weapon.EvasionRate);
                    updateCommand.Parameters.AddWithValue("@damage_absorption_rate", weapon.DamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", weapon.IgnoreDamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", weapon.AbsorbedDamageRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", weapon.VitalityRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", weapon.VitalityRegenerationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@accuracy_rate", weapon.AccuracyRate);
                    updateCommand.Parameters.AddWithValue("@lifesteal_rate", weapon.LifestealRate);
                    updateCommand.Parameters.AddWithValue("@shield_strength", weapon.ShieldStrength);
                    updateCommand.Parameters.AddWithValue("@tenacity", weapon.Tenacity);
                    updateCommand.Parameters.AddWithValue("@resistance_rate", weapon.ResistanceRate);
                    updateCommand.Parameters.AddWithValue("@combo_rate", weapon.ComboRate);
                    updateCommand.Parameters.AddWithValue("@ignore_combo_rate", weapon.IgnoreComboRate);
                    updateCommand.Parameters.AddWithValue("@combo_damage_rate", weapon.ComboDamageRate);
                    updateCommand.Parameters.AddWithValue("@combo_resistance_rate", weapon.ComboResistanceRate);
                    updateCommand.Parameters.AddWithValue("@stun_rate", weapon.StunRate);
                    updateCommand.Parameters.AddWithValue("@ignore_stun_rate", weapon.IgnoreStunRate);
                    updateCommand.Parameters.AddWithValue("@reflection_rate", weapon.ReflectionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", weapon.IgnoreReflectionRate);
                    updateCommand.Parameters.AddWithValue("@reflection_damage_rate", weapon.ReflectionDamageRate);
                    updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", weapon.ReflectionResistanceRate);
                    updateCommand.Parameters.AddWithValue("@mana", weapon.Mana);
                    updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", weapon.ManaRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", weapon.DamageToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", weapon.ResistanceToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", weapon.DamageToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", weapon.ResistanceToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@normal_damage_rate", weapon.NormalDamageRate);
                    updateCommand.Parameters.AddWithValue("@normal_resistance_rate", weapon.NormalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@skill_damage_rate", weapon.SkillDamageRate);
                    updateCommand.Parameters.AddWithValue("@skill_resistance_rate", weapon.SkillResistanceRate);

                    await updateCommand.ExecuteNonQueryAsync();
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
    public async Task<Weapons> GetUserWeaponByIdAsync(string userId, string Id)
    {
        Weapons weapon = new Weapons();
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string selectSQL = @"Select * from user_weapons where user_weapons.weapon_id=@id 
                and user_weapons.user_id=@user_id";
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@id", Id);
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            weapon = new Weapons
                            {
                                Id = reader.GetStringSafe("weapon_id"),
                                Level = reader.GetIntSafe("level"),
                                Quality = reader.GetDoubleSafe("quality"),
                                Experience = reader.GetDoubleSafe("experience"),
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
                                SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate"),
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
        return weapon;
    }
    public async Task<Weapons> SumPowerUserWeaponsAsync(string userId)
    {
        Weapons sumWeapons = new Weapons();
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string selectSQL = @"SELECT 
                -- Tính SUM trực tiếp áp dụng Quality, Star (min = 1) và Level (min = 1)
                SUM(uc.health * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS health,
                SUM(uc.physical_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS physical_attack,
                SUM(uc.physical_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS physical_defense,
                SUM(uc.magical_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS magical_attack,
                SUM(uc.magical_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS magical_defense,
                SUM(uc.chemical_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS chemical_attack,
                SUM(uc.chemical_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS chemical_defense,
                SUM(uc.atomic_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS atomic_attack,
                SUM(uc.atomic_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS atomic_defense,
                SUM(uc.mental_attack * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS mental_attack,
                SUM(uc.mental_defense * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS mental_defense,
                SUM(uc.speed * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS speed,
                SUM(uc.critical_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS critical_damage_rate,
                SUM(uc.critical_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS critical_rate,
                SUM(uc.critical_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS critical_resistance_rate,
                SUM(uc.ignore_critical_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_critical_rate,
                SUM(uc.penetration_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS penetration_rate,
                SUM(uc.penetration_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS penetration_resistance_rate,
                SUM(uc.evasion_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS evasion_rate,
                SUM(uc.damage_absorption_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS damage_absorption_rate,
                SUM(uc.ignore_damage_absorption_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_damage_absorption_rate,
                SUM(uc.absorbed_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS absorbed_damage_rate,
                SUM(uc.vitality_regeneration_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS vitality_regeneration_rate,
                SUM(uc.vitality_regeneration_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS vitality_regeneration_resistance_rate,
                SUM(uc.accuracy_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS accuracy_rate,
                SUM(uc.lifesteal_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS lifesteal_rate,
                SUM(uc.shield_strength * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS shield_strength,
                SUM(uc.tenacity * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS tenacity,
                SUM(uc.resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS resistance_rate,
                SUM(uc.combo_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS combo_rate,
                SUM(uc.ignore_combo_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_combo_rate,
                SUM(uc.combo_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS combo_damage_rate,
                SUM(uc.combo_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS combo_resistance_rate,
                SUM(uc.stun_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS stun_rate,
                SUM(uc.ignore_stun_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_stun_rate,
                SUM(uc.reflection_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS reflection_rate,
                SUM(uc.ignore_reflection_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS ignore_reflection_rate,
                SUM(uc.reflection_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS reflection_damage_rate,
                SUM(uc.reflection_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS reflection_resistance_rate,
                SUM(uc.mana * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS mana,
                SUM(uc.mana_regeneration_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS mana_regeneration_rate,
                SUM(uc.damage_to_different_faction_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS damage_to_different_faction_rate,
                SUM(uc.resistance_to_different_faction_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS resistance_to_different_faction_rate,
                SUM(uc.damage_to_same_faction_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS damage_to_same_faction_rate,
                SUM(uc.resistance_to_same_faction_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS resistance_to_same_faction_rate,
                SUM(uc.normal_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS normal_damage_rate,
                SUM(uc.normal_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS normal_resistance_rate,
                SUM(uc.skill_damage_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS skill_damage_rate,
                SUM(uc.skill_resistance_rate * (1 + uc.quality / 10.0) * GREATEST(uc.star, 1) * (1 + GREATEST(uc.level, 1) / 100.0)) AS skill_resistance_rate
            FROM user_weapons uc
            WHERE user_id = @user_id;
            ";
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumWeapons.Health = reader.GetDoubleSafe("health");
                            sumWeapons.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            sumWeapons.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            sumWeapons.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            sumWeapons.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            sumWeapons.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            sumWeapons.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            sumWeapons.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            sumWeapons.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            sumWeapons.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            sumWeapons.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            sumWeapons.Speed = reader.GetDoubleSafe("speed");
                            sumWeapons.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            sumWeapons.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            sumWeapons.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            sumWeapons.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            sumWeapons.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            sumWeapons.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            sumWeapons.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            sumWeapons.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            sumWeapons.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            sumWeapons.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            sumWeapons.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            sumWeapons.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            sumWeapons.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            sumWeapons.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            sumWeapons.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            sumWeapons.Tenacity = reader.GetDoubleSafe("tenacity");
                            sumWeapons.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            sumWeapons.ComboRate = reader.GetDoubleSafe("combo_rate");
                            sumWeapons.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            sumWeapons.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            sumWeapons.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            sumWeapons.StunRate = reader.GetDoubleSafe("stun_rate");
                            sumWeapons.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            sumWeapons.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            sumWeapons.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            sumWeapons.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            sumWeapons.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            sumWeapons.Mana = reader.GetDoubleSafe("mana");
                            sumWeapons.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            sumWeapons.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            sumWeapons.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            sumWeapons.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            sumWeapons.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            sumWeapons.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            sumWeapons.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            sumWeapons.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            sumWeapons.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
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
        return sumWeapons;
    }
}