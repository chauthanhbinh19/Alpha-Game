using System.Collections.Generic;
using System.Threading.Tasks;

public class UserVehiclesService : IUserVehiclesService
{
    private static UserVehiclesService _instance;
    private readonly IUserVehiclesRepository _userVehiclesRepository;

    public UserVehiclesService(IUserVehiclesRepository userVehiclesRepository)
    {
        _userVehiclesRepository = userVehiclesRepository;
    }

    public static UserVehiclesService Create()
    {
        if (_instance == null)
        {
            _instance = new UserVehiclesService(new UserVehiclesRepository());
        }
        return _instance;
    }

    public async Task<List<Vehicles>> GetUserVehiclesAsync(string userId, string search, string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> list = await _userVehiclesRepository.GetUserVehiclesAsync(userId, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserVehiclesCountAsync(string userId, string search, string type, string rare)
    {
        return await _userVehiclesRepository.GetUserVehiclesCountAsync(userId, search, type, rare);
    }

    public async Task<bool> InsertUserVehicleAsync(Vehicles vehicle, string userId)
    {
        return await _userVehiclesRepository.InsertUserVehicleAsync(vehicle, userId);
    }

    public async Task<bool> UpdateUserVehicleLevelAsync(string userId, Vehicles vehicle)
    {
        return await _userVehiclesRepository.UpdateUserVehicleLevelAsync(userId, vehicle);
    }

    public async Task<bool> UpdateUserVehicleStarAsync(string userId, Vehicles vehicle)
    {
        return await _userVehiclesRepository.UpdateUserVehicleStarAsync(userId, vehicle);
    }

    public async Task<bool> UpdateUserVehicleBreakthroughAsync(string userId, Vehicles vehicle, int star, double quantity)
    {
        return await _userVehiclesRepository.UpdateUserVehicleBreakthroughAsync(userId, vehicle, star, quantity);
    }

    public async Task<Vehicles> GetUserVehicleByIdAsync(string userId, string Id)
    {
        return await _userVehiclesRepository.GetUserVehicleByIdAsync(userId, Id);
    }

    public async Task<Vehicles> SumPowerUserVehiclesAsync(string userId)
    {
        return await _userVehiclesRepository.SumPowerUserVehiclesAsync(userId);
    }

    public async Task<bool> InsertOrUpdateUserVehiclesBatchAsync(string userId, List<Vehicles> vehicles)
    {
        return await _userVehiclesRepository.InsertOrUpdateUserVehiclesBatchAsync(userId, vehicles);
    }
}
