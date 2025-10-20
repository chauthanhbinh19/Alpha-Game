using System.Collections.Generic;

public interface ICardSpellService
{
    List<string> GetUniqueCardSpellTypes();
    List<string> GetUniqueCardSpellId();
    List<CardSpells> GetCardSpell(string type, int pageSize, int offset, string rare);
    int GetCardSpellCount(string type, string rare);
    List<CardSpells> GetCardSpellRandom(string type, int pageSize);
    List<CardSpells> GetAllCardSpell(string type);
    CardSpells GetCardSpellById(string Id);
    List<CardSpells> GetCardSpellWithPrice(string type, int pageSize, int offset);
    int GetCardSpellWithPriceCount(string type);
}
