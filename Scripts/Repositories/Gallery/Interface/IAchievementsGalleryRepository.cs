using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAchievementsGalleryRepository
{
    Task<List<Achievements>> GetAchievementCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetAchievementsCountAsync(string rare);
    Task InsertAchievementsGalleryAsync(string Id, Achievements AchievementFromDB);
    Task UpdateStatusAchievementsGalleryAsync(string Id);
    Task UpdateStarAchievementsGalleryAsync(string id, double star);
    Task UpdateAchievementsGalleryPowerAsync(string id, Achievements AchievementFromDB);
    Task<Achievements> SumPowerAchievementsGalleryAsync();
}