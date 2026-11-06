using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;
public class UserUpgradesRepository : IUserUpgradesRepository
{
    public async Task<UserUpgrades> GetUserUpgradesAsync(string userId, string upgradeId, string objectId, string userTable, string objectColumn)
    {
        UserUpgrades userUpgrade = new UserUpgrades();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = $@"
                SELECT 
                    u.id AS upgrade_id,
                    uchu.{objectColumn},
                    COALESCE(uchu.current_level, 0) AS current_level,
                    COALESCE(uchu.current_multiplier, 0) AS current_multiplier
                FROM upgrades u
                LEFT JOIN {userTable} uchu
                    ON u.id = uchu.upgrade_id
                    AND uchu.user_id = @user_id AND uchu.{objectColumn} = @object_id
                WHERE u.id = @upgrade_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);
                    selectCommand.Parameters.AddWithValue("@upgrade_id", upgradeId);
                    selectCommand.Parameters.AddWithValue("@object_id", objectId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userUpgrade.Id = reader.GetStringSafe("upgrade_id");
                            userUpgrade.CurrentLevel = reader.GetIntSafe("current_level");
                            userUpgrade.CurrentMultiplier = reader.GetDoubleSafe("current_multiplier");
                        }
                    }
                }
                return userUpgrade;
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return null;
    }
    public async Task<InsertOrUpdateResult<UserUpgrades>> InsertOrUpdateUserUpgradesAsync(string userId, UserUpgrades upgrade, string objectId, string userTable, string objectColumn, List<RecipeItemDetailDTO> totalDeductions = null)
    {
        // 1. Guard Clause: Kiểm tra tham số đầu vào
        if (upgrade == null || string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(objectId))
        {
            return InsertOrUpdateResult<UserUpgrades>.Failure("Dữ liệu upgrade hoặc tham số ID không hợp lệ.");
        }

        // Sanitize/Validate tên bảng và tên cột để tránh SQL Injection
        string safeUserTable = userTable?.Replace("`", "").Trim();
        string safeObjectColumn = objectColumn?.Replace("`", "").Trim();

        if (string.IsNullOrWhiteSpace(safeUserTable) || string.IsNullOrWhiteSpace(safeObjectColumn))
        {
            return InsertOrUpdateResult<UserUpgrades>.Failure("Tên bảng hoặc tên cột truyền vào không hợp lệ.");
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
                // Lọc các vật phẩm có số lượng khấu trừ > 0 và gom nhóm theo ObjectType
                var validDeductions = totalDeductions.Where(d => d != null && d.RequiredQuantity > 0).ToList();
                var groupedDeductions = validDeductions.GroupBy(d => d.ObjectType?.Trim().ToUpper());

                foreach (var group in groupedDeductions)
                {
                    string rawObjectType = group.Key;
                    if (string.IsNullOrWhiteSpace(rawObjectType)) continue;

                    // Chuẩn hóa tên bảng và cột ID tương ứng DTO
                    string tableName = DatabaseHelper.GetUserTableNameByObjectType(rawObjectType);
                    string idColumnName = DatabaseHelper.GetIdColumnNameByObjectType(rawObjectType);

                    if (string.IsNullOrEmpty(tableName)) continue;

                    // Gom nhóm các item trùng ObjectId để cộng dồn số lượng cần trừ
                    var itemList = group
                        .GroupBy(x => x.ObjectId)
                        .Select(g => new RecipeItemDetailDTO
                        {
                            ObjectId = g.Key,
                            ObjectType = rawObjectType,
                            RequiredQuantity = g.Sum(x => x.RequiredQuantity)
                        })
                        .ToList();

                    // 1.1 Tạo IN Clause để SELECT FOR UPDATE 1 lần duy nhất cho ObjectType này
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
                        double ownedQty = currentStockMap.TryGetValue(item.ObjectId, out double qty) ? qty : 0;
                        if (ownedQty < item.RequiredQuantity)
                        {
                            await transaction.RollbackAsync();
                            return InsertOrUpdateResult<UserUpgrades>.Failure(
                                $"Thất bại: Không đủ nguyên liệu '{item.ObjectId}' ({rawObjectType}). " +
                                $"Cần: {item.RequiredQuantity}, Hiện có: {ownedQty}.");
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
                            return InsertOrUpdateResult<UserUpgrades>.Failure($"Thất bại khi cập nhật số lượng cho loại {rawObjectType}.");
                        }
                    }
                }
            }

            // -------------------------------------------------------------------------------------
            // BƯỚC 2: UPSERT BẢNG DỘNG USER_UPGRADES (CHỨA safeUserTable VÀ safeObjectColumn)
            // -------------------------------------------------------------------------------------
            string upsertSQL = $@"
        INSERT INTO `{safeUserTable}` (
            user_id,
            `{safeObjectColumn}`,
            upgrade_id,
            current_level,
            current_multiplier
        )
        VALUES (
            @user_id,
            @object_id,
            @upgrade_id,
            @current_level,
            @current_multiplier
        )
        ON DUPLICATE KEY UPDATE
            current_level = VALUES(current_level),
            current_multiplier = VALUES(current_multiplier);";

            await using var command = new MySqlCommand(upsertSQL, connection, transaction);
            command.Parameters.AddWithValue("@user_id", userId);
            command.Parameters.AddWithValue("@object_id", objectId);
            command.Parameters.AddWithValue("@upgrade_id", upgrade.Id);
            command.Parameters.AddWithValue("@current_level", upgrade.CurrentLevel);
            command.Parameters.AddWithValue("@current_multiplier", upgrade.CurrentMultiplier);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            // Cam kết Transaction thành công
            await transaction.CommitAsync();

            if (rowsAffected == 1)
            {
                return InsertOrUpdateResult<UserUpgrades>.Inserted(upgrade);
            }
            else if (rowsAffected >= 2 || rowsAffected == 0)
            {
                return InsertOrUpdateResult<UserUpgrades>.Updated(upgrade);
            }
            else
            {
                return InsertOrUpdateResult<UserUpgrades>.Failure("Không thể thực hiện lưu hoặc cập nhật upgrade.");
            }
        }
        catch (MySqlException ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError($"[InsertOrUpdateUserUpgradesAsync MySqlError]: {ex.Message}");
            return InsertOrUpdateResult<UserUpgrades>.Failure($"Lỗi Database: {ex.Message}");
        }
        catch (System.Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.LogError($"[InsertOrUpdateUserUpgradesAsync Exception]: {ex.Message}");
            return InsertOrUpdateResult<UserUpgrades>.Failure($"Lỗi hệ thống: {ex.Message}");
        }
    }
    public async Task<UserUpgrades> GetSumUserUpgradesAsync(string userId, string objectId, string userTable, string objectColumn)
    {
        UserUpgrades userUpgrades = new UserUpgrades();
        string connectionString = DatabaseConfig.ConnectionString;

        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = $@"
                SELECT 
                    SUM(current_multiplier) AS total_multiplier
                FROM {userTable}
                WHERE user_id = @user_id AND {objectColumn} = @object_id;
            ";

                using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@user_id", userId);
                    selectCommand.Parameters.AddWithValue("@object_id", objectId);

                    using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            userUpgrades.Power = reader.IsDBNull(reader.GetOrdinal("total_multiplier")) ? 0 : reader.GetDoubleSafe("total_multiplier");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
            }
        }

        return userUpgrades;
    }
}