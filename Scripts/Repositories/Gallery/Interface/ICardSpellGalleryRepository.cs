using System.Collections.Generic;

public interface ICardSpellGalleryRepository
{
    List<CardSpells> GetCardSpellCollection(string type, int pageSize, int offset, string rare);
    int GetCardSpellCount(string type, string rare);
    void InsertCardSpellGallery(string Id, CardSpells CardSpellFromDB);
    void UpdateStatusCardSpellGallery(string Id);
    void UpdateStarCardSpellGallery(string Id, double star);
    void UpdateCardSpellGalleryPower(string Id, CardSpells CardSpellFromDB);
    CardSpells SumPowerCardSpellGallery();
}