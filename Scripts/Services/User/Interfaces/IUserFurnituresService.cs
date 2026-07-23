using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserFurnituresService
{
    Task<List<Furnitures>> GetUserFurnituresAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserFurnituresCountAsync(string userId, string search, string type, string rare);
    Task<bool> InsertUserFurnitureAsync(Furnitures furniture, string userId);
    Task<bool> InsertOrUpdateUserFurnituresBatchAsync(string userId, List<Furnitures> furnitures);
    Task<bool> UpdateUserFurnitureLevelAsync(string userId, Furnitures furniture);
    Task<bool> UpdateUserFurnitureStarAsync(string userId, Furnitures furniture);
    Task<bool> UpdateUserFurnitureBreakthroughAsync(string userId, Furnitures furniture, int star, double quantity);
    Task<Furnitures> GetUserFurnitureByIdAsync(string userId, string Id);
    Task<Furnitures> SumPowerUserFurnituresAsync(string userId);
}