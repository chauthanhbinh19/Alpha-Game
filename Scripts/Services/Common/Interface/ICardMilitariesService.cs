using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMilitariesService
{
    Task<List<string>> GetUniqueCardMilitariesTypesAsync();
    Task<List<string>> GetUniqueCardMilitariesIdAsync();
    Task<List<CardMilitaries>> GetCardMilitariesAsync(string search, string type, string rare, int pageSize, int offset);
    Task<int> GetCardMilitariesCountAsync(string search, string type, string rare);
    Task<List<CardMilitaries>> GetCardMilitariesRandomAsync(string type, int pageSize);
    Task<List<CardMilitaries>> GetAllCardMilitariesAsync(string type);
    Task<CardMilitaries> GetCardMilitaryByIdAsync(string Id);
    Task<List<CardMilitaries>> GetCardMilitariesWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetCardMilitariesWithPriceCountAsync(string type);
}