using System.Collections.Generic;

public interface IPuppetGalleryService
{
    List<Puppet> GetPuppetCollection(string type, int pageSize, int offset);
    int GetPuppetCount(string type);
    void InsertPuppetGallery(string Id);
    void UpdateStatusPuppetGallery(string Id);
    Puppet SumPowerPuppetGallery();
}