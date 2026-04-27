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

    public async Task<bool> InsertUserSymbolAsync(Symbols symbols, string userId)
    {
        return await _userSymbolsRepository.InsertUserSymbolAsync(symbols, userId);
    }

    public async Task<bool> UpdateSymbolLevelAsync(Symbols symbols, int cardLevel)
    {
        return await _userSymbolsRepository.UpdateSymbolLevelAsync(symbols, cardLevel);
    }

    public async Task<bool> UpdateSymbolBreakthroughAsync(Symbols symbols, int star, double quantity)
    {
        return await _userSymbolsRepository.UpdateSymbolBreakthroughAsync(symbols, star, quantity);
    }

    public async Task<Symbols> GetUserSymbolByIdAsync(string user_id, string Id)
    {
        return await _userSymbolsRepository.GetUserSymbolByIdAsync(user_id, Id);
    }

    public async Task<Symbols> SumPowerUserSymbolsAsync()
    {
        return await _userSymbolsRepository.SumPowerUserSymbolsAsync();
    }
}
