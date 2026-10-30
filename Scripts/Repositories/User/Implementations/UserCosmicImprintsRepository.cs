using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;
public class UserCosmicImprintsRepository : IUserCosmicImprintsRepository
{
    public async Task<UserCosmicImprints> GetUserCosmicImprintsAsync(string userId, string id)
    {
        UserCosmicImprints userCosmicImprint = new UserCosmicImprints();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT uc.*
                FROM user_hisns uc INNER JOIN hisns c ON c.id = uc.hisn_id
                WHERE uc.user_id = @user_id AND uc.hisn_id = @hisn_id AND c.is_active = TRUE AND c.is_deleted = FALSE;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);
                    selectCommand.Parameters.AddWithValue("@hisn_id", id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userCosmicImprint.Id = reader.GetStringSafe("hisn_id");
                            userCosmicImprint.Level = reader.GetIntSafe("hisn_level");
                            userCosmicImprint.Power = reader.GetDoubleSafe("power");
                            userCosmicImprint.Health = reader.GetDoubleSafe("health");
                            userCosmicImprint.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            userCosmicImprint.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            userCosmicImprint.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            userCosmicImprint.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            userCosmicImprint.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            userCosmicImprint.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            userCosmicImprint.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            userCosmicImprint.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            userCosmicImprint.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            userCosmicImprint.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            userCosmicImprint.Speed = reader.GetDoubleSafe("speed");
                            userCosmicImprint.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            userCosmicImprint.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            userCosmicImprint.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            userCosmicImprint.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            userCosmicImprint.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            userCosmicImprint.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            userCosmicImprint.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            userCosmicImprint.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            userCosmicImprint.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            userCosmicImprint.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            userCosmicImprint.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            userCosmicImprint.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            userCosmicImprint.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            userCosmicImprint.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            userCosmicImprint.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            userCosmicImprint.Tenacity = reader.GetDoubleSafe("tenacity");
                            userCosmicImprint.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            userCosmicImprint.ComboRate = reader.GetDoubleSafe("combo_rate");
                            userCosmicImprint.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            userCosmicImprint.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            userCosmicImprint.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            userCosmicImprint.StunRate = reader.GetDoubleSafe("stun_rate");
                            userCosmicImprint.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            userCosmicImprint.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            userCosmicImprint.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            userCosmicImprint.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            userCosmicImprint.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            userCosmicImprint.Mana = reader.GetDoubleSafe("mana");
                            userCosmicImprint.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            userCosmicImprint.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            userCosmicImprint.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            userCosmicImprint.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            userCosmicImprint.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            userCosmicImprint.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            userCosmicImprint.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            userCosmicImprint.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            userCosmicImprint.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            userCosmicImprint.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            userCosmicImprint.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            userCosmicImprint.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            userCosmicImprint.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            userCosmicImprint.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            userCosmicImprint.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            userCosmicImprint.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            userCosmicImprint.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            userCosmicImprint.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            userCosmicImprint.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            userCosmicImprint.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
                return userCosmicImprint;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task<InsertOrUpdateResult<UserCosmicImprints>> InsertOrUpdateUserCosmicImprintsAsync(string userId, UserCosmicImprints userCosmicImprint, string type, List<RecipeItemDetailDTO> totalDeductions)
    {
        // 1. Guard Clauses
        if (userCosmicImprint == null || string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(type))
        {
            return InsertOrUpdateResult<UserCosmicImprints>.Failure("Dữ liệu đầu vào không hợp lệ.");
        }

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();

        try
        {
            // -------------------------------------------------------------------------------------
            // BƯỚC 1: NHÓM NGUYÊN LIỆU THEO OBJECT_TYPE VÀ KHẤU TRỪ TỐI ƯU (IN + CASE WHEN)
            // -------------------------------------------------------------------------------------
            if (totalDeductions != null && totalDeductions.Count > 0)
            {
                // Lọc các vật phẩm có số lượng > 0 và gom nhóm theo ObjectType
                var validDeductions = totalDeductions.Where(d => d.RequiredQuantity > 0).ToList();
                var groupedDeductions = validDeductions.GroupBy(d => d.ObjectType.ToUpper());

                foreach (var group in groupedDeductions)
                {
                    string objectType = group.Key;
                    var itemList = group.ToList();

                    string tableName = GetUserTableNameByObjectType(objectType);
                    string idColumnName = GetIdColumnNameByObjectType(objectType);

                    if (string.IsNullOrEmpty(tableName)) continue;

                    // 1.1 Tạo IN Clause để SELECT FOR UPDATE 1 lần cho ObjectType này
                    var objectIds = itemList.Select(i => i.ObjectId).Distinct().ToList();
                    string inClause = string.Join(",", objectIds.Select((_, idx) => $"@id{idx}"));

                    string checkStockSQL = $@"
                    SELECT {idColumnName} AS object_id, quantity 
                    FROM {tableName} 
                    WHERE user_id = @userId AND {idColumnName} IN ({inClause})
                    FOR UPDATE;";

                    var currentStockMap = new Dictionary<string, double>();

                    await using (var checkCommand = new MySqlCommand(checkStockSQL, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@userId", userId);
                        for (int i = 0; i < objectIds.Count; i++)
                        {
                            checkCommand.Parameters.AddWithValue($"@id{i}", objectIds[i]);
                        }

                        await using var reader = await checkCommand.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            string objId = reader.GetStringSafe("object_id");
                            double qty = reader.GetDoubleSafe("quantity");
                            currentStockMap[objId] = qty;
                        }
                    }

                    // 1.2 Kiểm tra xem người dùng có đủ số lượng cho tất cả vật phẩm thuộc ObjectType này không
                    foreach (var item in itemList)
                    {
                        if (!currentStockMap.TryGetValue(item.ObjectId, out double ownedQty) || ownedQty < item.RequiredQuantity)
                        {
                            await transaction.RollbackAsync();
                            return InsertOrUpdateResult<UserCosmicImprints>.Failure(
                                $"Thất bại: Không đủ nguyên liệu '{item.ObjectId}' ({objectType}). " +
                                $"Cần: {item.RequiredQuantity}, Hiện có: {(currentStockMap.ContainsKey(item.ObjectId) ? currentStockMap[item.ObjectId] : 0)}.");
                        }
                    }

                    // 1.3 Dùng CASE WHEN để UPDATE Bulk 1 lần duy nhất cho toàn bộ ObjectType này
                    var caseStatements = new List<string>();
                    for (int i = 0; i < itemList.Count; i++)
                    {
                        caseStatements.Add($"WHEN {idColumnName} = @updId{i} THEN quantity - @updQty{i}");
                    }

                    string bulkUpdateSQL = $@"
                    UPDATE {tableName}
                    SET quantity = CASE 
                        {string.Join(" ", caseStatements)}
                        ELSE quantity 
                    END
                    WHERE user_id = @userId AND {idColumnName} IN ({inClause});";

                    await using (var updateCommand = new MySqlCommand(bulkUpdateSQL, connection, transaction))
                    {
                        updateCommand.Parameters.AddWithValue("@userId", userId);
                        for (int i = 0; i < itemList.Count; i++)
                        {
                            updateCommand.Parameters.AddWithValue($"@updId{i}", itemList[i].ObjectId);
                            updateCommand.Parameters.AddWithValue($"@updQty{i}", itemList[i].RequiredQuantity);
                        }

                        // Re-bind tham số cho IN Clause
                        for (int i = 0; i < objectIds.Count; i++)
                        {
                            updateCommand.Parameters.AddWithValue($"@id{i}", objectIds[i]);
                        }

                        int affectedRows = await updateCommand.ExecuteNonQueryAsync();
                        if (affectedRows == 0)
                        {
                            await transaction.RollbackAsync();
                            return InsertOrUpdateResult<UserCosmicImprints>.Failure($"Thất bại khi cập nhật số lượng cho loại {objectType}.");
                        }
                    }
                }
            }

            // -------------------------------------------------------------------------------------
            // BƯỚC 2: UPSERT BẢNG USER_UNIVERSES
            // -------------------------------------------------------------------------------------
            string upsertSQL = @"
            INSERT INTO user_hisns (
                user_id, hisn_id, hisn_level, power, health, mana, speed,
                physical_attack, physical_defense, magical_attack, magical_defense, chemical_attack, chemical_defense,
                atomic_attack, atomic_defense, mental_attack, mental_defense,
                critical_damage_rate, critical_rate, critical_resistance_rate, ignore_critical_rate,
                penetration_rate, penetration_resistance_rate, evasion_rate,
                damage_absorption_rate, ignore_damage_absorption_rate, absorbed_damage_rate,
                vitality_regeneration_rate, vitality_regeneration_resistance_rate, accuracy_rate, lifesteal_rate,
                shield_strength, tenacity, resistance_rate,
                combo_rate, ignore_combo_rate, combo_damage_rate, combo_resistance_rate,
                stun_rate, ignore_stun_rate, reflection_rate, ignore_reflection_rate,
                reflection_damage_rate, reflection_resistance_rate, mana_regeneration_rate,
                damage_to_different_faction_rate, resistance_to_different_faction_rate,
                damage_to_same_faction_rate, resistance_to_same_faction_rate,
                normal_damage_rate, normal_resistance_rate, skill_damage_rate, skill_resistance_rate,
                percent_all_health, percent_all_physical_attack, percent_all_physical_defense,
                percent_all_magical_attack, percent_all_magical_defense, percent_all_chemical_attack, percent_all_chemical_defense,
                percent_all_atomic_attack, percent_all_atomic_defense, percent_all_mental_attack, percent_all_mental_defense
            )
            SELECT 
                @user_id, c.id, @hisn_level, @power, @health, @mana, @speed,
                @physical_attack, @physical_defense, @magical_attack, @magical_defense, @chemical_attack, @chemical_defense,
                @atomic_attack, @atomic_defense, @mental_attack, @mental_defense,
                @critical_damage_rate, @critical_rate, @critical_resistance_rate, @ignore_critical_rate,
                @penetration_rate, @penetration_resistance_rate, @evasion_rate,
                @damage_absorption_rate, @ignore_damage_absorption_rate, @absorbed_damage_rate,
                @vitality_regeneration_rate, @vitality_regeneration_resistance_rate, @accuracy_rate, @lifesteal_rate,
                @shield_strength, @tenacity, @resistance_rate,
                @combo_rate, @ignore_combo_rate, @combo_damage_rate, @combo_resistance_rate,
                @stun_rate, @ignore_stun_rate, @reflection_rate, @ignore_reflection_rate,
                @reflection_damage_rate, @reflection_resistance_rate, @mana_regeneration_rate,
                @damage_to_different_faction_rate, @resistance_to_different_faction_rate,
                @damage_to_same_faction_rate, @resistance_to_same_faction_rate,
                @normal_damage_rate, @normal_resistance_rate, @skill_damage_rate, @skill_resistance_rate,
                @percent_all_health, @percent_all_physical_attack, @percent_all_physical_defense,
                @percent_all_magical_attack, @percent_all_magical_defense, @percent_all_chemical_attack, @percent_all_chemical_defense,
                @percent_all_atomic_attack, @percent_all_atomic_defense, @percent_all_mental_attack, @percent_all_mental_defense
            FROM hisns c
            WHERE c.id = @hisn_id AND c.is_active = TRUE AND c.is_deleted = FALSE
            ON DUPLICATE KEY UPDATE
                hisn_level = VALUES(hisn_level), power = VALUES(power), health = VALUES(health), mana = VALUES(mana), speed = VALUES(speed),
                physical_attack = VALUES(physical_attack), physical_defense = VALUES(physical_defense),
                magical_attack = VALUES(magical_attack), magical_defense = VALUES(magical_defense),
                chemical_attack = VALUES(chemical_attack), chemical_defense = VALUES(chemical_defense),
                atomic_attack = VALUES(atomic_attack), atomic_defense = VALUES(atomic_defense),
                mental_attack = VALUES(mental_attack), mental_defense = VALUES(mental_defense),
                critical_damage_rate = VALUES(critical_damage_rate), critical_rate = VALUES(critical_rate),
                critical_resistance_rate = VALUES(critical_resistance_rate), ignore_critical_rate = VALUES(ignore_critical_rate),
                penetration_rate = VALUES(penetration_rate), penetration_resistance_rate = VALUES(penetration_resistance_rate),
                evasion_rate = VALUES(evasion_rate), damage_absorption_rate = VALUES(damage_absorption_rate),
                ignore_damage_absorption_rate = VALUES(ignore_damage_absorption_rate), absorbed_damage_rate = VALUES(absorbed_damage_rate),
                vitality_regeneration_rate = VALUES(vitality_regeneration_rate),
                vitality_regeneration_resistance_rate = VALUES(vitality_regeneration_resistance_rate),
                accuracy_rate = VALUES(accuracy_rate), lifesteal_rate = VALUES(lifesteal_rate),
                shield_strength = VALUES(shield_strength), tenacity = VALUES(tenacity),
                resistance_rate = VALUES(resistance_rate), combo_rate = VALUES(combo_rate),
                ignore_combo_rate = VALUES(ignore_combo_rate), combo_damage_rate = VALUES(combo_damage_rate),
                combo_resistance_rate = VALUES(combo_resistance_rate), stun_rate = VALUES(stun_rate),
                ignore_stun_rate = VALUES(ignore_stun_rate), reflection_rate = VALUES(reflection_rate),
                ignore_reflection_rate = VALUES(ignore_reflection_rate), reflection_damage_rate = VALUES(reflection_damage_rate),
                reflection_resistance_rate = VALUES(reflection_resistance_rate), mana_regeneration_rate = VALUES(mana_regeneration_rate),
                damage_to_different_faction_rate = VALUES(damage_to_different_faction_rate),
                resistance_to_different_faction_rate = VALUES(resistance_to_different_faction_rate),
                damage_to_same_faction_rate = VALUES(damage_to_same_faction_rate),
                resistance_to_same_faction_rate = VALUES(resistance_to_same_faction_rate),
                normal_damage_rate = VALUES(normal_damage_rate), normal_resistance_rate = VALUES(normal_resistance_rate),
                skill_damage_rate = VALUES(skill_damage_rate), skill_resistance_rate = VALUES(skill_resistance_rate),
                percent_all_health = VALUES(percent_all_health),
                percent_all_physical_attack = VALUES(percent_all_physical_attack), percent_all_physical_defense = VALUES(percent_all_physical_defense),
                percent_all_magical_attack = VALUES(percent_all_magical_attack), percent_all_magical_defense = VALUES(percent_all_magical_defense),
                percent_all_chemical_attack = VALUES(percent_all_chemical_attack), percent_all_chemical_defense = VALUES(percent_all_chemical_defense),
                percent_all_atomic_attack = VALUES(percent_all_atomic_attack), percent_all_atomic_defense = VALUES(percent_all_atomic_defense),
                percent_all_mental_attack = VALUES(percent_all_mental_attack), percent_all_mental_defense = VALUES(percent_all_mental_defense);";

            int rowsAffected = 0;
            await using (var command = new MySqlCommand(upsertSQL, connection, transaction))
            {
                AddAllParameters(command, userCosmicImprint, userId, type);
                rowsAffected = await command.ExecuteNonQueryAsync();
            }

            if (rowsAffected == 0)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<UserCosmicImprints>.Failure("Thất bại: CosmicImprint không tồn tại hoặc đã bị khóa/xóa.");
            }

            await transaction.CommitAsync();

            if (rowsAffected == 1)
            {
                return InsertOrUpdateResult<UserCosmicImprints>.Inserted(userCosmicImprint);
            }
            else
            {
                return InsertOrUpdateResult<UserCosmicImprints>.Updated(userCosmicImprint);
            }
        }
        catch (MySqlException ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError($"[InsertOrUpdateUserCosmicImprintsAsync MySqlException]: {ex.Message}");
            return InsertOrUpdateResult<UserCosmicImprints>.Failure($"Lỗi Database: {ex.Message}");
        }
        catch (System.Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError($"[InsertOrUpdateUserCosmicImprintsAsync Exception]: {ex.Message}");
            return InsertOrUpdateResult<UserCosmicImprints>.Failure($"Lỗi hệ thống: {ex.Message}");
        }
    }
    public async Task<UserCosmicImprints> GetSumUserCosmicImprintsAsync(string userId)
    {
        UserCosmicImprints userCosmicImprints = new UserCosmicImprints();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT 
                    SUM(power) AS total_power,
                    SUM(health) AS total_health,
                    SUM(mana) AS total_mana,
                    SUM(physical_attack) AS total_physical_attack,
                    SUM(physical_defense) AS total_physical_defense,
                    SUM(magical_attack) AS total_magical_attack,
                    SUM(magical_defense) AS total_magical_defense,
                    SUM(chemical_attack) AS total_chemical_attack,
                    SUM(chemical_defense) AS total_chemical_defense,
                    SUM(atomic_attack) AS total_atomic_attack,
                    SUM(atomic_defense) AS total_atomic_defense,
                    SUM(mental_attack) AS total_mental_attack,
                    SUM(mental_defense) AS total_mental_defense,
                    SUM(speed) AS total_speed,
                    SUM(critical_damage_rate) AS total_critical_damage_rate,
                    SUM(critical_rate) AS total_critical_rate,
                    SUM(critical_resistance_rate) AS total_critical_resistance_rate,
                    SUM(ignore_critical_rate) AS total_ignore_critical_rate,
                    SUM(penetration_rate) AS total_penetration_rate,
                    SUM(penetration_resistance_rate) AS total_penetration_resistance_rate,
                    SUM(evasion_rate) AS total_evasion_rate,
                    SUM(damage_absorption_rate) AS total_damage_absorption_rate,
                    SUM(ignore_damage_absorption_rate) AS total_ignore_damage_absorption_rate,
                    SUM(absorbed_damage_rate) AS total_absorbed_damage_rate,
                    SUM(vitality_regeneration_rate) AS total_vitality_regeneration_rate,
                    SUM(vitality_regeneration_resistance_rate) AS total_vitality_regeneration_resistance_rate,
                    SUM(accuracy_rate) AS total_accuracy_rate,
                    SUM(lifesteal_rate) AS total_lifesteal_rate,
                    SUM(shield_strength) AS total_shield_strength,
                    SUM(tenacity) AS total_tenacity,
                    SUM(resistance_rate) AS total_resistance_rate,
                    SUM(combo_rate) AS total_combo_rate,
                    SUM(ignore_combo_rate) AS total_ignore_combo_rate,
                    SUM(combo_damage_rate) AS total_combo_damage_rate,
                    SUM(combo_resistance_rate) AS total_combo_resistance_rate,
                    SUM(stun_rate) AS total_stun_rate,
                    SUM(ignore_stun_rate) AS total_ignore_stun_rate,
                    SUM(reflection_rate) AS total_reflection_rate,
                    SUM(ignore_reflection_rate) AS total_ignore_reflection_rate,
                    SUM(reflection_damage_rate) AS total_reflection_damage_rate,
                    SUM(reflection_resistance_rate) AS total_reflection_resistance_rate,
                    SUM(mana_regeneration_rate) AS total_mana_regeneration_rate,
                    SUM(damage_to_different_faction_rate) AS total_damage_to_different_faction_rate,
                    SUM(resistance_to_different_faction_rate) AS total_resistance_to_different_faction_rate,
                    SUM(damage_to_same_faction_rate) AS total_damage_to_same_faction_rate,
                    SUM(resistance_to_same_faction_rate) AS total_resistance_to_same_faction_rate,
                    SUM(normal_damage_rate) AS total_normal_damage_rate,
                    SUM(normal_resistance_rate) AS total_normal_resistance_rate,
                    SUM(skill_damage_rate) AS total_skill_damage_rate,
                    SUM(skill_resistance_rate) AS total_skill_resistance_rate,
                    SUM(percent_all_health) AS percent_all_health,
                    SUM(percent_all_physical_attack) AS percent_all_physical_attack,
                    SUM(percent_all_physical_defense) AS percent_all_physical_defense,
                    SUM(percent_all_magical_attack) AS percent_all_magical_attack,
                    SUM(percent_all_magical_defense) AS percent_all_magical_defense,
                    SUM(percent_all_chemical_attack) AS percent_all_chemical_attack,
                    SUM(percent_all_chemical_defense) AS percent_all_chemical_defense,
                    SUM(percent_all_atomic_attack) AS percent_all_atomic_attack,
                    SUM(percent_all_atomic_defense) AS percent_all_atomic_defense,
                    SUM(percent_all_mental_attack) AS percent_all_mental_attack,
                    SUM(percent_all_mental_defense) AS percent_all_mental_defense
                FROM user_hisns uc INNER JOIN hisns c ON c.id = uc.hisn_id 
                WHERE uc.user_id = @user_id AND c.is_active = TRUE AND c.is_deleted = FALSE;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userCosmicImprints.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            userCosmicImprints.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            userCosmicImprints.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            userCosmicImprints.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            userCosmicImprints.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            userCosmicImprints.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            userCosmicImprints.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            userCosmicImprints.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            userCosmicImprints.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            userCosmicImprints.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            userCosmicImprints.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            userCosmicImprints.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            userCosmicImprints.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            userCosmicImprints.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            userCosmicImprints.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            userCosmicImprints.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            userCosmicImprints.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            userCosmicImprints.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            userCosmicImprints.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            userCosmicImprints.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            userCosmicImprints.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            userCosmicImprints.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            userCosmicImprints.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            userCosmicImprints.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            userCosmicImprints.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            userCosmicImprints.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            userCosmicImprints.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            userCosmicImprints.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            userCosmicImprints.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            userCosmicImprints.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            userCosmicImprints.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            userCosmicImprints.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            userCosmicImprints.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            userCosmicImprints.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            userCosmicImprints.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            userCosmicImprints.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            userCosmicImprints.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            userCosmicImprints.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            userCosmicImprints.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            userCosmicImprints.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            userCosmicImprints.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            userCosmicImprints.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            userCosmicImprints.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            userCosmicImprints.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            userCosmicImprints.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            userCosmicImprints.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            userCosmicImprints.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            userCosmicImprints.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            userCosmicImprints.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            userCosmicImprints.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            userCosmicImprints.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            userCosmicImprints.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            userCosmicImprints.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            userCosmicImprints.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            userCosmicImprints.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            userCosmicImprints.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            userCosmicImprints.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            userCosmicImprints.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            userCosmicImprints.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            userCosmicImprints.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            userCosmicImprints.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return userCosmicImprints;
    }
    private void AddAllParameters(MySqlCommand command, UserCosmicImprints userCosmicImprint, string userId, string type)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@hisn_id", type);

