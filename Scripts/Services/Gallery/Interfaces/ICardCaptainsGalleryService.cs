using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardCaptainsGalleryService
{
    Task<List<CardCaptains>> GetCardCaptainsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardCaptainsCountAsync(string search, string type, string rare);
    Task InsertCardCaptainGalleryAsync(string userId, string Id);
    Task UpdateStatusCardCaptainGalleryAsync(string userId, string Id);
    Task UpdateStarCardCaptainGalleryAsync(string userId, string Id, double star);
    Task UpdateCardCaptainGalleryPowerAsync(string userId, string Id);
    Task<CardCaptains> SumPowerCardCaptainsGalleryAsync(string userId);
}