using System.Threading.Tasks;

public class UserCardSpellsMasterService : IUserCardSpellsMasterService
{
    private readonly IUserCardSpellsMasterRepository _cardSpellMasterRepository;

    public UserCardSpellsMasterService(IUserCardSpellsMasterRepository cardSpellMasterRepository)
    {
        _cardSpellMasterRepository = cardSpellMasterRepository;
    }

    public static UserCardSpellsMasterService Create()
    {
        return new UserCardSpellsMasterService(new UserCardSpellsMasterRepository());
    }

    public async Task<Master> GetCardSpellMasterAsync(string id, string card_id)
    {
        return await _cardSpellMasterRepository.GetCardSpellMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardSpellMasterAsync(Master master, string card_id)
    {
        await _cardSpellMasterRepository.InsertOrUpdateCardSpellMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumCardSpellsMasterAsync(string user_id, string card_id)
    {
        return await _cardSpellMasterRepository.GetSumCardSpellsMasterAsync(user_id, card_id);
    }
}
