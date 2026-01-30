using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserFurnituresService
{
    Task<Furnitures> GetNewLevelPowerAsync(Furnitures c, double coefficient);
    Task<Furnitures> GetNewBreakthroughPowerAsync(Furnitures c, double coefficient);
    Task<List<Furnitures>> GetUserFurnituresAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserFurnituresCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserFurnitureAsync(Furnitures Furniture, string userId);
    Task<bool> UpdateFurnitureLevelAsync(Furnitures Furniture, int cardLevel);
    Task<bool> UpdateFurnitureBreakthroughAsync(Furnitures Furniture, int star, double quantity);
    Task<Furnitures> GetUserFurnitureByIdAsync(string user_id, string Id);
    Task<Furnitures> SumPowerUserFurnituresAsync();
}