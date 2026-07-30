using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AchievementsService : IAchievementsService
{
    private readonly IAchievementsRepository _achievementsRepository;

    public AchievementsService(IAchievementsRepository achievementsRepository)
    {
        _achievementsRepository = achievementsRepository;
    }

    public static IAchievementsService Create() => ServiceContainer.GetService<IAchievementsService>();

    public async Task<List<Achievements>> GetAchievementsAsync(string search, string rare, int pageSize, int offset)
    {
        List<Achievements> list = await _achievementsRepository.GetAchievementsAsync(search, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
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
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAchievementsWithPriceCountAsync()
    {
        return await _achievementsRepository.GetAchievementsWithPriceCountAsync();
    }
    
    public async Task<Achievements> SumPowerAchievementsPercentAsync(string userId)
    {
        return await _achievementsRepository.SumPowerAchievementsPercentAsync(userId);
    }

    public async Task<List<Achievements>> GetAchievementsWithoutLimitAsync()
    {
        return await _achievementsRepository.GetAchievementsWithoutLimitAsync();
    }
}