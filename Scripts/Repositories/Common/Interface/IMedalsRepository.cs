using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMedalsRepository
{
    Task<List<string>> GetUniqueMedalsIdAsync();
    Task<List<Medals>> GetMedalsAsync(int pageSize, int offset, string rare);
    Task<int> GetMedalsCountAsync(string rare);
    Task<List<Medals>> GetMedalsWithPriceAsync(int pageSize, int offset);
    Task<int> GetMedalsWithPriceCountAsync();
    Task<Medals> GetMedalByIdAsync(string Id);
    Task<Medals> SumPowerMedalsPercentAsync();
}
