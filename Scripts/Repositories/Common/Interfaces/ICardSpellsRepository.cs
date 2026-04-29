using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardSpellsRepository
{
    Task<List<string>> GetUniqueCardSpellsTypesAsync();
    Task<List<string>> GetUniqueCardSpellsIdAsync();
    Task<List<CardSpells>> GetCardSpellsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<int> GetCardSpellsCountAsync(string search, string type, string rare);
    Task<List<CardSpells>> GetCardSpellsRandomAsync(string type, int pageSize);
    Task<List<CardSpells>> GetAllCardSpellsAsync(string type);
    Task<CardSpells> GetCardSpellByIdAsync(string Id);
    Task<List<CardSpells>> GetCardSpellsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetCardSpellsWithPriceCountAsync(string type);
}
