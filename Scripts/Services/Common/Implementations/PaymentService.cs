using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class PaymentService : IPaymentService
{
    private readonly PaymentRepository _paymentRepository;

    // Cache tự tạo bằng ConcurrentDictionary (Thread-safe)
    private static readonly ConcurrentDictionary<string, CacheEntry> _processedTransactions
        = new ConcurrentDictionary<string, CacheEntry>();

    // Lock chặn Spam Click (Đã có TryRemove ở finally nên an toàn RAM)
    private static readonly ConcurrentDictionary<string, bool> _activeLocks
        = new ConcurrentDictionary<string, bool>();

    // Cấu hình thời gian lưu Cache (Ví dụ: 10 phút)
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

    public PaymentService(PaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public static IPaymentService Create() => ServiceContainer.GetService<IPaymentService>();

    /// <summary>
    /// Lấy danh sách các gói nạp đang active, hỗ trợ lọc theo Tab (Category)
    /// </summary>
    public async Task<List<ShopPackageModel>> GetAllActivePackagesAsync(string categoryFilter = null)
    {
        try
        {
            return await _paymentRepository.GetAllActivePackagesAsync(categoryFilter);
        }
        catch (Exception ex)
        {
            Debug.LogError($"[PaymentService] GetAllActivePackagesAsync Exception: {ex.Message}");
            return new List<ShopPackageModel>();
        }
    }

    /// <summary>
    /// Lấy danh sách tất cả các Category
    /// </summary>
    public async Task<List<string>> GetAllCategoriesAsync()
    {
        try
        {
            return await _paymentRepository.GetAllCategoriesAsync();
        }
        catch (Exception ex)
        {
            Debug.LogError($"[PaymentService] GetAllCategoriesAsync Exception: {ex.Message}");
            return new List<string>();
        }
    }

    /// <summary>
    /// Lấy thông tin chi tiết của 1 gói nạp theo packageId
    /// </summary>
    public async Task<ShopPackageModel> GetPackageByIdAsync(string packageId)
    {
        if (string.IsNullOrEmpty(packageId))
        {
            Debug.LogWarning("[PaymentService] GetPackageByIdAsync: packageId is null or empty.");
            return null;
        }

        try
        {
            return await _paymentRepository.GetPackageByIdAsync(packageId);
        }
        catch (Exception ex)
        {
            Debug.LogError($"[PaymentService] GetPackageByIdAsync Exception: {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Xử lý thanh toán/nạp tiền bằng cách truyền vào userId và ShopPackageModel
    /// </summary>
    /// <param name="userId">ID của người chơi (string)</param>
    /// <param name="package">Gói nạp người chơi chọn mua</param>
    /// <param name="idempotencyKey">Mã định danh duy nhất cho Request (truyền từ Client hoặc sinh tự động)</param>
    /// <param name="actionType">Loại hành động: DIRECT_BUY, CREATE_PENDING, FULFILL</param>
    /// <param name="providerTxId">Transaction ID từ nhà cung cấp (VNPAY, Google, Apple...)</param>
    /// <param name="provider">Cổng thanh toán (mặc định: GOOGLE_PLAY)</param>
    /// <param name="currencyCode">Mã tiền tệ thanh toán (mặc định: VND)</param>
    /// <param name="exchangeRateUsdToLocal">Tỷ giá quy đổi từ USD ra tiền địa phương (mặc định 1 USD = 25,000 VND)</param>
    public async Task<TopupResponseDTO> ProcessPackagePaymentAsync(
        string userId,
        ShopPackageModel package,
        string idempotencyKey = null,
        string actionType = "DIRECT_BUY",
        string providerTxId = null,
        string provider = "GOOGLE_PLAY",
        string currencyCode = "VND",
        decimal exchangeRateUsdToLocal = 25000m)
    {
        // Dọn dẹp bớt các Key cũ đã quá hạn để giải phóng RAM
        CleanupExpiredCache();

        // 1. Validate Input
        if (string.IsNullOrEmpty(userId))
        {
            return new TopupResponseDTO
            {
                Success = false,
                ResultCode = TopupResultCode.DatabaseError,
                Message = MessageConstants.INVALID_USER_ID
            };
        }

        if (package == null || string.IsNullOrEmpty(package.PackageId) || !package.IsActive)
        {
            return new TopupResponseDTO
            {
                Success = false,
                ResultCode = TopupResultCode.PackageNotFound,
                Message = MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE
            };
        }

        // 2. Tạo Idempotency Key
        if (string.IsNullOrEmpty(idempotencyKey))
        {
            idempotencyKey = $"IDEM_{userId}_{package.PackageId}_{actionType}";
        }

        // 3. CHECK CACHE: Kiểm tra xem đã có trong Cache và còn hạn không
        if (_processedTransactions.TryGetValue(idempotencyKey, out CacheEntry cachedEntry))
        {
            if (DateTime.UtcNow < cachedEntry.ExpirationTime)
            {
                Debug.LogWarning($"[PaymentService] Idempotency Hit! Key: {idempotencyKey}");
                return cachedEntry.Response;
            }
            else
            {
                _processedTransactions.TryRemove(idempotencyKey, out _);
            }
        }

        // 4. LOCKING: Chống Spam Click đồng thời
        if (!_activeLocks.TryAdd(idempotencyKey, true))
        {
            return new TopupResponseDTO
            {
                Success = false,
                ResultCode = TopupResultCode.DatabaseError,
                Message = MessageConstants.TRANSACTION_PROCESSING
            };
        }

        try
        {
            // 5. Thực thi nạp tiền
            string transactionId = $"ORDER_{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid().ToString().Substring(0, 8)}";
            decimal chargedAmount = package.PriceUsd * exchangeRateUsdToLocal;

            var topupRequest = new TopupRequestDTO
            {
                TransactionId = transactionId,
                PlayerId = userId,
                PackageId = package.PackageId,
                Provider = provider,
                ChargedAmount = chargedAmount,
                ChargedCurrency = currencyCode
            };

            // Gọi Repository truyền thêm actionType và providerTxId
            TopupResultCode resultCode = await _paymentRepository.ProcessTopupAsync(topupRequest, actionType, providerTxId);

            bool isSuccess = resultCode == TopupResultCode.Success || resultCode == TopupResultCode.AlreadyProcessed;

            var response = new TopupResponseDTO
            {
                Success = isSuccess,
                ResultCode = resultCode,
                TransactionId = transactionId,
                Message = GetResponseMessage(resultCode)
            };

            // 6. THÊM VÀO CACHE KÈM EXPIRATION TIME
            if (isSuccess)
            {
                _processedTransactions[idempotencyKey] = new CacheEntry
                {
                    Response = response,
                    ExpirationTime = DateTime.UtcNow.Add(CacheDuration)
                };
            }

            return response;
        }
        finally
        {
            // GIẢI PHÓNG LOCK NGAY LẬP TỨC
            _activeLocks.TryRemove(idempotencyKey, out _);
        }
    }

    /// <summary>
    /// Hàm tự động quét và giải phóng RAM cho các Key đã hết hạn
    /// </summary>
    private void CleanupExpiredCache()
    {
        DateTime now = DateTime.UtcNow;
        var expiredKeys = _processedTransactions
            .Where(kvp => kvp.Value.ExpirationTime <= now)
            .Select(kvp => kvp.Key)
            .ToList();

        foreach (var key in expiredKeys)
        {
            _processedTransactions.TryRemove(key, out _);
        }
    }

    /// <summary>
    /// Map ResultCode sang Message Key chuẩn hoá theo MessageConstants
    /// </summary>
    private string GetResponseMessage(TopupResultCode resultCode)
    {
        return resultCode switch
        {
            TopupResultCode.Success => MessageConstants.INSERT_ITEM_INTO_INVENTORY,
            TopupResultCode.AlreadyProcessed => MessageConstants.TRANSACTION_ALREADY_PROCESSED,
            TopupResultCode.PackageNotFound => MessageConstants.THE_DATA_WAS_DELETED_OR_INACTIVE,
            TopupResultCode.PlayerNotFound => MessageConstants.INVALID_USER_ID,
            TopupResultCode.PendingCreated => MessageConstants.TRANSACTION_PENDING,
            TopupResultCode.DatabaseError => MessageConstants.PURCHASE_FAILED,
            _ => MessageConstants.PURCHASE_FAILED
        };
    }
}