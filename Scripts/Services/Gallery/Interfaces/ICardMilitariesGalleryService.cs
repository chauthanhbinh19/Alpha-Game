using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMilitariesGalleryService
{
    Task<List<CardMilitaries>> GetCardMilitariesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardMilitariesCountAsync(string search, string type, string rare);
    Task InsertCardMilitaryGalleryAsync(string userId, string Id);
    Task UpdateStatusCardMilitaryGalleryAsync(string userId, string Id);
    Task UpdateStarCardMilitaryGalleryAsync(string userId, string Id, double star);
    Task UpdateCardMilitaryGalleryPowerAsync(string userId, string Id);
    Task<CardMilitaries> SumPowerCardMilitariesGalleryAsync(string userId);
}