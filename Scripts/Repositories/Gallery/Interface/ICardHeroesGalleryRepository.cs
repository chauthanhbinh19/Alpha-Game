using System.Collections.Generic;

public interface ICardHeroesGalleryRepository
{ 
    List<CardHeroes> GetCardHeroesCollection(string type, int pageSize, int offset);
    int GetCardHeroesCount(string type);
    void InsertCardHeroesGallery(string Id, CardHeroes CardFromDB);
    void UpdateStatusCardHeroesGallery(string Id);
    CardHeroes SumPowerCardHeroesGallery();
}