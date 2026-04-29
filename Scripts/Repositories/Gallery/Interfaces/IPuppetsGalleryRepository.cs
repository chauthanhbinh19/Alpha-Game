using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPuppetsGalleryRepository
{
    Task<List<Puppets>> GetPuppetsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetPuppetsCountAsync(string search, string type, string rare);
    Task InsertPuppetGalleryAsync(string Id, Puppets PuppetFromDB);
    Task UpdateStatusPuppetGalleryAsync(string Id);
    Task UpdateStarPuppetGalleryAsync(string Id, double star);
    Task UpdatePuppetGalleryPowerAsync(string Id, Puppets PuppetFromDB);
    Task<Puppets> SumPowerPuppetsGalleryAsync();
}