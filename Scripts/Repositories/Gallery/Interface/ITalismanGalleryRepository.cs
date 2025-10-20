using System.Collections.Generic;

public interface ITalismanGalleryRepository
{
    List<Talismans> GetTalismanCollection(string type, int pageSize, int offset, string rare);
    int GetTalismanCount(string type, string rare);
    void InsertTalismanGallery(string Id, Talismans TalismanFromDB);
    void UpdateStatusTalismanGallery(string Id);
    void UpdateStarTalismanGallery(string Id, double star);
    void UpdateTalismanGalleryPower(string Id, Talismans TalismanFromDB);
    Talismans SumPowerTalismanGallery();
}