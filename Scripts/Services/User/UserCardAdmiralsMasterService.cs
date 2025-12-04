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

    public async Task<Master> GetCardAdmiralMasterAsync(string type, string card_id)
    {
        return await _cardAdmiralsMasterRepository.GetCardAdmiralMasterAsync(type, card_id);
    }

    public async Task InsertOrUpdateCardAdmiralMasterAsync(Master master, string type, string card_id)
    {
        await _cardAdmiralsMasterRepository.InsertOrUpdateCardAdmiralMasterAsync(master, type, card_id);
    }

    public async Task<Master> GetSumCardAdmiralsMasterAsync(string user_id, string card_id)
    {
        return await _cardAdmiralsMasterRepository.GetSumCardAdmiralsMasterAsync(user_id, card_id);
    }
}
