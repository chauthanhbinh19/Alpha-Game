using System.Threading.Tasks;

public interface IGameGrowthConfigService
{
    /// <summary>
    /// Tính toán EXP cần thiết để nâng cấp cho Object dựa trên cấp độ hiện tại
    /// </summary>
    Task<long> CalculateRequiredExpAsync(string objectType, string growthType, int currentLevel);

    /// <summary>
    /// Lấy cấu hình thô từ Cache (dùng khi cần xem thông số cụm)
    /// </summary>
    Task<GameGrowthConfig?> GetConfigAsync(string objectType, string growthType, int currentLevel);

    /// <summary>
    /// Xóa Cache để Reload lại cấu hình mới nhất từ Database (Dùng khi Admin update game)
    /// </summary>
    Task ClearCacheAsync();
}