using System.Collections.Generic;

public interface ISpiritBeastRepository
{
    List<string> GetUniqueSpiritBeastId();
    List<SpiritBeast> GetSpiritBeast(int pageSize, int offset, string rare);
    int GetSpiritBeastCount(string rare);
    List<SpiritBeast> GetSpiritBeastWithPrice(int pageSize, int offset);
    int GetSpiritBeastWithPriceCount();
    SpiritBeast GetSpiritBeastById(string Id);
    SpiritBeast SumPowerSpiritBeastPercent();
}
