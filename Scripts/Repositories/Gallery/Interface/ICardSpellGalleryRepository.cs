using System.Collections.Generic;

public interface ICardSpellGalleryRepository
{
    List<CardSpell> GetCardSpellCollection(string type, int pageSize, int offset, string rare);
    int GetCardSpellCount(string type, string rare);
    void InsertCardSpellGallery(string Id, CardSpell CardSpellFromDB);
    void UpdateStatusCardSpellGallery(string Id);
    void UpdateStarCardSpellGallery(string Id, double star);
    void UpdateCardSpellGalleryPower(string Id, CardSpell CardSpellFromDB);
    CardSpell SumPowerCardSpellGallery();
}