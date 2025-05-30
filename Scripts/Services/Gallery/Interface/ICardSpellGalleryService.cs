using System.Collections.Generic;

public interface ICardSpellGalleryService
{
    List<CardSpell> GetCardSpellCollection(string type, int pageSize, int offset);
    int GetCardSpellCount(string type);
    void InsertCardSpellGallery(string Id);
    void UpdateStatusCardSpellGallery(string Id);
    CardSpell SumPowerCardSpellGallery();
}