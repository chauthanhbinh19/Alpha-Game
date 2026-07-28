using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AchievementsGalleryService : IAchievementsGalleryService
{
    private static AchievementsGalleryService _instance;
    private readonly IAchievementsGalleryRepository _achievementsGalleryRepository;

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

    public async Task<List<Achievements>> GetAchievementsCollectionAsync(string userId, string search, int pageSize, int offset, string rare)
    {
        List<Achievements> list = await _achievementsGalleryRepository.GetAchievementsCollectionAsync(userId, search, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAchievementsCountAsync(string search, string rare)
    {
        return await _achievementsGalleryRepository.GetAchievementsCountAsync(search, rare);
    }

    public async Task InsertAchievementGalleryAsync(string userId, string Id)
    {
        IAchievementsRepository _repository = new AchievementsRepository();
        AchievementsService _service = new AchievementsService(_repository);
        await _achievementsGalleryRepository.InsertAchievementGalleryAsync(userId, Id, await _service.GetAchievementByIdAsync(Id));
    }

    public async Task<Achievements> SumPowerAchievementsGalleryAsync(string userId)
    {
        return await _achievementsGalleryRepository.SumPowerAchievementsGalleryAsync(userId);
    }

    public async Task UpdateAchievementGalleryPowerAsync(string userId, string Id, Achievements AchievementFromDB)
    {
        await _achievementsGalleryRepository.UpdateAchievementGalleryPowerAsync(userId, Id, AchievementFromDB);
    }

    public async Task UpdateStarAchievementGalleryAsync(string userId, string Id, double star)
    {
        await _achievementsGalleryRepository.UpdateStarAchievementGalleryAsync(userId, Id, star);
    }

    public async Task UpdateStatusAchievementGalleryAsync(string userId, string Id)
    {
        await _achievementsGalleryRepository.UpdateStatusAchievementGalleryAsync(userId, Id);
    }
}