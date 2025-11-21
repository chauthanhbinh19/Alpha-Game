using System.Collections.Generic;

public interface IWeaponsGalleryRepository
{
    List<Weapons> GetWeaponsCollection(int pageSize, int offset, string rare);
    int GetWeaponsCount(string rare);
    void InsertWeaponsGallery(string Id, Weapons TitleFromDB);
    void UpdateStatusWeaponsGallery(string Id);
    void UpdateStarWeaponsGallery(string Id, double star);
    void UpdateWeaponsGalleryPower(string Id, Weapons TitleFromDB);
    Weapons SumPowerWeaponsGallery();
}