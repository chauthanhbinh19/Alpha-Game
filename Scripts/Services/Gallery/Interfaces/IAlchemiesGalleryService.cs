using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAlchemiesGalleryService
{
    Task<List<Alchemies>> GetAlchemiesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetAlchemyCountAsync(string search, string type, string rare);
    Task<bool> InsertAlchemyGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusAlchemyGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusAlchemiesGalleryAsync(string userId);
    Task<bool> UpdateStarAlchemyGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarAlchemyGalleryAsync(string userId, string alchemyId);
    Task<bool> UpdateBatchCurrentStarAlchemiesGalleryAsync(string userId);
    Task<bool> InsertBatchAlchemiesGalleryAsync(string userId, List<Alchemies> alchemies);
    Task<Alchemies> GetAlchemyCollectionByIdAsync(string userId, string objectId);
    Task UpdateAlchemyGalleryPowerAsync(string userId, string Id);
    Task<Alchemies> SumPowerAlchemiesGalleryAsync(string userId);
}
