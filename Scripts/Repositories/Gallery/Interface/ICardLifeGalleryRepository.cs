using System.Collections.Generic;

public interface ICardLifeGalleryRepository
{
    List<CardLife> GetCardLifeCollection(string type, int pageSize, int offset);
    int GetCardLifeCount(string type);
    void InsertCardLifeGallery(string Id, CardLife CardLifeFromDB);
    void UpdateStatusCardLifeGallery(string Id);
    CardLife SumPowerCardLifeGallery();
}