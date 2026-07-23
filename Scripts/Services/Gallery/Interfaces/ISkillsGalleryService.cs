using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISkillsGalleryService
{
    Task<List<Skills>> GetSkillsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSkillsCountAsync(string search, string type, string rare);
    Task InsertSkillGalleryAsync(string userId, string Id);
    Task UpdateStatusSkillGalleryAsync(string userId, string Id);
    Task UpdateStarSkillGalleryAsync(string userId, string Id, double star);
    Task UpdateSkillGalleryPowerAsync(string userId, string Id);
    Task<Skills> SumPowerSkillsGalleryAsync(string userId);
}