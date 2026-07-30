using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRelicsGalleryService
{
    Task<List<Relics>> GetRelicsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetRelicsCountAsync(string search, string type, string rare);
    Task<bool> InsertRelicGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusRelicGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusRelicsGalleryAsync(string userId);
    Task<bool> UpdateStarRelicGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarRelicGalleryAsync(string userId, string relicId);
    Task<bool> UpdateBatchCurrentStarRelicsGalleryAsync(string userId);
    Task<bool> InsertBatchRelicsGalleryAsync(string userId, List<Relics> relics);
    Task<Relics> GetRelicCollectionByIdAsync(string userId, string objectId);
    Task UpdateRelicGalleryPowerAsync(string userId, string Id);
    Task<Relics> SumPowerRelicsGalleryAsync(string userId);
}