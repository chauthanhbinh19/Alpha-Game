using System.Collections.Generic;

public interface IWeaponsGalleryService
{
    List<Weapons> GetWeaponsCollection(int pageSize, int offset, string rare);
    int GetWeaponsCount(string rare);
    void InsertWeaponsGallery(string Id);
    void UpdateStatusWeaponsGallery(string Id);
    void UpdateStarWeaponsGallery(string Id, double star);
    void UpdateWeaponsGalleryPower(string Id);
    Weapons SumPowerWeaponsGallery();
}