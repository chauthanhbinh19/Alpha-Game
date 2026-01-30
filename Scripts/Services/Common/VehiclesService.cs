using System.Collections.Generic;
using System.Threading.Tasks;

public class VehiclesService : IVehiclesService
{
    private readonly IVehiclesRepository _VehiclesRepository;

    public VehiclesService(IVehiclesRepository VehiclesRepository)
    {
        _VehiclesRepository = VehiclesRepository;
    }

    public static VehiclesService Create()
    {
        return new VehiclesService(new VehiclesRepository());
    }

    public async Task<List<string>> GetUniqueVehiclesTypesAsync()
    {
        return await _VehiclesRepository.GetUniqueVehiclesTypesAsync();
    }

    public async Task<List<Vehicles>> GetVehiclesAsync(string search, string type, string rare, int pageSize, int offset)
    {
        List<Vehicles> list = await _VehiclesRepository.GetVehiclesAsync(search, type, rare, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetVehiclesCountAsync(string search, string type, string rare)
    {
        return await _VehiclesRepository.GetVehiclesCountAsync(search, type, rare);
    }

    public async Task<List<Vehicles>> GetVehiclesWithPriceAsync(string type, int pageSize, int offset)
    {
        List<Vehicles> list = await _VehiclesRepository.GetVehiclesWithPriceAsync(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public async Task<int> GetVehiclesWithPriceCountAsync(string type)
    {
        return await _VehiclesRepository.GetVehiclesWithPriceCountAsync(type);
    }

    public async Task<Vehicles> GetVehicleByIdAsync(string Id)
    {
        return await _VehiclesRepository.GetVehicleByIdAsync(Id);
    }

    public async Task<Vehicles> SumPowerVehiclesPercentAsync()
    {
        return await _VehiclesRepository.SumPowerVehiclesPercentAsync();
    }

    public async Task<List<string>> GetUniqueVehiclesIdAsync()
    {
        return await _VehiclesRepository.GetUniqueVehiclesIdAsync();
    }
}
