using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardColonelsService
{
    Task<List<string>> GetUniqueCardColonelsTypesAsync();
    Task<List<string>> GetUniqueCardColonelsIdAsync();
    Task<List<CardColonels>> GetCardColonelsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<CardColonels>> GetCardColonelsWithoutLimitAsync();
    Task<int> GetCardColonelsCountAsync(string search, string type, string rare);
    Task<List<CardColonels>> GetCardColonelsRandomAsync(string type, int pageSize);
    Task<List<CardColonels>> GetAllCardColonelsAsync(string type);
    Task<CardColonels> GetCardColonelByIdAsync(string id);
    Task<List<CardColonels>> GetCardColonelsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetCardColonelsWithPriceCountAsync(string type);
}