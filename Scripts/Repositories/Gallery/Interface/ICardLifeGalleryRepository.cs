using System.Collections.Generic;

public interface ICardLifeGalleryRepository
{
    List<CardLife> GetCardLifeCollection(string type, int pageSize, int offset, string rare);
    int GetCardLifeCount(string type, string rare);
    void InsertCardLifeGallery(string Id, CardLife CardLifeFromDB);
    void UpdateStatusCardLifeGallery(string Id);
    CardLife SumPowerCardLifeGallery();
}