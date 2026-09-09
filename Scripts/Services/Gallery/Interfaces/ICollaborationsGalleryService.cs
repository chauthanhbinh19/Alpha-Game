using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICollaborationsGalleryService
{
    Task<List<Collaborations>> GetCollaborationsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetCollaborationsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertCollaborationGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCollaborationGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCollaborationsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarCollaborationGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarCollaborationGalleryAsync(string userId, string collaborationId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarCollaborationsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchCollaborationsGalleryAsync(string userId, List<Collaborations> collaborations);
    Task<Collaborations> GetCollaborationCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateCollaborationGalleryPowerAsync(string userId, string id);
    Task<Collaborations> SumPowerCollaborationsGalleryAsync(string userId);
}