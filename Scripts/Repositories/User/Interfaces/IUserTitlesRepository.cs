using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserTitlesRepository
{
    Task<List<Titles>> GetUserTitlesAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserTitlesCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Titles>> InsertOrUpdateUserTitleAsync(string userId, Titles title);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Titles>>> InsertOrUpdateUserTitlesBatchAsync(string userId, List<Titles> titles);
    Task<InsertOrUpdateResult<bool>> UpdateUserTitleLevelAsync(string userId, Titles title);
    Task<InsertOrUpdateResult<bool>> UpdateUserTitleStarAsync(string userId, Titles title);
    Task<Titles> GetUserTitleByIdAsync(string userId, string Id);
    Task<Titles> SumPowerUserTitlesAsync(string userId);
}