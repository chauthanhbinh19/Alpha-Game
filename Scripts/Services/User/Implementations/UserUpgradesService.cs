using System.Collections.Generic;
using System.Threading.Tasks;
public class UserUpgradesService : IUserUpgradesService
{
    private static UserUpgradesService _instance;
    private readonly IUserUpgradesRepository _userUpgradesRepository;

    public UserUpgradesService(IUserUpgradesRepository userUpgradesRepository)
    {
        _userUpgradesRepository = userUpgradesRepository;
    }

    public static UserUpgradesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserUpgradesService(new UserUpgradesRepository());
        }
        return _instance;
    }

    public async Task<UserUpgrades> GetUserUpgradesAsync(string id)
    {
        return await _userUpgradesRepository.GetUserUpgradesAsync(id);
    }

    public async Task<UserUpgrades> GetSumUserUpgradesAsync(string user_id)
    {
        return await _userUpgradesRepository.GetSumUserUpgradesAsync(user_id);
    }

    public async Task InsertOrUpdateUserUpgradesAsync(string userId, UserUpgrades Upgrades, string id)
    {
        await _userUpgradesRepository.InsertOrUpdateUserUpgradesAsync(userId, Upgrades, id);
    }

}