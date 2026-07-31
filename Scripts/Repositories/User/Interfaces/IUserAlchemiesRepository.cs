using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserAlchemiesRepository
{
    Task<List<Alchemies>> GetUserAlchemiesAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserAlchemiesCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<Alchemies>> InsertOrUpdateUserAlchemyAsync(string userId, Alchemies alchemy);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Alchemies>>> InsertOrUpdateUserAlchemiesBatchAsync(string userId, List<Alchemies> alchemies);
    Task<InsertOrUpdateResult<bool>> UpdateUserAlchemyLevelAsync(string userId, Alchemies alchemy);
    Task<InsertOrUpdateResult<bool>> UpdateUserAlchemyStarAsync(string userId, Alchemies alchemy);
    Task<Alchemies> GetUserAlchemyByIdAsync(string userId, string Id);
    Task<Alchemies> SumPowerUserAlchemiesAsync(string userId);
}
