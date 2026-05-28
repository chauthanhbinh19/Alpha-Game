using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserFurnituresRepository
{
    Task<List<Furnitures>> GetUserFurnituresAsync(string user_id, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserFurnituresCountAsync(string user_id, string search, string type, string rare);
    Task<bool> InsertUserFurnitureAsync(Furnitures furniture, string userId);
    Task<bool> InsertOrUpdateUserFurnituresBatchAsync(List<Furnitures> furnitures);
    Task<bool> UpdateFurnitureLevelAsync(Furnitures furniture, int level);
    Task<bool> UpdateFurnitureBreakthroughAsync(Furnitures furniture, int star, double quantity);
    Task<Furnitures> GetUserFurnitureByIdAsync(string user_id, string Id);
    Task<Furnitures> SumPowerUserFurnituresAsync();
}