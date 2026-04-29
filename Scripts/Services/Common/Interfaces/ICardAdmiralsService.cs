using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardAdmiralsService
{
    Task<List<string>> GetUniqueCardAdmiralsTypesAsync();
    Task<List<string>> GetUniqueCardAdmiralsIdAsync();
    Task<List<CardAdmirals>> GetCardAdmiralsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<int> GetCardAdmiralsCountAsync(string search, string type, string rare);    
    Task<List<CardAdmirals>> GetCardAdmiralsRandomAsync(string type, int pageSize);
    Task<List<CardAdmirals>> GetAllCardAdmiralsAsync(string type);
    Task<CardAdmirals> GetCardAdmiralByIdAsync(string Id);
    Task<List<CardAdmirals>> GetCardAdmiralsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetCardAdmiralsWithPriceCountAsync(string type);
}