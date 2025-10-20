using System.Collections.Generic;

public interface ICardLifeGalleryRepository
{
    List<CardLives> GetCardLifeCollection(string type, int pageSize, int offset, string rare);
    int GetCardLifeCount(string type, string rare);
    void InsertCardLifeGallery(string Id, CardLives CardLifeFromDB);
    void UpdateStatusCardLifeGallery(string Id);
    void UpdateStarCardLifeGallery(string Id, double star);
    void UpdateCardLifeGalleryPower(string Id, CardLives CardLifeFromDB);
    CardLives SumPowerCardLifeGallery();
}