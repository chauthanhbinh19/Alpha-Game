using System.Collections.Generic;

public interface IDailyCheckinService
{
    void InsertDailyCheckin(DailyCheckin dailyCheckin);
    void DeleteDailyCheckin(string dailyCheckinId);
}
