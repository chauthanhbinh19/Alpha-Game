using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRelicsRepository
{
    Task<List<Relics>> GetUserRelicsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserRelicsCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserRelicAsync(Relics relic, string userId);
    Task<bool> InsertOrUpdateUserRelicsBatchAsync(string userId, List<Relics> relics);
    Task<bool> UpdateUserRelicLevelAsync(string userId, Relics relic);
    Task<bool> UpdateUserRelicStarAsync(string userId, Relics relic);
    Task<bool> UpdateUserRelicBreakthroughAsync(string userId, Relics relic, int star, double quantity);
    Task<Relics> GetUserRelicByIdAsync(string userId, string Id);
    Task<Relics> SumPowerUserRelicsAsync(string userId);
}