using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserBadgesRepository : IUserBadgesRepository
{
    public async Task<List<Badges>> GetUserBadgesAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Badges> badges = new List<Badges>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT ut.*, t.id, t.name, t.image, t.rare, t.description 
                FROM Badges t
                JOIN user_badges ut ON t.id = ut.badge_id
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
                            Badges badge = new Badges
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

                            badges.Add(badge);
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

        return badges;
    }
    public async Task<int> GetUserBadgesCountAsync(string userId, string search, string rare)
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
                    FROM Badges t
                    JOIN user_badges ut ON t.id = ut.badge_id
                    WHERE ut.user_id = @userId 
                ";
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
                await connection.CloseAsync(); // vẫn giữ close
            }
        }

        return count;
    }
    public async Task<InsertOrUpdateResult<Badges>> InsertOrUpdateUserBadgeAsync(string userId, Badges badge)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Query thực hiện Insert hoặc Update nếu đã tồn tại Composite Primary Key (user_id, badge_id)
            string upsertSQL = @"
            INSERT INTO user_badges (
                user_id, badge_id, rare, level, experience, star, quality, block, quantity,
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
                @user_id, @badge_id, @rare, 0, 0, 0, @quality, false, @quantity,
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
            )
            ON DUPLICATE KEY UPDATE 
                quantity = VALUES(quantity);";

            await using MySqlCommand command = new MySqlCommand(upsertSQL, connection);

            // Add Parameters
            command.Parameters.AddWithValue("@user_id", userId);
            command.Parameters.AddWithValue("@badge_id", badge.Id);
            command.Parameters.AddWithValue("@rare", badge.Rarity);
            command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(badge.Rarity));
            command.Parameters.AddWithValue("@quantity", badge.Quantity);
            command.Parameters.AddWithValue("@power", badge.Power);
            command.Parameters.AddWithValue("@health", badge.Health);
            command.Parameters.AddWithValue("@physical_attack", badge.PhysicalAttack);
            command.Parameters.AddWithValue("@physical_defense", badge.PhysicalDefense);
            command.Parameters.AddWithValue("@magical_attack", badge.MagicalAttack);
            command.Parameters.AddWithValue("@magical_defense", badge.MagicalDefense);
            command.Parameters.AddWithValue("@chemical_attack", badge.ChemicalAttack);
            command.Parameters.AddWithValue("@chemical_defense", badge.ChemicalDefense);
            command.Parameters.AddWithValue("@atomic_attack", badge.AtomicAttack);
            command.Parameters.AddWithValue("@atomic_defense", badge.AtomicDefense);
            command.Parameters.AddWithValue("@mental_attack", badge.MentalAttack);
            command.Parameters.AddWithValue("@mental_defense", badge.MentalDefense);
            command.Parameters.AddWithValue("@speed", badge.Speed);
            command.Parameters.AddWithValue("@critical_damage_rate", badge.CriticalDamageRate);
            command.Parameters.AddWithValue("@critical_rate", badge.CriticalRate);
            command.Parameters.AddWithValue("@critical_resistance_rate", badge.CriticalResistanceRate);
            command.Parameters.AddWithValue("@ignore_critical_rate", badge.IgnoreCriticalRate);
            command.Parameters.AddWithValue("@penetration_rate", badge.PenetrationRate);
            command.Parameters.AddWithValue("@penetration_resistance_rate", badge.PenetrationResistanceRate);
            command.Parameters.AddWithValue("@evasion_rate", badge.EvasionRate);
            command.Parameters.AddWithValue("@damage_absorption_rate", badge.DamageAbsorptionRate);
            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", badge.IgnoreDamageAbsorptionRate);
            command.Parameters.AddWithValue("@absorbed_damage_rate", badge.AbsorbedDamageRate);
            command.Parameters.AddWithValue("@vitality_regeneration_rate", badge.VitalityRegenerationRate);
            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", badge.VitalityRegenerationResistanceRate);
            command.Parameters.AddWithValue("@accuracy_rate", badge.AccuracyRate);
            command.Parameters.AddWithValue("@lifesteal_rate", badge.LifestealRate);
            command.Parameters.AddWithValue("@shield_strength", badge.ShieldStrength);
            command.Parameters.AddWithValue("@tenacity", badge.Tenacity);
            command.Parameters.AddWithValue("@resistance_rate", badge.ResistanceRate);
            command.Parameters.AddWithValue("@combo_rate", badge.ComboRate);
            command.Parameters.AddWithValue("@ignore_combo_rate", badge.IgnoreComboRate);
            command.Parameters.AddWithValue("@combo_damage_rate", badge.ComboDamageRate);
            command.Parameters.AddWithValue("@combo_resistance_rate", badge.ComboResistanceRate);
            command.Parameters.AddWithValue("@stun_rate", badge.StunRate);
            command.Parameters.AddWithValue("@ignore_stun_rate", badge.IgnoreStunRate);
            command.Parameters.AddWithValue("@reflection_rate", badge.ReflectionRate);
            command.Parameters.AddWithValue("@ignore_reflection_rate", badge.IgnoreReflectionRate);
            command.Parameters.AddWithValue("@reflection_damage_rate", badge.ReflectionDamageRate);
            command.Parameters.AddWithValue("@reflection_resistance_rate", badge.ReflectionResistanceRate);
            command.Parameters.AddWithValue("@mana", badge.Mana);
            command.Parameters.AddWithValue("@mana_regeneration_rate", badge.ManaRegenerationRate);
            command.Parameters.AddWithValue("@damage_to_different_faction_rate", badge.DamageToDifferentFactionRate);
            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", badge.ResistanceToDifferentFactionRate);
            command.Parameters.AddWithValue("@damage_to_same_faction_rate", badge.DamageToSameFactionRate);
            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", badge.ResistanceToSameFactionRate);
            command.Parameters.AddWithValue("@normal_damage_rate", badge.NormalDamageRate);
            command.Parameters.AddWithValue("@normal_resistance_rate", badge.NormalResistanceRate);
            command.Parameters.AddWithValue("@skill_damage_rate", badge.SkillDamageRate);
            command.Parameters.AddWithValue("@skill_resistance_rate", badge.SkillResistanceRate);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            // MySQL quy ước: Insert mới = 1, Update = 2, Không thay đổi = 0
            if (rowsAffected == 1)
            {
                return InsertOrUpdateResult<Badges>.Inserted(badge);
            }
            else if (rowsAffected == 2 || rowsAffected == 0)
            {
                return InsertOrUpdateResult<Badges>.Updated(badge);
            }

            return InsertOrUpdateResult<Badges>.Failure();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Database Error: " + ex.Message);
            return InsertOrUpdateResult<Badges>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<BatchOperationResultDTO<Badges>>> InsertOrUpdateUserBadgesBatchAsync(
    string userId, List<Badges> badges)
    {
        if (badges == null || badges.Count == 0)
        {
            return new InsertOrUpdateResult<BatchOperationResultDTO<Badges>>
            {
                Data = new BatchOperationResultDTO<Badges>(),
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // 1. Query lấy TOÀN BỘ badge_id hiện có của User (Cực nhanh nhờ Index user_id)
            var existingIds = new HashSet<string>();
            string checkSql = "SELECT badge_id FROM user_badges WHERE user_id = @user_id;";

            await using (var checkCmd = new MySqlCommand(checkSql, connection))
            {
                checkCmd.Parameters.AddWithValue("@user_id", userId);
                await using var reader = await checkCmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    existingIds.Add(reader.GetString(0));
                }
            }

            // 2. Phân loại Badges giữ NGUYÊN VẸN OBJECT thuộc tính trong RAM C#
            var batchResult = new BatchOperationResultDTO<Badges>();
            foreach (var card in badges)
            {
                if (existingIds.Contains(card.Id))
                {
                    batchResult.UpdatedItems.Add(card); // Trả về full object card
                }
                else
                {
                    batchResult.InsertedItems.Add(card); // Trả về full object card để dùng truyền sang Gallery
                }
            }

            // 3. Thực hiện Bulk Insert/Update
            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 500; // Giảm batchSize vì câu lệnh có nhiều cột

            for (int i = 0; i < badges.Count; i += batchSize)
            {
                var batch = badges.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
            INSERT INTO user_badges (
                user_id, badge_id, rare, level, experience, star, quality, block, quantity,
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
                (@user_id, @badge_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
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
                    new MySqlParameter($"@badge_id_{j}", c.Id),
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

                stringBuilder.Length--; // remove dấu phẩy thừa

                stringBuilder.Append(@"
            ON DUPLICATE KEY UPDATE
                quantity = COALESCE(user_badges.quantity, 0) + VALUES(quantity);
            ");

                await using var command = new MySqlCommand(stringBuilder.ToString(), connection, (MySqlTransaction)transaction);

                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddRange(parameters.ToArray());

                await command.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();

            // 4. Trả về kết quả
            var operationType = DatabaseOperationType.None;

            if (batchResult.InsertedItems.Count > 0 && batchResult.UpdatedItems.Count > 0)
            {
                operationType = DatabaseOperationType.Mixed;
            }
            else if (batchResult.InsertedItems.Count > 0)
            {
                operationType = DatabaseOperationType.Inserted;
            }
            else if (batchResult.UpdatedItems.Count > 0)
            {
                operationType = DatabaseOperationType.Updated;
            }

            return new InsertOrUpdateResult<BatchOperationResultDTO<Badges>>
            {
                Data = batchResult,
                OperationType = operationType
            };
        }
        catch (Exception ex)
        {
            Debug.LogError("Batch Error: " + ex.Message);
            return InsertOrUpdateResult<BatchOperationResultDTO<Badges>>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateUserBadgeLevelAsync(string userId, Badges badge)
    {
        if (badge == null)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Thêm điều kiện (level != @level OR experience != @experience) để tránh update thừa khi dữ liệu trùng khớp
            string updateSQL = @"
            UPDATE user_badges
            SET 
                level = @level, 
                experience = @experience
            WHERE user_id = @user_id 
              AND badge_id = @badge_id
              AND (level != @level OR experience != @experience);
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@badge_id", badge.Id);
            updateCommand.Parameters.AddWithValue("@level", badge.Level);
            updateCommand.Parameters.AddWithValue("@experience", badge.Experience);

            int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                return InsertOrUpdateResult<bool>.Updated(true);
            }
            else
            {
                return new InsertOrUpdateResult<bool>
                {
                    Data = false,
                    OperationType = DatabaseOperationType.None,
                    Message = MessageConstants.NOTHING_WAS_UPDATED
                };
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error UpdateUserBadgeLevel: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateUserBadgeStarAsync(string userId, Badges badge)
    {
        if (badge == null)
        {
            return new InsertOrUpdateResult<bool>
            {
                Data = false,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Kiểm tra (star != @star OR quantity != @quantity) để không tốn I/O nếu dữ liệu không đổi
            string updateSQL = @"
            UPDATE user_badges
            SET 
                star = @star, 
                quantity = @quantity
            WHERE user_id = @user_id 
              AND badge_id = @badge_id
              AND (star != @star OR quantity != @quantity);
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@badge_id", badge.Id);
            updateCommand.Parameters.AddWithValue("@star", badge.Star);
            updateCommand.Parameters.AddWithValue("@quantity", badge.Quantity);

            int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                return InsertOrUpdateResult<bool>.Updated(true);
            }
            else
            {
                return new InsertOrUpdateResult<bool>
                {
                    Data = false,
                    OperationType = DatabaseOperationType.None,
                    Message = MessageConstants.NOTHING_WAS_UPDATED
                };
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error UpdateUserBadgeStar: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<Badges> GetUserBadgeByIdAsync(string userId, string Id)
    {
        Badges badge = null;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT * FROM user_badges 
                             WHERE badge_id = @id AND user_id = @user_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@id", Id);
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    await using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            badge = new Badges
                            {
                                Id = reader.GetStringSafe("badge_id"),
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

        return badge;
    }
    public async Task<Badges> SumPowerUserBadgesAsync(string userId)
    {
        Badges sumBadges = new Badges();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                WITH CalculatedObjects AS (
                    SELECT 
                        uc.*,
                        -- TÍNH TOTAL MULTIPLIER CHO TỪNG OBJECT:
                        -- 1. Quality: (1 + quality / 10.0)
                        -- 2. Star: GREATEST(star, 1) -> star = 0 hay 1 đều nhân 1
                        -- 3. Level: (1 + GREATEST(level, 0) / 100.0) -> level <= 0 thì nhân 1.0
                        (
                            (1 + uc.quality / 10.0) 
                            * GREATEST(uc.star, 1) 
                            * (1 + GREATEST(uc.level, 0) / 100.0)
                        ) AS total_multiplier
                    FROM user_badges uc
                    WHERE uc.user_id = @user_id
                )
                SELECT 
                    SUM(health * total_multiplier) AS health,
                    SUM(physical_attack * total_multiplier) AS physical_attack,
                    SUM(physical_defense * total_multiplier) AS physical_defense,
                    SUM(magical_attack * total_multiplier) AS magical_attack,
                    SUM(magical_defense * total_multiplier) AS magical_defense,
                    SUM(chemical_attack * total_multiplier) AS chemical_attack,
                    SUM(chemical_defense * total_multiplier) AS chemical_defense,
                    SUM(atomic_attack * total_multiplier) AS atomic_attack,
                    SUM(atomic_defense * total_multiplier) AS atomic_defense,
                    SUM(mental_attack * total_multiplier) AS mental_attack,
                    SUM(mental_defense * total_multiplier) AS mental_defense,
                    SUM(speed * total_multiplier) AS speed,
                    SUM(critical_damage_rate * total_multiplier) AS critical_damage_rate,
                    SUM(critical_rate * total_multiplier) AS critical_rate,
                    SUM(critical_resistance_rate * total_multiplier) AS critical_resistance_rate,
                    SUM(ignore_critical_rate * total_multiplier) AS ignore_critical_rate,
                    SUM(penetration_rate * total_multiplier) AS penetration_rate,
                    SUM(penetration_resistance_rate * total_multiplier) AS penetration_resistance_rate,
                    SUM(evasion_rate * total_multiplier) AS evasion_rate,
                    SUM(damage_absorption_rate * total_multiplier) AS damage_absorption_rate,
                    SUM(ignore_damage_absorption_rate * total_multiplier) AS ignore_damage_absorption_rate,
                    SUM(absorbed_damage_rate * total_multiplier) AS absorbed_damage_rate,
                    SUM(vitality_regeneration_rate * total_multiplier) AS vitality_regeneration_rate,
                    SUM(vitality_regeneration_resistance_rate * total_multiplier) AS vitality_regeneration_resistance_rate,
                    SUM(accuracy_rate * total_multiplier) AS accuracy_rate,
                    SUM(lifesteal_rate * total_multiplier) AS lifesteal_rate,
                    SUM(shield_strength * total_multiplier) AS shield_strength,
                    SUM(tenacity * total_multiplier) AS tenacity,
                    SUM(resistance_rate * total_multiplier) AS resistance_rate,
                    SUM(combo_rate * total_multiplier) AS combo_rate,
                    SUM(ignore_combo_rate * total_multiplier) AS ignore_combo_rate,
                    SUM(combo_damage_rate * total_multiplier) AS combo_damage_rate,
                    SUM(combo_resistance_rate * total_multiplier) AS combo_resistance_rate,
                    SUM(stun_rate * total_multiplier) AS stun_rate,
                    SUM(ignore_stun_rate * total_multiplier) AS ignore_stun_rate,
                    SUM(reflection_rate * total_multiplier) AS reflection_rate,
                    SUM(ignore_reflection_rate * total_multiplier) AS ignore_reflection_rate,
                    SUM(reflection_damage_rate * total_multiplier) AS reflection_damage_rate,
                    SUM(reflection_resistance_rate * total_multiplier) AS reflection_resistance_rate,
                    SUM(mana * total_multiplier) AS mana,
                    SUM(mana_regeneration_rate * total_multiplier) AS mana_regeneration_rate,
                    SUM(damage_to_different_faction_rate * total_multiplier) AS damage_to_different_faction_rate,
                    SUM(resistance_to_different_faction_rate * total_multiplier) AS resistance_to_different_faction_rate,
                    SUM(damage_to_same_faction_rate * total_multiplier) AS damage_to_same_faction_rate,
                    SUM(resistance_to_same_faction_rate * total_multiplier) AS resistance_to_same_faction_rate,
                    SUM(normal_damage_rate * total_multiplier) AS normal_damage_rate,
                    SUM(normal_resistance_rate * total_multiplier) AS normal_resistance_rate,
                    SUM(skill_damage_rate * total_multiplier) AS skill_damage_rate,
                    SUM(skill_resistance_rate * total_multiplier) AS skill_resistance_rate
                FROM CalculatedObjects;
            ";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    await using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumBadges.Health = reader.GetDoubleSafe("health");
                            sumBadges.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            sumBadges.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            sumBadges.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            sumBadges.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            sumBadges.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            sumBadges.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            sumBadges.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            sumBadges.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            sumBadges.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            sumBadges.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            sumBadges.Speed = reader.GetDoubleSafe("speed");
                            sumBadges.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            sumBadges.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            sumBadges.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            sumBadges.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            sumBadges.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            sumBadges.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            sumBadges.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            sumBadges.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            sumBadges.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            sumBadges.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            sumBadges.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            sumBadges.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            sumBadges.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            sumBadges.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            sumBadges.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            sumBadges.Tenacity = reader.GetDoubleSafe("tenacity");
                            sumBadges.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            sumBadges.ComboRate = reader.GetDoubleSafe("combo_rate");
                            sumBadges.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            sumBadges.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            sumBadges.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            sumBadges.StunRate = reader.GetDoubleSafe("stun_rate");
                            sumBadges.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            sumBadges.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            sumBadges.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            sumBadges.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            sumBadges.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            sumBadges.Mana = reader.GetDoubleSafe("mana");
                            sumBadges.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            sumBadges.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            sumBadges.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            sumBadges.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            sumBadges.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            sumBadges.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            sumBadges.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            sumBadges.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            sumBadges.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
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

        return sumBadges;
    }
}