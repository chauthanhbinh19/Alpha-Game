using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardColonelsGalleryService
{
    Task<List<CardColonels>> GetCardColonelsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardColonelsCountAsync(string search, string type, string rare);
    Task InsertCardColonelGalleryAsync(string userId, string Id);
    Task UpdateStatusCardColonelGalleryAsync(string userId, string Id);
    Task UpdateStarCardColonelGalleryAsync(string userId, string Id, double star);
    Task UpdateCardColonelGalleryPowerAsync(string userId, string Id);
    Task<CardColonels> SumPowerCardColonelsGalleryAsync(string userId);
}