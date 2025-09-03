using System.Collections.Generic;

public interface ICardGeneralsGalleryRepository
{
    List<CardGenerals> GetCardGeneralsCollection(string type, int pageSize, int offset, string rare);
    int GetCardGeneralsCount(string type, string rare);
    void InsertCardGeneralsGallery(string Id, CardGenerals CaptainFromDB);
    void UpdateStatusCardGeneralsGallery(string Id);
    void UpdateStarCardGeneralsGallery(string Id, double star);
    void UpdateCardGeneralsGalleryPower(string Id, CardGenerals CardGeneralFromDB);
    CardGenerals SumPowerCardGeneralsGallery();
}