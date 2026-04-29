using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardMilitariesGallerService
{
    Task<List<CardMilitaries>> GetCardMilitariesCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardMilitariesCountAsync(string search, string type, string rare);
    Task InsertCardMilitaryGalleryAsync(string Id);
    Task UpdateStatusCardMilitaryGalleryAsync(string Id);
    Task UpdateStarCardMilitaryGalleryAsync(string Id, double star);
    Task UpdateCardMilitaryGalleryPowerAsync(string Id);
    Task<CardMilitaries> SumPowerCardMilitariesGalleryAsync();
}