using System.Collections.Generic;
using System.Threading.Tasks;

public interface ISkillsGalleryService
{
    Task<List<Skills>> GetSkillsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetSkillsCountAsync(string search, string type, string rare);
    Task InsertSkillGalleryAsync(string Id);
    Task UpdateStatusSkillGalleryAsync(string Id);
    Task UpdateStarSkillGalleryAsync(string Id, double star);
    Task UpdateSkillGalleryPowerAsync(string Id);
    Task<Skills> SumPowerSkillsGalleryAsync();
}