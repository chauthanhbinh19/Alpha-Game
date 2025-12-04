using System.Collections.Generic;
using System.Threading.Tasks;
public interface IRelicsService
{
    Task<List<string>> GetUniqueRelicsTypesAsync();
    Task<List<string>> GetUniqueRelicsIdAsync();
    Task<List<Relics>> GetRelicsAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetRelicsCountAsync(string type, string rare);
    Task<List<Relics>> GetRelicsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetRelicsWithPriceCountAsync(string type);
    Task<Relics> GetRelicByIdAsync(string id);
    Task<Relics> SumPowerRelicsPercentAsync();
}
