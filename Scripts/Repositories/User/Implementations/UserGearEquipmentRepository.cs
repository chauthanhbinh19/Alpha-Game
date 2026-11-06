using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;
using UnityEngine; // Dùng cho Debug.LogError (nếu chạy trên Unity)

public class UserGearEquipmentRepository : IUserGearEquipmentRepository
{
    private readonly string _connectionString;

    public UserGearEquipmentRepository(string connectionString = null)
    {
        _connectionString = connectionString ?? DatabaseConfig.ConnectionString;
    }

    public async Task<List<UserGearEquipment>> GetUserGearEquipmentAsync(string userId)
    {
        var list = new List<UserGearEquipment>();
        if (string.IsNullOrWhiteSpace(userId)) return list;

        string sql = @"
            SELECT user_id, number_of_gear, created_at, updated_at, is_deleted, is_active
            FROM user_gear_equipment
            WHERE user_id = @userId AND is_deleted = FALSE;";

        try
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@userId", userId);

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new UserGearEquipment
                {
                    UserId = reader.GetStringSafe("user_id"),
                    NumberOfGear = reader.GetIntSafe("number_of_gear"),
                    // CreatedAt = reader.GetDateTimeSafe("created_at"),
                    // UpdatedAt = reader.GetDateTimeSafe("updated_at"),
                    IsDeleted = reader.GetBoolSafe("is_deleted"),
                    IsActive = reader.GetBoolSafe("is_active")
                });
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[GetUserGearEquipmentAsync Exception]: {ex.Message}");
        }

        return list;
    }

    public async Task<InsertOrUpdateResult<UserGearEquipment>> InsertOrUpdateUserGearEquipmentAsync(string userId, int numberOfGear)
    {
        if (string.IsNullOrWhiteSpace(userId) || numberOfGear < 0)
        {
            return InsertOrUpdateResult<UserGearEquipment>.Failure("Tham số truyền vào không hợp lệ.");
        }

        string sql = @"
            INSERT INTO user_gear_equipment (user_id, number_of_gear, is_deleted, is_active)
            VALUES (@userId, @numberOfGear, FALSE, TRUE)
            ON DUPLICATE KEY UPDATE 
                updated_at = CURRENT_TIMESTAMP;";

        try
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@numberOfGear", numberOfGear);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            var entity = new UserGearEquipment { UserId = userId, NumberOfGear = numberOfGear };

            // MySQL ON DUPLICATE KEY UPDATE: 1 là Insert mới, 2 (hoặc lớn hơn/bằng 0 tùy driver) là Update
            if (rowsAffected == 1)
            {
                return InsertOrUpdateResult<UserGearEquipment>.Inserted(entity);
            }
            else
            {
                return InsertOrUpdateResult<UserGearEquipment>.Updated(entity);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[InsertOrUpdateUserGearEquipmentAsync Exception]: {ex.Message}");
            return InsertOrUpdateResult<UserGearEquipment>.Failure($"Lỗi Database: {ex.Message}");
        }
    }

    public async Task<InsertOrUpdateResult<UserGearEquipmentDetail>> InsertOrUpdateUserGearEquipmentDetailAsync(string userId, int numberOfGear, string equipmentId, int position)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(equipmentId) || numberOfGear < 0)
        {
            return InsertOrUpdateResult<UserGearEquipmentDetail>.Failure("Tham số chi tiết gear không hợp lệ.");
        }

        string sql = @"
            INSERT INTO user_gear_equipment_detail (user_id, number_of_gear, equipment_id, position, is_deleted, is_active)
            VALUES (@userId, @numberOfGear, @equipmentId, @position, FALSE, TRUE)
            ON DUPLICATE KEY UPDATE 
                position = VALUES(position),
                updated_at = CURRENT_TIMESTAMP;";

        try
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@numberOfGear", numberOfGear);
            command.Parameters.AddWithValue("@equipmentId", equipmentId);
            command.Parameters.AddWithValue("@position", position);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            var detailEntity = new UserGearEquipmentDetail
            {
                UserId = userId,
                NumberOfGear = numberOfGear,
                EquipmentId = equipmentId,
                Position = position
            };

            if (rowsAffected == 1)
            {
                return InsertOrUpdateResult<UserGearEquipmentDetail>.Inserted(detailEntity);
            }
            else
            {
                return InsertOrUpdateResult<UserGearEquipmentDetail>.Updated(detailEntity);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[InsertOrUpdateUserGearEquipmentDetailAsync Exception]: {ex.Message}");
            return InsertOrUpdateResult<UserGearEquipmentDetail>.Failure($"Lỗi Database: {ex.Message}");
        }
    }

    // --- CÁC HÀM HELPER DÙNG CHUNG CHO CARD GEAR EQUIPMENT ---
    private async Task<InsertOrUpdateResult<bool>> ExecuteCardGearUpsertAsync(string tableName, string idColumnName, string userId, string cardId, int numberOfGear)
    {
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(cardId) || numberOfGear < 0)
        {
            return InsertOrUpdateResult<bool>.Failure("Tham số card gear không hợp lệ.");
        }

        string sql = $@"
            INSERT INTO `{tableName}` (user_id, `{idColumnName}`, number_of_gear)
            VALUES (@userId, @cardId, @numberOfGear)
            ON DUPLICATE KEY UPDATE 
                number_of_gear = VALUES(number_of_gear);";

        try
        {
            await using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            await using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@cardId", cardId);
            command.Parameters.AddWithValue("@numberOfGear", numberOfGear);

            int rowsAffected = await command.ExecuteNonQueryAsync();

            if (rowsAffected == 1)
            {
                return InsertOrUpdateResult<bool>.Inserted(true);
            }
            else
            {
                return InsertOrUpdateResult<bool>.Updated(true);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[{tableName} Exception]: {ex.Message}");
            return InsertOrUpdateResult<bool>.Failure($"Lỗi Database: {ex.Message}");
        }
    }

    public Task<InsertOrUpdateResult<bool>> InsertOrUpdateHeroGearEquipmentAsync(string userId, string cardHeroId, int numberOfGear)
        => ExecuteCardGearUpsertAsync("user_card_heroes_gear_equipment", "card_hero_id", userId, cardHeroId, numberOfGear);

    public Task<InsertOrUpdateResult<bool>> InsertOrUpdateCaptainGearEquipmentAsync(string userId, string cardCaptainId, int numberOfGear)
        => ExecuteCardGearUpsertAsync("user_card_captains_gear_equipment", "card_captain_id", userId, cardCaptainId, numberOfGear);

    public Task<InsertOrUpdateResult<bool>> InsertOrUpdateColonelGearEquipmentAsync(string userId, string cardColonelId, int numberOfGear)
        => ExecuteCardGearUpsertAsync("user_card_colonels_gear_equipment", "card_colonel_id", userId, cardColonelId, numberOfGear);

    public Task<InsertOrUpdateResult<bool>> InsertOrUpdateGeneralGearEquipmentAsync(string userId, string cardGeneralId, int numberOfGear)
        => ExecuteCardGearUpsertAsync("user_card_generals_gear_equipment", "card_general_id", userId, cardGeneralId, numberOfGear);

    public Task<InsertOrUpdateResult<bool>> InsertOrUpdateAdmiralGearEquipmentAsync(string userId, string cardAdmiralId, int numberOfGear)
        => ExecuteCardGearUpsertAsync("user_card_admirals_gear_equipment", "card_admiral_id", userId, cardAdmiralId, numberOfGear);

    public Task<InsertOrUpdateResult<bool>> InsertOrUpdateMonsterGearEquipmentAsync(string userId, string cardMonsterId, int numberOfGear)
        => ExecuteCardGearUpsertAsync("user_card_monsters_gear_equipment", "card_monster_id", userId, cardMonsterId, numberOfGear);

    public Task<InsertOrUpdateResult<bool>> InsertOrUpdateMilitaryGearEquipmentAsync(string userId, string cardMilitaryId, int numberOfGear)
        => ExecuteCardGearUpsertAsync("user_card_militaries_gear_equipment", "card_military_id", userId, cardMilitaryId, numberOfGear);

    public Task<InsertOrUpdateResult<bool>> InsertOrUpdateSoldierGearEquipmentAsync(string userId, string cardSoldierId, int numberOfGear)
        => ExecuteCardGearUpsertAsync("user_card_soldiers_gear_equipment", "card_soldier_id", userId, cardSoldierId, numberOfGear);

    public Task<InsertOrUpdateResult<bool>> InsertOrUpdateSpellGearEquipmentAsync(string userId, string cardSpellId, int numberOfGear)
        => ExecuteCardGearUpsertAsync("user_card_spells_gear_equipment", "card_spell_id", userId, cardSpellId, numberOfGear);
}