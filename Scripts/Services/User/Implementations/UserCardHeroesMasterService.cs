using System.Threading.Tasks;

public class UserCardHeroesMasterService : IUserCardHeroesMasterService
{
     private static UserCardHeroesMasterService _instance;
    private readonly IUserCardHeroesMasterRepository _userCardHeroesMasterRepository;

    public UserCardHeroesMasterService(IUserCardHeroesMasterRepository userCardHeroesMasterRepository)
    {
        _userCardHeroesMasterRepository = userCardHeroesMasterRepository;
    }

    public static UserCardHeroesMasterService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardHeroesMasterService(new UserCardHeroesMasterRepository());
        }
        return _instance;
    }

    public async Task<Master> GetUserCardHeroMasterAsync(string userId, string id, string card_id)
    {
        return await _userCardHeroesMasterRepository.GetUserCardHeroMasterAsync(userId, id, card_id);
    }

    public async Task InsertOrUpdateUserCardHeroMasterAsync(string userId, UserMasters userMaster, string card_id)
    {
        await _userCardHeroesMasterRepository.InsertOrUpdateUserCardHeroMasterAsync(userId, userMaster, card_id);
    }

    public async Task<Master> GetSumUserCardHeroesMasterAsync(string userId, string card_id)
    {
        return await _userCardHeroesMasterRepository.GetSumUserCardHeroesMasterAsync(userId, card_id);
    }
}
