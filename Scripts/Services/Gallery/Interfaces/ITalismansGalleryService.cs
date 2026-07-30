using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITalismansGalleryService
{
    Task<List<Talismans>> GetTalismansCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetTalismansCountAsync(string search, string type, string rare);
    Task<bool> InsertTalismanGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusTalismanGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusTalismansGalleryAsync(string userId);
    Task<bool> UpdateStarTalismanGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarTalismanGalleryAsync(string userId, string talismanId);
    Task<bool> UpdateBatchCurrentStarTalismansGalleryAsync(string userId);
    Task<bool> InsertBatchTalismansGalleryAsync(string userId, List<Talismans> talismans);
    Task<Talismans> GetTalismanCollectionByIdAsync(string userId, string objectId);
    Task UpdateTalismanGalleryPowerAsync(string userId, string Id);
    Task<Talismans> SumPowerTalismansGalleryAsync(string userId);
}