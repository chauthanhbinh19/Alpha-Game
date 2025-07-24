using System.Collections.Generic;

public interface ICardLifeGalleryService
{
    List<CardLife> GetCardLifeCollection(string type, int pageSize, int offset, string rare);
    int GetCardLifeCount(string type, string rare);
    void InsertCardLifeGallery(string Id);
    void UpdateStatusCardLifeGallery(string Id);
    CardLife SumPowerCardLifeGallery();
}