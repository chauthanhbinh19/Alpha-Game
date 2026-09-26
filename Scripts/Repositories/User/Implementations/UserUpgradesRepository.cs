using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
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
    public async Task<InsertOrUpdateResult<UserUpgrades>> InsertOrUpdateUserUpgradesAsync(string userId, UserUpgrades upgrade, string objectId, string userTable, string objectColumn)
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

        // 2. Tối ưu câu lệnh Upsert tương thích rộng với các bản MySQL (dùng VALUES() thay vì alias "AS new")
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

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using var command = new MySqlCommand(upsertSQL, connection);
            command.Parameters.AddWithValue("@user_id", userId);
            command.Parameters.AddWithValue("@object_id", objectId);
            command.Parameters.AddWithValue("@upgrade_id", upgrade.Id);
            command.Parameters.AddWithValue("@current_level", upgrade.CurrentLevel);
            command.Parameters.AddWithValue("@current_multiplier", upgrade.CurrentMultiplier);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            // Trong MySQL ON DUPLICATE KEY UPDATE:
            // - rowsAffected = 1: Thêm mới thành công (INSERT)
            // - rowsAffected = 2: Cập nhật thành công (UPDATE)
            // - rowsAffected = 0: Không có thay đổi (Dữ liệu update giống hệt dữ liệu cũ)
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
            Debug.LogError($"[InsertOrUpdateUserUpgradesAsync MySqlError]: {ex.Message}");
            return InsertOrUpdateResult<UserUpgrades>.Failure($"Lỗi Database: {ex.Message}");
        }
        catch (System.Exception ex)
        {
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