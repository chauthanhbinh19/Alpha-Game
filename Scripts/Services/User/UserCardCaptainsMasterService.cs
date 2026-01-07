using System.Threading.Tasks;

public class UserCardCaptainsMasterService : IUserCardCaptainsMasterService
{
    private readonly IUserCardCaptainsMasterRepository _cardCaptainsMasterRepository;

    public UserCardCaptainsMasterService(IUserCardCaptainsMasterRepository cardCaptainsMasterRepository)
    {
        _cardCaptainsMasterRepository = cardCaptainsMasterRepository;
    }

    public static UserCardCaptainsMasterService Create()
    {
        return new UserCardCaptainsMasterService(new UserCardCaptainsMasterRepository());
    }

    public async Task<Master> GetCardCaptainMasterAsync(string id, string card_id)
    {
        return await _cardCaptainsMasterRepository.GetCardCaptainMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateCardCaptainMasterAsync(Master master, string card_id)
    {
        await _cardCaptainsMasterRepository.InsertOrUpdateCardCaptainMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumCardCaptainsMasterAsync(string user_id, string card_id)
    {
        return await _cardCaptainsMasterRepository.GetSumCardCaptainsMasterAsync(user_id, card_id);
    }
}
