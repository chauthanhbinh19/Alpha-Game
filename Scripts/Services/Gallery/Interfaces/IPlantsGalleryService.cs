using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPlantsGalleryService
{
    Task<List<Plants>> GetPlantsCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetPlantsCountAsync(string search, string rare);
    Task InsertPlantGalleryAsync(string Id);
    Task UpdateStatusPlantGalleryAsync(string Id);
    Task UpdateStarPlantGalleryAsync(string id, double star);
    Task UpdatePlantGalleryPowerAsync(string id);
    Task<Plants> SumPowerPlantsGalleryAsync();
}