using System.Collections.Generic;

public interface IMedalsGalleryService
{
    List<Medals> GetMedalsCollection(int pageSize, int offset);
    int GetMedalsCount();
    void InsertMedalsGallery(string Id);
    void UpdateStatusMedalsGallery(string Id);
    Medals SumPowerMedalsGallery();
}