using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMedalsRepository
{
    Task<List<Medals>> GetUserMedalsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserMedalsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<Medals>> InsertOrUpdateUserMedalAsync(string userId, Medals medal);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Medals>>> InsertOrUpdateUserMedalsBatchAsync(string userId, List<Medals> medals);
    Task<InsertOrUpdateResult<bool>> UpdateUserMedalLevelAsync(string userId, Medals medal);
    Task<InsertOrUpdateResult<bool>> UpdateUserMedalStarAsync(string userId, Medals medal);
    Task<Medals> GetUserMedalByIdAsync(string userId, string Id);
    Task<Medals> SumPowerUserMedalsAsync(string userId);

}