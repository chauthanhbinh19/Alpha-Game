using System.Collections.Generic;

public interface IRelicsGalleryService
{
    List<Relics> GetRelicsCollection(string type, int pageSize, int offset, string rare);
    int GetRelicsCount(string type, string rare);
    void InsertRelicsGallery(string Id);
    void UpdateStatusRelicsGallery(string Id);
    Relics SumPowerRelicsGallery();
}