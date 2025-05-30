using System.Collections.Generic;

public interface ICardHeroesGalleryService
{ 
    List<CardHeroes> GetCardHeroesCollection(string type, int pageSize, int offset);
    int GetCardHeroesCount(string type);
    void InsertCardHeroesGallery(string Id);
    void UpdateStatusCardHeroesGallery(string Id);
    CardHeroes SumPowerCardHeroesGallery();
}