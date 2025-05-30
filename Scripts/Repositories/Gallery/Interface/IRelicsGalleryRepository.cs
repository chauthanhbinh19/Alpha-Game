using System.Collections.Generic;

public interface IRelicsGalleryRepository
{
    List<Relics> GetRelicsCollection(string type, int pageSize, int offset);
    int GetRelicsCount(string type);
    void InsertRelicsGallery(string Id, Relics relicFromDB);
    void UpdateStatusRelicsGallery(string Id);
    Relics SumPowerRelicsGallery();
}