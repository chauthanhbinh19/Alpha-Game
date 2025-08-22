public class UserCardCaptainsMasterService : IUserCardCaptainsMasterService
{
    private readonly IUserCardCaptainsMasterRepository _cardCaptainsMasterRepository;

    public UserCardCaptainsMasterService(IUserCardCaptainsMasterRepository cardCaptainsMasterRepository)
    {
        _cardCaptainsMasterRepository = cardCaptainsMasterRepository;
    }

    public static UserCardCaptainsMasterService Create()
    {
        return new UserCardCaptainsMasterService(new UserCardCaptainsMasterRepository());
    }

    public Master GetCardCaptainsMaster(string type, string card_id)
    {
        return _cardCaptainsMasterRepository.GetCardCaptainsMaster(type, card_id);
    }

    public void InsertOrUpdateCardCaptainsMaster(Master master, string type, string card_id)
    {
        _cardCaptainsMasterRepository.InsertOrUpdateCardCaptainsMaster(master, type, card_id);
    }

    public Master GetSumCardCaptainsMaster(string user_id, string card_id)
    {
        return _cardCaptainsMasterRepository.GetSumCardCaptainsMaster(user_id, card_id);
    }
}
