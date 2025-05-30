using System.Collections.Generic;

public interface IMedalsService
{
    List<Medals> GetMedals(int pageSize, int offset);
    int GetMedalsCount();
    List<Medals> GetMedalsWithPrice(int pageSize, int offset);
    int GetMedalsWithPriceCount();
    Medals GetMedalsById(string Id);
    Medals SumPowerMedalsPercent();
}
