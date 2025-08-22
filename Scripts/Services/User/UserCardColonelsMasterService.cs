public class UserCardColonelsMasterService : IUserCardColonelsMasterService
{
    private readonly IUserCardColonelsMasterRepository _cardColonelsMasterRepository;

    public UserCardColonelsMasterService(IUserCardColonelsMasterRepository cardColonelsMasterRepository)
    {
        _cardColonelsMasterRepository = cardColonelsMasterRepository;
    }

    public static UserCardColonelsMasterService Create()
    {
        return new UserCardColonelsMasterService(new UserCardColonelsMasterRepository());
    }

    public Master GetCardColonelsMaster(string type, string card_id)
    {
        return _cardColonelsMasterRepository.GetCardColonelsMaster(type, card_id);
    }

    public void InsertOrUpdateCardColonelsMaster(Master master, string type, string card_id)
    {
        _cardColonelsMasterRepository.InsertOrUpdateCardColonelsMaster(master, type, card_id);
    }

    public Master GetSumCardColonelsMaster(string user_id, string card_id)
    {
        return _cardColonelsMasterRepository.GetSumCardColonelsMaster(user_id, card_id);
    }
}
