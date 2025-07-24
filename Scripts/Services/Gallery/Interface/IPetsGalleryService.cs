using System.Collections.Generic;

public interface IPetsGalleryService
{
    List<Pets> GetPetsCollection(string type, int pageSize, int offset, string rare);
    int GetPetsCount(string type, string rare);
    void InsertPetsGallery(string Id);
    void UpdateStatusPetsGallery(string Id);
    Pets SumPowerPetsGallery();
}