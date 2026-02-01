using System.Collections.Generic;
using System.Threading.Tasks;

public class DailyCheckinService : IDailyCheckinService
{
    private static DailyCheckinService _instance;
    private readonly IDailyCheckinRepository _dailyCheckinRepository;

    public DailyCheckinService(IDailyCheckinRepository dailyCheckinRepository)
    {
        _dailyCheckinRepository = dailyCheckinRepository;
    }
    public static DailyCheckinService Create()
    {
        if (_instance == null)
        {
            _instance = new DailyCheckinService(new DailyCheckinRepository());
        }
        return _instance;
    }

    public async Task DeleteDailyCheckinAsync(string dailyCheckinId)
    {
        await _dailyCheckinRepository.DeleteDailyCheckinAsync(dailyCheckinId);
    }

    public async Task InsertDailyCheckinAsync(DailyCheckin dailyCheckin)
    {
        await _dailyCheckinRepository.InsertDailyCheckinAsync(dailyCheckin);
    }
}