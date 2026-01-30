using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArchitecturesService
{
    Task<List<string>> GetUniqueArchitecturesIdAsync();
    Task<List<Architectures>> GetArchitecturesAsync(string search, string rare, int pageSize, int offset);
    Task<int> GetArchitecturesCountAsync(string search, string rare);
    Task<List<Architectures>> GetArchitecturesWithPriceAsync(int pageSize, int offset);
    Task<int> GetArchitecturesWithPriceCountAsync();
    Task<Architectures> GetArchitectureByIdAsync(string id);
    Task<Architectures> SumPowerArchitecturesPercentAsync();
}
