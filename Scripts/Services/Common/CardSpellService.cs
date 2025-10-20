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

    public List<CardSpells> GetCardSpell(string type, int pageSize, int offset, string rare)
    {
        List<CardSpells> list = _cardSpellRepository.GetCardSpell(type, pageSize, offset, rare);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardSpellCount(string type, string rare)
    {
        return _cardSpellRepository.GetCardSpellCount(type, rare);
    }

    public List<CardSpells> GetCardSpellRandom(string type, int pageSize)
    {
        return _cardSpellRepository.GetCardSpellRandom(type, pageSize);
    }

    public List<CardSpells> GetAllCardSpell(string type)
    {
        return _cardSpellRepository.GetAllCardSpell(type);
    }

    public CardSpells GetCardSpellById(string Id)
    {
        return _cardSpellRepository.GetCardSpellById(Id);
    }

    public List<CardSpells> GetCardSpellWithPrice(string type, int pageSize, int offset)
    {
        List<CardSpells> list = _cardSpellRepository.GetCardSpellWithPrice(type, pageSize, offset);
        list = QualityEvaluator.GetQualityPower(list);
        return list;
    }

    public int GetCardSpellWithPriceCount(string type)
    {
        return _cardSpellRepository.GetCardSpellWithPriceCount(type);
    }

    public List<string> GetUniqueCardSpellId()
    {
        return _cardSpellRepository.GetUniqueCardSpellId();
    }
}
