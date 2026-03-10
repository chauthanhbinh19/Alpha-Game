using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

public class RecipeRepository : IRecipeRepository
{
    public async Task<List<RecipeItemDto>> GetRecipeItemsAsync(string featureName, int level, string userId)
    {
        var result = new List<RecipeItemDto>();

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        string sql = @"
            SELECT 
                r.id AS recipe_id,
                i.id AS item_id,
                i.image AS item_image,
                rli.quantity AS required_quantity,
                COALESCE(ui.quantity, 0) AS user_quantity,
                rl.min_level AS recipe_min_level,
                rl.max_level AS recipe_max_level
            FROM features f
            JOIN feature_recipe fr ON f.id = fr.feature_id
            JOIN recipes r ON fr.recipe_id = r.id
            JOIN recipe_level_items rli ON r.id = rli.recipe_id
            JOIN recipe_levels rl ON rli.recipe_level_id = rl.id
            JOIN items i ON rli.item_id = i.id
            LEFT JOIN user_items ui 
                ON ui.item_id = i.id 
                AND ui.user_id = @userId
            WHERE f.feature_name = @featureName
            AND @level BETWEEN rl.min_level AND rl.max_level;
        ";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@featureName", featureName);
        command.Parameters.AddWithValue("@level", level);
        command.Parameters.AddWithValue("@userId", userId);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(new RecipeItemDto
            {
                RecipeId = reader.GetString("recipe_id"),
                ItemId = reader.GetString("item_id"),
                ItemImage = reader.GetString("item_image"),
                RequiredQuantity = reader.GetDouble("required_quantity"),
                UserQuantity = reader.GetDouble("user_quantity"),
                MinLevel = reader.GetIntSafe("recipe_min_level"),
                MaxLevel = reader.GetIntSafe("recipe_max_level"),
            });
        }

        return result;
    }
    public async Task DeductItemsAsync(string userId, List<RecipeItemDto> items)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        await using var transaction = await connection.BeginTransactionAsync();

        try
        {
            foreach (var item in items)
            {
                string sql = @"
                    UPDATE user_items
                    SET quantity = quantity - @quantity
                    WHERE user_id = @userId
                    AND item_id = @itemId";

                await using var command = new MySqlCommand(sql, connection, transaction);
                command.Parameters.AddWithValue("@quantity", item.RequiredQuantity);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@itemId", item.ItemId);

                await command.ExecuteNonQueryAsync();
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    public async Task<List<RecipeItemDto>> GetRecipeItemsByItemIdAsync(string itemId, int level, string userId)
    {
        var result = new List<RecipeItemDto>();

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        string sql = @"
            SELECT 
            r.id AS recipe_id,
            i.id AS item_id,
            i.image AS item_image,
            rli.quantity AS required_quantity,
            COALESCE(ui.quantity, 0) AS user_quantity,
            rl.min_level AS recipe_min_level,
            rl.max_level AS recipe_max_level
        FROM recipes r
        JOIN recipe_level_items rli ON r.id = rli.recipe_id
        JOIN recipe_levels rl ON rli.recipe_level_id = rl.id
        JOIN items i ON rli.item_id = i.id
        LEFT JOIN user_items ui 
            ON ui.item_id = i.id 
            AND ui.user_id = @userId
        WHERE r.id IN (
            SELECT rli2.recipe_id
            FROM recipe_level_items rli2
            JOIN recipe_levels rl2 ON rli2.recipe_level_id = rl2.id
            WHERE rli2.item_id = @itemId
            AND @level BETWEEN rl.min_level AND rl.max_level
        );
        ";

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@itemId", itemId);
        command.Parameters.AddWithValue("@level", level);
        command.Parameters.AddWithValue("@userId", userId);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(new RecipeItemDto
            {
                RecipeId = reader.GetString("recipe_id"),
                ItemId = reader.GetString("item_id"),
                ItemImage = reader.GetString("item_image"),
                RequiredQuantity = reader.GetDouble("required_quantity"),
                UserQuantity = reader.GetDouble("user_quantity"),
                MinLevel = reader.GetIntSafe("recipe_min_level"),
                MaxLevel = reader.GetIntSafe("recipe_max_level"),
            });
        }

        return result;
    }
}
