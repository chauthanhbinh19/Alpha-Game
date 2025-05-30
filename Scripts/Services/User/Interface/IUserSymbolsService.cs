using System.Collections.Generic;

public interface IUserSymbolsService
{
    Symbols GetNewLevelPower(Symbols c, double coefficient);
    Symbols GetNewBreakthroughPower(Symbols c, double coefficient);
    List<Symbols> GetUserSymbols(string user_id, string type, int pageSize, int offset);
    int GetUserSymbolsCount(string user_id, string type);
    bool InsertUserSymbols(Symbols symbols);
    bool UpdateSymbolsLevel(Symbols symbols, int cardLevel);
    bool UpdateSymbolsBreakthrough(Symbols symbols, int star, int quantity);
    Symbols GetUserSymbolsById(string user_id, string Id);
    Symbols SumPowerUserSymbols();
}