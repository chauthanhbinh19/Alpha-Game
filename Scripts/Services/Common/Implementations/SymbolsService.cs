using System.Collections.Generic;
using System.Threading.Tasks;

public class SymbolsService : ISymbolsService
{
    private readonly ISymbolsRepository _symbolsRepository;

    public SymbolsService(ISymbolsRepository symbolsRepository)
    {
        _symbolsRepository = symbolsRepository;
    }

    public static ISymbolsService Create() => ServiceContainer.GetService<ISymbolsService>();

    public async Task<List<string>> GetUniqueSymbolsTypesAsync()
    {
        return await _symbolsRepository.GetUniqueSymbolsTypesAsync();
    }

    public async Task<List<Symbols>> GetSymbolsAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Symbols> list = await _symbolsRepository.GetSymbolsAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSymbolsCountAsync(string search, string type, string rare)
    {
        return await _symbolsRepository.GetSymbolsCountAsync(search, type, rare);
    }

    public async Task<InsertOrUpdateResult<bool>> InsertSymbolAsync(Symbols entity)
    {
        var result = await _symbolsRepository.InsertSymbolAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Inserted(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<InsertOrUpdateResult<bool>> UpdateSymbolAsync(Symbols entity)
    {
        var result = await _symbolsRepository.UpdateSymbolAsync(entity);

        if(result.Data != null && result.OperationType == DatabaseOperationType.Inserted)
        {
            return InsertOrUpdateResult<bool>.Updated(true);
        }

        return InsertOrUpdateResult<bool>.Failure();
    }

    public async Task<List<Symbols>> GetSymbolsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Symbols> list = await _symbolsRepository.GetSymbolsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSymbolsWithPriceCountAsync(string type)
    {
        return await _symbolsRepository.GetSymbolsWithPriceCountAsync(type);
    }

    public async Task<Symbols> GetSymbolByIdAsync(string Id)
    {
        return await _symbolsRepository.GetSymbolByIdAsync(Id);
    }

    public async Task<Symbols> SumPowerSymbolsPercentAsync(string userId)
    {
        return await _symbolsRepository.SumPowerSymbolsPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueSymbolsIdAsync()
    {
        return await _symbolsRepository.GetUniqueSymbolsIdAsync();
    }

    public async Task<List<Symbols>> GetSymbolsWithoutLimitAsync()
    {
        return await _symbolsRepository.GetSymbolsWithoutLimitAsync();
    }
}
