using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserFashionsService
{
    Task<Fashions> GetNewLevelPowerAsync(Fashions c, double coefficient);
    Task<Fashions> GetNewBreakthroughPowerAsync(Fashions c, double coefficient);
    Task<List<Fashions>> GetUserFashionsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserFashionsCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserFashionAsync(Fashions Fashion, string userId);
    Task<bool> UpdateFashionLevelAsync(Fashions Fashion, int cardLevel);
    Task<bool> UpdateFashionBreakthroughAsync(Fashions Fashion, int star, double quantity);
    Task<Fashions> GetUserFashionByIdAsync(string user_id, string Id);
    Task<Fashions> SumPowerUserFashionsAsync();
}