using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRobotsGalleryService
{
    Task<List<Robots>> GetRobotsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetRobotsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<bool>> InsertRobotGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusRobotGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusRobotsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarRobotGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarRobotGalleryAsync(string userId, string robotId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarRobotsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchRobotsGalleryAsync(string userId, List<Robots> robots);
    Task<Robots> GetRobotCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateRobotGalleryPowerAsync(string userId, string id);
    Task<Robots> SumPowerRobotsGalleryAsync(string userId);
}