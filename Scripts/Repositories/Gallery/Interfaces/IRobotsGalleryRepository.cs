using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRobotsGalleryRepository
{
    Task<List<Robots>> GetRobotsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetRobotsCountAsync(string search, string rare);
    Task InsertRobotGalleryAsync(string userId, string Id, Robots RobotFromDB);
    Task UpdateStatusRobotGalleryAsync(string userId, string Id);
    Task UpdateStarRobotGalleryAsync(string userId, string id, double star);
    Task UpdateRobotGalleryPowerAsync(string userId, string id, Robots RobotFromDB);
    Task<Robots> SumPowerRobotsGalleryAsync(string userId);
}