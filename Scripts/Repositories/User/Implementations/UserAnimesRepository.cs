using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;
public class UserAnimesRepository : IUserAnimesRepository
{
    public async Task<UserAnimes> GetUserAnimesAsync(string userId, string id)
    {
        UserAnimes userAnime = new UserAnimes();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT uc.*
                FROM user_animes uc INNER JOIN animes c ON c.id = uc.anime_id
                WHERE uc.user_id = @user_id AND uc.anime_id = @anime_id AND c.is_active = TRUE AND c.is_deleted = FALSE;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);
                    selectCommand.Parameters.AddWithValue("@anime_id", id);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userAnime.Id = reader.GetStringSafe("anime_id");
                            userAnime.Level = reader.GetIntSafe("anime_level");
                            userAnime.Power = reader.GetDoubleSafe("power");
                            userAnime.Health = reader.GetDoubleSafe("health");
                            userAnime.PhysicalAttack = reader.GetDoubleSafe("physical_attack");
                            userAnime.PhysicalDefense = reader.GetDoubleSafe("physical_defense");
                            userAnime.MagicalAttack = reader.GetDoubleSafe("magical_attack");
                            userAnime.MagicalDefense = reader.GetDoubleSafe("magical_defense");
                            userAnime.ChemicalAttack = reader.GetDoubleSafe("chemical_attack");
                            userAnime.ChemicalDefense = reader.GetDoubleSafe("chemical_defense");
                            userAnime.AtomicAttack = reader.GetDoubleSafe("atomic_attack");
                            userAnime.AtomicDefense = reader.GetDoubleSafe("atomic_defense");
                            userAnime.MentalAttack = reader.GetDoubleSafe("mental_attack");
                            userAnime.MentalDefense = reader.GetDoubleSafe("mental_defense");
                            userAnime.Speed = reader.GetDoubleSafe("speed");
                            userAnime.CriticalDamageRate = reader.GetDoubleSafe("critical_damage_rate");
                            userAnime.CriticalRate = reader.GetDoubleSafe("critical_rate");
                            userAnime.CriticalResistanceRate = reader.GetDoubleSafe("critical_resistance_rate");
                            userAnime.IgnoreCriticalRate = reader.GetDoubleSafe("ignore_critical_rate");
                            userAnime.PenetrationRate = reader.GetDoubleSafe("penetration_rate");
                            userAnime.PenetrationResistanceRate = reader.GetDoubleSafe("penetration_resistance_rate");
                            userAnime.EvasionRate = reader.GetDoubleSafe("evasion_rate");
                            userAnime.DamageAbsorptionRate = reader.GetDoubleSafe("damage_absorption_rate");
                            userAnime.IgnoreDamageAbsorptionRate = reader.GetDoubleSafe("ignore_damage_absorption_rate");
                            userAnime.AbsorbedDamageRate = reader.GetDoubleSafe("absorbed_damage_rate");
                            userAnime.VitalityRegenerationRate = reader.GetDoubleSafe("vitality_regeneration_rate");
                            userAnime.VitalityRegenerationResistanceRate = reader.GetDoubleSafe("vitality_regeneration_resistance_rate");
                            userAnime.AccuracyRate = reader.GetDoubleSafe("accuracy_rate");
                            userAnime.LifestealRate = reader.GetDoubleSafe("lifesteal_rate");
                            userAnime.ShieldStrength = reader.GetDoubleSafe("shield_strength");
                            userAnime.Tenacity = reader.GetDoubleSafe("tenacity");
                            userAnime.ResistanceRate = reader.GetDoubleSafe("resistance_rate");
                            userAnime.ComboRate = reader.GetDoubleSafe("combo_rate");
                            userAnime.IgnoreComboRate = reader.GetDoubleSafe("ignore_combo_rate");
                            userAnime.ComboDamageRate = reader.GetDoubleSafe("combo_damage_rate");
                            userAnime.ComboResistanceRate = reader.GetDoubleSafe("combo_resistance_rate");
                            userAnime.StunRate = reader.GetDoubleSafe("stun_rate");
                            userAnime.IgnoreStunRate = reader.GetDoubleSafe("ignore_stun_rate");
                            userAnime.ReflectionRate = reader.GetDoubleSafe("reflection_rate");
                            userAnime.IgnoreReflectionRate = reader.GetDoubleSafe("ignore_reflection_rate");
                            userAnime.ReflectionDamageRate = reader.GetDoubleSafe("reflection_damage_rate");
                            userAnime.ReflectionResistanceRate = reader.GetDoubleSafe("reflection_resistance_rate");
                            userAnime.Mana = reader.GetDoubleSafe("mana");
                            userAnime.ManaRegenerationRate = reader.GetDoubleSafe("mana_regeneration_rate");
                            userAnime.DamageToDifferentFactionRate = reader.GetDoubleSafe("damage_to_different_faction_rate");
                            userAnime.ResistanceToDifferentFactionRate = reader.GetDoubleSafe("resistance_to_different_faction_rate");
                            userAnime.DamageToSameFactionRate = reader.GetDoubleSafe("damage_to_same_faction_rate");
                            userAnime.ResistanceToSameFactionRate = reader.GetDoubleSafe("resistance_to_same_faction_rate");
                            userAnime.NormalDamageRate = reader.GetDoubleSafe("normal_damage_rate");
                            userAnime.NormalResistanceRate = reader.GetDoubleSafe("normal_resistance_rate");
                            userAnime.SkillDamageRate = reader.GetDoubleSafe("skill_damage_rate");
                            userAnime.SkillResistanceRate = reader.GetDoubleSafe("skill_resistance_rate");
                            userAnime.PercentAllHealth = reader.GetDoubleSafe("percent_all_health");
                            userAnime.PercentAllPhysicalAttack = reader.GetDoubleSafe("percent_all_physical_attack");
                            userAnime.PercentAllPhysicalDefense = reader.GetDoubleSafe("percent_all_physical_defense");
                            userAnime.PercentAllMagicalAttack = reader.GetDoubleSafe("percent_all_magical_attack");
                            userAnime.PercentAllMagicalDefense = reader.GetDoubleSafe("percent_all_magical_defense");
                            userAnime.PercentAllChemicalAttack = reader.GetDoubleSafe("percent_all_chemical_attack");
                            userAnime.PercentAllChemicalDefense = reader.GetDoubleSafe("percent_all_chemical_defense");
                            userAnime.PercentAllAtomicAttack = reader.GetDoubleSafe("percent_all_atomic_attack");
                            userAnime.PercentAllAtomicDefense = reader.GetDoubleSafe("percent_all_atomic_defense");
                            userAnime.PercentAllMentalAttack = reader.GetDoubleSafe("percent_all_mental_attack");
                            userAnime.PercentAllMentalDefense = reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
                return userAnime;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task<InsertOrUpdateResult<UserAnimes>> InsertOrUpdateUserAnimesAsync(string userId, UserAnimes userAnime, string type, List<RecipeItemDetailDTO> totalDeductions)
    {
        // 1. Guard Clauses
        if (userAnime == null || string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(type))
        {
            return InsertOrUpdateResult<UserAnimes>.Failure("Dữ liệu đầu vào không hợp lệ.");
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
                            return InsertOrUpdateResult<UserAnimes>.Failure(
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
                            return InsertOrUpdateResult<UserAnimes>.Failure($"Thất bại khi cập nhật số lượng cho loại {objectType}.");
                        }
                    }
                }
            }

            // -------------------------------------------------------------------------------------
            // BƯỚC 2: UPSERT BẢNG USER_UNIVERSES
            // -------------------------------------------------------------------------------------
            string upsertSQL = @"
            INSERT INTO user_animes (
                user_id, anime_id, anime_level, power, health, mana, speed,
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
                @user_id, c.id, @anime_level, @power, @health, @mana, @speed,
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
            FROM animes c
            WHERE c.id = @anime_id AND c.is_active = TRUE AND c.is_deleted = FALSE
            ON DUPLICATE KEY UPDATE
                anime_level = VALUES(anime_level), power = VALUES(power), health = VALUES(health), mana = VALUES(mana), speed = VALUES(speed),
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
                AddAllParameters(command, userAnime, userId, type);
                rowsAffected = await command.ExecuteNonQueryAsync();
            }

            if (rowsAffected == 0)
            {
                await transaction.RollbackAsync();
                return InsertOrUpdateResult<UserAnimes>.Failure("Thất bại: Anime không tồn tại hoặc đã bị khóa/xóa.");
            }

            await transaction.CommitAsync();

            if (rowsAffected == 1)
            {
                return InsertOrUpdateResult<UserAnimes>.Inserted(userAnime);
            }
            else
            {
                return InsertOrUpdateResult<UserAnimes>.Updated(userAnime);
            }
        }
        catch (MySqlException ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError($"[InsertOrUpdateUserAnimesAsync MySqlException]: {ex.Message}");
            return InsertOrUpdateResult<UserAnimes>.Failure($"Lỗi Database: {ex.Message}");
        }
        catch (System.Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError($"[InsertOrUpdateUserAnimesAsync Exception]: {ex.Message}");
            return InsertOrUpdateResult<UserAnimes>.Failure($"Lỗi hệ thống: {ex.Message}");
        }
    }
    public async Task<UserAnimes> GetSumUserAnimesAsync(string userId)
    {
        UserAnimes userAnimes = new UserAnimes();
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
                FROM user_animes uc INNER JOIN animes c ON c.id = uc.anime_id 
                WHERE uc.user_id = @user_id AND c.is_active = TRUE AND c.is_deleted = FALSE;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userAnimes.Power = reader.IsDBNull(reader.GetOrdinal("total_power")) ? 0 : reader.GetDoubleSafe("total_power");
                            userAnimes.Health = reader.IsDBNull(reader.GetOrdinal("total_health")) ? 0 : reader.GetDoubleSafe("total_health");
                            userAnimes.Mana = reader.IsDBNull(reader.GetOrdinal("total_mana")) ? 0 : reader.GetFloat("total_mana");
                            userAnimes.PhysicalAttack = reader.IsDBNull(reader.GetOrdinal("total_physical_attack")) ? 0 : reader.GetDoubleSafe("total_physical_attack");
                            userAnimes.PhysicalDefense = reader.IsDBNull(reader.GetOrdinal("total_physical_defense")) ? 0 : reader.GetDoubleSafe("total_physical_defense");
                            userAnimes.MagicalAttack = reader.IsDBNull(reader.GetOrdinal("total_magical_attack")) ? 0 : reader.GetDoubleSafe("total_magical_attack");
                            userAnimes.MagicalDefense = reader.IsDBNull(reader.GetOrdinal("total_magical_defense")) ? 0 : reader.GetDoubleSafe("total_magical_defense");
                            userAnimes.ChemicalAttack = reader.IsDBNull(reader.GetOrdinal("total_chemical_attack")) ? 0 : reader.GetDoubleSafe("total_chemical_attack");
                            userAnimes.ChemicalDefense = reader.IsDBNull(reader.GetOrdinal("total_chemical_defense")) ? 0 : reader.GetDoubleSafe("total_chemical_defense");
                            userAnimes.AtomicAttack = reader.IsDBNull(reader.GetOrdinal("total_atomic_attack")) ? 0 : reader.GetDoubleSafe("total_atomic_attack");
                            userAnimes.AtomicDefense = reader.IsDBNull(reader.GetOrdinal("total_atomic_defense")) ? 0 : reader.GetDoubleSafe("total_atomic_defense");
                            userAnimes.MentalAttack = reader.IsDBNull(reader.GetOrdinal("total_mental_attack")) ? 0 : reader.GetDoubleSafe("total_mental_attack");
                            userAnimes.MentalDefense = reader.IsDBNull(reader.GetOrdinal("total_mental_defense")) ? 0 : reader.GetDoubleSafe("total_mental_defense");
                            userAnimes.Speed = reader.IsDBNull(reader.GetOrdinal("total_speed")) ? 0 : reader.GetDoubleSafe("total_speed");
                            userAnimes.CriticalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_critical_damage_rate")) ? 0 : reader.GetDoubleSafe("total_critical_damage_rate");
                            userAnimes.CriticalRate = reader.IsDBNull(reader.GetOrdinal("total_critical_rate")) ? 0 : reader.GetDoubleSafe("total_critical_rate");
                            userAnimes.CriticalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_critical_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_critical_resistance_rate");
                            userAnimes.IgnoreCriticalRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_critical_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_critical_rate");
                            userAnimes.PenetrationRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_rate");
                            userAnimes.PenetrationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_penetration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_penetration_resistance_rate");
                            userAnimes.EvasionRate = reader.IsDBNull(reader.GetOrdinal("total_evasion_rate")) ? 0 : reader.GetDoubleSafe("total_evasion_rate");
                            userAnimes.DamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_damage_absorption_rate");
                            userAnimes.IgnoreDamageAbsorptionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_damage_absorption_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_damage_absorption_rate");
                            userAnimes.AbsorbedDamageRate = reader.IsDBNull(reader.GetOrdinal("total_absorbed_damage_rate")) ? 0 : reader.GetDoubleSafe("total_absorbed_damage_rate");
                            userAnimes.VitalityRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_rate");
                            userAnimes.VitalityRegenerationResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_vitality_regeneration_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_vitality_regeneration_resistance_rate");
                            userAnimes.AccuracyRate = reader.IsDBNull(reader.GetOrdinal("total_accuracy_rate")) ? 0 : reader.GetDoubleSafe("total_accuracy_rate");
                            userAnimes.LifestealRate = reader.IsDBNull(reader.GetOrdinal("total_lifesteal_rate")) ? 0 : reader.GetDoubleSafe("total_lifesteal_rate");
                            userAnimes.ShieldStrength = reader.IsDBNull(reader.GetOrdinal("total_shield_strength")) ? 0 : reader.GetDoubleSafe("total_shield_strength");
                            userAnimes.Tenacity = reader.IsDBNull(reader.GetOrdinal("total_tenacity")) ? 0 : reader.GetDoubleSafe("total_tenacity");
                            userAnimes.ResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_rate");
                            userAnimes.ComboRate = reader.IsDBNull(reader.GetOrdinal("total_combo_rate")) ? 0 : reader.GetDoubleSafe("total_combo_rate");
                            userAnimes.IgnoreComboRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_combo_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_combo_rate");
                            userAnimes.ComboDamageRate = reader.IsDBNull(reader.GetOrdinal("total_combo_damage_rate")) ? 0 : reader.GetDoubleSafe("total_combo_damage_rate");
                            userAnimes.ComboResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_combo_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_combo_resistance_rate");
                            userAnimes.StunRate = reader.IsDBNull(reader.GetOrdinal("total_stun_rate")) ? 0 : reader.GetDoubleSafe("total_stun_rate");
                            userAnimes.IgnoreStunRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_stun_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_stun_rate");
                            userAnimes.ReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_rate");
                            userAnimes.IgnoreReflectionRate = reader.IsDBNull(reader.GetOrdinal("total_ignore_reflection_rate")) ? 0 : reader.GetDoubleSafe("total_ignore_reflection_rate");
                            userAnimes.ReflectionDamageRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_damage_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_damage_rate");
                            userAnimes.ReflectionResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_reflection_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_reflection_resistance_rate");
                            userAnimes.ManaRegenerationRate = reader.IsDBNull(reader.GetOrdinal("total_mana_regeneration_rate")) ? 0 : reader.GetDoubleSafe("total_mana_regeneration_rate");
                            userAnimes.DamageToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_different_faction_rate");
                            userAnimes.ResistanceToDifferentFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_different_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_different_faction_rate");
                            userAnimes.DamageToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_damage_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_damage_to_same_faction_rate");
                            userAnimes.ResistanceToSameFactionRate = reader.IsDBNull(reader.GetOrdinal("total_resistance_to_same_faction_rate")) ? 0 : reader.GetDoubleSafe("total_resistance_to_same_faction_rate");
                            userAnimes.NormalDamageRate = reader.IsDBNull(reader.GetOrdinal("total_normal_damage_rate")) ? 0 : reader.GetDoubleSafe("total_normal_damage_rate");
                            userAnimes.NormalResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_normal_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_normal_resistance_rate");
                            userAnimes.SkillDamageRate = reader.IsDBNull(reader.GetOrdinal("total_skill_damage_rate")) ? 0 : reader.GetDoubleSafe("total_skill_damage_rate");
                            userAnimes.SkillResistanceRate = reader.IsDBNull(reader.GetOrdinal("total_skill_resistance_rate")) ? 0 : reader.GetDoubleSafe("total_skill_resistance_rate");
                            userAnimes.PercentAllHealth = reader.IsDBNull(reader.GetOrdinal("percent_all_health")) ? 0 : reader.GetDoubleSafe("percent_all_health");
                            userAnimes.PercentAllPhysicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_physical_attack");
                            userAnimes.PercentAllPhysicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_physical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_physical_defense");
                            userAnimes.PercentAllMagicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_magical_attack");
                            userAnimes.PercentAllMagicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_magical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_magical_defense");
                            userAnimes.PercentAllChemicalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_attack")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_attack");
                            userAnimes.PercentAllChemicalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_chemical_defense")) ? 0 : reader.GetDoubleSafe("percent_all_chemical_defense");
                            userAnimes.PercentAllAtomicAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_attack")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_attack");
                            userAnimes.PercentAllAtomicDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_atomic_defense")) ? 0 : reader.GetDoubleSafe("percent_all_atomic_defense");
                            userAnimes.PercentAllMentalAttack = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_attack")) ? 0 : reader.GetDoubleSafe("percent_all_mental_attack");
                            userAnimes.PercentAllMentalDefense = reader.IsDBNull(reader.GetOrdinal("percent_all_mental_defense")) ? 0 : reader.GetDoubleSafe("percent_all_mental_defense");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return userAnimes;
    }
    private void AddAllParameters(MySqlCommand command, UserAnimes userAnime, string userId, string type)
    {
        command.Parameters.AddWithValue("@user_id", userId);
        command.Parameters.AddWithValue("@anime_id", type);

        command.Parameters.AddWithValue("@anime_level", userAnime.Level == 0 ? 1 : userAnime.Level);
        command.Parameters.AddWithValue("@power", userAnime.Power);
        command.Parameters.AddWithValue("@health", userAnime.Health);
        command.Parameters.AddWithValue("@mana", userAnime.Mana);
        command.Parameters.AddWithValue("@speed", userAnime.Speed);

        command.Parameters.AddWithValue("@physical_attack", userAnime.PhysicalAttack);
        command.Parameters.AddWithValue("@physical_defense", userAnime.PhysicalDefense);
        command.Parameters.AddWithValue("@magical_attack", userAnime.MagicalAttack);
        command.Parameters.AddWithValue("@magical_defense", userAnime.MagicalDefense);

        command.Parameters.AddWithValue("@chemical_attack", userAnime.ChemicalAttack);
        command.Parameters.AddWithValue("@chemical_defense", userAnime.ChemicalDefense);
        command.Parameters.AddWithValue("@atomic_attack", userAnime.AtomicAttack);
        command.Parameters.AddWithValue("@atomic_defense", userAnime.AtomicDefense);
        command.Parameters.AddWithValue("@mental_attack", userAnime.MentalAttack);
        command.Parameters.AddWithValue("@mental_defense", userAnime.MentalDefense);

        command.Parameters.AddWithValue("@critical_damage_rate", userAnime.CriticalDamageRate);
        command.Parameters.AddWithValue("@critical_rate", userAnime.CriticalRate);
        command.Parameters.AddWithValue("@critical_resistance_rate", userAnime.CriticalResistanceRate);
        command.Parameters.AddWithValue("@ignore_critical_rate", userAnime.IgnoreCriticalRate);
        command.Parameters.AddWithValue("@penetration_resistance_rate", userAnime.PenetrationResistanceRate);
        command.Parameters.AddWithValue("@penetration_rate", userAnime.PenetrationRate);
        command.Parameters.AddWithValue("@evasion_rate", userAnime.EvasionRate);
        command.Parameters.AddWithValue("@damage_absorption_rate", userAnime.DamageAbsorptionRate);
        command.Parameters.AddWithValue("@vitality_regeneration_rate", userAnime.VitalityRegenerationRate);
        command.Parameters.AddWithValue("@ignore_damage_absorption_rate", userAnime.IgnoreDamageAbsorptionRate);
        command.Parameters.AddWithValue("@absorbed_damage_rate", userAnime.AbsorbedDamageRate);
        command.Parameters.AddWithValue("@vitality_regeneration_resistance_rate", userAnime.VitalityRegenerationResistanceRate);

        command.Parameters.AddWithValue("@accuracy_rate", userAnime.AccuracyRate);
        command.Parameters.AddWithValue("@lifesteal_rate", userAnime.LifestealRate);
        command.Parameters.AddWithValue("@shield_strength", userAnime.ShieldStrength);
        command.Parameters.AddWithValue("@tenacity", userAnime.Tenacity);
        command.Parameters.AddWithValue("@resistance_rate", userAnime.ResistanceRate);
        command.Parameters.AddWithValue("@combo_rate", userAnime.ComboRate);
        command.Parameters.AddWithValue("@reflection_rate", userAnime.ReflectionRate);
        command.Parameters.AddWithValue("@ignore_combo_rate", userAnime.IgnoreComboRate);
        command.Parameters.AddWithValue("@combo_damage_rate", userAnime.ComboDamageRate);
        command.Parameters.AddWithValue("@combo_resistance_rate", userAnime.ComboResistanceRate);
        command.Parameters.AddWithValue("@stun_rate", userAnime.StunRate);
        command.Parameters.AddWithValue("@ignore_stun_rate", userAnime.IgnoreStunRate);
        command.Parameters.AddWithValue("@ignore_reflection_rate", userAnime.IgnoreReflectionRate);
        command.Parameters.AddWithValue("@reflection_damage_rate", userAnime.ReflectionDamageRate);
        command.Parameters.AddWithValue("@reflection_resistance_rate", userAnime.ReflectionResistanceRate);

        command.Parameters.AddWithValue("@mana_regeneration_rate", userAnime.ManaRegenerationRate);
        command.Parameters.AddWithValue("@damage_to_different_faction_rate", userAnime.DamageToDifferentFactionRate);
        command.Parameters.AddWithValue("@resistance_to_different_faction_rate", userAnime.ResistanceToDifferentFactionRate);
        command.Parameters.AddWithValue("@damage_to_same_faction_rate", userAnime.DamageToSameFactionRate);
        command.Parameters.AddWithValue("@resistance_to_same_faction_rate", userAnime.ResistanceToSameFactionRate);
        command.Parameters.AddWithValue("@normal_damage_rate", userAnime.NormalDamageRate);
        command.Parameters.AddWithValue("@normal_resistance_rate", userAnime.NormalResistanceRate);
        command.Parameters.AddWithValue("@skill_damage_rate", userAnime.SkillDamageRate);
        command.Parameters.AddWithValue("@skill_resistance_rate", userAnime.SkillResistanceRate);

        command.Parameters.AddWithValue("@percent_all_health", userAnime.PercentAllHealth);
        command.Parameters.AddWithValue("@percent_all_physical_attack", userAnime.PercentAllPhysicalAttack);
        command.Parameters.AddWithValue("@percent_all_physical_defense", userAnime.PercentAllPhysicalDefense);
        command.Parameters.AddWithValue("@percent_all_magical_attack", userAnime.PercentAllMagicalAttack);
        command.Parameters.AddWithValue("@percent_all_magical_defense", userAnime.PercentAllMagicalDefense);
        command.Parameters.AddWithValue("@percent_all_chemical_attack", userAnime.PercentAllChemicalAttack);
        command.Parameters.AddWithValue("@percent_all_chemical_defense", userAnime.PercentAllChemicalDefense);
        command.Parameters.AddWithValue("@percent_all_atomic_attack", userAnime.PercentAllAtomicAttack);
        command.Parameters.AddWithValue("@percent_all_atomic_defense", userAnime.PercentAllAtomicDefense);
        command.Parameters.AddWithValue("@percent_all_mental_attack", userAnime.PercentAllMentalAttack);
        command.Parameters.AddWithValue("@percent_all_mental_defense", userAnime.PercentAllMentalDefense);
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
