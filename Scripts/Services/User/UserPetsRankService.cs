using System;
using System.Threading.Tasks;

public class UserPetsRankService : IUserPetsRankService
{
    private readonly IUserPetsRankRepository _userPetsRankRepository;

    // Constructor để inject dependency của repository
    public UserPetsRankService(IUserPetsRankRepository userPetsRankRepository)
    {
        _userPetsRankRepository = userPetsRankRepository ?? throw new ArgumentNullException(nameof(userPetsRankRepository));
    }

    public static UserPetsRankService Create()
    {
        return new UserPetsRankService(new UserPetsRankRepository());
    }

    public async Task<Rank> GetPetRankAsync(string id, string card_id)
    {
        return await _userPetsRankRepository.GetPetRankAsync(id, card_id);
    }

    public async Task InsertOrUpdatePetRankAsync(Rank rank, string card_id)
    {
        await _userPetsRankRepository.InsertOrUpdatePetRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumPetsRankAsync(string user_id, string card_id)
    {
        return await _userPetsRankRepository.GetSumPetsRankAsync(user_id, card_id);;
    }
}