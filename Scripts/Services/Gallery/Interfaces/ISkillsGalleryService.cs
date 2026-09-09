using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISkillsGalleryService
{
    Task<List<Skills>> GetSkillsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSkillsCountAsync(string search, string type, string rare);
    Task<InsertOrUpdateResult<bool>> InsertSkillGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateStatusSkillGalleryAsync(string userId, string Id);
    Task<InsertOrUpdateResult<bool>> UpdateBatchStatusSkillsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> UpdateTempStarSkillGalleryAsync(string userId, string Id, double star);
    Task<InsertOrUpdateResult<bool>> UpdateCurrentStarSkillGalleryAsync(string userId, string skillId);
    Task<InsertOrUpdateResult<bool>> UpdateBatchCurrentStarSkillsGalleryAsync(string userId);
    Task<InsertOrUpdateResult<bool>> InsertBatchSkillsGalleryAsync(string userId, List<Skills> skills);
    Task<Skills> GetSkillCollectionByIdAsync(string userId, string objectId);
    Task<InsertOrUpdateResult<bool>> UpdateSkillGalleryPowerAsync(string userId, string Id);
    Task<Skills> SumPowerSkillsGalleryAsync(string userId);
}