using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardSoldiersRepository
{
    Task<List<string>> GetUniqueCardSoldiersTypesAsync();
    Task<List<string>> GetUniqueCardSoldiersIdAsync();
    Task<List<CardSoldiers>> GetCardSoldiersAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<CardSoldiers>> GetCardSoldiersWithoutLimitAsync();
    Task<int> GetCardSoldiersCountAsync(string search, string type, string rare);    
    Task<List<CardSoldiers>> GetCardSoldiersRandomAsync(string type, int pageSize);
    Task<List<CardSoldiers>> GetAllCardSoldiersAsync(string type);
    Task<CardSoldiers> GetCardSoldierByIdAsync(string Id);
    Task<List<CardSoldiers>> GetCardSoldiersWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetCardSoldiersWithPriceCountAsync(string type);
}