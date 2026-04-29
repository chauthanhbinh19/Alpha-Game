using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardLivesGalleryRepository
{
    Task<List<CardLives>> GetCardLivesCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardLivesCountAsync(string search, string type, string rare);
    Task InsertCardLifeGalleryAsync(string Id, CardLives CardLifeFromDB);
    Task UpdateStatusCardLifeGalleryAsync(string Id);
    Task UpdateStarCardLifeGalleryAsync(string Id, double star);
    Task UpdateCardLifeGalleryPowerAsync(string Id, CardLives CardLifeFromDB);
    Task<CardLives> SumPowerCardLivesGalleryAsync();
}