using System.Collections.Generic;

public interface ICardHeroesGalleryRepository
{ 
    List<CardHeroes> GetCardHeroesCollection(string type, int pageSize, int offset, string rare);
    int GetCardHeroesCount(string type, string rare);
    void InsertCardHeroesGallery(string Id, CardHeroes CardFromDB);
    void UpdateStatusCardHeroesGallery(string Id);
    CardHeroes SumPowerCardHeroesGallery();
}