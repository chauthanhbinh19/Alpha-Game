using System.Collections.Generic;

public interface ISpiritBeastGalleryRepository
{
    List<SpiritBeast> GetSpiritBeastCollection(int pageSize, int offset, string rare);
    int GetSpiritBeastCount(string rare);
    void InsertSpiritBeastGallery(string Id, SpiritBeast TitleFromDB);
    void UpdateStatusSpiritBeastGallery(string Id);
    SpiritBeast SumPowerSpiritBeastGallery();
}