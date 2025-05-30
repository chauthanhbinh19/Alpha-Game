using System.Collections.Generic;

public interface IPuppetGalleryRepository
{
    List<Puppet> GetPuppetCollection(string type, int pageSize, int offset);
    int GetPuppetCount(string type);
    void InsertPuppetGallery(string Id, Puppet PuppetFromDB);
    void UpdateStatusPuppetGallery(string Id);
    Puppet SumPowerPuppetGallery();
}