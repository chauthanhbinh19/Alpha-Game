using System.Collections.Generic;

public interface ICardGeneralsGalleryService
{
    List<CardGenerals> GetCardGeneralsCollection(string type, int pageSize, int offset);
    int GetCardGeneralsCount(string type);
    void InsertCardGeneralsGallery(string Id);
    void UpdateStatusCardGeneralsGallery(string Id);
    CardGenerals SumPowerCardGeneralsGallery();
}