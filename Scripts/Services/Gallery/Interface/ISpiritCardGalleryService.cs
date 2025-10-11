using System.Collections.Generic;

public interface ISpiritCardGalleryService
{
    List<SpiritCard> GetSpiritCardCollection(string type, int pageSize, int offset, string rare);
    int GetSpiritCardCount(string type, string rare);
    void InsertSpiritCardGallery(string Id);
    void UpdateStatusSpiritCardGallery(string Id);
    void UpdateStarSpiritCardGallery(string Id, double star);
    void UpdateSpiritCardGalleryPower(string Id);
    SpiritCard SumPowerSpiritCardGallery();
}