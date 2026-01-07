using System.Threading.Tasks;

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

    public async Task<Master> GetCardAdmiralMasterAsync(string id, string card_id)
    {
        return await _cardAdmiralsMasterRepository.GetCardAdmiralMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardAdmiralMasterAsync(Master master, string card_id)
    {
        await _cardAdmiralsMasterRepository.InsertOrUpdateCardAdmiralMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumCardAdmiralsMasterAsync(string user_id, string card_id)
    {
        return await _cardAdmiralsMasterRepository.GetSumCardAdmiralsMasterAsync(user_id, card_id);
    }
}
