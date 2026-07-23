using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISkillsGalleryRepository
{
    Task<List<Skills>> GetSkillsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSkillsCountAsync(string search, string type, string rare);
    Task InsertSkillGalleryAsync(string userId, string Id, Skills SkillFromDB);
    Task UpdateStatusSkillGalleryAsync(string userId, string Id);
    Task UpdateStarSkillGalleryAsync(string userId, string Id, double star);
    Task UpdateSkillGalleryPowerAsync(string userId, string Id, Skills SkillFromDB);
    Task<Skills> SumPowerSkillsGalleryAsync(string userId);
}