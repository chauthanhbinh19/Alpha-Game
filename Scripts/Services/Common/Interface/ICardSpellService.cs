using System.Collections.Generic;

public interface ICardSpellService
{
    List<string> GetUniqueCardSpellTypes();
    List<string> GetUniqueCardSpellId();
    List<CardSpell> GetCardSpell(string type, int pageSize, int offset);
    int GetCardSpellCount(string type);
    List<CardSpell> GetCardSpellRandom(string type, int pageSize);
    List<CardSpell> GetAllCardSpell(string type);
    CardSpell GetCardSpellById(string Id);
    List<CardSpell> GetCardSpellWithPrice(string type, int pageSize, int offset);
    int GetCardSpellWithPriceCount(string type);
}
