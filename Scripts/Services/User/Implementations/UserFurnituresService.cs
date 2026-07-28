using System.Collections.Generic;
using System.Threading.Tasks;

public class UserFurnituresService : IUserFurnituresService
{
    private static UserFurnituresService _instance;
    private readonly IUserFurnituresRepository _userFurnituresRepository;

    public UserFurnituresService(IUserFurnituresRepository userFurnituresRepository)
    {
        _userFurnituresRepository = userFurnituresRepository;
    }

    public static UserFurnituresService Create()
    {
        if (_instance == null)
        {
            _instance = new UserFurnituresService(new UserFurnituresRepository());
        }
        return _instance;
    }

    public async Task<List<Furnitures>> GetUserFurnituresAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Furnitures> list = await _userFurnituresRepository.GetUserFurnituresAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserFurnituresCountAsync(string userId, string search, string type, string rare)
    {
        return await _userFurnituresRepository.GetUserFurnituresCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserFurnitureAsync(Furnitures furniture, string userId)
    {
        var result = await _userFurnituresRepository.InsertUserFurnitureAsync(furniture, userId);
        if (result)
        {
            await FurnituresGalleryService.Create().InsertFurnitureGalleryAsync(userId, furniture.Id);
        }
        return result;
    }

    public async Task<bool> UpdateUserFurnitureLevelAsync(string userId, Furnitures furniture)
    {
        return await _userFurnituresRepository.UpdateUserFurnitureLevelAsync(userId, furniture);
    }

    public async Task<bool> UpdateUserFurnitureStarAsync(string userId, Furnitures furniture)
    {
        var result = await _userFurnituresRepository.UpdateUserFurnitureStarAsync(userId, furniture);
        if (result)
        {
            await FurnituresGalleryService.Create().UpdateStarFurnitureGalleryAsync(userId, furniture.Id, furniture.Star);
        }
        return result;
    }

    public async Task<bool> UpdateUserFurnitureBreakthroughAsync(string userId, Furnitures furniture, int star, double quantity)
    {
        return await _userFurnituresRepository.UpdateUserFurnitureBreakthroughAsync(userId, furniture, star, quantity);
    }

    public async Task<Furnitures> GetUserFurnitureByIdAsync(string userId, string Id)
    {
        return await _userFurnituresRepository.GetUserFurnitureByIdAsync(userId, Id);
    }

    public async Task<Furnitures> SumPowerUserFurnituresAsync(string userId)
    {
        return await _userFurnituresRepository.SumPowerUserFurnituresAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserFurnituresBatchAsync(string userId, List<Furnitures> furnitures)
    {
        return await _userFurnituresRepository.InsertOrUpdateUserFurnituresBatchAsync(userId, furnitures);
    }
}
