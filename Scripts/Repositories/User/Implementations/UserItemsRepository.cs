using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Text;

public class UserItemsRepository : IUserItemsRepository
{
    public async Task<List<Items>> GetUserItemsAsync(string userId, string search, string type, int pageSize, int offset)
    {
        List<Items> items = new List<Items>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT ui.*, i.id, i.name, i.type, i.image
                FROM items i
                JOIN user_items ui ON i.id = ui.item_id AND i.is_active = TRUE AND i.is_deleted = FALSE
                WHERE ui.user_id = @userId";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND i.type = @type";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND i.name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += " LIMIT @limit OFFSET @offset";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    if (!string.IsNullOrEmpty(type) && type != "All")
                    {
                        selectCommand.Parameters.AddWithValue("@type", type);
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
                            Items item = new Items
                            {
                                Id = reader.GetStringSafe("item_id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
                                Type = reader.GetStringSafe("type"),
                                Quantity = reader.GetDoubleSafe("quantity")
                            };

                            items.Add(item);
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

        return items;
    }
    public async Task<int> GetUserItemsCountAsync(string userId, string search, string type)
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
                FROM items i
                JOIN user_items ui ON i.id = ui.item_id AND i.is_active = TRUE AND i.is_deleted = FALSE
                WHERE ui.user_id = @userId";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND i.type = @type";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND i.name LIKE CONCAT('%', @search, '%')";
                }

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    if (!string.IsNullOrEmpty(type) && type != "All")
                    {
                        selectCommand.Parameters.AddWithValue("@type", type);
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
    public async Task<Items> GetUserItemByNameAsync(string userId, string itemName)
    {
        Items items = new Items();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT i.id AS itemId, i.name AS itemName, i.image AS itemImage,
                       IFNULL(ui.quantity, 0) AS quantity
                FROM items i
                LEFT JOIN user_items ui ON ui.item_id = i.id AND ui.user_id = @userId
                    AND i.is_active = TRUE AND i.is_deleted = FALSE
                WHERE i.name = @itemName";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    selectCommand.Parameters.AddWithValue("@itemName", itemName);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) // Nếu có dữ liệu
                        {
                            items.Id = reader.GetStringSafe("itemId");
                            items.Name = reader["itemName"]?.ToString() ?? string.Empty;
                            items.Image = reader["itemImage"]?.ToString() ?? string.Empty;
                            items.Quantity = reader.GetDoubleSafe("quantity");
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

        return items;
    }
    public async Task<Items> GetUserItemByCodeNameAsync(string userId, string codeName)
    {
        Items items = new Items();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT i.id AS itemId, i.name AS itemName, i.code_name AS itemCodeName, i.image AS itemImage,
                       IFNULL(ui.quantity, 0) AS quantity
                FROM items i
                LEFT JOIN user_items ui ON ui.item_id = i.id AND ui.user_id = @userId
                    AND i.is_active = TRUE AND i.is_deleted = FALSE
                WHERE i.code_name = @code_name";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", userId);
                    selectCommand.Parameters.AddWithValue("@code_name", codeName);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) // Nếu có dữ liệu
                        {
                            items.Id = reader.GetStringSafe("itemId");
                            items.Name = reader["itemName"]?.ToString() ?? string.Empty;
                            items.CodeName = reader["itemCodeName"]?.ToString() ?? string.Empty;
                            items.Image = reader["itemImage"]?.ToString() ?? string.Empty;
                            items.Quantity = reader.GetDoubleSafe("quantity");
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

        return items;
    }
    public async Task<ItemExperienceDTO> GetUserItemExperienceByCodeNameAsync(string userId, string codeName)
    {
        ItemExperienceDTO item = null;

        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string selectSQL = @"
                SELECT
                    i.id AS item_id,
                    i.name AS item_name,
                    i.code_name AS item_code_name,
                    i.image AS item_image,
                    IFNULL(ui.quantity, 0) AS quantity,
                    IFNULL(iec.experience_value, 0) AS experience_value
                FROM items i
                LEFT JOIN user_items ui
                    ON ui.item_id = i.id AND ui.user_id = @userId AND 
                LEFT JOIN item_experience_configs iec
                    ON iec.item_id = i.id
                WHERE i.code_name = @code_name";

                await using (MySqlCommand command =
                    new MySqlCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@code_name", codeName);

                    await using (MySqlDataReader reader =
                        await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            item = new ItemExperienceDTO
                            {
                                Id = reader.GetStringSafe("item_id"),
                                Name = reader["item_name"]?.ToString() ?? "",
                                CodeName = reader["item_code_name"]?.ToString() ?? "",
                                Image = reader["item_image"]?.ToString() ?? "",
                                Quantity = reader.GetDoubleSafe("quantity"),
                                ExperienceValue = Convert.ToDouble(reader["experience_value"])
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }
        }

        return item;
    }
    public async Task<bool> InsertUserItemAsync(string userId, Items item, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem item đã tồn tại chưa
                string selectSQL = @"SELECT COUNT(*) FROM user_items WHERE user_id = @user_id AND item_id = @item_id AND is_active = TRUE AND is_deleted = FALSE";
                await using (MySqlCommand checkCommand = new MySqlCommand(selectSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", userId);
                    checkCommand.Parameters.AddWithValue("@item_id", item.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        // Chèn mới
                        string insertSQL = @"INSERT INTO user_items (user_id, item_id, quantity) 
                                           VALUES (@user_id, @item_id, @quantity)";
                        await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@user_id", userId);
                            insertCommand.Parameters.AddWithValue("@item_id", item.Id);
                            insertCommand.Parameters.AddWithValue("@quantity", quantity);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Cập nhật số lượng item đã tồn tại
                        item.Quantity = quantity;
                        await UpdateUserItemQuantityAsync(userId, item); // Giả sử bạn đã có phiên bản async
                    }
                }
            }
            catch (MySqlException ex)
            {
                Debug.LogError("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        return true;
    }
    public async Task<Items> UpdateUserItemQuantityAsync(string userId, Items item)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"UPDATE user_items uc INNER JOIN items c ON c.id = uc.item_id 
                             SET quantity = @quantity
                             WHERE uc.user_id = @user_id AND uc.item_id = @item_id AND c.is_active = TRUE AND c.is_deleted = FALSE";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@item_id", item.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", item.Quantity);

                    await updateCommand.ExecuteNonQueryAsync();
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

        return item;
    }
    public async Task<Items> UpdateUserItemQuantityAsync(string userId, Items item, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"UPDATE user_items uc INNER JOIN items c ON c.id = uc.item_id 
                             SET quantity = @quantity
                             WHERE uc.user_id = @user_id AND uc.item_id = @item_id AND c.is_active = TRUE AND c.is_deleted = FALSE";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", userId);
                    updateCommand.Parameters.AddWithValue("@item_id", item.Id);
                    updateCommand.Parameters.AddWithValue("@quantity", quantity);

                    await updateCommand.ExecuteNonQueryAsync();
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

        return item;
    }
    public async Task<bool> InsertOrUpdateUserItemAsync(string userId, Items item, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string insertOrUpdateSQL = @"
                    INSERT INTO user_items (
                        user_id,
                        item_id,
                        sequence_index,
                        quantity
                    )
                    VALUES (
                        @user_id,
                        @item_id,
                        0,
                        @quantity
                    )
                    ON DUPLICATE KEY UPDATE
                        quantity = user_items.quantity + VALUES(quantity),
                        updated_at = CURRENT_TIMESTAMP;
                ";

                await using (MySqlCommand insertOrUpdateCommand = new MySqlCommand(insertOrUpdateSQL, connection))
                {
                    insertOrUpdateCommand.Parameters.AddWithValue("@user_id", userId);
                    insertOrUpdateCommand.Parameters.AddWithValue("@item_id", item.Id);
                    insertOrUpdateCommand.Parameters.AddWithValue("@quantity", quantity);

                    await insertOrUpdateCommand.ExecuteNonQueryAsync();
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

        return true;
    }
    public async Task<bool> InsertOrUpdateUserItemsBatchAsync(string userId, List<(Items item, double quantity)> items)
    {
        if (items == null || items.Count == 0)
            return true;

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 300;

            for (int i = 0; i < items.Count; i += batchSize)
            {
                var batch = items.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append("INSERT INTO user_items (user_id, item_id, sequence_index, quantity) VALUES ");

                for (int j = 0; j < batch.Count; j++)
                {
                    string itemIdParam = $"@item_id_{j}";
                    string quantityParam = $"@quantity_{j}";

                    stringBuilder.Append($"(@user_id, {itemIdParam}, 0, {quantityParam}),");

                    parameters.Add(new MySqlParameter(itemIdParam, batch[j].item.Id));
                    parameters.Add(new MySqlParameter(quantityParam, batch[j].quantity));
                }

                // remove dấu ,
                stringBuilder.Length--;

                stringBuilder.Append(@"
                ON DUPLICATE KEY UPDATE
                quantity = user_items.quantity + VALUES(quantity),
                updated_at = CURRENT_TIMESTAMP;
            ");

                await using var command = new MySqlCommand(stringBuilder.ToString(), connection, (MySqlTransaction)transaction);

                command.Parameters.AddWithValue("@user_id", userId);
                command.Parameters.AddRange(parameters.ToArray());

                await command.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError("Batch Insert Error: " + ex.Message);
            return false;
        }

        return true;
    }
    public async Task<bool> InsertOrUpdateUserItemChestViaProcAsync(string userId, Items item, double quantity)
    {
        if (item == null || string.IsNullOrEmpty(item.Id) || quantity <= 0)
            return true;

        string connectionString = DatabaseConfig.ConnectionString;

        string sql = @"
        INSERT INTO user_items (user_id, item_id, sequence_index, quantity)
        SELECT 
            @p_user_id,
            @p_item_id,
            COALESCE(cfg.sequence_index, 0) AS sequence_index,
            @p_quantity
        FROM (SELECT 1) AS dummy
        LEFT JOIN item_chest_configs cfg 
               ON cfg.item_id = @p_item_id 
              AND cfg.is_active = TRUE 
              AND cfg.is_deleted = FALSE
        ON DUPLICATE KEY UPDATE 
            quantity = user_items.quantity + VALUES(quantity),
            updated_at = CURRENT_TIMESTAMP;";

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            await using var command = new MySqlCommand(sql, connection);

            command.Parameters.Add("@p_user_id", MySqlDbType.VarChar, 32).Value = userId;
            command.Parameters.Add("@p_item_id", MySqlDbType.VarChar, 32).Value = item.Id;
            command.Parameters.Add("@p_quantity", MySqlDbType.Double).Value = quantity;

            await command.ExecuteNonQueryAsync();
            return true;
        }
        catch (MySqlException ex)
        {
            Debug.LogError("Error executing InsertOrUpdateUserItemChest: " + ex.Message);
            return false;
        }
    }
    public async Task<bool> InsertOrUpdateUserItemsChestBatchViaProcAsync(string userId, List<(Items item, double quantity)> items)
    {
        if (items == null || items.Count == 0)
            return true;

        // 1. Gom nhóm item trùng lặp và tính tổng quantity
        var groupedItems = items
            .GroupBy(x => x.item.Id)
            .Select(g => (ItemId: g.Key, TotalQuantity: g.Sum(x => x.quantity)))
            .ToList();

        string connectionString = DatabaseConfig.ConnectionString;
        int batchSize = 100;

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        for (int i = 0; i < groupedItems.Count; i += batchSize)
        {
            var currentBatch = groupedItems.Skip(i).Take(batchSize).ToList();

            var sb = new StringBuilder();
            var parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("@p_user_id", MySqlDbType.VarChar, 32) { Value = userId });

            // Tạo câu lệnh Bulk INSERT kết hợp LEFT JOIN dạng Subquery UNION ALL
            sb.AppendLine(@"
            INSERT INTO user_items (user_id, item_id, sequence_index, quantity)
            SELECT 
                @p_user_id,
                inp.item_id,
                COALESCE(cfg.sequence_index, 0) AS sequence_index,
                inp.total_qty
            FROM (");

            for (int j = 0; j < currentBatch.Count; j++)
            {
                string paramItem = $"@item_{j}";
                string paramQty = $"@qty_{j}";

                if (j > 0) sb.AppendLine(" UNION ALL ");
                sb.Append($"SELECT {paramItem} AS item_id, {paramQty} AS total_qty");

                parameters.Add(new MySqlParameter(paramItem, MySqlDbType.VarChar, 32) { Value = currentBatch[j].ItemId });
                parameters.Add(new MySqlParameter(paramQty, MySqlDbType.Double) { Value = currentBatch[j].TotalQuantity });
            }

            sb.AppendLine(@") inp
            LEFT JOIN item_chest_configs cfg 
                   ON inp.item_id = cfg.item_id 
                  AND cfg.is_active = TRUE 
                  AND cfg.is_deleted = FALSE
            ON DUPLICATE KEY UPDATE 
                quantity = user_items.quantity + VALUES(quantity),
                updated_at = CURRENT_TIMESTAMP;");

            try
            {
                await using var command = new MySqlCommand(sb.ToString(), connection);
                command.Parameters.AddRange(parameters.ToArray());

                await command.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Bulk Insert Error at batch {i}: " + ex.Message);
                return false;
            }
        }

        return true;
    }
}
