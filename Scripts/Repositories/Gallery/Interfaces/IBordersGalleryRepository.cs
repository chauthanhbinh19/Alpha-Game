using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBordersGalleryRepository
{
    Task<List<Borders>> GetBordersCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetBordersCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Borders>> InsertBorderGalleryAsync(string userId, string Id, Borders BorderFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusBorderGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusBordersGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarBorderGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarBorderGalleryAsync(string userId, string borderId);
    Task<InsertOrUpdateResult<List<(string BorderId, double CurrentStar)>>> UpdateBatchCurrentStarBordersGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Borders>>> InsertBatchBordersGalleryAsync(string userId, List<Borders> borders);
    Task<Borders> GetBorderCollectionByIdAsync(string userId, string objectId);
    Task UpdateBorderGalleryPowerAsync(string userId, string id, Borders BorderFromDB);
    Task<Borders> SumPowerBordersGalleryAsync(string userId);
}