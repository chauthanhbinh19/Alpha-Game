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

    public async Task<Master> GetCardHeroMasterAsync(string id, string card_id)
    {
        return await _userCardHeroesMasterRepository.GetCardHeroMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardHeroMasterAsync(Master master, string card_id)
    {
        await _userCardHeroesMasterRepository.InsertOrUpdateCardHeroMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumCardHeroesMasterAsync(string user_id, string card_id)
    {
        return await _userCardHeroesMasterRepository.GetSumCardHeroesMasterAsync(user_id, card_id);
    }
}
