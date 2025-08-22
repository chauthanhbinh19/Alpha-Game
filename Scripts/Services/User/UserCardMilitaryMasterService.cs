public class UserCardMilitaryMasterService : IUserCardMilitaryMasterService
{
    private readonly IUserCardMilitaryMasterRepository _cardMilitaryMasterRepository;

    public UserCardMilitaryMasterService(IUserCardMilitaryMasterRepository cardMilitaryMasterRepository)
    {
        _cardMilitaryMasterRepository = cardMilitaryMasterRepository;
    }

    public static UserCardMilitaryMasterService Create()
    {
        return new UserCardMilitaryMasterService(new UserCardMilitaryMasterRepository());
    }

    public Master GetCardMilitaryMaster(string type, string card_id)
    {
        return _cardMilitaryMasterRepository.GetCardMilitaryMaster(type, card_id);
    }

    public void InsertOrUpdateCardMilitaryMaster(Master master, string type, string card_id)
    {
        _cardMilitaryMasterRepository.InsertOrUpdateCardMilitaryMaster(master, type, card_id);
    }

    public Master GetSumCardMilitaryMaster(string user_id, string card_id)
    {
        return _cardMilitaryMasterRepository.GetSumCardMilitaryMaster(user_id, card_id);
    }
}
