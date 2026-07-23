using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRobotsGalleryService
{
    Task<List<Robots>> GetRobotsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetRobotsCountAsync(string search, string rare);
    Task InsertRobotGalleryAsync(string userId, string Id);
    Task UpdateStatusRobotGalleryAsync(string userId, string Id);
    Task UpdateStarRobotGalleryAsync(string userId, string id, double star);
    Task UpdateRobotGalleryPowerAsync(string userId, string id);
    Task<Robots> SumPowerRobotsGalleryAsync(string userId);
}