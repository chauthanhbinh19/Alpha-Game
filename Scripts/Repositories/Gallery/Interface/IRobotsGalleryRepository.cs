using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRobotsGalleryRepository
{
    Task<List<Robots>> GetRobotsCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetRobotsCountAsync(string rare);
    Task InsertRobotGalleryAsync(string Id, Robots RobotFromDB);
    Task UpdateStatusRobotGalleryAsync(string Id);
    Task UpdateStarRobotGalleryAsync(string id, double star);
    Task UpdateRobotGalleryPowerAsync(string id, Robots RobotFromDB);
    Task<Robots> SumPowerRobotsGalleryAsync();
}