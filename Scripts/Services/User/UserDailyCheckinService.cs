using System.Collections.Generic;
using System.Threading.Tasks;

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

    public async Task<bool> CheckUserDailyCheckinStatusAsync(string userId, int month, int year)
    {
        return await _userDailyCheckinRepository.CheckUserDailyCheckinStatusAsync(userId, month, year);
    }

    public async Task DeleteUserDailyCheckinAsync(string userId, string dailyCheckinId)
    {
        await _userDailyCheckinRepository.DeleteUserDailyCheckinAsync(userId, dailyCheckinId);
    }

    public async Task<List<UserDailyCheckin>> GetUserDailyCheckinAsync(string userId)
    {
        return await _userDailyCheckinRepository.GetUserDailyCheckinAsync(userId);
    }

    public async Task InsertUserDailyCheckinAsync(string userId, UserDailyCheckin userDailyCheckin)
    {
        await _userDailyCheckinRepository.InsertUserDailyCheckinAsync(userId, userDailyCheckin);
    }
}