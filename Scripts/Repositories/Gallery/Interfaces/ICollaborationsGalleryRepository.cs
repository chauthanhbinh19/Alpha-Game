using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICollaborationsGalleryRepository
{
    Task<List<Collaborations>> GetCollaborationsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetCollaborationsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Collaborations>> InsertCollaborationGalleryAsync(string userId, string Id, Collaborations CollaborationFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusCollaborationGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusCollaborationsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarCollaborationGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarCollaborationGalleryAsync(string userId, string collaborationId);
    Task<InsertOrUpdateResult<List<(string CollaborationId, double CurrentStar)>>> UpdateBatchCurrentStarCollaborationsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Collaborations>>> InsertBatchCollaborationsGalleryAsync(string userId, List<Collaborations> collaborations);
    Task<Collaborations> GetCollaborationCollectionByIdAsync(string userId, string objectId);
    Task UpdateCollaborationGalleryPowerAsync(string userId, string id, Collaborations CollaborationFromDB);
    Task<Collaborations> SumPowerCollaborationsGalleryAsync(string userId);
}