using System.Threading.Tasks;

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

    public async Task<Master> GetCardGeneralMasterAsync(string type, string card_id)
    {
        return await _cardGeneralsMasterRepository.GetCardGeneralMasterAsync(type, card_id);
    }

    public async Task InsertOrUpdateCardGeneralMasterAsync(Master master, string type, string card_id)
    {
        await _cardGeneralsMasterRepository.InsertOrUpdateCardGeneralMasterAsync(master, type, card_id);
    }

    public async Task<Master> GetSumCardGeneralsMasterAsync(string user_id, string card_id)
    {
        return await _cardGeneralsMasterRepository.GetSumCardGeneralsMasterAsync(user_id, card_id);
    }
}
