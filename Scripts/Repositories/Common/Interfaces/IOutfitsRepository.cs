using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOutfitsRepository
{
    Task<List<string>> GetUniqueOutfitsTypesAsync();
    Task<List<string>> GetUniqueOutfitsIdAsync();
    Task<List<Outfits>> GetOutfitsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<Outfits>> GetOutfitsWithoutLimitAsync();
    Task<int> GetOutfitsCountAsync(string search, string type, string rare);
    Task<List<Outfits>> GetOutfitsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetOutfitsWithPriceCountAsync(string type);
    Task<Outfits> GetOutfitByIdAsync(string id);
    Task<Outfits> SumPowerOutfitsPercentAsync();
}
