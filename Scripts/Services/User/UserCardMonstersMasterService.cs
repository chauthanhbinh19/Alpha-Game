using System.Threading.Tasks;

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

    public async Task<Master> GetCardMonsterMasterAsync(string id, string card_id)
    {
        return await _cardMonstersMasterRepository.GetCardMonsterMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardMonsterMasterAsync(Master master, string card_id)
    {
        await _cardMonstersMasterRepository.InsertOrUpdateCardMonsterMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumCardMonstersMasterAsync(string user_id, string card_id)
    {
        return await _cardMonstersMasterRepository.GetSumCardMonstersMasterAsync(user_id, card_id);
    }
}
