using System.Collections.Generic;
using System.Threading.Tasks;
public class UserUniversesService : IUserUniversesService
{
    private static UserUniversesService _instance;
    private readonly IUserUniversesRepository _userUniversesRepository;

    public UserUniversesService(IUserUniversesRepository userUniversesRepository)
    {
        _userUniversesRepository = userUniversesRepository;
    }

    public static UserUniversesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserUniversesService(new UserUniversesRepository());
        }
        return _instance;
    }

    public async Task<UserUniverses> GetUserUniversesAsync(string userId, string id)
    {
        return await _userUniversesRepository.GetUserUniversesAsync(userId, id);
    }

    public async Task<UserUniverses> GetSumUserUniversesAsync(string userId)
    {
        return await _userUniversesRepository.GetSumUserUniversesAsync(userId);
    }

    public async Task InsertOrUpdateUserUniversesAsync(string userId, UserUniverses Universes, string id)
    {
        await _userUniversesRepository.InsertOrUpdateUserUniversesAsync(userId, Universes, id);
    }

}