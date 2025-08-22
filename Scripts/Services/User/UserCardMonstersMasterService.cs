public class UserCardMonstersMasterService : IUserCardMonstersMasterService
{
    private readonly IUserCardMonstersMasterRepository _cardMonstersMasterRepository;

    public UserCardMonstersMasterService(IUserCardMonstersMasterRepository cardMonstersMasterRepository)
    {
        _cardMonstersMasterRepository = cardMonstersMasterRepository;
    }

    public static UserCardMonstersMasterService Create()
    {
        return new UserCardMonstersMasterService(new UserCardMonstersMasterRepository());
    }

    public Master GetCardMonstersMaster(string type, string card_id)
    {
        return _cardMonstersMasterRepository.GetCardMonstersMaster(type, card_id);
    }

    public void InsertOrUpdateCardMonstersMaster(Master master, string type, string card_id)
    {
        _cardMonstersMasterRepository.InsertOrUpdateCardMonstersMaster(master, type, card_id);
    }

    public Master GetSumCardMonstersMaster(string user_id, string card_id)
    {
        return _cardMonstersMasterRepository.GetSumCardMonstersMaster(user_id, card_id);
    }
}
