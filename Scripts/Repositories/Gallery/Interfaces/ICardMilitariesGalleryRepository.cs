using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMilitariesGalleryRepository
{
    Task<List<CardMilitaries>> GetCardMilitariesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardMilitariesCountAsync(string search, string type, string rare);
    Task InsertCardMilitaryGalleryAsync(string userId, string Id, CardMilitaries CardMilitaryFromDB);
    Task UpdateStatusCardMilitaryGalleryAsync(string userId, string Id);
    Task UpdateStarCardMilitaryGalleryAsync(string userId, string Id, double star);
    Task UpdateCardMilitaryGalleryPowerAsync(string userId, string Id, CardMilitaries CardMilitaryFromDB);
    Task<CardMilitaries> SumPowerCardMilitariesGalleryAsync(string userId);
}