using System.Collections.Generic;

public interface ICardColonelsGalleryService
{
    List<CardColonels> GetCardColonelsCollection(string type, int pageSize, int offset);
    int GetCardColonelsCount(string type);
    void InsertCardColonelsGallery(string Id);
    void UpdateStatusCardColonelsGallery(string Id);
    CardColonels SumPowerCardColonelsGallery();
}