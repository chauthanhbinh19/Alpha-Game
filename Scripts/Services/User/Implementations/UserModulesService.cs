using System.Collections.Generic;
using System.Threading.Tasks;
public class UserModulesService : IUserModulesService
{
    private static UserModulesService _instance;
    private readonly IUserModulesRepository _userModulesRepository;

    public UserModulesService(IUserModulesRepository userModulesRepository)
    {
        _userModulesRepository = userModulesRepository;
    }

    public static UserModulesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserModulesService(new UserModulesRepository());
        }
        return _instance;
    }

    public async Task<UserModules> GetUserModulesAsync(string id)
    {
        return await _userModulesRepository.GetUserModulesAsync(id);
    }

    public async Task<UserModules> GetSumUserModulesAsync(string user_id)
    {
        return await _userModulesRepository.GetSumUserModulesAsync(user_id);
    }

    public async Task InsertOrUpdateUserModulesAsync(string userId, UserModules Modules, string id)
    {
        await _userModulesRepository.InsertOrUpdateUserModulesAsync(userId, Modules, id);
    }

}