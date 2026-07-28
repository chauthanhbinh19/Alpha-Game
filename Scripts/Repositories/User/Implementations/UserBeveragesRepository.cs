using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserBeveragesRepository : IUserBeveragesRepository
{
    public async Task<List<Beverages>> GetUserBeveragesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Beverages> beverages = new List<Beverages>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT ut.*, t.id, t.name, t.image, t.rare, t.description
                FROM Beverages t
                INNER JOIN user_beverages ut ON t.id = ut.beverage_id
                WHERE ut.user_id = @userId";

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND t.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND t.name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += @" LIMIT @limit OFFSET @offset";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);

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
                            Beverages Beverage = new Beverages
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rarity = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
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

                            beverages.Add(Beverage);
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

        return beverages;
    }
    public async Task<int> GetUserBeveragesCountAsync(string userId, string search, string rare)
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
                FROM Beverages t
                INNER JOIN user_beverages ut ON t.id = ut.beverage_id
                WHERE ut.user_id = @userId";

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
    public async Task<bool> InsertUserBeverageAsync(Beverages beverage, string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem bản ghi đã tồn tại chưa
                string checkSQL = @"
                SELECT COUNT(*) FROM user_beverages 
                WHERE user_id = @user_id AND beverage_id = @beverage_id;";

                await using (MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@beverage_id", beverage.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        string insertSQL = @"
                        INSERT INTO user_beverages (
                            user_id, beverage_id, rare, level, experience, star, quality, block, quantity,
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
                            @user_id, @beverage_id, @rare, @level, @experience, @star, @quality, @block, @quantity,
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
                            insertCommand.Parameters.AddWithValue("@beverage_id", beverage.Id);
                            insertCommand.Parameters.AddWithValue("@rare", beverage.Rarity);
                            insertCommand.Parameters.AddWithValue("@level", 0);
                            insertCommand.Parameters.AddWithValue("@experience", 0);
                            insertCommand.Parameters.AddWithValue("@star", 0);
                            insertCommand.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(beverage.Rarity));
                            insertCommand.Parameters.AddWithValue("@block", false);
                            insertCommand.Parameters.AddWithValue("@quantity", beverage.Quantity);
                            insertCommand.Parameters.AddWithValue("@power", beverage.Power);
                            insertCommand.Parameters.AddWithValue("@health", beverage.Health);
                            insertCommand.Parameters.AddWithValue("@physical_attack", beverage.PhysicalAttack);
                            insertCommand.Parameters.AddWithValue("@physical_defense", beverage.PhysicalDefense);
                            insertCommand.Parameters.AddWithValue("@magical_attack", beverage.MagicalAttack);
                            insertCommand.Parameters.AddWithValue("@magical_defense", beverage.MagicalDefense);
                            insertCommand.Parameters.AddWithValue("@chemical_attack", beverage.ChemicalAttack);
                            insertCommand.Parameters.AddWithValue("@chemical_defense", beverage.ChemicalDefense);
                            insertCommand.Parameters.AddWithValue("@atomic_attack", beverage.AtomicAttack);
                            insertCommand.Parameters.AddWithValue("@atomic_defense", beverage.AtomicDefense);
                            insertCommand.Parameters.AddWithValue("@mental_attack", beverage.MentalAttack);
                            insertCommand.Parameters.AddWithValue("@mental_defense", beverage.MentalDefense);
                            insertCommand.Parameters.AddWithValue("@speed", beverage.Speed);
                            insertCommand.Parameters.AddWithValue("@critical_damage_rate", beverage.CriticalDamageRate);
                            insertCommand.Parameters.AddWithValue("@critical_rate", beverage.CriticalRate);
                            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", beverage.CriticalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", beverage.IgnoreCriticalRate);
                            insertCommand.Parameters.AddWithValue("@penetration_rate", beverage.PenetrationRate);
                            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", beverage.PenetrationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@evasion_rate", beverage.EvasionRate);
                            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", beverage.DamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", beverage.IgnoreDamageAbsorptionRate);
                            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", beverage.AbsorbedDamageRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", beverage.VitalityRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", beverage.VitalityRegenerationResistanceRate);
                            insertCommand.Parameters.AddWithValue("@accuracy_rate", beverage.AccuracyRate);
                            insertCommand.Parameters.AddWithValue("@lifesteal_rate", beverage.LifestealRate);
                            insertCommand.Parameters.AddWithValue("@shield_strength", beverage.ShieldStrength);
                            insertCommand.Parameters.AddWithValue("@tenacity", beverage.Tenacity);
                            insertCommand.Parameters.AddWithValue("@resistance_rate", beverage.ResistanceRate);
                            insertCommand.Parameters.AddWithValue("@combo_rate", beverage.ComboRate);
                            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", beverage.IgnoreComboRate);
                            insertCommand.Parameters.AddWithValue("@combo_damage_rate", beverage.ComboDamageRate);
                            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", beverage.ComboResistanceRate);
                            insertCommand.Parameters.AddWithValue("@stun_rate", beverage.StunRate);
                            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", beverage.IgnoreStunRate);
                            insertCommand.Parameters.AddWithValue("@reflection_rate", beverage.ReflectionRate);
                            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", beverage.IgnoreReflectionRate);
                            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", beverage.ReflectionDamageRate);
                            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", beverage.ReflectionResistanceRate);
                            insertCommand.Parameters.AddWithValue("@mana", beverage.Mana);
                            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", beverage.ManaRegenerationRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", beverage.DamageToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", beverage.ResistanceToDifferentFactionRate);
                            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", beverage.DamageToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", beverage.ResistanceToSameFactionRate);
                            insertCommand.Parameters.AddWithValue("@normal_damage_rate", beverage.NormalDamageRate);
                            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", beverage.NormalResistanceRate);
                            insertCommand.Parameters.AddWithValue("@skill_damage_rate", beverage.SkillDamageRate);
                            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", beverage.SkillResistanceRate);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Nếu bản ghi đã tồn tại, thực hiện UPDATE
                        string updateSQL = @"
                        UPDATE user_beverages
                        SET quantity = @quantity
                        WHERE user_id = @user_id AND beverage_id = @beverage_id;";

                        await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                        {
                            updateCommand.Parameters.AddWithValue("@user_id", userId);
                            updateCommand.Parameters.AddWithValue("@beverage_id", beverage.Id);
                            updateCommand.Parameters.AddWithValue("@quantity", beverage.Quantity);

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
    public async Task<bool> InsertOrUpdateUserBeveragesBatchAsync(string userId, List<Beverages> beverages)
    {
        if (beverages == null || beverages.Count == 0)
            return true;

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 500; // vì nhiều column → giảm size

            for (int i = 0; i < beverages.Count; i += batchSize)
            {
                var batch = beverages.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
                INSERT INTO user_beverages (
                    user_id, beverage_id, rare, level, experience, star, quality, block, quantity,
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
                    (@user_id, @beverage_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
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
                        new MySqlParameter($"@beverage_id_{j}", c.Id),
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
                    quantity = COALESCE(user_beverages.quantity, 0) + VALUES(quantity);
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
    public async Task<bool> UpdateUserBeverageLevelAsync(string userId, Beverages beverage)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string updateSQL = @"
                UPDATE user_beverages
                SET 
                    level = @level, experience = @experience
                WHERE user_id = @user_id AND beverage_id = @beverage_id;";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@beverage_id", beverage.Id);
                    updateCommand.Parameters.AddWithValue("@level", beverage.Level);
                    updateCommand.Parameters.AddWithValue("@experience", beverage.Experience);

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
    public async Task<bool> UpdateUserBeverageStarAsync(string userId, Beverages beverage)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"
                UPDATE user_beverages
                SET 
                    star = @star, quantity = @quantity
                WHERE user_id = @user_id AND beverage_id = @beverage_id;
            ";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@beverage_id", beverage.Id);
                    updateCommand.Parameters.AddWithValue("@star", beverage.Star);
                    updateCommand.Parameters.AddWithValue("@quantity", beverage.Quantity);

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
    public async Task<bool> UpdateUserBeverageBreakthroughAsync(string userId, Beverages beverage, int star, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string updateSQL = @"
                UPDATE user_beverages
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
                WHERE user_id = @user_id AND beverage_id = @beverage_id;";
                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@beverage_id", beverage.Id);
                    updateCommand.Parameters.AddWithValue("@star", star);
                    updateCommand.Parameters.AddWithValue("@quantity", quantity);
                    updateCommand.Parameters.AddWithValue("@power", beverage.Power);
                    updateCommand.Parameters.AddWithValue("@health", beverage.Health);
                    updateCommand.Parameters.AddWithValue("@physical_attack", beverage.PhysicalAttack);
                    updateCommand.Parameters.AddWithValue("@physical_defense", beverage.PhysicalDefense);
                    updateCommand.Parameters.AddWithValue("@magical_attack", beverage.MagicalAttack);
                    updateCommand.Parameters.AddWithValue("@magical_defense", beverage.MagicalDefense);
                    updateCommand.Parameters.AddWithValue("@chemical_attack", beverage.ChemicalAttack);
                    updateCommand.Parameters.AddWithValue("@chemical_defense", beverage.ChemicalDefense);
                    updateCommand.Parameters.AddWithValue("@atomic_attack", beverage.AtomicAttack);
                    updateCommand.Parameters.AddWithValue("@atomic_defense", beverage.AtomicDefense);
                    updateCommand.Parameters.AddWithValue("@mental_attack", beverage.MentalAttack);
                    updateCommand.Parameters.AddWithValue("@mental_defense", beverage.MentalDefense);
                    updateCommand.Parameters.AddWithValue("@speed", beverage.Speed);
                    updateCommand.Parameters.AddWithValue("@critical_damage_rate", beverage.CriticalDamageRate);
                    updateCommand.Parameters.AddWithValue("@critical_rate", beverage.CriticalRate);
                    updateCommand.Parameters.AddWithValue("@critical_resistance_rate", beverage.CriticalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@ignore_critical_rate", beverage.IgnoreCriticalRate);
                    updateCommand.Parameters.AddWithValue("@penetration_rate", beverage.PenetrationRate);
                    updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", beverage.PenetrationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@evasion_rate", beverage.EvasionRate);
                    updateCommand.Parameters.AddWithValue("@damage_absorption_rate", beverage.DamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", beverage.IgnoreDamageAbsorptionRate);
                    updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", beverage.AbsorbedDamageRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", beverage.VitalityRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", beverage.VitalityRegenerationResistanceRate);
                    updateCommand.Parameters.AddWithValue("@accuracy_rate", beverage.AccuracyRate);
                    updateCommand.Parameters.AddWithValue("@lifesteal_rate", beverage.LifestealRate);
                    updateCommand.Parameters.AddWithValue("@shield_strength", beverage.ShieldStrength);
                    updateCommand.Parameters.AddWithValue("@tenacity", beverage.Tenacity);
                    updateCommand.Parameters.AddWithValue("@resistance_rate", beverage.ResistanceRate);
                    updateCommand.Parameters.AddWithValue("@combo_rate", beverage.ComboRate);
                    updateCommand.Parameters.AddWithValue("@ignore_combo_rate", beverage.IgnoreComboRate);
                    updateCommand.Parameters.AddWithValue("@combo_damage_rate", beverage.ComboDamageRate);
                    updateCommand.Parameters.AddWithValue("@combo_resistance_rate", beverage.ComboResistanceRate);
                    updateCommand.Parameters.AddWithValue("@stun_rate", beverage.StunRate);
                    updateCommand.Parameters.AddWithValue("@ignore_stun_rate", beverage.IgnoreStunRate);
                    updateCommand.Parameters.AddWithValue("@reflection_rate", beverage.ReflectionRate);
                    updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", beverage.IgnoreReflectionRate);
                    updateCommand.Parameters.AddWithValue("@reflection_damage_rate", beverage.ReflectionDamageRate);
                    updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", beverage.ReflectionResistanceRate);
                    updateCommand.Parameters.AddWithValue("@mana", beverage.Mana);
                    updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", beverage.ManaRegenerationRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", beverage.DamageToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", beverage.ResistanceToDifferentFactionRate);
                    updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", beverage.DamageToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", beverage.ResistanceToSameFactionRate);
                    updateCommand.Parameters.AddWithValue("@normal_damage_rate", beverage.NormalDamageRate);
                    updateCommand.Parameters.AddWithValue("@normal_resistance_rate", beverage.NormalResistanceRate);
                    updateCommand.Parameters.AddWithValue("@skill_damage_rate", beverage.SkillDamageRate);
                    updateCommand.Parameters.AddWithValue("@skill_resistance_rate", beverage.SkillResistanceRate);

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
    public async Task<Beverages> GetUserBeverageByIdAsync(string userId, string Id)
    {
        Beverages beverage = new Beverages();
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string selectSQL = @"Select * from user_beverages where user_beverages.beverage_id=@id 
                and user_beverages.user_id=@user_id";
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@id", Id);
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            beverage = new Beverages
                            {
                                Id = reader.GetStringSafe("beverage_id"),
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
        return beverage;
    }
    public async Task<Beverages> SumPowerUserBeveragesAsync(string userId)
    {
        Beverages sumBeverages = new Beverages();
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
            FROM user_beverages uc
            WHERE user_id = @user_id;
            ";
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumBeverages.Health = reader.GetDoubleSafe("health");
                            sumBeverages.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            sumBeverages.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            sumBeverages.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            sumBeverages.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            sumBeverages.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            sumBeverages.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            sumBeverages.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            sumBeverages.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            sumBeverages.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            sumBeverages.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            sumBeverages.Speed = reader.GetDoubleSafe("speed");
                            sumBeverages.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            sumBeverages.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            sumBeverages.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            sumBeverages.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            sumBeverages.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            sumBeverages.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            sumBeverages.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            sumBeverages.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            sumBeverages.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            sumBeverages.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            sumBeverages.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            sumBeverages.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            sumBeverages.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            sumBeverages.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            sumBeverages.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            sumBeverages.Tenacity = reader.GetDoubleSafe("tenacity");
                            sumBeverages.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            sumBeverages.ComboRate = reader.GetDoubleSafe("combo_rate");
                            sumBeverages.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            sumBeverages.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            sumBeverages.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            sumBeverages.StunRate = reader.GetDoubleSafe("stun_rate");
                            sumBeverages.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            sumBeverages.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            sumBeverages.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            sumBeverages.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            sumBeverages.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            sumBeverages.Mana = reader.GetDoubleSafe("mana");
                            sumBeverages.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            sumBeverages.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            sumBeverages.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            sumBeverages.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            sumBeverages.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            sumBeverages.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            sumBeverages.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            sumBeverages.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            sumBeverages.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
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
        return sumBeverages;
    }
}