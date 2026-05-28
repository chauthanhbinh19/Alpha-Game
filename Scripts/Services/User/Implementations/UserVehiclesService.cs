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

    public async Task<List<Vehicles>> GetUserVehiclesAsync(string user_id, string search, string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> list = await _userVehiclesRepository.GetUserVehiclesAsync(user_id, search, type, pageSize, offset, rare);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        ListSortHelper.SortByPower(list);
        return list;
    }

    public async Task<int> GetUserVehiclesCountAsync(string user_id, string search, string type, string rare)
    {
        return await _userVehiclesRepository.GetUserVehiclesCountAsync(user_id, search, type, rare);
    }

    public async Task<bool> InsertUserVehicleAsync(Vehicles vehicle, string userId)
    {
        return await _userVehiclesRepository.InsertUserVehicleAsync(vehicle, userId);
    }

    public async Task<bool> UpdateVehicleLevelAsync(Vehicles vehicle, int level)
    {
        return await _userVehiclesRepository.UpdateVehicleLevelAsync(vehicle, level);
    }

    public async Task<bool> UpdateVehicleBreakthroughAsync(Vehicles vehicle, int star, double quantity)
    {
        return await _userVehiclesRepository.UpdateVehicleBreakthroughAsync(vehicle, star, quantity);
    }

    public async Task<Vehicles> GetUserVehicleByIdAsync(string user_id, string Id)
    {
        return await _userVehiclesRepository.GetUserVehicleByIdAsync(user_id, Id);
    }

    public async Task<Vehicles> SumPowerUserVehiclesAsync()
    {
        return await _userVehiclesRepository.SumPowerUserVehiclesAsync();
    }

    public async Task<bool> InsertOrUpdateUserVehiclesBatchAsync(List<Vehicles> vehicles)
    {
        return await _userVehiclesRepository.InsertOrUpdateUserVehiclesBatchAsync(vehicles);
    }
}
