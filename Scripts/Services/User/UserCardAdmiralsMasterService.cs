public class UserCardAdmiralsMasterService : IUserCardAdmiralsMasterService
{
    private readonly IUserCardAdmiralsMasterRepository _cardAdmiralsMasterRepository;

    public UserCardAdmiralsMasterService(IUserCardAdmiralsMasterRepository cardAdmiralsMasterRepository)
    {
        _cardAdmiralsMasterRepository = cardAdmiralsMasterRepository;
    }

    public static UserCardAdmiralsMasterService Create()
    {
        return new UserCardAdmiralsMasterService(new UserCardAdmiralsMasterRepository());
    }

    public Master GetCardAdmiralsMaster(string type, string card_id)
    {
        return _cardAdmiralsMasterRepository.GetCardAdmiralsMaster(type, card_id);
    }

    public void InsertOrUpdateCardAdmiralsMaster(Master master, string type, string card_id)
    {
        _cardAdmiralsMasterRepository.InsertOrUpdateCardAdmiralsMaster(master, type, card_id);
    }

    public Master GetSumCardAdmiralsMaster(string user_id, string card_id)
    {
        return _cardAdmiralsMasterRepository.GetSumCardAdmiralsMaster(user_id, card_id);
    }
}
