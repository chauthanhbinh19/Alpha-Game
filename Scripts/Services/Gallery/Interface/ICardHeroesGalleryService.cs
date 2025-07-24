using System.Collections.Generic;

public interface ICardHeroesGalleryService
{ 
    List<CardHeroes> GetCardHeroesCollection(string type, int pageSize, int offset, string rare);
    int GetCardHeroesCount(string type, string rare);
    void InsertCardHeroesGallery(string Id);
    void UpdateStatusCardHeroesGallery(string Id);
    CardHeroes SumPowerCardHeroesGallery();
}