using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

public class UserOutfitsRepository : IUserOutfitsRepository
{
    public async Task<List<Outfits>> GetUserOutfitsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Outfits> outfits = new List<Outfits>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT ut.*, t.id, t.name, t.image, t.rare, t.type, t.description
                FROM Outfits t
                INNER JOIN user_outfits ut ON t.id = ut.outfit_id
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
                            Outfits outfit = new Outfits
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

                            outfits.Add(outfit);
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

        return outfits;
    }
    public async Task<int> GetUserOutfitsCountAsync(string userId, string search, string type, string rare)
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
                FROM Outfits t
                INNER JOIN user_outfits ut ON t.id = ut.outfit_id
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
    public async Task<InsertOrUpdateResult<Outfits>> InsertOrUpdateUserOutfitAsync(string userId, Outfits outfit)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Query thực hiện Insert hoặc Update nếu đã tồn tại Composite Primary Key (user_id, outfit_id)
            string upsertSQL = @"
            INSERT INTO user_outfits (
                user_id, outfit_id, rare, level, experience, star, quality, block, quantity,
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
                @user_id, @outfit_id, @rare, 0, 0, 0, @quality, false, @quantity,
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
            command.Parameters.AddWithValue("@outfit_id", outfit.Id);
            command.Parameters.AddWithValue("@rare", outfit.Rarity);
            command.Parameters.AddWithValue("@quality", QualityEvaluatorHelper.CheckQuality(outfit.Rarity));
            command.Parameters.AddWithValue("@quantity", outfit.Quantity);
            command.Parameters.AddWithValue("@power", outfit.Power);
            command.Parameters.AddWithValue("@health", outfit.Health);
            command.Parameters.AddWithValue("@physical_attack", outfit.PhysicalAttack);
            command.Parameters.AddWithValue("@physical_defense", outfit.PhysicalDefense);
            command.Parameters.AddWithValue("@magical_attack", outfit.MagicalAttack);
            command.Parameters.AddWithValue("@magical_defense", outfit.MagicalDefense);
            command.Parameters.AddWithValue("@chemical_attack", outfit.ChemicalAttack);
            command.Parameters.AddWithValue("@chemical_defense", outfit.ChemicalDefense);
            command.Parameters.AddWithValue("@atomic_attack", outfit.AtomicAttack);
            command.Parameters.AddWithValue("@atomic_defense", outfit.AtomicDefense);
            command.Parameters.AddWithValue("@mental_attack", outfit.MentalAttack);
            command.Parameters.AddWithValue("@mental_defense", outfit.MentalDefense);
            command.Parameters.AddWithValue("@speed", outfit.Speed);
            command.Parameters.AddWithValue("@critical_damage_rate", outfit.CriticalDamageRate);
            command.Parameters.AddWithValue("@critical_rate", outfit.CriticalRate);
            command.Parameters.AddWithValue("@critical_resistance_rate", outfit.CriticalResistanceRate);
            command.Parameters.AddWithValue("@ignore_critical_rate", outfit.IgnoreCriticalRate);
            command.Parameters.AddWithValue("@penetration_rate", outfit.PenetrationRate);
            command.Parameters.AddWithValue("@penetration_resistance_rate", outfit.PenetrationResistanceRate);
            command.Parameters.AddWithValue("@evasion_rate", outfit.EvasionRate);
            command.Parameters.AddWithValue("@damage_absorption_rate", outfit.DamageAbsorptionRate);
            command.Parameters.AddWithValue("@ignore_damage_absorption_rate", outfit.IgnoreDamageAbsorptionRate);
            command.Parameters.AddWithValue("@absorbed_damage_rate", outfit.AbsorbedDamageRate);
            command.Parameters.AddWithValue("@vitality_regeneration_rate", outfit.VitalityRegenerationRate);
            command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", outfit.VitalityRegenerationResistanceRate);
            command.Parameters.AddWithValue("@accuracy_rate", outfit.AccuracyRate);
            command.Parameters.AddWithValue("@lifesteal_rate", outfit.LifestealRate);
            command.Parameters.AddWithValue("@shield_strength", outfit.ShieldStrength);
            command.Parameters.AddWithValue("@tenacity", outfit.Tenacity);
            command.Parameters.AddWithValue("@resistance_rate", outfit.ResistanceRate);
            command.Parameters.AddWithValue("@combo_rate", outfit.ComboRate);
            command.Parameters.AddWithValue("@ignore_combo_rate", outfit.IgnoreComboRate);
            command.Parameters.AddWithValue("@combo_damage_rate", outfit.ComboDamageRate);
            command.Parameters.AddWithValue("@combo_resistance_rate", outfit.ComboResistanceRate);
            command.Parameters.AddWithValue("@stun_rate", outfit.StunRate);
            command.Parameters.AddWithValue("@ignore_stun_rate", outfit.IgnoreStunRate);
            command.Parameters.AddWithValue("@reflection_rate", outfit.ReflectionRate);
            command.Parameters.AddWithValue("@ignore_reflection_rate", outfit.IgnoreReflectionRate);
            command.Parameters.AddWithValue("@reflection_damage_rate", outfit.ReflectionDamageRate);
            command.Parameters.AddWithValue("@reflection_resistance_rate", outfit.ReflectionResistanceRate);
            command.Parameters.AddWithValue("@mana", outfit.Mana);
            command.Parameters.AddWithValue("@mana_regeneration_rate", outfit.ManaRegenerationRate);
            command.Parameters.AddWithValue("@damage_to_different_faction_rate", outfit.DamageToDifferentFactionRate);
            command.Parameters.AddWithValue("@resistance_to_different_faction_rate", outfit.ResistanceToDifferentFactionRate);
            command.Parameters.AddWithValue("@damage_to_same_faction_rate", outfit.DamageToSameFactionRate);
            command.Parameters.AddWithValue("@resistance_to_same_faction_rate", outfit.ResistanceToSameFactionRate);
            command.Parameters.AddWithValue("@normal_damage_rate", outfit.NormalDamageRate);
            command.Parameters.AddWithValue("@normal_resistance_rate", outfit.NormalResistanceRate);
            command.Parameters.AddWithValue("@skill_damage_rate", outfit.SkillDamageRate);
            command.Parameters.AddWithValue("@skill_resistance_rate", outfit.SkillResistanceRate);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            // MySQL quy ước: Insert mới = 1, Update = 2, Không thay đổi = 0
            if (rowsAffected == 1)
            {
                return InsertOrUpdateResult<Outfits>.Inserted(outfit);
            }
            else if (rowsAffected == 2 || rowsAffected == 0)
            {
                return InsertOrUpdateResult<Outfits>.Updated(outfit);
            }

            return InsertOrUpdateResult<Outfits>.Failure();
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Database Error: " + ex.Message);
            return InsertOrUpdateResult<Outfits>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<BatchOperationResultDTO<Outfits>>> InsertOrUpdateUserOutfitsBatchAsync(
    string userId, List<Outfits> outfits)
    {
        if (outfits == null || outfits.Count == 0)
        {
            return new InsertOrUpdateResult<BatchOperationResultDTO<Outfits>>
            {
                Data = new BatchOperationResultDTO<Outfits>(),
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.NOTHING_WAS_UPDATED
            };
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // 1. Query lấy TOÀN BỘ outfit_id hiện có của User (Cực nhanh nhờ Index user_id)
            var existingIds = new HashSet<string>();
            string checkSql = "SELECT outfit_id FROM user_outfits WHERE user_id = @user_id;";

            await using (var checkCmd = new MySqlCommand(checkSql, connection))
            {
                checkCmd.Parameters.AddWithValue("@user_id", userId);
                await using var reader = await checkCmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    existingIds.Add(reader.GetString(0));
                }
            }

            // 2. Phân loại Outfits giữ NGUYÊN VẸN OBJECT thuộc tính trong RAM C#
            var batchResult = new BatchOperationResultDTO<Outfits>();
            foreach (var card in outfits)
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

            for (int i = 0; i < outfits.Count; i += batchSize)
            {
                var batch = outfits.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append(@"
            INSERT INTO user_outfits (
                user_id, outfit_id, rare, level, experience, star, quality, block, quantity,
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
                (@user_id, @outfit_id_{j}, @rare_{j}, 0, 0, 0, @quality_{j}, 0, @quantity_{j},
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
                    new MySqlParameter($"@outfit_id_{j}", c.Id),
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
                quantity = COALESCE(user_outfits.quantity, 0) + VALUES(quantity);
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

            return new InsertOrUpdateResult<BatchOperationResultDTO<Outfits>>
            {
                Data = batchResult,
                OperationType = operationType
            };
        }
        catch (Exception ex)
        {
            Debug.LogError("Batch Error: " + ex.Message);
            return InsertOrUpdateResult<BatchOperationResultDTO<Outfits>>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateUserOutfitLevelAsync(string userId, Outfits outfit)
    {
        if (outfit == null)
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
            UPDATE user_outfits
            SET 
                level = @level, 
                experience = @experience
            WHERE user_id = @user_id 
              AND outfit_id = @outfit_id
              AND (level != @level OR experience != @experience);
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@outfit_id", outfit.Id);
            updateCommand.Parameters.AddWithValue("@level", outfit.Level);
            updateCommand.Parameters.AddWithValue("@experience", outfit.Experience);

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
            Debug.LogError("Error UpdateUserOutfitLevel: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateUserOutfitStarAsync(string userId, Outfits outfit)
    {
        if (outfit == null)
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
            UPDATE user_outfits
            SET 
                star = @star, 
                quantity = @quantity
            WHERE user_id = @user_id 
              AND outfit_id = @outfit_id
              AND (star != @star OR quantity != @quantity);
        ";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@outfit_id", outfit.Id);
            updateCommand.Parameters.AddWithValue("@star", outfit.Star);
            updateCommand.Parameters.AddWithValue("@quantity", outfit.Quantity);

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
            Debug.LogError("Error UpdateUserOutfitStar: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<Outfits> GetUserOutfitByIdAsync(string userId, string Id)
    {
        Outfits outfit = new Outfits();
        string connectionString = DatabaseConfig.ConnectionString;
        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();
                string selectSQL = @"Select * from user_outfits where user_outfits.outfit_id=@id 
                and user_outfits.user_id=@user_id";
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@id", Id);
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            outfit = new Outfits
                            {
                                Id = reader.GetStringSafe("outfit_id"),
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
        return outfit;
    }
    public async Task<Outfits> SumPowerUserOutfitsAsync(string userId)
    {
        Outfits sumOutfits = new Outfits();
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
            FROM user_outfits uc
            WHERE user_id = @user_id;
            ";
                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            sumOutfits.Health = reader.GetDoubleSafe("health");
                            sumOutfits.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            sumOutfits.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            sumOutfits.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            sumOutfits.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            sumOutfits.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            sumOutfits.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            sumOutfits.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            sumOutfits.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            sumOutfits.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            sumOutfits.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            sumOutfits.Speed = reader.GetDoubleSafe("speed");
                            sumOutfits.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            sumOutfits.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            sumOutfits.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            sumOutfits.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            sumOutfits.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            sumOutfits.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            sumOutfits.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            sumOutfits.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            sumOutfits.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            sumOutfits.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            sumOutfits.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            sumOutfits.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            sumOutfits.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            sumOutfits.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            sumOutfits.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            sumOutfits.Tenacity = reader.GetDoubleSafe("tenacity");
                            sumOutfits.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            sumOutfits.ComboRate = reader.GetDoubleSafe("combo_rate");
                            sumOutfits.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            sumOutfits.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            sumOutfits.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            sumOutfits.StunRate = reader.GetDoubleSafe("stun_rate");
                            sumOutfits.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            sumOutfits.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            sumOutfits.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            sumOutfits.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            sumOutfits.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            sumOutfits.Mana = reader.GetDoubleSafe("mana");
                            sumOutfits.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            sumOutfits.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            sumOutfits.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            sumOutfits.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            sumOutfits.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            sumOutfits.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            sumOutfits.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            sumOutfits.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            sumOutfits.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
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
        return sumOutfits;
    }
}