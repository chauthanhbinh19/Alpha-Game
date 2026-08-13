using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
public class OutfitsGalleryRepository : IOutfitsGalleryRepository
{
    public async Task<List<Outfits>> GetOutfitsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Outfits> outfits = new List<Outfits>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT c.*, 
                       CASE 
                           WHEN cg.outfit_id IS NULL THEN 'block' 
                           WHEN cg.status = 'pending' THEN 'pending' 
                           WHEN cg.status = 'available' THEN 'available' 
                       END AS status 
                FROM Outfits c 
                LEFT JOIN outfits_gallery cg 
                       ON c.id = cg.outfit_id AND cg.user_id = @userId 
                WHERE 1=1
            ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND m.type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += @"LIMIT @limit OFFSET @offset";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    if (!string.IsNullOrEmpty(type) && type != "All")
                    {
                        selectCommand.Parameters.AddWithValue("@type", type);
                    }

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

                    await using (MySqlDataReader reader = (MySqlDataReader)await selectCommand.ExecuteReaderAsync())
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
                                PercentAllHealth = reader.GetDoubleSafe("percent_all_health"),
                                PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack"),
                                PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense"),
                                PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack"),
                                PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense"),
                                PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack"),
                                PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense"),
                                PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack"),
                                PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense"),
                                PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack"),
                                PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense"),
                                Description = reader.GetStringSafe("description"),
                                Status = reader.GetStringSafe("status"),
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
    public async Task<int> GetOutfitsCountAsync(string search, string type, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT COUNT(*) FROM Outfits 
                WHERE 1=1";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND type = @type";
                }

                if (!string.IsNullOrEmpty(rare) && rare != "All")
                {
                    selectSQL += " AND rare = @rare";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND name LIKE CONCAT('%', @search, '%')";
                }

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
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
    public async Task<InsertOrUpdateResult<Outfits>> InsertOutfitGalleryAsync(string userId, string id, Outfits outfit)
    {
        int percent = QualityEvaluatorHelper.CheckQuality(outfit.Rarity);
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // 1. Kiểm tra xem đã có trong Gallery chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM outfits_gallery 
            WHERE user_id = @user_id AND outfit_id = @outfit_id;
        ";

            await using MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@outfit_id", id);

            int recordCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            // 2. Nếu ĐÃ TỒN TẠI: Bỏ qua, trả về OperationType = None (hoặc custom message)
            if (recordCount > 0)
            {
                return new InsertOrUpdateResult<Outfits>
                {
                    Data = outfit,
                    OperationType = DatabaseOperationType.None,
                    Message = MessageConstants.THIS_RECORD_ALREADY_EXISTS_IN_GALLERY
                };
            }

            // 3. Nếu CHƯA CÓ: Thực hiện INSERT
            string insertSQL = @"
            INSERT INTO outfits_gallery (
                user_id, outfit_id, status, current_star, temp_star, power, health, 
                physical_attack, physical_defense, magical_attack, magical_defense, 
                chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                mental_attack, mental_defense, speed, critical_damage_rate, critical_rate,
                critical_resistance_rate, ignore_critical_rate, penetration_rate, 
                penetration_resistance_rate, evasion_rate, damage_absorption_rate, 
                ignore_damage_absorption_rate, absorbed_damage_rate, vitality_regeneration_rate, 
                vitality_regeneration_resistance_rate, accuracy_rate, lifesteal_rate, 
                shield_strength, tenacity, resistance_rate, combo_rate, ignore_combo_rate, 
                combo_damage_rate, combo_resistance_rate, stun_rate, ignore_stun_rate, 
                reflection_rate, ignore_reflection_rate, reflection_damage_rate, 
                reflection_resistance_rate, mana, mana_regeneration_rate, 
                damage_to_different_faction_rate, resistance_to_different_faction_rate, 
                damage_to_same_faction_rate, resistance_to_same_faction_rate, 
                normal_damage_rate, normal_resistance_rate, skill_damage_rate, 
                skill_resistance_rate, percent_all_health, percent_all_physical_attack, 
                percent_all_physical_defense, percent_all_magical_attack, 
                percent_all_magical_defense, percent_all_chemical_attack, 
                percent_all_chemical_defense, percent_all_atomic_attack, 
                percent_all_atomic_defense, percent_all_mental_attack, 
                percent_all_mental_defense
            )
            VALUES (
                @user_id, @outfit_id, @status, @current_star, @temp_star, @power, @health,
                @physical_attack, @physical_defense, @magical_attack, @magical_defense,
                @chemical_attack, @chemical_defense, @atomic_attack, @atomic_defense,
                @mental_attack, @mental_defense, @speed, @critical_damage_rate, @critical_rate,
                @critical_resistance_rate, @ignore_critical_rate, @penetration_rate,
                @penetration_resistance_rate, @evasion_rate, @damage_absorption_rate,
                @ignore_damage_absorption_rate, @absorbed_damage_rate, @vitality_regeneration_rate,
                @vitality_regeneration_resistance_rate, @accuracy_rate, @lifesteal_rate,
                @shield_strength, @tenacity, @resistance_rate, @combo_rate, @ignore_combo_rate,
                @combo_damage_rate, @combo_resistance_rate, @stun_rate, @ignore_stun_rate,
                @reflection_rate, @ignore_reflection_rate, @reflection_damage_rate,
                @reflection_resistance_rate, @mana, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate, @skill_damage_rate,
                @skill_resistance_rate, @percent_all_health, @percent_all_physical_attack,
                @percent_all_physical_defense, @percent_all_magical_attack,
                @percent_all_magical_defense, @percent_all_chemical_attack,
                @percent_all_chemical_defense, @percent_all_atomic_attack,
                @percent_all_atomic_defense, @percent_all_mental_attack,
                @percent_all_mental_defense
            );";

            await using MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection);

            // Parameters
            insertCommand.Parameters.AddWithValue("@user_id", userId);
            insertCommand.Parameters.AddWithValue("@outfit_id", id);
            insertCommand.Parameters.AddWithValue("@status", "pending");
            insertCommand.Parameters.AddWithValue("@current_star", 0);
            insertCommand.Parameters.AddWithValue("@temp_star", 0);

