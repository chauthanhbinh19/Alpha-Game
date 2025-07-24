using System.Collections.Generic;

public interface IMedalsGalleryRepository
{
    List<Medals> GetMedalsCollection(int pageSize, int offset, string rare);
    int GetMedalsCount(string rare);
    void InsertMedalsGallery(string Id, Medals MedalFromDB);
    void UpdateStatusMedalsGallery(string Id);
    Medals SumPowerMedalsGallery();
}