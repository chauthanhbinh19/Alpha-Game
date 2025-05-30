using System.Collections.Generic;

public class CardLifeService : ICardLifeService
{
    private readonly ICardLifeRepository _cardLifeRepository;

    public CardLifeService(ICardLifeRepository cardLifeRepository)
    {
        _cardLifeRepository = cardLifeRepository;
    }

    public static CardLifeService Create()
    {
        return new CardLifeService(new CardLifeRepository());
    }

    public List<string> GetUniqueCardLifeTypes()
    {
        return _cardLifeRepository.GetUniqueCardLifeTypes();
    }

    public List<CardLife> GetCardLife(string type, int pageSize, int offset)
    {
        List<CardLife> list = _cardLifeRepository.GetCardLife(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardLifeCount(string type)
    {
        return _cardLifeRepository.GetCardLifeCount(type);
    }

    public List<CardLife> GetCardLifeWithPrice(string type, int pageSize, int offset)
    {
        List<CardLife> list = _cardLifeRepository.GetCardLifeWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardLifeWithPriceCount(string type)
    {
        return _cardLifeRepository.GetCardLifeWithPriceCount(type);
    }

    public CardLife GetCardLifeById(string Id)
    {
        return _cardLifeRepository.GetCardLifeById(Id);
    }

    public CardLife SumPowerCardLifePercent()
    {
        return _cardLifeRepository.SumPowerCardLifePercent();
    }
}