using System.Collections.Generic;
using System.Threading.Tasks;
public class UserAnimesService : IUserAnimesService
{
    private static UserAnimesService _instance;
    private readonly IUserAnimesRepository _userAnimesRepository;

    public UserAnimesService(IUserAnimesRepository userAnimesRepository)
    {
        _userAnimesRepository = userAnimesRepository;
    }

    public static UserAnimesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserAnimesService(new UserAnimesRepository());
        }
        return _instance;
    }

    public async Task<UserAnimes> GetUserAnimesAsync(string id)
    {
        return await _userAnimesRepository.GetUserAnimesAsync(id);
    }

    public async Task<UserAnimes> GetSumUserAnimesAsync(string user_id)
    {
        return await _userAnimesRepository.GetSumUserAnimesAsync(user_id);
    }

    public async Task InsertOrUpdateUserAnimesAsync(string userId, UserAnimes Animes, string id)
    {
        await _userAnimesRepository.InsertOrUpdateUserAnimesAsync(userId, Animes, id);
    }

}