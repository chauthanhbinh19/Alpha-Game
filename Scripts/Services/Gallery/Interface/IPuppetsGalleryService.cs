using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPuppetsGalleryService
{
    Task<List<Puppets>> GetPuppetsCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetPuppetsCountAsync(string type, string rare);
    Task InsertPuppetGalleryAsync(string Id);
    Task UpdateStatusPuppetGalleryAsync(string Id);
    Task UpdateStarPuppetGalleryAsync(string Id, double star);
    Task UpdatePuppetGalleryPowerAsync(string Id);
    Task<Puppets> SumPowerPuppetsGalleryAsync();
}