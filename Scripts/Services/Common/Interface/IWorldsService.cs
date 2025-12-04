using System.Collections.Generic;
using System.Threading.Tasks;

public interface IWorldsService
{
    Task<List<string>> GetUniqueWorldsIdAsync();
    Task<List<Worlds>> GetWorldsAsync(string userId, int pageSize, int offset);
    Task<int> GetWorldsCountAsync(string rare);
    Task<List<Worlds>> GetWorldsWithPriceAsync(int pageSize, int offset);
    Task<int> GetWorldsWithPriceCountAsync();
    Task<Worlds> GetWorldByIdAsync(string id);
    Task<Worlds> SumPowerWorldsPercentAsync();
}
