using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPuppetsRepository
{
    Task<List<string>> GetUniquePuppetsTypesAsync();
    Task<List<string>> GetUniquePuppetsIdAsync();
    Task<List<Puppets>> GetPuppetsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<int> GetPuppetsCountAsync(string search, string type, string rare);
    Task<List<Puppets>> GetPuppetsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetPuppetsWithPriceCountAsync(string type);
    Task<Puppets> GetPuppetByIdAsync(string Id);
    Task<Puppets> SumPowerPuppetsPercentAsync();
}
