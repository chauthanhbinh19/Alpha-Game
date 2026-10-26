using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine; // Đổi sang logger hệ thống của bạn nếu chạy trên Server console (vd: Serilog/NLog)

public class RecipeRepository : IRecipeRepository
{
    /// <summary>
    /// Lấy danh sách công thức theo FeatureName và Level chỉ định kèm kho đồ duy nhất của User
    /// </summary>
    public async Task<RecipeResponseDTO> GetRecipeItemsAsync(string featureName, int level, string userId)
    {
        var response = new RecipeResponseDTO();
        var recipeMap = new Dictionary<string, RecipeItemDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        const string selectSQL = @"
            SELECT 
                r.id AS recipe_id,
                rl.id AS recipe_level_id,
                rl.min_level AS recipe_min_level,
                rl.max_level AS recipe_max_level,
                rli.item_id AS object_id,
                rli.object_type AS object_type,
                COALESCE(cat.image, '') AS object_image,
                rli.quantity AS required_quantity,
                rli.item_value AS object_value
            FROM features f
            JOIN feature_recipe fr ON f.id = fr.feature_id
            JOIN recipes r ON fr.recipe_id = r.id
            JOIN recipe_level_items rli ON r.id = rli.recipe_id
            JOIN recipe_levels rl ON rli.recipe_level_id = rl.id
            LEFT JOIN game_item_catalog cat ON rli.item_id = cat.object_id AND rli.object_type = cat.object_type
            WHERE f.feature_name = @featureName
              AND @level BETWEEN rl.min_level AND rl.max_level;";

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using (var selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@featureName", featureName);
                selectCommand.Parameters.AddWithValue("@level", level);

                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    string recipeId = reader.GetStringSafe("recipe_id");
                    string recipeLevelId = reader.GetStringSafe("recipe_level_id");
                    string mapKey = $"{recipeId}_{recipeLevelId}";

                    if (!recipeMap.TryGetValue(mapKey, out var recipeDto))
                    {
                        recipeDto = new RecipeItemDTO
                        {
                            RecipeId = recipeId,
                            RecipeLevelId = recipeLevelId,
                            MinLevel = reader.GetIntSafe("recipe_min_level"),
                            MaxLevel = reader.GetIntSafe("recipe_max_level"),
                            RecipeItemDetails = new List<RecipeItemDetailDTO>()
                        };
                        recipeMap.Add(mapKey, recipeDto);
                    }

                    recipeDto.RecipeItemDetails.Add(new RecipeItemDetailDTO
                    {
                        ObjectId = reader.GetStringSafe("object_id"),
                        ObjectType = reader.GetStringSafe("object_type"),
                        ObjectImage = reader.GetStringSafe("object_image"),
                        RequiredQuantity = reader.GetDoubleSafe("required_quantity"),
                        ObjectValue = reader.GetDoubleSafe("object_value")
                    });
                }
            }

            response.Recipes = recipeMap.Values.ToList();

            // Đọc kho đồ duy nhất 1 lần cho tất cả nguyên liệu xuất hiện trong danh sách
            var allDetails = response.Recipes.SelectMany(r => r.RecipeItemDetails).ToList();
            if (allDetails.Count > 0)
            {
                response.UserInventory = await GetUserInventoryDictAsync(connection, userId, allDetails);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error GetRecipeItemsAsync (Level): " + ex.Message);
        }

