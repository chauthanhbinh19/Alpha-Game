using System.Threading.Tasks;

public class UserCardMilitariesMasterService : IUserCardMilitariesMasterService
{
    private readonly IUserCardMilitariesMasterRepository _cardMilitaryMasterRepository;

    public UserCardMilitariesMasterService(IUserCardMilitariesMasterRepository cardMilitaryMasterRepository)
    {
        _cardMilitaryMasterRepository = cardMilitaryMasterRepository;
    }

    public static UserCardMilitariesMasterService Create()
    {
        return new UserCardMilitariesMasterService(new UserCardMilitariesMasterRepository());
    }

    public async Task<Master> GetCardMilitaryMasterAsync(string type, string card_id)
    {
        return await _cardMilitaryMasterRepository.GetCardMilitaryMasterAsync(type, card_id);
    }

    public async Task InsertOrUpdateCardMilitaryMasterAsync(Master master, string type, string card_id)
    {
        await _cardMilitaryMasterRepository.InsertOrUpdateCardMilitaryMasterAsync(master, type, card_id);
    }

    public async Task<Master> GetSumCardMilitariesMasterAsync(string user_id, string card_id)
    {
        return await _cardMilitaryMasterRepository.GetSumCardMilitariesMasterAsync(user_id, card_id);
    }
}
