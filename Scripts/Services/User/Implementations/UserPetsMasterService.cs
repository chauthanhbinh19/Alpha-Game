using System;
using System.Threading.Tasks;

public class UserPetsMasterService : IUserPetsMasterService
{
    private readonly IUserPetsMasterRepository _userPetsMasterRepository;

    // Constructor để inject dependency của repository
    public UserPetsMasterService(IUserPetsMasterRepository userPetsMasterRepository)
    {
        _userPetsMasterRepository = userPetsMasterRepository;
    }

    public static IUserPetsMasterService Create() => ServiceContainer.GetService<IUserPetsMasterService>();

    public async Task<Master> GetUserPetMasterAsync(string userId, string id, string cardId)
    {
        return await _userPetsMasterRepository.GetUserPetMasterAsync(userId, id, cardId);
    }

    public async Task InsertOrUpdateUserPetMasterAsync(string userId, UserMasters userMaster, string cardId)
    {
        await _userPetsMasterRepository.InsertOrUpdateUserPetMasterAsync(userId, userMaster, cardId);
    }

    public async Task<Master> GetSumUserPetsMasterAsync(string userId, string cardId)
    {
        return await _userPetsMasterRepository.GetSumUserPetsMasterAsync(userId, cardId); ;
    }
}