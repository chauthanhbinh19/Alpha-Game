using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserRelicsRepository : IUserRelicsRepository
{
    public async Task<List<Relics>> GetUserRelicsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Relics> relics = new List<Relics>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT um.*, m.id, m.name, m.image, m.rare, m.type, m.description 
                FROM relics m
                JOIN user_relics um ON m.id = um.relic_id
                WHERE um.user_id = @userId 
            ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND m.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND m.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND m.name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += " LIMIT @limit OFFSET @offset";

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
                            Relics relic = new Relics
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

                            relics.Add(relic);
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

        return relics;
    }
    public async Task<int> GetUserRelicsCountAsync(string userId, string search, string type, string rare)
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
                FROM relics m
                JOIN user_relics um ON m.id = um.relic_id
                WHERE um.user_id = @userId 
            ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND m.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND m.rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND m.name LIKE CONCAT('%', @search, '%')";
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
    public async Task<InsertOrUpdateResult<Relics>> InsertOrUpdateUserRelicAsync(string userId, Relics relic)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Query thực hiện Insert hoặc Update nếu đã tồn tại Composite Primary Key (user_id, relic_id)
            string upsertSQL = @"
            INSERT INTO user_relics (
                user_id, relic_id, rare, level, experience, star, quality, block, quantity,
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
                @user_id, @relic_id, @rare, 0, 0, 0, @quality, false, @quantity,
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
            command.Parameters.AddWithValue("@relic_id", relic.Id);
            command.Parameters.AddWithValue("@rare", relic.Rarity);
            command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(relic.Rarity));
            command.Parameters.AddWithValue("@quantity", relic.Quantity);
            command.Parameters.AddWithValue("@power", relic.Power);
            command.Parameters.AddWithValue("@health", relic.Health);
            command.Parameters.AddWithValue("@physical_attack", relic.PhysicalAttack);
            command.Parameters.AddWithValue("@physical_defense", relic.PhysicalDefense);
            command.Parameters.AddWithValue("@magical_attack", relic.MagicalAttack);
            command.Parameters.AddWithValue("@magical_defense", relic.MagicalDefense);
            command.Parameters.AddWithValue("@chemical_attack", relic.ChemicalAttack);
            command.Parameters.AddWithValue("@chemical_defense", relic.ChemicalDefense);
            command.Parameters.AddWithValue("@atomic_attack", relic.AtomicAttack);
            command.Parameters.AddWithValue("@atomic_defense", relic.AtomicDefense);
            command.Parameters.AddWithValue("@mental_attack", relic.MentalAttack);
            command.Parameters.AddWithValue("@mental_defense", relic.MentalDefense);
            command.Parameters.AddWithValue("@speed", relic.Speed);
            command.Parameters.AddWithValue("@critical_damage_rate", relic.CriticalDamageRate);
            command.Parameters.AddWithValue("@critical_rate", relic.CriticalRate);
            command.Parameters.AddWithValue("@critical_resistance_rate", relic.CriticalResistanceRate);
            command.Parameters.AddWithValue("@ignore_critical_rate", relic.IgnoreCriticalRate);
            command.Parameters.AddWithValue("@penetration_rate", relic.PenetrationRate);
            command.Parameters.AddWithValue("@penetration_resistance_rate", relic.PenetrationResistanceRate);
            command.Parameters.AddWithValue("@evasion_rate", relic.EvasionRate);
            command.Parameters.AddWithValue("@damage_absorption_rate", relic.DamageAbsorptionRate);
            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", relic.IgnoreDamageAbsorptionRate);
            command.Parameters.AddWithValue("@absorbed_damage_rate", relic.AbsorbedDamageRate);
            command.Parameters.AddWithValue("@vitality_regeneration_rate", relic.VitalityRegenerationRate);
            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", relic.VitalityRegenerationResistanceRate);
            command.Parameters.AddWithValue("@accuracy_rate", relic.AccuracyRate);
            command.Parameters.AddWithValue("@lifesteal_rate", relic.LifestealRate);
            command.Parameters.AddWithValue("@shield_strength", relic.ShieldStrength);
            command.Parameters.AddWithValue("@tenacity", relic.Tenacity);
            command.Parameters.AddWithValue("@resistance_rate", relic.ResistanceRate);
            command.Parameters.AddWithValue("@combo_rate", relic.ComboRate);
            command.Parameters.AddWithValue("@ignore_combo_rate", relic.IgnoreComboRate);
            command.Parameters.AddWithValue("@combo_damage_rate", relic.ComboDamageRate);
            command.Parameters.AddWithValue("@combo_resistance_rate", relic.ComboResistanceRate);
            command.Parameters.AddWithValue("@stun_rate", relic.StunRate);
            command.Parameters.AddWithValue("@ignore_stun_rate", relic.IgnoreStunRate);
            command.Parameters.AddWithValue("@reflection_rate", relic.ReflectionRate);
            command.Parameters.AddWithValue("@ignore_reflection_rate", relic.IgnoreReflectionRate);
            command.Parameters.AddWithValue("@reflection_damage_rate", relic.ReflectionDamageRate);
            command.Parameters.AddWithValue("@reflection_resistance_rate", relic.ReflectionResistanceRate);
            command.Parameters.AddWithValue("@mana", relic.Mana);
            command.Parameters.AddWithValue("@mana_regeneration_rate", relic.ManaRegenerationRate);
            command.Parameters.AddWithValue("@damage_to_different_faction_rate", relic.DamageToDifferentFactionRate);
            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", relic.ResistanceToDifferentFactionRate);
            command.Parameters.AddWithValue("@damage_to_same_faction_rate", relic.DamageToSameFactionRate);
            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", relic.ResistanceToSameFactionRate);
            command.Parameters.AddWithValue("@normal_damage_rate", relic.NormalDamageRate);
            command.Parameters.AddWithValue("@normal_resistance_rate", relic.NormalResistanceRate);
            command.Parameters.AddWithValue("@skill_damage_rate", relic.SkillDamageRate);
            command.Parameters.AddWithValue("@skill_resistance_rate", relic.SkillResistanceRate);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            // MySQL quy ước: Insert mới = 1, Update = 2, Không thay đổi = 0
            if (rowsAffected == 1)
            {
                return InsertOrUpdateResult<Relics>.Inserted(relic);
            }
            else if (rowsAffected == 2 || rowsAffected == 0)
            {
                return InsertOrUpdateResult<Relics>.Updated(relic);
            }

            return InsertOrUpdateResult<Relics>.Failure();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Database Error: " + ex.Message);
            return InsertOrUpdateResult<Relics>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<BatchOperationResultDTO<Relics>>> InsertOrUpdateUserRelicsBatchAsync(
    string userId, List<Relics> relics)
    {
        if (relics == null || relics.Count == 0)
        {
            return new InsertOrUpdateResult<BatchOperationResultDTO<Relics>>
            {
                Data = new BatchOperationResultDTO<Relics>(),
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // 1. Query lấy TOÀN BỘ relic_id hiện có của User (Cực nhanh nhờ Index user_id)
            var existingIds = new HashSet<string>();
            string checkSql = "SELECT relic_id FROM user_relics WHERE user_id = @user_id;";

            await using (var checkCmd = new MySqlCommand(checkSql, connection))
            {
                checkCmd.Parameters.AddWithValue("@user_id", userId);
                await using var reader = await checkCmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    existingIds.Add(reader.GetString(0));
                }
            }

            // 2. Phân loại Relics giữ NGUYÊN VẸN OBJECT thuộc tính trong RAM C#
            var batchResult = new BatchOperationResultDTO<Relics>();
            foreach (var card in relics)
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

            for (int i = 0; i < relics.Count; i += batchSize)
            {
                var batch = relics.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
            INSERT INTO user_relics (
                user_id, relic_id, rare, level, experience, star, quality, block, quantity,
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
                (@user_id, @relic_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
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
                    new MySqlParameter($"@relic_id_{j}", c.Id),
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
                quantity = COALESCE(user_relics.quantity, 0) + VALUES(quantity);
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

            return new InsertOrUpdateResult<BatchOperationResultDTO<Relics>>
            {
                Data = batchResult,
                OperationType = operationType
            };
        }
        catch (Exception ex)
        {
            Debug.LogError("Batch Error: " + ex.Message);
            return InsertOrUpdateResult<BatchOperationResultDTO<Relics>>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateUserRelicLevelAsync(string userId, Relics relic)
    {
        if (relic == null)
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
            UPDATE user_relics
            SET 
                level = @level, 
                experience = @experience
            WHERE user_id = @user_id 
              AND relic_id = @relic_id
              AND (level != @level OR experience != @experience);
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@relic_id", relic.Id);
            updateCommand.Parameters.AddWithValue("@level", relic.Level);
            updateCommand.Parameters.AddWithValue("@experience", relic.Experience);

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
            Debug.LogError("Error UpdateUserRelicLevel: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateUserRelicStarAsync(string userId, Relics relic)
    {
        if (relic == null)
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
            UPDATE user_relics
            SET 
                star = @star, 
                quantity = @quantity
            WHERE user_id = @user_id 
              AND relic_id = @relic_id
              AND (star != @star OR quantity != @quantity);
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@relic_id", relic.Id);
            updateCommand.Parameters.AddWithValue("@star", relic.Star);
            updateCommand.Parameters.AddWithValue("@quantity", relic.Quantity);

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
            Debug.LogError("Error UpdateUserRelicStar: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<Relics> GetUserRelicByIdAsync(string userId, string Id)
    {
        Relics relic = null;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT * FROM user_Relics 
                             WHERE relic_id=@id AND user_id=@user_id";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@id", Id);
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    await using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            relic = new Relics
                            {
                                Id = reader.GetStringSafe("relic_id"),
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

        return relic;
    }
    public async Task<Relics> SumPowerUserRelicsAsync(string userId)
    {
        Relics sumRelics = new Relics();
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
                        (
                            -- Quality: 0 -> 1.0, 1 -> 1.1
                            (1 + COALESCE(uc.quality, 0) / 10.0) 
                            
                            -- Star: 0 -> 1.0, 1 -> 2.0, 2 -> 3.0
                            * (1 + COALESCE(uc.star, 0)) 
                            
                            -- Level: 0 -> 1.0, 10 -> 1.1
                            * (1 + COALESCE(uc.level, 0) / 100.0) 
                            
                            -- Module: 0/NULL -> 1.0
                            * (1 + COALESCE(ubm.current_multiplier, 0) / 100.0) 
                            
                            -- Upgrade: 0/NULL -> 1.0
                            * (1 + COALESCE(ubu.current_multiplier, 0) / 100.0)
                        ) AS total_multiplier
                    FROM user_relics uc
                    LEFT JOIN user_relics_module ubm ON uc.relic_id = ubm.user_relic_id
                    LEFT JOIN user_relics_upgrade ubu ON uc.relic_id = ubu.user_relic_id
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
                FROM CalculatedObjects;";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    await using (var reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumRelics.Health = reader.GetDoubleSafe("health");
                            sumRelics.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            sumRelics.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            sumRelics.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            sumRelics.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            sumRelics.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            sumRelics.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            sumRelics.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            sumRelics.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            sumRelics.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            sumRelics.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            sumRelics.Speed = reader.GetDoubleSafe("speed");
                            sumRelics.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            sumRelics.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            sumRelics.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            sumRelics.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            sumRelics.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            sumRelics.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            sumRelics.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            sumRelics.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            sumRelics.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            sumRelics.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            sumRelics.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            sumRelics.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            sumRelics.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            sumRelics.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            sumRelics.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            sumRelics.Tenacity = reader.GetDoubleSafe("tenacity");
                            sumRelics.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            sumRelics.ComboRate = reader.GetDoubleSafe("combo_rate");
                            sumRelics.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            sumRelics.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            sumRelics.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            sumRelics.StunRate = reader.GetDoubleSafe("stun_rate");
                            sumRelics.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            sumRelics.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            sumRelics.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            sumRelics.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            sumRelics.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            sumRelics.Mana = reader.GetDoubleSafe("mana");
                            sumRelics.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            sumRelics.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            sumRelics.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            sumRelics.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            sumRelics.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            sumRelics.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            sumRelics.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            sumRelics.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            sumRelics.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
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

        return sumRelics;
    }
}