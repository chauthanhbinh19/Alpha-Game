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
    public async Task<TopupResultCode> ProcessTopupAsync(TopupRequestDTO request)
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

                    // 1. Input parameters
                    command.Parameters.AddWithValue("p_transaction_id", request.TransactionId);
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

                    // Debug Log trực tiếp ra Unity Console
                    if (resultCode == TopupResultCode.Success)
                    {
                        Debug.Log($"<color=green>[PaymentRepository] SUCCESS:</color> Topup completed for Player {request.PlayerId} | Package: {request.PackageId} | TxId: {request.TransactionId}");
                    }
                    else if (resultCode == TopupResultCode.AlreadyProcessed)
                    {
                        Debug.LogWarning($"[PaymentRepository] WARNING: Transaction {request.TransactionId} was ALREADY PROCESSED before.");
                    }
                    else
                    {
                        Debug.LogError($"[PaymentRepository] FAILED: Result code {resultCode} for Player {request.PlayerId}");
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
    /// Lấy tất cả các gói nạp đang hoạt động (is_active = 1) để hiển thị lên UI Shop
    /// </summary>
    public async Task<List<ShopPackageModel>> GetAllActivePackagesAsync()
    {
        var packages = new List<ShopPackageModel>();
        string connectionString = DatabaseConfig.ConnectionString;
        string sql = @"
                SELECT package_id, package_name, price_usd, reward_currency_type, reward_amount, is_active
                FROM shop_packages
                WHERE is_active = 1
                ORDER BY price_usd ASC;";

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
                            var pkg = new ShopPackageModel
                            {
                                PackageId = reader.GetString("package_id"),
                                PackageName = reader.GetString("package_name"),
                                PriceUsd = reader.GetDecimal("price_usd"),
                                RewardCurrencyType = reader.GetString("reward_currency_type"),
                                RewardAmount = reader.GetInt64("reward_amount"),
                                IsActive = reader.GetBoolean("is_active")
                            };

                            packages.Add(pkg);
                        }
                    }
                }
            }

            Debug.Log($"<color=green>[PaymentRepository]</color> Fetched {packages.Count} active shop packages successfully.");
        }
        catch (Exception ex)
        {
            Debug.LogError($"[PaymentRepository] GetAllActivePackages Exception: {ex.Message}");
        }

        return packages;
    }

    /// <summary>
    /// Lấy chi tiết gói nạp để test UI hoặc validate
    /// </summary>
    public async Task<ShopPackageModel> GetPackageByIdAsync(string packageId)
    {
        string connectionString = DatabaseConfig.ConnectionString;
        string sql = @"
                SELECT package_id, package_name, price_usd, reward_currency_type, reward_amount, is_active
                FROM shop_packages
                WHERE package_id = @PackageId AND is_active = 1;";

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
                            return new ShopPackageModel
                            {
                                PackageId = reader.GetString("package_id"),
                                PackageName = reader.GetString("package_name"),
                                PriceUsd = reader.GetDecimal("price_usd"),
                                RewardCurrencyType = reader.GetString("reward_currency_type"),
                                RewardAmount = reader.GetInt64("reward_amount"),
                                IsActive = reader.GetBoolean("is_active")
                            };
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
}