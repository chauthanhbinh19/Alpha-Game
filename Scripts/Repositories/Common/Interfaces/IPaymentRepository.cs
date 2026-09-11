using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPaymentRepository
{
    /// <summary>
    /// Gọi Stored Procedure để cộng tiền cho Player và lưu log
    /// </summary>
    Task<TopupResultCode> ProcessTopupAsync(TopupRequestDTO request, string actionType = "DIRECT_BUY", string providerTxId = null);
    Task<List<string>> GetAllCategoriesAsync();
    /// <summary>
    /// Lấy tất cả các gói nạp đang hoạt động (is_active = 1) để hiển thị lên UI Shop
    /// </summary>
    Task<List<ShopPackageModel>> GetAllActivePackagesAsync(string categoryFilter = null);
    /// <summary>
    /// Lấy chi tiết thông tin gói nạp từ database
    /// </summary>
    Task<ShopPackageModel> GetPackageByIdAsync(string packageId);
}