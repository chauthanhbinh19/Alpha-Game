using System.Collections.Generic;

public interface ITalismanGalleryRepository
{
    List<Talisman> GetTalismanCollection(string type, int pageSize, int offset, string rare);
    int GetTalismanCount(string type, string rare);
    void InsertTalismanGallery(string Id, Talisman TalismanFromDB);
    void UpdateStatusTalismanGallery(string Id);
    Talisman SumPowerTalismanGallery();
}