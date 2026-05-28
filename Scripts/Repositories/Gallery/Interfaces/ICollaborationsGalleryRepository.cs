using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICollaborationsGalleryRepository
{
    Task<List<Collaborations>> GetCollaborationsCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetCollaborationsCountAsync(string search, string rare);
    Task InsertCollaborationGalleryAsync(string Id, Collaborations CollaborationFromDB);
    Task UpdateStatusCollaborationGalleryAsync(string Id);
    Task UpdateStarCollaborationGalleryAsync(string id, double star);
    Task UpdateCollaborationGalleryPowerAsync(string id, Collaborations CollaborationFromDB);
    Task<Collaborations> SumPowerCollaborationsGalleryAsync();
}