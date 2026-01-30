using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFurnituresRepository
{
    Task<List<string>> GetUniqueFurnituresTypesAsync();
    Task<List<string>> GetUniqueFurnituresIdAsync();
    Task<List<Furnitures>> GetFurnituresAsync(string search, string type, string rare, int pageSize, int offset);
    Task<int> GetFurnituresCountAsync(string search, string type, string rare);
    Task<List<Furnitures>> GetFurnituresWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetFurnituresWithPriceCountAsync(string type);
    Task<Furnitures> GetFurnitureByIdAsync(string Id);
    Task<Furnitures> SumPowerFurnituresPercentAsync();
}
