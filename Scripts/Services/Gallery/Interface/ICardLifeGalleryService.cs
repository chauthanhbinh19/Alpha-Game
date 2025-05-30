using System.Collections.Generic;

public interface ICardLifeGalleryService
{
    List<CardLife> GetCardLifeCollection(string type, int pageSize, int offset);
    int GetCardLifeCount(string type);
    void InsertCardLifeGallery(string Id);
    void UpdateStatusCardLifeGallery(string Id);
    CardLife SumPowerCardLifeGallery();
}