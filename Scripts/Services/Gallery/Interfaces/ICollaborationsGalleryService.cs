using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICollaborationsGalleryService
{
    Task<List<Collaborations>> GetCollaborationsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetCollaborationsCountAsync(string search, string rare);
    Task InsertCollaborationGalleryAsync(string userId, string Id);
    Task UpdateStatusCollaborationGalleryAsync(string userId, string Id);
    Task UpdateStarCollaborationGalleryAsync(string userId, string id, double star);
    Task UpdateCollaborationGalleryPowerAsync(string userId, string id);
    Task<Collaborations> SumPowerCollaborationsGalleryAsync(string userId);
}