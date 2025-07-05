using System.Collections.Generic;

public class CardMonstersService : ICardMonstersService
{
    private readonly ICardMonstersRepository _cardMonstersRepository;

    public CardMonstersService(ICardMonstersRepository cardMonstersRepository)
    {
        _cardMonstersRepository = cardMonstersRepository;
    }

    public static CardMonstersService Create()
    {
        return new CardMonstersService(new CardMonstersRepository());
    }

    public List<string> GetUniqueCardMonstersTypes()
    {
        return _cardMonstersRepository.GetUniqueCardMonstersTypes();
    }

    public List<CardMonsters> GetCardMonsters(string type, int pageSize, int offset)
    {
        List<CardMonsters> list = _cardMonstersRepository.GetCardMonsters(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardMonstersCount(string type)
    {
        return _cardMonstersRepository.GetCardMonstersCount(type);
    }

    public List<CardMonsters> GetCardMonstersRandom(string type, int pageSize)
    {
        return _cardMonstersRepository.GetCardMonstersRandom(type, pageSize);
    }

    public List<CardMonsters> GetAllCardMonsters(string type)
    {
        return _cardMonstersRepository.GetAllCardMonsters(type);
    }

    public CardMonsters GetCardMonstersById(string Id)
    {
        return _cardMonstersRepository.GetCardMonstersById(Id);
    }

    public List<CardMonsters> GetCardMonstersWithPrice(string type, int pageSize, int offset)
    {
        List<CardMonsters> list = _cardMonstersRepository.GetCardMonstersWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardMonstersWithPriceCount(string type)
    {
        return _cardMonstersRepository.GetCardMonstersWithPriceCount(type);
    }

    public List<string> GetUniqueCardMonstersId()
    {
        return _cardMonstersRepository.GetUniqueCardMonstersId();
    }
}
