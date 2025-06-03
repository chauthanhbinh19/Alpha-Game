using System.Collections.Generic;

public interface ISymbolsRepository
{
    List<string> GetUniqueSymbolsTypes();
    List<Symbols> GetSymbols(string type, int pageSize, int offset);
    int GetSymbolsCount(string type);
    List<Symbols> GetSymbolsWithPrice(string type, int pageSize, int offset);
    int GetSkillsWithPriceCount(string type);
    Symbols GetSymbolsById(string Id);
    Symbols SumPowerSymbolsPercent();
}
