using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardGeneralsGalleryRepository
{
    Task<List<CardGenerals>> GetCardGeneralsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardGeneralsCountAsync(string search, string type, string rare);
    Task InsertCardGeneralGalleryAsync(string userId, string Id, CardGenerals CardGeneralFromDB);
    Task UpdateStatusCardGeneralGalleryAsync(string userId, string Id);
    Task UpdateStarCardGeneralGalleryAsync(string userId, string Id, double star);
    Task UpdateCardGeneralGalleryPowerAsync(string userId, string Id, CardGenerals CardGeneralFromDB);
    Task<CardGenerals> SumPowerCardGeneralsGalleryAsync(string userId);
}