            insertCommand.Parameters.AddWithValue("@power", outfit.Power);
            insertCommand.Parameters.AddWithValue("@health", outfit.Health);
            insertCommand.Parameters.AddWithValue("@physical_attack", outfit.PhysicalAttack);
            insertCommand.Parameters.AddWithValue("@physical_defense", outfit.PhysicalDefense);
            insertCommand.Parameters.AddWithValue("@magical_attack", outfit.MagicalAttack);
            insertCommand.Parameters.AddWithValue("@magical_defense", outfit.MagicalDefense);
            insertCommand.Parameters.AddWithValue("@chemical_attack", outfit.ChemicalAttack);
            insertCommand.Parameters.AddWithValue("@chemical_defense", outfit.ChemicalDefense);
            insertCommand.Parameters.AddWithValue("@atomic_attack", outfit.AtomicAttack);
            insertCommand.Parameters.AddWithValue("@atomic_defense", outfit.AtomicDefense);
            insertCommand.Parameters.AddWithValue("@mental_attack", outfit.MentalAttack);
            insertCommand.Parameters.AddWithValue("@mental_defense", outfit.MentalDefense);
            insertCommand.Parameters.AddWithValue("@speed", outfit.Speed);
            insertCommand.Parameters.AddWithValue("@critical_damage_rate", outfit.CriticalDamageRate);
            insertCommand.Parameters.AddWithValue("@critical_rate", outfit.CriticalRate);
            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", outfit.CriticalResistanceRate);
            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", outfit.IgnoreCriticalRate);
            insertCommand.Parameters.AddWithValue("@penetration_rate", outfit.PenetrationRate);
            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", outfit.PenetrationResistanceRate);
            insertCommand.Parameters.AddWithValue("@evasion_rate", outfit.EvasionRate);
            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", outfit.DamageAbsorptionRate);
            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", outfit.IgnoreDamageAbsorptionRate);
            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", outfit.AbsorbedDamageRate);
            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", outfit.VitalityRegenerationRate);
            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", outfit.VitalityRegenerationResistanceRate);
            insertCommand.Parameters.AddWithValue("@accuracy_rate", outfit.AccuracyRate);
            insertCommand.Parameters.AddWithValue("@lifesteal_rate", outfit.LifestealRate);
            insertCommand.Parameters.AddWithValue("@shield_strength", outfit.ShieldStrength);
            insertCommand.Parameters.AddWithValue("@tenacity", outfit.Tenacity);
            insertCommand.Parameters.AddWithValue("@resistance_rate", outfit.ResistanceRate);
            insertCommand.Parameters.AddWithValue("@combo_rate", outfit.ComboRate);
            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", outfit.IgnoreComboRate);
            insertCommand.Parameters.AddWithValue("@combo_damage_rate", outfit.ComboDamageRate);
            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", outfit.ComboResistanceRate);
            insertCommand.Parameters.AddWithValue("@stun_rate", outfit.StunRate);
            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", outfit.IgnoreStunRate);
            insertCommand.Parameters.AddWithValue("@reflection_rate", outfit.ReflectionRate);
            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", outfit.IgnoreReflectionRate);
            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", outfit.ReflectionDamageRate);
            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", outfit.ReflectionResistanceRate);
            insertCommand.Parameters.AddWithValue("@mana", outfit.Mana);
            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", outfit.ManaRegenerationRate);
            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", outfit.DamageToDifferentFactionRate);
            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", outfit.ResistanceToDifferentFactionRate);
            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", outfit.DamageToSameFactionRate);
            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", outfit.ResistanceToSameFactionRate);
            insertCommand.Parameters.AddWithValue("@normal_damage_rate", outfit.NormalDamageRate);
            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", outfit.NormalResistanceRate);
            insertCommand.Parameters.AddWithValue("@skill_damage_rate", outfit.SkillDamageRate);
            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", outfit.SkillResistanceRate);

            // Buff percent
            insertCommand.Parameters.AddWithValue("@percent_all_health", percent);
            insertCommand.Parameters.AddWithValue("@percent_all_physical_attack", percent);
            insertCommand.Parameters.AddWithValue("@percent_all_physical_defense", percent);
            insertCommand.Parameters.AddWithValue("@percent_all_magical_attack", percent);
            insertCommand.Parameters.AddWithValue("@percent_all_magical_defense", percent);
            insertCommand.Parameters.AddWithValue("@percent_all_chemical_attack", percent);
            insertCommand.Parameters.AddWithValue("@percent_all_chemical_defense", percent);
            insertCommand.Parameters.AddWithValue("@percent_all_atomic_attack", percent);
            insertCommand.Parameters.AddWithValue("@percent_all_atomic_defense", percent);
            insertCommand.Parameters.AddWithValue("@percent_all_mental_attack", percent);
            insertCommand.Parameters.AddWithValue("@percent_all_mental_defense", percent);

            await insertCommand.ExecuteNonQueryAsync();

