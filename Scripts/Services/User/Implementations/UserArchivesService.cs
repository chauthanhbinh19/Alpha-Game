using System.Collections.Generic;
using System.Threading.Tasks;
public class UserArchivesService : IUserArchivesService
{
    private static UserArchivesService _instance;
    private readonly IUserArchivesRepository _userArchivesRepository;

    public UserArchivesService(IUserArchivesRepository userArchivesRepository)
    {
        _userArchivesRepository = userArchivesRepository;
    }

    public static UserArchivesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserArchivesService(new UserArchivesRepository());
        }
        return _instance;
    }

    public async Task<UserArchives> GetUserArchivesAsync(string id)
    {
        return await _userArchivesRepository.GetUserArchivesAsync(id);
    }

    public async Task<UserArchives> GetSumUserArchivesAsync(string user_id)
    {
        return await _userArchivesRepository.GetSumUserArchivesAsync(user_id);
    }

    public async Task InsertOrUpdateUserArchivesAsync(string userId, UserArchives Archives, string id)
    {
        await _userArchivesRepository.InsertOrUpdateUserArchivesAsync(userId, Archives, id);
    }
}