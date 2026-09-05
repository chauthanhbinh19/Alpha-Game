using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PaymentService : IPaymentService
{
    private readonly PaymentRepository _paymentRepository;

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
            Debug.LogError($"[PaymentService] GetActivePackagesAsync Exception: {ex.Message}");
            return new List<ShopPackageModel>();
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
    /// <param name="userId">ID của người chơi</param>
    /// <param name="package">Gói nạp người chơi chọn mua</param>
    /// <param name="provider">Cổng thanh toán (mặc định: GOOGLE_PLAY)</param>
    /// <param name="currencyCode">Mã tiền tệ thanh toán (mặc định: VND)</param>
    /// <param name="exchangeRateUsdToLocal">Tỷ giá quy đổi từ USD ra tiền địa phương (mặc định 1 USD = 25,000 VND)</param>
    public async Task<TopupResponseDTO> ProcessPackagePaymentAsync(
        long userId,
        ShopPackageModel package,
        string provider = "GOOGLE_PLAY",
        string currencyCode = "VND",
        decimal exchangeRateUsdToLocal = 25000m)
    {
        // 1. Validate dữ liệu đầu vào
        if (userId <= 0)
        {
            Debug.LogError("[PaymentService] Invalid UserId!");
            return new TopupResponseDTO
            {
                Success = false,
                ResultCode = TopupResultCode.DatabaseError,
                Message = "Invalid UserId."
            };
        }

        if (package == null || string.IsNullOrEmpty(package.PackageId) || !package.IsActive)
        {
            Debug.LogError($"[PaymentService] Invalid or Inactive ShopPackage for User {userId}!");
            return new TopupResponseDTO
            {
                Success = false,
                ResultCode = TopupResultCode.PackageNotFound,
                Message = "Package is invalid or no longer active."
            };
        }

        // 2. Sinh Transaction ID duy nhất (VD: GPA.BUY-8f3a1b2c hoặc Order GUID)
        string transactionId = $"ORDER_{DateTime.UtcNow:yyyyMMddHHmmss}_{Guid.NewGuid().ToString().Substring(0, 8)}";

        // 3. Tính toán số tiền thanh toán thực tế dựa trên giá USD của gói
        decimal chargedAmount = package.PriceUsd * exchangeRateUsdToLocal;

        // 4. Đóng gói DTO truyền sang Repository
        var topupRequest = new TopupRequestDTO
        {
            TransactionId = transactionId,
            PlayerId = userId,
            PackageId = package.PackageId,
            Provider = provider,
            ChargedAmount = chargedAmount,
            ChargedCurrency = currencyCode
        };

        Debug.Log($"<color=cyan>[PaymentService]</color> Initiating topup for User: {userId} | Package: {package.PackageName} (${package.PriceUsd}) | TxId: {transactionId}");

        // 5. Gọi Repository để nạp vào DB
        TopupResultCode resultCode = await _paymentRepository.ProcessTopupAsync(topupRequest);

        // 6. Xử lý và tổng hợp kết quả trả về
        bool isSuccess = resultCode == TopupResultCode.Success;
        string resultMessage = GetResponseMessage(resultCode, package, userId);

        if (isSuccess)
        {
            Debug.Log($"<color=green>[PaymentService] SUCCESS:</color> Granted {package.RewardAmount} {package.RewardCurrencyId} to User {userId}.");
        }
        else
        {
            Debug.LogWarning($"[PaymentService] FAILED: ResultCode = {resultCode} for User {userId}.");
        }

        return new TopupResponseDTO
        {
            Success = isSuccess,
            ResultCode = resultCode,
            TransactionId = transactionId,
            Message = resultMessage
        };
    }

    private string GetResponseMessage(TopupResultCode resultCode, ShopPackageModel package, long userId)
    {
        return resultCode switch
        {
            TopupResultCode.Success => $"Successfully purchased {package.PackageName}! Received {package.RewardAmount} {package.RewardCurrencyId}.",
            TopupResultCode.AlreadyProcessed => "This transaction has already been processed.",
            TopupResultCode.PackageNotFound => "The selected package is no longer available.",
            _ => "An error occurred while processing the payment. Please try again."
        };
    }
}