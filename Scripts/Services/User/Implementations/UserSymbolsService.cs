using System.Collections.Generic;
using System.Threading.Tasks;

public class UserSymbolsService : IUserSymbolsService
{
     private static UserSymbolsService _instance;
    private readonly IUserSymbolsRepository _userSymbolsRepository;

    public UserSymbolsService(IUserSymbolsRepository userSymbolsRepository)
    {
        _userSymbolsRepository = userSymbolsRepository;
    }

    public static UserSymbolsService Create()
    {
        if (_instance == null)
        {
            _instance = new UserSymbolsService(new UserSymbolsRepository());
        }
        return _instance;
    }

    public async Task<List<Symbols>> GetUserSymbolsAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Symbols> list = await _userSymbolsRepository.GetUserSymbolsAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSymbolsCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userSymbolsRepository.GetUserSymbolsCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserSymbolAsync(Symbols symbol, string userId)
    {
        return await _userSymbolsRepository.InsertUserSymbolAsync(symbol, userId);
    }

    public async Task<bool> UpdateSymbolLevelAsync(Symbols symbol, int level)
    {
        return await _userSymbolsRepository.UpdateSymbolLevelAsync(symbol, level);
    }

    public async Task<bool> UpdateSymbolBreakthroughAsync(Symbols symbol, int star, double quantity)
    {
        return await _userSymbolsRepository.UpdateSymbolBreakthroughAsync(symbol, star, quantity);
    }

    public async Task<Symbols> GetUserSymbolByIdAsync(string user_id, string Id)
    {
        return await _userSymbolsRepository.GetUserSymbolByIdAsync(user_id, Id);
    }

    public async Task<Symbols> SumPowerUserSymbolsAsync()
    {
        return await _userSymbolsRepository.SumPowerUserSymbolsAsync();
    }

    public async Task<bool> InsertOrUpdateUserSymbolsBatchAsync(List<Symbols> symbols)
    {
        return await _userSymbolsRepository.InsertOrUpdateUserSymbolsBatchAsync(symbols);
    }
}
