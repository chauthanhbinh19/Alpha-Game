using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardLivesGalleryRepository
{
    Task<List<CardLives>> GetCardLivesCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardLivesCountAsync(string search, string type, string rare);
    Task InsertCardLifeGalleryAsync(string userId, string Id, CardLives CardLifeFromDB);
    Task UpdateStatusCardLifeGalleryAsync(string userId, string Id);
    Task UpdateStarCardLifeGalleryAsync(string userId, string Id, double star);
    Task UpdateCardLifeGalleryPowerAsync(string userId, string Id, CardLives CardLifeFromDB);
    Task<CardLives> SumPowerCardLivesGalleryAsync(string userId);
}