using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSpiritCardsService : IUserSpiritCardsService
{
     private static UserSpiritCardsService _instance;
    private readonly IUserSpiritCardsRepository _userSpiritCardsRepository;

    public UserSpiritCardsService(IUserSpiritCardsRepository userSpiritCardsRepository)
    {
        _userSpiritCardsRepository = userSpiritCardsRepository;
    }

    public static UserSpiritCardsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserSpiritCardsService(new UserSpiritCardsRepository());
        }
        return _instance;
    }

    public async Task<List<SpiritCards>> GetUserSpiritCardAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> list = await _userSpiritCardsRepository.GetUserSpiritCardsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSpiritCardCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userSpiritCardsRepository.GetUserSpiritCardsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserSpiritCardAsync(SpiritCards spiritCard)
    {
        return await _userSpiritCardsRepository.InsertUserSpiritCardAsync(spiritCard);
    }

    public async Task<bool> UpdateSpiritCardLevelAsync(SpiritCards spiritCard, int level)
    {
        return await _userSpiritCardsRepository.UpdateSpiritCardLevelAsync(spiritCard, level);
    }

    public async Task<bool> UpdateSpiritCardBreakthroughAsync(SpiritCards spiritCard, int star, double quantity)
    {
        return await _userSpiritCardsRepository.UpdateSpiritCardBreakthroughAsync(spiritCard, star, quantity);
    }

    public async Task<SpiritCards> GetUserSpiritCardByIdAsync(string user_id, string Id)
    {
        return await _userSpiritCardsRepository.GetUserSpiritCardByIdAsync(user_id, Id);
    }

    public async Task<SpiritCards> SumPowerUserSpiritCardsAsync()
    {
        return await _userSpiritCardsRepository.SumPowerUserSpiritCardsAsync();
    }

    public async Task<bool> InsertOrUpdateUserSpiritCardsBatchAsync(List<SpiritCards> spiritCards)
    {
        return await _userSpiritCardsRepository.InsertOrUpdateUserSpiritCardsBatchAsync(spiritCards);
    }
}
