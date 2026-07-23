using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPuppetsGalleryService
{
    Task<List<Puppets>> GetPuppetsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetPuppetsCountAsync(string search, string type, string rare);
    Task InsertPuppetGalleryAsync(string userId, string Id);
    Task UpdateStatusPuppetGalleryAsync(string userId, string Id);
    Task UpdateStarPuppetGalleryAsync(string userId, string Id, double star);
    Task UpdatePuppetGalleryPowerAsync(string userId, string Id);
    Task<Puppets> SumPowerPuppetsGalleryAsync(string userId);
}