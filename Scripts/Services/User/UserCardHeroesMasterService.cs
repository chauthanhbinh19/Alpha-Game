using System.Threading.Tasks;

public class UserCardHeroesMasterService : IUserCardHeroesMasterService
{
    private readonly IUserCardHeroesMasterRepository _cardHeroesMasterRepository;

    public UserCardHeroesMasterService(IUserCardHeroesMasterRepository cardHeroesMasterRepository)
    {
        _cardHeroesMasterRepository = cardHeroesMasterRepository;
    }

    public static UserCardHeroesMasterService Create()
    {
        return new UserCardHeroesMasterService(new UserCardHeroesMasterRepository());
    }

    public async Task<Master> GetCardHeroMasterAsync(string type, string card_id)
    {
        return await _cardHeroesMasterRepository.GetCardHeroMasterAsync(type, card_id);
    }

    public async Task InsertOrUpdateCardHeroMasterAsync(Master master, string type, string card_id)
    {
        await _cardHeroesMasterRepository.InsertOrUpdateCardHeroMasterAsync(master, type, card_id);
    }

    public async Task<Master> GetSumCardHeroesMasterAsync(string user_id, string card_id)
    {
        return await _cardHeroesMasterRepository.GetSumCardHeroesMasterAsync(user_id, card_id);
    }
}
