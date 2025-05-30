using System;

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

    public Rank GetPetsRank(string type, string card_id)
    {
        return _userPetsRankRepository.GetPetsRank(type, card_id);
    }

    public void InsertOrUpdatePetsRank(Rank rank, string type, string card_id)
    {
        _userPetsRankRepository.InsertOrUpdatePetsRank(rank, type, card_id);
    }

    public Rank GetSumPetsRank(string user_id, string card_id)
    {
        return _userPetsRankRepository.GetSumPetsRank(user_id, card_id);;
    }
}