using System.Collections.Generic;

public interface ICardMonstersGalleryRepository
{
    List<CardMonsters> GetCardMonstersCollection(string type, int pageSize, int offset);
    int GetCardMonstersCount(string type);
    void InsertCardMonstersGallery(string Id, CardMonsters MonsterFromDB);
    void UpdateStatusCardMonstersGallery(string Id);
    CardMonsters SumPowerCardMonstersGallery();
}