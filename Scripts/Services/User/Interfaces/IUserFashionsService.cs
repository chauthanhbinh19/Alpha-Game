using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserFashionsService
{
    Task<List<Fashions>> GetUserFashionsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserFashionsCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserFashionAsync(Fashions fashion, string userId);
    Task<bool> InsertOrUpdateUserFashionsBatchAsync(List<Fashions> fashions);
    Task<bool> UpdateFashionLevelAsync(Fashions fashion, int level);
    Task<bool> UpdateFashionBreakthroughAsync(Fashions fashion, int star, double quantity);
    Task<Fashions> GetUserFashionByIdAsync(string user_id, string Id);
    Task<Fashions> SumPowerUserFashionsAsync();
}