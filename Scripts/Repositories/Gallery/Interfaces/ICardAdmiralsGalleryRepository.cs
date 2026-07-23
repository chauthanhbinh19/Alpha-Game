using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardAdmiralsGalleryRepository
{
    Task<List<CardAdmirals>> GetCardAdmiralsCollectionAsync(string userId, string search, string type, int pageSize, int offset, string rare);
    Task<int> GetCardAdmiralsCountAsync(string search, string type, string rare);
    Task InsertCardAdmiralGalleryAsync(string userId, string Id, CardAdmirals CardAdmiralFromDB);
    Task UpdateStatusCardAdmiralGalleryAsync(string userId, string Id);
    Task UpdateStarCardAdmiralGalleryAsync(string userId, string Id, double star);
    Task UpdateCardAdmiralGalleryPowerAsync(string userId, string Id, CardAdmirals CardAdmiralFromDB);
    Task<CardAdmirals> SumPowerCardAdmiralsGalleryAsync(string userId);
}