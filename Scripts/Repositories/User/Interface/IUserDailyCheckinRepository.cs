using System.Collections.Generic;

public interface IUserDailyCheckinRepository
{
    void InsertUserDailyCheckin(string userId, UserDailyCheckin userDailyCheckin);
    void DeleteUserDailyCheckin(string userId, string dailyCheckinId);
    bool CheckUserDailyCheckinStatus(string userId, int month, int year);
    List<UserDailyCheckin> GetUserDailyCheckin(string userId);
}