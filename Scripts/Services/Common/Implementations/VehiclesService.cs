using System.Collections.Generic;
using System.Threading.Tasks;

public class VehiclesService : IVehiclesService
{
    private readonly IVehiclesRepository _vehiclesRepository;

    public VehiclesService(IVehiclesRepository vehiclesRepository)
    {
        _vehiclesRepository = vehiclesRepository;
    }

    public static IVehiclesService Create() => ServiceContainer.GetService<IVehiclesService>();

    public async Task<List<string>> GetUniqueVehiclesTypesAsync()
    {
        return await _vehiclesRepository.GetUniqueVehiclesTypesAsync();
    }

    public async Task<List<Vehicles>> GetVehiclesAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Vehicles> list = await _vehiclesRepository.GetVehiclesAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetVehiclesCountAsync(string search, string type, string rare)
    {
        return await _vehiclesRepository.GetVehiclesCountAsync(search, type, rare);
    }

    public async Task<List<Vehicles>> GetVehiclesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Vehicles> list = await _vehiclesRepository.GetVehiclesWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluatorHelper.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetVehiclesWithPriceCountAsync(string type)
    {
        return await _vehiclesRepository.GetVehiclesWithPriceCountAsync(type);
    }

    public async Task<Vehicles> GetVehicleByIdAsync(string Id)
    {
        return await _vehiclesRepository.GetVehicleByIdAsync(Id);
    }

    public async Task<Vehicles> SumPowerVehiclesPercentAsync(string userId)
    {
        return await _vehiclesRepository.SumPowerVehiclesPercentAsync(userId);
    }

    public async Task<List<string>> GetUniqueVehiclesIdAsync()
    {
        return await _vehiclesRepository.GetUniqueVehiclesIdAsync();
    }

    public async Task<List<Vehicles>> GetVehiclesWithoutLimitAsync()
    {
        return await _vehiclesRepository.GetVehiclesWithoutLimitAsync();
    }
}
