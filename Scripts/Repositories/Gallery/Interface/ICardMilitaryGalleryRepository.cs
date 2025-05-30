using System.Collections.Generic;

public interface ICardMilitaryGalleryRepository
{
    List<CardMilitary> GetCardMilitaryCollection(string type, int pageSize, int offset);
    int GetCardMilitaryCount(string type);
    void InsertCardMilitaryGallery(string Id, CardMilitary CardMilitaryFromDB);
    void UpdateStatusCardMilitaryGallery(string Id);
    CardMilitary SumPowerCardMilitaryGallery();
}