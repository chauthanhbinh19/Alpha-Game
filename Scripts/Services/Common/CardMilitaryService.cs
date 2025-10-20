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

    public List<CardMilitaries> GetCardMilitary(string type, int pageSize, int offset, string rare)
    {
        List<CardMilitaries> list = _cardMilitaryRepository.GetCardMilitary(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardMilitaryCount(string type, string rare)
    {
        return _cardMilitaryRepository.GetCardMilitaryCount(type, rare);
    }

    public List<CardMilitaries> GetCardMilitaryRandom(string type, int pageSize)
    {
        return _cardMilitaryRepository.GetCardMilitaryRandom(type, pageSize);
    }

    public List<CardMilitaries> GetAllCardMilitary(string type)
    {
        return _cardMilitaryRepository.GetAllCardMilitary(type);
    }

    public CardMilitaries GetCardMilitaryById(string Id)
    {
        return _cardMilitaryRepository.GetCardMilitaryById(Id);
    }

    public List<CardMilitaries> GetCardMilitaryWithPrice(string type, int pageSize, int offset)
    {
        List<CardMilitaries> list = _cardMilitaryRepository.GetCardMilitaryWithPrice(type, pageSize, offset);
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