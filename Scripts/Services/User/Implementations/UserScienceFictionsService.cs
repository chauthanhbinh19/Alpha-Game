using System.Collections.Generic;
using System.Threading.Tasks;
public class UserScienceFictionsService : IUserScienceFictionsService
{
    private readonly IUserScienceFictionsRepository _scienceFictionsRepository;

    public UserScienceFictionsService(IUserScienceFictionsRepository scienceFictionsRepository)
    {
        _scienceFictionsRepository = scienceFictionsRepository;
    }

    public static IUserScienceFictionsService Create() => ServiceContainer.GetService<IUserScienceFictionsService>();

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