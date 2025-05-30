using System.Collections.Generic;

public class CardSpellService : ICardSpellService
{
    private readonly ICardSpellRepository _cardSpellRepository;

    public CardSpellService(ICardSpellRepository cardSpellRepository)
    {
        _cardSpellRepository = cardSpellRepository;
    }

    public static CardSpellService Create()
    {
        return new CardSpellService(new CardSpellRepository());
    }

    public List<string> GetUniqueCardSpellTypes()
    {
        return _cardSpellRepository.GetUniqueCardSpellTypes();
    }

    public List<CardSpell> GetCardSpell(string type, int pageSize, int offset)
    {
        List<CardSpell> list = _cardSpellRepository.GetCardSpell(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardSpellCount(string type)
    {
        return _cardSpellRepository.GetCardSpellCount(type);
    }

    public List<CardSpell> GetCardSpellRandom(string type, int pageSize)
    {
        return _cardSpellRepository.GetCardSpellRandom(type, pageSize);
    }

    public List<CardSpell> GetAllCardSpell(string type)
    {
        return _cardSpellRepository.GetAllCardSpell(type);
    }

    public CardSpell GetCardSpellById(string Id)
    {
        return _cardSpellRepository.GetCardSpellById(Id);
    }

    public List<CardSpell> GetCardSpellWithPrice(string type, int pageSize, int offset)
    {
        List<CardSpell> list = _cardSpellRepository.GetCardSpellWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardSpellWithPriceCount(string type)
    {
        return _cardSpellRepository.GetCardSpellWithPriceCount(type);
    }
}
