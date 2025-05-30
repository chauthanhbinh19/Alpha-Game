public class UserEquipmentsRankService : IUserEquipmentsRankService
{
    private readonly IUserEquipmentsRankRepository _userEquipmentsRankRepository;

    public UserEquipmentsRankService(IUserEquipmentsRankRepository userEquipmentsRankRepository)
    {
        _userEquipmentsRankRepository = userEquipmentsRankRepository;
    }

    public static UserEquipmentsRankService Create()
    {
        return new UserEquipmentsRankService(new UserEquipmentsRankRepository());
    }

    public Rank GetEquipmentsRank(string type, string card_id)
    {
        return _userEquipmentsRankRepository.GetEquipmentsRank(type, card_id);
    }

    public void InsertOrUpdateEquipmentsRank(Rank rank, string type, string card_id)
    {
        _userEquipmentsRankRepository.InsertOrUpdateEquipmentsRank(rank, type, card_id);
    }

    public Rank GetSumEquipmentsRank(string user_id, string card_id)
    {
        return _userEquipmentsRankRepository.GetSumEquipmentsRank(user_id, card_id);
    }
}
