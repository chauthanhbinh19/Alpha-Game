using System.Collections.Generic;

public interface ISpiritCardService
{
    List<string> GetUniqueSpiritCardTypes();
    List<string> GetUniqueTitleId();
    List<SpiritCards> GetSpiritCard(string type, int pageSize, int offset, string rare);
    int GetSpiritCardCount(string type, string rare);
    List<SpiritCards> GetSpiritCardWithPrice(string type, int pageSize, int offset);
    int GetSpiritCardWithPriceCount(string type);
    SpiritCards GetSpiritCardById(string Id);
    SpiritCards SumPowerSpiritCardPercent();
}
