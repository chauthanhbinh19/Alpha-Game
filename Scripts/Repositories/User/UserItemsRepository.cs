using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MySqlConnector;
using System.Threading.Tasks;

public class UserItemsRepository : IUserItemsRepository
{
    public async Task<List<Items>> GetUserItemsAsync(string user_id, string search, string type, int pageSize, int offset)
    {
        List<Items> items = new List<Items>();
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT ui.*, i.id, i.name, i.image
                FROM items i
                JOIN user_items ui ON i.id = ui.item_id
                WHERE ui.user_id = @userId 
                    AND (@type = 'All' OR i.type = @type)
                    AND (@search = '' OR i.name LIKE CONCAT('%', @search, '%'))
                ORDER BY i.name REGEXP '[0-9]+$', CAST(REGEXP_SUBSTR(i.name, '[0-9]+$') AS UNSIGNED), i.name
                LIMIT @limit OFFSET @offset";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@search", search);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@limit", pageSize);
                    command.Parameters.AddWithValue("@offset", offset);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
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
    public async Task<int> GetUserItemsCountAsync(string user_id, string search, string type)
    {
        int count = 0;
        string connectionString = DatabaseConfig.ConnectionString;

        await using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            try
            {
                await connection.OpenAsync();

                string query = @"
                SELECT COUNT(*) 
                FROM items i
                JOIN user_items ui ON i.id = ui.item_id
                WHERE ui.user_id = @userId 
                    AND (@type = 'All' OR i.type = @type)
                    AND (@search = '' OR i.name LIKE CONCAT('%', @search, '%'))";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", user_id);
                    command.Parameters.AddWithValue("@search", search);
                    command.Parameters.AddWithValue("@type", type);

                    object result = await command.ExecuteScalarAsync();
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

                string query = @"
                SELECT i.id AS itemId, i.name AS itemName, i.image AS itemImage,
                       IFNULL(ui.quantity, 0) AS quantity
                FROM items i
                LEFT JOIN user_items ui ON ui.item_id = i.id AND ui.user_id = @userId
                WHERE i.name = @itemName";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@userId", User.CurrentUserId);
                    command.Parameters.AddWithValue("@itemName", itemName);

                    await using (MySqlDataReader reader = await command.ExecuteReaderAsync())
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
                string query = @"SELECT COUNT(*) FROM user_items WHERE user_id = @user_id AND item_id = @item_id";
                await using (MySqlCommand checkCommand = new MySqlCommand(query, connection))
                {
                    checkCommand.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    checkCommand.Parameters.AddWithValue("@item_id", item.Id);

                    int count = Convert.ToInt32(await checkCommand.ExecuteScalarAsync());

                    if (count == 0)
                    {
                        // Chèn mới
                        string insertQuery = @"INSERT INTO user_items (user_id, item_id, quantity) 
                                           VALUES (@user_id, @item_id, @quantity)";
                        await using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
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

                string query = @"UPDATE user_items 
                             SET quantity = @quantity
                             WHERE user_id = @user_id AND item_id = @item_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@item_id", item.Id);
                    command.Parameters.AddWithValue("@quantity", item.Quantity);

                    await command.ExecuteNonQueryAsync();
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

                string query = @"UPDATE user_items 
                             SET quantity = @quantity
                             WHERE user_id = @user_id AND item_id = @item_id";

                await using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_id", User.CurrentUserId);
                    command.Parameters.AddWithValue("@item_id", item.Id);
                    command.Parameters.AddWithValue("@quantity", quantity);

                    await command.ExecuteNonQueryAsync();
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
}