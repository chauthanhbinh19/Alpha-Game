using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardGeneralsRepository
{
    Task<List<string>> GetUniqueCardGeneralsTypesAsync();
    Task<List<string>> GetUniqueCardGeneralsIdAsync();
    Task<List<CardGenerals>> GetCardGeneralsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<List<CardGenerals>> GetCardGeneralsWithoutLimitAsync();
    Task<int> GetCardGeneralsCountAsync(string search, string type, string rare);
    Task<List<CardGenerals>> GetCardGeneralsRandomAsync(string type, int pageSize);
    Task<List<CardGenerals>> GetAllCardGeneralsAsync(string type);
    Task<CardGenerals> GetCardGeneralByIdAsync(string Id);
    Task<List<CardGenerals>> GetCardGeneralsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetCardGeneralsWithPriceCountAsync(string type);
}