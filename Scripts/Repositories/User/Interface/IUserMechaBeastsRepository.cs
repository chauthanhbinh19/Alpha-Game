using System.Collections.Generic;

public interface IUserMechaBeastsRepository
{
    List<MechaBeasts> GetUserMechaBeasts(string user_id, int pageSize, int offset, string rare);
    int GetUserMechaBeastsCount(string user_id, string rare);
    bool InsertUserMechaBeasts(MechaBeasts MechaBeasts, string userId);
    bool UpdateMechaBeastsLevel(MechaBeasts MechaBeasts, int cardLevel);
    bool UpdateMechaBeastsBreakthrough(MechaBeasts MechaBeasts, int star, double quantity);
    MechaBeasts GetUserMechaBeastsById(string user_id, string Id);
    MechaBeasts SumPowerUserMechaBeasts();
}