using System.Collections.Generic;

public interface IUserVehicleService
{
    Vehicles GetNewLevelPower(Vehicles c, double coefficient);
    Vehicles GetNewBreakthroughPower(Vehicles c, double coefficient);
    List<Vehicles> GetUserVehicle(string user_id, string type, int pageSize, int offset, string rare);
    int GetUserVehicleCount(string user_id, string type, string rare);
    bool InsertUserVehicle(Vehicles Vehicle);
    bool UpdateVehicleLevel(Vehicles Vehicle, int cardLevel);
    bool UpdateVehicleBreakthrough(Vehicles Vehicle, int star, double quantity);
    Vehicles GetUserVehicleById(string user_id, string Id);
    Vehicles SumPowerUserVehicle();
}