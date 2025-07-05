using System.Collections.Generic;

public class DailyCheckinService : IDailyCheckinService
{
    private readonly IDailyCheckinRepository _dailyCheckinRepository;

    public DailyCheckinService(IDailyCheckinRepository dailyCheckinRepository)
    {
        _dailyCheckinRepository = dailyCheckinRepository;
    }
    public static DailyCheckinService Create()
    {
        return new DailyCheckinService(new DailyCheckinRepository());
    }

    public void DeleteDailyCheckin(string dailyCheckinId)
    {
        _dailyCheckinRepository.DeleteDailyCheckin(dailyCheckinId);
    }

    public void InsertDailyCheckin(DailyCheckin dailyCheckin)
    {
        _dailyCheckinRepository.InsertDailyCheckin(dailyCheckin);
    }
}