using System.Collections.Generic;

public interface ISpiritCardGalleryRepository
{
    List<SpiritCards> GetSpiritCardCollection(string type, int pageSize, int offset, string rare);
    int GetSpiritCardCount(string type, string rare);
    void InsertSpiritCardGallery(string Id, SpiritCards TitleFromDB);
    void UpdateStatusSpiritCardGallery(string Id);
    void UpdateStarSpiritCardGallery(string Id, double star);
    void UpdateSpiritCardGalleryPower(string Id, SpiritCards SpiritCardFromDB);
    SpiritCards SumPowerSpiritCardGallery();
}