using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRelicsRepository
{
    Task<List<Relics>> GetUserRelicsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserRelicsCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<Relics>> InsertOrUpdateUserRelicAsync(string userId, Relics relic);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Relics>>> InsertOrUpdateUserRelicsBatchAsync(string userId, List<Relics> relics);
    Task<InsertOrUpdateResult<bool>> UpdateUserRelicLevelAsync(string userId, Relics relic);
    Task<InsertOrUpdateResult<bool>> UpdateUserRelicStarAsync(string userId, Relics relic);
    Task<Relics> GetUserRelicByIdAsync(string userId, string Id);
    Task<Relics> SumPowerUserRelicsAsync(string userId);
}