using System.Collections.Generic;

public interface IRunesService
{
    List<string> GetUniqueRuneId();
    List<Runes> GetRunes(int pageSize, int offset, string rare);
    int GetRunesCount(string rare);
    List<Runes> GetRunesWithPrice(int pageSize, int offset);
    int GetRunesWithPriceCount();
    Runes GetRunesById(string Id);
    Runes SumPowerRunesPercent();
}
