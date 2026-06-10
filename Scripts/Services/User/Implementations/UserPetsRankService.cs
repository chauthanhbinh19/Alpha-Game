using System;
using System.Threading.Tasks;

public class UserPetsRankService : IUserPetsRankService
{
     private static UserPetsRankService _instance;
    private readonly IUserPetsRankRepository _userPetsRankRepository;

    // Constructor để inject dependency của repository
    public UserPetsRankService(IUserPetsRankRepository userPetsRankRepository)
    {
        _userPetsRankRepository = userPetsRankRepository;
    }

    public static UserPetsRankService Create()
    {
        if (_instance == null)
        {
            _instance = new UserPetsRankService(new UserPetsRankRepository());
        }
        return _instance;
    }

    public async Task<Rank> GetPetRankAsync(string id, string card_id)
    {
        return await _userPetsRankRepository.GetPetRankAsync(id, card_id);
    }

    public async Task InsertOrUpdatePetRankAsync(string userId, UserRanks userRank, string cardId)
    {
        await _userPetsRankRepository.InsertOrUpdatePetRankAsync(userId, userRank, cardId);
    }

    public async Task<Rank> GetSumPetsRankAsync(string user_id, string card_id)
    {
        return await _userPetsRankRepository.GetSumPetsRankAsync(user_id, card_id);;
    }
}