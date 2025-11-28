using System.Collections.Generic;

public interface IUserSymbolsRepository
{
    List<Symbols> GetUserSymbols(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserSymbolsCount(string user_id, string type, string rare);
    bool InsertUserSymbols(Symbols symbols, string userId);
    bool UpdateSymbolsLevel(Symbols symbols, int cardLevel);
    bool UpdateSymbolsBreakthrough(Symbols symbols, int star, double quantity);
    Symbols GetUserSymbolsById(string user_id, string id);
    Symbols SumPowerUserSymbols();
}