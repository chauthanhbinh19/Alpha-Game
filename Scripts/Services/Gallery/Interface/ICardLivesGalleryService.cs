using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardLivesGalleryService
{
    Task<List<CardLives>> GetCardLivesCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetCardLivesCountAsync(string type, string rare);
    Task InsertCardLifeGalleryAsync(string Id);
    Task UpdateStatusCardLifeGalleryAsync(string Id);
    Task UpdateStarCardLifeGalleryAsync(string Id, double star);
    Task UpdateCardLifeGalleryPowerAsync(string Id);
    Task<CardLives> SumPowerCardLivesGalleryAsync();
}