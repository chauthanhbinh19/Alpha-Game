using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

public class ShopsRepository : IShopsRepository
{
    public async Task<string> GetShopIdByCodeNameAsync(string shopCodeName)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        if (string.IsNullOrWhiteSpace(shopCodeName))
        {
            return null;
        }

        string sql = "SELECT shop_id FROM shops WHERE shop_code_name = @ShopCodeName LIMIT 1;";

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ShopCodeName", shopCodeName.Trim());

                    object result = await cmd.ExecuteScalarAsync();
                    
                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log lỗi nếu cần
            Console.WriteLine($"Lỗi khi lấy ShopId theo ShopCodeName: {ex.Message}");
            throw;
        }

        return null;
    }
    public async Task<List<int>> GetDistinctSequencesAsync(string shopId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        var result = new List<int>();

        if (string.IsNullOrWhiteSpace(shopId))
        {
            return result;
        }

        string sql = @"
            SELECT DISTINCT sequence 
            FROM shop_details 
            WHERE shop_id = @ShopId 
            ORDER BY sequence ASC;";

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ShopId", shopId);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                result.Add(reader.GetInt32(0));
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log lỗi nếu cần
            Console.WriteLine($"Lỗi khi lấy danh sách sequence: {ex.Message}");
            throw;
        }

        return result;
    }
    public async Task<InsertOrUpdateResult<Shops>> InsertShopAsync(Shops shop)
    {
        if (shop == null)
        {
            return InsertOrUpdateResult<Shops>.Failure("Dữ liệu shop truyền vào không được null.");
        }

        // 1. Sinh GUID nếu ShopId chưa tồn tại
        if (string.IsNullOrWhiteSpace(shop.ShopId))
        {
            shop.ShopId = Guid.NewGuid().ToString("N");
        }
        string connectionString = DatabaseConfig.ConnectionString;

        string shopCodeName = shop.ShopName?.Trim().Replace(" ", "_").ToUpper() ?? "SHOP_CODE";

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        // 2. Query Insert bảng shops
                        string insertShopSql = @"
                            INSERT INTO shops (
                                shop_id, shop_name, shop_code_name, shop_type, reset_type, description
                            ) VALUES (
                                @ShopId, @ShopName, @ShopCodeName, @ShopType, @ResetType, @Description
                            );";

                        using (var cmd = new MySqlCommand(insertShopSql, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ShopId", shop.ShopId);
                            cmd.Parameters.AddWithValue("@ShopName", shop.ShopName ?? string.Empty);
                            cmd.Parameters.AddWithValue("@ShopCodeName", shopCodeName);
                            cmd.Parameters.AddWithValue("@ShopType", shop.ShopType ?? "GENERAL");
                            cmd.Parameters.AddWithValue("@ResetType", string.IsNullOrEmpty(shop.ResetType) ? DBNull.Value : (object)shop.ResetType);
                            cmd.Parameters.AddWithValue("@Description", shop.Description ?? string.Empty);

                            await cmd.ExecuteNonQueryAsync();
                        }

                        // 3. Insert danh sách ShopDetails đi kèm (nếu có)
                        if (shop.ShopDetails != null && shop.ShopDetails.Count > 0)
                        {
                            string insertDetailSql = @"
                                INSERT INTO shop_details (
                                    shop_id, object_id, object_type, object_quantity, 
                                    currency_id, price, stock_limit, buy_limit_per_user
                                ) VALUES (
                                    @ShopId, @ObjectId, @ObjectType, @ObjectQuantity, 
                                    @CurrencyId, @Price, @StockLimit, @BuyLimitPerUser
                                );";

                            foreach (var detail in shop.ShopDetails)
                            {
                                detail.ShopId = shop.ShopId; // Đảm bảo FK khớp với Shop vừa khởi tạo

                                using (var detailCmd = new MySqlCommand(insertDetailSql, connection, transaction))
                                {
                                    detailCmd.Parameters.AddWithValue("@ShopId", detail.ShopId);
                                    detailCmd.Parameters.AddWithValue("@ObjectId", detail.ObjectId);
                                    detailCmd.Parameters.AddWithValue("@ObjectType", detail.ObjectType);
                                    detailCmd.Parameters.AddWithValue("@ObjectQuantity", detail.ObjectQuantity);
                                    detailCmd.Parameters.AddWithValue("@CurrencyId", detail.CurrencyId);
                                    detailCmd.Parameters.AddWithValue("@Price", detail.Price);
                                    detailCmd.Parameters.AddWithValue("@StockLimit", detail.StockLimit);
                                    detailCmd.Parameters.AddWithValue("@BuyLimitPerUser", detail.BuyLimitPerUser);

                                    await detailCmd.ExecuteNonQueryAsync();
                                }
                            }
                        }

                        // Commit transaction
                        await transaction.CommitAsync();

                        // Trả về kết quả thành công thông qua Factory Method Inserted
                        return InsertOrUpdateResult<Shops>.Inserted(shop);
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return InsertOrUpdateResult<Shops>.Failure($"Lỗi Transaction: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return InsertOrUpdateResult<Shops>.Failure($"Lỗi kết nối CSDL: {ex.Message}");
        }
    }
    public async Task<InsertOrUpdateResult<Shops>> InsertOrUpdateShopDetailsBatchAsync(Shops shop)
    {
        if (shop == null || string.IsNullOrWhiteSpace(shop.ShopId))
        {
            return InsertOrUpdateResult<Shops>.Failure("Dữ liệu shop hoặc ShopId không được để trống.");
        }

        if (shop.ShopDetails == null || shop.ShopDetails.Count == 0)
        {
            return InsertOrUpdateResult<Shops>.Success(shop, "Không có chi tiết shop nào để cập nhật.");
        }

        string connectionString = DatabaseConfig.ConnectionString;

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var transaction = await connection.BeginTransactionAsync())
                {
                    try
                    {
                        // 1. Kiểm tra tồn tại ShopId
                        string checkShopSql = "SELECT COUNT(1) FROM shops WHERE shop_id = @ShopId;";
                        using (var checkCmd = new MySqlCommand(checkShopSql, connection, transaction))
                        {
                            checkCmd.Parameters.AddWithValue("@ShopId", shop.ShopId);
                            bool shopExists = Convert.ToInt32(await checkCmd.ExecuteScalarAsync()) > 0;
                            if (!shopExists)
                            {
                                return InsertOrUpdateResult<Shops>.Failure($"Không tìm thấy ShopId: {shop.ShopId}");
                            }
                        }

                        // 2. Lấy MAX sequence và số bản ghi hiện tại của MAX sequence đó
                        string getSeqSql = @"
                            SELECT COALESCE(MAX(sequence), 0) AS max_seq,
                                   COUNT(1) AS current_count
                            FROM shop_details 
                            WHERE shop_id = @ShopId 
                              AND sequence = (SELECT COALESCE(MAX(sequence), 1) FROM shop_details WHERE shop_id = @ShopId);";

                        int currentSequence = 1;
                        int countInCurrentSeq = 0;

                        using (var seqCmd = new MySqlCommand(getSeqSql, connection, transaction))
                        {
                            seqCmd.Parameters.AddWithValue("@ShopId", shop.ShopId);
                            using (var reader = await seqCmd.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    int maxSeq = reader.GetInt32("max_seq");
                                    currentSequence = maxSeq == 0 ? 1 : maxSeq;
                                    countInCurrentSeq = reader.GetInt32("current_count");
                                }
                            }
                        }

                        // 3. Lấy tất cả object_id + object_type hiện có của shop này để phân biệt Insert mới hay Update
                        var existingKeys = new HashSet<string>();
                        string getExistingSql = "SELECT object_id, object_type FROM shop_details WHERE shop_id = @ShopId;";
                        using (var getCmd = new MySqlCommand(getExistingSql, connection, transaction))
                        {
                            getCmd.Parameters.AddWithValue("@ShopId", shop.ShopId);
                            using (var reader = await getCmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    existingKeys.Add($"{reader.GetString("object_id")}_{reader.GetString("object_type")}");
                                }
                            }
                        }

                        // 4. Build Batch Insert Query
                        var sqlBuilder = new StringBuilder();
                        sqlBuilder.Append(@"
                            INSERT INTO shop_details (
                                shop_id, object_id, object_type, object_quantity, sequence,
                                currency_id, price, stock_limit, buy_limit_per_user
                            ) VALUES ");

                        using (var batchCmd = new MySqlCommand())
                        {
                            batchCmd.Connection = connection;
                            batchCmd.Transaction = transaction;

                            var valueSqls = new List<string>();

                            for (int i = 0; i < shop.ShopDetails.Count; i++)
                            {
                                var detail = shop.ShopDetails[i];
                                detail.ShopId = shop.ShopId;

                                string key = $"{detail.ObjectId}_{detail.ObjectType}";
                                bool isNewItem = !existingKeys.Contains(key);

                                int targetSequence = currentSequence;
                                if (isNewItem)
                                {
                                    if (countInCurrentSeq >= 1000)
                                    {
                                        currentSequence++;
                                        countInCurrentSeq = 0;
                                    }
                                    targetSequence = currentSequence;
                                    countInCurrentSeq++;
                                }

                                // Tạo parameter tên dạng @ShopId_0, @ObjectId_0, ...
                                valueSqls.Add($"(@ShopId_{i}, @ObjectId_{i}, @ObjectType_{i}, @ObjectQuantity_{i}, @Sequence_{i}, @CurrencyId_{i}, @Price_{i}, @StockLimit_{i}, @BuyLimitPerUser_{i})");

                                batchCmd.Parameters.AddWithValue($"@ShopId_{i}", detail.ShopId);
                                batchCmd.Parameters.AddWithValue($"@ObjectId_{i}", detail.ObjectId);
                                batchCmd.Parameters.AddWithValue($"@ObjectType_{i}", detail.ObjectType);
                                batchCmd.Parameters.AddWithValue($"@ObjectQuantity_{i}", detail.ObjectQuantity);
                                batchCmd.Parameters.AddWithValue($"@Sequence_{i}", targetSequence);
                                batchCmd.Parameters.AddWithValue($"@CurrencyId_{i}", detail.CurrencyId);
                                batchCmd.Parameters.AddWithValue($"@Price_{i}", detail.Price);
                                batchCmd.Parameters.AddWithValue($"@StockLimit_{i}", detail.StockLimit);
                                batchCmd.Parameters.AddWithValue($"@BuyLimitPerUser_{i}", detail.BuyLimitPerUser);
                            }

                            sqlBuilder.Append(string.Join(", ", valueSqls));
                            sqlBuilder.Append(@"
                                ON DUPLICATE KEY UPDATE
                                    object_quantity = VALUES(object_quantity),
                                    currency_id = VALUES(currency_id),
                                    price = VALUES(price),
                                    stock_limit = VALUES(stock_limit),
                                    buy_limit_per_user = VALUES(buy_limit_per_user);");

                            batchCmd.CommandText = sqlBuilder.ToString();

                            // Thực thi duy nhất 1 câu SQL batch cho toàn bộ list
                            await batchCmd.ExecuteNonQueryAsync();
                        }

                        await transaction.CommitAsync();

                        return InsertOrUpdateResult<Shops>.Mixed(shop, $"Cập nhật batch {shop.ShopDetails.Count} shop_details thành công.");
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        return InsertOrUpdateResult<Shops>.Failure($"Lỗi Transaction Batch: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return InsertOrUpdateResult<Shops>.Failure($"Lỗi CSDL: {ex.Message}");
        }
    }
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
                AND sd.sequence = @sequence
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
        command.Parameters.AddWithValue("@sequence", shopRequestDTO.Sequence);
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
            -- 1. Lấy Shop ID chính xác nhất thỏa mãn điều kiện
            SELECT shop_id, shop_name, shop_code_name, shop_type, reset_type, description
            FROM shops
            WHERE is_deleted = FALSE 
            AND is_active = TRUE
            AND (@shopCodeName IS NULL OR shop_code_name = @shopCodeName OR shop_name LIKE CONCAT('%', @shopCodeName, '%'))
            AND (@shopType IS NULL OR shop_type = @shopType)
            ORDER BY created_at DESC
            LIMIT 1
        ) s
        -- 2. Lấy danh sách Vật phẩm (Đã lọc theo sequence và phân trang tại đây)
        INNER JOIN shop_details sd 
            ON s.shop_id = sd.shop_id 
        AND sd.object_type = @object_type
        AND sd.sequence = @sequence
        AND sd.is_deleted = FALSE 
        AND sd.is_active = TRUE
        -- 3. JOIN bảng Lượt mua của User (Cần bổ sung reset_key nếu có logic Reset)
        LEFT JOIN user_shop_purchase usp 
            ON usp.user_id = @userId 
        AND usp.shop_id = sd.shop_id 
        AND usp.object_id = sd.object_id
        -- AND usp.reset_key = @currentResetKey -- Bỏ comment nếu có quản lý Reset Key
        -- 4. JOIN thông tin hiển thị Vật phẩm & Tiền tệ
        LEFT JOIN {shopRequestDTO.ObjectTable} o 
            ON o.id = sd.object_id
        LEFT JOIN currencies c
            ON c.id = sd.currency_id
        ORDER BY sd.created_at ASC
        LIMIT @limit OFFSET @offset;";

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@userId", string.IsNullOrWhiteSpace(userId) ? DBNull.Value : userId);
        command.Parameters.AddWithValue("@shopCodeName", string.IsNullOrWhiteSpace(shopRequestDTO.ShopCodeName) ? DBNull.Value : shopRequestDTO.ShopCodeName);
        command.Parameters.AddWithValue("@shopType", string.IsNullOrWhiteSpace(shopRequestDTO.ShopType) ? DBNull.Value : shopRequestDTO.ShopType);
        command.Parameters.AddWithValue("@object_type", shopRequestDTO.ObjectType ?? (object)DBNull.Value);
        command.Parameters.AddWithValue("@sequence", shopRequestDTO.Sequence);
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
            AND sd.sequence = @sequence
            AND sd.is_deleted = FALSE 
            AND sd.is_active = TRUE;";

        await using var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();

        await using var command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@shopCodeName", string.IsNullOrWhiteSpace(shopRequestDTO.ShopCodeName) ? DBNull.Value : shopRequestDTO.ShopCodeName);
        command.Parameters.AddWithValue("@shopType", string.IsNullOrWhiteSpace(shopRequestDTO.ShopType) ? DBNull.Value : shopRequestDTO.ShopType);
        command.Parameters.AddWithValue("@object_type", shopRequestDTO.ObjectType);
        command.Parameters.AddWithValue("@sequence", shopRequestDTO.Sequence);

        // ExecuteScalarAsync nhanh và tối ưu nhất cho truy vấn trả về 1 giá trị duy nhất (Aggregate/Count)
        var result = await command.ExecuteScalarAsync();

        return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
    }
}