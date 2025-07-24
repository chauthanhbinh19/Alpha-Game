using System.Collections.Generic;

public interface IMedalsGalleryService
{
    List<Medals> GetMedalsCollection(int pageSize, int offset, string rare);
    int GetMedalsCount(string rare);
    void InsertMedalsGallery(string Id);
    void UpdateStatusMedalsGallery(string Id);
    Medals SumPowerMedalsGallery();
}