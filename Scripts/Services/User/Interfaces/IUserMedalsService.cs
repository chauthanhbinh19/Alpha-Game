using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserMedalsService
{
    Task<List<Medals>> GetUserMedalsAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetUserMedalsCountAsync(string userId, string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMedalAsync(string userId, Medals medal);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserMedalsBatchAsync(string userId, List<Medals> medals);
    Task<bool> UpdateUserMedalLevelAsync(string userId, Medals medal);
    Task<bool> UpdateUserMedalStarAsync(string userId, Medals medal);
    Task<Medals> GetUserMedalByIdAsync(string userId, string Id);
    Task<Medals> SumPowerUserMedalsAsync(string userId);

}