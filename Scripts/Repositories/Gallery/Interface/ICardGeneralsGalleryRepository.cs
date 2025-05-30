using System.Collections.Generic;

public interface ICardGeneralsGalleryRepository
{
    List<CardGenerals> GetCardGeneralsCollection(string type, int pageSize, int offset);
    int GetCardGeneralsCount(string type);
    void InsertCardGeneralsGallery(string Id, CardGenerals CaptainFromDB);
    void UpdateStatusCardGeneralsGallery(string Id);
    CardGenerals SumPowerCardGeneralsGallery();
}