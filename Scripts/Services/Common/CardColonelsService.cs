using System.Collections.Generic;

public class CardColonelsService : ICardColonelsService
{
    private readonly ICardColonelsRepository _cardColonelsRepository;

    public CardColonelsService(ICardColonelsRepository cardColonelsRepository)
    {
        _cardColonelsRepository = cardColonelsRepository;
    }

    public static CardColonelsService Create()
    {
        return new CardColonelsService(new CardColonelsRepository());
    }

    public List<string> GetUniqueCardColonelsTypes()
    {
        return _cardColonelsRepository.GetUniqueCardColonelsTypes();
    }

    public List<CardColonels> GetCardColonels(string type, int pageSize, int offset)
    {
        List<CardColonels> list = _cardColonelsRepository.GetCardColonels(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardColonelsCount(string type)
    {
        return _cardColonelsRepository.GetCardColonelsCount(type);
    }

    public List<CardColonels> GetCardColonelsRandom(string type, int pageSize)
    {
        return _cardColonelsRepository.GetCardColonelsRandom(type, pageSize);
    }

    public List<CardColonels> GetAllCardColonels(string type)
    {
        return _cardColonelsRepository.GetAllCardColonels(type);
    }

    public CardColonels GetCardColonelsById(string Id)
    {
        return _cardColonelsRepository.GetCardColonelsById(Id);
    }

    public List<CardColonels> GetCardColonelsWithPrice(string type, int pageSize, int offset)
    {
        List<CardColonels> list = _cardColonelsRepository.GetCardColonelsWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardColonelsWithPriceCount(string type)
    {
        return _cardColonelsRepository.GetCardColonelsWithPriceCount(type);
    }

    public List<string> GetUniqueCardColonelsId()
    {
        return _cardColonelsRepository.GetUniqueCardColonelsId();
    }
}