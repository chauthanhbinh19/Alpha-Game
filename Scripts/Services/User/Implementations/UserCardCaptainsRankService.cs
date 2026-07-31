using System.Threading.Tasks;

public class UserCardCaptainsRankService : IUserCardCaptainsRankService
{
    private readonly IUserCardCaptainsRankRepository _userCardCaptainsRankRepository;

    public UserCardCaptainsRankService(IUserCardCaptainsRankRepository userCardCaptainsRankRepository)
    {
        _userCardCaptainsRankRepository = userCardCaptainsRankRepository;
    }

    public static IUserCardCaptainsRankService Create() => ServiceContainer.GetService<IUserCardCaptainsRankService>();

    public async Task<Rank> GetUserCardCaptainRankAsync(string userId, string id, string cardId)
    {
        return await _userCardCaptainsRankRepository.GetUserCardCaptainRankAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardCaptainRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardCaptainsRankRepository.InsertOrUpdateUserCardCaptainRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumUserCardCaptainsRankAsync(string userId, string cardId)
    {
        return await _userCardCaptainsRankRepository.GetSumUserCardCaptainsRankAsync(userId, cardId);
    }
}
