using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserPuppetsRepository
{
    Task<List<Puppets>> GetUserPuppetsAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserPuppetsCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserPuppetAsync(Puppets Puppet, string userId);
    Task<bool> UpdatePuppetLevelAsync(Puppets Puppet, int cardLevel);
    Task<bool> UpdatePuppetBreakthroughAsync(Puppets Puppet, int star, double quantity);
    Task<Puppets> GetUserPuppetByIdAsync(string user_id, string Id);
    Task<Puppets> SumPowerUserPuppetsAsync();
}