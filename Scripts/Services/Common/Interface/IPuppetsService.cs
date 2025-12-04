using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPuppetsService
{
    Task<List<string>> GetUniquePuppetsTypesAsync();
    Task<List<string>> GetUniquePuppetsIdAsync();
    Task<List<Puppets>> GetPuppetsAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetPuppetsCountAsync(string type, string rare);
    Task<List<Puppets>> GetPuppetsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetPuppetsWithPriceCountAsync(string type);
    Task<Puppets> GetPuppetByIdAsync(string Id);
    Task<Puppets> SumPowerPuppetsPercentAsync();
}
