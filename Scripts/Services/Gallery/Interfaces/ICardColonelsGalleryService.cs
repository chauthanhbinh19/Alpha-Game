using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardColonelsGalleryService
{
    Task<List<CardColonels>> GetCardColonelsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardColonelsCountAsync(string search, string type, string rare);
    Task InsertCardColonelGalleryAsync(string Id);
    Task UpdateStatusCardColonelGalleryAsync(string Id);
    Task UpdateStarCardColonelGalleryAsync(string Id, double star);
    Task UpdateCardColonelGalleryPowerAsync(string Id);
    Task<CardColonels> SumPowerCardColonelsGalleryAsync();
}