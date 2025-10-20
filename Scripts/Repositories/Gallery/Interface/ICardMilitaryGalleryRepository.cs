using System.Collections.Generic;

public interface ICardMilitaryGalleryRepository
{
    List<CardMilitaries> GetCardMilitaryCollection(string type, int pageSize, int offset, string rare);
    int GetCardMilitaryCount(string type, string rare);
    void InsertCardMilitaryGallery(string Id, CardMilitaries CardMilitaryFromDB);
    void UpdateStatusCardMilitaryGallery(string Id);
    void UpdateStarCardMilitaryGallery(string Id, double star);
    void UpdateCardMilitaryGalleryPower(string Id, CardMilitaries CardMilitaryFromDB);
    CardMilitaries SumPowerCardMilitaryGallery();
}