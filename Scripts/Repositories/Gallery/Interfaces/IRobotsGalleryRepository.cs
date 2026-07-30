using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRobotsGalleryRepository
{
    Task<List<Robots>> GetRobotsCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetRobotsCountAsync(string search, string rare);
    Task<InsertOrUpdateResult<Robots>> InsertRobotGalleryAsync(string userId, string Id, Robots RobotFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusRobotGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusRobotsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateStarRobotGalleryAsync(string userId, string id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarRobotGalleryAsync(string userId, string robotId);
    Task<InsertOrUpdateResult<List<(string RobotId, double CurrentStar)>>> UpdateBatchCurrentStarRobotsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Robots>>> InsertBatchRobotsGalleryAsync(string userId, List<Robots> robots);
    Task<Robots> GetRobotCollectionByIdAsync(string userId, string objectId);
    Task UpdateRobotGalleryPowerAsync(string userId, string id, Robots RobotFromDB);
    Task<Robots> SumPowerRobotsGalleryAsync(string userId);
}