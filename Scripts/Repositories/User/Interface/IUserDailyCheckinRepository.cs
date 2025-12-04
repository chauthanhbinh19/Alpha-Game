using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserDailyCheckinRepository
{
    Task InsertUserDailyCheckinAsync(string userId, UserDailyCheckin userDailyCheckin);
    Task UpdateUserDailyCheckinAsync(string userId, string dailyCheckinId);
    Task DeleteUserDailyCheckinAsync(string userId, string dailyCheckinId);
    Task<bool> CheckUserDailyCheckinStatusAsync(string userId, int month, int year);
    Task<List<UserDailyCheckin>> GetUserDailyCheckinAsync(string userId);
}