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

    public async Task<UserScienceFictions> GetUserScienceFictionsAsync(string userId, string id)
    {
        return await _scienceFictionsRepository.GetUserScienceFictionsAsync(userId, id);
    }

    public async Task<UserScienceFictions> GetSumUserScienceFictionsAsync(string userId)
    {
        return await _scienceFictionsRepository.GetSumUserScienceFictionsAsync(userId);
    }

    public async Task InsertOrUpdateUserScienceFictionsAsync(string userId, UserScienceFictions scienceFiction, string id)
    {
        await _scienceFictionsRepository.InsertOrUpdateUserScienceFictionsAsync(userId, scienceFiction, id);
    }
}