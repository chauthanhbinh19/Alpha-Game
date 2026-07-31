using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserFurnituresRepository
{
    Task<List<Furnitures>> GetUserFurnituresAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetUserFurnituresCountAsync(string userId, string search, string type, string rare);
    Task<InsertOrUpdateResult<Furnitures>> InsertOrUpdateUserFurnitureAsync(string userId, Furnitures furniture);
    Task<InsertOrUpdateResult<BatchOperationResultDTO<Furnitures>>> InsertOrUpdateUserFurnituresBatchAsync(string userId, List<Furnitures> furnitures);
    Task<InsertOrUpdateResult<bool>> UpdateUserFurnitureLevelAsync(string userId, Furnitures furniture);
    Task<InsertOrUpdateResult<bool>> UpdateUserFurnitureStarAsync(string userId, Furnitures furniture);
    Task<Furnitures> GetUserFurnitureByIdAsync(string userId, string Id);
    Task<Furnitures> SumPowerUserFurnituresAsync(string userId);
}