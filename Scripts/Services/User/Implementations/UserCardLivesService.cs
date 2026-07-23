using System.Collections.Generic;
using System.Threading.Tasks;

public class UserCardLivesService : IUserCardLivesService
{
    private static UserCardLivesService _instance;
    private readonly IUserCardLivesRepository _userCardLivesRepository;

    public UserCardLivesService(IUserCardLivesRepository userCardLivesRepository)
    {
        _userCardLivesRepository = userCardLivesRepository;
    }

    public static UserCardLivesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserCardLivesService(new UserCardLivesRepository());
        }
        return _instance;
    }




    public async Task<List<CardLives>> GetUserCardLivesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardLives> list = await _userCardLivesRepository.GetUserCardLivesAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserCardLivesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userCardLivesRepository.GetUserCardLivesCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserCardLifeAsync(CardLives cardLife, string userId)
    {
        return await _userCardLivesRepository.InsertUserCardLifeAsync(cardLife, userId);
    }

    public async Task<bool> UpdateUserCardLifeLevelAsync(string userId, CardLives cardLife)
    {
        return await _userCardLivesRepository.UpdateUserCardLifeLevelAsync(userId, cardLife);
    }

    public async Task<bool> UpdateUserCardLifeStarAsync(string userId, CardLives cardLife)
    {
        return await _userCardLivesRepository.UpdateUserCardLifeStarAsync(userId, cardLife);
    }

    public async Task<bool> UpdateUserCardLifeBreakthroughAsync(string userId, CardLives cardLife, int star, double quantity)
    {
        return await _userCardLivesRepository.UpdateUserCardLifeBreakthroughAsync(userId, cardLife, star, quantity);
    }

    public async Task<CardLives> GetUserCardLifeByIdAsync(string userId, string Id)
    {
        return await _userCardLivesRepository.GetUserCardLifeByIdAsync(userId, Id);
    }

    public async Task<CardLives> SumPowerUserCardLivesAsync(string userId)
    {
        return await _userCardLivesRepository.SumPowerUserCardLivesAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserCardLivesBatchAsync(string userId, List<CardLives> cardLives)
    {
        return await _userCardLivesRepository.InsertOrUpdateUserCardLivesBatchAsync(userId, cardLives);
    }
}
