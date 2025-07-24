using System.Collections.Generic;

public interface ICardColonelsGalleryRepository
{
    List<CardColonels> GetCardColonelsCollection(string type, int pageSize, int offset, string rare);
    int GetCardColonelsCount(string type, string rare);
    void InsertCardColonelsGallery(string Id, CardColonels CaptainFromDB);
    void UpdateStatusCardColonelsGallery(string Id);
    CardColonels SumPowerCardColonelsGallery();
}