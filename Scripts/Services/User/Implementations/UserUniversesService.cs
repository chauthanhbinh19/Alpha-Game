using System.Collections.Generic;
using System.Threading.Tasks;
public class UserUniversesService : IUserUniversesService
{
    private readonly IUserUniversesRepository _userUniversesRepository;

    public UserUniversesService(IUserUniversesRepository userUniversesRepository)
    {
        _userUniversesRepository = userUniversesRepository;
    }

    public static IUserUniversesService Create() => ServiceContainer.GetService<IUserUniversesService>();

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