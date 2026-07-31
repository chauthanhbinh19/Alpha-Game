using System.Collections.Generic;
using System.Threading.Tasks;
public class UserAnimesService : IUserAnimesService
{
    private readonly IUserAnimesRepository _userAnimesRepository;

    public UserAnimesService(IUserAnimesRepository userAnimesRepository)
    {
        _userAnimesRepository = userAnimesRepository;
    }

    public static IUserAnimesService Create() => ServiceContainer.GetService<IUserAnimesService>();

    public async Task<UserAnimes> GetUserAnimesAsync(string userId, string id)
    {
        return await _userAnimesRepository.GetUserAnimesAsync(userId, id);
    }

    public async Task<UserAnimes> GetSumUserAnimesAsync(string userId)
    {
        return await _userAnimesRepository.GetSumUserAnimesAsync(userId);
    }

    public async Task InsertOrUpdateUserAnimesAsync(string userId, UserAnimes Animes, string id)
    {
        await _userAnimesRepository.InsertOrUpdateUserAnimesAsync(userId, Animes, id);
    }

}