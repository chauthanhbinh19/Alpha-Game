using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserFashionsRepository
{
    Task<List<Fashions>> GetUserFashionsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserFashionsCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<Fashions>> InsertOrUpdateUserFashionAsync(string userId, Fashions fashion);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Fashions>>> InsertOrUpdateUserFashionsBatchAsync(string userId, List<Fashions> fashions);
    Task<InsertOrUpdateResult<bool>> UpdateUserFashionLevelAsync(string userId, Fashions fashion);
    Task<InsertOrUpdateResult<bool>> UpdateUserFashionStarAsync(string userId, Fashions fashion);
    Task<Fashions> GetUserFashionByIdAsync(string userId, string Id);
    Task<Fashions> SumPowerUserFashionsAsync(string userId);
}