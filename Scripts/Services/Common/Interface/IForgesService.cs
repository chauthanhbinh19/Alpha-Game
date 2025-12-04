using System.Collections.Generic;
using System.Threading.Tasks;

public interface IForgesService
{
    Task<List<string>> GetUniqueForgesTypesAsync();
    Task<List<string>> GetUniqueForgesIdAsync();
    Task<List<Forges>> GetForgesAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetForgesCountAsync(string type, string rare);
    Task<List<Forges>> GetForgesWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetForgesWithPriceCountAsync(string type);
    Task<Forges> GetForgeByIdAsync(string Id);
    Task<Forges> SumPowerForgesPercentAsync();
}
