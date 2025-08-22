public class UserCardSpellMasterService : IUserCardSpellMasterService
{
    private readonly IUserCardSpellMasterRepository _cardSpellMasterRepository;

    public UserCardSpellMasterService(IUserCardSpellMasterRepository cardSpellMasterRepository)
    {
        _cardSpellMasterRepository = cardSpellMasterRepository;
    }

    public static UserCardSpellMasterService Create()
    {
        return new UserCardSpellMasterService(new UserCardSpellMasterRepository());
    }

    public Master GetCardSpellMaster(string type, string card_id)
    {
        return _cardSpellMasterRepository.GetCardSpellMaster(type, card_id);
    }

    public void InsertOrUpdateCardSpellMaster(Master master, string type, string card_id)
    {
        _cardSpellMasterRepository.InsertOrUpdateCardSpellMaster(master, type, card_id);
    }

    public Master GetSumCardSpellMaster(string user_id, string card_id)
    {
        return _cardSpellMasterRepository.GetSumCardSpellMaster(user_id, card_id);
    }
}