        command.Parameters.AddWithValue("@hisn_level", userCosmicImprint.Level == 0 ? 1 : userCosmicImprint.Level);
        command.Parameters.AddWithValue("@power", userCosmicImprint.Power);
        command.Parameters.AddWithValue("@health", userCosmicImprint.Health);
        command.Parameters.AddWithValue("@mana", userCosmicImprint.Mana);
        command.Parameters.AddWithValue("@speed", userCosmicImprint.Speed);

        command.Parameters.AddWithValue("@physical_attack", userCosmicImprint.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", userCosmicImprint.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", userCosmicImprint.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", userCosmicImprint.MagicalDefense);

        command.Parameters.AddWithValue("@chemical_attack", userCosmicImprint.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", userCosmicImprint.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", userCosmicImprint.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", userCosmicImprint.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", userCosmicImprint.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", userCosmicImprint.MentalDefense);

        command.Parameters.AddWithValue("@critical_damage_rate", userCosmicImprint.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", userCosmicImprint.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", userCosmicImprint.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", userCosmicImprint.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", userCosmicImprint.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@penetration_rate", userCosmicImprint.PenetrationRate);
        command.Parameters.AddWithValue("@evasion_rate", userCosmicImprint.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", userCosmicImprint.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", userCosmicImprint.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", userCosmicImprint.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", userCosmicImprint.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", userCosmicImprint.VitalityRegenerationResistanceRate);

        command.Parameters.AddWithValue("@accuracy_rate", userCosmicImprint.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", userCosmicImprint.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", userCosmicImprint.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", userCosmicImprint.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", userCosmicImprint.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", userCosmicImprint.ComboRate);
        command.Parameters.AddWithValue("@reflection_rate", userCosmicImprint.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", userCosmicImprint.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", userCosmicImprint.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", userCosmicImprint.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", userCosmicImprint.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", userCosmicImprint.IgnoreStunRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", userCosmicImprint.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", userCosmicImprint.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", userCosmicImprint.ReflectionResistanceRate);

        command.Parameters.AddWithValue("@mana_regeneration_rate", userCosmicImprint.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", userCosmicImprint.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", userCosmicImprint.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", userCosmicImprint.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", userCosmicImprint.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", userCosmicImprint.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", userCosmicImprint.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", userCosmicImprint.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", userCosmicImprint.SkillResistanceRate);

        command.Parameters.AddWithValue("@percent_all_health", userCosmicImprint.PercentAllHealth);
        command.Parameters.AddWithValue("@percent_all_physical_attack", userCosmicImprint.PercentAllPhysicalAttack);
        command.Parameters.AddWithValue("@percent_all_physical_defense", userCosmicImprint.PercentAllPhysicalDefense);
        command.Parameters.AddWithValue("@percent_all_magical_attack", userCosmicImprint.PercentAllMagicalAttack);
        command.Parameters.AddWithValue("@percent_all_magical_defense", userCosmicImprint.PercentAllMagicalDefense);
        command.Parameters.AddWithValue("@percent_all_chemical_attack", userCosmicImprint.PercentAllChemicalAttack);
        command.Parameters.AddWithValue("@percent_all_chemical_defense", userCosmicImprint.PercentAllChemicalDefense);
        command.Parameters.AddWithValue("@percent_all_atomic_attack", userCosmicImprint.PercentAllAtomicAttack);
        command.Parameters.AddWithValue("@percent_all_atomic_defense", userCosmicImprint.PercentAllAtomicDefense);
        command.Parameters.AddWithValue("@percent_all_mental_attack", userCosmicImprint.PercentAllMentalAttack);
        command.Parameters.AddWithValue("@percent_all_mental_defense", userCosmicImprint.PercentAllMentalDefense);
    }
    /// <summary>
    /// Ánh xạ ObjectType sang Tên Bảng kho người dùng tương ứng (Sắp xếp A-Z)
    /// </summary>
    private string GetUserTableNameByObjectType(string objectType)
    {
        return objectType.ToUpper() switch
        {
            AppConstants.ObjectType.ACHIEVEMENTS => DataBaseConstants.Table.USER_ACHIEVEMENTS,
            AppConstants.ObjectType.ALCHEMIES => DataBaseConstants.Table.USER_ALCHEMIES,
            AppConstants.ObjectType.ARCHITECTURES => DataBaseConstants.Table.USER_ARCHITECTURES,
            AppConstants.ObjectType.ARTIFACTS => DataBaseConstants.Table.USER_ARTIFACTS,
            AppConstants.ObjectType.ARTWORKS => DataBaseConstants.Table.USER_ARTWORKS,
            AppConstants.ObjectType.AVATARS => DataBaseConstants.Table.USER_AVATARS,
            AppConstants.ObjectType.BADGES => DataBaseConstants.Table.USER_BADGES,
            AppConstants.ObjectType.BEVERAGES => DataBaseConstants.Table.USER_BEVERAGES,
            AppConstants.ObjectType.BOOKS => DataBaseConstants.Table.USER_BOOKS,
            AppConstants.ObjectType.BORDERS => DataBaseConstants.Table.USER_BORDERS,
            AppConstants.ObjectType.BUILDINGS => DataBaseConstants.Table.USER_BUILDINGS,
            AppConstants.ObjectType.CARD_ADMIRALS => DataBaseConstants.Table.USER_CARD_ADMIRALS,
            AppConstants.ObjectType.CARD_CAPTAINS => DataBaseConstants.Table.USER_CARD_CAPTAINS,
            AppConstants.ObjectType.CARD_COLONELS => DataBaseConstants.Table.USER_CARD_COLONELS,
            AppConstants.ObjectType.CARD_GENERALS => DataBaseConstants.Table.USER_CARD_GENERALS,
            AppConstants.ObjectType.CARD_HEROES => DataBaseConstants.Table.USER_CARD_HEROES,
            AppConstants.ObjectType.CARD_LIVES => DataBaseConstants.Table.USER_CARD_LIVES,
            AppConstants.ObjectType.CARD_MILITARIES => DataBaseConstants.Table.USER_CARD_MILITARIES,
            AppConstants.ObjectType.CARD_MONSTERS => DataBaseConstants.Table.USER_CARD_MONSTERS,
            AppConstants.ObjectType.CARD_SOLDIERS => DataBaseConstants.Table.USER_CARD_SOLDIERS,
            AppConstants.ObjectType.CARD_SPELLS => DataBaseConstants.Table.USER_CARD_SPELLS,
            AppConstants.ObjectType.COLLABORATION_EQUIPMENTS => DataBaseConstants.Table.USER_COLLABORATION_EQUIPMENTS,
            AppConstants.ObjectType.COLLABORATIONS => DataBaseConstants.Table.USER_COLLABORATIONS,
            AppConstants.ObjectType.CORES => DataBaseConstants.Table.USER_CORES,
            AppConstants.ObjectType.EMOJIS => DataBaseConstants.Table.USER_EMOJIS,
            AppConstants.ObjectType.EQUIPMENTS => DataBaseConstants.Table.USER_EQUIPMENTS,
            AppConstants.ObjectType.FASHIONS => DataBaseConstants.Table.USER_FASHIONS,
            AppConstants.ObjectType.FOODS => DataBaseConstants.Table.USER_FOODS,
            AppConstants.ObjectType.FORGES => DataBaseConstants.Table.USER_FORGES,
            AppConstants.ObjectType.FURNITURES => DataBaseConstants.Table.USER_FURNITURES,
            AppConstants.ObjectType.ITEMS => DataBaseConstants.Table.USER_ITEMS,
            AppConstants.ObjectType.MAGIC_FORMATION_CIRCLES => DataBaseConstants.Table.USER_MAGIC_FORMATION_CIRCLES,
            AppConstants.ObjectType.MECHA_BEASTS => DataBaseConstants.Table.USER_MECHA_BEASTS,
            AppConstants.ObjectType.MEDALS => DataBaseConstants.Table.USER_MEDALS,
            AppConstants.ObjectType.OUTFITS => DataBaseConstants.Table.USER_OUTFITS,
            AppConstants.ObjectType.PETS => DataBaseConstants.Table.USER_PETS,
            AppConstants.ObjectType.PLANTS => DataBaseConstants.Table.USER_PLANTS,
            AppConstants.ObjectType.PUPPETS => DataBaseConstants.Table.USER_PUPPETS,
            AppConstants.ObjectType.RELICS => DataBaseConstants.Table.USER_RELICS,
            AppConstants.ObjectType.ROBOTS => DataBaseConstants.Table.USER_ROBOTS,
            AppConstants.ObjectType.RUNES => DataBaseConstants.Table.USER_RUNES,
            AppConstants.ObjectType.SKILLS => DataBaseConstants.Table.USER_SKILLS,
            AppConstants.ObjectType.SPIRIT_BEASTS => DataBaseConstants.Table.USER_SPIRIT_BEASTS,
            AppConstants.ObjectType.SPIRIT_CARDS => DataBaseConstants.Table.USER_SPIRIT_CARDS,
            AppConstants.ObjectType.SYMBOLS => DataBaseConstants.Table.USER_SYMBOLS,
            AppConstants.ObjectType.TALISMANS => DataBaseConstants.Table.USER_TALISMANS,
            AppConstants.ObjectType.TECHNOLOGIES => DataBaseConstants.Table.USER_TECHNOLOGIES,
            AppConstants.ObjectType.TITLES => DataBaseConstants.Table.USER_TITLES,
            AppConstants.ObjectType.VEHICLES => DataBaseConstants.Table.USER_VEHICLES,
            AppConstants.ObjectType.WEAPONS => DataBaseConstants.Table.USER_WEAPONS,
            AppConstants.ObjectType.CURRENCIES => DataBaseConstants.Table.USER_CURRENCIES,

            _ => DataBaseConstants.Table.USER_ITEMS
        };
    }

    /// <summary>
    /// Ánh xạ Cột ID chính trong bảng kho tương ứng (Sắp xếp A-Z)
    /// </summary>
    private string GetIdColumnNameByObjectType(string objectType)
    {
        return objectType.ToUpper() switch
        {
            AppConstants.ObjectType.ACHIEVEMENTS => DataBaseConstants.Column.ACHIEVEMENT_ID,
            AppConstants.ObjectType.ALCHEMIES => DataBaseConstants.Column.ALCHEMY_ID,
            AppConstants.ObjectType.ARCHITECTURES => DataBaseConstants.Column.ARCHITECTURE_ID,
            AppConstants.ObjectType.ARTIFACTS => DataBaseConstants.Column.ARTIFACT_ID,
            AppConstants.ObjectType.ARTWORKS => DataBaseConstants.Column.ARTWORK_ID,
            AppConstants.ObjectType.AVATARS => DataBaseConstants.Column.AVATAR_ID,
            AppConstants.ObjectType.BADGES => DataBaseConstants.Column.BADGE_ID,
            AppConstants.ObjectType.BEVERAGES => DataBaseConstants.Column.BEVERAGE_ID,
            AppConstants.ObjectType.BOOKS => DataBaseConstants.Column.BOOK_ID,
            AppConstants.ObjectType.BORDERS => DataBaseConstants.Column.BORDER_ID,
            AppConstants.ObjectType.BUILDINGS => DataBaseConstants.Column.BUILDING_ID,
            AppConstants.ObjectType.CARD_ADMIRALS => DataBaseConstants.Column.CARD_ADMIRAL_ID,
            AppConstants.ObjectType.CARD_CAPTAINS => DataBaseConstants.Column.CARD_CAPTAIN_ID,
            AppConstants.ObjectType.CARD_COLONELS => DataBaseConstants.Column.CARD_COLONEL_ID,
            AppConstants.ObjectType.CARD_GENERALS => DataBaseConstants.Column.CARD_GENERAL_ID,
            AppConstants.ObjectType.CARD_HEROES => DataBaseConstants.Column.CARD_HERO_ID,
            AppConstants.ObjectType.CARD_LIVES => DataBaseConstants.Column.CARD_LIFE_ID,
            AppConstants.ObjectType.CARD_MILITARIES => DataBaseConstants.Column.CARD_MILITARY_ID,
            AppConstants.ObjectType.CARD_MONSTERS => DataBaseConstants.Column.CARD_MONSTER_ID,
            AppConstants.ObjectType.CARD_SOLDIERS => DataBaseConstants.Column.CARD_SOLDIER_ID,
            AppConstants.ObjectType.CARD_SPELLS => DataBaseConstants.Column.CARD_SPELL_ID,
            AppConstants.ObjectType.COLLABORATION_EQUIPMENTS => DataBaseConstants.Column.COLLABORATION_EQUIPMENT_ID,
            AppConstants.ObjectType.COLLABORATIONS => DataBaseConstants.Column.COLLABORATION_ID,
            AppConstants.ObjectType.CORES => DataBaseConstants.Column.CORE_ID,
            AppConstants.ObjectType.EMOJIS => DataBaseConstants.Column.EMOJI_ID,
            AppConstants.ObjectType.EQUIPMENTS => DataBaseConstants.Column.EQUIPMENT_ID,
            AppConstants.ObjectType.FASHIONS => DataBaseConstants.Column.FASHION_ID,
            AppConstants.ObjectType.FOODS => DataBaseConstants.Column.FOOD_ID,
            AppConstants.ObjectType.FORGES => DataBaseConstants.Column.FORGE_ID,
            AppConstants.ObjectType.FURNITURES => DataBaseConstants.Column.FURNITURE_ID,
            AppConstants.ObjectType.ITEMS => DataBaseConstants.Column.ITEM_ID,
            AppConstants.ObjectType.MAGIC_FORMATION_CIRCLES => DataBaseConstants.Column.MAGIC_FORMATION_CIRCLE_ID,
            AppConstants.ObjectType.MECHA_BEASTS => DataBaseConstants.Column.MECHA_BEAST_ID,
            AppConstants.ObjectType.MEDALS => DataBaseConstants.Column.MEDAL_ID,
            AppConstants.ObjectType.OUTFITS => DataBaseConstants.Column.OUTFIT_ID,
            AppConstants.ObjectType.PETS => DataBaseConstants.Column.PET_ID,
            AppConstants.ObjectType.PLANTS => DataBaseConstants.Column.PLANT_ID,
            AppConstants.ObjectType.PUPPETS => DataBaseConstants.Column.PUPPET_ID,
            AppConstants.ObjectType.RELICS => DataBaseConstants.Column.RELIC_ID,
            AppConstants.ObjectType.ROBOTS => DataBaseConstants.Column.ROBOT_ID,
            AppConstants.ObjectType.RUNES => DataBaseConstants.Column.RUNE_ID,
            AppConstants.ObjectType.SKILLS => DataBaseConstants.Column.SKILL_ID,
            AppConstants.ObjectType.SPIRIT_BEASTS => DataBaseConstants.Column.SPIRIT_BEAST_ID,
            AppConstants.ObjectType.SPIRIT_CARDS => DataBaseConstants.Column.SPIRIT_CARD_ID,
            AppConstants.ObjectType.SYMBOLS => DataBaseConstants.Column.SYMBOL_ID,
            AppConstants.ObjectType.TALISMANS => DataBaseConstants.Column.TALISMAN_ID,
            AppConstants.ObjectType.TECHNOLOGIES => DataBaseConstants.Column.TECHNOLOGY_ID,
            AppConstants.ObjectType.TITLES => DataBaseConstants.Column.TITLE_ID,
            AppConstants.ObjectType.VEHICLES => DataBaseConstants.Column.VEHICLE_ID,
            AppConstants.ObjectType.WEAPONS => DataBaseConstants.Column.WEAPON_ID,
            AppConstants.ObjectType.CURRENCIES => DataBaseConstants.Column.CURRENCY_ID,

            _ => DataBaseConstants.Column.ITEM_ID
        };
    }
}
