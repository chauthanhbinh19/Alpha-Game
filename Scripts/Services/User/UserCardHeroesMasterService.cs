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

    public Master GetCardHeroesMaster(string type, string card_id)
    {
        return _cardHeroesMasterRepository.GetCardHeroesMaster(type, card_id);
    }

    public void InsertOrUpdateCardHeroesMaster(Master master, string type, string card_id)
    {
        _cardHeroesMasterRepository.InsertOrUpdateCardHeroesMaster(master, type, card_id);
    }

    public Master GetSumCardHeroesMaster(string user_id, string card_id)
    {
        return _cardHeroesMasterRepository.GetSumCardHeroesMaster(user_id, card_id);
    }
}
