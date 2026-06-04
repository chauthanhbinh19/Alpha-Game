using System.Collections.Generic;
using System.Threading.Tasks;
public class UserScienceFictionsService : IUserScienceFictionsService
{
    private static UserScienceFictionsService _instance;
    private readonly IUserScienceFictionsRepository _scienceFictionsRepository;

    public UserScienceFictionsService(IUserScienceFictionsRepository scienceFictionsRepository)
    {
        _scienceFictionsRepository = scienceFictionsRepository;
    }

    public static UserScienceFictionsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserScienceFictionsService(new UserScienceFictionsRepository());
        }
        return _instance;
    }

    public async Task<UserScienceFictions> GetUserScienceFictionsAsync(string id)
    {
        return await _scienceFictionsRepository.GetScienceFictionsAsync(id);
    }

    public async Task<UserScienceFictions> GetSumUserScienceFictionsAsync(string user_id)
    {
        return await _scienceFictionsRepository.GetSumScienceFictionsAsync(user_id);
    }

    public async Task InsertOrUpdateUserScienceFictionsAsync(string userId, UserScienceFictions scienceFiction, string id)
    {
        await _scienceFictionsRepository.InsertOrUpdateScienceFictionsAsync(userId, scienceFiction, id);
    }
}