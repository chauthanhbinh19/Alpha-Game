using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardAdmiralsGalleryRepository
{
    Task<List<CardAdmirals>> GetCardAdmiralsCollectionAsync(string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardAdmiralsCountAsync(string search, string type, string rare);
    Task InsertCardAdmiralGalleryAsync(string Id, CardAdmirals CardAdmiralFromDB);
    Task UpdateStatusCardAdmiralGalleryAsync(string Id);
    Task UpdateStarCardAdmiralGalleryAsync(string Id, double star);
    Task UpdateCardAdmiralGalleryPowerAsync(string Id, CardAdmirals CardAdmiralFromDB);
    Task<CardAdmirals> SumPowerCardAdmiralsGalleryAsync();
}