using System.Collections.Generic;
using System.Threading.Tasks;
public class UserMastersService : IUserMastersService
{
    private static UserMastersService _instance;
    private readonly IUserMastersRepository _userMastersRepository;

    public UserMastersService(IUserMastersRepository userMastersRepository)
    {
        _userMastersRepository = userMastersRepository;
    }

    public static UserMastersService Create()
    {
        if (_instance == null)
        {
            _instance = new UserMastersService(new UserMastersRepository());
        }
        return _instance;
    }

    public async Task<UserMasters> GetUserMastersAsync(string id)
    {
        return await _userMastersRepository.GetUserMastersAsync(id);
    }

    public async Task<UserMasters> GetSumUserMastersAsync(string user_id)
    {
        return await _userMastersRepository.GetSumUserMastersAsync(user_id);
    }

    public async Task InsertOrUpdateUserMastersAsync(string userId, UserMasters Masters, string id)
    {
        await _userMastersRepository.InsertOrUpdateUserMastersAsync(userId, Masters, id);
    }

}