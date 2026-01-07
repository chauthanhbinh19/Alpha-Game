using System.Threading.Tasks;

public class UserBooksMasterService : IUserBooksMasterService
{
    private readonly IUserBooksMasterRepository _BooksMasterRepository;

    public UserBooksMasterService(IUserBooksMasterRepository BooksMasterRepository)
    {
        _BooksMasterRepository = BooksMasterRepository;
    }

    public static UserBooksMasterService Create()
    {
        return new UserBooksMasterService(new UserBooksMasterRepository());
    }

    public async Task<Master> GetBookMasterAsync(string id, string card_id)
    {
        return await _BooksMasterRepository.GetBookMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdateBookMasterAsync(Master master, string card_id)
    {
        await _BooksMasterRepository.InsertOrUpdateBookMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumBooksMasterAsync(string user_id, string card_id)
    {
        return await _BooksMasterRepository.GetSumBooksMasterAsync(user_id, card_id);
    }
}
