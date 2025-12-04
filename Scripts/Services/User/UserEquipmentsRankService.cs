using System.Threading.Tasks;

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

    public async Task<Rank> GetEquipmentRankAsync(string type, string card_id)
    {
        return await _userEquipmentsRankRepository.GetEquipmentRankAsync(type, card_id);
    }

    public async Task InsertOrUpdateEquipmentRankAsync(Rank rank, string type, string card_id)
    {
        await _userEquipmentsRankRepository.InsertOrUpdateEquipmentRankAsync(rank, type, card_id);
    }

    public async Task<Rank> GetSumEquipmentsRankAsync(string user_id, string card_id)
    {
        return await _userEquipmentsRankRepository.GetSumEquipmentsRankAsync(user_id, card_id);
    }
}
