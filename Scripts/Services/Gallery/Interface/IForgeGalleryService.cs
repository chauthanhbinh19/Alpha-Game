using System.Collections.Generic;

public interface IForgeGalleryService
{
    List<Forge> GetForgeCollection(string type, int pageSize, int offset);
    int GetForgeCount(string type);
    void InsertForgeGallery(string Id);
    void UpdateStatusForgeGallery(string Id);
    Forge SumPowerForgeGallery();
}