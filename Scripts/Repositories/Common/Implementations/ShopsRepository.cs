using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MySqlConnector;

public class ShopsRepository : IShopsRepository
{
    public async Task<List<string>> GetShopCodeNamesAsync(string shopType = null)
    {
        var shopNames = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        string sql = @"
            SELECT shop_code_name
            FROM shops
            WHERE is_deleted = FALSE 
              AND is_active = TRUE
              AND (@shopType IS NULL OR shop_type = @shopType)
            ORDER BY shop_code_name DESC";

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@shopType", string.IsNullOrWhiteSpace(shopType) ? DBNull.Value : shopType);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            int shopCodeNameOrdinal = reader.GetOrdinal("shop_code_name");

            if (!reader.IsDBNull(shopCodeNameOrdinal))
            {
                shopNames.Add(reader.GetString(shopCodeNameOrdinal));
            }
        }

        return shopNames;
    }
    public async Task<List<Currencies>> GetCurrenciesByShopAsync(string userId, ShopRequestDTO shopRequestDTO)
    {
        var currencies = new List<Currencies>();
        string connectionString = DatabaseConfig.ConnectionString;
        // UnityEngine.Debug.Log(userId + shopRequestDTO.ShopId + shopRequestDTO.ObjectType);

        // Lấy danh sách currency_id duy nhất được dùng trong Shop 
        // và LEFT JOIN với user_currencies để lấy số lượng tiền của User
        string sql = @"
        SELECT DISTINCT
            c.id AS currency_id,
            c.name AS currency_name,
            c.image AS currency_image,
            COALESCE(uc.quantity, 0) AS quantity
        FROM shop_details sd
        JOIN currencies c 
            ON c.id = sd.currency_id
        LEFT JOIN user_currencies uc 
            ON uc.currency_id = c.id 
            AND uc.user_id = @userId
        WHERE sd.shop_id = @shopId
          AND sd.object_type = @objectType
          AND sd.is_deleted = FALSE 
          AND sd.is_active = TRUE;";

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@userId", string.IsNullOrWhiteSpace(userId) ? DBNull.Value : userId);
        command.Parameters.AddWithValue("@shopId", shopRequestDTO.ShopId);
        command.Parameters.AddWithValue("@objectType", shopRequestDTO.ObjectType);
        // command.Parameters.AddWithValue("@limit", shopRequestDTO.Limit);
        // command.Parameters.AddWithValue("@offset", shopRequestDTO.Offset);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            int quantityOrdinal = reader.GetOrdinal("quantity");
            currencies.Add(new Currencies
            {
                Id = reader.GetString("currency_id"),
                Name = reader.IsDBNull(reader.GetOrdinal("currency_name")) ? null : reader.GetString("currency_name"),
                Image = reader.IsDBNull(reader.GetOrdinal("currency_image")) ? null : reader.GetString("currency_image"),
                Quantity = reader.IsDBNull(quantityOrdinal) ? 0 : Convert.ToDouble(reader.GetValue(quantityOrdinal))
            });
        }

        return currencies;
    }
    public async Task<ShopDTO> GetShopsAsync(ShopRequestDTO shopRequestDTO)
    {
        ShopDTO shopDTO = new ShopDTO
        {
            ShopDetails = new List<ShopDetails>()
        };

        string connectionString = DatabaseConfig.ConnectionString;

        // Query lấy các Shop thỏa điều kiện + Join lấy toàn bộ ShopDetails của các Shop đó
        string sql = $@"
            SELECT 
                s.shop_id,
                s.shop_name,
                s.shop_code_name,
                s.shop_type,
                s.reset_type,
                s.description,
                o.name AS object_name,
                o.image AS object_image,
                c.image AS currency_image,
                sd.shop_id AS detail_shop_id,
                sd.object_id,
                sd.object_type,
                sd.object_quantity,
                sd.currency_id,
                sd.price,
                sd.stock_limit,
                sd.buy_limit_per_user,
                sd.is_active AS detail_is_active,
                sd.is_deleted AS detail_is_deleted,
                sd.created_at AS detail_created_at,
                sd.updated_at AS detail_updated_at
            FROM (
                SELECT shop_id, shop_name, shop_code_name, shop_type, reset_type, description
                FROM shops
                WHERE is_deleted = FALSE AND is_active = TRUE
                  AND (@shopCodeName IS NULL OR shop_name LIKE CONCAT('%', @shopCodeName, '%'))
                  AND (@shopType IS NULL OR shop_type = @shopType)
                ORDER BY created_at DESC
            ) s
            LEFT JOIN shop_details sd 
                ON s.shop_id = sd.shop_id 
                AND sd.object_type = @object_type
                AND sd.is_deleted = FALSE 
                AND sd.is_active = TRUE
            LEFT JOIN {shopRequestDTO.ObjectTable} o 
				ON o.id = sd.object_id
            LEFT JOIN currencies c
                ON c.id = sd.currency_id
            LIMIT @limit OFFSET @offset;";

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@shopCodeName", string.IsNullOrWhiteSpace(shopRequestDTO.ShopCodeName) ? DBNull.Value : shopRequestDTO.ShopCodeName);
        command.Parameters.AddWithValue("@shopType", string.IsNullOrWhiteSpace(shopRequestDTO.ShopType) ? DBNull.Value : shopRequestDTO.ShopType);
        command.Parameters.AddWithValue("@object_type", shopRequestDTO.ObjectType);
        command.Parameters.AddWithValue("@limit", shopRequestDTO.Limit);
        command.Parameters.AddWithValue("@offset", shopRequestDTO.Offset);

        await using var reader = await command.ExecuteReaderAsync();

        bool isShopInfoLoaded = false;

        while (await reader.ReadAsync())
        {
            // Gán thông tin chung của Shop 1 lần duy nhất từ dòng đầu tiên
            if (!isShopInfoLoaded)
            {
                shopDTO.ShopId = reader.IsDBNull(reader.GetOrdinal("shop_id")) ? string.Empty : reader.GetString("shop_id");
                shopDTO.ShopName = reader.IsDBNull(reader.GetOrdinal("shop_name")) ? string.Empty : reader.GetString("shop_name");
                shopDTO.ShopCodeName = reader.IsDBNull(reader.GetOrdinal("shop_code_name")) ? null : reader.GetString("shop_code_name");
                shopDTO.ShopType = reader.IsDBNull(reader.GetOrdinal("shop_type")) ? string.Empty : reader.GetString("shop_type");
                shopDTO.ResetType = reader.IsDBNull(reader.GetOrdinal("reset_type")) ? string.Empty : reader.GetString("reset_type");
                shopDTO.Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString("description");

                isShopInfoLoaded = true;
            }

            // Đọc từng chi tiết item (ShopDetails) gắn vào danh sách
            int objectIdOrdinal = reader.GetOrdinal("object_id");
            if (!reader.IsDBNull(objectIdOrdinal))
            {
                var detail = new ShopDetails
                {
                    ShopId = shopDTO.ShopId,
                    ObjectId = reader.GetString("object_id"),
                    ObjectType = reader.GetString("object_type"),
                    ObjectName = reader.IsDBNull(reader.GetOrdinal("object_name")) ? null : reader.GetString("object_name"),
                    ObjectImage = reader.IsDBNull(reader.GetOrdinal("object_image")) ? null : reader.GetString("object_image"),
                    ObjectQuantity = reader.GetDouble("object_quantity"),
                    CurrencyId = reader.GetString("currency_id"),
                    CurrencyImage = reader.IsDBNull(reader.GetOrdinal("currency_image")) ? null : reader.GetString("currency_image"),
                    Price = reader.GetDouble("price"),
                    StockLimit = reader.GetInt32("stock_limit"),
                    BuyLimitPerUser = reader.GetInt32("buy_limit_per_user"),
                    PurchaseCount = reader.GetInt32("purchase_count"),
                    IsActive = reader.GetBoolean("detail_is_active"),
                    IsDeleted = reader.GetBoolean("detail_is_deleted"),
                    CreatedAt = reader.GetDateTime("detail_created_at"),
                    UpdatedAt = reader.GetDateTime("detail_updated_at")
                };

                shopDTO.ShopDetails.Add(detail);
            }
        }

        return shopDTO;
    }
    public async Task<ShopDTO> GetUserShopsAsync(string userId, ShopRequestDTO shopRequestDTO)
    {
        // 1. Guard Clause: Bắt lỗi NULL tham số truyền vào
        if (shopRequestDTO == null)
        {
            throw new ArgumentNullException(nameof(shopRequestDTO), "shopRequestDTO không được để null.");
        }

        ShopDTO shopDTO = new ShopDTO
        {
            ShopDetails = new List<ShopDetails>()
        };

        string connectionString = DatabaseConfig.ConnectionString;

        string sql = $@"
        SELECT 
            s.shop_id,
            s.shop_name,
            s.shop_code_name,
            s.shop_type,
            s.reset_type,
            s.description,
            o.name AS object_name,
            o.image AS object_image,
            c.image AS currency_image,
            sd.shop_id AS detail_shop_id,
            sd.object_id,
            sd.object_type,
            sd.object_quantity,
            sd.currency_id,
            sd.price,
            sd.stock_limit,
            sd.buy_limit_per_user,
            sd.is_active AS detail_is_active,
            sd.is_deleted AS detail_is_deleted,
            sd.created_at AS detail_created_at,
            sd.updated_at AS detail_updated_at,
            COALESCE(usp.purchase_count, 0) AS purchase_count
        FROM (
            SELECT shop_id, shop_name, shop_code_name, shop_type, reset_type, description
            FROM shops
            WHERE is_deleted = FALSE AND is_active = TRUE
              AND (@shopCodeName IS NULL OR shop_name LIKE CONCAT('%', @shopCodeName, '%'))
              AND (@shopType IS NULL OR shop_type = @shopType)
            ORDER BY created_at DESC
            LIMIT 1
        ) s
        LEFT JOIN shop_details sd 
            ON s.shop_id = sd.shop_id 
            AND sd.object_type = @object_type
            AND sd.is_deleted = FALSE 
            AND sd.is_active = TRUE
        LEFT JOIN user_shop_purchase usp 
            ON usp.user_id = @userId 
            AND usp.shop_id = sd.shop_id 
            AND usp.object_id = sd.object_id
        LEFT JOIN {shopRequestDTO.ObjectTable} o 
            ON o.id = sd.object_id
        LEFT JOIN currencies c
            ON c.id = sd.currency_id
        LIMIT @limit OFFSET @offset;";

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@userId", string.IsNullOrWhiteSpace(userId) ? DBNull.Value : userId);
        command.Parameters.AddWithValue("@shopCodeName", string.IsNullOrWhiteSpace(shopRequestDTO.ShopCodeName) ? DBNull.Value : shopRequestDTO.ShopCodeName);
        command.Parameters.AddWithValue("@shopType", string.IsNullOrWhiteSpace(shopRequestDTO.ShopType) ? DBNull.Value : shopRequestDTO.ShopType);
        command.Parameters.AddWithValue("@object_type", shopRequestDTO.ObjectType ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@limit", shopRequestDTO.Limit);
        command.Parameters.AddWithValue("@offset", shopRequestDTO.Offset);

        await using var reader = await command.ExecuteReaderAsync();

        bool isShopInfoLoaded = false;

        while (await reader.ReadAsync())
        {
            // Gán thông tin chung của Shop 1 lần duy nhất từ dòng đầu tiên
            if (!isShopInfoLoaded)
            {
                shopDTO.ShopId = reader.IsDBNull(reader.GetOrdinal("shop_id")) ? string.Empty : reader.GetString("shop_id");
                shopDTO.ShopName = reader.IsDBNull(reader.GetOrdinal("shop_name")) ? string.Empty : reader.GetString("shop_name");
                shopDTO.ShopCodeName = reader.IsDBNull(reader.GetOrdinal("shop_code_name")) ? null : reader.GetString("shop_code_name");
                shopDTO.ShopType = reader.IsDBNull(reader.GetOrdinal("shop_type")) ? string.Empty : reader.GetString("shop_type");
                shopDTO.ResetType = reader.IsDBNull(reader.GetOrdinal("reset_type")) ? string.Empty : reader.GetString("reset_type");
                shopDTO.Description = reader.IsDBNull(reader.GetOrdinal("description")) ? null : reader.GetString("description");

                isShopInfoLoaded = true;
            }

            // Đọc từng chi tiết item (ShopDetails)
            int objectIdOrdinal = reader.GetOrdinal("object_id");
            if (!reader.IsDBNull(objectIdOrdinal))
            {
                var detail = new ShopDetails
                {
                    ShopId = shopDTO.ShopId,
                    ObjectId = reader.GetString("object_id"),
                    ObjectType = reader.IsDBNull(reader.GetOrdinal("object_type")) ? string.Empty : reader.GetString("object_type"),
                    ObjectName = reader.IsDBNull(reader.GetOrdinal("object_name")) ? null : reader.GetString("object_name"),
                    ObjectImage = reader.IsDBNull(reader.GetOrdinal("object_image")) ? null : reader.GetString("object_image"),
                    ObjectQuantity = reader.IsDBNull(reader.GetOrdinal("object_quantity")) ? 0 : reader.GetDouble("object_quantity"),
                    CurrencyId = reader.IsDBNull(reader.GetOrdinal("currency_id")) ? string.Empty : reader.GetString("currency_id"),
                    CurrencyImage = reader.IsDBNull(reader.GetOrdinal("currency_image")) ? null : reader.GetString("currency_image"),
                    Price = reader.IsDBNull(reader.GetOrdinal("price")) ? 0 : reader.GetDouble("price"),
                    StockLimit = reader.IsDBNull(reader.GetOrdinal("stock_limit")) ? 0 : reader.GetInt32("stock_limit"),
                    BuyLimitPerUser = reader.IsDBNull(reader.GetOrdinal("buy_limit_per_user")) ? 0 : reader.GetInt32("buy_limit_per_user"),
                    PurchaseCount = reader.IsDBNull(reader.GetOrdinal("purchase_count")) ? 0 : reader.GetInt32("purchase_count"),
                    IsActive = !reader.IsDBNull(reader.GetOrdinal("detail_is_active")) && reader.GetBoolean("detail_is_active"),
                    IsDeleted = !reader.IsDBNull(reader.GetOrdinal("detail_is_deleted")) && reader.GetBoolean("detail_is_deleted"),
                    CreatedAt = reader.IsDBNull(reader.GetOrdinal("detail_created_at")) ? DateTime.MinValue : reader.GetDateTime("detail_created_at"),
                    UpdatedAt = reader.IsDBNull(reader.GetOrdinal("detail_updated_at")) ? DateTime.MinValue : reader.GetDateTime("detail_updated_at")
                };

                shopDTO.ShopDetails.Add(detail);
            }
        }

        return shopDTO;
    }
    public async Task<int> GetShopItemCountAsync(ShopRequestDTO shopRequestDTO)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        // Subquery tìm Shop phù hợp -> JOIN shop_details để COUNT vật phẩm theo object_type
        string sql = @"
        SELECT COUNT(sd.object_id) AS total_items
        FROM (
            SELECT shop_id
            FROM shops
            WHERE is_deleted = FALSE AND is_active = TRUE
              AND (@shopCodeName IS NULL OR shop_name LIKE CONCAT('%', @shopCodeName, '%'))
              AND (@shopType IS NULL OR shop_type = @shopType)
            ORDER BY created_at DESC
            LIMIT 1
        ) s
        INNER JOIN shop_details sd 
            ON s.shop_id = sd.shop_id 
            AND sd.object_type = @object_type
            AND sd.is_deleted = FALSE 
            AND sd.is_active = TRUE;";

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@shopCodeName", string.IsNullOrWhiteSpace(shopRequestDTO.ShopCodeName) ? DBNull.Value : shopRequestDTO.ShopCodeName);
        command.Parameters.AddWithValue("@shopType", string.IsNullOrWhiteSpace(shopRequestDTO.ShopType) ? DBNull.Value : shopRequestDTO.ShopType);
        command.Parameters.AddWithValue("@object_type", shopRequestDTO.ObjectType);

        // ExecuteScalarAsync nhanh và tối ưu nhất cho truy vấn trả về 1 giá trị duy nhất (Aggregate/Count)
        var result = await command.ExecuteScalarAsync();

        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
    }
}