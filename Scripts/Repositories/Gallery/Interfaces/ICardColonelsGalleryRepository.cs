using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardColonelsGalleryRepository
{
    Task<List<CardColonels>> GetCardColonelsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardColonelsCountAsync(string search, string type, string rare);
    Task InsertCardColonelGalleryAsync(string Id, CardColonels CardColonelFromDB);
    Task UpdateStatusCardColonelGalleryAsync(string Id);
    Task UpdateStarCardColonelGalleryAsync(string Id, double star);
    Task UpdateCardColonelGalleryPowerAsync(string Id, CardColonels CardColonelFromDB);
    Task<CardColonels> SumPowerCardColonelsGalleryAsync();
}