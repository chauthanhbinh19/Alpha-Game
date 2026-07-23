using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserWorldsRepository
{
    Task<List<Worlds>> GetUserWorldsAsync(string userId, int pageSize, int offset, string rare);
    Task<int> GetUserWorldsCountAsync(string userId, string rare);
    Task<bool> InsertUserWorldAsync(Worlds Worlds, string userId);
    Task<bool> UpdateUserWorldLevelAsync(string userId, Worlds Worlds, int WorldLevel);
    Task<bool> UpdateUserWorldBreakthroughAsync(string userId, Worlds Worlds, int star, double quantity);
    Task<Worlds> GetUserWorldByIdAsync(string userId, string Id);
    Task<Worlds> SumPowerUserWorldsAsync(string userId);
}