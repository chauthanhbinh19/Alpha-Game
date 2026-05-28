using System.Collections.Generic;
using System.Threading.Tasks;

public interface IDailyCheckinRepository
{
    Task InsertDailyCheckinAsync(DailyCheckin dailyCheckin);
    Task DeleteDailyCheckinAsync(string dailyCheckinId);
}
