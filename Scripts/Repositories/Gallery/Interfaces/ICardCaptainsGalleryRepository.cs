using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardCaptainsGalleryRepository
{
    Task<List<CardCaptains>> GetCardCaptainsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardCaptainsCountAsync(string search, string type, string rare);
    Task InsertCardCaptainGalleryAsync(string Id, CardCaptains CardCaptainFromDB);
    Task UpdateStatusCardCaptainGalleryAsync(string Id);
    Task UpdateStarCardCaptainGalleryAsync(string Id, double star);
    Task UpdateCardCaptainGalleryPowerAsync(string Id, CardCaptains CardCaptainFromDB);
    Task<CardCaptains> SumPowerCardCaptainsGalleryAsync();
}