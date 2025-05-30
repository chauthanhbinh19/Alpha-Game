using System.Collections.Generic;

public class CardCaptainsService : ICardCaptainsService
{
    private readonly ICardCaptainsRepository _cardCaptainsRepository;

    public CardCaptainsService(ICardCaptainsRepository cardCaptainsRepository)
    {
        _cardCaptainsRepository = cardCaptainsRepository;
    }

    public static CardCaptainsService Create()
    {
        return new CardCaptainsService(new CardCaptainsRepository());
    }

    public List<string> GetUniqueCardCaptainsTypes()
    {
        return _cardCaptainsRepository.GetUniqueCardCaptainsTypes();
    }

    public List<CardCaptains> GetCardCaptains(string type, int pageSize, int offset)
    {
        List<CardCaptains> list = _cardCaptainsRepository.GetCardCaptains(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardCaptainsCount(string type)
    {
        return _cardCaptainsRepository.GetCardCaptainsCount(type);
    }

    public List<CardCaptains> GetCardCaptainsRandom(string type, int pageSize)
    {
        return _cardCaptainsRepository.GetCardCaptainsRandom(type, pageSize);
    }

    public List<CardCaptains> GetAllCardCaptains(string type)
    {
        return _cardCaptainsRepository.GetAllCardCaptains(type);
    }

    public CardCaptains GetCardCaptainsById(string Id)
    {
        return _cardCaptainsRepository.GetCardCaptainsById(Id);
    }

    public List<CardCaptains> GetCardCaptainsWithPrice(string type, int pageSize, int offset)
    {
        List<CardCaptains> list = _cardCaptainsRepository.GetCardCaptainsWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardCaptainsWithPriceCount(string type)
    {
        return _cardCaptainsRepository.GetCardCaptainsWithPriceCount(type);
    }

}