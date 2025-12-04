using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISkillsGalleryRepository
{
    Task<List<Skills>> GetSkillsCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetSkillsCountAsync(string type, string rare);
    Task InsertSkillGalleryAsync(string Id, Skills SkillFromDB);
    Task UpdateStatusSkillGalleryAsync(string Id);
    Task UpdateStarSkillGalleryAsync(string Id, double star);
    Task UpdateSkillGalleryPowerAsync(string Id, Skills SkillFromDB);
    Task<Skills> SumPowerSkillsGalleryAsync();
}