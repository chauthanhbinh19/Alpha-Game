using System.Collections.Generic;

public interface IForgeGalleryRepository
{
    List<Forge> GetForgeCollection(string type, int pageSize, int offset, string rare);
    int GetForgeCount(string type, string rare);
    void InsertForgeGallery(string Id, Forge ForgeFromDB);
    void UpdateStatusForgeGallery(string Id);
    Forge SumPowerForgeGallery();
}