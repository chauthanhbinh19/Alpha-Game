using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBeveragesService
{
    Task<List<string>> GetUniqueBeveragesIdAsync();
    Task<List<Beverages>> GetBeveragesAsync(string search, string rare, int pageSize, int offset);
    Task<List<Beverages>> GetBeveragesWithoutLimitAsync();
    Task<int> GetBeveragesCountAsync(string search, string rare);
    Task<List<Beverages>> GetBeveragesWithPriceAsync(int pageSize, int offset);
    Task<int> GetBeveragesWithPriceCountAsync();
    Task<Beverages> GetBeverageByIdAsync(string id);
    Task<Beverages> SumPowerBeveragesPercentAsync();
}
