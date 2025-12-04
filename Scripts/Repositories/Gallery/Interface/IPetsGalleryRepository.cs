using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPetsGalleryRepository
{
    Task<List<Pets>> GetPetsCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetPetsCountAsync(string type, string rare);
    Task InsertPetGalleryAsync(string Id, Pets PetFromDB);
    Task UpdateStatusPetGalleryAsync(string Id);
    Task UpdateStarPetGalleryAsync(string Id, double star);
    Task UpdatePetGalleryPowerAsync(string Id, Pets PetFromDB);
    Task<Pets> SumPowerPetsGalleryAsync();
}