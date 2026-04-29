using System.Collections.Generic;
using System.Threading.Tasks;

public interface IBuildingsGalleryRepository
{
    Task<List<Buildings>> GetBuildingsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetBuildingsCountAsync(string search, string type, string rare);
    Task InsertBuildingGalleryAsync(string Id, Buildings BuildingFromDB);
    Task UpdateStatusBuildingGalleryAsync(string Id);
    Task UpdateStarBuildingGalleryAsync(string Id, double star);
    Task UpdateBuildingGalleryPowerAsync(string Id, Buildings BuildingFromDB);
    Task<Buildings> SumPowerBuildingsGalleryAsync();
}