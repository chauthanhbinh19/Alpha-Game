using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserPuppetsService
{
    Task<List<Puppets>> GetUserPuppetsAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserPuppetsCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPuppetAsync(string userId, Puppets puppet);
    Task<InsertOrUpdateResult<bool>> InsertOrUpdateUserPuppetsBatchAsync(string userId, List<Puppets> puppets);
    Task<bool> UpdateUserPuppetLevelAsync(string userId, Puppets puppet);
    Task<bool> UpdateUserPuppetStarAsync(string userId, Puppets puppet);
    Task<Puppets> GetUserPuppetByIdAsync(string userId, string Id);
    Task<Puppets> SumPowerUserPuppetsAsync(string userId);
}