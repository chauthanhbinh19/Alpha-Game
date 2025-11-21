using System.Collections.Generic;

public interface ICoresService
{
    List<string> GetUniqueCoreId();
    List<Cores> GetCores(int pageSize, int offset, string rare);
    int GetCoresCount(string rare);
    List<Cores> GetCoresWithPrice(int pageSize, int offset);
    int GetCoresWithPriceCount();
    Cores GetCoresById(string Id);
    Cores SumPowerCoresPercent();
}
