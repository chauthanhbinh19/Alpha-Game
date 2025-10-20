using System.Collections.Generic;

public interface IPuppetGalleryService
{
    List<Puppets> GetPuppetCollection(string type, int pageSize, int offset, string rare);
    int GetPuppetCount(string type, string rare);
    void InsertPuppetGallery(string Id);
    void UpdateStatusPuppetGallery(string Id);
    void UpdateStarPuppetGallery(string Id, double star);
    void UpdatePuppetGalleryPower(string Id);
    Puppets SumPowerPuppetGallery();
}