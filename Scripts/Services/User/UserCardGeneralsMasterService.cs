public class UserCardGeneralsMasterService : IUserCardGeneralsMasterService
{
    private readonly IUserCardGeneralsMasterRepository _cardGeneralsMasterRepository;

    public UserCardGeneralsMasterService(IUserCardGeneralsMasterRepository cardGeneralsMasterRepository)
    {
        _cardGeneralsMasterRepository = cardGeneralsMasterRepository;
    }

    public static UserCardGeneralsMasterService Create()
    {
        return new UserCardGeneralsMasterService(new UserCardGeneralsMasterRepository());
    }

    public Master GetCardGeneralsMaster(string type, string card_id)
    {
        return _cardGeneralsMasterRepository.GetCardGeneralsMaster(type, card_id);
    }

    public void InsertOrUpdateCardGeneralsMaster(Master master, string type, string card_id)
    {
        _cardGeneralsMasterRepository.InsertOrUpdateCardGeneralsMaster(master, type, card_id);
    }

    public Master GetSumCardGeneralsMaster(string user_id, string card_id)
    {
        return _cardGeneralsMasterRepository.GetSumCardGeneralsMaster(user_id, card_id);
    }
}