            // TRẢ VỀ INSERTED THÀNH CÔNG
            return InsertOrUpdateResult<Outfits>.Inserted(outfit);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return InsertOrUpdateResult<Outfits>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<List<Outfits>>> InsertBatchOutfitsGalleryAsync(string userId, List<Outfits> outfits)
    {
        if (outfits == null || outfits.Count == 0)
        {
            return InsertOrUpdateResult<List<Outfits>>.Inserted(new List<Outfits>());
        }

        string connectionString = DatabaseConfig.ConnectionString;
        var insertedList = new List<Outfits>();
        int batchSize = 300;
        int timeoutSeconds = 120;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Chia danh sách thành từng Chunk/Batch nhỏ để tránh bị quá giới hạn max_allowed_packet của MySQL
            for (int i = 0; i < outfits.Count; i += batchSize)
            {
                var chunk = outfits.Skip(i).Take(batchSize).ToList();

                // Sử dụng Transaction để đảm bảo tính toàn vẹn cho mỗi Batch
                await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

                try
                {
                    var sb = new StringBuilder();

                    // INSERT IGNORE: Tự động bỏ qua các dòng đã trùng Primary Key (user_id, outfit_id)
                    sb.Append(@"
                    INSERT IGNORE INTO outfits_gallery (
                        user_id, outfit_id, status, current_star, temp_star, power, health, 
                        physical_attack, physical_defense, magical_attack, magical_defense, 
                        chemical_attack, chemical_defense, atomic_attack, atomic_defense, 
                        mental_attack, mental_defense, speed, critical_damage_rate, critical_rate,
                        critical_resistance_rate, ignore_critical_rate, penetration_rate, 
                        penetration_resistance_rate, evasion_rate, damage_absorption_rate, 
                        ignore_damage_absorption_rate, absorbed_damage_rate, vitality_regeneration_rate, 
                        vitality_regeneration_resistance_rate, accuracy_rate, lifesteal_rate, 
                        shield_strength, tenacity, resistance_rate, combo_rate, ignore_combo_rate, 
                        combo_damage_rate, combo_resistance_rate, stun_rate, ignore_stun_rate, 
                        reflection_rate, ignore_reflection_rate, reflection_damage_rate, 
                        reflection_resistance_rate, mana, mana_regeneration_rate, 
                        damage_to_different_faction_rate, resistance_to_different_faction_rate, 
                        damage_to_same_faction_rate, resistance_to_same_faction_rate, 
                        normal_damage_rate, normal_resistance_rate, skill_damage_rate, 
                        skill_resistance_rate, percent_all_health, percent_all_physical_attack, 
                        percent_all_physical_defense, percent_all_magical_attack, 
                        percent_all_magical_defense, percent_all_chemical_attack, 
                        percent_all_chemical_defense, percent_all_atomic_attack, 
                        percent_all_atomic_defense, percent_all_mental_attack, 
                        percent_all_mental_defense
                    ) VALUES ");

                    await using MySqlCommand command = new MySqlCommand();
                    command.Connection = connection;
                    command.Transaction = transaction;
                    command.CommandTimeout = timeoutSeconds; // Tăng timeout cho batch lớn

                    var valueSqls = new List<string>();

                    for (int j = 0; j < chunk.Count; j++)
                    {
                        var item = chunk[j];
                        int percent = QualityEvaluatorHelper.CheckQuality(item.Rarity);
                        string suffix = $"_{i}_{j}"; // Tránh trùng tên tham số giữa các phần tử

                        valueSqls.Add($@"
                        (@user_id{suffix}, @outfit_id{suffix}, 'pending', 0, 0, @power{suffix}, @health{suffix},
                         @physical_attack{suffix}, @physical_defense{suffix}, @magical_attack{suffix}, @magical_defense{suffix},
                         @chemical_attack{suffix}, @chemical_defense{suffix}, @atomic_attack{suffix}, @atomic_defense{suffix},
                         @mental_attack{suffix}, @mental_defense{suffix}, @speed{suffix}, @critical_damage_rate{suffix}, @critical_rate{suffix},
                         @critical_resistance_rate{suffix}, @ignore_critical_rate{suffix}, @penetration_rate{suffix},
                         @penetration_resistance_rate{suffix}, @evasion_rate{suffix}, @damage_absorption_rate{suffix},
                         @ignore_damage_absorption_rate{suffix}, @absorbed_damage_rate{suffix}, @vitality_regeneration_rate{suffix},
                         @vitality_regeneration_resistance_rate{suffix}, @accuracy_rate{suffix}, @lifesteal_rate{suffix},
                         @shield_strength{suffix}, @tenacity{suffix}, @resistance_rate{suffix}, @combo_rate{suffix}, @ignore_combo_rate{suffix},
                         @combo_damage_rate{suffix}, @combo_resistance_rate{suffix}, @stun_rate{suffix}, @ignore_stun_rate{suffix},
                         @reflection_rate{suffix}, @ignore_reflection_rate{suffix}, @reflection_damage_rate{suffix},
                         @reflection_resistance_rate{suffix}, @mana{suffix}, @mana_regeneration_rate{suffix},
                         @damage_to_different_faction_rate{suffix}, @resistance_to_different_faction_rate{suffix},
                         @damage_to_same_faction_rate{suffix}, @resistance_to_same_faction_rate{suffix},
                         @normal_damage_rate{suffix}, @normal_resistance_rate{suffix}, @skill_damage_rate{suffix},
                         @skill_resistance_rate{suffix}, @percent_all_health{suffix}, @percent_all_physical_attack{suffix},
                         @percent_all_physical_defense{suffix}, @percent_all_magical_attack{suffix},
                         @percent_all_magical_defense{suffix}, @percent_all_chemical_attack{suffix},
                         @percent_all_chemical_defense{suffix}, @percent_all_atomic_attack{suffix},
                         @percent_all_atomic_defense{suffix}, @percent_all_mental_attack{suffix},
                         @percent_all_mental_defense{suffix})");

                        // Bind Parameters
                        command.Parameters.AddWithValue($"@user_id{suffix}", userId);
                        command.Parameters.AddWithValue($"@outfit_id{suffix}", item.Id);

                        command.Parameters.AddWithValue($"@power{suffix}", item.Power);
                        command.Parameters.AddWithValue($"@health{suffix}", item.Health);
                        command.Parameters.AddWithValue($"@physical_attack{suffix}", item.PhysicalAttack);
                        command.Parameters.AddWithValue($"@physical_defense{suffix}", item.PhysicalDefense);
                        command.Parameters.AddWithValue($"@magical_attack{suffix}", item.MagicalAttack);
                        command.Parameters.AddWithValue($"@magical_defense{suffix}", item.MagicalDefense);
                        command.Parameters.AddWithValue($"@chemical_attack{suffix}", item.ChemicalAttack);
                        command.Parameters.AddWithValue($"@chemical_defense{suffix}", item.ChemicalDefense);
                        command.Parameters.AddWithValue($"@atomic_attack{suffix}", item.AtomicAttack);
                        command.Parameters.AddWithValue($"@atomic_defense{suffix}", item.AtomicDefense);
                        command.Parameters.AddWithValue($"@mental_attack{suffix}", item.MentalAttack);
                        command.Parameters.AddWithValue($"@mental_defense{suffix}", item.MentalDefense);
                        command.Parameters.AddWithValue($"@speed{suffix}", item.Speed);
                        command.Parameters.AddWithValue($"@critical_damage_rate{suffix}", item.CriticalDamageRate);
                        command.Parameters.AddWithValue($"@critical_rate{suffix}", item.CriticalRate);
                        command.Parameters.AddWithValue($"@critical_resistance_rate{suffix}", item.CriticalResistanceRate);
                        command.Parameters.AddWithValue($"@ignore_critical_rate{suffix}", item.IgnoreCriticalRate);
                        command.Parameters.AddWithValue($"@penetration_rate{suffix}", item.PenetrationRate);
                        command.Parameters.AddWithValue($"@penetration_resistance_rate{suffix}", item.PenetrationResistanceRate);
                        command.Parameters.AddWithValue($"@evasion_rate{suffix}", item.EvasionRate);
                        command.Parameters.AddWithValue($"@damage_absorption_rate{suffix}", item.DamageAbsorptionRate);
                        command.Parameters.AddWithValue($"@ignore_damage_absorption_rate{suffix}", item.IgnoreDamageAbsorptionRate);
                        command.Parameters.AddWithValue($"@absorbed_damage_rate{suffix}", item.AbsorbedDamageRate);
                        command.Parameters.AddWithValue($"@vitality_regeneration_rate{suffix}", item.VitalityRegenerationRate);
                        command.Parameters.AddWithValue($"@vitality_regeneration_resistance_rate{suffix}", item.VitalityRegenerationResistanceRate);
                        command.Parameters.AddWithValue($"@accuracy_rate{suffix}", item.AccuracyRate);
                        command.Parameters.AddWithValue($"@lifesteal_rate{suffix}", item.LifestealRate);
                        command.Parameters.AddWithValue($"@shield_strength{suffix}", item.ShieldStrength);
                        command.Parameters.AddWithValue($"@tenacity{suffix}", item.Tenacity);
                        command.Parameters.AddWithValue($"@resistance_rate{suffix}", item.ResistanceRate);
                        command.Parameters.AddWithValue($"@combo_rate{suffix}", item.ComboRate);
                        command.Parameters.AddWithValue($"@ignore_combo_rate{suffix}", item.IgnoreComboRate);
                        command.Parameters.AddWithValue($"@combo_damage_rate{suffix}", item.ComboDamageRate);
                        command.Parameters.AddWithValue($"@combo_resistance_rate{suffix}", item.ComboResistanceRate);
                        command.Parameters.AddWithValue($"@stun_rate{suffix}", item.StunRate);
                        command.Parameters.AddWithValue($"@ignore_stun_rate{suffix}", item.IgnoreStunRate);
                        command.Parameters.AddWithValue($"@reflection_rate{suffix}", item.ReflectionRate);
                        command.Parameters.AddWithValue($"@ignore_reflection_rate{suffix}", item.IgnoreReflectionRate);
                        command.Parameters.AddWithValue($"@reflection_damage_rate{suffix}", item.ReflectionDamageRate);
                        command.Parameters.AddWithValue($"@reflection_resistance_rate{suffix}", item.ReflectionResistanceRate);
                        command.Parameters.AddWithValue($"@mana{suffix}", item.Mana);
                        command.Parameters.AddWithValue($"@mana_regeneration_rate{suffix}", item.ManaRegenerationRate);
                        command.Parameters.AddWithValue($"@damage_to_different_faction_rate{suffix}", item.DamageToDifferentFactionRate);
                        command.Parameters.AddWithValue($"@resistance_to_different_faction_rate{suffix}", item.ResistanceToDifferentFactionRate);
                        command.Parameters.AddWithValue($"@damage_to_same_faction_rate{suffix}", item.DamageToSameFactionRate);
                        command.Parameters.AddWithValue($"@resistance_to_same_faction_rate{suffix}", item.ResistanceToSameFactionRate);
                        command.Parameters.AddWithValue($"@normal_damage_rate{suffix}", item.NormalDamageRate);
                        command.Parameters.AddWithValue($"@normal_resistance_rate{suffix}", item.NormalResistanceRate);
                        command.Parameters.AddWithValue($"@skill_damage_rate{suffix}", item.SkillDamageRate);
                        command.Parameters.AddWithValue($"@skill_resistance_rate{suffix}", item.SkillResistanceRate);

                        // Percent buff
                        command.Parameters.AddWithValue($"@percent_all_health{suffix}", percent);
                        command.Parameters.AddWithValue($"@percent_all_physical_attack{suffix}", percent);
                        command.Parameters.AddWithValue($"@percent_all_physical_defense{suffix}", percent);
                        command.Parameters.AddWithValue($"@percent_all_magical_attack{suffix}", percent);
                        command.Parameters.AddWithValue($"@percent_all_magical_defense{suffix}", percent);
                        command.Parameters.AddWithValue($"@percent_all_chemical_attack{suffix}", percent);
                        command.Parameters.AddWithValue($"@percent_all_chemical_defense{suffix}", percent);
                        command.Parameters.AddWithValue($"@percent_all_atomic_attack{suffix}", percent);
                        command.Parameters.AddWithValue($"@percent_all_atomic_defense{suffix}", percent);
                        command.Parameters.AddWithValue($"@percent_all_mental_attack{suffix}", percent);
                        command.Parameters.AddWithValue($"@percent_all_mental_defense{suffix}", percent);
                    }

                    sb.Append(string.Join(",", valueSqls));
                    sb.Append(";");

                    command.CommandText = sb.ToString();
                    await command.ExecuteNonQueryAsync();

                    await transaction.CommitAsync();
                    insertedList.AddRange(chunk);
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            return InsertOrUpdateResult<List<Outfits>>.Inserted(insertedList);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error Batch Insert: " + ex.Message);
            return InsertOrUpdateResult<List<Outfits>>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateStatusOutfitGalleryAsync(string userId, string id, string status = "available")
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Thêm điều kiện (status IS NULL OR status != @status) để tránh update thừa khi status đã đúng sẵn
            string updateSQL = @"UPDATE outfits_gallery 
                             SET status = @status 
                             WHERE user_id = @user_id 
                               AND outfit_id = @outfit_id
                               AND (status IS NULL OR status != @status);";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@outfit_id", id);
            updateCommand.Parameters.AddWithValue("@status", status);

            int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                // Cập nhật thành công từ status cũ sang status mới
                return InsertOrUpdateResult<bool>.Updated(true);
            }
            else
            {
                // Không tìm thấy bản ghi hoặc status đã là 'available' từ trước
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
            Debug.LogError("Error UpdateStatusOutfitGallery: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusOutfitsGalleryAsync(string userId, string status = "available")
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Cập nhật tất cả bản ghi của user_id có status khác với status mới (tránh update thừa)
            string updateSQL = @"UPDATE outfits_gallery 
                             SET status = @status 
                             WHERE user_id = @user_id 
                               AND (status IS NULL OR status != @status);";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@status", status);

            int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                // Cập nhật thành công các bản ghi đủ điều kiện
                return InsertOrUpdateResult<bool>.Updated(true);
            }
            else
            {
                // Không tìm thấy bản ghi nào cần cập nhật (hoặc tất cả đã ở status này rồi)
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
            Debug.LogError("Error UpdateBatchStatusOutfitsGallery: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<double>> UpdateTempStarOutfitGalleryAsync(string userId, string outfitId, double star)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string updateSQL = @"
            UPDATE outfits_gallery 
            SET temp_star = @temp_star 
            WHERE user_id = @user_id 
              AND outfit_id = @outfit_id 
              AND (temp_star IS NULL OR temp_star < @temp_star);";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@outfit_id", outfitId);
            updateCommand.Parameters.AddWithValue("@temp_star", star);

            int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                // Trả về UPDATED nếu số sao mới thực sự cập nhật thành công
                return InsertOrUpdateResult<double>.Updated(star);
            }
            else
            {
                // Trả về NONE nếu sao mới <= sao cũ hoặc không tìm thấy bản ghi
                return new InsertOrUpdateResult<double>
                {
                    Data = star,
                    OperationType = DatabaseOperationType.None,
                    Message = MessageConstants.NOTHING_WAS_UPDATED
                };
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return InsertOrUpdateResult<double>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<double>> UpdateCurrentStarOutfitGalleryAsync(string userId, string outfitId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Bước 1: Update current_star = temp_star
            string updateSQL = @"
            UPDATE outfits_gallery 
            SET current_star = temp_star
            WHERE user_id = @user_id 
              AND outfit_id = @outfit_id 
              AND temp_star > current_star;";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@outfit_id", outfitId);

            int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                // Bước 2: Lấy current_star mới ra để trả về cho Client / Service tính Stats
                string selectSQL = @"
                SELECT current_star 
                FROM outfits_gallery 
                WHERE user_id = @user_id AND outfit_id = @outfit_id;";

                await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@user_id", userId);
                selectCommand.Parameters.AddWithValue("@outfit_id", outfitId);

                double newCurrentStar = Convert.ToDouble(await selectCommand.ExecuteScalarAsync());

                return InsertOrUpdateResult<double>.Updated(newCurrentStar);
            }

            return new InsertOrUpdateResult<double>
            {
                Data = 0,
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.NOTHING_WAS_UPDATED
            };
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return InsertOrUpdateResult<double>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<List<(string OutfitId, double CurrentStar)>>> UpdateBatchCurrentStarOutfitsGalleryAsync(string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Bước 1: Cập nhật tất cả bản ghi có temp_star > current_star của userId
            string updateSQL = @"
            UPDATE outfits_gallery 
            SET current_star = temp_star
            WHERE user_id = @user_id 
              AND temp_star > current_star;";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
            updateCommand.Parameters.AddWithValue("@user_id", userId);

            int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                // Bước 2: Lấy danh sách outfit_id và current_star vừa được cập nhật (current_star == temp_star)
                string selectSQL = @"
                SELECT outfit_id, current_star 
                FROM outfits_gallery 
                WHERE user_id = @user_id 
                  AND current_star = temp_star;";

                await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@user_id", userId);

                await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();
                var updatedList = new List<(string OutfitId, double CurrentStar)>();

                while (await reader.ReadAsync())
                {
                    string id = reader.GetString("outfit_id");
                    double star = reader.GetDouble("current_star");
                    updatedList.Add((id, star));
                }

                return InsertOrUpdateResult<List<(string OutfitId, double CurrentStar)>>.Updated(updatedList);
            }

            return new InsertOrUpdateResult<List<(string OutfitId, double CurrentStar)>>
            {
                Data = new List<(string OutfitId, double CurrentStar)>(),
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.NOTHING_WAS_UPDATED
            };
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return InsertOrUpdateResult<List<(string OutfitId, double CurrentStar)>>.Failure(ex.Message);
        }
    }
    public async Task<Outfits> GetOutfitCollectionByIdAsync(string userId, string objectId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using MySqlConnection connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT mg.* 
            FROM outfits_gallery mg 
            WHERE mg.user_id = @userId AND mg.outfit_id = @objectId AND mg.status = 'available';";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", userId);
            selectCommand.Parameters.AddWithValue("@objectId", objectId);

            await using var reader = await selectCommand.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                // Trả về object đã map
                return new Outfits
                {
                    Id = reader.GetStringSafe("outfit_id"),
                    CurrentStar = reader.IsDBNull(reader.GetOrdinal("current_star")) ? 0 : reader.GetIntSafe("current_star"),
                    TempStar = reader.IsDBNull(reader.GetOrdinal("temp_star")) ? 0 : reader.GetIntSafe("temp_star"),
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

                    PercentAllHealth = reader.GetDoubleSafe("percent_all_health"),
                    PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack"),
                    PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense"),
                    PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack"),
                    PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense"),
                    PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack"),
                    PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense"),
                    PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack"),
                    PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense"),
                    PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack"),
                    PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense"),
                };
            }

