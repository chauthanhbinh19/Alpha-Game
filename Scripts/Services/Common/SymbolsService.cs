using System.Collections.Generic;

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

    public List<string> GetUniqueSymbolsTypes()
    {
        return _symbolsRepository.GetUniqueSymbolsTypes();
    }

    public List<Symbols> GetSymbols(string type, int pageSize, int offset)
    {
        List<Symbols> list = _symbolsRepository.GetSymbols(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetSymbolsCount(string type)
    {
        return _symbolsRepository.GetSymbolsCount(type);
    }

    public List<Symbols> GetSymbolsWithPrice(string type, int pageSize, int offset)
    {
        List<Symbols> list = _symbolsRepository.GetSymbolsWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetSkillsWithPriceCount(string type)
    {
        return _symbolsRepository.GetSkillsWithPriceCount(type);
    }

    public Symbols GetSymbolsById(string Id)
    {
        return _symbolsRepository.GetSymbolsById(Id);
    }

    public Symbols SumPowerSymbolsPercent()
    {
        return _symbolsRepository.SumPowerSymbolsPercent();
    }

    public List<string> GetUniqueSymbolsId()
    {
        return _symbolsRepository.GetUniqueSymbolsId();
    }
}
