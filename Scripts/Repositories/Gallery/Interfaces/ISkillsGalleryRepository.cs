using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISkillsGalleryRepository
{
    Task<List<Skills>> GetSkillsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSkillsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<Skills>> InsertSkillGalleryAsync(string userId, string Id, Skills SkillFromDB);
    Task<InsertOrUpdateResult<bool>> UpdateStatusSkillGalleryAsync(string userId, string id, string status = "available");
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusSkillsGalleryAsync(string userId, string status = "available");
    Task<InsertOrUpdateResult<double>> UpdateTempStarSkillGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<double>> UpdateCurrentStarSkillGalleryAsync(string userId, string skillId);
    Task<InsertOrUpdateResult<List<(string SkillId, double CurrentStar)>>> UpdateBatchCurrentStarSkillsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<List<Skills>>> InsertBatchSkillsGalleryAsync(string userId, List<Skills> skills);
    Task<Skills> GetSkillCollectionByIdAsync(string userId, string objectId);
    Task UpdateSkillGalleryPowerAsync(string userId, string Id, Skills SkillFromDB);
    Task<Skills> SumPowerSkillsGalleryAsync(string userId);
}