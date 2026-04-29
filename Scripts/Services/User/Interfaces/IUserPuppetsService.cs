using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserPuppetsService
{
    Task<List<Puppets>> GetUserPuppetsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserPuppetsCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserPuppetAsync(Puppets puppet, string userId);
    Task<bool> InsertOrUpdateUserPuppetsBatchAsync(List<Puppets> puppets);
    Task<bool> UpdatePuppetLevelAsync(Puppets puppet, int level);
    Task<bool> UpdatePuppetBreakthroughAsync(Puppets puppet, int star, double quantity);
    Task<Puppets> GetUserPuppetByIdAsync(string user_id, string Id);
    Task<Puppets> SumPowerUserPuppetsAsync();
}