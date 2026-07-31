using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserBordersRepository
{
    Task<List<Borders>> GetUserBordersAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserBordersCountAsync(string userId, string search, string rare);
    Task<bool> InsertUserBorderByIdAsync(Borders border, string userId);
    Task<InsertOrUpdateResult<Borders>> InsertOrUpdateUserBorderAsync(string userId, Borders border);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Borders>>> InsertOrUpdateUserBordersBatchAsync(string userId, List<Borders> borders);
    Task<InsertOrUpdateResult<bool>> UpdateUserBorderLevelAsync(string userId, Borders border);
    Task<InsertOrUpdateResult<bool>> UpdateUserBorderStarAsync(string userId, Borders border);
    Task<Borders> GetUserBorderByUsedAsync(string userId);
    Task UpdateIsUsedUserBorderAsync(string borderId, string userId, bool is_used);
    Task<Borders> SumPowerUserBordersAsync(string userId);
}