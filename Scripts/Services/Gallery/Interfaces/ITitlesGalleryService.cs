using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITitlesGalleryService
{
    Task<List<Titles>> GetTitlesCollectionAsync(string userId, string search, int pageSize, int offset, string rare);
    Task<int> GetTitlesCountAsync(string search, string rare);
    Task InsertTitleGalleryAsync(string userId, string Id);
    Task UpdateStatusTitleGalleryAsync(string userId, string Id);
    Task UpdateStarTitleGalleryAsync(string userId, string id, double star);
    Task UpdateTitleGalleryPowerAsync(string userId, string id);
    Task<Titles> SumPowerTitlesGalleryAsync(string userId);
}