using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPaymentService
{
    /// <summary>
    /// Xử lý thanh toán gói nạp dựa trên userId và ShopPackageModel
    /// </summary>
    /// <param name="userId">ID người chơi</param>
    /// <param name="package">Thông tin gói nạp từ Shop</param>
    /// <param name="provider">Cổng thanh toán (mặc định: GOOGLE_PLAY)</param>
    /// <param name="currencyCode">Mã tiền tệ quốc gia (mặc định: VND)</param>
    /// <param name="exchangeRateUsdToLocal">Tỷ giá USD quy đổi ra tiền tệ local (mặc định: 25000)</param>
    Task<TopupResponseDTO> ProcessPackagePaymentAsync(
        long userId,
        ShopPackageModel package,
        string provider = "GOOGLE_PLAY",
        string currencyCode = "VND",
        decimal exchangeRateUsdToLocal = 25000m);

    /// <summary>
    /// Lấy tất cả các gói nạp đang active để hiển thị lên UI Shop
    /// </summary>
    Task<List<ShopPackageModel>> GetActivePackagesAsync();
}