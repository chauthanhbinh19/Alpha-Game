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

    public async Task<Master> GetCardCaptainMasterAsync(string type, string card_id)
    {
        return await _cardCaptainsMasterRepository.GetCardCaptainMasterAsync(type, card_id);
    }

    public async Task InsertOrUpdateCardCaptainMasterAsync(Master master, string type, string card_id)
    {
        await _cardCaptainsMasterRepository.InsertOrUpdateCardCaptainMasterAsync(master, type, card_id);
    }

    public async Task<Master> GetSumCardCaptainsMasterAsync(string user_id, string card_id)
    {
        return await _cardCaptainsMasterRepository.GetSumCardCaptainsMasterAsync(user_id, card_id);
    }
}
