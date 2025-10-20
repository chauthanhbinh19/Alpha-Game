using System.Collections.Generic;

public interface ISpiritBeastGalleryRepository
{
    List<SpiritBeasts> GetSpiritBeastCollection(int pageSize, int offset, string rare);
    int GetSpiritBeastCount(string rare);
    void InsertSpiritBeastGallery(string Id, SpiritBeasts TitleFromDB);
    void UpdateStatusSpiritBeastGallery(string Id);
    void UpdateStarSpiritBeastGallery(string Id, double star);
    void UpdateSpiritBeastGalleryPower(string Id, SpiritBeasts SpiritBeastFromDB);
    SpiritBeasts SumPowerSpiritBeastGallery();
}