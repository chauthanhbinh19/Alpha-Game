using System.Collections.Generic;
using System.Threading.Tasks;

public interface IPetsRepository
{
    Task<List<string>> GetUniquePetsTypesAsync();
    Task<List<string>> GetUniquePetsIdAsync();
    Task<List<Pets>> GetPetsAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetPetsCountAsync(string type, string rare);
    Task<List<Pets>> GetPetsWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetPetsWithPriceCountAsync(string type);
    Task<Pets> GetPetByIdAsync(string Id);
}
