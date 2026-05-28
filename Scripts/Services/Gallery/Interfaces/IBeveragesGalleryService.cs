using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBeveragesGalleryService
{
    Task<List<Beverages>> GetBeveragesCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetBeveragesCountAsync(string search, string rare);
    Task InsertBeverageGalleryAsync(string Id);
    Task UpdateStatusBeverageGalleryAsync(string Id);
    Task UpdateStarBeverageGalleryAsync(string id, double star);
    Task UpdateBeverageGalleryPowerAsync(string id);
    Task<Beverages> SumPowerBeveragesGalleryAsync();
}