using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFashionsGalleryService
{
    Task<List<Fashions>> GetFashionsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetFashionsCountAsync(string search, string type, string rare);
    Task<bool> InsertFashionGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusFashionGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusFashionsGalleryAsync(string userId);
    Task<bool> UpdateTempStarFashionGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarFashionGalleryAsync(string userId, string fashionId);
    Task<bool> UpdateBatchCurrentStarFashionsGalleryAsync(string userId);
    Task<bool> InsertBatchFashionsGalleryAsync(string userId, List<Fashions> fashions);
    Task<Fashions> GetFashionCollectionByIdAsync(string userId, string objectId);
    Task UpdateFashionGalleryPowerAsync(string userId, string Id);
    Task<Fashions> SumPowerFashionsGalleryAsync(string userId);
}