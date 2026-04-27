using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRelicsService
{
    Task<List<Relics>> GetUserRelicsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserRelicsCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserRelicAsync(Relics Relic, string userId);
    Task<bool> UpdateRelicLevelAsync(Relics Relic, int cardLevel);
    Task<bool> UpdateRelicBreakthroughAsync(Relics Relic, int star, double quantity);
    Task<Relics> GetUserRelicByIdAsync(string user_id, string Id);
    Task<Relics> SumPowerUserRelicsAsync();
}