using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserTalismansRepository
{
    Task<List<Talismans>> GetUserTalismansAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserTalismansCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserTalismanAsync(Talismans talisman, string userId);
    Task<bool> InsertOrUpdateUserTalismansBatchAsync(List<Talismans> talismans);
    Task<bool> UpdateTalismanLevelAsync(Talismans talisman, int level);
    Task<bool> UpdateTalismanBreakthroughAsync(Talismans talisman, int star, double quantity);
    Task<Talismans> GetUserTalismanByIdAsync(string user_id, string Id);
    Task<Talismans> SumPowerUserTalismansAsync();

}