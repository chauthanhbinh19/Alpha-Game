using System.Threading.Tasks;

public interface IPaymentRepository
{
    /// <summary>
    /// Gọi Stored Procedure để cộng tiền cho Player và lưu log
    /// </summary>
    Task<TopupResultCode> ProcessTopupAsync(TopupRequestDTO request);

    /// <summary>
    /// Lấy chi tiết thông tin gói nạp từ database
    /// </summary>
    Task<ShopPackageModel> GetPackageByIdAsync(string packageId);
}