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

    
    

    public async Task<List<CardLives>> GetUserCardLivesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<CardLives> list = await _userCardLivesRepository.GetUserCardLivesAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserCardLivesCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userCardLivesRepository.GetUserCardLivesCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserCardLifeAsync(CardLives cardLife, string userId)
    {
        return await _userCardLivesRepository.InsertUserCardLifeAsync(cardLife, userId);
    }

    public async Task<bool> UpdateCardLifeLevelAsync(CardLives cardLife, int level)
    {
        return await _userCardLivesRepository.UpdateCardLifeLevelAsync(cardLife, level);
    }

    public async Task<bool> UpdateCardLifeBreakthroughAsync(CardLives cardLife, int star, double quantity)
    {
        return await _userCardLivesRepository.UpdateCardLifeBreakthroughAsync(cardLife, star, quantity);
    }

    public async Task<CardLives> GetUserCardLifeByIdAsync(string user_id, string Id)
    {
        return await _userCardLivesRepository.GetUserCardLifeByIdAsync(user_id, Id);
    }

    public async Task<CardLives> SumPowerUserCardLivesAsync()
    {
        return await _userCardLivesRepository.SumPowerUserCardLivesAsync();
    }

    public async Task<bool> InsertOrUpdateUserCardLivesBatchAsync(List<CardLives> cardLives)
    {
        return await _userCardLivesRepository.InsertOrUpdateUserCardLivesBatchAsync(cardLives);
    }
}
