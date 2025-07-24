using System.Collections.Generic;

public interface IPetsGalleryRepository
{
    List<Pets> GetPetsCollection(string type, int pageSize, int offset, string rare);
    int GetPetsCount(string type, string rare);
    void InsertPetsGallery(string Id, Pets petFromDB);
    void UpdateStatusPetsGallery(string Id);
    Pets SumPowerPetsGallery();
}