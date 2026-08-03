using System.Threading.Tasks;

public class UserCardAdmiralsRankService : IUserCardAdmiralsRankService
{
    private readonly IUserCardAdmiralsRankRepository _userCardAdmiralsRankRepository;

    public UserCardAdmiralsRankService(IUserCardAdmiralsRankRepository userCardAdmiralsRankRepository)
    {
        _userCardAdmiralsRankRepository = userCardAdmiralsRankRepository;
    }

    public static IUserCardAdmiralsRankService Create() => ServiceContainer.GetService<IUserCardAdmiralsRankService>();

    public async Task<UserRanks> GetUserCardAdmiralRankAsync(string userId, string id, string cardId)
    {
        return await _userCardAdmiralsRankRepository.GetUserCardAdmiralRankAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserCardAdmiralRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userCardAdmiralsRankRepository.InsertOrUpdateUserCardAdmiralRankAsync(userId, userRank, cardId);
    }

    public async Task<UserRanks> GetSumUserCardAdmiralsRankAsync(string userId, string cardId)
    {
        return await _userCardAdmiralsRankRepository.GetSumUserCardAdmiralsRankAsync(userId, cardId);
    }
}
