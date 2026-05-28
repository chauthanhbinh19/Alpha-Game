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

    public async Task<List<Furnitures>> GetUserFurnituresAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Furnitures> list = await _userFurnituresRepository.GetUserFurnituresAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserFurnituresCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userFurnituresRepository.GetUserFurnituresCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserFurnitureAsync(Furnitures furniture, string userId)
    {
        return await _userFurnituresRepository.InsertUserFurnitureAsync(furniture, userId);
    }

    public async Task<bool> UpdateFurnitureLevelAsync(Furnitures furniture, int level)
    {
        return await _userFurnituresRepository.UpdateFurnitureLevelAsync(furniture, level);
    }

    public async Task<bool> UpdateFurnitureBreakthroughAsync(Furnitures furniture, int star, double quantity)
    {
        return await _userFurnituresRepository.UpdateFurnitureBreakthroughAsync(furniture, star, quantity);
    }

    public async Task<Furnitures> GetUserFurnitureByIdAsync(string user_id, string Id)
    {
        return await _userFurnituresRepository.GetUserFurnitureByIdAsync(user_id, Id);
    }

    public async Task<Furnitures> SumPowerUserFurnituresAsync()
    {
        return await _userFurnituresRepository.SumPowerUserFurnituresAsync();
    }

    public async Task<bool> InsertOrUpdateUserFurnituresBatchAsync(List<Furnitures> furnitures)
    {
        return await _userFurnituresRepository.InsertOrUpdateUserFurnituresBatchAsync(furnitures);
    }
}
