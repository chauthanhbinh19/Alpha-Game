using System.Collections.Generic;

public interface ICardCaptainsGalleryService
{
    List<CardCaptains> GetCardCaptainsCollection(string type, int pageSize, int offset);
    int GetCardCaptainsCount(string type);
    void InsertCardCaptainsGallery(string Id);
    void UpdateStatusCardCaptainsGallery(string Id);
    CardCaptains SumPowerCardCaptainsGallery();
}