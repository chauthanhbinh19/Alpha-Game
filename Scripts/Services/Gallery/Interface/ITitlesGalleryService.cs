using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITitlesGalleryService
{
    Task<List<Titles>> GetTitlesCollectionAsync(string search, int pageSize, int offset, string rare);
    Task<int> GetTitlesCountAsync(string search, string rare);
    Task InsertTitleGalleryAsync(string Id);
    Task UpdateStatusTitleGalleryAsync(string Id);
    Task UpdateStarTitleGalleryAsync(string id, double star);
    Task UpdateTitleGalleryPowerAsync(string id);
    Task<Titles> SumPowerTitlesGalleryAsync();
}