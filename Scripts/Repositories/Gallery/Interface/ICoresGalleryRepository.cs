using System.Collections.Generic;

public interface ICoresGalleryRepository
{
    List<Cores> GetCoresCollection(int pageSize, int offset, string rare);
    int GetCoresCount(string rare);
    void InsertCoresGallery(string Id, Cores TitleFromDB);
    void UpdateStatusCoresGallery(string Id);
    void UpdateStarCoresGallery(string Id, double star);
    void UpdateCoresGalleryPower(string Id, Cores TitleFromDB);
    Cores SumPowerCoresGallery();
}