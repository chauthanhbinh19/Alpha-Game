using System.Collections.Generic;

public interface IPuppetGalleryRepository
{
    List<Puppet> GetPuppetCollection(string type, int pageSize, int offset, string rare);
    int GetPuppetCount(string type, string rare);
    void InsertPuppetGallery(string Id, Puppet PuppetFromDB);
    void UpdateStatusPuppetGallery(string Id);
    Puppet SumPowerPuppetGallery();
}