using System.Collections.Generic;

public interface ICardCaptainsGalleryRepository
{
    List<CardCaptains> GetCardCaptainsCollection(string type, int pageSize, int offset);
    int GetCardCaptainsCount(string type);
    void InsertCardCaptainsGallery(string Id, CardCaptains CaptainFromDB);
    void UpdateStatusCardCaptainsGallery(string Id);
    CardCaptains SumPowerCardCaptainsGallery();
}