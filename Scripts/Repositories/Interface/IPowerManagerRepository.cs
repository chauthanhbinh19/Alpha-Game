using System.Collections.Generic;

public interface IPowerManagerRepository
{
    void InsertUserStats(string user_id, PowerManager powerManager);
    void UpdateUserStats(string user_id, PowerManager powerManager);
    PowerManager GetUserStats(string user_id);
}