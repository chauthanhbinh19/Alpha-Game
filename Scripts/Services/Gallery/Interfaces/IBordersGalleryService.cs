using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBordersGalleryService
{
    Task<List<Borders>> GetBordersCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetBordersCountAsync(string search, string rare);
    Task<bool> InsertBorderGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusBorderGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusBordersGalleryAsync(string userId);
    Task<bool> UpdateTempStarBorderGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarBorderGalleryAsync(string userId, string borderId);
    Task<bool> UpdateBatchCurrentStarBordersGalleryAsync(string userId);
    Task<bool> InsertBatchBordersGalleryAsync(string userId, List<Borders> borders);
    Task<Borders> GetBorderCollectionByIdAsync(string userId, string objectId);
    Task UpdateBorderGalleryPowerAsync(string userId, string id);
    Task<Borders> SumPowerBordersGalleryAsync(string userId);
}