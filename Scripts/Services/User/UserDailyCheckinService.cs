using System.Collections.Generic;

public class UserDailyCheckinService : IUserDailyCheckinService
{
    private readonly IUserDailyCheckinRepository _userDailyCheckinRepository;

    public UserDailyCheckinService(IUserDailyCheckinRepository userDailyCheckinRepository)
    {
        _userDailyCheckinRepository = userDailyCheckinRepository;
    }
    public static UserDailyCheckinService Create()
    {
        return new UserDailyCheckinService(new UserDailyCheckinRepository());
    }

    public bool CheckUserDailyCheckinStatus(string userId, int month, int year)
    {
        return _userDailyCheckinRepository.CheckUserDailyCheckinStatus(userId, month, year);
    }

    public void DeleteUserDailyCheckin(string userId, string dailyCheckinId)
    {
        _userDailyCheckinRepository.DeleteUserDailyCheckin(userId, dailyCheckinId);
    }

    public List<UserDailyCheckin> GetUserDailyCheckin(string userId)
    {
        return _userDailyCheckinRepository.GetUserDailyCheckin(userId);
    }

    public void InsertUserDailyCheckin(string userId, UserDailyCheckin userDailyCheckin)
    {
        _userDailyCheckinRepository.InsertUserDailyCheckin(userId, userDailyCheckin);
    }
}