using System;
using System.Threading.Tasks;

public class UserPetsRankService : IUserPetsRankService
{
    private readonly IUserPetsRankRepository _userPetsRankRepository;

    // Constructor để inject dependency của repository
    public UserPetsRankService(IUserPetsRankRepository userPetsRankRepository)
    {
        _userPetsRankRepository = userPetsRankRepository;
    }

    public static IUserPetsRankService Create() => ServiceContainer.GetService<IUserPetsRankService>();

    public async Task<UserRanks> GetUserPetRankAsync(string userId, string id, string cardId)
    {
        return await _userPetsRankRepository.GetUserPetRankAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserPetRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userPetsRankRepository.InsertOrUpdateUserPetRankAsync(userId, userRank, cardId);
    }

    public async Task<UserRanks> GetSumUserPetsRankAsync(string userId, string cardId)
    {
        return await _userPetsRankRepository.GetSumUserPetsRankAsync(userId, cardId); ;
    }
}