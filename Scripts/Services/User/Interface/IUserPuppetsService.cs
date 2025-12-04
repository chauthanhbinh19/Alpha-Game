using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserPuppetsService
{
    Task<Puppets> GetNewLevelPowerAsync(Puppets c, double coefficient);
    Task<Puppets> GetNewBreakthroughPowerAsync(Puppets c, double coefficient);
    Task<List<Puppets>> GetUserPuppetsAsync(string user_id, string type, int pageSize, int offset, string rare);
    Task<int> GetUserPuppetsCountAsync(string user_id, string type, string rare);
    Task<bool> InsertUserPuppetAsync(Puppets Puppet, string userId);
    Task<bool> UpdatePuppetLevelAsync(Puppets Puppet, int cardLevel);
    Task<bool> UpdatePuppetBreakthroughAsync(Puppets Puppet, int star, double quantity);
    Task<Puppets> GetUserPuppetByIdAsync(string user_id, string Id);
    Task<Puppets> SumPowerUserPuppetsAsync();
}