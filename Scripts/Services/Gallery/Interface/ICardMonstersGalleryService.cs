using System.Collections.Generic;

public interface ICardMonstersGalleryService
{
    List<CardMonsters> GetCardMonstersCollection(string type, int pageSize, int offset);
    int GetCardMonstersCount(string type);
    void InsertCardMonstersGallery(string Id);
    void UpdateStatusCardMonstersGallery(string Id);
    CardMonsters SumPowerCardMonstersGallery();
}