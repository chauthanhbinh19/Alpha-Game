using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardGeneralsGalleryService
{
    Task<List<CardGenerals>> GetCardGeneralsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardGeneralsCountAsync(string search, string type, string rare);
    Task InsertCardGeneralGalleryAsync(string Id);
    Task UpdateStatusCardGeneralGalleryAsync(string Id);
    Task UpdateStarCardGeneralGalleryAsync(string Id, double star);
    Task UpdateCardGeneralGalleryPowerAsync(string Id);
    Task<CardGenerals> SumPowerCardGeneralsGalleryAsync();
}