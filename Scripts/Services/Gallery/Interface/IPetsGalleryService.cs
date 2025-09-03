using System.Collections.Generic;

public interface IPetsGalleryService
{
    List<Pets> GetPetsCollection(string type, int pageSize, int offset, string rare);
    int GetPetsCount(string type, string rare);
    void InsertPetsGallery(string Id);
    void UpdateStatusPetsGallery(string Id);
    void UpdateStarPetsGallery(string Id, double star);
    void UpdatePetsGalleryPower(string Id);
    Pets SumPowerPetsGallery();
}