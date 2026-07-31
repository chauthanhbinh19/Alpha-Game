using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserFashionsService
{
    Task<List<Fashions>> GetUserFashionsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserFashionsCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFashionAsync(string userId, Fashions fashion);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserFashionsBatchAsync(string userId, List<Fashions> fashions);
    Task<bool> UpdateUserFashionLevelAsync(string userId, Fashions fashion);
    Task<bool> UpdateUserFashionStarAsync(string userId, Fashions fashion);
    Task<Fashions> GetUserFashionByIdAsync(string userId, string Id);
    Task<Fashions> SumPowerUserFashionsAsync(string userId);
}