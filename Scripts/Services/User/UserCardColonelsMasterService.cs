using System.Threading.Tasks;

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

    public async Task<Master> GetCardColonelMasterAsync(string type, string card_id)
    {
        return await _cardColonelsMasterRepository.GetCardColonelMasterAsync(type, card_id);
    }

    public async Task InsertOrUpdateCardColonelMasterAsync(Master master, string type, string card_id)
    {
        await _cardColonelsMasterRepository.InsertOrUpdateCardColonelMasterAsync(master, type, card_id);
    }

    public async Task<Master> GetSumCardColonelsMasterAsync(string user_id, string card_id)
    {
        return await _cardColonelsMasterRepository.GetSumCardColonelsMasterAsync(user_id, card_id);
    }
}
