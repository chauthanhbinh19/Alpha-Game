using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPowerManagerService
{
    Task InsertUserStatsAsync(string userId);
    Task UpdateUserStatsAsync(string userId);
    Task UpdateUserStatsAsync(string userId, PowerManager powerManager);
    Task<PowerManager> GetUserStatsAsync(string userId);
}