using System.Collections.Generic;

public interface IVehiclesRepository
{
    List<string> GetUniqueVehicleTypes();
    List<string> GetUniqueVehicleId();
    List<Vehicles> GetVehicles(string type, int pageSize, int offset, string rare);
    int GetVehicleCount(string type, string rare);
    List<Vehicles> GetVehicleWithPrice(string type, int pageSize, int offset);
    int GetVehicleWithPriceCount(string type);
    Vehicles GetVehicleById(string Id);
    Vehicles SumPowerVehiclePercent();
}
