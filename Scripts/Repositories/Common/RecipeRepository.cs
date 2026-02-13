using MySqlConnector;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

public class RecipeRepository : IRecipeRepository
{
    public async Task<List<RecipeItemDto>> GetRecipeItemsAsync(string featureName,int level,string userId)
    {
        var result = new List<RecipeItemDto>();

        string connectionString = DatabaseConfig.ConnectionString;

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        string sql = @"
            SELECT 
                r.id AS recipe_id,
                i.id AS item_id,
                rli.quantity AS required_quantity,
                COALESCE(ui.quantity, 0) AS user_quantity
            FROM features f
            JOIN feature_recipe fr ON f.id = fr.feature_id
            JOIN recipes r ON fr.recipe_id = r.id
            JOIN recipe_level_items rli ON r.id = rli.recipe_id
            JOIN recipe_levels rl ON rli.recipe_level_id = rl.id
            JOIN items i ON rli.item_id = i.id
            LEFT JOIN user_items ui 
                ON ui.item_id = i.id 
                AND ui.user_id = @userId
            WHERE f.name = @featureName
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
                RequiredQuantity = reader.GetDouble("required_quantity"),
                UserQuantity = reader.GetDouble("user_quantity"),
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
}
