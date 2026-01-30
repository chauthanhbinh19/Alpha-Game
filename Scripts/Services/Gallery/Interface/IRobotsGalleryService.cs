using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRobotsGalleryService
{
    Task<List<Robots>> GetRobotsCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetRobotsCountAsync(string search, string rare);
    Task InsertRobotGalleryAsync(string Id);
    Task UpdateStatusRobotGalleryAsync(string Id);
    Task UpdateStarRobotGalleryAsync(string id, double star);
    Task UpdateRobotGalleryPowerAsync(string id);
    Task<Robots> SumPowerRobotsGalleryAsync();
}