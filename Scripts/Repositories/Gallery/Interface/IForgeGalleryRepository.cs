using System.Collections.Generic;

public interface IForgeGalleryRepository
{
    List<Forge> GetForgeCollection(string type, int pageSize, int offset);
    int GetForgeCount(string type);
    void InsertForgeGallery(string Id, Forge ForgeFromDB);
    void UpdateStatusForgeGallery(string Id);
    Forge SumPowerForgeGallery();
}