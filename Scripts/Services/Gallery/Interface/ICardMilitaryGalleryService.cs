using System.Collections.Generic;

public interface ICardMilitaryGallerService
{
    List<CardMilitary> GetCardMilitaryCollection(string type, int pageSize, int offset);
    int GetCardMilitaryCount(string type);
    void InsertCardMilitaryGallery(string Id);
    void UpdateStatusCardMilitaryGallery(string Id);
    CardMilitary SumPowerCardMilitaryGallery();
}