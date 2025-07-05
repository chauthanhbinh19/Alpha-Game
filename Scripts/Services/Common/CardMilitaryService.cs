using System.Collections.Generic;

public class CardMilitaryService : ICardMilitaryService
{
    private readonly ICardMilitaryRepository _cardMilitaryRepository;

    public CardMilitaryService(ICardMilitaryRepository cardMilitaryRepository)
    {
        _cardMilitaryRepository = cardMilitaryRepository;
    }

    public static CardMilitaryService Create()
    {
        return new CardMilitaryService(new CardMilitaryRepository());
    }

    public List<string> GetUniqueCardMilitaryTypes()
    {
        return _cardMilitaryRepository.GetUniqueCardMilitaryTypes();
    }

    public List<CardMilitary> GetCardMilitary(string type, int pageSize, int offset)
    {
        List<CardMilitary> list = _cardMilitaryRepository.GetCardMilitary(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardMilitaryCount(string type)
    {
        return _cardMilitaryRepository.GetCardMilitaryCount(type);
    }

    public List<CardMilitary> GetCardMilitaryRandom(string type, int pageSize)
    {
        return _cardMilitaryRepository.GetCardMilitaryRandom(type, pageSize);
    }

    public List<CardMilitary> GetAllCardMilitary(string type)
    {
        return _cardMilitaryRepository.GetAllCardMilitary(type);
    }

    public CardMilitary GetCardMilitaryById(string Id)
    {
        return _cardMilitaryRepository.GetCardMilitaryById(Id);
    }

    public List<CardMilitary> GetCardMilitaryWithPrice(string type, int pageSize, int offset)
    {
        List<CardMilitary> list = _cardMilitaryRepository.GetCardMilitaryWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardMilitaryWithPriceCount(string type)
    {
        return _cardMilitaryRepository.GetCardMilitaryWithPriceCount(type);
    }

    public List<string> GetUniqueCardMilitaryId()
    {
        return _cardMilitaryRepository.GetUniqueCardMilitaryId();
    }
}