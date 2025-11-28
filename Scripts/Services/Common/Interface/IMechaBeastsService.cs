using System.Collections.Generic;

public interface IMechaBeastsService
{
    List<string> GetUniqueMechaBeastId();
    List<MechaBeasts> GetMechaBeasts(int pageSize, int offset, string rare);
    int GetMechaBeastsCount(string rare);
    List<MechaBeasts> GetMechaBeastsWithPrice(int pageSize, int offset);
    int GetMechaBeastsWithPriceCount();
    MechaBeasts GetMechaBeastsById(string Id);
    MechaBeasts SumPowerMechaBeastsPercent();
}
