using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBordersRepository
{
    Task<List<Borders>> GetUserBordersAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserBordersCountAsync(string user_id, string rare);
    Task<bool> InsertUserBorderAsync(Borders borders, string userId);
    Task<bool> InsertUserBorderByIdAsync(Borders borders, string userId);
    Task<Borders> GetBorderByUsedAsync(string user_id);
    Task UpdateIsUsedBorderAsync(string borderId, string userId, bool is_used);
    Task<Borders> SumPowerUserBordersAsync();
}