using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardCaptainsService
{
    Task<List<string>> GetUniqueCardCaptainsTypesAsync();
    Task<List<string>> GetUniqueCardCaptainsIdAsync();
    Task<List<CardCaptains>> GetCardCaptainsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<CardCaptains>> GetCardCaptainsWithoutLimitAsync();
    Task<int> GetCardCaptainsCountAsync(string search, string type, string rare);
    Task<List<CardCaptains>> GetCardCaptainsRandomAsync(string type, int pageSize);
    Task<List<CardCaptains>> GetAllCardCaptainsAsync(string type);
    Task<CardCaptains> GetCardCaptainByIdAsync(string Id);
    Task<List<CardCaptains>> GetCardCaptainsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetCardCaptainsWithPriceCountAsync(string type);
}