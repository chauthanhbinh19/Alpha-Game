using System.Collections.Generic;

public interface IDailyCheckinRepository
{
    void InsertDailyCheckin(DailyCheckin dailyCheckin);
    void DeleteDailyCheckin(string dailyCheckinId);
}
