using System.Collections.Generic;

public interface ITalismanService
{
    List<string> GetUniqueTalismanTypes();
    List<string> GetUniqueTalismanId();
    List<Talisman> GetTalisman(string type, int pageSize, int offset, string rare);
    int GetTalismanCount(string type, string rare);
    List<Talisman> GetTalismanWithPrice(string type, int pageSize, int offset);
    int GetTalismanWithPriceCount(string type);
    Talisman GetTalismanById(string Id);
    Talisman SumPowerTalismanPercent();
}
