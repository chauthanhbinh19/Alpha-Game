using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardsGalleryRepository
{
    Task<List<Cards>> GetCardsCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetCardsCountAsync(string search, string rare);
    Task InsertCardGalleryAsync(string Id, Cards CardFromDB);
    Task UpdateStatusCardGalleryAsync(string Id);
    Task UpdateStarCardGalleryAsync(string id, double star);
    Task UpdateCardGalleryPowerAsync(string id, Cards CardFromDB);
    Task<Cards> SumPowerCardsGalleryAsync();
}