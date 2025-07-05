using System.Collections.Generic;

public class CardGeneralsService : ICardGeneralsService
{
    private readonly ICardGeneralsRepository _cardGeneralsRepository;

    public CardGeneralsService(ICardGeneralsRepository cardGeneralsRepository)
    {
        _cardGeneralsRepository = cardGeneralsRepository;
    }

    public static CardGeneralsService Create()
    {
        return new CardGeneralsService(new CardGeneralsRepository());
    }

    public List<string> GetUniqueCardGeneralsTypes()
    {
        return _cardGeneralsRepository.GetUniqueCardGeneralsTypes();
    }

    public List<CardGenerals> GetCardGenerals(string type, int pageSize, int offset)
    {
        List<CardGenerals> list = _cardGeneralsRepository.GetCardGenerals(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardGeneralsCount(string type)
    {
        return _cardGeneralsRepository.GetCardGeneralsCount(type);
    }

    public List<CardGenerals> GetCardGeneralsRandom(string type, int pageSize)
    {
        return _cardGeneralsRepository.GetCardGeneralsRandom(type, pageSize);
    }

    public List<CardGenerals> GetAllCardGenerals(string type)
    {
        return _cardGeneralsRepository.GetAllCardGenerals(type);
    }

    public CardGenerals GetCardGeneralsById(string Id)
    {
        return _cardGeneralsRepository.GetCardGeneralsById(Id);
    }

    public List<CardGenerals> GetCardGeneralsWithPrice(string type, int pageSize, int offset)
    {
        List<CardGenerals> list = _cardGeneralsRepository.GetCardGeneralsWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardGeneralsWithPriceCount(string type)
    {
        return _cardGeneralsRepository.GetCardGeneralsWithPriceCount(type);
    }

    public List<string> GetUniqueCardGeneralsId()
    {
        return _cardGeneralsRepository.GetUniqueCardGeneralsId();
    }
}