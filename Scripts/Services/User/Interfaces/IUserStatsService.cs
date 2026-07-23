using System.Threading.Tasks;

public interface IUserStatsService
{
    // Hàm hỗ trợ gom và tạo nhanh Context dùng chung
    Task<UserStatsContextDTO> GetUserStatsContextAsync(string userId);
}