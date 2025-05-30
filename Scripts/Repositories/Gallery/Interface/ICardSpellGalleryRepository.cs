using System.Collections.Generic;

public interface ICardSpellGalleryRepository
{
    List<CardSpell> GetCardSpellCollection(string type, int pageSize, int offset);
    int GetCardSpellCount(string type);
    void InsertCardSpellGallery(string Id, CardSpell CardSpellFromDB);
    void UpdateStatusCardSpellGallery(string Id);
    CardSpell SumPowerCardSpellGallery();
}