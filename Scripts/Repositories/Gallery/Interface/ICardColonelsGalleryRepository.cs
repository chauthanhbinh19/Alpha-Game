using System.Collections.Generic;

public interface ICardColonelsGalleryRepository
{
    List<CardColonels> GetCardColonelsCollection(string type, int pageSize, int offset);
    int GetCardColonelsCount(string type);
    void InsertCardColonelsGallery(string Id, CardColonels CaptainFromDB);
    void UpdateStatusCardColonelsGallery(string Id);
    CardColonels SumPowerCardColonelsGallery();
}