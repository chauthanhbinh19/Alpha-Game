using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserTalismansService
{
    Task<Talismans> GetNewLevelPowerAsync(Talismans c, double coefficient);
    Task<Talismans> GetNewBreakthroughPowerAsync(Talismans c, double coefficient);
    Task<List<Talismans>> GetUserTalismansAsync(string user_id, string type, int pageSize, int offset, string rare);
    Task<int> GetUserTalismansCountAsync(string user_id, string type, string rare);
    Task<bool> InsertUserTalismanAsync(Talismans Talisman, string userId);
    Task<bool> UpdateTalismanLevelAsync(Talismans Talisman, int cardLevel);
    Task<bool> UpdateTalismanBreakthroughAsync(Talismans Talisman, int star, double quantity);
    Task<Talismans> GetUserTalismanByIdAsync(string user_id, string Id);
    Task<Talismans> SumPowerUserTalismansAsync();

}