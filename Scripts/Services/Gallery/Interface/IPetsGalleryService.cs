using System.Collections.Generic;

public interface IPetsGalleryService
{
    List<Pets> GetPetsCollection(string type, int pageSize, int offset);
    int GetPetsCount(string type);
    void InsertPetsGallery(string Id);
    void UpdateStatusPetsGallery(string Id);
    Pets SumPowerPetsGallery();
}