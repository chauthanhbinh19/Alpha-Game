using System.Collections.Generic;

public interface ICardColonelsGalleryService
{
    List<CardColonels> GetCardColonelsCollection(string type, int pageSize, int offset, string rare);
    int GetCardColonelsCount(string type, string rare);
    void InsertCardColonelsGallery(string Id);
    void UpdateStatusCardColonelsGallery(string Id);
    CardColonels SumPowerCardColonelsGallery();
}