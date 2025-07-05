using System.Collections.Generic;

public class CardHeroesService : ICardHeroesService
{
    private readonly ICardHeroesRepository _cardHeroesRepository;

    public CardHeroesService(ICardHeroesRepository cardHeroesRepository)
    {
        _cardHeroesRepository = cardHeroesRepository;
    }

    public static CardHeroesService Create()
    {
        return new CardHeroesService(new CardHeroesRepository());
    }

    public List<string> GetUniqueCardHeroTypes()
    {
        return _cardHeroesRepository.GetUniqueCardHeroTypes();
    }

    public List<CardHeroes> GetCardHeroes(string type, int pageSize, int offset)
    {
        List<CardHeroes> list = _cardHeroesRepository.GetCardHeroes(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardHeroesCount(string type)
    {
        return _cardHeroesRepository.GetCardHeroesCount(type);
    }

    public List<CardHeroes> GetCardHeroesRandom(string type, int pageSize)
    {
        return _cardHeroesRepository.GetCardHeroesRandom(type, pageSize);
    }

    public List<CardHeroes> GetAllCardHeroes(string type)
    {
        return _cardHeroesRepository.GetAllCardHeroes(type);
    }

    public int GetMaxQuantity(string Id)
    {
        return _cardHeroesRepository.GetMaxQuantity(Id);
    }

    public CardHeroes GetCardHeroesById(string Id)
    {
        return _cardHeroesRepository.GetCardHeroesById(Id);
    }

    public List<CardHeroes> GetCardHeroesWithPrice(string type, int pageSize, int offset)
    {
        List<CardHeroes> list = _cardHeroesRepository.GetCardHeroesWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardHeroesWithPriceCount(string type)
    {
        return _cardHeroesRepository.GetCardHeroesWithPriceCount(type);
    }

    public List<string> GetUniqueCardHeroId()
    {
        return _cardHeroesRepository.GetUniqueCardHeroId();
    }
}