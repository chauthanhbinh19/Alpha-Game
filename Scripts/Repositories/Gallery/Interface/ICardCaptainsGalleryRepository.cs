using System.Collections.Generic;

public interface ICardCaptainsGalleryRepository
{
    List<CardCaptains> GetCardCaptainsCollection(string type, int pageSize, int offset, string rare);
    int GetCardCaptainsCount(string type, string rare);
    void InsertCardCaptainsGallery(string Id, CardCaptains CaptainFromDB);
    void UpdateStatusCardCaptainsGallery(string Id);
    CardCaptains SumPowerCardCaptainsGallery();
}