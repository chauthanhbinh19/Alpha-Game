using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMedalsGalleryRepository
{
    Task<List<Medals>> GetMedalsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetMedalsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Medals>> InsertMedalGalleryAsync(string userId, string Id, Medals MedalFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusMedalGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusMedalsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarMedalGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarMedalGalleryAsync(string userId, string medalId);
    Task<InsertOrUpdateResult<List<(string MedalId, double CurrentStar)>>> UpdateBatchCurrentStarMedalsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Medals>>> InsertBatchMedalsGalleryAsync(string userId, List<Medals> medals);
    Task<Medals> GetMedalCollectionByIdAsync(string userId, string objectId);
    Task UpdateMedalGalleryPowerAsync(string userId, string id, Medals MedalFromDB);
    Task<Medals> SumPowerMedalsGalleryAsync(string userId);
}