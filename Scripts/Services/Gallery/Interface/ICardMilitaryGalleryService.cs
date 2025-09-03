using System.Collections.Generic;

public interface ICardMilitaryGallerService
{
    List<CardMilitary> GetCardMilitaryCollection(string type, int pageSize, int offset, string rare);
    int GetCardMilitaryCount(string type, string rare);
    void InsertCardMilitaryGallery(string Id);
    void UpdateStatusCardMilitaryGallery(string Id);
    void UpdateStarCardMilitaryGallery(string Id, double star);
    void UpdateCardMilitaryGalleryPower(string Id);
    CardMilitary SumPowerCardMilitaryGallery();
}