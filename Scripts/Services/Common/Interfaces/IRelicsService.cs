using System.Collections.Generic;
using System.Threading.Tasks;
public interface IRelicsService
{
    Task<List<string>> GetUniqueRelicsTypesAsync();
    Task<List<string>> GetUniqueRelicsIdAsync();
    Task<List<Relics>> GetRelicsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<Relics>> GetRelicsWithoutLimitAsync();
    Task<int> GetRelicsCountAsync(string search, string type, string rare);
    Task<List<Relics>> GetRelicsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetRelicsWithPriceCountAsync(string type);
    Task<Relics> GetRelicByIdAsync(string id);
    Task<Relics> SumPowerRelicsPercentAsync();
}
