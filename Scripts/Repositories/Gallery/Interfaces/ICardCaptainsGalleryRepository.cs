using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardCaptainsGalleryRepository
{
    Task<List<CardCaptains>> GetCardCaptainsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardCaptainsCountAsync(string search, string type, string rare);
    Task InsertCardCaptainGalleryAsync(string userId, string Id, CardCaptains CardCaptainFromDB);
    Task UpdateStatusCardCaptainGalleryAsync(string userId, string Id);
    Task UpdateStarCardCaptainGalleryAsync(string userId, string Id, double star);
    Task UpdateCardCaptainGalleryPowerAsync(string userId, string Id, CardCaptains CardCaptainFromDB);
    Task<CardCaptains> SumPowerCardCaptainsGalleryAsync(string userId);
}