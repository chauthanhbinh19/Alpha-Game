using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardsGalleryRepository
{
    Task<List<Cards>> GetCardsCollectionAsync(int pageSize, int offset, string rare);
    Task<int> GetCardsCountAsync(string rare);
    Task InsertCardGalleryAsync(string Id, Cards CardFromDB);
    Task UpdateStatusCardGalleryAsync(string Id);
    Task UpdateStarCardGalleryAsync(string id, double star);
    Task UpdateCardGalleryPowerAsync(string id, Cards CardFromDB);
    Task<Cards> SumPowerCardsGalleryAsync();
}