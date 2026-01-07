using System;
using System.Threading.Tasks;

public class UserPetsMasterService : IUserPetsMasterService
{
    private readonly IUserPetsMasterRepository _userPetsMasterRepository;

    // Constructor để inject dependency của repository
    public UserPetsMasterService(IUserPetsMasterRepository userPetsMasterRepository)
    {
        _userPetsMasterRepository = userPetsMasterRepository ?? throw new ArgumentNullException(nameof(userPetsMasterRepository));
    }

    public static UserPetsMasterService Create()
    {
        return new UserPetsMasterService(new UserPetsMasterRepository());
    }

    public async Task<Master> GetPetMasterAsync(string id, string card_id)
    {
        return await _userPetsMasterRepository.GetPetMasterAsync(id, card_id);
    }

    public async Task InsertOrUpdatePetMasterAsync(Master master, string card_id)
    {
        await _userPetsMasterRepository.InsertOrUpdatePetMasterAsync(master, card_id);
    }

    public async Task<Master> GetSumPetsMasterAsync(string user_id, string card_id)
    {
        return await _userPetsMasterRepository.GetSumPetsMasterAsync(user_id, card_id);;
    }
}