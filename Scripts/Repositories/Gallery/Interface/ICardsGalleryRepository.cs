using System.Collections.Generic;

public interface ICardsGalleryRepository
{
    List<Cards> GetCardsCollection(int pageSize, int offset, string rare);
    int GetCardsCount(string rare);
    void InsertCardsGallery(string Id, Cards TitleFromDB);
    void UpdateStatusCardsGallery(string Id);
    void UpdateStarCardsGallery(string Id, double star);
    void UpdateCardsGalleryPower(string Id, Cards TitleFromDB);
    Cards SumPowerCardsGallery();
}