using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMedalsGalleryService
{
    Task<List<Medals>> GetMedalsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetMedalsCountAsync(string search, string rare);
    Task<bool> InsertMedalGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusMedalGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusMedalsGalleryAsync(string userId);
    Task<bool> UpdateStarMedalGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarMedalGalleryAsync(string userId, string medalId);
    Task<bool> UpdateBatchCurrentStarMedalsGalleryAsync(string userId);
    Task<bool> InsertBatchMedalsGalleryAsync(string userId, List<Medals> medals);
    Task<Medals> GetMedalCollectionByIdAsync(string userId, string objectId);
    Task UpdateMedalGalleryPowerAsync(string userId, string id);
    Task<Medals> SumPowerMedalsGalleryAsync(string userId);
}