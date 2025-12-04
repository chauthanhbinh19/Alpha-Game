using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRobotsGalleryService
{
    Task<List<Robots>> GetRobotsCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetRobotsCountAsync(string rare);
    Task InsertRobotGalleryAsync(string Id);
    Task UpdateStatusRobotGalleryAsync(string Id);
    Task UpdateStarRobotGalleryAsync(string id, double star);
    Task UpdateRobotGalleryPowerAsync(string id);
    Task<Robots> SumPowerRobotsGalleryAsync();
}