using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBordersRepository
{
    Task<List<Borders>> GetUserBordersAsync(string user_id, string search, int pageSize, int offset, string rare);
    Task<int> GetUserBordersCountAsync(string user_id, string search, string rare);
    Task<bool> InsertUserBorderAsync(Borders border, string userId);
    Task<bool> InsertUserBorderByIdAsync(Borders border, string userId);
    Task<bool> InsertOrUpdateUserBordersBatchAsync(List<Borders> borders);
    Task<Borders> GetBorderByUsedAsync(string user_id);
    Task UpdateIsUsedBorderAsync(string borderId, string userId, bool is_used);
    Task<Borders> SumPowerUserBordersAsync();
}