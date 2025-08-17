using System.Collections.Generic;

public interface ISpiritBeastService
{
    List<string> GetUniqueTitleId();
    List<SpiritBeast> GetSpiritBeast(int pageSize, int offset, string rare);
    int GetSpiritBeastCount(string rare);
    List<SpiritBeast> GetSpiritBeastWithPrice(int pageSize, int offset);
    int GetSpiritBeastWithPriceCount();
    SpiritBeast GetSpiritBeastById(string Id);
    SpiritBeast SumPowerSpiritBeastPercent();
}
