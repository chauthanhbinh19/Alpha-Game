using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPetsGalleryService
{
    Task<List<Pets>> GetPetsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetPetsCountAsync(string search, string type, string rare);
    Task InsertPetGalleryAsync(string userId, string Id);
    Task UpdateStatusPetGalleryAsync(string userId, string Id);
    Task UpdateStarPetGalleryAsync(string userId, string Id, double star);
    Task UpdatePetGalleryPowerAsync(string userId, string Id);
    Task<Pets> SumPowerPetsGalleryAsync(string userId);
}