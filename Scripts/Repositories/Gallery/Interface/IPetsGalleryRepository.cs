using System.Collections.Generic;

public interface IPetsGalleryRepository
{
    List<Pets> GetPetsCollection(string type, int pageSize, int offset);
    int GetPetsCount(string type);
    void InsertPetsGallery(string Id, Pets petFromDB);
    void UpdateStatusPetsGallery(string Id);
    Pets SumPowerPetsGallery();
}