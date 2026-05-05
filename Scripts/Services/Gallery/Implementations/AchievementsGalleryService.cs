using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AchievementsGalleryService : IAchievementsGalleryService
{
    private static AchievementsGalleryService _instance;
    private IAchievementsGalleryRepository _achievementsGalleryRepository;

    public AchievementsGalleryService(IAchievementsGalleryRepository achievementsGalleryRepository)
    {
        _achievementsGalleryRepository = achievementsGalleryRepository;
    }

    public static AchievementsGalleryService Create()
    {
        if (_instance == null)
        {
            _instance = new AchievementsGalleryService(new AchievementsGalleryRepository());
        }
        return _instance;
    }

    public async Task<List<Achievements>> GetAchievementsCollectionAsync(string search, int pageSize, int offset, string rare)
    {
        List<Achievements> list = await _achievementsGalleryRepository.GetAchievementsCollectionAsync(search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAchievementsCountAsync(string search, string rare)
    {
        return await _achievementsGalleryRepository.GetAchievementsCountAsync(search, rare);
    }

    public async Task InsertAchievementsGalleryAsync(string Id, Achievements AchievementFromDB)
    {
        await _achievementsGalleryRepository.InsertAchievementsGalleryAsync(Id, AchievementFromDB);
    }

    public async Task<Achievements> SumPowerAchievementsGalleryAsync()
    {
        return await _achievementsGalleryRepository.SumPowerAchievementsGalleryAsync();
    }

    public async Task UpdateAchievementsGalleryPowerAsync(string Id, Achievements AchievementFromDB)
    {
        await _achievementsGalleryRepository.UpdateAchievementsGalleryPowerAsync(Id, AchievementFromDB);
    }

    public async Task UpdateStarAchievementsGalleryAsync(string Id, double star)
    {
        await _achievementsGalleryRepository.UpdateStarAchievementsGalleryAsync(Id, star);
    }

    public async Task UpdateStatusAchievementsGalleryAsync(string Id)
    {
        await _achievementsGalleryRepository.UpdateStatusAchievementsGalleryAsync(Id);
    }
}