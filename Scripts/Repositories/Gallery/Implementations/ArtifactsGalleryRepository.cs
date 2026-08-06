using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

public class ArtifactsGalleryRepository : IArtifactsGalleryRepository
{
    public async Task<List<Artifacts>> GetArtifactsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Artifacts> artifacts = new List<Artifacts>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT c.*, 
                       CASE 
                           WHEN cg.artifact_id IS NULL THEN 'block' 
                           WHEN cg.status = 'pending' THEN 'pending' 
                           WHEN cg.status = 'available' THEN 'available' 
                       END AS status 
                FROM Artifacts c 
                LEFT JOIN artifacts_gallery cg 
                       ON c.id = cg.artifact_id AND cg.user_id = @userId 
                WHERE 1=1";
                
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
                            Artifacts Artifact = new Artifacts
                            {
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Rarity = reader.GetStringSafe("rare"),
                                Quality = reader.GetDoubleSafe("quality"),
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

                            artifacts.Add(Artifact);
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

        return artifacts;
    }
    public async Task<int> GetArtifactsCountAsync(string search, string rare)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"SELECT COUNT(*) FROM Artifacts WHERE 1=1";

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
    public async Task<InsertOrUpdateResult<Artifacts>> InsertArtifactGalleryAsync(string userId, string id, Artifacts artifact)
    {
        int percent = QualityEvaluatorHelper.CheckQuality(artifact.Rarity);
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // 1. Kiểm tra xem đã có trong Gallery chưa
            string checkSQL = @"
            SELECT COUNT(*) 
            FROM artifacts_gallery 
            WHERE user_id = @user_id AND artifact_id = @artifact_id;
        ";

            await using MySqlCommand checkCommand = new MySqlCommand(checkSQL, connection);
            checkCommand.Parameters.AddWithValue("@user_id", userId);
            checkCommand.Parameters.AddWithValue("@artifact_id", id);

            int recordCount = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

            // 2. Nếu ĐÃ TỒN TẠI: Bỏ qua, trả về OperationType = None (hoặc custom message)
            if (recordCount > 0)
            {
                return new InsertOrUpdateResult<Artifacts>
                {
                    Data = artifact,
                    OperationType = DatabaseOperationType.None,
                    Message = MessageConstants.THIS_RECORD_ALREADY_EXISTS_IN_GALLERY
                };
            }

            // 3. Nếu CHƯA CÓ: Thực hiện INSERT
            string insertSQL = @"
            INSERT INTO artifacts_gallery (
                user_id, artifact_id, status, current_star, temp_star, power, health, 
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
                @user_id, @artifact_id, @status, @current_star, @temp_star, @power, @health,
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
            insertCommand.Parameters.AddWithValue("@artifact_id", id);
            insertCommand.Parameters.AddWithValue("@status", "pending");
            insertCommand.Parameters.AddWithValue("@current_star", 0);
            insertCommand.Parameters.AddWithValue("@temp_star", 0);

            insertCommand.Parameters.AddWithValue("@power", artifact.Power);
            insertCommand.Parameters.AddWithValue("@health", artifact.Health);
            insertCommand.Parameters.AddWithValue("@physical_attack", artifact.PhysicalAttack);
            insertCommand.Parameters.AddWithValue("@physical_defense", artifact.PhysicalDefense);
            insertCommand.Parameters.AddWithValue("@magical_attack", artifact.MagicalAttack);
            insertCommand.Parameters.AddWithValue("@magical_defense", artifact.MagicalDefense);
            insertCommand.Parameters.AddWithValue("@chemical_attack", artifact.ChemicalAttack);
            insertCommand.Parameters.AddWithValue("@chemical_defense", artifact.ChemicalDefense);
            insertCommand.Parameters.AddWithValue("@atomic_attack", artifact.AtomicAttack);
            insertCommand.Parameters.AddWithValue("@atomic_defense", artifact.AtomicDefense);
            insertCommand.Parameters.AddWithValue("@mental_attack", artifact.MentalAttack);
            insertCommand.Parameters.AddWithValue("@mental_defense", artifact.MentalDefense);
            insertCommand.Parameters.AddWithValue("@speed", artifact.Speed);
            insertCommand.Parameters.AddWithValue("@critical_damage_rate", artifact.CriticalDamageRate);
            insertCommand.Parameters.AddWithValue("@critical_rate", artifact.CriticalRate);
            insertCommand.Parameters.AddWithValue("@critical_resistance_rate", artifact.CriticalResistanceRate);
            insertCommand.Parameters.AddWithValue("@ignore_critical_rate", artifact.IgnoreCriticalRate);
            insertCommand.Parameters.AddWithValue("@penetration_rate", artifact.PenetrationRate);
            insertCommand.Parameters.AddWithValue("@penetration_resistance_rate", artifact.PenetrationResistanceRate);
            insertCommand.Parameters.AddWithValue("@evasion_rate", artifact.EvasionRate);
            insertCommand.Parameters.AddWithValue("@damage_absorption_rate", artifact.DamageAbsorptionRate);
            insertCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", artifact.IgnoreDamageAbsorptionRate);
            insertCommand.Parameters.AddWithValue("@absorbed_damage_rate", artifact.AbsorbedDamageRate);
            insertCommand.Parameters.AddWithValue("@vitality_regeneration_rate", artifact.VitalityRegenerationRate);
            insertCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", artifact.VitalityRegenerationResistanceRate);
            insertCommand.Parameters.AddWithValue("@accuracy_rate", artifact.AccuracyRate);
            insertCommand.Parameters.AddWithValue("@lifesteal_rate", artifact.LifestealRate);
            insertCommand.Parameters.AddWithValue("@shield_strength", artifact.ShieldStrength);
            insertCommand.Parameters.AddWithValue("@tenacity", artifact.Tenacity);
            insertCommand.Parameters.AddWithValue("@resistance_rate", artifact.ResistanceRate);
            insertCommand.Parameters.AddWithValue("@combo_rate", artifact.ComboRate);
            insertCommand.Parameters.AddWithValue("@ignore_combo_rate", artifact.IgnoreComboRate);
            insertCommand.Parameters.AddWithValue("@combo_damage_rate", artifact.ComboDamageRate);
            insertCommand.Parameters.AddWithValue("@combo_resistance_rate", artifact.ComboResistanceRate);
            insertCommand.Parameters.AddWithValue("@stun_rate", artifact.StunRate);
            insertCommand.Parameters.AddWithValue("@ignore_stun_rate", artifact.IgnoreStunRate);
            insertCommand.Parameters.AddWithValue("@reflection_rate", artifact.ReflectionRate);
            insertCommand.Parameters.AddWithValue("@ignore_reflection_rate", artifact.IgnoreReflectionRate);
            insertCommand.Parameters.AddWithValue("@reflection_damage_rate", artifact.ReflectionDamageRate);
            insertCommand.Parameters.AddWithValue("@reflection_resistance_rate", artifact.ReflectionResistanceRate);
            insertCommand.Parameters.AddWithValue("@mana", artifact.Mana);
            insertCommand.Parameters.AddWithValue("@mana_regeneration_rate", artifact.ManaRegenerationRate);
            insertCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", artifact.DamageToDifferentFactionRate);
            insertCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", artifact.ResistanceToDifferentFactionRate);
            insertCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", artifact.DamageToSameFactionRate);
            insertCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", artifact.ResistanceToSameFactionRate);
            insertCommand.Parameters.AddWithValue("@normal_damage_rate", artifact.NormalDamageRate);
            insertCommand.Parameters.AddWithValue("@normal_resistance_rate", artifact.NormalResistanceRate);
            insertCommand.Parameters.AddWithValue("@skill_damage_rate", artifact.SkillDamageRate);
            insertCommand.Parameters.AddWithValue("@skill_resistance_rate", artifact.SkillResistanceRate);

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
            return InsertOrUpdateResult<Artifacts>.Inserted(artifact);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return InsertOrUpdateResult<Artifacts>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<List<Artifacts>>> InsertBatchArtifactsGalleryAsync(string userId, List<Artifacts> artifacts)
    {
        if (artifacts == null || artifacts.Count == 0)
        {
            return InsertOrUpdateResult<List<Artifacts>>.Inserted(new List<Artifacts>());
        }

        string connectionString = DatabaseConfig.ConnectionString;
        var insertedList = new List<Artifacts>();
        int batchSize = 300;
        int timeoutSeconds = 120;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Chia danh sách thành từng Chunk/Batch nhỏ để tránh bị quá giới hạn max_allowed_packet của MySQL
            for (int i = 0; i < artifacts.Count; i += batchSize)
            {
                var chunk = artifacts.Skip(i).Take(batchSize).ToList();

                // Sử dụng Transaction để đảm bảo tính toàn vẹn cho mỗi Batch
                await using MySqlTransaction transaction = await connection.BeginTransactionAsync();

                try
                {
                    var sb = new StringBuilder();

                    // INSERT IGNORE: Tự động bỏ qua các dòng đã trùng Primary Key (user_id, artifact_id)
                    sb.Append(@"
                    INSERT IGNORE INTO artifacts_gallery (
                        user_id, artifact_id, status, current_star, temp_star, power, health, 
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
                        (@user_id{suffix}, @artifact_id{suffix}, 'pending', 0, 0, @power{suffix}, @health{suffix},
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
                        command.Parameters.AddWithValue($"@artifact_id{suffix}", item.Id);

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

            return InsertOrUpdateResult<List<Artifacts>>.Inserted(insertedList);
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error Batch Insert: " + ex.Message);
            return InsertOrUpdateResult<List<Artifacts>>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateStatusArtifactGalleryAsync(string userId, string id, string status = "available")
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Thêm điều kiện (status IS NULL OR status != @status) để tránh update thừa khi status đã đúng sẵn
            string updateSQL = @"UPDATE artifacts_gallery 
                             SET status = @status 
                             WHERE user_id = @user_id 
                               AND artifact_id = @artifact_id
                               AND (status IS NULL OR status != @status);";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@artifact_id", id);
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
            Debug.LogError("Error UpdateStatusArtifactGallery: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<bool>> UpdateBatchStatusArtifactsGalleryAsync(string userId, string status = "available")
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Cập nhật tất cả bản ghi của user_id có status khác với status mới (tránh update thừa)
            string updateSQL = @"UPDATE artifacts_gallery 
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
            Debug.LogError("Error UpdateBatchStatusArtifactsGallery: " + ex.Message);
            return InsertOrUpdateResult<bool>.Failure(ex.Message);
        }
    }
    public async Task<InsertOrUpdateResult<double>> UpdateTempStarArtifactGalleryAsync(string userId, string artifactId, double star)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            string updateSQL = @"
            UPDATE artifacts_gallery 
            SET temp_star = @temp_star 
            WHERE user_id = @user_id 
              AND artifact_id = @artifact_id 
              AND (temp_star IS NULL OR temp_star < @temp_star);";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@artifact_id", artifactId);
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
    public async Task<InsertOrUpdateResult<double>> UpdateCurrentStarArtifactGalleryAsync(string userId, string artifactId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Bước 1: Update current_star = temp_star
            string updateSQL = @"
            UPDATE artifacts_gallery 
            SET current_star = temp_star
            WHERE user_id = @user_id 
              AND artifact_id = @artifact_id 
              AND temp_star > current_star;";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
            updateCommand.Parameters.AddWithValue("@user_id", userId);
            updateCommand.Parameters.AddWithValue("@artifact_id", artifactId);

            int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                // Bước 2: Lấy current_star mới ra để trả về cho Client / Service tính Stats
                string selectSQL = @"
                SELECT current_star 
                FROM artifacts_gallery 
                WHERE user_id = @user_id AND artifact_id = @artifact_id;";

                await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@user_id", userId);
                selectCommand.Parameters.AddWithValue("@artifact_id", artifactId);

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
    public async Task<InsertOrUpdateResult<List<(string ArtifactId, double CurrentStar)>>> UpdateBatchCurrentStarArtifactsGalleryAsync(string userId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using MySqlConnection connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            // Bước 1: Cập nhật tất cả bản ghi có temp_star > current_star của userId
            string updateSQL = @"
            UPDATE artifacts_gallery 
            SET current_star = temp_star
            WHERE user_id = @user_id 
              AND temp_star > current_star;";

            await using MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);
            updateCommand.Parameters.AddWithValue("@user_id", userId);

            int rowsAffected = await updateCommand.ExecuteNonQueryAsync();

            if (rowsAffected > 0)
            {
                // Bước 2: Lấy danh sách artifact_id và current_star vừa được cập nhật (current_star == temp_star)
                string selectSQL = @"
                SELECT artifact_id, current_star 
                FROM artifacts_gallery 
                WHERE user_id = @user_id 
                  AND current_star = temp_star;";

                await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@user_id", userId);

                await using MySqlDataReader reader = await selectCommand.ExecuteReaderAsync();
                var updatedList = new List<(string ArtifactId, double CurrentStar)>();

                while (await reader.ReadAsync())
                {
                    string id = reader.GetString("artifact_id");
                    double star = reader.GetDouble("current_star");
                    updatedList.Add((id, star));
                }

                return InsertOrUpdateResult<List<(string ArtifactId, double CurrentStar)>>.Updated(updatedList);
            }

            return new InsertOrUpdateResult<List<(string ArtifactId, double CurrentStar)>>
            {
                Data = new List<(string ArtifactId, double CurrentStar)>(),
                OperationType = DatabaseOperationType.None,
                Message = MessageConstants.NOTHING_WAS_UPDATED
            };
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error: " + ex.Message);
            return InsertOrUpdateResult<List<(string ArtifactId, double CurrentStar)>>.Failure(ex.Message);
        }
    }
    public async Task<Artifacts> GetArtifactCollectionByIdAsync(string userId, string objectId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using MySqlConnection connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            string selectSQL = @"
            SELECT mg.* 
            FROM artifacts_gallery mg 
            WHERE mg.user_id = @userId AND mg.artifact_id = @objectId AND mg.status = 'available';";

            await using MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
            selectCommand.Parameters.AddWithValue("@userId", userId);
            selectCommand.Parameters.AddWithValue("@objectId", objectId);

            await using var reader = await selectCommand.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                // Trả về object đã map
                return new Artifacts
                {
                    Id = reader.GetStringSafe("artifact_id"),
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
    public async Task UpdateArtifactGalleryPowerAsync(string userId, string id, Artifacts artifact)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"UPDATE artifacts_gallery
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
                AND artifact_id = @artifact_id;
            ";

                MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection);

                // IDs
                updateCommand.Parameters.AddWithValue("@user_id", userId);
                updateCommand.Parameters.AddWithValue("@artifact_id", id);

                // Base flags
                updateCommand.Parameters.AddWithValue("@status", "pending");
                updateCommand.Parameters.AddWithValue("@current_star", 0);

                // Stats
                updateCommand.Parameters.AddWithValue("@power", artifact.Power);
                updateCommand.Parameters.AddWithValue("@health", artifact.Health);
                updateCommand.Parameters.AddWithValue("@physical_attack", artifact.PhysicalAttack);
                updateCommand.Parameters.AddWithValue("@physical_defense", artifact.PhysicalDefense);
                updateCommand.Parameters.AddWithValue("@magical_attack", artifact.MagicalAttack);
                updateCommand.Parameters.AddWithValue("@magical_defense", artifact.MagicalDefense);
                updateCommand.Parameters.AddWithValue("@chemical_attack", artifact.ChemicalAttack);
                updateCommand.Parameters.AddWithValue("@chemical_defense", artifact.ChemicalDefense);
                updateCommand.Parameters.AddWithValue("@atomic_attack", artifact.AtomicAttack);
                updateCommand.Parameters.AddWithValue("@atomic_defense", artifact.AtomicDefense);
                updateCommand.Parameters.AddWithValue("@mental_attack", artifact.MagicalAttack);
                updateCommand.Parameters.AddWithValue("@mental_defense", artifact.MagicalDefense);
                updateCommand.Parameters.AddWithValue("@speed", artifact.Speed);
                updateCommand.Parameters.AddWithValue("@critical_damage_rate", artifact.CriticalDamageRate);
                updateCommand.Parameters.AddWithValue("@critical_rate", artifact.CriticalRate);
                updateCommand.Parameters.AddWithValue("@critical_resistance_rate", artifact.CriticalResistanceRate);
                updateCommand.Parameters.AddWithValue("@ignore_critical_rate", artifact.IgnoreCriticalRate);
                updateCommand.Parameters.AddWithValue("@penetration_rate", artifact.PenetrationRate);
                updateCommand.Parameters.AddWithValue("@penetration_resistance_rate", artifact.PenetrationResistanceRate);
                updateCommand.Parameters.AddWithValue("@evasion_rate", artifact.EvasionRate);
                updateCommand.Parameters.AddWithValue("@damage_absorption_rate", artifact.DamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@ignore_damage_absorption_rate", artifact.IgnoreDamageAbsorptionRate);
                updateCommand.Parameters.AddWithValue("@absorbed_damage_rate", artifact.AbsorbedDamageRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_rate", artifact.VitalityRegenerationRate);
                updateCommand.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", artifact.VitalityRegenerationResistanceRate);
                updateCommand.Parameters.AddWithValue("@accuracy_rate", artifact.AccuracyRate);
                updateCommand.Parameters.AddWithValue("@lifesteal_rate", artifact.LifestealRate);
                updateCommand.Parameters.AddWithValue("@shield_strength", artifact.ShieldStrength);
                updateCommand.Parameters.AddWithValue("@tenacity", artifact.Tenacity);
                updateCommand.Parameters.AddWithValue("@resistance_rate", artifact.ResistanceRate);
                updateCommand.Parameters.AddWithValue("@combo_rate", artifact.ComboRate);
                updateCommand.Parameters.AddWithValue("@ignore_combo_rate", artifact.IgnoreComboRate);
                updateCommand.Parameters.AddWithValue("@combo_damage_rate", artifact.ComboDamageRate);
                updateCommand.Parameters.AddWithValue("@combo_resistance_rate", artifact.ComboResistanceRate);
                updateCommand.Parameters.AddWithValue("@stun_rate", artifact.StunRate);
                updateCommand.Parameters.AddWithValue("@ignore_stun_rate", artifact.IgnoreStunRate);
                updateCommand.Parameters.AddWithValue("@reflection_rate", artifact.ReflectionRate);
                updateCommand.Parameters.AddWithValue("@ignore_reflection_rate", artifact.IgnoreReflectionRate);
                updateCommand.Parameters.AddWithValue("@reflection_damage_rate", artifact.ReflectionDamageRate);
                updateCommand.Parameters.AddWithValue("@reflection_resistance_rate", artifact.ReflectionResistanceRate);
                updateCommand.Parameters.AddWithValue("@mana", artifact.Mana);
                updateCommand.Parameters.AddWithValue("@mana_regeneration_rate", artifact.ManaRegenerationRate);
                updateCommand.Parameters.AddWithValue("@damage_to_different_faction_rate", artifact.DamageToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_different_faction_rate", artifact.ResistanceToDifferentFactionRate);
                updateCommand.Parameters.AddWithValue("@damage_to_same_faction_rate", artifact.DamageToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@resistance_to_same_faction_rate", artifact.ResistanceToSameFactionRate);
                updateCommand.Parameters.AddWithValue("@normal_damage_rate", artifact.NormalDamageRate);
                updateCommand.Parameters.AddWithValue("@normal_resistance_rate", artifact.NormalResistanceRate);
                updateCommand.Parameters.AddWithValue("@skill_damage_rate", artifact.SkillDamageRate);
                updateCommand.Parameters.AddWithValue("@skill_resistance_rate", artifact.SkillResistanceRate);

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
    public async Task<Artifacts> SumPowerArtifactsGalleryAsync(string userId)
    {
        Artifacts sumArtifacts = new Artifacts();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT 
                -- Multiplier: Nếu current_star = 0 thì tính là 1, ngược lại lấy current_star
                    SUM(power * IF(current_star = 0, 1, current_star)) AS total_power,
                    SUM(health * IF(current_star = 0, 1, current_star)) AS total_health,
                    SUM(mana * IF(current_star = 0, 1, current_star)) AS total_mana,
                    
                    SUM(physical_attack * IF(current_star = 0, 1, current_star)) AS total_physical_attack,
                    SUM(physical_defense * IF(current_star = 0, 1, current_star)) AS total_physical_defense,
                    SUM(magical_attack * IF(current_star = 0, 1, current_star)) AS total_magical_attack,
                    SUM(magical_defense * IF(current_star = 0, 1, current_star)) AS total_magical_defense,
                    SUM(chemical_attack * IF(current_star = 0, 1, current_star)) AS total_chemical_attack,
                    SUM(chemical_defense * IF(current_star = 0, 1, current_star)) AS total_chemical_defense,
                    SUM(atomic_attack * IF(current_star = 0, 1, current_star)) AS total_atomic_attack,
                    SUM(atomic_defense * IF(current_star = 0, 1, current_star)) AS total_atomic_defense,
                    SUM(mental_attack * IF(current_star = 0, 1, current_star)) AS total_mental_attack,
                    SUM(mental_defense * IF(current_star = 0, 1, current_star)) AS total_mental_defense,
                    
                    SUM(speed * IF(current_star = 0, 1, current_star)) AS total_speed,
                    SUM(critical_damage_rate * IF(current_star = 0, 1, current_star)) AS total_critical_damage_rate,
                    SUM(critical_rate * IF(current_star = 0, 1, current_star)) AS total_critical_rate,
                    SUM(critical_resistance_rate * IF(current_star = 0, 1, current_star)) AS total_critical_resistance_rate,
                    SUM(ignore_critical_rate * IF(current_star = 0, 1, current_star)) AS total_ignore_critical_rate,
                    
                    SUM(penetration_rate * IF(current_star = 0, 1, current_star)) AS total_penetration_rate,
                    SUM(penetration_resistance_rate * IF(current_star = 0, 1, current_star)) AS total_penetration_resistance_rate,
                    SUM(evasion_rate * IF(current_star = 0, 1, current_star)) AS total_evasion_rate,
                    SUM(damage_absorption_rate * IF(current_star = 0, 1, current_star)) AS total_damage_absorption_rate,
                    SUM(ignore_damage_absorption_rate * IF(current_star = 0, 1, current_star)) AS total_ignore_damage_absorption_rate,
                    SUM(absorbed_damage_rate * IF(current_star = 0, 1, current_star)) AS total_absorbed_damage_rate,
                    
                    SUM(vitality_regeneration_rate * IF(current_star = 0, 1, current_star)) AS total_vitality_regeneration_rate,
                    SUM(vitality_regeneration_resistance_rate * IF(current_star = 0, 1, current_star)) AS total_vitality_regeneration_resistance_rate,
                    SUM(accuracy_rate * IF(current_star = 0, 1, current_star)) AS total_accuracy_rate,
                    SUM(lifesteal_rate * IF(current_star = 0, 1, current_star)) AS total_lifesteal_rate,
                    SUM(shield_strength * IF(current_star = 0, 1, current_star)) AS total_shield_strength,
                    SUM(tenacity * IF(current_star = 0, 1, current_star)) AS total_tenacity,
                    SUM(resistance_rate * IF(current_star = 0, 1, current_star)) AS total_resistance_rate,
                    
                    SUM(combo_rate * IF(current_star = 0, 1, current_star)) AS total_combo_rate,
                    SUM(ignore_combo_rate * IF(current_star = 0, 1, current_star)) AS total_ignore_combo_rate,
                    SUM(combo_damage_rate * IF(current_star = 0, 1, current_star)) AS total_combo_damage_rate,
                    SUM(combo_resistance_rate * IF(current_star = 0, 1, current_star)) AS total_combo_resistance_rate,
                    
                    SUM(stun_rate * IF(current_star = 0, 1, current_star)) AS total_stun_rate,
                    SUM(ignore_stun_rate * IF(current_star = 0, 1, current_star)) AS total_ignore_stun_rate,
                    SUM(reflection_rate * IF(current_star = 0, 1, current_star)) AS total_reflection_rate,
                    SUM(ignore_reflection_rate * IF(current_star = 0, 1, current_star)) AS total_ignore_reflection_rate,
                    SUM(reflection_damage_rate * IF(current_star = 0, 1, current_star)) AS total_reflection_damage_rate,
                    SUM(reflection_resistance_rate * IF(current_star = 0, 1, current_star)) AS total_reflection_resistance_rate,
                    SUM(mana_regeneration_rate * IF(current_star = 0, 1, current_star)) AS total_mana_regeneration_rate,
                    
                    SUM(damage_to_different_faction_rate * IF(current_star = 0, 1, current_star)) AS total_damage_to_different_faction_rate,
                    SUM(resistance_to_different_faction_rate * IF(current_star = 0, 1, current_star)) AS total_resistance_to_different_faction_rate,
                    SUM(damage_to_same_faction_rate * IF(current_star = 0, 1, current_star)) AS total_damage_to_same_faction_rate,
                    SUM(resistance_to_same_faction_rate * IF(current_star = 0, 1, current_star)) AS total_resistance_to_same_faction_rate,
                    
                    SUM(normal_damage_rate * IF(current_star = 0, 1, current_star)) AS total_normal_damage_rate,
                    SUM(normal_resistance_rate * IF(current_star = 0, 1, current_star)) AS total_normal_resistance_rate,
                    SUM(skill_damage_rate * IF(current_star = 0, 1, current_star)) AS total_skill_damage_rate,
                    SUM(skill_resistance_rate * IF(current_star = 0, 1, current_star)) AS total_skill_resistance_rate,
                    
                    SUM(percent_all_health * IF(current_star = 0, 1, current_star)) AS total_percent_all_health,
                    SUM(percent_all_physical_attack * IF(current_star = 0, 1, current_star)) AS total_percent_all_physical_attack,
                    SUM(percent_all_physical_defense * IF(current_star = 0, 1, current_star)) AS total_percent_all_physical_defense,
                    SUM(percent_all_magical_attack * IF(current_star = 0, 1, current_star)) AS total_percent_all_magical_attack,
                    SUM(percent_all_magical_defense * IF(current_star = 0, 1, current_star)) AS total_percent_all_magical_defense,
                    SUM(percent_all_chemical_attack * IF(current_star = 0, 1, current_star)) AS total_percent_all_chemical_attack,
                    SUM(percent_all_chemical_defense * IF(current_star = 0, 1, current_star)) AS total_percent_all_chemical_defense,
                    SUM(percent_all_atomic_attack * IF(current_star = 0, 1, current_star)) AS total_percent_all_atomic_attack,
                    SUM(percent_all_atomic_defense * IF(current_star = 0, 1, current_star)) AS total_percent_all_atomic_defense,
                    SUM(percent_all_mental_attack * IF(current_star = 0, 1, current_star)) AS total_percent_all_mental_attack,
                    SUM(percent_all_mental_defense * IF(current_star = 0, 1, current_star)) AS total_percent_all_mental_defense
                FROM artifacts_gallery 
                WHERE user_id = @user_id AND status = 'available';
            ";

                MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection);
                selectCommand.Parameters.AddWithValue("@user_id", userId);

                await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        sumArtifacts.Power = reader["total_power"] as double? ?? 0;
                        sumArtifacts.Health = reader["total_health"] as double? ?? 0;
                        sumArtifacts.Mana = reader["total_mana"] as double? ?? 0f;

                        sumArtifacts.PhysicalAttack = reader["total_physical_attack"] as double? ?? 0;
                        sumArtifacts.PhysicalDefense = reader["total_physical_defense"] as double? ?? 0;
                        sumArtifacts.MagicalAttack = reader["total_magical_attack"] as double? ?? 0;
                        sumArtifacts.MagicalDefense = reader["total_magical_defense"] as double? ?? 0;
                        sumArtifacts.ChemicalAttack = reader["total_chemical_attack"] as double? ?? 0;
                        sumArtifacts.ChemicalDefense = reader["total_chemical_defense"] as double? ?? 0;
                        sumArtifacts.AtomicAttack = reader["total_atomic_attack"] as double? ?? 0;
                        sumArtifacts.AtomicDefense = reader["total_atomic_defense"] as double? ?? 0;
                        sumArtifacts.MentalAttack = reader["total_mental_attack"] as double? ?? 0;
                        sumArtifacts.MentalDefense = reader["total_mental_defense"] as double? ?? 0;

                        sumArtifacts.Speed = reader["total_speed"] as double? ?? 0;
                        sumArtifacts.CriticalDamageRate = reader["total_critical_damage_rate"] as double? ?? 0;
                        sumArtifacts.CriticalRate = reader["total_critical_rate"] as double? ?? 0;
                        sumArtifacts.CriticalResistanceRate = reader["total_critical_resistance_rate"] as double? ?? 0;

                        sumArtifacts.IgnoreCriticalRate = reader["total_ignore_critical_rate"] as double? ?? 0;
                        sumArtifacts.PenetrationRate = reader["total_penetration_rate"] as double? ?? 0;
                        sumArtifacts.PenetrationResistanceRate = reader["total_penetration_resistance_rate"] as double? ?? 0;

                        sumArtifacts.EvasionRate = reader["total_evasion_rate"] as double? ?? 0;
                        sumArtifacts.DamageAbsorptionRate = reader["total_damage_absorption_rate"] as double? ?? 0;
                        sumArtifacts.IgnoreDamageAbsorptionRate = reader["total_ignore_damage_absorption_rate"] as double? ?? 0;
                        sumArtifacts.AbsorbedDamageRate = reader["total_absorbed_damage_rate"] as double? ?? 0;

                        sumArtifacts.VitalityRegenerationRate = reader["total_vitality_regeneration_rate"] as double? ?? 0;
                        sumArtifacts.VitalityRegenerationResistanceRate = reader["total_vitality_regeneration_resistance_rate"] as double? ?? 0;

                        sumArtifacts.AccuracyRate = reader["total_accuracy_rate"] as double? ?? 0;
                        sumArtifacts.LifestealRate = reader["total_lifesteal_rate"] as double? ?? 0;
                        sumArtifacts.ShieldStrength = reader["total_shield_strength"] as double? ?? 0;

                        sumArtifacts.Tenacity = reader["total_tenacity"] as double? ?? 0;
                        sumArtifacts.ResistanceRate = reader["total_resistance_rate"] as double? ?? 0;

                        sumArtifacts.ComboRate = reader["total_combo_rate"] as double? ?? 0;
                        sumArtifacts.IgnoreComboRate = reader["total_ignore_combo_rate"] as double? ?? 0;
                        sumArtifacts.ComboDamageRate = reader["total_combo_damage_rate"] as double? ?? 0;
                        sumArtifacts.ComboResistanceRate = reader["total_combo_resistance_rate"] as double? ?? 0;

                        sumArtifacts.StunRate = reader["total_stun_rate"] as double? ?? 0;
                        sumArtifacts.IgnoreStunRate = reader["total_ignore_stun_rate"] as double? ?? 0;

                        sumArtifacts.ReflectionRate = reader["total_reflection_rate"] as double? ?? 0;
                        sumArtifacts.IgnoreReflectionRate = reader["total_ignore_reflection_rate"] as double? ?? 0;
                        sumArtifacts.ReflectionDamageRate = reader["total_reflection_damage_rate"] as double? ?? 0;
                        sumArtifacts.ReflectionResistanceRate = reader["total_reflection_resistance_rate"] as double? ?? 0;

                        sumArtifacts.ManaRegenerationRate = reader["total_mana_regeneration_rate"] as double? ?? 0;

                        sumArtifacts.DamageToDifferentFactionRate = reader["total_damage_to_different_faction_rate"] as double? ?? 0;
                        sumArtifacts.ResistanceToDifferentFactionRate = reader["total_resistance_to_different_faction_rate"] as double? ?? 0;

                        sumArtifacts.DamageToSameFactionRate = reader["total_damage_to_same_faction_rate"] as double? ?? 0;
                        sumArtifacts.ResistanceToSameFactionRate = reader["total_resistance_to_same_faction_rate"] as double? ?? 0;

                        sumArtifacts.NormalDamageRate = reader["total_normal_damage_rate"] as double? ?? 0;
                        sumArtifacts.NormalResistanceRate = reader["total_normal_resistance_rate"] as double? ?? 0;

                        sumArtifacts.SkillDamageRate = reader["total_skill_damage_rate"] as double? ?? 0;
                        sumArtifacts.SkillResistanceRate = reader["total_skill_resistance_rate"] as double? ?? 0;

                        sumArtifacts.PercentAllHealth = reader["total_percent_all_health"] as double? ?? 0;
                        sumArtifacts.PercentAllPhysicalAttack = reader["total_percent_all_physical_attack"] as double? ?? 0;
                        sumArtifacts.PercentAllPhysicalDefense = reader["total_percent_all_physical_defense"] as double? ?? 0;
                        sumArtifacts.PercentAllMagicalAttack = reader["total_percent_all_magical_attack"] as double? ?? 0;
                        sumArtifacts.PercentAllMagicalDefense = reader["total_percent_all_magical_defense"] as double? ?? 0;
                        sumArtifacts.PercentAllChemicalAttack = reader["total_percent_all_chemical_attack"] as double? ?? 0;
                        sumArtifacts.PercentAllChemicalDefense = reader["total_percent_all_chemical_defense"] as double? ?? 0;
                        sumArtifacts.PercentAllAtomicAttack = reader["total_percent_all_atomic_attack"] as double? ?? 0;
                        sumArtifacts.PercentAllAtomicDefense = reader["total_percent_all_atomic_defense"] as double? ?? 0;
                        sumArtifacts.PercentAllMentalAttack = reader["total_percent_all_mental_attack"] as double? ?? 0;
                        sumArtifacts.PercentAllMentalDefense = reader["total_percent_all_mental_defense"] as double? ?? 0;
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

        return sumArtifacts;
    }
}