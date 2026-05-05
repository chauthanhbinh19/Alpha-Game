using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;
using System.Linq;

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
                SELECT ui.*, i.id, i.name, i.image
                FROM items i
                JOIN user_items ui ON i.id = ui.item_id
                WHERE ui.user_id = @userId ";
                if (!string.IsNullOrEmpty(type) && type != "All")
                {
                    selectSQL += " AND i.type = @type";
                }

                if (!string.IsNullOrEmpty(search))
                {
                    selectSQL += " AND i.name LIKE CONCAT('%', @search, '%')";
                }

                selectSQL += " ORDER BY i.name";
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
                                Id = reader.GetStringSafe("id"),
                                Name = reader.GetStringSafe("name"),
                                Image = reader.GetStringSafe("image"),
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
                JOIN user_items ui ON i.id = ui.item_id
                WHERE ui.user_id = @userId ";
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
    public async Task<Items> GetUserItemByNameAsync(string itemName)
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
                WHERE i.name = @itemName";

                await using (MySqlCommand selectCommand = new MySqlCommand(selectSQL, connection))
                {
                    selectCommand.Parameters.AddWithValue("@userId", User.CurrentUserId);
                    selectCommand.Parameters.AddWithValue("@itemName", itemName);

                    await using (MySqlDataReader reader = await selectCommand.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) // Nếu có dữ liệu
                        {
                            items.Id = reader.GetStringSafe("itemId");
                            items.Name = reader["itemName"]?.ToString() ?? string.Empty;
                            items.Image = reader["itemImage"]?.ToString() ?? string.Empty;
                            items.Quantity = reader["quantity"] != DBNull.Value
                                             ? Convert.ToDouble(reader["quantity"])
                                             : 0;
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
    public async Task<bool> InsertUserItemAsync(Items item, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                // Kiểm tra xem item đã tồn tại chưa
                string selectSQL = @"SELECT COUNT(*) FROM user_items WHERE user_id = @user_id AND item_id = @item_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(selectSQL, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@item_id", item.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        // Chèn mới
                        string insertSQL = @"INSERT INTO user_items (user_id, item_id, quantity) 
                                           VALUES (@user_id, @item_id, @quantity)";
                        await using (MySqlCommand insertCommand = new MySqlCommand(insertSQL, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                            insertCommand.Parameters.AddWithValue("@item_id", item.Id);
                            insertCommand.Parameters.AddWithValue("@quantity", quantity);

                            await insertCommand.ExecuteNonQueryAsync();
                        }
                    }
                    else
                    {
                        // Cập nhật số lượng item đã tồn tại
                        item.Quantity = quantity;
                        await UpdateUserItemQuantityAsync(item); // Giả sử bạn đã có phiên bản async
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
    public async Task<Items> UpdateUserItemQuantityAsync(Items item)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"UPDATE user_items 
                             SET quantity = @quantity
                             WHERE user_id = @user_id AND item_id = @item_id";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
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
    public async Task<Items> UpdateUserItemQuantityAsync(Items item, double quantity)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string updateSQL = @"UPDATE user_items 
                             SET quantity = @quantity
                             WHERE user_id = @user_id AND item_id = @item_id";

                await using (MySqlCommand updateCommand = new MySqlCommand(updateSQL, connection))
                {
                    updateCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
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
    public async Task<bool> InsertOrUpdateUserItemsBatchAsync(List<(Items item, double quantity)> items)
    {
        if (items == null || items.Count == 0)
            return true;

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);

        try
        {
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            int batchSize = 1000;

            for (int i = 0; i < items.Count; i += batchSize)
            {
                var batch = items.Skip(i).Take(batchSize).ToList();

                var stringBuilder = new System.Text.StringBuilder();
                var parameters = new List<MySqlParameter>();

                stringBuilder.Append("INSERT INTO user_items (user_id, item_id, quantity) VALUES ");

                for (int j = 0; j < batch.Count; j++)
                {
                    string itemIdParam = $"@item_id_{j}";
                    string quantityParam = $"@quantity_{j}";

                    stringBuilder.Append($"(@user_id, {itemIdParam}, {quantityParam}),");

                    parameters.Add(new MySqlParameter(itemIdParam, batch[j].item.Id));
                    parameters.Add(new MySqlParameter(quantityParam, batch[j].quantity));
                }

                // remove dấu ,
                stringBuilder.Length--;

                stringBuilder.Append(@"
                ON DUPLICATE KEY UPDATE
                quantity = quantity + VALUES(quantity);
            ");

                await using var command = new MySqlCommand(stringBuilder.ToString(), connection, (MySqlTransaction)transaction);

                command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
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
}