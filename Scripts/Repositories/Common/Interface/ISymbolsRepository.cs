using System.Collections.Generic;

public interface ISymbolsRepository
{
    List<string> GetUniqueSymbolsTypes();
    List<string> GetUniqueSymbolsId();
    List<Symbols> GetSymbols(string type, int pageSize, int offset, string rare);
    int GetSymbolsCount(string type, string rare);
    List<Symbols> GetSymbolsWithPrice(string type, int pageSize, int offset);
    int GetSkillsWithPriceCount(string type);
    Symbols GetSymbolsById(string Id);
    Symbols SumPowerSymbolsPercent();
}
