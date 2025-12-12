using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBeveragesRepository
{
    Task<List<string>> GetUniqueBeveragesIdAsync();
    Task<List<Beverages>> GetBeveragesAsync(int pageSize, int offset, string rare);
    Task<int> GetBeveragesCountAsync(string rare);
    Task<List<Beverages>> GetBeveragesWithPriceAsync(int pageSize, int offset);
    Task<int> GetBeveragesWithPriceCountAsync();
    Task<Beverages> GetBeverageByIdAsync(string id);
    Task<Beverages> SumPowerBeveragesPercentAsync();
}