            // Không tìm thấy record
            return null;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error: {ex.Message}");
            throw; // Throw lại exception để phía Gọi hàm biết có lỗi DB
        }
    }
    public async Task UpdateOutfitGalleryPowerAsync(string userId, string id, Outfits outfit)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"UPDATE outfits_gallery
                SET 
                    status = @status,
                    current_star = @current_star,
                    power = @power,
                    health = health + @health,
                    physical_attack = physical_attack + @physical_attack,
                    physical_defense = physical_defense + @physical_defense,
                    magical_attack = magical_attack + @magical_attack,
                    magical_defense = magical_defense + @magical_defense,
                    chemical_attack = chemical_attack + @chemical_attack,
                    chemical_defense = chemical_defense + @chemical_defense,
                    atomic_attack = atomic_attack + @atomic_attack,
                    atomic_defense = atomic_defense + @atomic_defense,
                    mental_attack = mental_attack + @mental_attack,
                    mental_defense = mental_defense + @mental_defense,
                    speed = speed + @speed,
                    critical_damage_rate = critical_damage_rate + @critical_damage_rate,
                    critical_rate = critical_rate + @critical_rate,
                    critical_resistance_rate = critical_resistance_rate + @critical_resistance_rate,
                    ignore_critical_rate = ignore_critical_rate + @ignore_critical_rate,
                    penetration_rate = penetration_rate + @penetration_rate,
                    penetration_resistance_rate = penetration_resistance_rate + @penetration_resistance_rate,
                    evasion_rate = evasion_rate + @evasion_rate,
                    damage_absorption_rate = damage_absorption_rate + @damage_absorption_rate,
                    ignore_damage_absorption_rate = ignore_damage_absorption_rate + @ignore_damage_absorption_rate,
                    absorbed_damage_rate = absorbed_damage_rate + @absorbed_damage_rate,
                    vitality_regeneration_rate = vitality_regeneration_rate + @vitality_regeneration_rate,
                    vitality_regeneration_resistance_rate = vitality_regeneration_resistance_rate + @vitality_regeneration_resistance_rate,
                    accuracy_rate = accuracy_rate + @accuracy_rate,
                    lifesteal_rate = lifesteal_rate + @lifesteal_rate,
                    shield_strength = shield_strength + @shield_strength,
                    tenacity = tenacity + @tenacity,
                    resistance_rate = resistance_rate + @resistance_rate,
                    combo_rate = combo_rate + @combo_rate,
                    ignore_combo_rate = ignore_combo_rate + @ignore_combo_rate,
                    combo_damage_rate = combo_damage_rate + @combo_damage_rate,
                    combo_resistance_rate = combo_resistance_rate + @combo_resistance_rate,
                    stun_rate = stun_rate + @stun_rate,
                    ignore_stun_rate = ignore_stun_rate + @ignore_stun_rate,
                    reflection_rate = reflection_rate + @reflection_rate,
                    ignore_reflection_rate = ignore_reflection_rate + @ignore_reflection_rate,
                    reflection_damage_rate = reflection_damage_rate + @reflection_damage_rate,
                    reflection_resistance_rate = reflection_resistance_rate + @reflection_resistance_rate,
                    mana = mana + @mana,
                    mana_regeneration_rate = mana_regeneration_rate + @mana_regeneration_rate,
                    damage_to_different_faction_rate = damage_to_different_faction_rate + @damage_to_different_faction_rate,
                    resistance_to_different_faction_rate = resistance_to_different_faction_rate + @resistance_to_different_faction_rate,
                    damage_to_same_faction_rate = damage_to_same_faction_rate + @damage_to_same_faction_rate,
                    resistance_to_same_faction_rate = resistance_to_same_faction_rate + @resistance_to_same_faction_rate,
                    normal_damage_rate = normal_damage_rate + @normal_damage_rate,
                    normal_resistance_rate = normal_resistance_rate + @normal_resistance_rate,
                    skill_damage_rate = skill_damage_rate + @skill_damage_rate,
                    skill_resistance_rate = skill_resistance_rate + @skill_resistance_rate,
                    percent_all_health = percent_all_health + @percent_all_health,
                    percent_all_physical_attack = percent_all_physical_attack + @percent_all_physical_attack,
                    percent_all_physical_defense = percent_all_physical_defense + @percent_all_physical_defense,
                    percent_all_magical_attack = percent_all_magical_attack + @percent_all_magical_attack,
                    percent_all_magical_defense = percent_all_magical_defense + @percent_all_magical_defense,
                    percent_all_chemical_attack = percent_all_chemical_attack + @percent_all_chemical_attack,
                    percent_all_chemical_defense = percent_all_chemical_defense + @percent_all_chemical_defense,
                    percent_all_atomic_attack = percent_all_atomic_attack + @percent_all_atomic_attack,
                    percent_all_atomic_defense = percent_all_atomic_defense + @percent_all_atomic_defense,
                    percent_all_mental_attack = percent_all_mental_attack + @percent_all_mental_attack,
                    percent_all_mental_defense = percent_all_mental_defense + @percent_all_mental_defense
                WHERE user_id = @user_id
                AND outfit_id = @outfit_id;
            ";

                MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

                // IDs
                updateCommand.Parameters.AddWithValue("@user_id", userId);
                updateCommand.Parameters.AddWithValue("@outfit_id", id);

                // Base flags
                updateCommand.Parameters.AddWithValue("@status", "pending");
                updateCommand.Parameters.AddWithValue("@current_star", 0);

                // Stats
                updateCommand.Parameters.AddWithValue("@power", outfit.Power);
                updateCommand.Parameters.AddWithValue("@health", outfit.Health);
                updateCommand.Parameters.AddWithValue("@physical_attack", outfit.PhysicalAttack);
                updateCommand.Parameters.AddWithValue("@physical_defense", outfit.PhysicalDefense);
                updateCommand.Parameters.AddWithValue("@magical_attack", outfit.MagicalAttack);
                updateCommand.Parameters.AddWithValue("@magical_defense", outfit.MagicalDefense);
                updateCommand.Parameters.AddWithValue("@chemical_attack", outfit.ChemicalAttack);
                updateCommand.Parameters.AddWithValue("@chemical_defense", outfit.ChemicalDefense);
                updateCommand.Parameters.AddWithValue("@atomic_attack", outfit.AtomicAttack);
                updateCommand.Parameters.AddWithValue("@atomic_defense", outfit.AtomicDefense);
                updateCommand.Parameters.AddWithValue("@mental_attack", outfit.MagicalAttack);
                updateCommand.Parameters.AddWithValue("@mental_defense", outfit.MagicalDefense);
                updateCommand.Parameters.AddWithValue("@speed", outfit.Speed);
                updateCommand.Parameters.AddWithValue("@critical_damage_rate", outfit.CriticalDamageRate);
                updateCommand.Parameters.AddWithValue("@critical_rate", outfit.CriticalRate);
                updateCommand.Parameters.AddWithValue("@critical_resistance_rate", outfit.CriticalResistanceRate);
                updateCommand.Parameters.AddWithValue("@ignore_critical_rate", outfit.IgnoreCriticalRate);
                updateCommand.Parameters.AddWithValue("@penetration_rate", outfit.PenetrationRate);
                updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", outfit.PenetrationResistanceRate);
                updateCommand.Parameters.AddWithValue("@evasion_rate", outfit.EvasionRate);
                updateCommand.Parameters.AddWithValue("@damage_absorption_rate", outfit.DamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", outfit.IgnoreDamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", outfit.AbsorbedDamageRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", outfit.VitalityRegenerationRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", outfit.VitalityRegenerationResistanceRate);
                updateCommand.Parameters.AddWithValue("@accuracy_rate", outfit.AccuracyRate);
                updateCommand.Parameters.AddWithValue("@lifesteal_rate", outfit.LifestealRate);
                updateCommand.Parameters.AddWithValue("@shield_strength", outfit.ShieldStrength);
                updateCommand.Parameters.AddWithValue("@tenacity", outfit.Tenacity);
                updateCommand.Parameters.AddWithValue("@resistance_rate", outfit.ResistanceRate);
                updateCommand.Parameters.AddWithValue("@combo_rate", outfit.ComboRate);
                updateCommand.Parameters.AddWithValue("@ignore_combo_rate", outfit.IgnoreComboRate);
                updateCommand.Parameters.AddWithValue("@combo_damage_rate", outfit.ComboDamageRate);
                updateCommand.Parameters.AddWithValue("@combo_resistance_rate", outfit.ComboResistanceRate);
                updateCommand.Parameters.AddWithValue("@stun_rate", outfit.StunRate);
                updateCommand.Parameters.AddWithValue("@ignore_stun_rate", outfit.IgnoreStunRate);
                updateCommand.Parameters.AddWithValue("@reflection_rate", outfit.ReflectionRate);
                updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", outfit.IgnoreReflectionRate);
                updateCommand.Parameters.AddWithValue("@reflection_damage_rate", outfit.ReflectionDamageRate);
                updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", outfit.ReflectionResistanceRate);
                updateCommand.Parameters.AddWithValue("@mana", outfit.Mana);
                updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", outfit.ManaRegenerationRate);
                updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", outfit.DamageToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", outfit.ResistanceToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", outfit.DamageToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", outfit.ResistanceToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@normal_damage_rate", outfit.NormalDamageRate);
                updateCommand.Parameters.AddWithValue("@normal_resistance_rate", outfit.NormalResistanceRate);
                updateCommand.Parameters.AddWithValue("@skill_damage_rate", outfit.SkillDamageRate);
                updateCommand.Parameters.AddWithValue("@skill_resistance_rate", outfit.SkillResistanceRate);

                // Percent bonuses (hard-coded)
                updateCommand.Parameters.AddWithValue("@percent_all_health", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_physical_attack", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_physical_defense", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_magical_attack", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_magical_defense", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_chemical_attack", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_chemical_defense", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_atomic_attack", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_atomic_defense", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_mental_attack", 5);
                updateCommand.Parameters.AddWithValue("@percent_all_mental_defense", 5);

                await updateCommand.ExecuteNonQueryAsync();
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
    }
    public async Task<Outfits> SumPowerOutfitsGalleryAsync(string userId)
    {
        Outfits sumOutfits = new Outfits();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT 
                SUM(power * (1 + current_star)) AS total_power,
                    SUM(health * (1 + current_star)) AS total_health,
                    SUM(mana * (1 + current_star)) AS total_mana,
                    SUM(physical_attack * (1 + current_star)) AS total_physical_attack,
                    SUM(physical_defense * (1 + current_star)) AS total_physical_defense,
                    SUM(magical_attack * (1 + current_star)) AS total_magical_attack,
                    SUM(magical_defense * (1 + current_star)) AS total_magical_defense,
                    SUM(chemical_attack * (1 + current_star)) AS total_chemical_attack,
                    SUM(chemical_defense * (1 + current_star)) AS total_chemical_defense,
                    SUM(atomic_attack * (1 + current_star)) AS total_atomic_attack,
                    SUM(atomic_defense * (1 + current_star)) AS total_atomic_defense,
                    SUM(mental_attack * (1 + current_star)) AS total_mental_attack,
                    SUM(mental_defense * (1 + current_star)) AS total_mental_defense,
                    SUM(speed * (1 + current_star)) AS total_speed,
                    SUM(critical_damage_rate * (1 + current_star)) AS total_critical_damage_rate,
                    SUM(critical_rate * (1 + current_star)) AS total_critical_rate,
                    SUM(critical_resistance_rate * (1 + current_star)) AS total_critical_resistance_rate,
                    SUM(ignore_critical_rate * (1 + current_star)) AS total_ignore_critical_rate,
                    SUM(penetration_rate * (1 + current_star)) AS total_penetration_rate,
                    SUM(penetration_resistance_rate * (1 + current_star)) AS total_penetration_resistance_rate,
                    SUM(evasion_rate * (1 + current_star)) AS total_evasion_rate,
                    SUM(damage_absorption_rate * (1 + current_star)) AS total_damage_absorption_rate,
                    SUM(ignore_damage_absorption_rate * (1 + current_star)) AS total_ignore_damage_absorption_rate,
                    SUM(absorbed_damage_rate * (1 + current_star)) AS total_absorbed_damage_rate,
                    SUM(vitality_regeneration_rate * (1 + current_star)) AS total_vitality_regeneration_rate,
                    SUM(vitality_regeneration_resistance_rate * (1 + current_star)) AS total_vitality_regeneration_resistance_rate,
                    SUM(accuracy_rate * (1 + current_star)) AS total_accuracy_rate,
                    SUM(lifesteal_rate * (1 + current_star)) AS total_lifesteal_rate,
                    SUM(shield_strength * (1 + current_star)) AS total_shield_strength,
                    SUM(tenacity * (1 + current_star)) AS total_tenacity,
                    SUM(resistance_rate * (1 + current_star)) AS total_resistance_rate,
                    SUM(combo_rate * (1 + current_star)) AS total_combo_rate,
                    SUM(ignore_combo_rate * (1 + current_star)) AS total_ignore_combo_rate,
                    SUM(combo_damage_rate * (1 + current_star)) AS total_combo_damage_rate,
                    SUM(combo_resistance_rate * (1 + current_star)) AS total_combo_resistance_rate,
                    SUM(stun_rate * (1 + current_star)) AS total_stun_rate,
                    SUM(ignore_stun_rate * (1 + current_star)) AS total_ignore_stun_rate,
                    SUM(reflection_rate * (1 + current_star)) AS total_reflection_rate,
                    SUM(ignore_reflection_rate * (1 + current_star)) AS total_ignore_reflection_rate,
                    SUM(reflection_damage_rate * (1 + current_star)) AS total_reflection_damage_rate,
                    SUM(reflection_resistance_rate * (1 + current_star)) AS total_reflection_resistance_rate,
                    SUM(mana_regeneration_rate * (1 + current_star)) AS total_mana_regeneration_rate,
                    SUM(damage_to_different_faction_rate * (1 + current_star)) AS total_damage_to_different_faction_rate,
                    SUM(resistance_to_different_faction_rate * (1 + current_star)) AS total_resistance_to_different_faction_rate,
                    SUM(damage_to_same_faction_rate * (1 + current_star)) AS total_damage_to_same_faction_rate,
                    SUM(resistance_to_same_faction_rate * (1 + current_star)) AS total_resistance_to_same_faction_rate,
                    SUM(normal_damage_rate * (1 + current_star)) AS total_normal_damage_rate,
                    SUM(normal_resistance_rate * (1 + current_star)) AS total_normal_resistance_rate,
                    SUM(skill_damage_rate * (1 + current_star)) AS total_skill_damage_rate,
                    SUM(skill_resistance_rate * (1 + current_star)) AS total_skill_resistance_rate,
                    
                    SUM(percent_all_health * (1 + current_star)) AS total_percent_all_health,
                    SUM(percent_all_physical_attack * (1 + current_star)) AS total_percent_all_physical_attack,
                    SUM(percent_all_physical_defense * (1 + current_star)) AS total_percent_all_physical_defense,
                    SUM(percent_all_magical_attack * (1 + current_star)) AS total_percent_all_magical_attack,
                    SUM(percent_all_magical_defense * (1 + current_star)) AS total_percent_all_magical_defense,
                    SUM(percent_all_chemical_attack * (1 + current_star)) AS total_percent_all_chemical_attack,
                    SUM(percent_all_chemical_defense * (1 + current_star)) AS total_percent_all_chemical_defense,
                    SUM(percent_all_atomic_attack * (1 + current_star)) AS total_percent_all_atomic_attack,
                    SUM(percent_all_atomic_defense * (1 + current_star)) AS total_percent_all_atomic_defense,
                    SUM(percent_all_mental_attack * (1 + current_star)) AS total_percent_all_mental_attack,
                    SUM(percent_all_mental_defense * (1 + current_star)) AS total_percent_all_mental_defense
                FROM outfits_gallery 
                WHERE user_id = @user_id AND status = 'available';
            ";

                MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@user_id", userId);

                await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        sumOutfits.Power = reader["total_power"] as double? ?? 0;
                        sumOutfits.Health = reader["total_health"] as double? ?? 0;
                        sumOutfits.Mana = reader["total_mana"] as double? ?? 0f;

                        sumOutfits.PhysicalAttack = reader["total_physical_attack"] as double? ?? 0;
                        sumOutfits.PhysicalDefense = reader["total_physical_defense"] as double? ?? 0;
                        sumOutfits.MagicalAttack = reader["total_magical_attack"] as double? ?? 0;
                        sumOutfits.MagicalDefense = reader["total_magical_defense"] as double? ?? 0;
                        sumOutfits.ChemicalAttack = reader["total_chemical_attack"] as double? ?? 0;
                        sumOutfits.ChemicalDefense = reader["total_chemical_defense"] as double? ?? 0;
                        sumOutfits.AtomicAttack = reader["total_atomic_attack"] as double? ?? 0;
                        sumOutfits.AtomicDefense = reader["total_atomic_defense"] as double? ?? 0;
                        sumOutfits.MentalAttack = reader["total_mental_attack"] as double? ?? 0;
                        sumOutfits.MentalDefense = reader["total_mental_defense"] as double? ?? 0;

                        sumOutfits.Speed = reader["total_speed"] as double? ?? 0;
                        sumOutfits.CriticalDamageRate = reader["total_critical_damage_rate"] as double? ?? 0;
                        sumOutfits.CriticalRate = reader["total_critical_rate"] as double? ?? 0;
                        sumOutfits.CriticalResistanceRate = reader["total_critical_resistance_rate"] as double? ?? 0;

                        sumOutfits.IgnoreCriticalRate = reader["total_ignore_critical_rate"] as double? ?? 0;
                        sumOutfits.PenetrationRate = reader["total_penetration_rate"] as double? ?? 0;
                        sumOutfits.PenetrationResistanceRate = reader["total_penetration_resistance_rate"] as double? ?? 0;

                        sumOutfits.EvasionRate = reader["total_evasion_rate"] as double? ?? 0;
                        sumOutfits.DamageAbsorptionRate = reader["total_damage_absorption_rate"] as double? ?? 0;
                        sumOutfits.IgnoreDamageAbsorptionRate = reader["total_ignore_damage_absorption_rate"] as double? ?? 0;
                        sumOutfits.AbsorbedDamageRate = reader["total_absorbed_damage_rate"] as double? ?? 0;

                        sumOutfits.VitalityRegenerationRate = reader["total_vitality_regeneration_rate"] as double? ?? 0;
                        sumOutfits.VitalityRegenerationResistanceRate = reader["total_vitality_regeneration_resistance_rate"] as double? ?? 0;

                        sumOutfits.AccuracyRate = reader["total_accuracy_rate"] as double? ?? 0;
                        sumOutfits.LifestealRate = reader["total_lifesteal_rate"] as double? ?? 0;
                        sumOutfits.ShieldStrength = reader["total_shield_strength"] as double? ?? 0;

                        sumOutfits.Tenacity = reader["total_tenacity"] as double? ?? 0;
                        sumOutfits.ResistanceRate = reader["total_resistance_rate"] as double? ?? 0;

                        sumOutfits.ComboRate = reader["total_combo_rate"] as double? ?? 0;
                        sumOutfits.IgnoreComboRate = reader["total_ignore_combo_rate"] as double? ?? 0;
                        sumOutfits.ComboDamageRate = reader["total_combo_damage_rate"] as double? ?? 0;
                        sumOutfits.ComboResistanceRate = reader["total_combo_resistance_rate"] as double? ?? 0;

                        sumOutfits.StunRate = reader["total_stun_rate"] as double? ?? 0;
                        sumOutfits.IgnoreStunRate = reader["total_ignore_stun_rate"] as double? ?? 0;

                        sumOutfits.ReflectionRate = reader["total_reflection_rate"] as double? ?? 0;
                        sumOutfits.IgnoreReflectionRate = reader["total_ignore_reflection_rate"] as double? ?? 0;
                        sumOutfits.ReflectionDamageRate = reader["total_reflection_damage_rate"] as double? ?? 0;
                        sumOutfits.ReflectionResistanceRate = reader["total_reflection_resistance_rate"] as double? ?? 0;

                        sumOutfits.ManaRegenerationRate = reader["total_mana_regeneration_rate"] as double? ?? 0;

                        sumOutfits.DamageToDifferentFactionRate = reader["total_damage_to_different_faction_rate"] as double? ?? 0;
                        sumOutfits.ResistanceToDifferentFactionRate = reader["total_resistance_to_different_faction_rate"] as double? ?? 0;

                        sumOutfits.DamageToSameFactionRate = reader["total_damage_to_same_faction_rate"] as double? ?? 0;
                        sumOutfits.ResistanceToSameFactionRate = reader["total_resistance_to_same_faction_rate"] as double? ?? 0;

                        sumOutfits.NormalDamageRate = reader["total_normal_damage_rate"] as double? ?? 0;
                        sumOutfits.NormalResistanceRate = reader["total_normal_resistance_rate"] as double? ?? 0;

                        sumOutfits.SkillDamageRate = reader["total_skill_damage_rate"] as double? ?? 0;
                        sumOutfits.SkillResistanceRate = reader["total_skill_resistance_rate"] as double? ?? 0;

                        sumOutfits.PercentAllHealth = reader["total_percent_all_health"] as double? ?? 0;
                        sumOutfits.PercentAllPhysicalAttack = reader["total_percent_all_physical_attack"] as double? ?? 0;
                        sumOutfits.PercentAllPhysicalDefense = reader["total_percent_all_physical_defense"] as double? ?? 0;
                        sumOutfits.PercentAllMagicalAttack = reader["total_percent_all_magical_attack"] as double? ?? 0;
                        sumOutfits.PercentAllMagicalDefense = reader["total_percent_all_magical_defense"] as double? ?? 0;
                        sumOutfits.PercentAllChemicalAttack = reader["total_percent_all_chemical_attack"] as double? ?? 0;
                        sumOutfits.PercentAllChemicalDefense = reader["total_percent_all_chemical_defense"] as double? ?? 0;
                        sumOutfits.PercentAllAtomicAttack = reader["total_percent_all_atomic_attack"] as double? ?? 0;
                        sumOutfits.PercentAllAtomicDefense = reader["total_percent_all_atomic_defense"] as double? ?? 0;
                        sumOutfits.PercentAllMentalAttack = reader["total_percent_all_mental_attack"] as double? ?? 0;
                        sumOutfits.PercentAllMentalDefense = reader["total_percent_all_mental_defense"] as double? ?? 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("MySQL Error: " + ex.Message);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return sumOutfits;
    }
}