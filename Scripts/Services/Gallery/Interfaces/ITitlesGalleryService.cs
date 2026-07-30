using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITitlesGalleryService
{
    Task<List<Titles>> GetTitlesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetTitlesCountAsync(string search, string rare);
    Task<bool> InsertTitleGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusTitleGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusTitlesGalleryAsync(string userId);
    Task<bool> UpdateStarTitleGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarTitleGalleryAsync(string userId, string titleId);
    Task<bool> UpdateBatchCurrentStarTitlesGalleryAsync(string userId);
    Task<bool> InsertBatchTitlesGalleryAsync(string userId, List<Titles> titles);
    Task<Titles> GetTitleCollectionByIdAsync(string userId, string titleId);
    Task UpdateTitleGalleryPowerAsync(string userId, string id);
    Task<Titles> SumPowerTitlesGalleryAsync(string userId);
}