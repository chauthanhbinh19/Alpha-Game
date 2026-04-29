using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPetsRepository
{
    Task<List<string>> GetUniquePetsTypesAsync();
    Task<List<string>> GetUniquePetsIdAsync();
    Task<List<Pets>> GetPetsAsync(string search, string type, string rare, int pageSize, int offset);
    Task<int> GetPetsCountAsync(string search, string type, string rare);
    Task<List<Pets>> GetPetsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetPetsWithPriceCountAsync(string type);
    Task<Pets> GetPetByIdAsync(string Id);
}
