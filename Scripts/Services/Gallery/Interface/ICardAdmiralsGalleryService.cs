using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICardAdmiralsGalleryService
{
    Task<List<CardAdmirals>> GetCardAdmiralsCollectionAsync(string type, int pageSize, int offset, string rare);
    Task<int> GetCardAdmiralsCountAsync(string type, string rare);
    Task InsertCardAdmiralGalleryAsync(string Id);
    Task UpdateStatusCardAdmiralGalleryAsync(string Id);
    Task UpdateStarCardAdmiralGalleryAsync(string Id, double star);
    Task UpdateCardAdmiralGalleryPowerAsync(string Id);
    Task<CardAdmirals> SumPowerCardAdmiralsGalleryAsync();
}