using System.Collections.Generic;
using System.Threading.Tasks;
public class UserPowersService : IUserPowersService
{
    private readonly IUserPowersRepository _userPowersRepository;

    public UserPowersService(IUserPowersRepository userPowersRepository)
    {
        _userPowersRepository = userPowersRepository;
    }

    public static IUserPowersService Create() => ServiceContainer.GetService<IUserPowersService>();

    public async Task<UserPowers> GetSumUserPowersAsync(string userId)
    {
        return await _userPowersRepository.GetSumUserPowersAsync(userId);
    }

}