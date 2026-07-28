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

    public async Task<List<SpiritCards>> GetUserSpiritCardAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<SpiritCards> list = await _userSpiritCardsRepository.GetUserSpiritCardsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSpiritCardCountAsync(string userId, string search, string type, string rare)
    {
        return await _userSpiritCardsRepository.GetUserSpiritCardsCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserSpiritCardAsync(string userId, SpiritCards spiritCard)
    {
        var result = await _userSpiritCardsRepository.InsertUserSpiritCardAsync(userId, spiritCard);
        if (result)
        {
            await SpiritCardsGalleryService.Create().InsertSpiritCardGalleryAsync(userId, spiritCard.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserSpiritCardLevelAsync(string userId, SpiritCards spiritCard)
    {
        return await _userSpiritCardsRepository.UpdateUserSpiritCardLevelAsync(userId, spiritCard);
    }

    public async Task<bool> UpdateUserSpiritCardStarAsync(string userId, SpiritCards spiritCard)
    {
        var result = await _userSpiritCardsRepository.UpdateUserSpiritCardStarAsync(userId, spiritCard);
        if (result)
        {
            await SpiritCardsGalleryService.Create().UpdateStarSpiritCardGalleryAsync(userId, spiritCard.Id, spiritCard.Star);
        }
        return result;
    }

    public async Task<bool> UpdateUserSpiritCardBreakthroughAsync(string userId, SpiritCards spiritCard, int star, double quantity)
    {
        return await _userSpiritCardsRepository.UpdateUserSpiritCardBreakthroughAsync(userId, spiritCard, star, quantity);
    }

    public async Task<SpiritCards> GetUserSpiritCardByIdAsync(string userId, string Id)
    {
        return await _userSpiritCardsRepository.GetUserSpiritCardByIdAsync(userId, Id);
    }

    public async Task<SpiritCards> SumPowerUserSpiritCardsAsync(string userId)
    {
        return await _userSpiritCardsRepository.SumPowerUserSpiritCardsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserSpiritCardsBatchAsync(string userId, List<SpiritCards> spiritCards)
    {
        return await _userSpiritCardsRepository.InsertOrUpdateUserSpiritCardsBatchAsync(userId, spiritCards);
    }
}
