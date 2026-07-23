using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserFashionsRepository
{
    Task<List<Fashions>> GetUserFashionsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserFashionsCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserFashionAsync(Fashions fashion, string userId);
    Task<bool> InsertOrUpdateUserFashionsBatchAsync(string userId, List<Fashions> fashions);
    Task<bool> UpdateUserFashionLevelAsync(string userId, Fashions fashion);
    Task<bool> UpdateUserFashionStarAsync(string userId, Fashions fashion);
    Task<bool> UpdateUserFashionBreakthroughAsync(string userId, Fashions fashion, int star, double quantity);
    Task<Fashions> GetUserFashionByIdAsync(string userId, string Id);
    Task<Fashions> SumPowerUserFashionsAsync(string userId);
}