using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBeveragesGalleryService
{
    Task<List<Beverages>> GetBeveragesCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetBeveragesCountAsync(string rare);
    Task InsertBeverageGalleryAsync(string Id);
    Task UpdateStatusBeverageGalleryAsync(string Id);
    Task UpdateStarBeverageGalleryAsync(string id, double star);
    Task UpdateBeverageGalleryPowerAsync(string id);
    Task<Beverages> SumPowerBeveragesGalleryAsync();
}