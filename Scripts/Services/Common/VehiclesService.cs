using System.Collections.Generic;

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

    public List<string> GetUniqueVehicleTypes()
    {
        return _VehiclesRepository.GetUniqueVehicleTypes();
    }

    public List<Vehicles> GetVehicles(string type, int pageSize, int offset, string rare)
    {
        List<Vehicles> list = _VehiclesRepository.GetVehicles(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetVehicleCount(string type, string rare)
    {
        return _VehiclesRepository.GetVehicleCount(type, rare);
    }

    public List<Vehicles> GetVehiclesWithPrice(string type, int pageSize, int offset)
    {
        List<Vehicles> list = _VehiclesRepository.GetVehicleWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetVehicleWithPriceCount(string type)
    {
        return _VehiclesRepository.GetVehicleWithPriceCount(type);
    }

    public Vehicles GetVehicleById(string Id)
    {
        return _VehiclesRepository.GetVehicleById(Id);
    }

    public Vehicles SumPowerVehiclePercent()
    {
        return _VehiclesRepository.SumPowerVehiclePercent();
    }

    public List<string> GetUniqueVehicleId()
    {
        return _VehiclesRepository.GetUniqueVehicleId();
    }
}
