using System.Collections.Generic;

public interface ITalismanRepository
{
    List<string> GetUniqueTalismanTypes();
    List<string> GetUniqueTalismanId();
    List<Talismans> GetTalisman(string type, int pageSize, int offset, string rare);
    int GetTalismanCount(string type, string rare);
    List<Talismans> GetTalismanWithPrice(string type, int pageSize, int offset);
    int GetTalismanWithPriceCount(string type);
    Talismans GetTalismanById(string Id);
    Talismans SumPowerTalismanPercent();
}
