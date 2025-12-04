using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMilitariesGalleryRepository
{
    Task<List<CardMilitaries>> GetCardMilitariesCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetCardMilitariesCountAsync(string type, string rare);
    Task InsertCardMilitaryGalleryAsync(string Id, CardMilitaries CardMilitaryFromDB);
    Task UpdateStatusCardMilitaryGalleryAsync(string Id);
    Task UpdateStarCardMilitaryGalleryAsync(string Id, double star);
    Task UpdateCardMilitaryGalleryPowerAsync(string Id, CardMilitaries CardMilitaryFromDB);
    Task<CardMilitaries> SumPowerCardMilitariesGalleryAsync();
}