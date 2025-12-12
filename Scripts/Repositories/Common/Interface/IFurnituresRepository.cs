using System.Collections.Generic;
using System.Threading.Tasks;

public interface IFurnituresRepository
{
    Task<List<string>> GetUniqueFurnituresTypesAsync();
    Task<List<string>> GetUniqueFurnituresIdAsync();
    Task<List<Furnitures>> GetFurnituresAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetFurnituresCountAsync(string type, string rare);
    Task<List<Furnitures>> GetFurnituresWithPriceAsync(string type, int pageSize, int offset);
    Task<int> GetFurnituresWithPriceCountAsync(string type);
    Task<Furnitures> GetFurnitureByIdAsync(string Id);
    Task<Furnitures> SumPowerFurnituresPercentAsync();
}
