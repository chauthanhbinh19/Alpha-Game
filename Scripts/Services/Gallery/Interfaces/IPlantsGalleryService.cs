using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPlantsGalleryService
{
    Task<List<Plants>> GetPlantsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetPlantsCountAsync(string search, string rare);
    Task InsertPlantGalleryAsync(string userId, string Id);
    Task UpdateStatusPlantGalleryAsync(string userId, string Id);
    Task UpdateStarPlantGalleryAsync(string userId, string id, double star);
    Task UpdatePlantGalleryPowerAsync(string userId, string id);
    Task<Plants> SumPowerPlantsGalleryAsync(string userId);
}