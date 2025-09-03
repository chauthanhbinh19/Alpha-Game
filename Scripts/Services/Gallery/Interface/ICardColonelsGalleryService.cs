using System.Collections.Generic;

public interface ICardColonelsGalleryService
{
    List<CardColonels> GetCardColonelsCollection(string type, int pageSize, int offset, string rare);
    int GetCardColonelsCount(string type, string rare);
    void InsertCardColonelsGallery(string Id);
    void UpdateStatusCardColonelsGallery(string Id);
    void UpdateStarCardColonelsGallery(string Id, double star);
    void UpdateCardColonelsGalleryPower(string Id);
    CardColonels SumPowerCardColonelsGallery();
}