using System.Collections.Generic;

public interface ICardGeneralsGalleryService
{
    List<CardGenerals> GetCardGeneralsCollection(string type, int pageSize, int offset, string rare);
    int GetCardGeneralsCount(string type, string rare);
    void InsertCardGeneralsGallery(string Id);
    void UpdateStatusCardGeneralsGallery(string Id);
    void UpdateStarCardGeneralsGallery(string Id, double star);
    void UpdateCardGeneralsGalleryPower(string Id);
    CardGenerals SumPowerCardGeneralsGallery();
}