using System.Collections.Generic;
using System.Threading.Tasks;

public interface IArchitecturesService
{
    Task<List<string>> GetUniqueArchitecturesIdAsync();
    Task<List<Architectures>> GetArchitecturesAsync(int pageSize, int offset, string rare);
    Task<int> GetArchitecturesCountAsync(string rare);
    Task<List<Architectures>> GetArchitecturesWithPriceAsync(int pageSize, int offset);
    Task<int> GetArchitecturesWithPriceCountAsync();
    Task<Architectures> GetArchitectureByIdAsync(string id);
    Task<Architectures> SumPowerArchitecturesPercentAsync();
}
