using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRelicsRepository
{
    Task<List<Relics>> GetUserRelicsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserRelicsCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserRelicAsync(Relics relic, string userId);
    Task<bool> InsertOrUpdateUserRelicsBatchAsync(List<Relics> relics);
    Task<bool> UpdateRelicLevelAsync(Relics relic, int level);
    Task<bool> UpdateRelicBreakthroughAsync(Relics relic, int star, double quantity);
    Task<Relics> GetUserRelicByIdAsync(string user_id, string Id);
    Task<Relics> SumPowerUserRelicsAsync();
}