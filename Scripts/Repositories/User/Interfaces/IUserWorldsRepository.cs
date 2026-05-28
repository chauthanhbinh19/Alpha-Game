using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserWorldsRepository
{
    Task<List<Worlds>> GetUserWorldsAsync(string user_id, int pageSize, int offset, string rare);
    Task<int> GetUserWorldsCountAsync(string user_id, string rare);
    Task<bool> InsertUserWorldAsync(Worlds Worlds, string userId);
    Task<bool> UpdateWorldLevelAsync(Worlds Worlds, int WorldLevel);
    Task<bool> UpdateWorldBreakthroughAsync(Worlds Worlds, int star, double quantity);
    Task<Worlds> GetUserWorldByIdAsync(string user_id, string Id);
    Task<Worlds> SumPowerUserWorldsAsync();
}