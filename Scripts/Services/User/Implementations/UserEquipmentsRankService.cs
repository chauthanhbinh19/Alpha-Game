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

    public async Task<Rank> GetUserEquipmentRankAsync(string userId, string id, string card_id)
    {
        return await _userEquipmentsRankRepository.GetEquipmentRankAsync(userId, id, card_id);
    }

    public async Task InsertOrUpdateUserEquipmentRankAsync(string userId, Rank rank, string card_id)
    {
        await _userEquipmentsRankRepository.InsertOrUpdateEquipmentRankAsync(userId, rank, card_id);
    }

    public async Task<Rank> GetSumUserEquipmentsRankAsync(string userId, string card_id)
    {
        return await _userEquipmentsRankRepository.GetSumEquipmentsRankAsync(userId, card_id);
    }
}
