using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITechnologiesGalleryService
{
    Task<List<Technologies>> GetTechnologiesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetTechnologiesCountAsync(string search, string rare);
    Task InsertTechnologyGalleryAsync(string userId, string Id);
    Task UpdateStatusTechnologyGalleryAsync(string userId, string Id);
    Task UpdateStarTechnologyGalleryAsync(string userId, string id, double star);
    Task UpdateTechnologyGalleryPowerAsync(string userId, string id);
    Task<Technologies> SumPowerTechnologiesGalleryAsync(string userId);
}