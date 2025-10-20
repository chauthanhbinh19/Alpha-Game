using System.Collections.Generic;

public interface ISpiritBeastService
{
    List<string> GetUniqueTitleId();
    List<SpiritBeasts> GetSpiritBeast(int pageSize, int offset, string rare);
    int GetSpiritBeastCount(string rare);
    List<SpiritBeasts> GetSpiritBeastWithPrice(int pageSize, int offset);
    int GetSpiritBeastWithPriceCount();
    SpiritBeasts GetSpiritBeastById(string Id);
    SpiritBeasts SumPowerSpiritBeastPercent();
}
