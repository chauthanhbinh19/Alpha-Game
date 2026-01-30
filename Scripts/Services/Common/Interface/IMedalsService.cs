using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMedalsService
{
    Task<List<string>> GetUniqueMedalsIdAsync();
    Task<List<Medals>> GetMedalsAsync(string search, string rare, int pageSize, int offset);
    Task<int> GetMedalsCountAsync(string search, string rare);
    Task<List<Medals>> GetMedalsWithPriceAsync(int pageSize, int offset);
    Task<int> GetMedalsWithPriceCountAsync();
    Task<Medals> GetMedalByIdAsync(string Id);
    Task<Medals> SumPowerMedalsPercentAsync();
}
