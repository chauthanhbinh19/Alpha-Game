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

    public async Task<Rank> GetPetRankAsync(string type, string card_id)
    {
        return await _userPetsRankRepository.GetPetRankAsync(type, card_id);
    }

    public async Task InsertOrUpdatePetRankAsync(Rank rank, string type, string card_id)
    {
        await _userPetsRankRepository.InsertOrUpdatePetRankAsync(rank, type, card_id);
    }

    public async Task<Rank> GetSumPetsRankAsync(string user_id, string card_id)
    {
        return await _userPetsRankRepository.GetSumPetsRankAsync(user_id, card_id);;
    }
}