using System.Collections.Generic;

public interface IForgeGalleryService
{
    List<Forges> GetForgeCollection(string type, int pageSize, int offset, string rare);
    int GetForgeCount(string type, string rare);
    void InsertForgeGallery(string Id);
    void UpdateStatusForgeGallery(string Id);
    void UpdateStarForgeGallery(string Id, double star);
    void UpdateForgeGalleryPower(string Id);
    Forges SumPowerForgeGallery();
}