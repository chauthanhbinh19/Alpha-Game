using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITalismansService
{
    Task<List<string>> GetUniqueTalismansTypesAsync();
    Task<List<string>> GetUniqueTalismansIdAsync();
    Task<List<Talismans>> GetTalismansAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<Talismans>> GetTalismansWithoutLimitAsync();
    Task<int> GetTalismansCountAsync(string search, string type, string rare);
    Task<List<Talismans>> GetTalismansWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetTalismansWithPriceCountAsync(string type);
    Task<Talismans> GetTalismanByIdAsync(string Id);
    Task<Talismans> SumPowerTalismansPercentAsync();
}
