using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISkillsGalleryService
{
    Task<List<Skills>> GetSkillsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSkillsCountAsync(string search, string type, string rare);
    Task<bool> InsertSkillGalleryAsync(string userId, string Id);
    Task<bool> UpdateStatusSkillGalleryAsync(string userId, string Id);
    Task<bool> UpdateBatchStatusSkillsGalleryAsync(string userId);
    Task<bool> UpdateStarSkillGalleryAsync(string userId, string Id, double star);
    Task<bool> UpdateCurrentStarSkillGalleryAsync(string userId, string skillId);
    Task<bool> UpdateBatchCurrentStarSkillsGalleryAsync(string userId);
    Task<bool> InsertBatchSkillsGalleryAsync(string userId, List<Skills> skills);
    Task<Skills> GetSkillCollectionByIdAsync(string userId, string objectId);
    Task UpdateSkillGalleryPowerAsync(string userId, string Id);
    Task<Skills> SumPowerSkillsGalleryAsync(string userId);
}