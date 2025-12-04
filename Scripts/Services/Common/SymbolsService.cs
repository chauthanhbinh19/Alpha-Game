using System.Collections.Generic;
using System.Threading.Tasks;

public class SymbolsService : ISymbolsService
{
    private readonly ISymbolsRepository _symbolsRepository;

    public SymbolsService(ISymbolsRepository symbolsRepository)
    {
        _symbolsRepository = symbolsRepository;
    }

    public static SymbolsService Create()
    {
        return new SymbolsService(new SymbolsRepository());
    }

    public async Task<List<string>> GetUniqueSymbolsTypesAsync()
    {
        return await _symbolsRepository.GetUniqueSymbolsTypesAsync();
    }

    public async Task<List<Symbols>> GetSymbolsAsync(string type, int pageSize, int offset, string rare)
    {
        List<Symbols> list = await _symbolsRepository.GetSymbolsAsync(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetSymbolsCountAsync(string type, string rare)
    {
        return await _symbolsRepository.GetSymbolsCountAsync(type, rare);
    }

    public async Task<List<Symbols>> GetSymbolsWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Symbols> list = await _symbolsRepository.GetSymbolsWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
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

    public async Task<Symbols> SumPowerSymbolsPercentAsync()
    {
        return await _symbolsRepository.SumPowerSymbolsPercentAsync();
    }

    public async Task<List<string>> GetUniqueSymbolsIdAsync()
    {
        return await _symbolsRepository.GetUniqueSymbolsIdAsync();
    }
}
