using System.Collections.Generic;

public interface ICardSpellGalleryService
{
    List<CardSpells> GetCardSpellCollection(string type, int pageSize, int offset, string rare);
    int GetCardSpellCount(string type, string rare);
    void InsertCardSpellGallery(string Id);
    void UpdateStatusCardSpellGallery(string Id);
    void UpdateStarCardSpellGallery(string Id, double star);
    void UpdateCardSpellGalleryPower(string Id);
    CardSpells SumPowerCardSpellGallery();
}