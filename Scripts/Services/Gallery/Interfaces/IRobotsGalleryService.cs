using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRobotsGalleryService
{
    Task<List<Robots>> GetRobotsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetRobotsCountAsync(string search, string rare);
    Task<bool> InsertRobotGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusRobotGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusRobotsGalleryAsync(string userId);
    Task<bool> UpdateTempStarRobotGalleryAsync(string userId, string id, double star);
    Task<bool> UpdateCurrentStarRobotGalleryAsync(string userId, string robotId);
    Task<bool> UpdateBatchCurrentStarRobotsGalleryAsync(string userId);
    Task<bool> InsertBatchRobotsGalleryAsync(string userId, List<Robots> robots);
    Task<Robots> GetRobotCollectionByIdAsync(string userId, string objectId);
    Task UpdateRobotGalleryPowerAsync(string userId, string id);
    Task<Robots> SumPowerRobotsGalleryAsync(string userId);
}