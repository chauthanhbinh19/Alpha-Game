using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITechnologiesGalleryRepository
{
    Task<List<Technologies>> GetTechnologiesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetTechnologiesCountAsync(string search, string rare);
    Task InsertTechnologyGalleryAsync(string userId, string Id, Technologies TechnologyFromDB);
    Task UpdateStatusTechnologyGalleryAsync(string userId, string Id);
    Task UpdateStarTechnologyGalleryAsync(string userId, string id, double star);
    Task UpdateTechnologyGalleryPowerAsync(string userId, string id, Technologies TechnologyFromDB);
    Task<Technologies> SumPowerTechnologiesGalleryAsync(string userId);
}