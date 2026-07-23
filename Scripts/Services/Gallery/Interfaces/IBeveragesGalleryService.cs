using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBeveragesGalleryService
{
    Task<List<Beverages>> GetBeveragesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetBeveragesCountAsync(string search, string rare);
    Task InsertBeverageGalleryAsync(string userId, string Id);
    Task UpdateStatusBeverageGalleryAsync(string userId, string Id);
    Task UpdateStarBeverageGalleryAsync(string userId, string id, double star);
    Task UpdateBeverageGalleryPowerAsync(string userId, string id);
    Task<Beverages> SumPowerBeveragesGalleryAsync(string userId);
}