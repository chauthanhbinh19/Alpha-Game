using System.Threading.Tasks;

public class UserCardHeroesMasterService : IUserCardHeroesMasterService
{
    private readonly IUserCardHeroesMasterRepository _userCardHeroesMasterRepository;

    public UserCardHeroesMasterService(IUserCardHeroesMasterRepository userCardHeroesMasterRepository)
    {
        _userCardHeroesMasterRepository = userCardHeroesMasterRepository;
    }

    public static IUserCardHeroesMasterService Create() => ServiceContainer.GetService<IUserCardHeroesMasterService>();

    public async Task<UserMasters> GetUserCardHeroMasterAsync(string userId, string id, string cardId)
    {
        return await _userCardHeroesMasterRepository.GetUserCardHeroMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardHeroMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userCardHeroesMasterRepository.InsertOrUpdateUserCardHeroMasterAsync(userId, userMaster, cardId);
    }

    public async Task<UserMasters> GetSumUserCardHeroesMasterAsync(string userId, string cardId)
    {
        return await _userCardHeroesMasterRepository.GetSumUserCardHeroesMasterAsync(userId, cardId);
    }
}
