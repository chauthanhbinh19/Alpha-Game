using System.Collections.Generic;
using System.Threading.Tasks;
public interface IAchievementsGalleryService
{
    Task<List<Achievements>> GetAchievementsCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetAchievementsCountAsync(string search, string rare);
    Task InsertAchievementsGalleryAsync(string Id, Achievements AchievementFromDB);
    Task UpdateStatusAchievementsGalleryAsync(string Id);
    Task UpdateStarAchievementsGalleryAsync(string id, double star);
    Task UpdateAchievementsGalleryPowerAsync(string id, Achievements AchievementFromDB);
    Task<Achievements> SumPowerAchievementsGalleryAsync();
}