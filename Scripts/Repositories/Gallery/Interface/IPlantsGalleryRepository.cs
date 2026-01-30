using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPlantsGalleryRepository
{
    Task<List<Plants>> GetPlantsCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetPlantsCountAsync(string search, string rare);
    Task InsertPlantGalleryAsync(string Id, Plants PlantFromDB);
    Task UpdateStatusPlantGalleryAsync(string Id);
    Task UpdateStarPlantGalleryAsync(string id, double star);
    Task UpdatePlantGalleryPowerAsync(string id, Plants PlantFromDB);
    Task<Plants> SumPowerPlantsGalleryAsync();
}