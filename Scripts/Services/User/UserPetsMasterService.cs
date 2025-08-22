using System;

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

    public Master GetPetsMaster(string type, string card_id)
    {
        return _userPetsMasterRepository.GetPetsMaster(type, card_id);
    }

    public void InsertOrUpdatePetsMaster(Master master, string type, string card_id)
    {
        _userPetsMasterRepository.InsertOrUpdatePetsMaster(master, type, card_id);
    }

    public Master GetSumPetsMaster(string user_id, string card_id)
    {
        return _userPetsMasterRepository.GetSumPetsMaster(user_id, card_id);;
    }
}