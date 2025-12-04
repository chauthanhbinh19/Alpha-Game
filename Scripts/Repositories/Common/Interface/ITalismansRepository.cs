using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITalismansRepository
{
    Task<List<string>> GetUniqueTalismansTypesAsync();
    Task<List<string>> GetUniqueTalismansIdAsync();
    Task<List<Talismans>> GetTalismansAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetTalismansCountAsync(string type, string rare);
    Task<List<Talismans>> GetTalismansWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetTalismansWithPriceCountAsync(string type);
    Task<Talismans> GetTalismanByIdAsync(string Id);
    Task<Talismans> SumPowerTalismansPercentAsync();
}
