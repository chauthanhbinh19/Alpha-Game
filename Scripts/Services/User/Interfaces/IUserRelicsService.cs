using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRelicsService
{
    Task<List<Relics>> GetUserRelicsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserRelicsCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRelicAsync(string userId, Relics relic);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserRelicsBatchAsync(string userId, List<Relics> relics);
    Task<InsertOrUpdateResult<bool>> UpdateUserRelicLevelAsync(string userId, Relics relic, List<RecipeItemDetailDTO> totalDeductions);
    Task<InsertOrUpdateResult<bool>> UpdateUserRelicStarAsync(string userId, Relics relic);
    Task<InsertOrUpdateResult<bool>> UpdateUserRelicQuantityAsync(string userId, Relics relic, List<RecipeItemDetailDTO> totalDeductions);
    Task<Relics> GetUserRelicByIdAsync(string userId, string Id);
    Task<Relics> SumPowerUserRelicsAsync(string userId);
}