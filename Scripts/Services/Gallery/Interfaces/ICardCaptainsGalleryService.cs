using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardCaptainsGalleryService
{
    Task<List<CardCaptains>> GetCardCaptainsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardCaptainsCountAsync(string search, string type, string rare);
    Task InsertCardCaptainGalleryAsync(string Id);
    Task UpdateStatusCardCaptainGalleryAsync(string Id);
    Task UpdateStarCardCaptainGalleryAsync(string Id, double star);
    Task UpdateCardCaptainGalleryPowerAsync(string Id);
    Task<CardCaptains> SumPowerCardCaptainsGalleryAsync();
}