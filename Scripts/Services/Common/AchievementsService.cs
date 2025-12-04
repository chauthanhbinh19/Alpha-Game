using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AchievementsService : IAchievementsService
{
    private IAchievementsRepository _achievementsRepository;

    public AchievementsService(IAchievementsRepository achievementsRepository)
    {
        _achievementsRepository = achievementsRepository;
    }

    public static AchievementsService Create()
    {
        return new AchievementsService(new AchievementsRepository());
    }


    public async Task<List<Achievements>> GetAchievementsAsync(int pageSize, int offset, string rare)
    {
        List<Achievements> list = await _achievementsRepository.GetAchievementsAsync(pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetAchievementsCountAsync(string rare)
    {
        return await _achievementsRepository.GetAchievementsCountAsync(rare);
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