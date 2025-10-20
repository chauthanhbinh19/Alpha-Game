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

    public List<CardLives> GetCardLife(string type, int pageSize, int offset, string rare)
    {
        List<CardLives> list = _cardLifeRepository.GetCardLife(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardLifeCount(string type, string rare)
    {
        return _cardLifeRepository.GetCardLifeCount(type, rare);
    }

    public List<CardLives> GetCardLifeWithPrice(string type, int pageSize, int offset)
    {
        List<CardLives> list = _cardLifeRepository.GetCardLifeWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardLifeWithPriceCount(string type)
    {
        return _cardLifeRepository.GetCardLifeWithPriceCount(type);
    }

    public CardLives GetCardLifeById(string Id)
    {
        return _cardLifeRepository.GetCardLifeById(Id);
    }

    public CardLives SumPowerCardLifePercent()
    {
        return _cardLifeRepository.SumPowerCardLifePercent();
    }

    public List<string> GetUniqueCardLifeId()
    {
        return _cardLifeRepository.GetUniqueCardLifeId();
    }
}