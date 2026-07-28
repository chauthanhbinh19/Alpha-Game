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

    public async Task<List<Symbols>> GetUserSymbolsAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Symbols> list = await _userSymbolsRepository.GetUserSymbolsAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserSymbolsCountAsync(string userId, string search, string type, string rare)
    {
        return await _userSymbolsRepository.GetUserSymbolsCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserSymbolAsync(Symbols symbol, string userId)
    {
        var result = await _userSymbolsRepository.InsertUserSymbolAsync(symbol, userId);
        if (result)
        {
            await SymbolsGalleryService.Create().InsertSymbolGalleryAsync(userId, symbol.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserSymbolLevelAsync(string userId, Symbols symbol)
    {
        return await _userSymbolsRepository.UpdateUserSymbolLevelAsync(userId, symbol);
    }

    public async Task<bool> UpdateUserSymbolStarAsync(string userId, Symbols symbol)
    {
        var result = await _userSymbolsRepository.UpdateUserSymbolStarAsync(userId, symbol);
        if (result)
        {
            await SymbolsGalleryService.Create().UpdateStarSymbolGalleryAsync(userId, symbol.Id, symbol.Star);
        }
        return result;
    }

    public async Task<bool> UpdateUserSymbolBreakthroughAsync(string userId, Symbols symbol, int star, double quantity)
    {
        return await _userSymbolsRepository.UpdateUserSymbolBreakthroughAsync(userId, symbol, star, quantity);
    }

    public async Task<Symbols> GetUserSymbolByIdAsync(string userId, string Id)
    {
        return await _userSymbolsRepository.GetUserSymbolByIdAsync(userId, Id);
    }

    public async Task<Symbols> SumPowerUserSymbolsAsync(string userId)
    {
        return await _userSymbolsRepository.SumPowerUserSymbolsAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserSymbolsBatchAsync(string userId, List<Symbols> symbols)
    {
        return await _userSymbolsRepository.InsertOrUpdateUserSymbolsBatchAsync(userId, symbols);
    }
}
