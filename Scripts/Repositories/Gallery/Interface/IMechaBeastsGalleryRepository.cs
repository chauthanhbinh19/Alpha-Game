using System.Collections.Generic;

public interface IMechaBeastsGalleryRepository
{
    List<MechaBeasts> GetMechaBeastsCollection(int pageSize, int offset, string rare);
    int GetMechaBeastsCount(string rare);
    void InsertMechaBeastsGallery(string Id, MechaBeasts TitleFromDB);
    void UpdateStatusMechaBeastsGallery(string Id);
    void UpdateStarMechaBeastsGallery(string Id, double star);
    void UpdateMechaBeastsGalleryPower(string Id, MechaBeasts TitleFromDB);
    MechaBeasts SumPowerMechaBeastsGallery();
}