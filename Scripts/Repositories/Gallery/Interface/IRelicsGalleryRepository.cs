using System.Collections.Generic;

public interface IRelicsGalleryRepository
{
    List<Relics> GetRelicsCollection(string type, int pageSize, int offset, string rare);
    int GetRelicsCount(string type, string rare);
    void InsertRelicsGallery(string Id, Relics relicFromDB);
    void UpdateStatusRelicsGallery(string Id);
    void UpdateStarRelicsGallery(string Id, double star);
    void UpdateRelicsGalleryPower(string Id, Relics RelicFromDB);
    Relics SumPowerRelicsGallery();
}