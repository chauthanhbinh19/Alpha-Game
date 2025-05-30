using System.Collections.Generic;

public interface ITalismanGalleryService
{
    List<Talisman> GetTalismanCollection(string type, int pageSize, int offset);
    int GetTalismanCount(string type);
    void InsertTalismanGallery(string Id);
    void UpdateStatusTalismanGallery(string Id);
    Talisman SumPowerTalismanGallery();
}