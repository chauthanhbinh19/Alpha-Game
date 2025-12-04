using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBasesService
{
    Task<List<string>> GetUniqueBasesIdAsync();
    Task<List<Bases>> GetBasesAsync(string userId, int pageSize, int offset);
    Task<int> GetBasesCountAsync(string rare);
    Task<List<Bases>> GetBasesWithPriceAsync(int pageSize, int offset);
    Task<int> GetBasesWithPriceCountAsync();
    Task<Bases> GetBaseByIdAsync(string Id);
    Task<Bases> SumPowerBasesPercentAsync();
}
