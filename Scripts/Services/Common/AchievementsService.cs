using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AchievementsService : IAchievementsService
{
    private static AchievementsService _instance;
    private IAchievementsRepository _achievementsRepository;

    public AchievementsService(IAchievementsRepository achievementsRepository)
    {
        _achievementsRepository = achievementsRepository;
    }

    public static AchievementsService Create()
    {
        if (_instance == null)
        {
            _instance = new AchievementsService(new AchievementsRepository());
        }
        return _instance;
    }

    public async Task<List<Achievements>> GetAchievementsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Achievements> list = await _achievementsRepository.GetAchievementsAsync(search, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAchievementsCountAsync(string search, string rare)
    {
        return await _achievementsRepository.GetAchievementsCountAsync(search, rare);
    }

    public async Task<Achievements> GetAchievementByIdAsync(string Id)
    {
        return await _achievementsRepository.GetAchievementByIdAsync(Id);
    }

    public async Task<List<Achievements>> GetAchievementsWithPriceAsync(int pageSize, int offset)
    {
        List<Achievements> list = await _achievementsRepository.GetAchievementsWithPriceAsync(pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAchievementsWithPriceCountAsync()
    {
        return await _achievementsRepository.GetAchievementsWithPriceCountAsync();
    }
    
    public async Task<Achievements> SumPowerAchievementsPercentAsync()
    {
        return await _achievementsRepository.SumPowerAchievementsPercentAsync();
    }
    
}