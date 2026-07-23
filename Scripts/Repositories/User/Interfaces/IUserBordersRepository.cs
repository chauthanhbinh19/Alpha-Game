using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBordersRepository
{
    Task<List<Borders>> GetUserBordersAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserBordersCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserBorderAsync(Borders border, string userId);
    Task<bool> InsertUserBorderByIdAsync(Borders border, string userId);
    Task<bool> InsertOrUpdateUserBordersBatchAsync(string userId, List<Borders> borders);
    Task<bool> UpdateUserBorderLevelAsync(string userId, Borders border);
    Task<bool> UpdateUserBorderStarAsync(string userId, Borders border);
    Task<Borders> GetUserBorderByUsedAsync(string userId);
    Task UpdateIsUsedUserBorderAsync(string borderId, string userId, bool is_used);
    Task<Borders> SumPowerUserBordersAsync(string userId);
}