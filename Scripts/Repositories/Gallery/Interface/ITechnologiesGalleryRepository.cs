using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITechnologiesGalleryRepository
{
    Task<List<Technologies>> GetTechnologiesCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetTechnologiesCountAsync(string search, string rare);
    Task InsertTechnologyGalleryAsync(string Id, Technologies TechnologyFromDB);
    Task UpdateStatusTechnologyGalleryAsync(string Id);
    Task UpdateStarTechnologyGalleryAsync(string id, double star);
    Task UpdateTechnologyGalleryPowerAsync(string id, Technologies TechnologyFromDB);
    Task<Technologies> SumPowerTechnologiesGalleryAsync();
}