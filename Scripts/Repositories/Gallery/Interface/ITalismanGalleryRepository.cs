using System.Collections.Generic;

public interface ITalismanGalleryRepository
{
    List<Talisman> GetTalismanCollection(string type, int pageSize, int offset);
    int GetTalismanCount(string type);
    void InsertTalismanGallery(string Id, Talisman TalismanFromDB);
    void UpdateStatusTalismanGallery(string Id);
    Talisman SumPowerTalismanGallery();
}