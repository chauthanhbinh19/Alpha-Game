using System.Collections.Generic;
using System.Threading.Tasks;

public class DailyCheckinService : IDailyCheckinService
{
    private readonly IDailyCheckinRepository _dailyCheckinRepository;

    public DailyCheckinService(IDailyCheckinRepository dailyCheckinRepository)
    {
        _dailyCheckinRepository = dailyCheckinRepository;
    }
    public static IDailyCheckinService Create() => ServiceContainer.GetService<IDailyCheckinService>();

    public async Task DeleteDailyCheckinAsync(string dailyCheckinId)
    {
        await _dailyCheckinRepository.DeleteDailyCheckinAsync(dailyCheckinId);
    }

    public async Task InsertDailyCheckinAsync(DailyCheckin dailyCheckin)
    {
        await _dailyCheckinRepository.InsertDailyCheckinAsync(dailyCheckin);
    }
}