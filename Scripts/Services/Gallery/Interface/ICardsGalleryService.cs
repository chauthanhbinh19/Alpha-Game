using System.Collections.Generic;

public interface ICardsGalleryService
{
    List<Cards> GetCardsCollection(int pageSize, int offset, string rare);
    int GetCardsCount(string rare);
    void InsertCardsGallery(string Id);
    void UpdateStatusCardsGallery(string Id);
    void UpdateStarCardsGallery(string Id, double star);
    void UpdateCardsGalleryPower(string Id);
    Cards SumPowerCardsGallery();
}