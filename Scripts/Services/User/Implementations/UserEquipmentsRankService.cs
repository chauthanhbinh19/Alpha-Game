using System.Threading.Tasks;

public class UserEquipmentsRankService : IUserEquipmentsRankService
{
     private static UserEquipmentsRankService _instance;
    private readonly IUserEquipmentsRankRepository _userEquipmentsRankRepository;

    public UserEquipmentsRankService(IUserEquipmentsRankRepository userEquipmentsRankRepository)
    {
        _userEquipmentsRankRepository = userEquipmentsRankRepository;
    }

    public static UserEquipmentsRankService Create()
    {
        if (_instance == null)
        {
            _instance = new UserEquipmentsRankService(new UserEquipmentsRankRepository());
        }
        return _instance;
    }

    public async Task<Rank> GetEquipmentRankAsync(string id, string card_id)
    {
        return await _userEquipmentsRankRepository.GetEquipmentRankAsync(id, card_id);
    }

    public async Task InsertOrUpdateEquipmentRankAsync(Rank rank, string card_id)
    {
        await _userEquipmentsRankRepository.InsertOrUpdateEquipmentRankAsync(rank, card_id);
    }

    public async Task<Rank> GetSumEquipmentsRankAsync(string user_id, string card_id)
    {
        return await _userEquipmentsRankRepository.GetSumEquipmentsRankAsync(user_id, card_id);
    }
}
