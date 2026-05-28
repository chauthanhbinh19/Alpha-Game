using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardGeneralsGalleryRepository
{
    Task<List<CardGenerals>> GetCardGeneralsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardGeneralsCountAsync(string search, string type, string rare);
    Task InsertCardGeneralGalleryAsync(string Id, CardGenerals CardGeneralFromDB);
    Task UpdateStatusCardGeneralGalleryAsync(string Id);
    Task UpdateStarCardGeneralGalleryAsync(string Id, double star);
    Task UpdateCardGeneralGalleryPowerAsync(string Id, CardGenerals CardGeneralFromDB);
    Task<CardGenerals> SumPowerCardGeneralsGalleryAsync();
}