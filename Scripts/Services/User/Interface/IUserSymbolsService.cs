using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserSymbolsService
{
    Task<Symbols> GetNewLevelPowerAsync(Symbols c, double coefficient);
    Task<Symbols> GetNewBreakthroughPowerAsync(Symbols c, double coefficient);
    Task<List<Symbols>> GetUserSymbolsAsync(string user_id, string type, int pageSize, int offset, string rare);
    Task<int> GetUserSymbolsCountAsync(string user_id, string type, string rare);
    Task<bool> InsertUserSymbolAsync(Symbols Symbol, string userId);
    Task<bool> UpdateSymbolLevelAsync(Symbols Symbol, int cardLevel);
    Task<bool> UpdateSymbolBreakthroughAsync(Symbols Symbol, int star, double quantity);
    Task<Symbols> GetUserSymbolByIdAsync(string user_id, string Id);
    Task<Symbols> SumPowerUserSymbolsAsync();
}