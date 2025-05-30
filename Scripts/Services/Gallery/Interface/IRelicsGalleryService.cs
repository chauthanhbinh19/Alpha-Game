using System.Collections.Generic;

public interface IRelicsGalleryService
{
    List<Relics> GetRelicsCollection(string type, int pageSize, int offset);
    int GetRelicsCount(string type);
    void InsertRelicsGallery(string Id);
    void UpdateStatusRelicsGallery(string Id);
    Relics SumPowerRelicsGallery();
}