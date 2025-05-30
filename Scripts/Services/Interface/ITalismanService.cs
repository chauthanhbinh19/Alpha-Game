using System.Collections.Generic;

public interface ITalismanService
{
    List<string> GetUniqueTalismanTypes();
    List<Talisman> GetTalisman(string type, int pageSize, int offset);
    int GetTalismanCount(string type);
    List<Talisman> GetTalismanWithPrice(string type, int pageSize, int offset);
    int GetTalismanWithPriceCount(string type);
    Talisman GetTalismanById(string Id);
    Talisman SumPowerTalismanPercent();
}
