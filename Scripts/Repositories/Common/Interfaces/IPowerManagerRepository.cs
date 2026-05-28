using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPowerManagerRepository
{
    Task InsertUserStatsAsync(string user_id, PowerManager powerManager);
    Task UpdateUserStatsAsync(string user_id, PowerManager powerManager);
    Task<PowerManager> GetUserStatsAsync(string user_id);
}