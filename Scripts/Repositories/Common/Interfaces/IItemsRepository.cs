using System.Collections.Generic;
using System.Threading.Tasks;

public interface IItemsRepository
{
    Task<List<string>> GetUniqueItemsIdAsync();
    Task<List<string>> GetUniqueItemsTypesAsync();
    Task<List<Items>> GetItemsAsync();
}
