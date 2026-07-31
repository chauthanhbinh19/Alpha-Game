using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICollaborationsGalleryService
{
    Task<List<Collaborations>> GetCollaborationsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetCollaborationsCountAsync(string search, string rare);
    Task<bool> InsertCollaborationGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusCollaborationGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusCollaborationsGalleryAsync(string userId);
    Task<bool> UpdateTempStarCollaborationGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarCollaborationGalleryAsync(string userId, string collaborationId);
    Task<bool> UpdateBatchCurrentStarCollaborationsGalleryAsync(string userId);
    Task<bool> InsertBatchCollaborationsGalleryAsync(string userId, List<Collaborations> collaborations);
    Task<Collaborations> GetCollaborationCollectionByIdAsync(string userId, string objectId);
    Task UpdateCollaborationGalleryPowerAsync(string userId, string id);
    Task<Collaborations> SumPowerCollaborationsGalleryAsync(string userId);
}