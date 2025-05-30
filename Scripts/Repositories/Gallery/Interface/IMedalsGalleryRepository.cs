using System.Collections.Generic;

public interface IMedalsGalleryRepository
{
    List<Medals> GetMedalsCollection(int pageSize, int offset);
    int GetMedalsCount();
    void InsertMedalsGallery(string Id, Medals MedalFromDB);
    void UpdateStatusMedalsGallery(string Id);
    Medals SumPowerMedalsGallery();
}