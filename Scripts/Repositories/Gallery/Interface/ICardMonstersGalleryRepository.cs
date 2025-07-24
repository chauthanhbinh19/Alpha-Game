using System.Collections.Generic;

public interface ICardMonstersGalleryRepository
{
    List<CardMonsters> GetCardMonstersCollection(string type, int pageSize, int offset, string rare);
    int GetCardMonstersCount(string type, string rare);
    void InsertCardMonstersGallery(string Id, CardMonsters MonsterFromDB);
    void UpdateStatusCardMonstersGallery(string Id);
    CardMonsters SumPowerCardMonstersGallery();
}