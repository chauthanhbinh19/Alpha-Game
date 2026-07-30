using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBeveragesGalleryService
{
    Task<List<Beverages>> GetBeveragesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetBeveragesCountAsync(string search, string rare);
    Task<bool> InsertBeverageGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusBeverageGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusBeveragesGalleryAsync(string userId);
    Task<bool> UpdateStarBeverageGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarBeverageGalleryAsync(string userId, string beverageId);
    Task<bool> UpdateBatchCurrentStarBeveragesGalleryAsync(string userId);
    Task<bool> InsertBatchBeveragesGalleryAsync(string userId, List<Beverages> beverages);
    Task<Beverages> GetBeverageCollectionByIdAsync(string userId, string beverageId);
    Task UpdateBeverageGalleryPowerAsync(string userId, string id);
    Task<Beverages> SumPowerBeveragesGalleryAsync(string userId);
}