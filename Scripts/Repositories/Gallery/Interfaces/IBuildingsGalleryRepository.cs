using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBuildingsGalleryRepository
{
    Task<List<Buildings>> GetBuildingsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetBuildingsCountAsync(string search, string type, string rare);
    Task InsertBuildingGalleryAsync(string userId, string Id, Buildings BuildingFromDB);
    Task UpdateStatusBuildingGalleryAsync(string userId, string Id);
    Task UpdateStarBuildingGalleryAsync(string userId, string Id, double star);
    Task UpdateBuildingGalleryPowerAsync(string userId, string Id, Buildings BuildingFromDB);
    Task<Buildings> SumPowerBuildingsGalleryAsync(string userId);
}