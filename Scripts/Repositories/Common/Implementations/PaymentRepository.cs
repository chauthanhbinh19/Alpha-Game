using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using UnityEngine;

public class PaymentRepository : IPaymentRepository
{
    /// <summary>
    /// Gọi Stored Procedure nạp tiền vào MySQL và Debug.Log kết quả
    /// </summary>
    /// <summary>
    /// Thực thi Stored Procedure xử lý nạp tiền (Hỗ trợ DIRECT_BUY, CREATE_PENDING, FULFILL)
    /// </summary>
    public async Task<TopupResultCode> ProcessTopupAsync(TopupRequestDTO request, string actionType = "DIRECT_BUY", string providerTxId = null)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = new MySqlCommand("sp_process_topup_multi_currency", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // 1. Input parameters (Khớp với SP mới)
                    command.Parameters.AddWithValue("p_action_type", actionType);
                    command.Parameters.AddWithValue("p_transaction_id", request.TransactionId);
                    command.Parameters.AddWithValue("p_provider_tx_id", string.IsNullOrEmpty(providerTxId) ? (object)DBNull.Value : providerTxId);
                    command.Parameters.AddWithValue("p_player_id", request.PlayerId);
                    command.Parameters.AddWithValue("p_package_id", request.PackageId);
                    command.Parameters.AddWithValue("p_provider", request.Provider);
                    command.Parameters.AddWithValue("p_charged_amount", request.ChargedAmount);
                    command.Parameters.AddWithValue("p_charged_currency", request.ChargedCurrency);

                    // 2. Output parameter
                    var resultCodeParam = new MySqlParameter("p_result_code", MySqlDbType.Int32)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(resultCodeParam);

                    // 3. Execute
                    await command.ExecuteNonQueryAsync();

                    // 4. Parse result
                    int rawResult = Convert.ToInt32(resultCodeParam.Value);
                    TopupResultCode resultCode = (TopupResultCode)rawResult;

                    // Debug Log
                    if (resultCode == TopupResultCode.Success)
                    {
                        Debug.Log($"<color=green>[PaymentRepository] SUCCESS ({actionType}):</color> Player {request.PlayerId} | Package: {request.PackageId} | TxId: {request.TransactionId}");
                    }
                    else if (resultCode == TopupResultCode.AlreadyProcessed)
                    {
                        Debug.LogWarning($"[PaymentRepository] WARNING: Transaction {request.TransactionId} was ALREADY PROCESSED.");
                    }
                    else
                    {
                        Debug.LogError($"[PaymentRepository] FAILED ({actionType}): Code {resultCode} (Raw: {rawResult}) for Player {request.PlayerId}");
                    }

                    return resultCode;
                }
            }
        }
        catch (MySqlException ex)
        {
            Debug.LogError($"[PaymentRepository] MySQL Exception: {ex.Number} - {ex.Message}");
            return TopupResultCode.DatabaseError;
        }
        catch (Exception ex)
        {
            Debug.LogError($"[PaymentRepository] Exception: {ex.Message}");
            return TopupResultCode.DatabaseError;
        }
    }

    /// <summary>
    /// Lấy danh sách tất cả các danh mục gói nạp
    /// </summary>
    public async Task<List<string>> GetAllCategoriesAsync()
    {
        var categories = new List<string>();
        string connectionString = DatabaseConfig.ConnectionString;

        string sql = @"
                SELECT DISTINCT category 
                FROM shop_packages 
                WHERE is_active = 1 AND category IS NOT NULL AND category != ''
                ORDER BY category ASC;";

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(sql, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                categories.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[PaymentRepository] GetAllCategories Exception: {ex.Message}");
        }

        return categories;
    }

    /// <summary>
    /// Lấy tất cả các gói nạp đang hoạt động (is_active = 1) để hiển thị lên UI Shop
    /// </summary>
    public async Task<List<ShopPackageModel>> GetAllActivePackagesAsync(string categoryFilter = null)
    {
        var packages = new List<ShopPackageModel>();
        string connectionString = DatabaseConfig.ConnectionString;

        // Dùng INNER JOIN để đảm bảo lấy được ảnh tiền tệ từ bảng currencies
        string sql = @"
                SELECT sp.package_id, sp.package_name, sp.category, sp.price_usd, sp.original_price_usd, 
                       sp.discount_percent, sp.reward_currency_id, sp.reward_amount, sp.is_active,
                       COALESCE(c.image, '') AS reward_currency_image
                FROM shop_packages sp
                INNER JOIN currencies c ON sp.reward_currency_id = c.id
                WHERE sp.is_active = 1 
                  AND (@Category IS NULL OR sp.category = @Category)
                ORDER BY sp.category ASC, sp.price_usd ASC;";

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Category", string.IsNullOrEmpty(categoryFilter) ? (object)DBNull.Value : categoryFilter);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            packages.Add(MapReaderToShopPackage(reader));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[PaymentRepository] GetAllActivePackages Exception: {ex.Message}");
        }

        return packages;
    }

    /// <summary>
    /// Lấy chi tiết một gói nạp theo packageId
    /// </summary>
    public async Task<ShopPackageModel> GetPackageByIdAsync(string packageId)
    {
        string connectionString = DatabaseConfig.ConnectionString;

        string sql = @"
                SELECT sp.package_id, sp.package_name, sp.category, sp.price_usd, sp.original_price_usd, 
                       sp.discount_percent, sp.reward_currency_id, sp.reward_amount, sp.is_active,
                       COALESCE(c.image, '') AS reward_currency_image
                FROM shop_packages sp
                LEFT JOIN currencies c ON sp.reward_currency_id = c.id
                WHERE sp.package_id = @PackageId AND sp.is_active = 1;";

        try
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@PackageId", packageId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return MapReaderToShopPackage(reader);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"[PaymentRepository] GetPackageById Exception: {ex.Message}");
        }

        return null;
    }

    #region Private Helper Mapping
    private ShopPackageModel MapReaderToShopPackage(MySqlDataReader reader)
    {
        int origPriceOrdinal = reader.GetOrdinal("original_price_usd");

        return new ShopPackageModel
        {
            PackageId = reader.GetString("package_id"),
            PackageName = reader.GetString("package_name"),
            Category = reader.GetString("category"),
            PriceUsd = reader.GetDecimal("price_usd"),
            OriginalPriceUsd = reader.IsDBNull(origPriceOrdinal) ? null : (decimal?)reader.GetDecimal(origPriceOrdinal),
            DiscountPercent = reader.GetInt32("discount_percent"),
            RewardCurrencyId = reader.GetString("reward_currency_id"),
            RewardCurrencyImage = reader.IsDBNull(reader.GetOrdinal("reward_currency_image")) ? string.Empty : reader.GetString("reward_currency_image"),
            RewardAmount = reader.GetInt64("reward_amount"),
            IsActive = reader.GetBoolean("is_active")
        };
    }
    #endregion
}