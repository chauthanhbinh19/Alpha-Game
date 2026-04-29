using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPetsGalleryService
{
    Task<List<Pets>> GetPetsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetPetsCountAsync(string search, string type, string rare);
    Task InsertPetGalleryAsync(string Id);
    Task UpdateStatusPetGalleryAsync(string Id);
    Task UpdateStarPetGalleryAsync(string Id, double star);
    Task UpdatePetGalleryPowerAsync(string Id);
    Task<Pets> SumPowerPetsGalleryAsync();
}