        return response;
    }

    /// <summary>
    /// Lấy toàn bộ danh sách công thức theo FeatureName (Tất cả mốc Level)
    /// </summary>
    public async Task<RecipeResponseDTO> GetRecipeItemsAsync(string featureName, string userId)
    {
        var response = new RecipeResponseDTO();
        var recipeMap = new Dictionary<string, RecipeItemDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        const string selectSQL = @"
            SELECT 
                r.id AS recipe_id,
                rl.id AS recipe_level_id,
                rl.min_level AS recipe_min_level,
                rl.max_level AS recipe_max_level,
                rli.item_id AS object_id,
                rli.object_type AS object_type,
                COALESCE(cat.image, '') AS object_image,
                rli.quantity AS required_quantity,
                rli.item_value AS object_value
            FROM features f
            JOIN feature_recipe fr ON f.id = fr.feature_id
            JOIN recipes r ON fr.recipe_id = r.id
            JOIN recipe_level_items rli ON r.id = rli.recipe_id
            JOIN recipe_levels rl ON rli.recipe_level_id = rl.id
            LEFT JOIN game_item_catalog cat ON rli.item_id = cat.object_id AND rli.object_type = cat.object_type
            WHERE f.feature_name = @featureName;";

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using (var selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@featureName", featureName);

                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    string recipeId = reader.GetStringSafe("recipe_id");
                    string recipeLevelId = reader.GetStringSafe("recipe_level_id");
                    string mapKey = $"{recipeId}_{recipeLevelId}";

                    if (!recipeMap.TryGetValue(mapKey, out var recipeDto))
                    {
                        recipeDto = new RecipeItemDTO
                        {
                            RecipeId = recipeId,
                            RecipeLevelId = recipeLevelId,
                            MinLevel = reader.GetIntSafe("recipe_min_level"),
                            MaxLevel = reader.GetIntSafe("recipe_max_level"),
                            RecipeItemDetails = new List<RecipeItemDetailDTO>()
                        };
                        recipeMap.Add(mapKey, recipeDto);
                    }

                    recipeDto.RecipeItemDetails.Add(new RecipeItemDetailDTO
                    {
                        ObjectId = reader.GetStringSafe("object_id"),
                        ObjectType = reader.GetStringSafe("object_type"),
                        ObjectImage = reader.GetStringSafe("object_image"),
                        RequiredQuantity = reader.GetDoubleSafe("required_quantity"),
                        ObjectValue = reader.GetDoubleSafe("object_value")
                    });
                }
            }

            response.Recipes = recipeMap.Values.ToList();

            var allDetails = response.Recipes.SelectMany(r => r.RecipeItemDetails).ToList();
            if (allDetails.Count > 0)
            {
                response.UserInventory = await GetUserInventoryDictAsync(connection, userId, allDetails);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error GetRecipeItemsAsync (All): " + ex.Message);
        }

        return response;
    }

    /// <summary>
    /// Lấy danh sách công thức chứa vật phẩm cụ thể theo mốc level
    /// </summary>
    public async Task<RecipeResponseDTO> GetRecipeItemsByItemIdAsync(string itemId, int level, string userId)
    {
        var response = new RecipeResponseDTO();
        var recipeMap = new Dictionary<string, RecipeItemDTO>();
        string connectionString = DatabaseConfig.ConnectionString;

        const string selectSQL = @"
            SELECT 
                r.id AS recipe_id,
                rl.id AS recipe_level_id,
                rl.min_level AS recipe_min_level,
                rl.max_level AS recipe_max_level,
                rli.item_id AS object_id,
                rli.object_type AS object_type,
                COALESCE(cat.image, '') AS object_image,
                rli.quantity AS required_quantity,
                rli.item_value AS object_value
            FROM recipes r
            JOIN recipe_level_items rli ON r.id = rli.recipe_id
            JOIN recipe_levels rl ON rli.recipe_level_id = rl.id
            LEFT JOIN game_item_catalog cat ON rli.item_id = cat.object_id AND rli.object_type = cat.object_type
            WHERE r.id IN (
                SELECT rli2.recipe_id
                FROM recipe_level_items rli2
                JOIN recipe_levels rl2 ON rli2.recipe_level_id = rl2.id
                WHERE rli2.item_id = @itemId
                  AND @level BETWEEN rl2.min_level AND rl2.max_level
            )
            AND @level BETWEEN rl.min_level AND rl.max_level;";

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using (var selectCommand = new MySqlCommand(selectSQL, connection))
            {
                selectCommand.Parameters.AddWithValue("@itemId", itemId);
                selectCommand.Parameters.AddWithValue("@level", level);

                await using var reader = await selectCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    string recipeId = reader.GetStringSafe("recipe_id");
                    string recipeLevelId = reader.GetStringSafe("recipe_level_id");
                    string mapKey = $"{recipeId}_{recipeLevelId}";

                    if (!recipeMap.TryGetValue(mapKey, out var recipeDto))
                    {
                        recipeDto = new RecipeItemDTO
                        {
                            RecipeId = recipeId,
                            RecipeLevelId = recipeLevelId,
                            MinLevel = reader.GetIntSafe("recipe_min_level"),
                            MaxLevel = reader.GetIntSafe("recipe_max_level"),
                            RecipeItemDetails = new List<RecipeItemDetailDTO>()
                        };
                        recipeMap.Add(mapKey, recipeDto);
                    }

                    recipeDto.RecipeItemDetails.Add(new RecipeItemDetailDTO
                    {
                        ObjectId = reader.GetStringSafe("object_id"),
                        ObjectType = reader.GetStringSafe("object_type"),
                        ObjectImage = reader.GetStringSafe("object_image"),
                        RequiredQuantity = reader.GetDoubleSafe("required_quantity"),
                        ObjectValue = reader.GetDoubleSafe("object_value")
                    });
                }
            }

            response.Recipes = recipeMap.Values.ToList();

            var allDetails = response.Recipes.SelectMany(r => r.RecipeItemDetails).ToList();
            if (allDetails.Count > 0)
            {
                response.UserInventory = await GetUserInventoryDictAsync(connection, userId, allDetails);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error GetRecipeItemsByItemIdAsync: " + ex.Message);
        }

        return response;
    }

    /// <summary>
    /// Trừ vật phẩm nguyên liệu theo từng ObjectType trong Transaction
    /// </summary>
    public async Task DeductItemsAsync(string userId, List<RecipeItemDTO> items)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            await using var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();

            await using var transaction = await connection.BeginTransactionAsync();

            try
            {
                var allDetails = items.Where(r => r.RecipeItemDetails != null)
                                      .SelectMany(r => r.RecipeItemDetails)
                                      .ToList();

                foreach (var detail in allDetails)
                {
                    string tableName = GetUserTableNameByObjectType(detail.ObjectType);
                    string idColumnName = GetIdColumnNameByObjectType(detail.ObjectType);

                    string updateSQL = $@"
                        UPDATE {tableName}
                        SET quantity = quantity - @quantity
                        WHERE user_id = @userId
                          AND {idColumnName} = @objectId;";

                    await using var updateCommand = new MySqlCommand(updateSQL, connection, transaction);
                    updateCommand.Parameters.AddWithValue("@quantity", detail.RequiredQuantity);
                    updateCommand.Parameters.AddWithValue("@userId", userId);
                    updateCommand.Parameters.AddWithValue("@objectId", detail.ObjectId);

                    await updateCommand.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Debug.LogError("Error DeductItemsAsync Transaction: " + ex.Message);
                throw;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error DeductItemsAsync Connection: " + ex.Message);
        }
    }

    /// <summary>
    /// Đọc số lượng sở hữu từ các bảng kho tương ứng với ObjectType và lưu vào Dictionary
    /// </summary>
    private async Task<Dictionary<string, double>> GetUserInventoryDictAsync(MySqlConnection connection, string userId, List<RecipeItemDetailDTO> details)
    {
        var inventoryDict = new Dictionary<string, double>();
        var groupedByObjectType = details.GroupBy(d => d.ObjectType.ToUpper());

        foreach (var group in groupedByObjectType)
        {
            string objectType = group.Key;
            var objectIds = group.Select(g => g.ObjectId).Distinct().ToList();

            if (objectIds.Count == 0) continue;

            string tableName = GetUserTableNameByObjectType(objectType);
            string idColumnName = GetIdColumnNameByObjectType(objectType);

            if (string.IsNullOrEmpty(tableName)) continue;

            string inClause = string.Join(",", objectIds.Select((_, i) => $"@id{i}"));
            string query = $@"
                SELECT {idColumnName} AS object_id, quantity 
                FROM {tableName} 
                WHERE user_id = @userId AND {idColumnName} IN ({inClause});";

            await using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@userId", userId);
            for (int i = 0; i < objectIds.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@id{i}", objectIds[i]);
            }

            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                string objId = reader.GetStringSafe("object_id");
                double qty = reader.GetDoubleSafe("quantity");
                inventoryDict[objId] = qty;
            }
        }

        return inventoryDict;
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

            _ => DataBaseConstants.Column.ITEM_ID
        };
    }
}