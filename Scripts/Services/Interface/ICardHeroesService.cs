using System.Collections.Generic;

public interface ICardHeroesService
{
    List<string> GetUniqueCardHeroTypes();
    List<CardHeroes> GetCardHeroes(string type, int pageSize, int offset);
    int GetCardHeroesCount(string type);
    List<CardHeroes> GetCardHeroesRandom(string type, int pageSize);
    List<CardHeroes> GetAllCardHeroes(string type);
    int GetMaxQuantity(string Id);
    CardHeroes GetCardHeroesById(string Id);
    List<CardHeroes> GetCardHeroesWithPrice(string type, int pageSize, int offset);
    int GetCardHeroesWithPriceCount(string type);
    
}